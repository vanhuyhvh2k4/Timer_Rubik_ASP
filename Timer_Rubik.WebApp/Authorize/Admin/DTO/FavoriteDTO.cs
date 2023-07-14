namespace Timer_Rubik.WebApp.Authorize.Admin.DTO
{
    public class GetFavoriteDTO_Admin
    {
        public Guid Id { get; set; }

        public Guid AccountId { get; set; }

        public Guid ScrambleId { get; set; }

        public float Time { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }

    public class CreateFavoriteDTO_Admin
    {
        public Guid AccountId { get; set; }

        public Guid ScrambleId { get; set; }

        public float Time { get; set; }
    }

    public class UpdateFavoriteDTO_Admin
    {
        public Guid Id { get; set; }

        public Guid AccountId { get; set; }

        public Guid ScrambleId { get; set; }

        public float Time { get; set; }
    }
}
