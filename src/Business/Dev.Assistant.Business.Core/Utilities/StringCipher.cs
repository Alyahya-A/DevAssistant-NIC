using System.Security.Cryptography;
using System.Text;

namespace Dev.Assistant.Business.Core.Utilities;

public static class StringCipher
{
    private const string EncryptionKey = "U.are.U@2022";

    //public static string Encrypt(string plainText)
    //{
    //    if (string.IsNullOrWhiteSpace(plainText))
    //        return plainText;

    //    try
    //    {
    //        byte[] clearBytes = Encoding.Unicode.GetBytes(plainText);

    //        using Aes encryptor = Aes.Create();
    //        Rfc2898DeriveBytes pdb = new(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
    //        encryptor.Key = pdb.GetBytes(32);
    //        encryptor.IV = pdb.GetBytes(16);

    //        using MemoryStream ms = new();

    //        using (CryptoStream cs = new(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
    //        {
    //            cs.Write(clearBytes, 0, clearBytes.Length);
    //        }

    //        plainText = Convert.ToBase64String(ms.ToArray());
    //    }
    //    catch (Exception)
    //    {
    //    }

    //    return plainText;
    //}

    //public static string Decrypt(string cipherText)
    //{
    //    if (string.IsNullOrWhiteSpace(cipherText))
    //        return cipherText;

    //    try
    //    {
    //        cipherText = cipherText.Replace(" ", "+");
    //        byte[] cipherBytes = Convert.FromBase64String(cipherText);

    //        using Aes encryptor = Aes.Create();
    //        Rfc2898DeriveBytes pdb = new(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
    //        encryptor.Key = pdb.GetBytes(32);
    //        encryptor.IV = pdb.GetBytes(16);

    //        using MemoryStream ms = new();

    //        using (CryptoStream cs = new(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
    //        {
    //            cs.Write(cipherBytes, 0, cipherBytes.Length);
    //        }

    //        cipherText = Encoding.Unicode.GetString(ms.ToArray());
    //    }
    //    catch (Exception)
    //    {
    //    }

    //    return cipherText;
    //}
}