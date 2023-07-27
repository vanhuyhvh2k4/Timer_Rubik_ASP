namespace Timer_Rubik.WebApp.Interfaces.Utils
{
    public interface IJWTUtils
    {
        string GenerateAccessToken(string userId, string ruleId);
    }
}
