

namespace RestaurantSystem.Shared.DTOs.User
{
    public class CreateOrEditUserDto
    {
        public string Id { get; set; }
        public string Names { get; set; }
        public string LastsNames { get; set; }
        public string Email { get; set; }
        public string PassWord { get; set; }
        public string Rol { get; set; }
        public string PassWordConfirmed { get; set; }
    }
}
