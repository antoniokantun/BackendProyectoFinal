using BackendProyectoFinal.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Application.Services
{
    public class PasswordService : IPasswordService
    {
        private const int SaltSize = 16; // 128 bits
        private const int KeySize = 32; // 256 bits
        private const int Iterations = 10000;
        private static readonly HashAlgorithmName HashAlgorithm = HashAlgorithmName.SHA256;
        private const char Delimiter = ':';

        public string HashPassword(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(SaltSize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                Iterations,
                HashAlgorithm,
                KeySize);

            return string.Join(
                Delimiter,
                Convert.ToBase64String(salt),
                Convert.ToBase64String(hash),
                Iterations,
                HashAlgorithm);
        }

        public bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            var parts = hashedPassword.Split(Delimiter);
            if (parts.Length != 4)
                return false;

            var salt = Convert.FromBase64String(parts[0]);
            var hash = Convert.FromBase64String(parts[1]);
            var iterations = int.Parse(parts[2]);
            var algorithm = new HashAlgorithmName(parts[3]);

            var providedHash = Rfc2898DeriveBytes.Pbkdf2(
                providedPassword,
                salt,
                iterations,
                algorithm,
                hash.Length);

            return CryptographicOperations.FixedTimeEquals(hash, providedHash);
        }
    }
}
