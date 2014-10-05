namespace MyFram.Core.Social.Contract
{
    using System;
    using System.Linq;
    using Domain;

    public interface ISocialHelper
    {
        AccessTokenEntity GetAccessToken(RequestTokenEntity requestToken, string verifierCode);

        Tuple<RequestTokenEntity, string> GetAuthorizationUri();

        IQueryable<FeedEntity> GetUserTimelineFeeds(AccessTokenEntity accessToken);
    }
}