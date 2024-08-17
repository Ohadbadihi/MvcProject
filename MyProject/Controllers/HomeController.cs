using Microsoft.AspNetCore.Mvc;
using MyProject.Models;
using MyProject.Repository;

namespace MyProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository _repository;

        public HomeController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()//home page
        {
            var allAnimals = await _repository.GetAllAnimalsAsync();

            if (!allAnimals.Any())
            {
                return RedirectToAction("Index", "Error");
            }
            // checking for valid comments
            var animalsWithValidComments = allAnimals.Where(animal =>
                animal.Comments != null && animal.Comments.Any(comment => !string.IsNullOrEmpty(comment.Comment))).ToList();

            // if there are no animals with valid comments go in
            if (!animalsWithValidComments.Any())
            {           
                return RedirectToAction("Index", "Error");
            }

            List<Animal> selectedAnimals;

            if (animalsWithValidComments.Count >= 2)
            {
                selectedAnimals = animalsWithValidComments.OrderByDescending(animal => animal.Comments!.Count(comment => !string.IsNullOrEmpty(comment.Comment)))
                    .Take(2).ToList();
            }
            else
            {
                selectedAnimals = animalsWithValidComments;
            }

            return View(selectedAnimals);
        }
    }
}
