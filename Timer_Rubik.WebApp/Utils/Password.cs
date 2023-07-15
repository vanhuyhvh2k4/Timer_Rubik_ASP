using BcryptNet = BCrypt.Net.BCrypt;
namespace Timer_Rubik.WebApp.Utils
{
    public class Password
    {
        public static string HashPassword(string password)
        {
            string hashedPassword = BcryptNet.HashPassword(password, BcryptNet.GenerateSalt(10));
            return hashedPassword;
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            bool isPasswordCorrect = BcryptNet.Verify(password, hashedPassword);
            return isPasswordCorrect;
        }
    }
}
