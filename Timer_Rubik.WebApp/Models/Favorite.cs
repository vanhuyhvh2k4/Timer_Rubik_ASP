namespace Timer_Rubik.WebApp.Models
{
    public class Favorite
    {
        public Guid Id { get; set; }

        public Guid AccountId { get; set; }

        public Guid ScrambleId { get; set; }

        public long Time { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public Account Account { get; set; }

        public Scramble Scramble { get; set; }
    }
}
