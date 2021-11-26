using GoTSkillZ.Application.Transforms.DTO;
using GoTSkillZ.CoreServices.SiteCoreDataService.Interfaces;
using GoTSkillZ.CoreServices.SiteCoreDataService.Services;
using GoTSkillZ.CoreServices.YoutubeService.Interfaces;
using GoTSkillZ.CoreServices.YoutubeService.Services;
using System.Collections.Generic;

namespace GoTSkillZ.Web.UI.Site.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SiteCoreDataAPI" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SiteCoreDataAPI.svc or SiteCoreDataAPI.svc.cs at the Solution Explorer and start debugging.
    public class SiteCoreDataAPI : ISiteCoreDataAPI
    {
        private readonly ISiteCoreDataService _siteCoreDataService;
        private readonly IYoutubeService _youtubeService;
        public SiteCoreDataAPI()
        {
            _siteCoreDataService = new SiteCoreDataService();
            _youtubeService = new YoutubeService();
        }


        public int GetCurrentSponsorCount()
        {
            return _siteCoreDataService.GetCurrentSponsorCount();
        }

      

        public List<RecentSubDTO> GetRecentSponsors()
        {
            return _siteCoreDataService.GetRecentSponsors();
        }


        public string AddYouTubeSubList(List<YouTubeSubscriberListDTO> youTubeSubscriberList)
        {
            return _siteCoreDataService.AddYouTubeSubList(youTubeSubscriberList);
        }

        public string UpdateYouTubeStatistics(YouTubeStatisticsDTO youTubeStatisticsDto)
        {
            return _siteCoreDataService.UpdateYouTubeStatistics(youTubeStatisticsDto);
        }

        public YouTubeLiveStreamDTO SetYouTubeIsLive(YouTubeLiveStreamDTO youTubeLiveStreamDto)
        {
            return _youtubeService.UpdateYouTubeLiveStreamValue(youTubeLiveStreamDto);
        }

        public bool GetYouTubeIsLive()
        {
            return _youtubeService.CheckIfYouTubeIsLive();
        }

        public YouTubeLiveStreamDTO GetYouTubeLiveStreamData()
        {
            return _youtubeService.GetYouTubeliveStreamData();
        }

        public string AddYouTubePlayListData(List<YouTubePlayListDTO> youTubePlayList)
        {
            return _youtubeService.AddYouTubePlayListData(youTubePlayList);
        }

        public string AddYouTubeVideoData(List<YouTubeVideosDTO> youTubeVideos)
        {
            return _youtubeService.AddYouTubeVideoData(youTubeVideos);
        }


        public string AddYouTubeSuperChatData(List<YouTubeSuperChatDTO> youTubeSuperChatDto)
        {
            return _youtubeService.AddYouTubeSuperChatData(youTubeSuperChatDto);
        }
    }
}
