using System.Security.Cryptography;
using System.Text;

namespace proyecto_cs
{
    public static class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        public static bool VerifyPassword(string password, string storedHash)
        {
            using var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            string computedHash = Convert.ToBase64String(bytes);

            return storedHash == computedHash;
        }
    }
}

// Para que se usa este apartado? se usa para poder dar una contraseña que es cifrada y no es la verdadera para que no se filtre la que es real