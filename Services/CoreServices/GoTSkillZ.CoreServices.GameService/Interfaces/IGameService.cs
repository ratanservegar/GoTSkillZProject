using GoTSkillZ.Application.Transforms.DTO;
using GoTSkillZ.Models.CSGO.Data;
using System.Collections.Generic;

namespace GoTSkillZ.CoreServices.GameService.Interfaces
{
    public interface IGameService
    {
        List<GameTypeDTO> GetGameTypes();
        List<GameRoleDTO> GetGameRoles();


        string[] GetUserCSGOMainConfig(int userId);

        string[] GetUserCSGOAutoexecConfig(int userId);

        string[] GetUserCSGOPracConfig(int userId);


        CSGOVideoConfiguration GetCSGOVideoConfiguration(int userId);

        string SaveCSGOVideoConfiguration(CSGOVideoConfigurationDTO csgoVideoConfigurationDto);

        string DeleteCSGOVideoConfiguration(int userId);


        string SaveCSGOSensitivity(CSGOSensitivityDTO csgoSensitivityDto);

        CSGOSensitivityDTO GetActiveCSGOSensitivity(int userId);
    }
}