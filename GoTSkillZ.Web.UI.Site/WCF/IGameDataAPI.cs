using GoTSkillZ.Application.Transforms.DTO;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace GoTSkillZ.Web.UI.Site.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IGameDataAPI" in both code and config file together.
    [ServiceContract]
    public interface IGameDataAPI
    {
        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetAllGameTypes", ResponseFormat = WebMessageFormat.Json)]
        List<GameTypeDTO> GetAllGameTypes();

        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetAllGameRoles", ResponseFormat = WebMessageFormat.Json)]
        List<GameRoleDTO> GetAllGameRoles();



        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetUserCSGOMainConfig/{userId}", ResponseFormat = WebMessageFormat.Json)]
        string[] GetUserCSGOMainConfig(string userId);

        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetUserCSGOAutoexecConfig/{userId}", ResponseFormat = WebMessageFormat.Json)]
        string[] GetUserCSGOAutoexecConfig(string userId);

        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetUserCSGOPracConfig/{userId}", ResponseFormat = WebMessageFormat.Json)]
        string[] GetUserCSGOPracConfig(string userId);


        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCSGOVideoConfiguration/{userId}", ResponseFormat = WebMessageFormat.Json)]
        CSGOVideoConfigurationDTO GetCSGOVideoConfiguration(string userId);


        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveCSGOVideoConfiguration",
    RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string SaveCSGOVideoConfiguration(CSGOVideoConfigurationDTO csgoVideoConfigurationDto);

        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "DeleteCSGOVideoConfiguration/{userId}", ResponseFormat = WebMessageFormat.Json)]
        string DeleteCSGOVideoConfiguration(string userId);



        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveCSGOSensitivity",
    RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string SaveCSGOSensitivity(CSGOSensitivityDTO csgoSensitivityDto);


        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetActiveCSGOSensitivity/{userId}",
   RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        CSGOSensitivityDTO GetActiveCSGOSensitivity(string userId);
    }
}
