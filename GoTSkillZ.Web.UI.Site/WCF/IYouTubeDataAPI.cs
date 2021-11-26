using GoTSkillZ.Application.Transforms.DTO;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace GoTSkillZ.Web.UI.Site.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IYouTubeDataAPI" in both code and config file together.
    [ServiceContract]
    public interface IYouTubeDataAPI
    {
        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetYouTubePlaylists", ResponseFormat = WebMessageFormat.Json)]
        List<YouTubePlayListDTO> GetYouTubePlaylists();

        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetYouTubeVideos", ResponseFormat = WebMessageFormat.Json)]
        List<YouTubeVideosDTO> GetYouTubeVideos();


        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetYouTubeSuperChats", ResponseFormat = WebMessageFormat.Json)]
        List<YouTubeSuperChatDTO> GetYouTubeSuperChats();
    }
}
