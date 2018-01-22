using System;
using System.Security.Cryptography;
using System.Text;

namespace OctagonPlatform.Helpers
{
    public static class Cryptography
    {
        public static string GenerateKey()
        {
            const string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            var randNum = new Random();
            var chars = new char[100];
            var allowedCharCount = allowedChars.Length;
          
            for (var i = 0; i <= 100 - 1; i++)
            {
                chars[i] = allowedChars[Convert.ToInt32((allowedChars.Length) * randNum.NextDouble())];
            }
            return new string(chars);
        }

        public static string EncodePasswordMd5(string pass) //Encrypt using MD5    
        {
            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)    
            MD5 md5 = new MD5CryptoServiceProvider();
            var originalBytes = Encoding.Default.GetBytes(pass);
            var encodedBytes = md5.ComputeHash(originalBytes);
            //Convert encoded bytes back to a 'readable' string    
            return BitConverter.ToString(encodedBytes);
        }

        public static string EncodePassword(string pass, string key) //encrypt password    
        {
            var bytes = Encoding.Unicode.GetBytes(pass);
            var src = Encoding.Unicode.GetBytes(key);
            var dst = new byte[src.Length + bytes.Length];
            System.Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            System.Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);
            var algorithm = HashAlgorithm.Create("SHA1");
            var inArray = algorithm.ComputeHash(dst);
            //return Convert.ToBase64String(inArray);    
            return EncodePasswordMd5(Convert.ToBase64String(inArray));
        }
    }
}