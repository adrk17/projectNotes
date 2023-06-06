using System.Security.Cryptography;
using System.Text;

namespace projectNotes.Utils.Encryption
{
    /// <summary>
    /// Wrapper encryption class for hashing passwords
    /// </summary>
    public class PasswordHash
    {
        HashAlgorithm _algorithm;
        Encoding _encoding;
        public PasswordHash(string algorithmName, Encoding encoding)
        {
            _algorithm  = GetHashAlgorithm(algorithmName);
            _encoding = encoding;
        }

        public PasswordHash(string algorithmName) : this(algorithmName, Encoding.UTF8)
        {
        }



        /// <summary>
        /// HashPassword is used under the registration when we have to hash the password and convert it back to string in order to store in the database
        /// </summary>
        /// <param name="password"></param>
        /// <returns>string version of the password hash</returns>
        public string HashPassword(string password)
        {
            return _encoding.GetString(Hash(password));
        }
        private byte[] Hash(string password)
        {
            return _algorithm.ComputeHash(_encoding.GetBytes(password));
        }


        /// <summary>
        /// ComparePasswords is used under the login when we have to compare the password from the database with the one the user entered
        /// </summary>
        /// <param name="password"> password that the user inserted under the login process </param>
        /// <param name="passwordHash"> hash of the password from the database </param>
        /// <returns> boolean value, true if password hashes match, false if not </returns>
        public bool ComparePasswords(string password, string passwordHash)
        {
            return CompareHashes(_encoding.GetBytes(HashPassword(password)), _encoding.GetBytes(passwordHash));
        }

        private bool CompareHashes(byte[] hash1, byte[] hash2)
        {
            if (hash1.Length != hash2.Length)
            {
                return false;
            }
            for (int i = 0; i < hash1.Length; i++)
            {
                if (hash1[i] != hash2[i])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Creates an instance of the specified hash algorithm
        /// </summary>
        /// <param name="algorithmName">Supported values: sha256, sha512, md5</param>
        /// <returns>Instance of a hash algorithm</returns>
        /// <exception cref="ArgumentException">When the algorithm name does not mathc the supported algorithms' names</exception>
        private static HashAlgorithm GetHashAlgorithm(string algorithmName)
        {
            switch (algorithmName.ToUpper())
            {
                case "SHA256":
                    return SHA256.Create();
                case "SHA512":
                    return SHA512.Create();
                case "MD5":
                    return MD5.Create();
                default:
                    throw new ArgumentException("Algorithm not supported!");
            }
        }
    }
}
