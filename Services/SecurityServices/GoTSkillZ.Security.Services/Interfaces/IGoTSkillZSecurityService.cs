using GoTSkillZ.Application.Transforms.DTO;
using GoTSkillZ.Application.Transforms.STO;

namespace GoTSkillZ.Security.Services.Interfaces
{
    public interface IGoTSkillZSecurityService
    {
        ResponseDTO AuthorizeUser(GoogleSTO authReq);

        ResponseDTO ValidateCookie();

        ResponseDTO GetLoggedInUserCookieData();
        ResponseDTO GetUserInternalId();

        bool CheckIfUserIsSubscriber(string token, int userId);

    }
}