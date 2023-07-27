using Timer_Rubik.WebApp.Interfaces.Utils;
using BcryptNet = BCrypt.Net.BCrypt;

namespace Timer_Rubik.WebApp.Utilities
{
    public class PasswordUtils : IPasswordUtils
    {
        public string GenerateRandomPassword(int length)
        {
            string[] array = new string[] { "A", "B", "C", "D", "E", "F", "a", "b", "c", "e", "f", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            string randomPassword = "";

            for (int dem = 0; dem < length; dem++)
            {
                var random = new Random();
                int randomNumber = random.Next(0, array.Length);
                randomPassword += array[randomNumber];
            }

            return randomPassword;
        }

        public string HashPassword(string password)
        {
            string hashedPassword = BcryptNet.HashPassword(password, BcryptNet.GenerateSalt(10));
            return hashedPassword;
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            bool isPasswordCorrect = BcryptNet.Verify(password, hashedPassword);
            return isPasswordCorrect;
        }
    }
}
