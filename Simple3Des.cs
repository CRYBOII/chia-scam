using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.VisualBasic.CompilerServices;
using System.IO;

namespace spam_chai
{
    public sealed class Simple3Des
    {
        // Token: 0x0600006B RID: 107 RVA: 0x000044FC File Offset: 0x000026FC
        private byte[] TruncateHash(string key, int length)
        {
            HashAlgorithm hashAlgorithm = new SHA1CryptoServiceProvider();
            byte[] keyBytes = Encoding.Unicode.GetBytes(key);
            return (byte[])Utils.CopyArray(hashAlgorithm.ComputeHash(keyBytes), new byte[checked(length - 1 + 1)]);
        }

        // Token: 0x0600006C RID: 108 RVA: 0x00004534 File Offset: 0x00002734
        public Simple3Des(string key)
        {
            this.TripleDes = new TripleDESCryptoServiceProvider();
            this.TripleDes.Key = this.TruncateHash(key, this.TripleDes.KeySize / 8);
            this.TripleDes.IV = this.TruncateHash("", this.TripleDes.BlockSize / 8);
        }

        // Token: 0x0600006D RID: 109 RVA: 0x00004594 File Offset: 0x00002794
        public string EncryptData(string plaintext)
        {
            byte[] plaintextBytes = Encoding.Unicode.GetBytes(plaintext);
            MemoryStream ms = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(ms, this.TripleDes.CreateEncryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(plaintextBytes, 0, plaintextBytes.Length);
            cryptoStream.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }

        // Token: 0x0600006E RID: 110 RVA: 0x000045E0 File Offset: 0x000027E0
        public string DecryptData(string encryptedtext)
        {
            byte[] encryptedBytes = Convert.FromBase64String(encryptedtext);
            MemoryStream ms = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(ms, this.TripleDes.CreateDecryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(encryptedBytes, 0, encryptedBytes.Length);
            cryptoStream.FlushFinalBlock();
            return Encoding.Unicode.GetString(ms.ToArray());
        }

        // Token: 0x04000033 RID: 51
        private TripleDESCryptoServiceProvider TripleDes;
    }
}
