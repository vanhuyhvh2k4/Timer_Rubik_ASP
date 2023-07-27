namespace Timer_Rubik.WebApp.DTO.Client
{
    public class CreateScrambleDTO
    {
        public Guid CategoryId { get; set; }

        public string Algorithm { get; set; }

        public string Thumbnail { get; set; }
    }

    public class UpdateScrambleDTO
    {
        public Guid CategoryId { get; set; }

        public string Algorithm { get; set; }

        public string Thumbnail { get; set; }
    }
}
