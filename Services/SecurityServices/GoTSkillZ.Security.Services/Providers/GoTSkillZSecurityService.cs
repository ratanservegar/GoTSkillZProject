using GoTSkillZ.Application.Transforms.DTO;
using GoTSkillZ.Application.Transforms.Enums;
using GoTSkillZ.Application.Transforms.STO;
using GoTSkillZ.CoreServices.MembershipService.Interfaces;
using GoTSkillZ.CoreServices.MembershipService.Services;
using GoTSkillZ.Models.Membership.Data;
using GoTSkillZ.Models.Membership.Interfaces;
using GoTSkillZ.Models.Membership.Providers;
using GoTSkillZ.Models.UserDataExtension.Data;
using GoTSkillZ.Models.UserDataExtension.Interfaces;
using GoTSkillZ.Models.UserDataExtension.Provider;
using GoTSkillZ.Security.Services.Helper;
using GoTSkillZ.Security.Services.Interfaces;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using HttpCookie = System.Web.HttpCookie;

namespace GoTSkillZ.Security.Services.Providers
{
    public class GoTSkillZSecurityService : IGoTSkillZSecurityService
    {
        private readonly IMembershipService _membershipService;
        private readonly IUserProvider _userProvider;
        private readonly IUserRoleProvider _userRoleProvider;
        private readonly IYouTubeSubscribersProvider _youTubeSubscribersProvider;


        public GoTSkillZSecurityService()
        {
            _userProvider = new UserProvider();
            _userRoleProvider = new UserRoleProvider();
            _membershipService = new MembershipService();
            _youTubeSubscribersProvider = new YouTubeSubscribersProvider();
        }

        public ResponseDTO AuthorizeUser(GoogleSTO authReq)
        {
            var responseDTO = new ResponseDTO();
            var isTokenValid = ValidateAccessToken(authReq.GoogleAccessToken);


            if (!isTokenValid)
            {
                responseDTO.StateCode = GoTSkillZEnum.InvalidToken;
                responseDTO.ResponseText = Enum.GetName(typeof(GoTSkillZEnum), GoTSkillZEnum.InvalidToken);
                responseDTO.Success = false;
                return responseDTO;
            }


            var newUser = RegisterUserAndAuthorize(authReq);

            if (newUser.UserId != 0)
            {
                AuthHelper.SetCookie(authReq, newUser.UserId);

                //check and add user to sub table if user is new and a sub
                CheckIfUserIsSubscriber(authReq.GoogleOAuthAccessToken, newUser.UserId);

                responseDTO.StateCode = GoTSkillZEnum.ValidUser;
                responseDTO.ResponseText = Enum.GetName(typeof(GoTSkillZEnum), GoTSkillZEnum.ValidUser);
                responseDTO.Success = true;
                responseDTO.UserId = newUser.UserId;
                responseDTO.UserProfile = _membershipService.GetUser(newUser.UserId, false);
            }

            return responseDTO;
        }

        public ResponseDTO ValidateCookie()
        {
            var responseObj = new ResponseDTO();

            var cookieData = AuthHelper.GetCookieValue();

            if (cookieData.Any())
            {
                var accessToken = cookieData["userToken"];
                var oAuthToken = cookieData["offlineToken"];
                var userId = cookieData["XXXID"];
                if (!string.IsNullOrEmpty(accessToken))
                {
                    
                    if (ValidateAccessToken(accessToken))
                    {
                        responseObj.StateCode = GoTSkillZEnum.ValidToken;
                        responseObj.ResponseText = Enum.GetName(typeof(GoTSkillZEnum), GoTSkillZEnum.ValidToken);
                        responseObj.Success = true;
                        responseObj.UserProfile = null;
                        responseObj.UserId = int.Parse(userId);

                        CheckIfUserIsSubscriber(oAuthToken, int.Parse(userId));
                    }

                }
                else
                {
                    responseObj.StateCode = GoTSkillZEnum.InvalidToken;
                    responseObj.ResponseText = Enum.GetName(typeof(GoTSkillZEnum), GoTSkillZEnum.InvalidToken);
                }
            }
            else
            {
                responseObj.StateCode = GoTSkillZEnum.NotLoggedIn;
                responseObj.ResponseText = Enum.GetName(typeof(GoTSkillZEnum), GoTSkillZEnum.NotLoggedIn);
                responseObj.Success = false;
            }

            //set current cookie to blank
            if (responseObj.Success == false)
                HttpContext.Current.Response.SetCookie(new HttpCookie("GoTSkillZ", ""));


            return responseObj;
        }


        public ResponseDTO GetLoggedInUserCookieData()
        {
            var responseObj = new ResponseDTO();

            var cookieData = AuthHelper.GetCookieValue();

            if (cookieData.Any())
            {
                var userId = cookieData["XXXID"];

                responseObj.StateCode = GoTSkillZEnum.ValidToken;
                responseObj.ResponseText = Enum.GetName(typeof(GoTSkillZEnum), GoTSkillZEnum.ValidToken);
                responseObj.Success = true;
                responseObj.UserProfile = null;
                responseObj.UserId = int.Parse(userId);
            }
            else
            {
                responseObj.StateCode = GoTSkillZEnum.NotLoggedIn;
                responseObj.ResponseText = Enum.GetName(typeof(GoTSkillZEnum), GoTSkillZEnum.NotLoggedIn);
                responseObj.Success = false;
                responseObj.UserProfile = null;
                responseObj.UserId = 0;
            }

            //set current cookie to blank
            if (responseObj.Success == false)
                HttpContext.Current.Response.SetCookie(new HttpCookie("GoTSkillZ", ""));


            return responseObj;
        }

        public ResponseDTO GetUserInternalId()
        {
            var responseObj = new ResponseDTO();

            var cookieData = AuthHelper.GetCookieValue();

            if (cookieData.Any())
            {
                var userId = cookieData["XXXID"];

                if (!string.IsNullOrEmpty(userId))
                {
                    responseObj.UserId = int.Parse(userId);
                    responseObj.StateCode = GoTSkillZEnum.ValidUser;
                    responseObj.ResponseText = Enum.GetName(typeof(GoTSkillZEnum), GoTSkillZEnum.ValidUser);
                    responseObj.Success = true;
                }
                else
                {
                    responseObj.StateCode = GoTSkillZEnum.InvalidUser;
                    responseObj.ResponseText = Enum.GetName(typeof(GoTSkillZEnum), GoTSkillZEnum.InvalidUser);
                }
            }


            return responseObj;
        }


        private Users RegisterUserAndAuthorize(GoogleSTO authReq)
        {
            var returnUser = new Users();

            if (authReq == null) return returnUser;
            if (authReq.GoogleUserId == "") return returnUser;


            var checkForExistingUser =
                _userProvider.FindBy(
                        x => x.GooglePublicId.Equals(authReq.GoogleUserId) && x.Email.Equals(authReq.GoogleUserEmail))
                    .FirstOrDefault();

            if (checkForExistingUser == null)
            {
                var newUser = new Users
                {
                    GooglePublicId = authReq.GoogleUserId,
                    GoogleToken = authReq.GoogleAccessToken,
                    GoogleOAuthToken = authReq.GoogleOAuthAccessToken,
                    FirstName = authReq.GoogleFirstName ?? "",
                    LastName = authReq.GoogleLastName ?? "",
                    Email = authReq.GoogleUserEmail,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedDate = DateTime.Now
                };

                returnUser = _userProvider.Add(newUser);

                //set read-only role for the new user
                if (returnUser.UserId != 0)
                {
                    var newUserRole = new UserRoles
                    {
                        UserId = returnUser.UserId,
                        RoleId = 2, //read-only
                        IsActive = true,
                        IsDeleted = false
                    };

                    _userRoleProvider.Add(newUserRole);
                }
            }
            else
            {
                //update the existing usersAccess Token with the new one

                if (checkForExistingUser.GoogleToken != authReq.GoogleAccessToken)
                    checkForExistingUser.GoogleToken = authReq.GoogleAccessToken;

                if (checkForExistingUser.GoogleOAuthToken != authReq.GoogleOAuthAccessToken)
                    checkForExistingUser.GoogleOAuthToken = authReq.GoogleOAuthAccessToken;

                _userProvider.Update(checkForExistingUser);

                //return the existing user
                returnUser = checkForExistingUser;
            }

            return returnUser;
        }


        private bool ValidateAccessToken(string token)
        {
            var isValidated = false;
            var client = new RestClient("https://oauth2.googleapis.com/");

            var request = new RestRequest("tokeninfo?id_token=" + token, DataFormat.Json);

            var response = client.Get(request);

            if (response.StatusCode == HttpStatusCode.OK)
                isValidated = true;


            return isValidated;
        }


        public bool CheckIfUserIsSubscriber(string token, int userId)
        {

            var cookieData = AuthHelper.GetCookieValue();
          
            if (cookieData.Any())
            {
                 token = cookieData["offlineToken"];
                 userId = int.Parse(cookieData["XXXID"]);
            }

            if (userId == 1) return true;
            if (string.IsNullOrEmpty(token)) return false;


            var isValidated = false;
            var client = new RestClient("https://www.googleapis.com/youtube/v3/subscriptions?part=snippet,contentDetails&forChannelId=UCdMrMjhgjEm7GJwQM-4Fn0w&mine=true&key=AIzaSyDzUwSllu-soFvkK7SwzqciP54-eEsGe2Y");

            var request = new RestRequest(Method.GET);
            request.AddParameter("Authorization", string.Format("Bearer " + token), ParameterType.HttpHeader);

            var response = client.Get(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var joResponse = JObject.Parse(response.Content);
                var items = (JArray) joResponse["items"];

                if (items.Count > 0)
                {
                    isValidated = true;
                    AddUserSubscription(userId);
                }
                else
                {
                    isValidated = false;
                    UnSubscribeUser(userId);
                }
            }
            else
            {
                UnSubscribeUser(userId);
            }

            return isValidated;
        }


        private void AddUserSubscription(int userId)
        {
            

            var checkForExistingSub = _youTubeSubscribersProvider.FindBy(x => x.UserId == userId).FirstOrDefault();

            if (checkForExistingSub != null)
            {
                if (checkForExistingSub.IsSubscribed == false)
                {
                    checkForExistingSub.IsSubscribed = true;
                    _youTubeSubscribersProvider.Update(checkForExistingSub);
                }
            }
            else
            {
                var newSubscriber = new YouTubeSubscribers
                {
                    UserId = userId,
                    IsSubscribed = true,
                    SubscribedDate = DateTime.Now
                };
                _youTubeSubscribersProvider.Add(newSubscriber);
            }

            var checkForExistingRole =
                _userRoleProvider.FindBy(x => x.UserId == userId && x.RoleId == 5).FirstOrDefault();

            if (checkForExistingRole == null)
            {
                var newUserRole = new UserRoles
                {
                    UserId = userId,
                    RoleId = 5, //sub
                    IsActive = true,
                    IsDeleted = false
                };

                _userRoleProvider.Add(newUserRole);
            }
        }

        private void UnSubscribeUser(int userId)
        {
            var existingSub = _youTubeSubscribersProvider.FindBy(x => x.UserId == userId).FirstOrDefault();

            if (existingSub != null)
                if (existingSub.IsSubscribed)
                {
                    existingSub.IsSubscribed = false;
                    existingSub.UnSubscribedDate = DateTime.Now;
                    _youTubeSubscribersProvider.Update(existingSub);
                }

            var existingSubRole = _userRoleProvider.FindBy(x => x.UserId == userId && x.RoleId == 5).FirstOrDefault();

            if (existingSubRole != null) _userRoleProvider.Delete(userId, 5);
        }
    }
}