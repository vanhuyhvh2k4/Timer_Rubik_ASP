namespace Timer_Rubik.WebApp.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(string toAddress, string subject, string body);
    }
}
