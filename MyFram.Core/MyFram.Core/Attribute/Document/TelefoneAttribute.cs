namespace MyFram.Core.Attribute.Document
{
    using Core.Document;

    public class TelefoneAttribute : BaseDocumentAttribute
    {
        public TelefoneAttribute()
        {
            Document = new TelefoneDocument();
        }
    }
}