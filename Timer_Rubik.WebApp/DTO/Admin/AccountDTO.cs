namespace Timer_Rubik.WebApp.DTO.Admin
{
    public class LoginDTO
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }

    public class GetAccountDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Thumbnail { get; set; }

        public string Email { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }

    public class UpdateAccountDTO
    {
        public string Name { get; set; }

        public string Thumbnail { get; set; }
    }
}
