using System.Security.Cryptography;
using System.Text;

namespace KiraShopApi.Services
{
    public class AuthService
    {
        public string HashPassword(string password)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            string hashedInput = HashPassword(password);
            return hashedInput.Equals(hashedPassword, StringComparison.OrdinalIgnoreCase);
        }

        public string GenerateToken(int userId)
        {
            // Para simplicidade, vamos usar um token simples baseado no ID do usuário
            // Em produção, você deveria usar JWT ou outro método mais seguro
            return Convert.ToBase64String(Encoding.UTF8.GetBytes($"user_{userId}_{DateTime.UtcNow.Ticks}"));
        }

        public int? ValidateToken(string token)
        {
            try
            {
                string decoded = Encoding.UTF8.GetString(Convert.FromBase64String(token));
                if (decoded.StartsWith("user_"))
                {
                    string[] parts = decoded.Split('_');
                    if (parts.Length >= 2 && int.TryParse(parts[1], out int userId))
                    {
                        return userId;
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}

