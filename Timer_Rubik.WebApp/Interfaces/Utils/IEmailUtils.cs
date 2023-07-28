namespace Timer_Rubik.WebApp.Interfaces.Utils
{
    public interface IEmailUtils
    {
        void SendEmail(string toAddress, string subject, string body);

        bool EmailValid(string email);
    }
}
