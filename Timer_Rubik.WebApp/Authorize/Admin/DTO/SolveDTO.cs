namespace Timer_Rubik.WebApp.Authorize.Admin.DTO
{
    public class GetSolveDTO_Admin
    {
        public Guid Id { get; set; }

        public Guid ScrambleId { get; set; }

        public string Answer { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
    
    public class CreateSolveDTO_Admin
    {
        public Guid ScrambleId { get; set; }

        public string Answer { get; set; }
    }

    public class UpdateSolveDTO_Admin
    {
        public Guid Id { get; set; }

        public string Answer { get; set; }
    }
}
