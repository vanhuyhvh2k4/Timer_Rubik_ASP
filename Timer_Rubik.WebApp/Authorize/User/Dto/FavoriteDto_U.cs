namespace Timer_Rubik.WebApp.Authorize.User.Dto
{
    public class FavoriteDto_U
    {
        public Guid Id { get; set; }

        public Guid AccountId { get; set; }

        public Guid ScrambleId { get; set; }

        public float Time { get; set; }
    }
}
