namespace MyFram.Core.Attribute.Document
{
    using Core.Document;

    public class CepAttribute : BaseDocumentAttribute
    {
        public CepAttribute()
        {
            Document = new CepDocument();
        }
    }
}