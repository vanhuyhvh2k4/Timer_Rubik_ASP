namespace Timer_Rubik.WebApp.Authorize.Admin.DTO
{
    public class GetCategoryDTO_Admin
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }

    public class CreateCategoryDTO_Admin
    {
        public string Name { get; set; }
    }
    
    public class UpdateCategoryDTO_Admin
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
