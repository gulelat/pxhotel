using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace PX.Core.Ultilities
{
    class PasswordUtilities
    {
        public static string GetRandomString(int lenght = 8)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(
                Enumerable.Repeat(chars, lenght)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
        }

        public static string EncryptString(string toEncrypt, string key = "")
        {
            var toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);

            var hashmd5 = new MD5CryptoServiceProvider();
            var keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));
            //Always release the resources and flush data
            //of the Cryptographic service provide. Best Practice

            hashmd5.Clear();

            var tdes = new TripleDESCryptoServiceProvider
            {
                Key = keyArray,              //set the secret key for the tripleDES algorithm
                Mode = CipherMode.ECB,       //mode of operation. there are other 4 modes. We choose ECB(Electronic code Book)
                Padding = PaddingMode.PKCS7  //padding mode(if any extra byte added)
            };

            var cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            var resultArray = cTransform.TransformFinalBlock
                    (toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //Return the encrypted data into unreadable string format
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string DecryptString(string cipherString, string key = "")
        {
            var toEncryptArray = Convert.FromBase64String(cipherString);

            //if hashing was used get the hash code with regards to your key
            var hashmd5 = new MD5CryptoServiceProvider();
            var keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));
            hashmd5.Clear();

            var tdes = new TripleDESCryptoServiceProvider
            {
                Key = keyArray,               //set the secret key for the tripleDES algorithm
                Mode = CipherMode.ECB,        //mode of operation. there are other 4 modes. We choose ECB(Electronic code Book)
                Padding = PaddingMode.PKCS7   //padding mode(if any extra byte added)
            };

            var cTransform = tdes.CreateDecryptor();
            var resultArray = cTransform.TransformFinalBlock
                    (toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //return the Clear decrypted TEXT
            return Encoding.UTF8.GetString(resultArray);
        }
    }
}
