// Controllers/HomeController.cs
using k8sFormResultViewApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ResultApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPersonService _personService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IPersonService personService, ILogger<HomeController> logger)
        {
            _personService = personService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var persons = await _personService.GetAllPersonsAsync();
                var totalCount = await _personService.GetTotalCountAsync();

                ViewBag.TotalCount = totalCount;
                ViewBag.LastRefresh = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                return View(persons);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading persons");
                ViewBag.Error = "Error loading data. Please try again.";
                ViewBag.TotalCount = 0;
                return View(new List<k8sFormResultViewApp.Models.PersonViewModel>());
            }
        }
    }
}