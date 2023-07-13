
namespace Timer_Rubik.WebApp.Utils
{
    public class Password
    {
        public static string HashPassword(string password)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt(10);
            string passwordWithSalt = salt + password;
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(passwordWithSalt);
            return hashedPassword;
        }
    }
}
