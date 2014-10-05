namespace MyFram.Core.Social.Domain
{
    public partial class FeedEntity : BaseSocialEntity
    {
        public override string GetPluralName()
        {
            return "Feeds";
        }

        public override string GetSingleName()
        {
            return "Feed";
        }
    }
}