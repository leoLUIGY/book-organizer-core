using BookOrganizer.Data;
using Microsoft.AspNetCore.Identity;

namespace BookOrganizer.Features
{
    public static class RegisterUser
    {
        public record Request(string Email, string Password, bool EnableNotifications = false);

        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/register", async (Request request, BookOrganizerContext context,UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) =>
            {
                using var transaction = await context.Database.BeginTransactionAsync();
                var user = new ApplicationUser
                {
                    UserName = request.Email,
                    Email = request.Email,
                    EnableNotifications = request.EnableNotifications
                };
                var result = await userManager.CreateAsync(user, request.Password);
                if (!result.Succeeded)
                {
                    return Results.BadRequest(result.Errors);
                }
                // Assign the "User" role to the newly registered user
                if (!await roleManager.RoleExistsAsync(Roles.User))
                {
                    await roleManager.CreateAsync(new IdentityRole(Roles.User));
                }
                await userManager.AddToRoleAsync(user, Roles.User);

                await transaction.CommitAsync();
                return Results.Ok(new { user.Id, user.Email, user.EnableNotifications });
            })
            .WithName("RegisterUser")
            .Produces(StatusCodes.Status200OK)
            .ProducesValidationProblem();
        }
    }
}
