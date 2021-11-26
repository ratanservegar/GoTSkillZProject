using GoTSkillZ.DataUtilities.Core.Interfaces;
using GoTSkillZ.Models.UserSetup.Data;

namespace GoTSkillZ.Models.UserSetup.Interfaces
{
    public interface IUserPeripheralsProvider : IRepositoryModel<UserPeripherals>
    {
        string DeleteUserPeripheral(string userPeripheralId);
    }
}