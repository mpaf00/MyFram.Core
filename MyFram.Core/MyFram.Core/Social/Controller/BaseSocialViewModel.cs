namespace MyFram.Core.Social.Controller
{
    using System;
    using Api.Controller;
    using Api.Domain;
    using Contract;
    using Domain;

    public partial class BaseSocialController<T> : BaseController where T : ISocialHelper
    {
        public BaseSocialController()
        {
            SocialHelper = Activator.CreateInstance<T>();
        }

        private T SocialHelper { get; set; }

        public ReturnApi GetAccessToken(string code)
        {
            var returnApi = new ReturnApi
            {
                IsValid = true,
                Message = string.Empty
            };

            try
            {
                var accessToken = SocialHelper.GetAccessToken(new RequestTokenEntity(), code);
            }
            catch (Exception e)
            {
                returnApi.IsValid = false;
                returnApi.Data = e;
            }

            return returnApi;
        }

        public ReturnApi GetAuthorizationUri()
        {
            var returnApi = new ReturnApi
                                {
                                    IsValid = true,
                                    Message = string.Empty
                                };

            try
            {
                var autorizationUri = SocialHelper.GetAuthorizationUri();

                returnApi.Data = autorizationUri.Item2;
            }
            catch (Exception e)
            {
                returnApi.IsValid = false;
                returnApi.Data = e;
            }

            return returnApi;
        }
    }
}