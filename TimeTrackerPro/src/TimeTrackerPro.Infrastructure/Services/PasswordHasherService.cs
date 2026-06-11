using System.Security.Cryptography;
using System.Text;

namespace TimeTrackerPro.Infrastructure.Services
{
    public interface IPasswordHasherService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hash);
    }

    public class PasswordHasherService : IPasswordHasherService
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 10000;

        public string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Password cannot be empty");

            using (var salt = new Rfc2898DeriveBytes(password, SaltSize, Iterations, HashAlgorithmName.SHA256))
            {
                var hash = salt.GetBytes(HashSize);
                var saltBytes = salt.Salt;

                var hashWithSalt = new byte[SaltSize + HashSize];
                Array.Copy(saltBytes, 0, hashWithSalt, 0, SaltSize);
                Array.Copy(hash, 0, hashWithSalt, SaltSize, HashSize);

                return Convert.ToBase64String(hashWithSalt);
            }
        }

        public bool VerifyPassword(string password, string hash)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hash))
                return false;

            try
            {
                var hashBytes = Convert.FromBase64String(hash);
                var saltBytes = new byte[SaltSize];
                Array.Copy(hashBytes, 0, saltBytes, 0, SaltSize);

                using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, Iterations, HashAlgorithmName.SHA256))
                {
                    var hashToCompare = pbkdf2.GetBytes(HashSize);

                    for (int i = 0; i < HashSize; i++)
                    {
                        if (hashBytes[i + SaltSize] != hashToCompare[i])
                            return false;
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
