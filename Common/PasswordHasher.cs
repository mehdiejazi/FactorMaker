using System;
using System.Security.Cryptography;

namespace Common
{
    /// <summary>
    /// Salted PBKDF2 (HMAC-SHA256) password hashing.
    /// Format: PBKDF2$iterations$saltBase64$hashBase64
    /// Legacy unsalted SHA1 hex hashes are still verified so existing users can log in;
    /// callers should rehash on successful login when NeedsRehash returns true.
    /// </summary>
    public static class PasswordHasher
    {
        private const string Prefix = "PBKDF2";
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 100000;

        public static string Hash(string password)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));

            var salt = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            var hash = Derive(password, salt, Iterations);
            return string.Join("$", Prefix, Iterations.ToString(), Convert.ToBase64String(salt), Convert.ToBase64String(hash));
        }

        public static bool Verify(string storedHash, string password)
        {
            if (string.IsNullOrEmpty(storedHash) || password == null) return false;

            if (IsLegacy(storedHash))
            {
                var legacy = Utilities.HashSHA1(password);
                return FixedTimeEquals(
                    System.Text.Encoding.ASCII.GetBytes(storedHash.ToUpperInvariant()),
                    System.Text.Encoding.ASCII.GetBytes(legacy));
            }

            var parts = storedHash.Split('$');
            if (parts.Length != 4 || parts[0] != Prefix) return false;
            if (!int.TryParse(parts[1], out var iterations) || iterations <= 0) return false;

            byte[] salt, expected;
            try
            {
                salt = Convert.FromBase64String(parts[2]);
                expected = Convert.FromBase64String(parts[3]);
            }
            catch (FormatException)
            {
                return false;
            }

            var actual = Derive(password, salt, iterations, expected.Length);
            return FixedTimeEquals(actual, expected);
        }

        public static bool NeedsRehash(string storedHash)
        {
            if (string.IsNullOrEmpty(storedHash) || IsLegacy(storedHash)) return true;
            var parts = storedHash.Split('$');
            return parts.Length != 4 || parts[0] != Prefix || parts[1] != Iterations.ToString();
        }

        private static bool IsLegacy(string storedHash)
        {
            return !storedHash.StartsWith(Prefix + "$", StringComparison.Ordinal);
        }

        private static byte[] Derive(string password, byte[] salt, int iterations, int length = HashSize)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256))
            {
                return pbkdf2.GetBytes(length);
            }
        }

        private static bool FixedTimeEquals(byte[] a, byte[] b)
        {
            if (a.Length != b.Length) return false;
            var diff = 0;
            for (var i = 0; i < a.Length; i++)
            {
                diff |= a[i] ^ b[i];
            }
            return diff == 0;
        }
    }
}
