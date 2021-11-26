using System;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using GoTSkillZ.CoreServices.PMSService.Services;
using GoTSkillZ.CoreServices.SiteCoreDataService.Services;
using GoTSkillZ.CoreServices.YoutubeService.Services;
using GoTSkillZ.Security.Services.Helper;
using Microsoft.AspNet.Identity;

namespace GoTSkillZ.Web.UI.Site
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;


        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection) responseCookie.Secure = true;
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? string.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? string.Empty))
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            hdnPageName.Value = Page.Title;

            var cookieData = AuthHelper.GetCookieValue();
            var _siteCoreDataService = new SiteCoreDataService();
            if (cookieData.Any())
            {
                var userId = cookieData["XXXID"];

                hdnUserId.Value = userId;
            }

            if (HttpContext.Current.Session["LoadingMessages"] == null)
            {
                var pmsService = new PMSService();
                HttpContext.Current.Session["LoadingMessages"] = pmsService.GetAllLoadingMessages();
            }

            HttpContext.Current.Session["CurrentSubCount"] = _siteCoreDataService.GetCurrentSubCount();
            HttpContext.Current.Session["RecentYouTubeSubscribers"] = _siteCoreDataService.GetAllYouTubeSubscribers();
            HttpContext.Current.Session["RecentDonations"] = _siteCoreDataService.GetDonations();
            HttpContext.Current.Session["NewSiteUsers"] = _siteCoreDataService.GetNewSiteUsers();



            HttpContext.Current.Session["IsLive"] = new YoutubeService().CheckIfYouTubeIsLive();



        

            
            

           

            
           



        }


    }
}