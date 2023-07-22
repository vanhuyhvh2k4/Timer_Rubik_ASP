namespace Timer_Rubik.WebApp.Authorize.General.DTO
{
    public class LoginRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }

    public class RegisterRequest
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Thumbnail { get; set; }
    }

    public class GetAccountDTO
    {
        public Guid Id { get; set; }

        public Guid RuleId { get; set; }

        public string Name { get; set; }

        public string Thumbnail { get; set; }

        public string Email { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }

    public class SendEmailDTO
    {
        public string Email { get; set; }
    }
}
