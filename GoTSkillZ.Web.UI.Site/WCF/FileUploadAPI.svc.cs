using GoTSkillZ.Application.Transforms.DTO;
using GoTSkillZ.CoreServices.UserExtenionService.Interfaces;
using GoTSkillZ.CoreServices.UserExtenionService.Services;
using GoTSkillZ.Security.Services.Interfaces;
using GoTSkillZ.Security.Services.Providers;
using HttpMultipartParser;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Web;

namespace GoTSkillZ.Web.UI.Site.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "FileUploadAPI" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select FileUploadAPI.svc or FileUploadAPI.svc.cs at the Solution Explorer and start debugging.
    public class FileUploadAPI : IFileUploadAPI
    {
        private readonly IUserExtensionService _userExtensionService;
        private readonly IGoTSkillZSecurityService _gotSkillZSecurityService;

        public FileUploadAPI()
        {
            _userExtensionService = new UserExtensionService();
            _gotSkillZSecurityService = new GoTSkillZSecurityService();
        }


        public string UploadProfileImage(Stream data)
        {
            var parser = new MultipartFormDataParser(data);

            // From this point the data is parsed, we can retrieve the
            // form data using the GetParameterValue method.
            var userId = parser.GetParameterValue("userId");

            // Files are stored in a list:
            var file = parser.Files.First();
            Stream data2 = file.Data;


            return _userExtensionService.SaveUserFiles(int.Parse(userId), data2);

        }

        public string UploadSetupImages(Stream data)
        {
            var returnString = "";
            var parser = new MultipartFormDataParser(data);

            // From this point the data is parsed, we can retrieve the
            // form data using the GetParameterValue method.
            var userId = parser.GetParameterValue("userId");
            var setupId = parser.GetParameterValue("setupId");

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(setupId))
            {
                // Files are stored in a list:
                var currentFile = parser.Files.First();
                Stream streamData = currentFile.Data;
                returnString = _userExtensionService.SaveUserSetupImages(int.Parse(userId), int.Parse(setupId), streamData, currentFile.FileName);
            }

            return returnString;
        }

        public string UploadPeripheralImages(Stream data)
        {
            var returnString = "";
            var parser = new MultipartFormDataParser(data);

            // From this point the data is parsed, we can retrieve the
            // form data using the GetParameterValue method.
            var userId = parser.GetParameterValue("userId");


            if (!string.IsNullOrEmpty(userId))
            {
                // Files are stored in a list:
                var currentFile = parser.Files.First();
                Stream streamData = currentFile.Data;
                returnString = _userExtensionService.SaveUserPeripheralImages(int.Parse(userId), streamData, currentFile.FileName);
            }

            return returnString;
        }

        public string RemoveSetupImages(Stream data)
        {
            var returnString = "";

            string strData = new StreamReader(data).ReadToEnd();
            NameValueCollection nvc = HttpUtility.ParseQueryString(strData);



            var userId = nvc["userId"];
            var setupId = nvc["setupId"];
            var fileName = nvc["fileNames"];

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(setupId) && !string.IsNullOrEmpty(fileName))
            {
                returnString = _userExtensionService.RemoveSetupImages(int.Parse(userId), int.Parse(setupId), fileName);
            }

            return returnString;
        }

        public string RemovePeripheralImages(Stream data)
        {
            var returnString = "";

            string strData = new StreamReader(data).ReadToEnd();
            NameValueCollection nvc = HttpUtility.ParseQueryString(strData);



            var userId = nvc["userId"];
            var fileName = nvc["fileNames"];

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(fileName))
            {
                returnString = _userExtensionService.RemovePeripheralImages(int.Parse(userId), fileName);
            }

            return returnString;
        }

        public List<KendoFileDTO> GetSetupImages(string userId, string setupId)
        {
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(setupId))
            {
                return _userExtensionService.GetSetupImages(int.Parse(userId), int.Parse(setupId));
            }

            return null;
        }

        public List<KendoFileDTO> GetPeripheralImages(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                return _userExtensionService.GetPeripheralImages(int.Parse(userId));
            }

            return null;
        }

        public List<KendoFileDTO> GetUserProfileImage(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                return _userExtensionService.GetUserProfileImage(int.Parse(userId));
            }

            return null;
        }


        public string RemoveProfileImage(Stream data)
        {
            var returnString = "";

            string strData = new StreamReader(data).ReadToEnd();
            NameValueCollection nvc = HttpUtility.ParseQueryString(strData);



            var userId = nvc["userId"];
            var fileName = nvc["fileNames"];

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(fileName))
            {
                returnString = _userExtensionService.RemoveProfileImage(int.Parse(userId), fileName);
            }

            return returnString;
        }

        public string UploadCSGOMainConfig(Stream data)
        {
            var returnString = "";
            var parser = new MultipartFormDataParser(data);

            // From this point the data is parsed, we can retrieve the
            // form data using the GetParameterValue method.
            var userId = parser.GetParameterValue("userId") ?? _gotSkillZSecurityService.GetLoggedInUserCookieData().UserId.ToString();

            if (!string.IsNullOrEmpty(userId))
            {
                // Files are stored in a list:
                var currentFile = parser.Files.First();
                Stream streamData = currentFile.Data;
                returnString = _userExtensionService.SaveCSGOMainConfig(int.Parse(userId), streamData, currentFile.FileName);
            }

            return returnString;
        }

        public string RemoveCSGOMainConfig(Stream data)
        {
            var returnString = "";

            string strData = new StreamReader(data).ReadToEnd();
            NameValueCollection nvc = HttpUtility.ParseQueryString(strData);



            var userId = nvc["userId"];
            var fileName = nvc["fileNames"];

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(fileName))
            {
                returnString = _userExtensionService.RemoveCSGOMainConfig(int.Parse(userId), fileName);
            }

            return returnString;
        }

        public string RemoveCSGOMainConfigForUser(string fileName)
        {
            var returnString = "";
            var responseObj = _gotSkillZSecurityService.GetLoggedInUserCookieData();
            if (responseObj.UserId != 0 && !string.IsNullOrEmpty(fileName))
            {
                returnString = _userExtensionService.RemoveCSGOMainConfig(responseObj.UserId, fileName);
            }

            return returnString;
        }

        public List<KendoFileDTO> GetCSGOMainConfigFile(string userId)
        {

            if (string.IsNullOrEmpty(userId)) return new List<KendoFileDTO>();

            return _userExtensionService.GetCSGOMainConfig(int.Parse(userId));
        }

        public string UploadCSGOAutoexecConfig(Stream data)
        {
            var returnString = "";
            var parser = new MultipartFormDataParser(data);

            // From this point the data is parsed, we can retrieve the
            // form data using the GetParameterValue method.
            var userId = parser.GetParameterValue("userId") ??
                         _gotSkillZSecurityService.GetLoggedInUserCookieData().UserId.ToString();

            if (!string.IsNullOrEmpty(userId))
            {
                // Files are stored in a list:
                var currentFile = parser.Files.First();
                Stream streamData = currentFile.Data;
                returnString = _userExtensionService.SaveCSGOAutoexecConfig(int.Parse(userId), streamData, currentFile.FileName);
            }

            return returnString;
        }

        public string RemoveCSGOAutoexecConfig(Stream data)
        {
            var returnString = "";

            string strData = new StreamReader(data).ReadToEnd();
            NameValueCollection nvc = HttpUtility.ParseQueryString(strData);



            var userId = nvc["userId"];
            var fileName = nvc["fileNames"];

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(fileName))
            {
                returnString = _userExtensionService.RemoveCSGOAutoexecConfig(int.Parse(userId), fileName);
            }

            return returnString;
        }

        public string RemoveCSGOAutoexecConfigForUser(string fileName)
        {
            var returnString = "";
            var responseObj = _gotSkillZSecurityService.GetLoggedInUserCookieData();
            if (responseObj.UserId != 0 && !string.IsNullOrEmpty(fileName))
            {
                returnString = _userExtensionService.RemoveCSGOAutoexecConfig(responseObj.UserId, fileName);
            }

            return returnString;
        }

        public List<KendoFileDTO> GetCSGOAutoexecConfigFile(string userId)
        {
            if (string.IsNullOrEmpty(userId)) return new List<KendoFileDTO>();

            return _userExtensionService.GetCSGOAutoexecConfig(int.Parse(userId));
        }

        public string UploadCSGOPracConfig(Stream data)
        {
            var returnString = "";
            var parser = new MultipartFormDataParser(data);

            // From this point the data is parsed, we can retrieve the
            // form data using the GetParameterValue method.
            var userId = parser.GetParameterValue("userId") ??
                         _gotSkillZSecurityService.GetLoggedInUserCookieData().UserId.ToString();

            if (!string.IsNullOrEmpty(userId))
            {
                // Files are stored in a list:
                var currentFile = parser.Files.First();
                Stream streamData = currentFile.Data;
                returnString = _userExtensionService.SaveCSGOPracConfig(int.Parse(userId), streamData, currentFile.FileName);
            }

            return returnString;
        }

        public string RemoveCSGOPracConfig(Stream data)
        {
            var returnString = "";

            string strData = new StreamReader(data).ReadToEnd();
            NameValueCollection nvc = HttpUtility.ParseQueryString(strData);



            var userId = nvc["userId"];
            var fileName = nvc["fileNames"];

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(fileName))
            {
                returnString = _userExtensionService.RemoveCSGOPracConfig(int.Parse(userId), fileName);
            }

            return returnString;
        }

        public string RemoveCSGOPracConfigForUser(string fileName)
        {
            var returnString = "";
            var responseObj = _gotSkillZSecurityService.GetLoggedInUserCookieData();
            if (responseObj.UserId != 0 && !string.IsNullOrEmpty(fileName))
            {
                returnString = _userExtensionService.RemoveCSGOPracConfig(responseObj.UserId, fileName);
            }

            return returnString;
        }

        public List<KendoFileDTO> GetCSGOPracConfigFIle(string userId)
        {
            if (string.IsNullOrEmpty(userId)) return new List<KendoFileDTO>();

            return _userExtensionService.GetCSGOPracConfig(int.Parse(userId));
        }

        public List<AllConfigFileDTO> GetAllUserCSConfigFiles()
        {
            var responseObj = _gotSkillZSecurityService.GetLoggedInUserCookieData();
            if (responseObj.Success == false) return new List<AllConfigFileDTO>();

            return _userExtensionService.GetAllUserCSConfigFiles(responseObj.UserId);

        }
    }
}
