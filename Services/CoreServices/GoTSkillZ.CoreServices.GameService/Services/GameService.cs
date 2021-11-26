using GoTSkillZ.Application.Transforms.DTO;
using GoTSkillZ.CoreServices.GameService.Interfaces;
using GoTSkillZ.Models.CSGO.Data;
using GoTSkillZ.Models.CSGO.Interfaces;
using GoTSkillZ.Models.CSGO.Provider;
using GoTSkillZ.Models.Game.Interfaces;
using GoTSkillZ.Models.Game.Provider;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace GoTSkillZ.CoreServices.GameService.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRolesProvider _gameRolesProvider;
        private readonly IGameTypesProvider _gameTypesProvider;
        private readonly ICSGOSensitivityProvider _csgoSensitivityProvider;
        private readonly ICSGOVideoConfigurationProvider _csgoVideoConfigurationProvider;


        public GameService()
        {
            _gameRolesProvider = new GameRolesProvider();
            _gameTypesProvider = new GameTypesProvider();
            _csgoSensitivityProvider = new CSGOSensitivityProvider();
            _csgoVideoConfigurationProvider = new CSGOConfigurationProvider();

        }

        public List<GameTypeDTO> GetGameTypes()
        {
            return _gameTypesProvider.GetAll().Select(x => new GameTypeDTO
            {
                Id = x.Id,
                GameName = x.GameName
            }).ToList();
        }

        public List<GameRoleDTO> GetGameRoles()
        {
            return _gameRolesProvider.GetAll().Select(x => new GameRoleDTO
            {
                Id = x.Id,
                RoleName = x.RoleName,
                GameTypeId = x.GameTypeId
            }).ToList();
        }

        public string[] GetUserCSGOMainConfig(int userId)
        {
            var returnString = new string[] { };


            var userFileLocation = ConfigurationManager.AppSettings["UserFiles"] + userId + "\\CSGO\\Config\\main\\";

            if (Directory.Exists(userFileLocation))
            {
                var files = Directory.GetFiles(userFileLocation, "*.*", SearchOption.TopDirectoryOnly);

                foreach (var file in files)
                {
                    returnString = File.ReadAllLines(file);
                }
            }
            return returnString;
        }

        public string[] GetUserCSGOAutoexecConfig(int userId)
        {
            var returnString = new string[] { };


            var userFileLocation = ConfigurationManager.AppSettings["UserFiles"] + userId + "\\CSGO\\Config\\autoexec\\";

            if (Directory.Exists(userFileLocation))
            {
                var files = Directory.GetFiles(userFileLocation, "*.*", SearchOption.TopDirectoryOnly);

                foreach (var file in files)
                {
                    returnString = File.ReadAllLines(file);
                }
            }
            return returnString;
        }

        public string[] GetUserCSGOPracConfig(int userId)
        {
            var returnString = new string[] { };


            var userFileLocation = ConfigurationManager.AppSettings["UserFiles"] + userId + "\\CSGO\\Config\\practice\\";

            if (Directory.Exists(userFileLocation))
            {
                var files = Directory.GetFiles(userFileLocation, "*.*", SearchOption.TopDirectoryOnly);

                foreach (var file in files)
                {
                    returnString = File.ReadAllLines(file);
                }
            }
            return returnString;
        }

        public CSGOVideoConfiguration GetCSGOVideoConfiguration(int userId)
        {
            if (userId == 0) return null;

            return _csgoVideoConfigurationProvider.FindBy(x => x.UserId == userId).FirstOrDefault();
        }

        public string SaveCSGOVideoConfiguration(CSGOVideoConfigurationDTO csgoVideoConfigurationDto)
        {
            var returnString = "Success";
            if (csgoVideoConfigurationDto == null) return "Failed: Empty Object Passed";


            if (csgoVideoConfigurationDto.Id == 0)
            {
                var newCSGoVideoConfiguration = new CSGOVideoConfiguration
                {
                    UserId = csgoVideoConfigurationDto.UserId,
                    AspectRatio = csgoVideoConfigurationDto.AspectRatio,
                    ColorMode = csgoVideoConfigurationDto.ColorMode,
                    Brightness = csgoVideoConfigurationDto.Brightness,
                    DisplayMode = csgoVideoConfigurationDto.DisplayMode,
                    EffectDetail = csgoVideoConfigurationDto.EffectDetail,
                    FXXAAAnti_Aliasing = csgoVideoConfigurationDto.FXXAAAnti_Aliasing,
                    GameView = csgoVideoConfigurationDto.GameView,
                    GlobalShadowQuality = csgoVideoConfigurationDto.GlobalShadowQuality,
                    LaptopPowerSavings = csgoVideoConfigurationDto.LaptopPowerSavings,
                    ModelTextureDetail = csgoVideoConfigurationDto.ModelTextureDetail,
                    MotionBlur = csgoVideoConfigurationDto.MotionBlur,
                    MultiCoreRendering = csgoVideoConfigurationDto.MultiCoreRendering,
                    MultisamplingAntiAliasingMode = csgoVideoConfigurationDto.MultisamplingAntiAliasingMode,
                    Resolution = csgoVideoConfigurationDto.Resolution,
                    ShaderDetail = csgoVideoConfigurationDto.ShaderDetail,
                    TextureFilteringMode = csgoVideoConfigurationDto.TextureFilteringMode,
                    TripleMonitorMode = csgoVideoConfigurationDto.TripleMonitorMode,
                    WaitForVerticalSync = csgoVideoConfigurationDto.WaitForVerticalSync,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = null
                };

                var insertItem = _csgoVideoConfigurationProvider.Add(newCSGoVideoConfiguration);

                if (insertItem == null) returnString = "Failed: to save CSGO video configuration";
            }
            else
            {

                var existingCSGOVideoConfig =
                    _csgoVideoConfigurationProvider.FindBy(x => x.UserId == csgoVideoConfigurationDto.UserId).FirstOrDefault();

                if (existingCSGOVideoConfig != null)
                {
                    existingCSGOVideoConfig.UserId = csgoVideoConfigurationDto.UserId;
                    existingCSGOVideoConfig.AspectRatio = csgoVideoConfigurationDto.AspectRatio;
                    existingCSGOVideoConfig.ColorMode = csgoVideoConfigurationDto.ColorMode;
                    existingCSGOVideoConfig.Brightness = csgoVideoConfigurationDto.Brightness;
                    existingCSGOVideoConfig.DisplayMode = csgoVideoConfigurationDto.DisplayMode;
                    existingCSGOVideoConfig.EffectDetail = csgoVideoConfigurationDto.EffectDetail;
                    existingCSGOVideoConfig.FXXAAAnti_Aliasing = csgoVideoConfigurationDto.FXXAAAnti_Aliasing;
                    existingCSGOVideoConfig.GameView = csgoVideoConfigurationDto.GameView;
                    existingCSGOVideoConfig.GlobalShadowQuality = csgoVideoConfigurationDto.GlobalShadowQuality;
                    existingCSGOVideoConfig.LaptopPowerSavings = csgoVideoConfigurationDto.LaptopPowerSavings;
                    existingCSGOVideoConfig.ModelTextureDetail = csgoVideoConfigurationDto.ModelTextureDetail;
                    existingCSGOVideoConfig.MotionBlur = csgoVideoConfigurationDto.MotionBlur;
                    existingCSGOVideoConfig.MultiCoreRendering = csgoVideoConfigurationDto.MultiCoreRendering;
                    existingCSGOVideoConfig.MultisamplingAntiAliasingMode = csgoVideoConfigurationDto.MultisamplingAntiAliasingMode;
                    existingCSGOVideoConfig.Resolution = csgoVideoConfigurationDto.Resolution;
                    existingCSGOVideoConfig.ShaderDetail = csgoVideoConfigurationDto.ShaderDetail;
                    existingCSGOVideoConfig.TextureFilteringMode = csgoVideoConfigurationDto.TextureFilteringMode;
                    existingCSGOVideoConfig.TripleMonitorMode = csgoVideoConfigurationDto.TripleMonitorMode;
                    existingCSGOVideoConfig.WaitForVerticalSync = csgoVideoConfigurationDto.WaitForVerticalSync;
                    existingCSGOVideoConfig.CreatedDate = csgoVideoConfigurationDto.CreatedDate;
                    existingCSGOVideoConfig.ModifiedDate = DateTime.Now;

                    var updateItem = _csgoVideoConfigurationProvider.Update(existingCSGOVideoConfig);

                    if (updateItem == null) returnString = "Failed: to save CSGO video configuration";
                }
            }




            return returnString;
        }

        public string DeleteCSGOVideoConfiguration(int userId)
        {
            return _csgoVideoConfigurationProvider.DeleteCSGOVideoConfiguration(userId);
        }

        public string SaveCSGOSensitivity(CSGOSensitivityDTO csgoSensitivityDto)
        {
            var returnString = "Success";
            if (csgoSensitivityDto == null) return "Failed: Empty Object Passed";


            if (csgoSensitivityDto.UserId != 0)
            {

                var existingSensiObj =
                    _csgoSensitivityProvider.FindBy(x => x.UserId == csgoSensitivityDto.UserId && x.Active == true).FirstOrDefault();

                if (existingSensiObj != null)
                {
                    existingSensiObj.Active = false;
                    existingSensiObj.EndDate = DateTime.Now;
                    existingSensiObj.EndUnixDatetime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                    _csgoSensitivityProvider.Update(existingSensiObj);
                }


                var csgoSensitvityObj = new CSGOSensitivity
                {
                    UserId = csgoSensitivityDto.UserId,
                    DPI = csgoSensitivityDto.DPI,
                    Sensitivity = decimal.Parse(csgoSensitivityDto.Sensitivity),
                    eDPI = csgoSensitivityDto.eDPI,
                    RawInput = csgoSensitivityDto.RawInput,
                    WindowsSensitivity = csgoSensitivityDto.WindowsSensitivity,
                    MouseHz = csgoSensitivityDto.MouseHz,
                    Active = true,
                    CreatedDate = DateTime.Now,
                    StartUnixDatetime = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
                };

                var insertItem = _csgoSensitivityProvider.Add(csgoSensitvityObj);

                if (insertItem == null) returnString = "Failed: to save CSGO Sensitivity";
            }


            return returnString;
        }

        public CSGOSensitivityDTO GetActiveCSGOSensitivity(int userId)
        {

            var returnObj = new CSGOSensitivityDTO();
            var csgoSensiObj = _csgoSensitivityProvider.FindBy(x => x.UserId == userId && x.Active).FirstOrDefault();

            if (csgoSensiObj != null)
            {
                returnObj.Sensitivity = csgoSensiObj.Sensitivity.ToString();
                returnObj.DPI = csgoSensiObj.DPI;
                returnObj.eDPI = csgoSensiObj.eDPI;
                returnObj.RawInput = csgoSensiObj.RawInput;
                returnObj.WindowsSensitivity = csgoSensiObj.WindowsSensitivity;
                returnObj.MouseHz = csgoSensiObj.MouseHz;
                returnObj.UserId = csgoSensiObj.UserId;
                returnObj.StartUnixDatetime = csgoSensiObj.StartUnixDatetime;
                returnObj.EndUnixDatetime = csgoSensiObj.EndUnixDatetime;
                returnObj.EndDate = csgoSensiObj.EndDate;
                returnObj.CreatedDate = csgoSensiObj.CreatedDate;

            }

            return returnObj;
        }
    }
}