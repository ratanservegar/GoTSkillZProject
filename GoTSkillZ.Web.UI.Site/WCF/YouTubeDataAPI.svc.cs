using GoTSkillZ.Application.Transforms.DTO;
using GoTSkillZ.CoreServices.YoutubeService.Interfaces;
using GoTSkillZ.CoreServices.YoutubeService.Services;
using System.Collections.Generic;

namespace GoTSkillZ.Web.UI.Site.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "YouTubeDataAPI" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select YouTubeDataAPI.svc or YouTubeDataAPI.svc.cs at the Solution Explorer and start debugging.
    public class YouTubeDataAPI : IYouTubeDataAPI
    {
        private readonly IYoutubeService _youtubeService;

        public YouTubeDataAPI()
        {
            _youtubeService = new YoutubeService();
        }

        public List<YouTubePlayListDTO> GetYouTubePlaylists()
        {
            return _youtubeService.GetYouTubePlaylist();
        }

        public List<YouTubeVideosDTO> GetYouTubeVideos()
        {
            return _youtubeService.GetYouTubeVideos();
        }

        public List<YouTubeSuperChatDTO> GetYouTubeSuperChats()
        {
            return _youtubeService.GetYouTubeSuperChatList();
        }
    }
}
