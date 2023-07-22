namespace Timer_Rubik.WebApp.Authorize.User.DTO
{
    public class GetAccountDTO_User
    {
        public Guid Id { get; set; }

        public Guid RuleId { get; set; }

        public string Name { get; set; }

        public string Thumbnail { get; set; }

        public string Email { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }

    public class UpdateAccountDTO_User
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Thumbnail { get; set; }

        public string Password { get; set; }
    }
}
