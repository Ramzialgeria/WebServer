#nullable disable
using System;
using System.Security.Cryptography;
using System.Text;

namespace ClientMonitoring.Web.Services
{
    /// <summary>
    /// Builds an Activation.enc file for CommuneServer.
    /// Runs on central only: it needs the PRIVATE key.
    ///
    /// File layout (integers big-endian):
    ///   "CMAF" | version(1) | iterations(4) | salt(16) | iv(16) | length(4)
    ///   | AES-256-CBC("CMAP" + json utf-8) | RSA-2048/SHA-256 signature
    /// AES key = PBKDF2-HMAC-SHA256(activation code, salt, iterations).
    ///
    /// The reader is CommuneSecurity/Activation/ActivationFileReader.cs.
    /// Keep both in sync.
    /// </summary>
    public static class ActivationFileWriter
    {
        private const byte FileVersion = 1;
        private const int Iterations = 200000;
        private const int SaltSize = 16;
        private const int IvSize = 16;
        private const int KeySize = 32;

        // no 0/O or 1/I/L confusion
        private const string CodeAlphabet = "ABCDEFGHJKMNPQRSTUVWXYZ23456789";

        /// <summary>Random activation code like K7QD-M2XW-9PTE-4HNC</summary>
        public static string GenerateCode()
        {
            byte[] rnd = RandomBytes(16);
            var raw = new char[16];

            for (int i = 0; i < raw.Length; i++)
                raw[i] = CodeAlphabet[rnd[i] % CodeAlphabet.Length];

            var s = new string(raw);

            return s.Substring(0, 4) + "-" + s.Substring(4, 4) + "-" +
                   s.Substring(8, 4) + "-" + s.Substring(12, 4);
        }

        public static byte[] Create(
            string json,
            string activationCode,
            string privateKeyXml)
        {
            string code = NormalizeCode(activationCode);
            if (code.Length < 12)
                throw new ArgumentException("Activation code is too short.");

            byte[] salt = RandomBytes(SaltSize);
            byte[] iv = RandomBytes(IvSize);

            byte[] key;
            using (var kdf = new Rfc2898DeriveBytes(
                Encoding.ASCII.GetBytes(code), salt, Iterations,
                HashAlgorithmName.SHA256))
            {
                key = kdf.GetBytes(KeySize);
            }

            byte[] plain = Combine(
                Encoding.ASCII.GetBytes("CMAP"),
                Encoding.UTF8.GetBytes(json));

            byte[] cipher;
            using (Aes aes = Aes.Create())
            {
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = key;
                aes.IV = iv;

                using (var enc = aes.CreateEncryptor())
                    cipher = enc.TransformFinalBlock(plain, 0, plain.Length);
            }

            Array.Clear(key, 0, key.Length);

            byte[] body = Combine(
                Encoding.ASCII.GetBytes("CMAF"),
                new[] { FileVersion },
                Int32BE(Iterations),
                salt,
                iv,
                Int32BE(cipher.Length),
                cipher);

            byte[] signature;
            using (RSA rsa = RSA.Create())
            {
                rsa.FromXmlString(privateKeyXml);
                signature = rsa.SignData(
                    body, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            }

            return Combine(body, signature);
        }

        public static string NormalizeCode(string code)
        {
            if (string.IsNullOrEmpty(code))
                return "";

            var sb = new StringBuilder();
            foreach (char c in code)
            {
                if (char.IsLetterOrDigit(c))
                    sb.Append(char.ToUpperInvariant(c));
            }
            return sb.ToString();
        }

        private static byte[] RandomBytes(int n)
        {
            var b = new byte[n];
            using (var rng = RandomNumberGenerator.Create())
                rng.GetBytes(b);
            return b;
        }

        private static byte[] Int32BE(int v)
        {
            return new[]
            {
                (byte)(v >> 24), (byte)(v >> 16), (byte)(v >> 8), (byte)v
            };
        }

        private static byte[] Combine(params byte[][] parts)
        {
            int len = 0;
            foreach (var p in parts) len += p.Length;

            var result = new byte[len];
            int pos = 0;
            foreach (var p in parts)
            {
                Buffer.BlockCopy(p, 0, result, pos, p.Length);
                pos += p.Length;
            }
            return result;
        }
    }
}
