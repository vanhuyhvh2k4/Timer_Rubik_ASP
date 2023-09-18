namespace Timer_Rubik.WebApp.DTO.Admin
{
    public class GetScrambleDTO
    {
        public Guid Id { get; set; }

        public dynamic Category { get; set; }

        public dynamic Account { get; set; }

        public string Algorithm { get; set; }

        public string Solve { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }

    public class CreateScrambleDTO
    {
        public Guid CategoryId { get; set; }

        public string Algorithm { get; set; }

        public string Solve { get; set; }
    }
}
