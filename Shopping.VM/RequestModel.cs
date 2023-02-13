using Shopping.VM.AccountVM;

namespace Shopping.VM
{
    public class RequestModel<T>
    {
        public string Token { get; set; } = null!;
        public AccountInfoVM User { get; set; } = null!;
        public T? Data { get; set; }
    }
}
