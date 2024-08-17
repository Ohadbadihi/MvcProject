using Microsoft.AspNetCore.Mvc;
using MyProject.Models;
using MyProject.Repository;

namespace MyProject.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IRepository _repository;

        public CatalogController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Catalog()
        {
            var categories = await _repository.GetAllCategoriesAsync();
            var categoriesList = categories.Select(c => new { c.CategoryId, c.CategoryName }).ToList();

            ViewBag.Categories = categoriesList;

            if (!categoriesList.Any())
            {
                return RedirectToAction("Index", "Error");
            }

            var animals = await _repository.GetAllAnimalsAsync();

            if (!animals.Any())
            {
                return RedirectToAction("Index", "Error");
            }

            return View(animals);
        }

        [HttpPost]
        public async Task<IActionResult> Catalog(int? categoryId)
        {
            var categories = await _repository.GetAllCategoriesAsync();
            var categoriesList = categories.Select(c => new { c.CategoryId, c.CategoryName }).ToList();

            ViewBag.Categories = categoriesList;
            ViewBag.SelectedCategoryId = categoryId;

            IEnumerable<Animal> animals;

            if (categoryId.HasValue && categoryId.Value > 0)
            {
                animals = await _repository.GetAnimalsByCategoryAsync(categoryId.Value);
               
            }
            else
            {
                animals = await _repository.GetAllAnimalsAsync();
            }

            if (!animals.Any())
            {
                return RedirectToAction("Index", "Error");
            }

            return View(animals);
        }


        public async Task<IActionResult> AnimalDetails(int animalId)
        {
            var animal = await _repository.GetAnimalByIdAsync(animalId);

            if (animal == null)
            {
                return RedirectToAction("Index", "Error");
            }

            ViewBag.Comments = await _repository.GetAllValidCommentsOfAnAnimalAsync(animalId);
            return View(animal);
        }

        [HttpPost]
        public async Task<IActionResult> AnimalComment(string comment, int animalId)
        {
            if (string.IsNullOrEmpty(comment))
            {
                return RedirectToAction("Index", "Error");
            }

            await _repository.InsertCommentAsync(comment, animalId);

            return RedirectToAction("AnimalDetails", new { animalId = animalId });
        }
    }
}
