using Microsoft.AspNetCore.Mvc;

namespace MovieApp.Controllers
{
    public class Rating : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
