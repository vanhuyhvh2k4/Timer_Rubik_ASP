using Timer_Rubik.WebApp.Authorize.Admin.Interfaces;
using Timer_Rubik.WebApp.Data;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Authorize.Admin.Repository
{
    public class CategoryRepository_Admin : ICategoryRepository_Admin
    {
        private readonly DataContext _context;

        public CategoryRepository_Admin(DataContext context)
        {
            _context = context;
        }

        public bool CreateCategory(Category category)
        {
            var newCategory = new Category()
            {
                Id = new Guid(),
                Name = category.Name,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.MinValue,
            };

            _context.Categories.Add(newCategory);

            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCategory(Category category)
        {
            var updateCategory = _context.Categories.Where(cate => cate.Id == category.Id).FirstOrDefault();

            updateCategory.Name = category.Name;
            updateCategory.UpdatedAt = DateTime.Now;
            return Save();
        }
    }
}
