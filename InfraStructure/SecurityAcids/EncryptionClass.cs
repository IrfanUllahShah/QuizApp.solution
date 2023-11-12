using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace InfraStructure.SecurityAcids
{
    public static class EncryptionClass
    {

        public static string EnryptString(string strEncrypted)
        {
            byte[] b = Encoding.ASCII.GetBytes(strEncrypted);
            string encrypted = Convert.ToBase64String(b);
            return encrypted;
        }
        //public static string EncryptRoomCode(string roomCode)
        //{
        //    // Encryption key and initialization vector (IV)
        //    string encryptionKey = "irfanullahshah"; // Replace with your secret encryption key
        //    string iv = "11223344556677"; // IV must be 16 bytes long for AES

        //    // Convert key and IV to bytes
        //    byte[] keyBytes = Encoding.UTF8.GetBytes(encryptionKey);
        //    byte[] ivBytes = Encoding.UTF8.GetBytes(iv);

        //    // Convert room code to bytes
        //    byte[] roomCodeBytes = Encoding.UTF8.GetBytes(roomCode);

        //    // Create AES encryption algorithm
        //    using (Aes aesAlg = Aes.Create())
        //    {
        //        aesAlg.Key = keyBytes;
        //        aesAlg.IV = ivBytes;

        //        // Create an encryptor to perform the stream transform
        //        ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        //        // Create the streams used for encryption
        //        using (MemoryStream msEncrypt = new MemoryStream())
        //        {
        //            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        //            {
        //                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
        //                {
        //                    // Write the room code to the stream
        //                    swEncrypt.Write(roomCode);
        //                }
        //            }

        //            // Convert the encrypted data from a byte array to a base64-encoded string
        //            return Convert.ToBase64String(msEncrypt.ToArray());
        //        }
        //    }
        //}

    }
}
