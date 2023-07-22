namespace Timer_Rubik.WebApp.Interfaces.Utils
{
    public interface IPasswordService
    {
        string HashPassword(string password);

        string GenerateRandomPassword(int length);

        bool VerifyPassword(string password, string hashedPassword);
    }
}
