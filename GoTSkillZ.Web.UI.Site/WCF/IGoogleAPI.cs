using GoTSkillZ.Application.Transforms.DTO;
using GoTSkillZ.Application.Transforms.STO;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace GoTSkillZ.Web.UI.Site.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IGoogleAPI" in both code and config file together.
    [ServiceContract]
    public interface IGoogleAPI
    {
        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "AuthorizeUser",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        ResponseDTO AuthorizeUser(GoogleSTO authReq);



        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "DeAuthorize",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void DeAuthorize();



        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "AuthenticateUser",
         RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        ResponseDTO AuthenticateUser();


        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "CheckAndSub/{userId}",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool CheckAndSub(string userId);


        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SubscribeUser/{userId}",
         RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        ResponseDTO SubscribeUser(string userId);


        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "UnSubscribeUser/{userId}",
        RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        ResponseDTO UnSubscribeUser(string userId);
    }
}