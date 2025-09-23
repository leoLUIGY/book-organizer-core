using BookOrganizer.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookOrganizer.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;

        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("BookOrganizerApi");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var response = await _httpClient.PostAsJsonAsync("/api/login", model);

            if (response.IsSuccessStatusCode)
            { // 🔹 Aqui você pode redirecionar para outra área da aplicação
                return RedirectToAction("Index", "TodoBook");
            }

            TempData["Error"] = "Email ou senha Invalidos"!;

            var error = await response.Content.ReadAsStringAsync();
           // ModelState.AddModelError("", "Email ou senha Invalidos" +);

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }



            var response = await _httpClient.PostAsJsonAsync("/api/register", model);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Success");
            }

            TempData["Error"] = "Erro ao Registrar usuario!";
            // var errors = await response.Content.ReadFromJsonAsync<object>();
           // ModelState.AddModelError("", "Erro ao registrar usuario " + response.StatusCode);

            return View(model);
        }

        public IActionResult Success()
        {
            return RedirectToAction("Index","TodoBook");
        }
    }
}
