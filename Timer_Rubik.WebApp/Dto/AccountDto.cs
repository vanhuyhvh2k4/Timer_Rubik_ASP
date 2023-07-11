namespace Timer_Rubik.WebApp.Dto
{
    public class AccountDto
    {
        public Guid Id { get; set; }

        public Guid RuleId { get; set; }

        public string Name { get; set; }

        public string Thumbnail { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
