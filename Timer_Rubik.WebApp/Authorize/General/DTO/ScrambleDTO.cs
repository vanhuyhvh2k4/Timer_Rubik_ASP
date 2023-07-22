namespace Timer_Rubik.WebApp.Authorize.General.DTO
{
    public class GetScrambleDTO
    {
        public Guid Id { get; set; }

        public Guid CategoryId { get; set; }

        public Guid AccountId { get; set; }

        public string Algorithm { get; set; }

        public string Thumbnail { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }

    public class CreateScrambleDTO
    {
        public Guid CategoryId { get; set; }

        public Guid AccountId { get; set; }

        public string Algorithm { get; set; }

        public string Thumbnail { get; set; }
    }

    public class UpdateScrambleDTO
    {
        public Guid Id { get; set; }

        public Guid CategoryId { get; set; }

        public Guid AccountId { get; set; }

        public string Algorithm { get; set; }

        public string Thumbnail { get; set; }
    }
}
