using GoTSkillZ.Application.Transforms.DTO;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace GoTSkillZ.Web.UI.Site.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IPMSAPI" in both code and config file together.
    [ServiceContract]
    public interface IPMSAPI
    {
        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetAllSitemaps", ResponseFormat = WebMessageFormat.Json)]
        List<SitemapDTO> GetAllSitemaps();

        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetSitemapTree", ResponseFormat = WebMessageFormat.Json)]
        List<KendoHierarchicalDTO> GetSitemapTree();

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetSitemap", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        SitemapDTO GetSitemap(SitemapDTO sitemapDto);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveSitemap", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        SitemapDTO SaveSitemap(SitemapDTO sitemapDto);


        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetAllPages", ResponseFormat = WebMessageFormat.Json)]
        List<PagesDTO> GetAllPages();


        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SavePage", ResponseFormat = WebMessageFormat.Json)]
        PagesDTO SavePage(PagesDTO pageDto);

        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetNavigationForUser", ResponseFormat = WebMessageFormat.Json)]
        List<KendoHierarchicalDTO> GetNavigationForUser();

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetPageRoles", ResponseFormat = WebMessageFormat.Json)]
        List<PageRoleDTO> GetPageRoles(PageRoleDTO pageRoleDto);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SavePageRoles", ResponseFormat = WebMessageFormat.Json)]
        List<PageRoleDTO> SavePageRoles(List<PageRoleDTO> pageRoles);


        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "CheckUserPageAccess/{pageId}", ResponseFormat = WebMessageFormat.Json)]
        ResponseDTO CheckUserPageAccess(string pageId);


     
    }
}