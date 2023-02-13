using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Shopping.VM.AccountVM
{
    public class RegisterVM : AccountInfoVM
    {
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string password { get; set; } = null!;

        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage = "The password and confirmation password don't match.")]
        public string confirmPassword { get; set; } = null!;

        public IFormFile? imageFile { get; set; }
    }
}
