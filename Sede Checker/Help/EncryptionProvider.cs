using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Sede_Checker.Help
{
    internal static class EncryptionProvider
    {
        public const string INIT_VECTOR = "pOWaTbO92LfXbh69JkYzfT7P465TNc0h";
        public const string SALT = "4Bvq75DG";

        public static string AESDecrypt(string enctyptedText, string passPhrase, string saltValue, string hashAlgorithm, int passwordIterations, string initVector)
        {
            // Convert strings defining encryption key characteristics into byte
            // arrays. Let us assume that strings only contain ASCII codes.
            // If strings include Unicode characters, use Unicode, UTF7, or UTF8
            // encoding.
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

            // Convert our ciphertext into a byte array.
            var cipherTextBytes = Convert.FromBase64String(enctyptedText);

            // First, we must create a password, from which the key will be 
            // derived. This password will be generated from the specified 
            // passphrase and salt value. The password will be created using
            // the specified hash algorithm. Password creation can be done in
            // several iterations.
            //PasswordDeriveBytes password = new PasswordDeriveBytes(
            //                                                passPhrase,
            //                                                saltValueBytes,
            //                                                hashAlgorithm,
            //                                                passwordIterations);

            Rfc2898DeriveBytes password = new Rfc2898DeriveBytes(passPhrase, saltValueBytes, passwordIterations);

            // Use the password to generate pseudo-random bytes for the encryption
            // key. Specify the size of the key in bytes (instead of bits).
            
            // Create uninitialized Rijndael encryption object.
            RijndaelManaged symmetricKey = new RijndaelManaged();

            // It is reasonable to set encryption mode to Cipher Block Chaining
            // (CBC). Use default options for other symmetric key parameters.
            symmetricKey.BlockSize = 256;
            symmetricKey.KeySize = 256;
            symmetricKey.Padding = PaddingMode.Zeros;
            symmetricKey.Mode = CipherMode.CBC;

            byte[] keyBytes = password.GetBytes(symmetricKey.KeySize / 8);

            // Generate decryptor from the existing key bytes and initialization 
            // vector. Key size will be defined based on the number of the key 
            // bytes.
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(
                                                             keyBytes,
                                                             initVectorBytes);

            // Define memory stream which will be used to hold encrypted data.
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);

            // Define cryptographic stream (always use Read mode for encryption).
            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                          decryptor,
                                                          CryptoStreamMode.Read);

            // Since at this point we don't know what the size of decrypted data
            // will be, allocate the buffer long enough to hold ciphertext;
            // plaintext is never longer than ciphertext.
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            // Start decrypting.
            int decryptedByteCount = cryptoStream.Read(plainTextBytes,
                                                       0,
                                                       plainTextBytes.Length);

            // Close both streams.
            memoryStream.Close();
            cryptoStream.Close();

            // Convert decrypted data into a string. 
            // Let us assume that the original plaintext string was UTF8-encoded.
            string plainText = Encoding.UTF8.GetString(plainTextBytes,
                                                       0,
                                                       decryptedByteCount);

            // Return decrypted string.   
            return plainText;
        }

        public static string AESEncrypt(string plainText, string passPhrase, string saltValue, string hashAlgorithm, int passwordIterations, string initVector)
        {
            // Convert strings into byte arrays.
            // Let us assume that strings only contain ASCII codes.
            // If strings include Unicode characters, use Unicode, UTF7, or UTF8 
            // encoding.
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

            // Convert our plaintext into a byte array.
            // Let us assume that plaintext contains UTF8-encoded characters.
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            // First, we must create a password, from which the key will be derived.
            // This password will be generated from the specified passphrase and 
            // salt value. The password will be created using the specified hash 
            // algorithm. Password creation can be done in several iterations.
            //PasswordDeriveBytes password = new PasswordDeriveBytes(
            //                                                passPhrase,
            //                                                saltValueBytes,
            //                                                hashAlgorithm,
            //                                                passwordIterations);

            Rfc2898DeriveBytes password = new Rfc2898DeriveBytes(passPhrase, saltValueBytes, passwordIterations);

            // Use the password to generate pseudo-random bytes for the encryption
            // key. Specify the size of the key in bytes (instead of bits).
            

            // Create uninitialized Rijndael encryption object.
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.BlockSize = 256;
            symmetricKey.KeySize = 256;
            symmetricKey.Padding = PaddingMode.Zeros;
            // It is reasonable to set encryption mode to Cipher Block Chaining
            // (CBC). Use default options for other symmetric key parameters.
            symmetricKey.Mode = CipherMode.CBC;

            byte[] keyBytes = password.GetBytes(symmetricKey.KeySize / 8);
            
            // Generate encryptor from the existing key bytes and initialization 
            // vector. Key size will be defined based on the number of the key 
            // bytes.
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(
                                                             keyBytes,
                                                             initVectorBytes);

            // Define memory stream which will be used to hold encrypted data.
            MemoryStream memoryStream = new MemoryStream();

            // Define cryptographic stream (always use Write mode for encryption).
            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                         encryptor,
                                                         CryptoStreamMode.Write);
            // Start encrypting.
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

            // Finish encrypting.
            cryptoStream.FlushFinalBlock();

            // Convert our encrypted data from a memory stream into a byte array.
            byte[] cipherTextBytes = memoryStream.ToArray();

            // Close both streams.
            memoryStream.Close();
            cryptoStream.Close();

            // Convert encrypted data into a base64-encoded string.
            string cipherText = Convert.ToBase64String(cipherTextBytes);

            // Return encrypted string.
            return cipherText;
        }

        public static string EncryptText(string text, string password)
        {
            return AESEncrypt(text, password, SALT, "SHA1", 1000, INIT_VECTOR);
        }

        public static string DecryptText(string encryptedText, string password)
        {
            return AESDecrypt(encryptedText, password, SALT, "SHA1", 1000, INIT_VECTOR);
        }

        public static void DecryptFile(string inputFile, string outputFile, string skey)
        {

            using (RijndaelManaged aes = new RijndaelManaged())
            {
                aes.BlockSize = 256;
                aes.KeySize = 256;
                byte[] key = ASCIIEncoding.UTF8.GetBytes(skey);
                byte[] IV = ASCIIEncoding.UTF8.GetBytes(skey);

                using (FileStream fsCrypt = new FileStream(inputFile, FileMode.Open))
                {
                    using (FileStream fsOut = new FileStream(outputFile, FileMode.Create))
                    {
                        using (ICryptoTransform decryptor = aes.CreateDecryptor(key, IV))
                        {
                            using (CryptoStream cs = new CryptoStream(fsCrypt, decryptor, CryptoStreamMode.Read))
                            {
                                int data;
                                while ((data = cs.ReadByte()) != -1)
                                {
                                    fsOut.WriteByte((byte)data);
                                }
                            }
                        }
                    }
                }
            }
        }

        public static MemoryStream DecryptFile(MemoryStream inputStream, string skey)
        {
            var result = new MemoryStream();
            using (var aes = new RijndaelManaged())
            {
                aes.BlockSize = 256;
                aes.KeySize = 256;
                var key = Encoding.UTF8.GetBytes(skey);
                var IV = Encoding.UTF8.GetBytes(skey);

                using (var decryptor = aes.CreateDecryptor(key, IV))
                {
                    using (var cs = new CryptoStream(inputStream, decryptor, CryptoStreamMode.Read))
                    {
                        int data;
                        while ((data = cs.ReadByte()) != -1)
                        {
                            result.WriteByte((byte)data);
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Get string MD5 hash
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string MD5(string text)
        {
            return new MD5CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes(text)).Aggregate(string.Empty, (current, b) => current + b.ToString("X2"));
        }
    }
}