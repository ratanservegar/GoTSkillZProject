using GoTSkillZ.Application.Transforms.DTO;
using GoTSkillZ.CoreServices.UserExtenionService.Interfaces;
using GoTSkillZ.CoreServices.UserExtenionService.Services;
using System.Collections.Generic;

namespace GoTSkillZ.Web.UI.Site.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "UserCoreDataAPI" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select UserCoreDataAPI.svc or UserCoreDataAPI.svc.cs at the Solution Explorer and start debugging.
    public class UserCoreDataAPI : IUserCoreDataAPI
    {
        private readonly IUserExtensionService _userExtensionService;

        public UserCoreDataAPI()
        {
            _userExtensionService = new UserExtensionService();
        }
        public List<UserAchievementsDTO> GetUserAchievements(string userId)
        {
            return _userExtensionService.GetUserAchievements(int.Parse(userId));
        }
    }
}
