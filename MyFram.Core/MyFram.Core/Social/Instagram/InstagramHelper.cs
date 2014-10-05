namespace MyFram.Core.Social.Instagram
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contract;
    using Domain;
    using InstaSharp;

    public class InstagramHelper : ISocialHelper
    {
        public InstagramHelper()
        {
            Config = new InstagramConfig(Util.Config.InstagramApiUrl,
                                         Util.Config.InstagramOAuthUrl,
                                         Util.Config.InstagramOAuthClientId,
                                         Util.Config.InstagramOAuthClientSecret,
                                         Util.Config.InstagramCallbackUrl);
        }

        private InstagramConfig Config { get; set; }

        public AccessTokenEntity GetAccessToken(RequestTokenEntity requestToken, string verifierCode)
        {
            return new AccessTokenEntity();
        }

        public Tuple<RequestTokenEntity, string> GetAuthorizationUri()
        {
            var uri = string.Format(Util.Config.InstagramOAuthUrlComplete, Config.ClientId, Config.RedirectURI);

            return new Tuple<RequestTokenEntity, string>(new RequestTokenEntity(), uri);
        }

        public IQueryable<FeedEntity> GetUserTimelineFeeds(AccessTokenEntity accessToken)
        {
            var feeds = new List<FeedEntity>();

            try
            {
                var authInfo = new AuthInfo
                {
                    Access_Token = accessToken.Token,
                    Config = Config
                };

                var users = new InstaSharp.Endpoints.Users.Authenticated(Config, authInfo);
                var instaSharpFeeds = users.Feed("self");
            }
            catch (System.Exception e)
            {
                throw e;
            }

            return feeds.AsQueryable();
        }
    }
}