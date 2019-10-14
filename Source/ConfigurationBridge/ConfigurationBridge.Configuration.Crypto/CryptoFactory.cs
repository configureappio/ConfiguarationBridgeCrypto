using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ConfigurationBridge.Configuration.Crypto
{

    public class CryptoFactory : ICryptoFactory
    {
        public ICryptoAlgorithm Create<T>(string password, string salt) where T : SymmetricAlgorithm, new()
        {
            return new CryptoAlgorithm(password, salt, new T());
        }

        public ICryptoAlgorithm Create(Type type, string password, string salt)
        {
            var tempType = Activator.CreateInstance(type);

            if (!(tempType is SymmetricAlgorithm parsedType) ) throw new ArgumentException(nameof(type));

            return new CryptoAlgorithm(password, salt, parsedType);
        }

        /// <remarks>
        /// This class is based on code from https://bitlush.com/blog/symmetric-encryption-in-c-sharp
        /// </remarks>>
        private class CryptoAlgorithm : ICryptoAlgorithm 
        {
            private readonly ICryptoTransform _encryptor;

            private readonly ICryptoTransform _decryptor;

            public CryptoAlgorithm(string password, string salt, SymmetricAlgorithm algorithm)
            {
                if (password is null ) throw new ArgumentNullException(nameof(password));
                if (salt is null) throw new ArgumentNullException(nameof(salt));
                
                DeriveBytes rgb = new Rfc2898DeriveBytes(password, Encoding.Unicode.GetBytes(salt));
                var rgbKey = rgb.GetBytes(algorithm.KeySize >> 3);
                var rgbIv = rgb.GetBytes(algorithm.BlockSize >> 3);
                _encryptor = algorithm.CreateEncryptor(rgbKey, rgbIv);
                _decryptor = algorithm.CreateDecryptor(rgbKey, rgbIv);
            }

            public string Encrypt(string text)
            {
                if (string.IsNullOrWhiteSpace(text)) return string.Empty;

                using (var buffer = new MemoryStream())
                {
                    using (var stream = new CryptoStream(buffer, _encryptor, CryptoStreamMode.Write))
                    {
                        using (var writer = new StreamWriter(stream, Encoding.Unicode))
                        {
                            writer.Write(text);
                        }
                    }

                    return Convert.ToBase64String(buffer.ToArray());
                }
            }

            public string Decrypt(string text)
            {
                if (string.IsNullOrWhiteSpace(text)) return string.Empty;

                using (var buffer = new MemoryStream(Convert.FromBase64String(text)))
                {
                    using (var stream = new CryptoStream(buffer, _decryptor, CryptoStreamMode.Read))
                    {
                        using (var reader = new StreamReader(stream, Encoding.Unicode))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }

            public string HashKey(string plainText)
            {
                if (string.IsNullOrWhiteSpace(plainText)) return string.Empty;

                var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
                var hash = new SHA384CryptoServiceProvider();
                hash.Initialize();
                return Convert.ToBase64String(hash.ComputeHash(plainTextBytes));
            }
        }
    }
}
