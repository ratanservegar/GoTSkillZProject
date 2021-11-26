using GoTSkillZ.Application.Transforms.DTO;
using GoTSkillZ.Application.Transforms.Enums;
using GoTSkillZ.CoreServices.YoutubeService.Interfaces;
using GoTSkillZ.Models.Membership.Data;
using GoTSkillZ.Models.Membership.Interfaces;
using GoTSkillZ.Models.Membership.Providers;
using GoTSkillZ.Models.UserDataExtension.Data;
using GoTSkillZ.Models.UserDataExtension.Interfaces;
using GoTSkillZ.Models.UserDataExtension.Provider;
using GoTSkillZ.Models.YouTube.Data;
using GoTSkillZ.Models.YouTube.Interfaces;
using GoTSkillZ.Models.YouTube.Provider;
using GoTSkillZ.Security.Services.Interfaces;
using GoTSkillZ.Security.Services.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using GoTSkillZ.DataUtilities.Core.Interfaces;
using GoTSkillZ.DataUtilities.Core.Services;

namespace GoTSkillZ.CoreServices.YoutubeService.Services
{
    public class YoutubeService : IYoutubeService
    {
        private readonly IGoTSkillZSecurityService _gotSkillZSecurityService;
        private readonly IUserProvider _userProvider;
        private readonly IUserRoleProvider _userRoleProvider;
        private readonly IYouTubeLiveStreamProvider _youTubeLiveStreamProvider;
        private readonly IYouTubeSponsorsProvider _youTubeSponsorsProvider;
        private readonly IYouTubeSubscribersProvider _youTubeSubscribersProvider;
        private readonly IYouTubePlaylistsProvider _youTubePlaylistsProvider;
        private readonly IYouTubeVideosProvider _youTubeVideosProvider;
        private readonly IYouTubeSuperChatListProvider _youTubeSuperChatListProvider;
        private readonly IDbUtility _dbUtility;

        public YoutubeService()
        {
            _userProvider = new UserProvider();
            _userRoleProvider = new UserRoleProvider();
            _youTubeSponsorsProvider = new YouTubeSponsorsProvider();
            _youTubeSubscribersProvider = new YouTubeSubscribersProvider();
            _gotSkillZSecurityService = new GoTSkillZSecurityService();
            _youTubeLiveStreamProvider = new YouTubeLiveStreamProvider();
            _youTubePlaylistsProvider = new YouTubePlaylistsProvider();
           _youTubeVideosProvider = new YouTubeVideosProvider();
           _youTubeSuperChatListProvider= new YouTubeSuperChatListProvider();
           _dbUtility = new DbUtility();
        }

        public ResponseDTO AddUserSubscription(int loggedInUserId)
        {
            var responseDTO = new ResponseDTO();

            var responObj = _gotSkillZSecurityService.GetUserInternalId();

            if (responObj.Success == false) return responObj;

            var userId = responObj.UserId;

            if (userId != loggedInUserId)
            {
                responseDTO.StateCode = GoTSkillZEnum.InvalidUser;
                responseDTO.ResponseText = Enum.GetName(typeof(GoTSkillZEnum), GoTSkillZEnum.InvalidUser);
                responseDTO.Success = false;
                return responseDTO;
            }

            try
            {
                YouTubeSubscribers youTubeSubObj = null;
                UserRoles userRoleObj = null;
                var checkForExistingSub = _youTubeSubscribersProvider.FindBy(x => x.UserId == userId).FirstOrDefault();

                if (checkForExistingSub != null)
                {
                    if (checkForExistingSub.IsSubscribed == false)
                    {
                        checkForExistingSub.IsSubscribed = true;
                        youTubeSubObj = _youTubeSubscribersProvider.Update(checkForExistingSub);
                    }
                }
                else
                {
                    var newSubscriber = new YouTubeSubscribers
                    {
                        UserId = userId,
                        IsSubscribed = true,
                        SubscribedDate = DateTime.Now
                    };
                    youTubeSubObj = _youTubeSubscribersProvider.Add(newSubscriber);
                }

                var checkForExistingRole =
                    _userRoleProvider.FindBy(x => x.UserId == userId && x.RoleId == 5).FirstOrDefault();

                if (checkForExistingRole == null)
                {
                    var newUserRole = new UserRoles
                    {
                        UserId = userId,
                        RoleId = 5, //sub
                        IsActive = true,
                        IsDeleted = false
                    };

                    userRoleObj = _userRoleProvider.Add(newUserRole);
                }
                else
                {
                    userRoleObj = checkForExistingRole;
                }

                if (youTubeSubObj != null && userRoleObj != null)
                {
                    responseDTO.StateCode = GoTSkillZEnum.ValidUser;
                    responseDTO.ResponseText = Enum.GetName(typeof(GoTSkillZEnum), GoTSkillZEnum.ValidUser);
                    responseDTO.Success = true;
                }
            }
            catch (Exception)
            {
                responseDTO.StateCode = GoTSkillZEnum.InternalError;
                responseDTO.ResponseText = Enum.GetName(typeof(GoTSkillZEnum), GoTSkillZEnum.InternalError);
                responseDTO.Success = false;
            }


            return responseDTO;
        }

        public ResponseDTO UnSubscribeUser(int userId)
        {
            var responseDTO = new ResponseDTO();


            var existingSub = _youTubeSubscribersProvider.FindBy(x => x.UserId == userId).FirstOrDefault();

            if (existingSub != null)
            {
                if (existingSub.IsSubscribed)
                {
                    existingSub.IsSubscribed = false;
                    existingSub.UnSubscribedDate = DateTime.Now;
                    _youTubeSubscribersProvider.Update(existingSub);
                }
            }

            var existingSubRole = _userRoleProvider.FindBy(x => x.UserId == userId && x.RoleId == 5).FirstOrDefault();

            if (existingSubRole != null)
            {
                _userRoleProvider.Delete(userId, 5);
            }


            responseDTO.StateCode = GoTSkillZEnum.NotSubscriber;
            responseDTO.ResponseText = Enum.GetName(typeof(GoTSkillZEnum), GoTSkillZEnum.NotSubscriber);
            responseDTO.Success = false;

            return responseDTO;
        }

        public YouTubeLiveStreamDTO UpdateYouTubeLiveStreamValue(YouTubeLiveStreamDTO youTubeLiveStreamDto)
        {
            if (youTubeLiveStreamDto != null)
            {
                var existingItem =
                    _youTubeLiveStreamProvider.FindBy(x => x.UserId == youTubeLiveStreamDto.UserId).FirstOrDefault();
                if (existingItem != null)
                {
                    existingItem.EmbedHTML = youTubeLiveStreamDto.EmbedHTML;
                    existingItem.LiveChatId = youTubeLiveStreamDto.LiveChatId;
                    existingItem.StreamTitle = youTubeLiveStreamDto.StreamTitle;
                    existingItem.VideoId = youTubeLiveStreamDto.VideoId;
                    existingItem.IsLive = youTubeLiveStreamDto.IsLive;

                    _youTubeLiveStreamProvider.Update(existingItem);
                }
            }

            return youTubeLiveStreamDto;
        }

        public bool CheckIfYouTubeIsLive()
        {

            var dataTable = _dbUtility.RunSqlQuery("SELECT IsLive FROM YouTubeLiveStream WITH(NOLOCK)");
            return (bool) dataTable.Rows[0]["IsLive"];

        }

        public YouTubeLiveStreamDTO GetYouTubeliveStreamData()
        {
            return _youTubeLiveStreamProvider.FindBy(x => x.Id == 1).Select(x => new YouTubeLiveStreamDTO
            {
                EmbedHTML = x.EmbedHTML,
                IsLive = x.IsLive,
                LiveChatId = x.LiveChatId,
                StreamTitle = x.StreamTitle,
                VideoId = x.VideoId,
                YouTubeChannelId = x.YouTubeChannalId,
                Id = x.Id,
                UserId = x.UserId
            }).FirstOrDefault();
        }

        public string AddYouTubePlayListData(List<YouTubePlayListDTO> youTubePlayListDtos)
        {
            var returnString = "Success";
            if (youTubePlayListDtos.Count == 0) return returnString = "Failed";

            foreach (var playListItem in youTubePlayListDtos)
            {
                var checkForExistingPlayList =
                    _youTubePlaylistsProvider.FindBy(x => x.PlaylistId == playListItem.PlaylistId).FirstOrDefault();

                if (checkForExistingPlayList == null)
                {
                    var newPlayListItem = new YouTubePlaylist
                    {
                        ChannelId = playListItem.ChannelId,
                        PlaylistId = playListItem.PlaylistId,
                        PlaylistTitle = playListItem.PlaylistTitle,
                        PlayListDescription = playListItem.PlayListDescription,
                        CustomThumbail = playListItem.CustomThumbail,
                        DefaultThumbnail = playListItem.DefaultThumbnail,
                        HighThumbnail = playListItem.HighThumbnail,
                        MaxThumbnail = playListItem.MaxThumbnail,
                        MediumThumbnail = playListItem.MediumThumbnail,
                        PlaylistActive = playListItem.PlaylistActive,
                        PlaylistItemCount = playListItem.PlaylistItemCount,
                        PlaylistCreatedDate = Convert.ToDateTime(playListItem.PlaylistCreatedDate),
                        CreatedDate = DateTime.Now
                    };
                    _youTubePlaylistsProvider.Add(newPlayListItem);
                }
                else
                {
                    checkForExistingPlayList.PlaylistTitle = playListItem.PlaylistTitle;
                    checkForExistingPlayList.PlayListDescription = playListItem.PlayListDescription;
                    checkForExistingPlayList.DefaultThumbnail = playListItem.DefaultThumbnail;
                    checkForExistingPlayList.HighThumbnail = playListItem.HighThumbnail;
                    checkForExistingPlayList.MaxThumbnail = playListItem.MaxThumbnail;
                    checkForExistingPlayList.MediumThumbnail = playListItem.MediumThumbnail;
                    playListItem.PlaylistItemCount = playListItem.PlaylistItemCount;
                    _youTubePlaylistsProvider.Update(checkForExistingPlayList);
                }
            }


            return returnString;
        }

        public string AddYouTubeVideoData(List<YouTubeVideosDTO> youTubeVideosDtos)
        {
            var returnString = "Success";
            if (youTubeVideosDtos.Count == 0) return returnString = "Failed";

            foreach (var videoItem in youTubeVideosDtos)
            {
                var checkForExistingItem = _youTubeVideosProvider.FindBy(x => x.VideoId == videoItem.VideoId && x.PlaylistId == videoItem.PlaylistId).FirstOrDefault();

                if (checkForExistingItem == null)
                {
                    var newVideoItem = new YouTubeVideo
                    {
                        ChannelId = videoItem.ChannelId,
                        PlaylistId = videoItem.PlaylistId,
                        VideoId = videoItem.VideoId,
                        VideoTitle = videoItem.VideoTitle,
                        VideoDescription = videoItem.VideoDescription,
                        Standardthumbnail = videoItem.Standardthumbnail,
                        DefaultThumbnail = videoItem.DefaultThumbnail,
                        HighThumbnail = videoItem.HighThumbnail,
                        MaxThumbnail = videoItem.MaxThumbnail,
                        MediumThumbnail = videoItem.MediumThumbnail,
                        IsDisplayed = videoItem.IsDisplayed,
                        VideoCreatedDate = Convert.ToDateTime(videoItem.VideoCreatedDate),
                        CreatedDate = DateTime.Now
                    };
                    _youTubeVideosProvider.Add(newVideoItem);
                }
                else
                {
                    checkForExistingItem.PlaylistId = videoItem.PlaylistId;
                    checkForExistingItem.VideoTitle = videoItem.VideoTitle;
                    checkForExistingItem.VideoDescription = videoItem.VideoDescription;
                    checkForExistingItem.Standardthumbnail = videoItem.Standardthumbnail;
                    checkForExistingItem.DefaultThumbnail = videoItem.DefaultThumbnail;
                    checkForExistingItem.HighThumbnail = videoItem.HighThumbnail;
                    checkForExistingItem.MaxThumbnail = videoItem.MaxThumbnail;
                    checkForExistingItem.MediumThumbnail = videoItem.MediumThumbnail;
                    _youTubeVideosProvider.Update(checkForExistingItem);
                }
            }


            return returnString;
        }

        public List<YouTubePlayListDTO> GetYouTubePlaylist()
        {
            return _youTubePlaylistsProvider.FindBy(x => x.PlaylistActive).Select(x => new YouTubePlayListDTO
            {
                Id = x.Id,
                ChannelId = x.ChannelId,
                PlaylistId = x.PlaylistId,
                PlaylistTitle = x.PlaylistTitle,
                PlayListDescription = x.PlayListDescription,
                CustomThumbail = x.CustomThumbail,
                DefaultThumbnail = x.DefaultThumbnail,
                HighThumbnail = x.HighThumbnail,
                MaxThumbnail = x.MaxThumbnail,
                MediumThumbnail = x.MediumThumbnail,
                PlaylistActive = x.PlaylistActive,
                CreatedDate = x.CreatedDate.ToString(),
                PlaylistCreatedDate = x.PlaylistCreatedDate.ToString(),
                PlaylistItemCount = x.PlaylistItemCount
            }).OrderBy(x => Convert.ToDateTime(x.PlaylistCreatedDate)).ToList();
        }

        public List<YouTubeVideosDTO> GetYouTubeVideos()
        {
            return _youTubeVideosProvider.FindBy(x => x.IsDisplayed).Select(x => new YouTubeVideosDTO
            {
                Id = x.Id,
                ChannelId = x.ChannelId,
                CreatedDate = x.CreatedDate.ToString(),
                PlaylistId = x.PlaylistId,
                VideoId = x.VideoId,
                MaxThumbnail = x.MaxThumbnail,
                MediumThumbnail = x.MediumThumbnail,
                DefaultThumbnail = x.DefaultThumbnail,
                HighThumbnail = x.HighThumbnail,
                IsDisplayed = x.IsDisplayed,
                Standardthumbnail = x.Standardthumbnail,
                VideoDescription = x.VideoDescription,
                VideoCreatedDate = x.VideoCreatedDate.ToString(),
                VideoTitle = x.VideoTitle
            }).OrderBy(x => Convert.ToDateTime(x.VideoCreatedDate)).ToList();
        }

        public List<YouTubeSuperChatDTO> GetYouTubeSuperChatList()
        {
            return _youTubeSuperChatListProvider.FindBy(x => x.ShowSuperChat == true).Select(x => new YouTubeSuperChatDTO
            {
                Id = x.Id,
                YouTubeSuperChatId = x.YouTubeSuperChatId,
                Channeld = x.Channeld,
                ChannelUrl = x.ChannelUrl,
                DisplayName = x.DisplayName,
                CommentText = x.CommentText,
                Currency = x.Currency,
                DisplayString = x.DisplayString,
                AmountMicros = x.AmountMicros,
                ProfileImageUrl = x.ProfileImageUrl,
                MessageType = x.MessageType,
                IsSuperStickerEvent = x.IsSuperStickerEvent,
                StickerId = x.StickerId,
                altText = x.altText,
                ShowSuperChat = x.ShowSuperChat,
                CreatedAt = x.CreatedAt,
                CreatedDate = x.CreatedDate.ToString()
            }).OrderBy(x => Convert.ToDateTime(x.CreatedDate)).ToList();
        }

        public string AddYouTubeSuperChatData(List<YouTubeSuperChatDTO> youTubeSuperChatDto)
        {
            var returnString = "Success";
            if (youTubeSuperChatDto.Count == 0) return returnString = "Failed";

            foreach (var superChatItem in youTubeSuperChatDto)
            {
                var checkForExistingSuperChat= _youTubeSuperChatListProvider.FindBy(x => x.YouTubeSuperChatId == superChatItem.YouTubeSuperChatId).FirstOrDefault();

                if (checkForExistingSuperChat == null)
                {
                    var newSuperChatItem = new YouTubeSuperChatList
                    {
                        
                        YouTubeSuperChatId = superChatItem.YouTubeSuperChatId,
                        Channeld = superChatItem.Channeld,
                        ChannelUrl = superChatItem.ChannelUrl,
                        DisplayString = superChatItem.DisplayString,
                        CommentText = superChatItem.CommentText,
                        Currency = superChatItem.Currency,
                        DisplayName = superChatItem.DisplayName,
                        AmountMicros = superChatItem.AmountMicros,
                        ProfileImageUrl = superChatItem.ProfileImageUrl,
                        MessageType = superChatItem.MessageType,
                        IsSuperStickerEvent = superChatItem.IsSuperStickerEvent,
                        StickerId = superChatItem.StickerId,
                        altText = superChatItem.altText,
                        CreatedAt = superChatItem.CreatedAt,
                        ShowSuperChat = true,
                        CreatedDate = DateTime.Now
                    };
                    _youTubeSuperChatListProvider.Add(newSuperChatItem);
                }
                else
                {

                    checkForExistingSuperChat.Channeld = superChatItem.Channeld;
                    checkForExistingSuperChat.ChannelUrl = superChatItem.ChannelUrl;
                    checkForExistingSuperChat.DisplayString = superChatItem.DisplayString;
                    checkForExistingSuperChat.CommentText = superChatItem.CommentText;
                    checkForExistingSuperChat.Currency = superChatItem.Currency;
                    checkForExistingSuperChat.DisplayName = superChatItem.DisplayName;
                    checkForExistingSuperChat.AmountMicros = superChatItem.AmountMicros;
                    checkForExistingSuperChat.ProfileImageUrl = superChatItem.ProfileImageUrl;
                    checkForExistingSuperChat.MessageType = superChatItem.MessageType;
                    checkForExistingSuperChat.IsSuperStickerEvent = superChatItem.IsSuperStickerEvent;
                    checkForExistingSuperChat.StickerId = superChatItem.StickerId;
                    checkForExistingSuperChat.altText = superChatItem.altText;
                    checkForExistingSuperChat.CreatedAt = superChatItem.CreatedAt;
                    checkForExistingSuperChat.ShowSuperChat = superChatItem.ShowSuperChat;
                    checkForExistingSuperChat.CreatedDate = DateTime.Now;
                    _youTubeSuperChatListProvider.Update(checkForExistingSuperChat);
                }
            }


            return returnString;
        }
    }
}