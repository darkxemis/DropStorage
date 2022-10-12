using System.Security.Cryptography;
using DropStorage.WebApi.DataModel.Extensions;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace DropStorage.WebApi.DataModel.Security
{
    public static class Hasher
    {
        public static string GenerateIdentityV3Hash(string password, KeyDerivationPrf prf = KeyDerivationPrf.HMACSHA256, int iterationCount = 10000, int saltSize = 16)
        {
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                byte[] salt = new byte[saltSize];
                rng.GetBytes(salt);

                byte[] pbkdf2Hash = KeyDerivation.Pbkdf2(password, salt, prf, iterationCount, 32);
                return Convert.ToBase64String(ComposeIdentityV3Hash(salt, (uint)iterationCount, pbkdf2Hash));
            }
        }

        public static bool VerifyIdentityV3Hash(string password, string passwordHash)
        {
            var identityV3HashArray = Convert.FromBase64String(passwordHash);
            if (identityV3HashArray[0] != 1)
            {
                throw new InvalidOperationException("passwordHash is not Identity V3");
            }

            var prfAsArray = new byte[4];
            Buffer.BlockCopy(identityV3HashArray, 1, prfAsArray, 0, 4);
            var prf = (KeyDerivationPrf)prfAsArray.ConvertFromNetworOrder();

            var iterationCountAsArray = new byte[4];
            Buffer.BlockCopy(identityV3HashArray, 5, iterationCountAsArray, 0, 4);
            var iterationCount = (int)iterationCountAsArray.ConvertFromNetworOrder();

            var saltSizeAsArray = new byte[4];
            Buffer.BlockCopy(identityV3HashArray, 9, saltSizeAsArray, 0, 4);
            var saltSize = (int)saltSizeAsArray.ConvertFromNetworOrder();

            var salt = new byte[saltSize];
            Buffer.BlockCopy(identityV3HashArray, 13, salt, 0, saltSize);

            var savedHashedPassword = new byte[identityV3HashArray.Length - 1 - 4 - 4 - 4 - saltSize];
            Buffer.BlockCopy(identityV3HashArray, 13 + saltSize, savedHashedPassword, 0, savedHashedPassword.Length);

            var hashFromInputPassword = KeyDerivation.Pbkdf2(password, salt, prf, iterationCount, 32);

            return hashFromInputPassword.IsByteArrayEqual(savedHashedPassword);
        }

        private static byte[] ComposeIdentityV3Hash(byte[] salt, uint iterationCount, byte[] passwordHash)
        {
            var hash = new byte[1 + 4/*KeyDerivationPrf value*/ + 4/*Iteration count*/ + 4/*salt size*/ + salt.Length /*salt*/ + 32 /*password hash size*/];
            hash[0] = 1; // Identity V3 marker

            Buffer.BlockCopy(((uint)KeyDerivationPrf.HMACSHA256).ConvertToNetworkOrder(), 0, hash, 1, sizeof(uint));
            Buffer.BlockCopy(iterationCount.ConvertToNetworkOrder(), 0, hash, 1 + sizeof(uint), sizeof(uint));
            Buffer.BlockCopy(((uint)salt.Length).ConvertToNetworkOrder(), 0, hash, 1 + (2 * sizeof(uint)), sizeof(uint));
            Buffer.BlockCopy(salt, 0, hash, 1 + (3 * sizeof(uint)), salt.Length);
            Buffer.BlockCopy(passwordHash, 0, hash, 1 + (3 * sizeof(uint)) + salt.Length, passwordHash.Length);

            return hash;
        }
    }
}
