namespace EmploymentApp.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public int UserTypeId { get; set; }

        public string Password { get; set; } = null!;
    }
}
