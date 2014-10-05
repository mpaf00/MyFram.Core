namespace MyFram.Core.Social.Domain
{
    public partial class AccessTokenEntity : BaseSocialEntity
    {
        public override string GetPluralName()
        {
            return "Access Token";
        }

        public override string GetSingleName()
        {
            return "Access Token";
        }
    }
}