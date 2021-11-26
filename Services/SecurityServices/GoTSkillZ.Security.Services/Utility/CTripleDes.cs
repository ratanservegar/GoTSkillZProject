using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace GoTSkillZ.Security.Services.Utility
{
    public class CTripleDes
    {
        // define the triple des provider
        private readonly TripleDESCryptoServiceProvider _des = new TripleDESCryptoServiceProvider();

        // define the string handler
        private readonly UTF8Encoding _utf8 = new UTF8Encoding();

        // define the local property arrays
        private readonly byte[] _key;
        private readonly byte[] _iv;

        public CTripleDes(byte[] key, byte[] iv)
        {
            _key = key;
            _iv = iv;
        }

        public string Encrypt(string text)
        {
            byte[] input = _utf8.GetBytes(text);
            byte[] output = Transform(input, _des.CreateEncryptor(_key, _iv));
            return Convert.ToBase64String(output);
        }

        public string Decrypt(string text)
        {
            byte[] input = Convert.FromBase64String(text);
            byte[] output = Transform(input, _des.CreateDecryptor(_key, _iv));
            return _utf8.GetString(output);
        }

        private static byte[] Transform(byte[] input, ICryptoTransform cryptoTransform)
        {
            // create the necessary streams
            var memStream = new MemoryStream();
            var cryptStream = new CryptoStream(memStream, cryptoTransform, CryptoStreamMode.Write);

            // transform the bytes as requested
            cryptStream.Write(input, 0, input.Length);
            cryptStream.FlushFinalBlock();

            // Read the memory stream and
            // convert it back into byte array
            memStream.Position = 0;
            byte[] result = memStream.ToArray();

            // close and release the streams
            memStream.Close();
            cryptStream.Close();

            // hand back the encrypted buffer
            return result;
        }
    }
}
