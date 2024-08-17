using MyProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyProject.Repository
{
    public interface IRepository
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();

        Task<IEnumerable<Animal>> GetAllAnimalsAsync();

        Task<IEnumerable<CommentModel>> GetAllCommentsAsync();

        Task InsertCategoryAsync(Category category);

        Task InsertAnimalAsync(Animal animal);

        Task InsertCommentAsync(string comment, int animalId);

        Task UpdateCategoryAsync(int categoryId, Category category);

        Task UpdateAnimalAsync(int animalId, Animal animal);

        Task UpdateCommentAsync(int commentId, CommentModel comment);

        Task DeleteCategoryAsync(int categoryId);

        Task DeleteAnimalAsync(int animalId);

        Task DeleteCommentAsync(int commentId);

        Task<IEnumerable<Animal>> GetAnimalsByCategoryAsync(int categoryId);

        Task<Animal?> GetAnimalByIdAsync(int id);

        Task<IEnumerable<CommentModel>> GetAllValidCommentsOfAnAnimalAsync(int? animalId);
    }
}
