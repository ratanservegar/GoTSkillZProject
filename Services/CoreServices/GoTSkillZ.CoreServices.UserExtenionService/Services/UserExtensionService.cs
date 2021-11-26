using GoTSkillZ.Application.Transforms.DTO;
using GoTSkillZ.CoreServices.UserExtenionService.Interfaces;
using GoTSkillZ.Models.UserDataExtension.Interfaces;
using GoTSkillZ.Models.UserDataExtension.Provider;
using GoTSkillZ.Models.UserSetup.Data;
using GoTSkillZ.Models.UserSetup.Interfaces;
using GoTSkillZ.Models.UserSetup.Provider;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;

namespace GoTSkillZ.CoreServices.UserExtenionService.Services
{
    public class UserExtensionService : IUserExtensionService
    {
        private readonly ISetupOptionsProvider _setupOptionsProvider;
        private readonly ISetupTypesProvider _setupTypesProvider;
        private readonly IUserAchievementProvider _userAchievementProvider;
        private readonly IUserSetupsProvider _userSetupsProvider;


        public UserExtensionService()
        {
            _userAchievementProvider = new UserAchievementProvider();
            _userSetupsProvider = new UserSetupsProvider();
            _setupTypesProvider = new SetupTypesProvider();
            _setupOptionsProvider = new SetupOptionsProvider();

        }

        public List<UserAchievementsDTO> GetUserAchievements(int userId)
        {
            var userAchievements =
                _userAchievementProvider.FindBy(x => x.UserId == userId)
                    .OrderByDescending(x => x.Date)
                    .ToList()
                    .Select(x => new UserAchievementsDTO
                    {
                        Id = x.Id,
                        UserId = x.UserId,
                        Name = x.Name,
                        Description = x.Description,
                        Location = x.Location,
                        Position = x.Position,
                        Type = x.Type,
                        IsActive = x.IsActive.ToString(),
                        Date = x.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
                    }).ToList();

            return userAchievements;
        }


        public string SaveUserFiles(int userId, Stream fileStream)
        {
            var returnString = "success";
            var userFileLocation = ConfigurationManager.AppSettings["UserFiles"] + userId + "\\ProfileImage\\";

            try
            {
                if (!Directory.Exists(userFileLocation))
                    Directory.CreateDirectory(userFileLocation);


                using (var newFileStream = File.Create(userFileLocation + "\\ProfileImage.jpg"))
                {
                    fileStream.Seek(0, SeekOrigin.Begin);
                    fileStream.CopyTo(newFileStream);
                }
            }
            catch (Exception)
            {
                returnString = "false";
            }


            return returnString;
        }

        public string SaveUserSetupImages(int userId, int setupId, Stream fileStream, string fileName)
        {
            var returnString = "success";
            var userFileLocation = ConfigurationManager.AppSettings["UserFiles"] + userId + "\\SetupImages\\" + setupId +
                                   "\\";

            try
            {
                if (!Directory.Exists(userFileLocation))
                    Directory.CreateDirectory(userFileLocation);


                using (var newFileStream = File.Create(userFileLocation + "\\" + fileName))
                {
                    fileStream.Seek(0, SeekOrigin.Begin);
                    fileStream.CopyTo(newFileStream);
                }
            }
            catch (Exception)
            {
                returnString = "false";
            }


            return returnString;
        }

        public string SaveUserPeripheralImages(int userId, Stream fileStream, string fileName)
        {
            var returnString = "success";
            var userFileLocation = ConfigurationManager.AppSettings["UserFiles"] + userId + "\\PeripheralImages\\";

            try
            {
                if (!Directory.Exists(userFileLocation))
                    Directory.CreateDirectory(userFileLocation);


                using (var newFileStream = File.Create(userFileLocation + "\\" + fileName))
                {
                    fileStream.Seek(0, SeekOrigin.Begin);
                    fileStream.CopyTo(newFileStream);
                }
            }
            catch (Exception)
            {
                returnString = "false";
            }


            return returnString;
        }

        public string RemoveSetupImages(int userId, int setupId, string fileName)
        {
            var returnString = "success";
            var userFileLocation = ConfigurationManager.AppSettings["UserFiles"] + userId + "\\SetupImages\\" + setupId +
                                   "\\";

            try
            {
                if (Directory.Exists(userFileLocation))
                {
                    File.Delete(userFileLocation + "\\" + fileName);
                }
            }
            catch (Exception)
            {
                returnString = "false";
            }


            return returnString;
        }

        public string RemovePeripheralImages(int userId, string fileName)
        {
            var returnString = "success";
            var userFileLocation = ConfigurationManager.AppSettings["UserFiles"] + userId + "\\PeripheralImages\\";

            try
            {
                if (Directory.Exists(userFileLocation))
                {
                    File.Delete(userFileLocation + "\\" + fileName);
                }
            }
            catch (Exception)
            {
                returnString = "false";
            }


            return returnString;
        }


        public List<SetupType> GetSetupTypes()
        {
            return _setupTypesProvider.GetAll().OrderBy(x => x.SetupType1).ToList();
        }

        public List<SetupOption> GetSetupOptions()
        {
            return _setupOptionsProvider.GetAll().OrderBy(x => x.SetupOption1).ToList();
        }

        public List<KendoFileDTO> GetSetupImages(int userId, int setupId)
        {
            var setupImages = new List<KendoFileDTO>();

            var userFileLocation = ConfigurationManager.AppSettings["UserFiles"] + userId + "\\SetupImages\\" + setupId +
                                   "\\";

            try
            {
                if (Directory.Exists(userFileLocation))
                {
                    var files = Directory.GetFiles(userFileLocation, "*.*", SearchOption.TopDirectoryOnly);
                    foreach (var file in files)
                    {
                        var newSetupImage = new KendoFileDTO();

                        var currentFile = new FileInfo(file);
                        newSetupImage.name = currentFile.Name;
                        newSetupImage.extension = currentFile.Extension;
                        newSetupImage.size = currentFile.Length.ToString();
                        newSetupImage.imagePath = "/UserData/" + userId + "/SetupImages/" + setupId + "/" +
                                                  currentFile.Name;
                        setupImages.Add(newSetupImage);
                    }
                }
            }
            catch (Exception)
            {
            }


            return setupImages;
        }

        public List<KendoFileDTO> GetPeripheralImages(int userId)
        {
            var peripheralImages = new List<KendoFileDTO>();

            var userFileLocation = ConfigurationManager.AppSettings["UserFiles"] + userId + "\\PeripheralImages\\";

            try
            {
                if (Directory.Exists(userFileLocation))
                {
                    var files = Directory.GetFiles(userFileLocation, "*.*", SearchOption.TopDirectoryOnly);
                    foreach (var file in files)
                    {
                        var newPeripheralImage = new KendoFileDTO();

                        var currentFile = new FileInfo(file);
                        newPeripheralImage.name = currentFile.Name;
                        newPeripheralImage.extension = currentFile.Extension;
                        newPeripheralImage.size = currentFile.Length.ToString();
                        newPeripheralImage.imagePath = "/UserData/" + userId + "/PeripheralImages/" + currentFile.Name;
                        peripheralImages.Add(newPeripheralImage);
                    }
                }
            }
            catch (Exception)
            {
            }


            return peripheralImages;
        }

        public List<KendoFileDTO> GetUserProfileImage(int userId)
        {
            var profileImage = new List<KendoFileDTO>();

            var userFileLocation = ConfigurationManager.AppSettings["UserFiles"] + userId + "\\ProfileImage\\";

            try
            {
                if (Directory.Exists(userFileLocation))
                {
                    var files = Directory.GetFiles(userFileLocation, "*.*", SearchOption.TopDirectoryOnly);
                    foreach (var file in files)
                    {
                        var newProfileImage = new KendoFileDTO();

                        var currentFile = new FileInfo(file);
                        newProfileImage.name = currentFile.Name;
                        newProfileImage.extension = currentFile.Extension;
                        newProfileImage.size = currentFile.Length.ToString();
                        newProfileImage.imagePath = "/UserData/" + userId + "/ProfileImage/" + currentFile.Name;
                        profileImage.Add(newProfileImage);
                    }
                }
            }
            catch (Exception)
            {
            }


            return profileImage;
        }

        public string SaveCSGOMainConfig(int userId, Stream fileStream, string fileName)
        {
            var returnString = "success";
            var userFileLocation = ConfigurationManager.AppSettings["UserFiles"] + userId + "\\CSGO\\Config\\main\\";

            try
            {
                if (!Directory.Exists(userFileLocation))
                    Directory.CreateDirectory(userFileLocation);


                using (var newFileStream = File.Create(userFileLocation + "\\" + fileName))
                {
                    fileStream.Seek(0, SeekOrigin.Begin);
                    fileStream.CopyTo(newFileStream);
                }
            }
            catch (Exception)
            {
                returnString = "false";
            }


            return returnString;
        }

        public string RemoveCSGOMainConfig(int userId, string fileName)
        {
            var returnString = "success";
            var userFileLocation = ConfigurationManager.AppSettings["UserFiles"] + userId + "\\CSGO\\Config\\main\\";

            try
            {
                if (Directory.Exists(userFileLocation))
                {
                    File.Delete(userFileLocation + "\\" + fileName);
                }
            }
            catch (Exception)
            {
                returnString = "false";
            }


            return returnString;
        }

        public List<KendoFileDTO> GetCSGOMainConfig(int userId)
        {
            var csgoConfig = new List<KendoFileDTO>();

            var userFileLocation = ConfigurationManager.AppSettings["UserFiles"] + userId + "\\CSGO\\Config\\main\\";

            try
            {
                if (Directory.Exists(userFileLocation))
                {
                    var files = Directory.GetFiles(userFileLocation, "*.*", SearchOption.TopDirectoryOnly);
                    foreach (var file in files)
                    {
                        var csgoConfigFile = new KendoFileDTO();

                        var currentFile = new FileInfo(file);
                        csgoConfigFile.name = currentFile.Name;
                        csgoConfigFile.extension = currentFile.Extension;
                        csgoConfigFile.size = currentFile.Length.ToString();
                        csgoConfigFile.imagePath = "/UserData/" + userId + "/CSGO/Config/main/" + currentFile.Name;
                        csgoConfigFile.userId = userId;
                        csgoConfig.Add(csgoConfigFile);
                    }
                }
            }
            catch (Exception)
            {
            }


            return csgoConfig;
        }

        public string SaveCSGOAutoexecConfig(int userId, Stream fileStream, string fileName)
        {
            var returnString = "success";
            var userFileLocation = ConfigurationManager.AppSettings["UserFiles"] + userId + "\\CSGO\\Config\\autoexec\\";

            try
            {
                if (!Directory.Exists(userFileLocation))
                    Directory.CreateDirectory(userFileLocation);


                using (var newFileStream = File.Create(userFileLocation + "\\" + fileName))
                {
                    fileStream.Seek(0, SeekOrigin.Begin);
                    fileStream.CopyTo(newFileStream);
                }
            }
            catch (Exception)
            {
                returnString = "false";
            }


            return returnString;
        }

        public string RemoveCSGOAutoexecConfig(int userId, string fileName)
        {
            var returnString = "success";
            var userFileLocation = ConfigurationManager.AppSettings["UserFiles"] + userId + "\\CSGO\\Config\\autoexec\\";

            try
            {
                if (Directory.Exists(userFileLocation))
                {
                    File.Delete(userFileLocation + "\\" + fileName);
                }
            }
            catch (Exception)
            {
                returnString = "false";
            }


            return returnString;
        }

        public List<KendoFileDTO> GetCSGOAutoexecConfig(int userId)
        {
            var csgoConfig = new List<KendoFileDTO>();

            var userFileLocation = ConfigurationManager.AppSettings["UserFiles"] + userId + "\\CSGO\\Config\\autoexec\\";

            try
            {
                if (Directory.Exists(userFileLocation))
                {
                    var files = Directory.GetFiles(userFileLocation, "*.*", SearchOption.TopDirectoryOnly);
                    foreach (var file in files)
                    {
                        var csgoConfigFile = new KendoFileDTO();

                        var currentFile = new FileInfo(file);
                        csgoConfigFile.name = currentFile.Name;
                        csgoConfigFile.extension = currentFile.Extension;
                        csgoConfigFile.size = currentFile.Length.ToString();
                        csgoConfigFile.imagePath = "/UserData/" + userId + "/CSGO/Config/autoexec/" + currentFile.Name;
                        csgoConfigFile.userId = userId;
                        csgoConfig.Add(csgoConfigFile);
                    }
                }
            }
            catch (Exception)
            {
            }


            return csgoConfig;
        }

        public string SaveCSGOPracConfig(int userId, Stream fileStream, string fileName)
        {
            var returnString = "success";
            var userFileLocation = ConfigurationManager.AppSettings["UserFiles"] + userId + "\\CSGO\\Config\\practice\\";

            try
            {
                if (!Directory.Exists(userFileLocation))
                    Directory.CreateDirectory(userFileLocation);


                using (var newFileStream = File.Create(userFileLocation + "\\" + fileName))
                {
                    fileStream.Seek(0, SeekOrigin.Begin);
                    fileStream.CopyTo(newFileStream);
                }
            }
            catch (Exception)
            {
                returnString = "false";
            }


            return returnString;
        }

        public string RemoveCSGOPracConfig(int userId, string fileName)
        {
            var returnString = "success";
            var userFileLocation = ConfigurationManager.AppSettings["UserFiles"] + userId + "\\CSGO\\Config\\practice\\";

            try
            {
                if (Directory.Exists(userFileLocation))
                {
                    File.Delete(userFileLocation + "\\" + fileName);
                }
            }
            catch (Exception)
            {
                returnString = "false";
            }


            return returnString;
        }

        public List<KendoFileDTO> GetCSGOPracConfig(int userId)
        {
            var csgoConfig = new List<KendoFileDTO>();

            var userFileLocation = ConfigurationManager.AppSettings["UserFiles"] + userId + "\\CSGO\\Config\\practice\\";

            try
            {
                if (Directory.Exists(userFileLocation))
                {
                    var files = Directory.GetFiles(userFileLocation, "*.*", SearchOption.TopDirectoryOnly);
                    foreach (var file in files)
                    {
                        var csgoConfigFile = new KendoFileDTO();

                        var currentFile = new FileInfo(file);
                        csgoConfigFile.name = currentFile.Name;
                        csgoConfigFile.extension = currentFile.Extension;
                        csgoConfigFile.size = currentFile.Length.ToString();
                        csgoConfigFile.imagePath = "/UserData/" + userId + "/CSGO/Config/practice/" + currentFile.Name;
                        csgoConfigFile.userId = userId;
                        csgoConfig.Add(csgoConfigFile);
                    }
                }
            }
            catch (Exception)
            {
            }


            return csgoConfig;
        }

        public List<AllConfigFileDTO> GetAllUserCSConfigFiles(int userId)
        {
            var allConfigFiles = new List<AllConfigFileDTO>();

            string[] folderArray = { "main", "autoexec", "practice" };


            foreach (var folder in folderArray)
            {
                var userFileLocation = ConfigurationManager.AppSettings["UserFiles"] + userId + "\\CSGO\\Config\\" +
                                       folder + "\\";

                try
                {
                    if (Directory.Exists(userFileLocation))
                    {
                        var files = Directory.GetFiles(userFileLocation, "*.*", SearchOption.TopDirectoryOnly);
                        foreach (var file in files)
                        {
                            var csgoConfigFile = new AllConfigFileDTO();

                            var currentFile = new FileInfo(file);
                            csgoConfigFile.name = currentFile.Name;
                            csgoConfigFile.extension = currentFile.Extension;
                            csgoConfigFile.size = currentFile.Length.ToString();
                            csgoConfigFile.filePath = "/UserData/" + userId + "/CSGO/Config/" + folder + "/" +
                                                      currentFile.Name;
                            csgoConfigFile.userId = userId;
                            csgoConfigFile.configType = folder;
                            allConfigFiles.Add(csgoConfigFile);
                        }
                    }
                }
                catch (Exception)
                {
                }
            }

            return allConfigFiles;
        }




        public string RemoveProfileImage(int userId, string fileName)
        {
            var returnString = "success";
            var userFileLocation = ConfigurationManager.AppSettings["UserFiles"] + userId + "\\ProfileImage\\";

            try
            {
                if (Directory.Exists(userFileLocation))
                {
                    File.Delete(userFileLocation + "\\" + fileName);
                }
            }
            catch (Exception)
            {
                returnString = "false";
            }


            return returnString;
        }


        public List<UserSetupDTO> GetUserSetups(int userId)
        {
            var userSetups = _userSetupsProvider.FindBy(x => x.UserId == userId).ToList().Select(x => new UserSetupDTO
            {
                Id = x.Id,
                UserId = x.UserId,
                SetupTypeId = x.SetupTypeId,
                SetupImagePath = x.SetupImagePath,
                SetupName = x.SetupName
            }).ToList();

            return userSetups;
        }
    }
}