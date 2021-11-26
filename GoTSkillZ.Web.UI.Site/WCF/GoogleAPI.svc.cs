using GoTSkillZ.Application.Transforms.DTO;
using GoTSkillZ.Application.Transforms.STO;
using GoTSkillZ.CoreServices.YoutubeService.Interfaces;
using GoTSkillZ.CoreServices.YoutubeService.Services;
using GoTSkillZ.Security.Services.Interfaces;
using GoTSkillZ.Security.Services.Providers;
using System.ServiceModel.Activation;
using System.Web;

namespace GoTSkillZ.Web.UI.Site.WCF
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class GoogleAPI : IGoogleAPI
    {
        private readonly IGoTSkillZSecurityService _gotSkillZSecurityService;
        private readonly IYoutubeService _youtubeService;

        public GoogleAPI()
        {
            _gotSkillZSecurityService = new GoTSkillZSecurityService();
            _youtubeService = new YoutubeService();
        }


        public ResponseDTO AuthorizeUser(GoogleSTO authReq)
        {
            return _gotSkillZSecurityService.AuthorizeUser(authReq);
        }

        public void DeAuthorize()
        {
            var httpContext = HttpContext.Current;

            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();
            httpContext.Response.Cookies.Add(new HttpCookie("GoTSkillZ", ""));
        }

        public ResponseDTO AuthenticateUser()
        {
            return _gotSkillZSecurityService.ValidateCookie();
        }

        public bool CheckAndSub(string userId)
        {
            return _gotSkillZSecurityService.CheckIfUserIsSubscriber("", int.Parse(userId));
        }

        public ResponseDTO SubscribeUser(string userId)
        {
            return _youtubeService.AddUserSubscription(int.Parse(userId));
        }

        public ResponseDTO UnSubscribeUser(string userId)
        {
            if (userId == "1")
            {
                return new ResponseDTO();
            }

            return _youtubeService.UnSubscribeUser(int.Parse(userId));
        }
    }
}