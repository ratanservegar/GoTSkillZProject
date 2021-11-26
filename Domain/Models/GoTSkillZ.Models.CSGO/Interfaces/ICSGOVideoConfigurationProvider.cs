using GoTSkillZ.DataUtilities.Core.Interfaces;
using GoTSkillZ.Models.CSGO.Data;

namespace GoTSkillZ.Models.CSGO.Interfaces
{
    public interface ICSGOVideoConfigurationProvider : IRepositoryModel<CSGOVideoConfiguration>
    {
        string DeleteCSGOVideoConfiguration(int userId);

    }
}