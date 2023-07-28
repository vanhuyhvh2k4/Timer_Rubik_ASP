namespace Timer_Rubik.WebApp.DTO.Client
{
    public class GetFavoriteDTO
    {
        public Guid Id { get; set; }

        public dynamic Account { get; set; }

        public dynamic Scramble { get; set; }

        public float Time { get; set; }

        public DateTime CreatedAt { get; set; }
    }

    public class CreateFavoriteDTO
    {
        public Guid ScrambleId { get; set; }

        public float Time { get; set; }
    }
}
