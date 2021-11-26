using GoTSkillZ.DataUtilities.Core.Interfaces;
using GoTSkillZ.Models.UserSetup.Data;

namespace GoTSkillZ.Models.UserSetup.Interfaces
{
    public interface IUserSetupsProvider : IRepositoryModel<UserSetups>
    {
        string DeleteUserSetup(string userSetupId);
    }
}