using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Timer = System.Timers.Timer; 

namespace RsaKeys
{
    public static class RsaEnc
    {
        public static byte[] Encrypt(byte[] data, RSAParameters rsaKey, bool doPadding)
        {
            try
            {
                using (var rsa = new RSACryptoServiceProvider())
                {
                    rsa.ImportParameters(rsaKey);
                    var endData = rsa.Encrypt(data, doPadding);
                    return endData;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public static byte[] Decrypt(byte[] data, RSAParameters rsaKey, bool doPadding)
        {
            try
            {
                byte[] decryptData;
                using (var rsa = new RSACryptoServiceProvider())
                {
                    rsa.ImportParameters(rsaKey);
                   decryptData = rsa.Decrypt(data, doPadding);
                    return decryptData;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }

    public static class Program
    {
        private static void Main()
        {
            var encoding = new UTF8Encoding();
            var rsa = new RSACryptoServiceProvider();

            Console.WriteLine("Enter text to encrypt");
            var text = Console.ReadLine();
            if (string.IsNullOrEmpty(text)) return;
            var plainText = encoding.GetBytes(text);

            var rsaParam = rsa.ExportParameters(false);
            var cypherText = RsaEnc.Encrypt(plainText, rsa.ExportParameters(false), false);
            var encryptedText = encoding.GetString(cypherText);

            Console.WriteLine($" Encrypted Text: {encryptedText}");
            Console.ReadLine();

            var decryptedText = RsaEnc.Decrypt(cypherText, rsa.ExportParameters(true), false);

            Console.WriteLine($" Decrypted Text: {encoding.GetString(decryptedText)}");
            Console.ReadLine();

        }
    }
}
