using GoTSkillZ.Application.Transforms.DTO;
using GoTSkillZ.Application.Transforms.Enums;
using GoTSkillZ.CoreServices.MembershipService.Interfaces;
using GoTSkillZ.CoreServices.MembershipService.Services;
using GoTSkillZ.CoreServices.UserExtenionService.Interfaces;
using GoTSkillZ.CoreServices.UserExtenionService.Services;
using GoTSkillZ.Models.Membership.Data;
using GoTSkillZ.Models.UserSetup.Data;
using GoTSkillZ.Security.Services.Interfaces;
using GoTSkillZ.Security.Services.Providers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoTSkillZ.Web.UI.Site.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MembershipAPI" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select MembershipAPI.svc or MembershipAPI.svc.cs at the Solution Explorer and start debugging.
    public class MembershipAPI : IMembershipAPI
    {
        private readonly IMembershipService _membershipService;
        private readonly IGoTSkillZSecurityService _gotSkillZSecurityService;
        private readonly IUserExtensionService _userExtensionService;

        public MembershipAPI()
        {
            _gotSkillZSecurityService = new GoTSkillZSecurityService();
            _membershipService = new MembershipService();
            _userExtensionService = new UserExtensionService();

        }

        public List<RoleDTO> GetAllActiveRoles()
        {
            return _membershipService.GetAllRoles().AsEnumerable().Where(c => c.IsActive && !c.IsDeleted).Select(x => new RoleDTO
            {
                Id = x.Id,
                RoleName = x.RoleName,
                RoleDescription = x.RoleDescription,
                IsActive = x.IsActive,
                IsDeleted = x.IsDeleted
            }).OrderBy(x => x.RoleName).ToList();
        }

        public ResponseDTO GetUserProfileMetaData(string uid)
        {

            var responseObj = new ResponseDTO();
            var returnedUser = new UserProfileDTO();
            if (uid != "0")
            {

                if (uid.ToLowerInvariant() == "gotskillz")
                    uid = "1";

                var userId = int.Parse(uid);

                var loggedInUserId = _gotSkillZSecurityService.GetLoggedInUserCookieData().UserId;
                bool readOnly = loggedInUserId != userId;

                returnedUser = _membershipService.GetUser(userId, readOnly);

                if (returnedUser.UserId != 0)
                {
                    responseObj.UserProfile = returnedUser;
                }
                else
                {
                    responseObj.StateCode = GoTSkillZEnum.InvalidUser;
                    responseObj.ResponseText = Enum.GetName(typeof(GoTSkillZEnum), GoTSkillZEnum.InvalidUser);
                }
            }
            else
            {
                responseObj = _gotSkillZSecurityService.GetLoggedInUserCookieData();
                if (responseObj != null)
                {
                    returnedUser = _membershipService.GetUser(responseObj.UserId, false);
                    responseObj.UserProfile = returnedUser;

                }

            }

            return responseObj;

        }

        public List<CountryDTO> GetCountry(string searchKey)
        {
            return _membershipService.FindCountries(searchKey);
        }

        public List<StateDTO> GetState(string searchKey, string countryId)
        {
            return _membershipService.FindState(searchKey, int.Parse(countryId));
        }

        public List<CityDTO> GetCity(string searchKey, string countryId, string stateId)
        {
            return _membershipService.FindCity(searchKey, int.Parse(countryId), int.Parse(stateId));
        }

        public string SaveUserProfileData(UserProfileDTO userProfileDTO)
        {
            return _membershipService.SaveUserProfileData(userProfileDTO);

        }



        public List<UserSetupDTO> GetUserSetups()
        {
            var userId = _gotSkillZSecurityService.GetLoggedInUserCookieData().UserId;

            if (userId == 0) return null;

            return _userExtensionService.GetUserSetups(userId);
        }

        public List<SetupType> GetSetupTypes()
        {
            return _userExtensionService.GetSetupTypes();
        }

        public List<SetupOption> GetSetupOptions()
        {
            return _userExtensionService.GetSetupOptions();
        }

        public ResponseDTO GetUserProfileHeaderData()
        {
            var responseObj = _gotSkillZSecurityService.GetLoggedInUserCookieData();
            if (responseObj.Success == false) return responseObj;

            responseObj.UserProfile = _membershipService.GetUserHeaderData(responseObj.UserId);

            return responseObj;
        }

        public string AddNewUserSetup(List<UserSetupDataDTO> newUserSetup)
        {
            if (!newUserSetup.Any()) return "failed";

            return _membershipService.AddNewUserSetup(newUserSetup);

        }

        public string UpdateUserSetup(List<UserSetupDataDTO> newUserSetup)
        {
            if (!newUserSetup.Any()) return "failed";

            return _membershipService.UpdateUserSetup(newUserSetup);
        }

        public string RemoveSetup(string setupId)
        {
            if (string.IsNullOrEmpty(setupId)) return "failed";

            return _membershipService.RemoveUserSetup(setupId);
        }

        public List<UserOccupation> GetUserOccupations()
        {
            return _membershipService.GetUserOccupations();
        }
    }
}
