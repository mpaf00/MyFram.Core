namespace MyFram.Core.Social.Domain
{
    public partial class AccessTokenEntity : BaseSocialEntity
    {
        public virtual string Token { get; set; }

        public virtual string TokenSecret { get; set; }

        public virtual string UserId { get; set; }

        public virtual string UserName { get; set; }
    }
}