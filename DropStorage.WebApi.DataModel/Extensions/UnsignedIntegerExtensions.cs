namespace DropStorage.WebApi.DataModel.Extensions
{
    public static class UnsignedIntegerExtensions
    {
        public static byte[] ConvertToNetworkOrder(this uint number)
        {
            return BitConverter.GetBytes(number).Reverse().ToArray();
        }
    }
}
