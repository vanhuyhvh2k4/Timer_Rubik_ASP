namespace Timer_Rubik.WebApp.Interfaces.Utils
{
    public interface IJWTService
    {
        string GenerateAccessToken(string userId, string ruleId);
    }
}
