namespace Timer_Rubik.WebApp.Authorize.Admin.DTO
{
    public class GetRuleDTO_Admin
    {
        public Guid Id { get; set; }

        public string RoleName { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
