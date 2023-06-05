using System.Security.Cryptography;

namespace projectNotes.Utils.Encryption
{
    /// <summary>
    /// Wrapper encryption clss for hashing passwords
    /// </summary>
    public class PasswordHash
    {
        HashAlgorithm _algorithm;
        public PasswordHash(string algorithmName)
        {
            _algorithm  = GetHashAlgorithm(algorithmName);
        }


        public byte[] Hash(string password)
        {
            return _algorithm.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }



        public bool CompareHashes(byte[] hash1, byte[] hash2)
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
