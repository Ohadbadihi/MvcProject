using Microsoft.AspNetCore.Mvc;
using MyProject.Models;
using MyProject.Repository;

namespace MyProject.Controllers
{
    public class AdministratorController : Controller
    {

        private readonly IRepository _repository;

        public AdministratorController(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Administrator(int? categoryId)
        {
            var categories = await _repository.GetAllCategoriesAsync();
            var categoriesList = categories.Select(c => new { c.CategoryId, c.CategoryName }).ToList();

            ViewBag.Categories = categoriesList;
            ViewBag.SelectedCategoryId = categoryId;

            if (!categoriesList.Any())
            {
                return RedirectToAction("Index", "Error");
            }

            IEnumerable<Animal> animals;

            if (categoryId.HasValue && categoryId.Value > 0)
            {
                animals = await _repository.GetAnimalsByCategoryAsync(categoryId.Value);
            }
            else
            {
                animals = await _repository.GetAllAnimalsAsync();
            }



            return View("Administrator", animals);
        }

        public async Task<IActionResult> DeleteAnimal(int animalId)
        {
            var animal = await _repository.GetAnimalByIdAsync(animalId);
            if (animal == null)
            {
                TempData["ErrorMessage"] = "Animal not found!";
                return RedirectToAction("Index", "Error");
            }

            await _repository.DeleteAnimalAsync(animalId);
            TempData["SuccessMessage"] = $"Animal {animal.Name} has been successfully deleted.";
            return RedirectToAction("Administrator");
        }

        [HttpGet]
        public async Task<IActionResult> AddAnimal()
        {
            var categories = await _repository.GetAllCategoriesAsync();

            if (!categories.Any())
            {
                return RedirectToAction("Index", "Error");
            }

            ViewBag.Categories = categories;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddAnimal(Animal animal, IFormFile pictureFile)
        {
            if (ModelState.IsValid)
            {
                if (pictureFile != null && pictureFile.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(pictureFile.FileName).ToLowerInvariant();
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await pictureFile.CopyToAsync(fileStream);
                    }
                    animal.PictureName = Path.Combine("Images", fileName); // Store the path including "Images"
                }
                else
                {
                    ModelState.AddModelError("PictureFile", "Please upload an animal picture");
                    var categories = await _repository.GetAllCategoriesAsync();
                    ViewBag.Categories = categories;
                    return View(animal);
                }

                await _repository.InsertAnimalAsync(animal); // Inserting the animal
                return RedirectToAction("Administrator");
            }

            var categoriesForView = await _repository.GetAllCategoriesAsync();
            ViewBag.Categories = categoriesForView;
            return View(animal);
        }

        [HttpGet]
        public async Task<IActionResult> EditAnimal(int animalId)
        {
            var animal = await _repository.GetAnimalByIdAsync(animalId);
            if (animal == null)
            {
                return RedirectToAction("Administrator");
            }
            var categories = await _repository.GetAllCategoriesAsync();
            ViewBag.Categories = categories;

            return View(animal);
        }

        [HttpPost]
        public async Task<IActionResult> EditAnimal(int animalId, Animal animal, IFormFile? pictureFile)
        {
            if (ModelState.IsValid)
            {
                var existingAnimal = await _repository.GetAnimalByIdAsync(animalId);

                if (existingAnimal == null)
                {
                    return RedirectToAction("Administrator");
                }

                if (pictureFile != null && pictureFile.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(pictureFile.FileName).ToLowerInvariant();
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await pictureFile.CopyToAsync(fileStream);
                    }
                    animal.PictureName = Path.Combine("Images", fileName);
                }
                else
                {
                    animal.PictureName = existingAnimal.PictureName; // שמירה על תמונה קיימת אם לא הועלתה חדשה
                }

                await _repository.UpdateAnimalAsync(animalId, animal);
                return RedirectToAction("Administrator");
            }

            var categories = await _repository.GetAllCategoriesAsync();
            ViewBag.Categories = categories;
            return View(animal);
        }
    }
}
