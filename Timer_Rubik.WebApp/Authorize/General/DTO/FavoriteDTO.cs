namespace Timer_Rubik.WebApp.Authorize.General.DTO
{
    public class GetFavoriteDTO
    {
        public Guid Id { get; set; }

        public Guid AccountId { get; set; }

        public Guid ScrambleId { get; set; }

        public float Time { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }

    public class CreateFavoriteDTO
    {
        public Guid AccountId { get; set; }

        public Guid ScrambleId { get; set; }

        public float Time { get; set; }
    }

    public class UpdateFavoriteDTO
    {
        public Guid Id { get; set; }

        public Guid AccountId { get; set; }

        public Guid ScrambleId { get; set; }

        public float Time { get; set; }
    }
}
