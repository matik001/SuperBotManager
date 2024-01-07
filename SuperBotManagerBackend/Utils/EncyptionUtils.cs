using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Cryptography;

namespace SuperBotManagerBackend.Utils
{
    public static class EncyptionUtils
    {
        public static byte[] EncryptAES(string value, byte[] key, byte[] IV)
        {
            byte[] encrypted;
            using(Aes aes = Aes.Create())
            {
                ICryptoTransform encryptor = aes.CreateEncryptor(key, IV);
                using(MemoryStream ms = new MemoryStream())
                {
                    using(CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using(StreamWriter sw = new StreamWriter(cs))
                            sw.Write(value);
                        encrypted = ms.ToArray();
                    }
                }
            }
            return encrypted;
        }
        public static string DecryptAES(byte[] cipherText, byte[] Key, byte[] IV)
        {
            string plaintext = null;
            using(var aes = Aes.Create())
            {
                ICryptoTransform decryptor = aes.CreateDecryptor(Key, IV);
                using(MemoryStream ms = new MemoryStream(cipherText))
                {
                    using(CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using(StreamReader reader = new StreamReader(cs))
                            plaintext = reader.ReadToEnd();
                    }
                }
            }
            return plaintext;
        }

        public static byte[] GenerateIV()
        {
            using(var aes = Aes.Create())
            {
                aes.GenerateIV();
                return aes.IV;
            }
        }
    }
}
