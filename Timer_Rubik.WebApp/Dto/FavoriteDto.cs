namespace Timer_Rubik.WebApp.Dto
{
    public class FavoriteDto
    {
        public Guid Id { get; set; }

        public Guid AccountId { get; set; }

        public Guid ScrambleId { get; set; }

        public float Time { get; set; }
    }
}
