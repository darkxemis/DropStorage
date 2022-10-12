namespace DropStorage.WebApi.DataModel.Extensions
{
    public static class ByteArrayExtensions
    {
        public static bool IsByteArrayEqual(this byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length)
            {
                return false;
            }

            bool areEqual = true;
            for (int i = 0; i < array1.Length; i++)
            {
                areEqual &= array1[i] == array2[i];
            }

            // If you stop as soon as the arrays don't match you'll be disclosing information about how different they are by the time it takes to compare them
            // this way no information is disclosed
            return areEqual;
        }

        public static uint ConvertFromNetworOrder(this byte[] reversedUint)
        {
            return BitConverter.ToUInt32(reversedUint.Reverse().ToArray(), 0);
        }
    }
}
