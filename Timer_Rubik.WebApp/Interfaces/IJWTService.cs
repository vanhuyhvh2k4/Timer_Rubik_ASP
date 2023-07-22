namespace Timer_Rubik.WebApp.Interfaces
{
    public interface IJWTService
    {
        string GenerateAccessToken(string userId, string ruleId);
    }
}
