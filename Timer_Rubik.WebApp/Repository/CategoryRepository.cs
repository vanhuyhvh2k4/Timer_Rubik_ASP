using Timer_Rubik.WebApp.Data;
using Timer_Rubik.WebApp.Interfaces;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }

        public bool CategoryExists(Guid categoryId)
        {
            return _context.Categories.Any(cate => cate.Id == categoryId);
        }

        public ICollection<Category> GetCategories()
        {
            return _context.Categories.OrderBy(cate => cate.Id).ToList();
        }

        public Category GetCategory(Guid categoryId)
        {
            return _context.Categories.Find(categoryId);
        }
    }
}
