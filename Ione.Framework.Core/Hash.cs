using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Ione.Framework.Core
{
    /// <summary>
    /// Hash 
    /// Berisi fungsi-fungsi untuk encrypt/decrypt data
    /// </summary>
    public static class Hash
    {
        /// <summary>
        /// Encode menggunakan metode Base64
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        /// <summary>
        /// Decode menggunakan metode Base64
        /// </summary>
        /// <param name="base64EncodedData"></param>
        /// <returns></returns>
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        /// <summary>
        /// take any string and encrypt it using MD5 then
        /// return the encrypted data 
        /// </summary>
        /// <param name="data">input text you will enterd to encrypt it</param>
        /// <returns>return the encrypted text as hexadecimal string</returns>
        public static string GetMD5(string data)
        {
            //create new instance of md5
            MD5 md5 = MD5.Create();

            //convert the input text to array of bytes
            byte[] hashData = md5.ComputeHash(Encoding.Default.GetBytes(data));

            return Converter.ToHexStringUsingStringBuilder(hashData);

            ////create new instance of StringBuilder to save hashed data
            //StringBuilder returnValue = new StringBuilder();

            ////loop for each byte and add it to StringBuilder
            //for (int i = 0; i < hashData.Length; i++)
            //{
            //    returnValue.Append(hashData[i].ToString());
            //}

            //// return hexadecimal string
            //return returnValue.ToString();

        }

        /// <summary>
        /// take any string and encrypt it using SHA1 then
        /// return the encrypted data
        /// </summary>
        /// <param name="data">input text you will enterd to encrypt it</param>
        /// <returns>return the encrypted text as hexadecimal string</returns>
        public static string GetSHA1(string data)
        {
            //create new instance of md5
            SHA1 sha1 = SHA1.Create();

            //convert the input text to array of bytes
            byte[] hashData = sha1.ComputeHash(Encoding.Default.GetBytes(data));

            return Converter.ToHexStringUsingStringBuilder(hashData);

            ////create new instance of StringBuilder to save hashed data
            //StringBuilder returnValue = new StringBuilder();

            ////loop for each byte and add it to StringBuilder
            //for (int i = 0; i < hashData.Length; i++)
            //{
            //    returnValue.Append(hashData[i].ToString());
            //}

            //// return hexadecimal string
            //return returnValue.ToString();
        }
        /// <summary>
        /// encrypt input text using MD5 and compare it with
        /// the stored encrypted text
        /// </summary>
        /// <param name="inputData">input text you will enterd to encrypt it</param>
        /// <param name="storedHashData">the encrypted text
        ///         stored on file or database ... etc</param>
        /// <returns>true or false depending on input validation</returns>
        public static bool ValidateMD5(string inputData, string storedHashData)
        {
            //hash input text and save it string variable
            string getHashInputData = GetMD5(inputData);

            if (string.Compare(getHashInputData, storedHashData) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// encrypt input text using SHA1 and compare it with
        /// the stored encrypted text
        /// </summary>
        /// <param name="inputData">input text you will enterd to encrypt it</param>
        /// <param name="storedHashData">the encrypted
        ///           text stored on file or database ... etc</param>
        /// <returns>true or false depending on input validation</returns>

        public static bool ValidateSHA1(string inputData, string storedHashData)
        {
            //hash input text and save it string variable
            string getHashInputData = GetSHA1(inputData);

            if (string.Compare(getHashInputData, storedHashData) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //public static byte[] AESEncrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        //{
        //    byte[] encryptedBytes = null;

        //    // Set your salt here, change it to meet your flavor:
        //    // The salt bytes must be at least 8 bytes.
        //    byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        using (RijndaelManaged AES = new RijndaelManaged())
        //        {
        //            AES.KeySize = 256;
        //            AES.BlockSize = 128;

        //            var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
        //            AES.Key = key.GetBytes(AES.KeySize / 8);
        //            AES.IV = key.GetBytes(AES.BlockSize / 8);

        //            AES.Mode = CipherMode.CBC;

        //            using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
        //            {
        //                cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
        //                cs.Close();
        //            }
        //            encryptedBytes = ms.ToArray();
        //        }
        //    }

        //    return encryptedBytes;
        //}

        //public static byte[] AESDecrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        //{
        //    byte[] decryptedBytes = null;

        //    // Set your salt here, change it to meet your flavor:
        //    // The salt bytes must be at least 8 bytes.
        //    byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        using (RijndaelManaged AES = new RijndaelManaged())
        //        {
        //            AES.KeySize = 256;
        //            AES.BlockSize = 128;

        //            var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
        //            AES.Key = key.GetBytes(AES.KeySize / 8);
        //            AES.IV = key.GetBytes(AES.BlockSize / 8);

        //            AES.Mode = CipherMode.CBC;

        //            using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
        //            {
        //                cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
        //                cs.Close();
        //            }
        //            decryptedBytes = ms.ToArray();
        //        }
        //    }

        //    return decryptedBytes;
        //}

        public static byte[] AESEncrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;
            byte[] iv = Encoding.UTF8.GetBytes("1234567890123456");

            using (MemoryStream ms = new MemoryStream())
            {
                using (var AES = Aes.Create("AesManaged"))
                {
                    AES.Key = passwordBytes;
                    AES.IV = iv;
                    AES.Mode = CipherMode.CBC;
                    AES.Padding = PaddingMode.PKCS7;
                    AES.FeedbackSize = 128;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }
        public static byte[] AESDecrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;
            byte[] iv = Encoding.UTF8.GetBytes("1234567890123456");

            using (MemoryStream ms = new MemoryStream())
            {
                using (var AES = Aes.Create("AesManaged"))
                {
                    AES.Key = passwordBytes;
                    AES.IV = iv;
                    AES.Mode = CipherMode.CBC;
                    AES.Padding = PaddingMode.PKCS7;
                    AES.FeedbackSize = 128;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }

            return decryptedBytes;
        }
        public static string EncryptText(string key,string input)
        {
            try
            {
                // Get the bytes of the string
                byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(input);
                byte[] passwordBytes = Encoding.UTF8.GetBytes(key);

                // Hash the password with SHA256
                //passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

                byte[] bytesEncrypted = AESEncrypt(bytesToBeEncrypted, passwordBytes);

                //string result = Convert.ToBase64String(bytesEncrypted);
                //return result;

                return Converter.ToHexStringUsingStringBuilder(bytesEncrypted);

            }
            catch
            {
                return "";
            }
        }
        public static string DecryptText(string key, string input)
        {
            try
            {
                // Get the bytes of the string
                byte[] bytesToBeDecrypted = Converter.ToByteFromHex(input);
                byte[] passwordBytes = Encoding.UTF8.GetBytes(key);

                //passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

                byte[] bytesDecrypted = AESDecrypt(bytesToBeDecrypted, passwordBytes);

                string result = Encoding.UTF8.GetString(bytesDecrypted);

                return result;
            }
            catch
            {
                return "";
            }

        }
        public static string Password(string key, string input)
        {
            if (input.IsHex())
            {
                return GetSHA1(GetMD5(DecryptText(key,input)));
            }
            else
            {
                return GetSHA1(GetMD5(input));
            }
        }

        public static void EncryptFile()
        {
            string file = "C:\\SampleFile.DLL";
            string password = "abcd1234";

            byte[] bytesToBeEncrypted = File.ReadAllBytes(file);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            // Hash the password with SHA256
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesEncrypted = AESEncrypt(bytesToBeEncrypted, passwordBytes);

            string fileEncrypted = "C:\\SampleFileEncrypted.DLL";

            File.WriteAllBytes(fileEncrypted, bytesEncrypted);
        }
        public static void DecryptFile()
        {
            string fileEncrypted = "C:\\SampleFileEncrypted.DLL";
            string password = "abcd1234";

            byte[] bytesToBeDecrypted = File.ReadAllBytes(fileEncrypted);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesDecrypted = AESDecrypt(bytesToBeDecrypted, passwordBytes);

            string file = "C:\\SampleFile.DLL";
            File.WriteAllBytes(file, bytesDecrypted);
        }

        public static string TokenKey(this string input)
        {
            return GetMD5(input);
        }
        public static string TokenSecret()
        {
            return Guid.NewGuid().ToString("N");
        }
        public static string ConsumerKey(this string input, string token)
        {
            return Hash.GetMD5(input + token);
        }
        public static string ConsumerSecret()
        {
            return Guid.NewGuid().ToString("N");
        }
        public static DateTime Expires()
        {
            DateTime date = DateTime.Now;
            date = date.AddDays(31);
            return date;
        }
    }
}
