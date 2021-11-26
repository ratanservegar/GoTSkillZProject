using GoTSkillZ.DataUtilities.Core.Interfaces;
using GoTSkillZ.Models.UserSetup.Data;

namespace GoTSkillZ.Models.UserSetup.Interfaces
{
    public interface IUserSetupDataProvider : IRepositoryModel<UserSetupData>
    {
        string DeleteUserSetupData(string userSetupId);
    }
}