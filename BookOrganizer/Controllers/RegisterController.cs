using BookOrganizer.Data;
using BookOrganizer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookOrganizer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController:ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RegisterController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                EnableNotifications = model.EnableNotifications,

            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok(new { message = "Usuário registrado com sucesso" });
            }

                // lógica de cadastro
                return BadRequest(result.Errors);
        }
    }
}
