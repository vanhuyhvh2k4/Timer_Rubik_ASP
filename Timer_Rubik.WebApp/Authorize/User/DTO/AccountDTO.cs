namespace Timer_Rubik.WebApp.Authorize.User.DTO
{
    public class GetAccountDTO_User
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Thumbnail { get; set; }

        public string Email { get; set; }
    }

    public class UpdateAccountDTO_User
    {
        public string Name { get; set; }

        public string Thumbnail { get; set; }

        public string Password { get; set; }
    }
}
