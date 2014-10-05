namespace MyFram.Core.Api.Domain
{
    public class ReturnApi
    {
        public object Data { get; set; }

        public bool IsValid { get; set; }

        public string Message { get; set; }
    }
}