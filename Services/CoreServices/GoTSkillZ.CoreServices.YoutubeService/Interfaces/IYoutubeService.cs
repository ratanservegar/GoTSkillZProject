using GoTSkillZ.Application.Transforms.DTO;
using System;
using System.Collections.Generic;

namespace GoTSkillZ.CoreServices.YoutubeService.Interfaces
{
    public interface IYoutubeService
    {
        ResponseDTO AddUserSubscription(int userId);
        ResponseDTO UnSubscribeUser(int userId);


        YouTubeLiveStreamDTO UpdateYouTubeLiveStreamValue(YouTubeLiveStreamDTO youTubeLiveStreamDto);

        bool CheckIfYouTubeIsLive();

        YouTubeLiveStreamDTO GetYouTubeliveStreamData();


        String AddYouTubePlayListData(List<YouTubePlayListDTO> youTubePlayListDtos);


        String AddYouTubeVideoData(List<YouTubeVideosDTO> youTubeVideosDtos);

        List<YouTubePlayListDTO> GetYouTubePlaylist();


        List<YouTubeVideosDTO> GetYouTubeVideos();


        List<YouTubeSuperChatDTO> GetYouTubeSuperChatList();


        String AddYouTubeSuperChatData(List<YouTubeSuperChatDTO> youTubeSuperChatDto);
    }
}