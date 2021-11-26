using GoTSkillZ.Application.Transforms.DTO;
using GoTSkillZ.Models.Membership.Data;
using GoTSkillZ.Models.UserDataExtension.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GoTSkillZ.CoreServices.MembershipService.Interfaces
{
    public interface IMembershipService
    {
        #region Users
        IEnumerable<Users> QueryMembers(Expression<Func<Users, bool>> predicate);
        void DeactivateUser(int membershipEntityId);
        Users GetUserByGoogleToken(string googleToken);

        Users GetUserByGooglePublicId(string googlePublicId);
        IEnumerable<Users> GetUsersInRole(Roles role);

        IEnumerable<Users> GetUsersByRole(string roleName);
        IEnumerable<Users> GetAllUsers();

        UserProfileDTO GetUserProfileMetaData(int userId);

        Users SaveUser(Users user);

        UserProfileDTO GetUser(int userId, bool readOnly);

        UserProfileDTO GetUserHeaderData(int userId);
        Users GetMember(int userId);
        #endregion

        #region Roles
        List<Roles> GetAllRoles();

        Roles GetRole(int roleId);
        #endregion


        List<UserAchievementsDTO> GetUserAchievements(int userId);

        UserSocialLinks GetUserSocialLinks(int userId);

        bool CheckIfUserIsASubscriber(int userId);

        bool CheckIfUserIsASponsor(int userId);


        List<CountryDTO> FindCountries(string searchKey);

        List<StateDTO> FindState(string searchKey, int countryId);

        List<CityDTO> FindCity(string searchKey, int countryId, int stateId);


        string SaveUserProfileData(UserProfileDTO userProfileDTO);


        List<UserOccupation> GetUserOccupations();


        string AddNewUserSetup(List<UserSetupDataDTO> newUserSetupObj);

        string UpdateUserSetup(List<UserSetupDataDTO> updateUserSetup);

        string RemoveUserSetup(string setupId);
    }
}