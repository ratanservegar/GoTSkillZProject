using GoTSkillZ.Application.Transforms.DTO;
using GoTSkillZ.Models.Membership.Data;
using GoTSkillZ.Models.UserSetup.Data;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace GoTSkillZ.Web.UI.Site.WCF
{

    [ServiceContract]
    public interface IMembershipAPI
    {
        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetAllActiveRoles", ResponseFormat = WebMessageFormat.Json)]
        List<RoleDTO> GetAllActiveRoles();



        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetUserProfileMetaData/{uid}", ResponseFormat = WebMessageFormat.Json)]
        ResponseDTO GetUserProfileMetaData(string uid);

        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCountry/{searchKey}", ResponseFormat = WebMessageFormat.Json)]
        List<CountryDTO> GetCountry(string searchKey);

        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetState/{searchKey}/{countryId}", ResponseFormat = WebMessageFormat.Json)]
        List<StateDTO> GetState(string searchKey, string countryId);

        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCity/{searchKey}/{countryId}/{stateId}", ResponseFormat = WebMessageFormat.Json)]
        List<CityDTO> GetCity(string searchKey, string countryId, string stateId);


        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveUserProfileData",
       RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string SaveUserProfileData(UserProfileDTO userProfileDTO);

        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetUserOccupations",
RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        List<UserOccupation> GetUserOccupations();


        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetUserSetups",
RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        List<UserSetupDTO> GetUserSetups();


        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetSetupTypes",
RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        List<SetupType> GetSetupTypes();

        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetSetupOptions",
RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        List<SetupOption> GetSetupOptions();


        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetUserProfileHeaderData", ResponseFormat = WebMessageFormat.Json)]
        ResponseDTO GetUserProfileHeaderData();



        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "AddNewUserSetup",
RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string AddNewUserSetup(List<UserSetupDataDTO> newUserSetup);


        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "UpdateUserSetup",
RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string UpdateUserSetup(List<UserSetupDataDTO> newUserSetup);


        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "RemoveSetup/{setupId}",
RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string RemoveSetup(string setupId);

    }
}
