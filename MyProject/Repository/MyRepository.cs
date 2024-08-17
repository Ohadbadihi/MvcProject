using Microsoft.EntityFrameworkCore;
using MyProject.Data;
using MyProject.Models;

namespace MyProject.Repository
{
    public class MyRepository : IRepository
    {
        private readonly PetShopContext _context;

        public MyRepository(PetShopContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Animal>> GetAllAnimalsAsync()
        {
            return await _context.Animals
                .Include(a => a.Comments).ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<IEnumerable<CommentModel>> GetAllCommentsAsync()
        {
            return await _context.Comments!.ToListAsync();
        }

        public async Task InsertAnimalAsync(Animal animal)
        {           
            _context.Animals!.Add(animal);
            await _context.SaveChangesAsync();
        }

        public async Task InsertCategoryAsync(Category category)
        {
            _context.Categories!.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task InsertCommentAsync(string comment, int animalId)
        {
            CommentModel newComment = new CommentModel()
            {
                AnimalId = animalId,
                Comment = comment
            };

            _context.Comments!.Add(newComment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAnimalAsync(int animalId, Animal animal)
        {
            var animalInDB = await _context.Animals!.SingleAsync(a => a.AnimalId == animalId);
            animalInDB.Name = animal.Name;
            animalInDB.Age = animal.Age;
            animalInDB.PictureName = animal.PictureName;
            animalInDB.Description = animal.Description;
            animalInDB.CategoryId = animal.CategoryId;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(int categoryId, Category category)
        {
            var categoryInDB = await _context.Categories!.SingleAsync(c => c.CategoryId == categoryId);
            categoryInDB.CategoryName = category.CategoryName;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCommentAsync(int commentId, CommentModel comment)
        {
            var commentInDB = await _context.Comments!.SingleAsync(c => c.CommentId == commentId);
            commentInDB.Comment = comment.Comment;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAnimalAsync(int animalId)
        {
            var animal = await _context.Animals!.SingleAsync(a => a.AnimalId == animalId);
            _context.Animals!.Remove(animal);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            var category = await _context.Categories!.SingleAsync(c => c.CategoryId == categoryId);
            _context.Categories!.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCommentAsync(int commentId)
        {
            var comment = await _context.Comments!.SingleAsync(b => b.CommentId == commentId);
            _context.Comments!.Remove(comment);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Animal>> GetAnimalsByCategoryAsync(int categoryId)
        {
            return await _context.Animals
                .Where(a => a.CategoryId == categoryId).ToListAsync();
        }

        public async Task<Animal?> GetAnimalByIdAsync(int id)
        {
            return await _context.Animals
                    .Include(a => a.Category)
                    .Include(a => a.Comments)
                    .FirstOrDefaultAsync(a => a.AnimalId == id);
        }

        public async Task<IEnumerable<CommentModel>> GetAllValidCommentsOfAnAnimalAsync(int? animalId)
        {
            return await _context.Comments
                .Where(c => c.AnimalId == animalId && !string.IsNullOrEmpty(c.Comment))
                .ToListAsync();
        }
    }
}
