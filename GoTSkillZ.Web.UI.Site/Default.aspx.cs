using System;
using System.Web.UI;

namespace GoTSkillZ.Web.UI.Site
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Headers.Add("X-XSS-Protection", "1");
            Response.Headers.Add("X-Content-Type-Options", "nosniff");
            Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
            Response.Headers.Add("Referrer-Policy", "same-origin");
            Response.Headers.Add("Strict-Transport-Security", "max-age=31536000");
            //            Response.Headers.Add("Content-Security-Policy", "script-src 'nonce-EDNnf03nceIOfn39fn3e9h3sdfa'");
            // Response.Headers.Add("Feature-Policy", "usermedia *; sync-xhr 'self'");
        }
    }
}