using GoTSkillZ.DataUtilities.Core.Interfaces;
using GoTSkillZ.Models.Membership.Data;
using System.Collections.Generic;

namespace GoTSkillZ.Models.Membership.Interfaces
{
    public interface IUserRoleProvider : IRepositoryModel<UserRoles>
    {
        void SaveUserRoles(List<UserRoles> userRoles);

        void Delete(int userId, int roleId);
    }
}