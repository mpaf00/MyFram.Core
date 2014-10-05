namespace MyFram.Core.Attribute.Document
{
    using Core.Document;

    public class EmailAttribute : BaseDocumentAttribute
    {
        public EmailAttribute()
        {
            Document = new EmailDocument();
        }
    }
}