namespace MyFram.Core.Social.Twitter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using Contract;
    using Domain;
    using TweetSharp;
    using Util;

    public class TwitterHelper : ISocialHelper
    {
        public TwitterHelper()
        {
            Service = new TwitterService(Config.TwitterOAuthConsumerKey, Config.TwitterOAuthConsumerSecret);
        }

        private TwitterService Service { get; set; }

        public AccessTokenEntity GetAccessToken(RequestTokenEntity requestToken, string verifierCode)
        {
            var accessTokenEntity = new AccessTokenEntity();

            try
            {
                var oAuthRequestToken = new OAuthRequestToken
                                            {
                                                OAuthCallbackConfirmed = requestToken.IsConfirmed,
                                                Token = requestToken.Token,
                                                TokenSecret = requestToken.TokenSecret
                                            };

                var accessToken = Service.GetAccessToken(oAuthRequestToken, verifierCode);

                accessTokenEntity.Token = accessToken.Token;
                accessTokenEntity.TokenSecret = accessToken.TokenSecret;
                accessTokenEntity.UserId = accessToken.UserId.ToString();
                accessTokenEntity.UserName = accessToken.ScreenName;
            }
            catch (Exception e)
            {
                throw e;
            }

            return accessTokenEntity;
        }

        public Tuple<RequestTokenEntity, string> GetAuthorizationUri()
        {
            try
            {
                var oAuthrequestToken = Service.GetRequestToken();
                var requestToken = new RequestTokenEntity
                                      {
                                          IsConfirmed = oAuthrequestToken.OAuthCallbackConfirmed,
                                          Token = oAuthrequestToken.Token,
                                          TokenSecret = oAuthrequestToken.TokenSecret
                                      };

                var uri = Service.GetAuthorizationUri(oAuthrequestToken);

                return new Tuple<RequestTokenEntity, string>(requestToken, uri.ToString());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IQueryable<FeedEntity> GetUserTimelineFeeds(AccessTokenEntity accessToken)
        {
            var feeds = new List<FeedEntity>();

            try
            {
                var service = new TwitterService(Config.TwitterOAuthConsumerKey, Config.TwitterOAuthConsumerSecret, accessToken.Token, accessToken.TokenSecret);
                var rateSummary = service.GetRateLimitStatus(new GetRateLimitStatusOptions());
                var tweets = service.ListTweetsOnHomeTimeline(new ListTweetsOnHomeTimelineOptions { Count = 200 });
                var rate = service.Response.RateLimitStatus;

                if (service.Response.StatusCode == HttpStatusCode.OK)
                {
                }
                else
                {
                    throw service.Response.InnerException;
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return feeds.AsQueryable();
        }
    }
}