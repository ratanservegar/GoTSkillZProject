using GoTSkillZ.Application.Transforms.DTO;
using GoTSkillZ.Models.UserSetup.Data;
using System.Collections.Generic;
using System.IO;

namespace GoTSkillZ.CoreServices.UserExtenionService.Interfaces
{
    public interface IUserExtensionService
    {
        List<UserAchievementsDTO> GetUserAchievements(int userId);

        List<UserSetupDTO> GetUserSetups(int userId);

        string SaveUserFiles(int userId, Stream fileStream);

        string SaveUserSetupImages(int userId, int setupId, Stream fileStream, string fileName);

        string SaveUserPeripheralImages(int userId, Stream fileStream, string fileName);

        string RemoveSetupImages(int userId, int setupId, string fileName);

        string RemovePeripheralImages(int userId, string fileName);

        string RemoveProfileImage(int userId, string fileName);


        List<SetupType> GetSetupTypes();
        List<SetupOption> GetSetupOptions();


        List<KendoFileDTO> GetSetupImages(int userId, int setupId);

        List<KendoFileDTO> GetPeripheralImages(int userId);
        List<KendoFileDTO> GetUserProfileImage(int userId);


        string SaveCSGOMainConfig(int userId, Stream fileStream, string fileName);

        string RemoveCSGOMainConfig(int userId, string fileName);

        List<KendoFileDTO> GetCSGOMainConfig(int userId);



        string SaveCSGOAutoexecConfig(int userId, Stream fileStream, string fileName);

        string RemoveCSGOAutoexecConfig(int userId, string fileName);

        List<KendoFileDTO> GetCSGOAutoexecConfig(int userId);



        string SaveCSGOPracConfig(int userId, Stream fileStream, string fileName);

        string RemoveCSGOPracConfig(int userId, string fileName);

        List<KendoFileDTO> GetCSGOPracConfig(int userId);



        List<AllConfigFileDTO> GetAllUserCSConfigFiles(int userId);


    }
}