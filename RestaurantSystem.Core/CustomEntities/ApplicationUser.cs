using Microsoft.AspNetCore.Identity;

namespace RestaurantSystem.Core.CustomEntities
{
    public class ApplicationUser : IdentityUser
    {
        public string Names { get; set; }
        public string LastsNames { get; set; }
        public bool IsFirstLogin { get; set; } = true;
    }
}