using GoTSkillZ.Application.Transforms.STO;
using GoTSkillZ.Security.Services.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace GoTSkillZ.Security.Services.Helper
{
    public static class AuthHelper
    {
        private static readonly string _decryptKey = ConfigurationManager.AppSettings["DecryptKey"];
        private static readonly string _decryptIv = ConfigurationManager.AppSettings["DecryptIV"];


        public static Dictionary<string, string> GetCookieValue()
        {
            var deCryptCookieData = new Dictionary<string, string>();

            var httpCookie = HttpContext.Current.Request.Cookies["GoTSkillZ"];
            if (httpCookie != null)
            {
                var cookie = httpCookie.Value;
                if (cookie == "") return deCryptCookieData;
                var deCryptedData = Decrypt(cookie);
                deCryptCookieData = deCryptedData.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(part => part.Split(':'))
                    .ToDictionary(split => split[0], split => split[1]);
            }

            return deCryptCookieData;
        }

        public static void SetCookie(GoogleSTO authReq, int internalUserId)
        {
            var userData = "XXXID:" + internalUserId + ",UserId:" + authReq.GoogleUserId + ",UserEmail:" +
                           authReq.GoogleUserEmail + ",userToken:" + authReq.GoogleAccessToken + ",offlineToken:" + authReq.GoogleOAuthAccessToken;

            var encryptData = Encrypt(userData);


            var preAuthenticationCookie = new HttpCookie("GoTSkillZ", encryptData);
            HttpContext.Current.Response.SetCookie(preAuthenticationCookie);




        }


        public static string Decrypt(string data)
        {
            var key = Convert.FromBase64String(_decryptKey);
            var iv = Convert.FromBase64String(_decryptIv);

            // instantiate the class with the arrays
            var des = new CTripleDes(key, iv);
            var decryptedData = des.Decrypt(data);
            return decryptedData;
        }


        public static string Encrypt(string data)
        {
            var key = Convert.FromBase64String(_decryptKey);
            var iv = Convert.FromBase64String(_decryptIv);

            // instantiate the class with the arrays
            var des = new CTripleDes(key, iv);
            var encryptedData = des.Encrypt(data);
            return encryptedData;
        }
    }
}