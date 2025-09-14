namespace BookOrganizer.Data
{
    public sealed class ApplicationUser: Microsoft.AspNetCore.Identity.IdentityUser
    {
        public bool EnableNotifications { get; set; }
    }
}
