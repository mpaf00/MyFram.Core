namespace MyFram.Core.Social.Domain
{
    public partial class RequestTokenEntity : BaseSocialEntity
    {
        public virtual string Token { get; set; }

        public virtual string TokenSecret { get; set; }

        public virtual bool IsConfirmed { get; set; }
    }
}