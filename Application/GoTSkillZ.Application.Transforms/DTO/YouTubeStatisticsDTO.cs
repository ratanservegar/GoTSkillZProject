using GoTSkillZ.Models.Membership.Data;
using System;
using System.Runtime.Serialization;

namespace GoTSkillZ.Application.Transforms.DTO
{
    [Serializable]
    [DataContract]
    public class YouTubeStatisticsDTO
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int? SubCount { get; set; }

        [DataMember]
        public int? ViewCount { get; set; }


        [DataMember]
        public int? CommentCount { get; set; }

        [DataMember]
        public bool HiddenSubCount { get; set; }

        [DataMember]
        public int? VideoCount { get; set; }

        public YouTubeStatisticsDTO()
        {
            Id = 0;
            SubCount = 0;
            ViewCount = 0;
            CommentCount = 0;
            HiddenSubCount = false;
            VideoCount = 0;
        }

        public YouTubeStatisticsDTO(YouTubeChannelStatistic youTubeChannelStatistic)
        {
            Id = youTubeChannelStatistic.Id;
            SubCount = youTubeChannelStatistic.SubCount;
            ViewCount = youTubeChannelStatistic.ViewCount;
            CommentCount = youTubeChannelStatistic.CommentCount;
            HiddenSubCount = (bool)youTubeChannelStatistic.HiddenSubCount;
            VideoCount = youTubeChannelStatistic.VideoCount;
        }
    }
}
