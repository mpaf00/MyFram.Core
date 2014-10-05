namespace MyFram.Core.Social.Controller
{
    using Api.Controller;
    using Contract;

    public partial class BaseSocialController<T> : BaseController where T : ISocialHelper
    {
    }
}