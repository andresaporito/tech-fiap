using System.ComponentModel.DataAnnotations;
using Tech.Domain.Enums;

namespace Tech.Domain.Requests
{
    public sealed record NewUsersRequest
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; init; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[\W_]).+$",
            ErrorMessage = "Password must contain at least one letter, one number, and one special character.")]
        public string Password { get; init; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; init; }

        [Required(ErrorMessage = "Permission is required")]
        public TypePermissionEnum Permission { get; init; }
    }
}
