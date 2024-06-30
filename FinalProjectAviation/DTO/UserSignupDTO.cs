using System.ComponentModel.DataAnnotations;
using FinalProjectAviation.Models;

namespace FinalProjectAviation.DTO
{
    public class UserSignupDTO
    {
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Username should be between 2 and 50 characters.")]
        public string? Username { get; set; }

        [StringLength(100, ErrorMessage = "Email should not exceed 100 characters.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string? Email { get; set; }

        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*\W).{8,}$",
        ErrorMessage = "Password must contain at least one uppercase letter, " + "one lowercase letter, one digit, and one special character.")]
        public string? Password { get; set; }

        [StringLength(50, ErrorMessage = "First name should not exceed 50 characters")]
        public string? Firstname { get; set; }

        [StringLength(50, ErrorMessage = "Last name should not exceed 50 characters")]
        public string? Lastname { get; set; }

        [StringLength(15, ErrorMessage = "Phone number should not exceed 15 characters")]
        public string? PhoneNumber { get; set; }

        [StringLength(50, ErrorMessage = "Ethnicity should noy exceed 50 characters")]
        public string? Ethnicity { get; set; }

        [EnumDataType(typeof(UserRole), ErrorMessage = "Invalid user role")]
        public UserRole? UserRole { get; set; }

        public override string? ToString()
        {
            return $"{Username} {Firstname} {Lastname} {Email} {Password} {PhoneNumber} {Ethnicity} {UserRole}";
        }
    }
}
