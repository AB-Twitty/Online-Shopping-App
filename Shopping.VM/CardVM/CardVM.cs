using Shopping.VM.CardVM.Validations;

namespace Shopping.VM.CardVM
{
    public class CardVM
    {
        public int id { get; set; }
        public int customerId { get; set; }
        [CheckExistance("id")]
        public string cardNumber { get; set; } = null!;
        public string provider { get; set; } = null!;
    }
}
