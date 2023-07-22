namespace Timer_Rubik.WebApp.Authorize.Admin.DTO
{
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
