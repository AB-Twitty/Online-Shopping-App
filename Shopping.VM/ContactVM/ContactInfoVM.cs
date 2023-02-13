using System.ComponentModel.DataAnnotations;
using Shopping.VM.ContactVM.Validations;

namespace Shopping.VM.ContactVM
{
    public class ContactInfoVM
    {
        public int id { get; set; }
        [Display(Name = "Contact")]
        [CheckExistance("id")]
        public string contact { get; set; } = null!;
        public int customerId { get; set; }
        public int typeId { get; set; }
        public string type { get; set; } = null!;
    }
}
