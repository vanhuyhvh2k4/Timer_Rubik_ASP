namespace Timer_Rubik.WebApp.Models
{
    public class Scramble
    {
        public Guid Id { get; set; }

        public string Algorithm { get; set; }

        public string Thumbnail { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
