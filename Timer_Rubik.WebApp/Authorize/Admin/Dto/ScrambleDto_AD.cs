namespace Timer_Rubik.WebApp.Authorize.Admin.Dto
{
    public class ScrambleDto_AD
    {
        public Guid Id { get; set; }

        public Guid CategoryId { get; set; }

        public Guid AccountId { get; set; }

        public string Algorithm { get; set; }

        public string Thumbnail { get; set; }
    }
}
