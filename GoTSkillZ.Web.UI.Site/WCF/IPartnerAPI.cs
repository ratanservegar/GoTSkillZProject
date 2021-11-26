using GoTSkillZ.Models.Partner.Data;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace GoTSkillZ.Web.UI.Site.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IPartnerAPI" in both code and config file together.
    [ServiceContract]
    public interface IPartnerAPI
    {
        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCompanies",
RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        List<Companies> GetCompanies();
    }
}
