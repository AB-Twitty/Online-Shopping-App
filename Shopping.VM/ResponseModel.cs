namespace Shopping.VM
{
    public class ResponseModel<T>
    {
        public int Status { get; set; }
        public int PageCount { get; set; }
        public T? Data { get; set; }
        public IList<T> DataList { get; set; }
        public string Message { get; set; } = null!;
        public string Token { get; set; } = null!;
        public IList<Object> ErrorMessages { get; set; } = null!;
        public void OKResult(int status, T data, string respondMessage= "The request has succeeded")
        {
            Status = status;
            Data = data;
            Message = respondMessage;
        }
        public void OKResult(int status, IList<T> list, string respondMessage= "The request has succeeded")
        {
            Status = status;
            DataList = list;
            Message = respondMessage;
        }
        public void ErrorResult(int status, string respondMessage)
        {
            Status = status;
            Message = respondMessage;
        }
        public void ErrorResult(int status, string respondMessage, IList<Object> errors)
        {
            Status = status;
            Message = respondMessage;
            ErrorMessages = errors;
        }
        public void PageCountCalc(int count, int size)
        {
            double pageCount = (double)(count / Convert.ToDecimal(size));
            PageCount = (int)Math.Ceiling(pageCount);
        }

        public ResponseModel<T> SetToken(string token)
        {
            Token = token;
            return this;
        }
    }
}
