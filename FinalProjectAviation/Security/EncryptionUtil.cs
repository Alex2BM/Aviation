namespace FinalProjectAviation.Security
{
    public class EncryptionUtil
    {
       public static string Encrypt(string input)
        {
            var encryptedPassword = BCrypt.Net.BCrypt.HashPassword(input);
            return encryptedPassword;
        }

        public static bool IsValidPassword(string input, string cryptText)
        {
            var isValid = BCrypt.Net.BCrypt.Verify(input, cryptText);
            return isValid; 
        }
    }
}
