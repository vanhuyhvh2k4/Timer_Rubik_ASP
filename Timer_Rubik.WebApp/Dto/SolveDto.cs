namespace Timer_Rubik.WebApp.Dto
{
    public class SolveDto
    {
        public Guid Id { get; set; }

        public Guid ScrambleId { get; set; }

        public string Answer { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
