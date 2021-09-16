
using System.ComponentModel.DataAnnotations;

namespace CustomRoles.Data.ViewModels
{
    public class BusinessUserVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter Name")]
        public string UserName { get; set; }

        public string PhoneNumber { get; set; }

        public int BusinessId { get; set; }

        [Required(ErrorMessage = "Please enter Email")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter Password")]
        [StringLength(255, ErrorMessage = "Must at least 5 characters", MinimumLength = 5)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = "Password must be more secure try 1 lowercase,uppercase,numeric and special character")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter confirm Password")]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
        public string ConfirmPassword
        {
            get; set;
        }
    }
}
