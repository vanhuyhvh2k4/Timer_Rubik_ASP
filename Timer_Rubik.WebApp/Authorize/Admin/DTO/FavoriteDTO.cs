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
}
