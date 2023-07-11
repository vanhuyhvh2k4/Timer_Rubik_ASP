namespace Timer_Rubik.WebApp.Models
{
    public class Account
    {
        public Guid Id { get; set; }

        public Guid RuleId { get; set; }

        public string Name { get; set; }

        public string Thumbnail { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public Rule Rule { get; set; }

        public ICollection<Scramble> Scrambles { get; set; }

        public ICollection<Favorite> Favorites { get; set; }
    }
}
