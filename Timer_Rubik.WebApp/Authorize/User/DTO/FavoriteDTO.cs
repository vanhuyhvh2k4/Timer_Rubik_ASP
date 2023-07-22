namespace Timer_Rubik.WebApp.Authorize.User.DTO
{
    public class CreateFavoriteDTO_User
    {
        public Guid AccountId { get; set; }

        public Guid ScrambleId { get; set; }

        public float Time { get; set; }
    }

    public class UpdateFavoriteDTO_User
    {
        public Guid Id { get; set; }

        public Guid AccountId { get; set; }

        public Guid ScrambleId { get; set; }

        public float Time { get; set; }
    }
}
