using GoTSkillZ.Application.Transforms.DTO;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace GoTSkillZ.Web.UI.Site.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISiteCoreDataAPI" in both code and config file together.
    [ServiceContract]
    public interface ISiteCoreDataAPI
    {
       

        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCurrentSponsorCount", ResponseFormat = WebMessageFormat.Json)]
        int GetCurrentSponsorCount();

     

        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetRecentSponsors", ResponseFormat = WebMessageFormat.Json)]
        List<RecentSubDTO> GetRecentSponsors();


        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "AddYouTubeSubList",
        RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string AddYouTubeSubList(List<YouTubeSubscriberListDTO> youTubeSubscriberList);


        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "UpdateYouTubeStatistics",
    RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string UpdateYouTubeStatistics(YouTubeStatisticsDTO youTubeStatisticsDto);




        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SetYouTubeIsLive", ResponseFormat = WebMessageFormat.Json)]
        YouTubeLiveStreamDTO SetYouTubeIsLive(YouTubeLiveStreamDTO youTubeLiveStreamDto);




        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetYouTubeIsLive", ResponseFormat = WebMessageFormat.Json)]
        bool GetYouTubeIsLive();


        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetYouTubeLiveStreamData", ResponseFormat = WebMessageFormat.Json)]
        YouTubeLiveStreamDTO GetYouTubeLiveStreamData();



        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "AddYouTubePlayListData",
       RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string AddYouTubePlayListData(List<YouTubePlayListDTO> youTubePlayList);


        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "AddYouTubeVideoData",
     RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string AddYouTubeVideoData(List<YouTubeVideosDTO> youTubeVideos);


        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "AddYouTubeSuperChatData",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string AddYouTubeSuperChatData(List<YouTubeSuperChatDTO> youTubeSuperChatDto);

    }
}
