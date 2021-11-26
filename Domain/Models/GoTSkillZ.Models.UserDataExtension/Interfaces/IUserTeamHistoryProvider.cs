using GoTSkillZ.DataUtilities.Core.Interfaces;
using GoTSkillZ.Models.UserDataExtension.Data;

namespace GoTSkillZ.Models.UserDataExtension.Interfaces
{
    public interface IUserTeamHistoryProvider : IRepositoryModel<UserTeamHistory>
    {
        string DeleteUserTeamHistory(string userTeamHistoryId);
    }
}