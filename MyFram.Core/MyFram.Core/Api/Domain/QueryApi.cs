namespace MyFram.Core.Api.Domain
{
    public class QueryApi
    {
        public string Columns { get; set; }

        public string Delimiter { get; set; }

        public int Limit { get; set; }

        public string Order { get; set; }

        public int Page { get; set; }

        public string Where { get; set; }
    }
}