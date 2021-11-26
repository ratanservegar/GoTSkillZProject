using GoTSkillZ.Application.Transforms.DTO;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace GoTSkillZ.Web.UI.Site.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IFileUploadAPI" in both code and config file together.
    [ServiceContract]
    public interface IFileUploadAPI
    {
        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/UploadProfileImage", ResponseFormat = WebMessageFormat.Json)]
        string UploadProfileImage(Stream data);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/UploadSetupImages", ResponseFormat = WebMessageFormat.Json)]
        string UploadSetupImages(Stream data);



        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/UploadPeripheralImages", ResponseFormat = WebMessageFormat.Json)]
        string UploadPeripheralImages(Stream data);


        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RemoveSetupImages", ResponseFormat = WebMessageFormat.Json)]
        string RemoveSetupImages(Stream data);


        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RemovePeripheralImages", ResponseFormat = WebMessageFormat.Json)]
        string RemovePeripheralImages(Stream data);


        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/GetSetupImages/{userId}/{setupId}", ResponseFormat = WebMessageFormat.Json)]
        List<KendoFileDTO> GetSetupImages(string userId, string setupId);

        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/GetPeripheralImages/{userId}", ResponseFormat = WebMessageFormat.Json)]
        List<KendoFileDTO> GetPeripheralImages(string userId);


        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/GetUserProfileImage/{userId}", ResponseFormat = WebMessageFormat.Json)]
        List<KendoFileDTO> GetUserProfileImage(string userId);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RemoveProfileImage",
            ResponseFormat = WebMessageFormat.Json)]
        string RemoveProfileImage(Stream data);


        // CS GO Main Config --


        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/UploadCSGOMainConfig", ResponseFormat = WebMessageFormat.Json)]
        string UploadCSGOMainConfig(Stream data);


        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RemoveCSGOMainConfig",
            ResponseFormat = WebMessageFormat.Json)]
        string RemoveCSGOMainConfig(Stream data);


        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RemoveCSGOMainConfigForUser/{fileName}",
         ResponseFormat = WebMessageFormat.Json)]
        string RemoveCSGOMainConfigForUser(string fileName);

        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/GetCSGOMainConfigFile/{userId}", ResponseFormat = WebMessageFormat.Json)]
        List<KendoFileDTO> GetCSGOMainConfigFile(string userId);


        // CS GO Autoexec Config --

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/UploadCSGOAutoexecConfig", ResponseFormat = WebMessageFormat.Json)]
        string UploadCSGOAutoexecConfig(Stream data);


        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RemoveCSGOAutoexecConfig",
            ResponseFormat = WebMessageFormat.Json)]
        string RemoveCSGOAutoexecConfig(Stream data);


        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RemoveCSGOAutoexecConfigForUser/{fileName}",
         ResponseFormat = WebMessageFormat.Json)]
        string RemoveCSGOAutoexecConfigForUser(string fileName);

        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/GetCSGOAutoexecConfigFile/{userId}", ResponseFormat = WebMessageFormat.Json)]
        List<KendoFileDTO> GetCSGOAutoexecConfigFile(string userId);

        // CS GO prac Config --

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/UploadCSGOPracConfig", ResponseFormat = WebMessageFormat.Json)]
        string UploadCSGOPracConfig(Stream data);


        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RemoveCSGOPracConfig",
            ResponseFormat = WebMessageFormat.Json)]
        string RemoveCSGOPracConfig(Stream data);


        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RemoveCSGOPracConfigForUser/{fileName}",
         ResponseFormat = WebMessageFormat.Json)]
        string RemoveCSGOPracConfigForUser(string fileName);

        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/GetCSGOPracConfigFIle/{userId}", ResponseFormat = WebMessageFormat.Json)]
        List<KendoFileDTO> GetCSGOPracConfigFIle(string userId);



        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/GetAllUserCSConfigFiles", ResponseFormat = WebMessageFormat.Json)]
        List<AllConfigFileDTO> GetAllUserCSConfigFiles();

    }
}