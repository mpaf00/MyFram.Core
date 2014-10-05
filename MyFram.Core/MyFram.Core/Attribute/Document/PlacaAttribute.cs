namespace MyFram.Core.Attribute.Document
{
    using Core.Document;

    public class PlacaAttribute : BaseDocumentAttribute
    {
        public PlacaAttribute()
        {
            Document = new PlacaDocument();
        }
    }
}