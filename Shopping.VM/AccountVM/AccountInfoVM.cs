using System.ComponentModel.DataAnnotations;
using Shopping.VM.AccountVM.Validations;

namespace Shopping.VM.AccountVM
{
    public class AccountInfoVM
    {
        public int id { get; set; }
        [Display(Name = "Full Name")]
        [StringLength(60, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string name { get; set; } = null!;
        [Display(Name = "Username")]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [CheckExistance("id")]
        public string username { get; set; } = null!;
        [Display(Name = "Email")]
        [EmailAddress]
        [CheckExistance("id")]
        public string email { get; set; } = null!;
        [Display(Name = "National ID")]
        [StringLength(14, ErrorMessage = "National ID must be 14 numbers ", MinimumLength = 14)]
        public string nationalId { get; set; } = null!;
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [HasSixteenYearsOrMore]
        public DateTime birthDate { get; set; }
        public int countryId { get; set; }
        public string? imageURL { get; set; }
        
        [Display(Name = "Account Type")]
        public Role accountType { get; set; }
        public bool isDeleted { get; set; }
    }
}
