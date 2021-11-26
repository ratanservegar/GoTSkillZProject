using GoTSkillZ.Models.YouTube.Data;
using System;
using System.Runtime.Serialization;

namespace GoTSkillZ.Application.Transforms.DTO
{
    [Serializable]
    [DataContract]
    public class YouTubeLiveStreamDTO
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public string YouTubeChannelId { get; set; }


        [DataMember]
        public string VideoId { get; set; }

        [DataMember]
        public string EmbedHTML { get; set; }

        [DataMember]
        public string StreamTitle { get; set; }

        [DataMember]
        public string LiveChatId { get; set; }

        [DataMember]
        public bool IsLive { get; set; }


        public YouTubeLiveStreamDTO()
        {
            Id = 0;
            UserId = 1;
            YouTubeChannelId = "";
            EmbedHTML = "";
            StreamTitle = "";
            LiveChatId = "";
            VideoId = "";
            IsLive = false;
        }



        public YouTubeLiveStreamDTO(YouTubeLiveStream youTubeLiveStreamObj)
        {
            Id = youTubeLiveStreamObj.Id;
            UserId = youTubeLiveStreamObj.UserId;
            YouTubeChannelId = youTubeLiveStreamObj.YouTubeChannalId;
            VideoId = youTubeLiveStreamObj.VideoId;
            IsLive = youTubeLiveStreamObj.IsLive;
            EmbedHTML = youTubeLiveStreamObj.EmbedHTML;
            StreamTitle = youTubeLiveStreamObj.StreamTitle;
            LiveChatId = youTubeLiveStreamObj.LiveChatId;
        }

    }
}