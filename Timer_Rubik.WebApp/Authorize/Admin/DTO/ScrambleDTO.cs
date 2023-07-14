namespace Timer_Rubik.WebApp.Authorize.Admin.DTO
{
    public class GetScrambleDTO_Admin
    {
        public Guid Id { get; set; }

        public Guid CategoryId { get; set; }

        public Guid AccountId { get; set; }

        public string Algorithm { get; set; }

        public string Thumbnail { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
    
    public class CreateScrambleDTO_Admin
    {
        public Guid CategoryId { get; set; }

        public Guid AccountId { get; set; }

        public string Algorithm { get; set; }

        public string Thumbnail { get; set; }
    }
    
    public class UpdateScrambleDTO_Admin
    {
        public Guid Id { get; set; }

        public Guid CategoryId { get; set; }

        public Guid AccountId { get; set; }

        public string Algorithm { get; set; }

        public string Thumbnail { get; set; }
    }
}
