using BookOrganizer.Data;
using Microsoft.AspNetCore.Identity;

namespace BookOrganizer.Features
{
    public class LoginUser
    {
        public record Request(string Email, string Password, bool RememberMe);

        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/login", async (
                    Request request,
                    SignInManager<ApplicationUser> signInManager,
                    UserManager<ApplicationUser> UserManager
                ) =>
            {
                var user = await UserManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    return Results.BadRequest(new { message = "Usuario não encontrado" });
                }

                var result = await signInManager.PasswordSignInAsync(
                    user, request.Password, request.RememberMe, lockoutOnFailure: false);

                if (!result.Succeeded)
                {
                    return Results.BadRequest(new { message = "Login invalido" });

                }
                return Results.Ok(new {user.Id, user.Email});
            })
            .WithName("LoginUser")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);
        }
    }
}
