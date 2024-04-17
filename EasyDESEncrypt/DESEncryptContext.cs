using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace EasyDESEncrypt
{
    public static class DESEncryptContext
    {
        private static byte[] keys = { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };
        private static string encryptKey;

        internal static void InitializeDESEncryptContext(string EncryptKey)
        {
            encryptKey = EncryptKey;
        }

        public static string DESEncrypt(string EncryptString)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 16));
                byte[] rgbIV = keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(EncryptString);
                var DCSP = Aes.Create();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch (Exception ex)
            {
                return ex.Message + EncryptString;
            }
        }

        public static string DESDecrypt(string DecryptString)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 16));
                byte[] rgbIV = keys;
                byte[] inputByteArray = Convert.FromBase64String(DecryptString);
                var DCSP = Aes.Create();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                Byte[] inputByteArrays = new byte[inputByteArray.Length];
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch (Exception ex)
            {
                return ex.Message + DecryptString;
            }
        }
    }
}
