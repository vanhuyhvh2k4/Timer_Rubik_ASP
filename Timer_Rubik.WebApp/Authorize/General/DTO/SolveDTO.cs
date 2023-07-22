namespace Timer_Rubik.WebApp.Authorize.General.DTO
{
    public class GetSolveDTO
    {
        public Guid Id { get; set; }

        public Guid ScrambleId { get; set; }

        public string Answer { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }

    public class CreateSolveDTO
    {
        public Guid ScrambleId { get; set; }

        public string Answer { get; set; }
    }

    public class UpdateSolveDTO
    {
        public Guid Id { get; set; }

        public string Answer { get; set; }
    }
}
