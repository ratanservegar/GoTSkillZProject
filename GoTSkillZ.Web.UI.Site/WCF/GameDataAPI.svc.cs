using GoTSkillZ.Application.Transforms.DTO;
using GoTSkillZ.CoreServices.GameService.Interfaces;
using GoTSkillZ.CoreServices.GameService.Services;
using GoTSkillZ.Security.Services.Interfaces;
using GoTSkillZ.Security.Services.Providers;
using System.Collections.Generic;

namespace GoTSkillZ.Web.UI.Site.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "GameDataAPI" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select GameDataAPI.svc or GameDataAPI.svc.cs at the Solution Explorer and start debugging.
    public class GameDataAPI : IGameDataAPI
    {
        private readonly IGameService _gameService;

        public GameDataAPI()
        {
            _gameService = new GameService();
         
        }
        public List<GameTypeDTO> GetAllGameTypes()
        {
            return _gameService.GetGameTypes();
        }

        public List<GameRoleDTO> GetAllGameRoles()
        {
            return _gameService.GetGameRoles();
        }

        public string[] GetUserCSGOMainConfig(string userId)
        {
            if (string.IsNullOrEmpty(userId)) return new string[] { };

            return _gameService.GetUserCSGOMainConfig(int.Parse(userId));
        }

        public string[] GetUserCSGOAutoexecConfig(string userId)
        {
            if (string.IsNullOrEmpty(userId)) return new string[] { };
            return _gameService.GetUserCSGOAutoexecConfig(int.Parse(userId));
        }

        public string[] GetUserCSGOPracConfig(string userId)
        {
            if (string.IsNullOrEmpty(userId)) return new string[] { };
            return _gameService.GetUserCSGOPracConfig(int.Parse(userId));
        }

        public CSGOVideoConfigurationDTO GetCSGOVideoConfiguration(string userId)
        {
            var returnConfig = new CSGOVideoConfigurationDTO();
            if (string.IsNullOrEmpty(userId)) return null;

            var returnedConfig = _gameService.GetCSGOVideoConfiguration(int.Parse(userId));


            if (returnedConfig != null)
            {
                returnConfig = new CSGOVideoConfigurationDTO(returnedConfig);

            }



            return returnConfig;

        }

        public string SaveCSGOVideoConfiguration(CSGOVideoConfigurationDTO csgoVideoConfigurationDto)
        {
            return _gameService.SaveCSGOVideoConfiguration(csgoVideoConfigurationDto);
        }

        public string DeleteCSGOVideoConfiguration(string userId)
        {
            if (string.IsNullOrEmpty(userId)) return "Failed: Empty UserId String";

            return _gameService.DeleteCSGOVideoConfiguration(int.Parse(userId));
        }

        public string SaveCSGOSensitivity(CSGOSensitivityDTO csgoSensitivityDto)
        {
            return _gameService.SaveCSGOSensitivity(csgoSensitivityDto);
        }

        public CSGOSensitivityDTO GetActiveCSGOSensitivity(string userId)
        {
            if (string.IsNullOrEmpty(userId)) return new CSGOSensitivityDTO();

            return _gameService.GetActiveCSGOSensitivity(int.Parse(userId));
        }
    }
}
