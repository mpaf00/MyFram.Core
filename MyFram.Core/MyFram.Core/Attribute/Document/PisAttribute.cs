namespace MyFram.Core.Attribute.Document
{
    using Core.Document;

    public class PisAttribute : BaseDocumentAttribute
    {
        public PisAttribute()
        {
            Document = new PisDocument();
        }
    }
}