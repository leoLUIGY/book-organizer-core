using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace BookOrganizer.Controllers
{
    public class HomeWorldController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
