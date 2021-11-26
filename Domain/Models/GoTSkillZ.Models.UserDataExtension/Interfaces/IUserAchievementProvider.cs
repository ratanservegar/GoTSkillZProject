using GoTSkillZ.DataUtilities.Core.Interfaces;
using GoTSkillZ.Models.UserDataExtension.Data;

namespace GoTSkillZ.Models.UserDataExtension.Interfaces
{
    public interface IUserAchievementProvider : IRepositoryModel<UserAchievement>
    {
        string DeleteUserAchievements(string userAchievementIds);
    }
}