using System;
using System.Runtime.Serialization;

namespace GoTSkillZ.Application.Transforms.DTO
{
    [Serializable]
    [DataContract]
    public class YouTubeVideosDTO
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string PlaylistId { get; set; }

        [DataMember]
        public string VideoId { get; set; }

        [DataMember]
        public string VideoTitle { get; set; }

        [DataMember]
        public string VideoDescription { get; set; }

        [DataMember]
        public string DefaultThumbnail { get; set; }
        [DataMember]
        public string MediumThumbnail { get; set; }
        [DataMember]
        public string HighThumbnail { get; set; }
        [DataMember]
        public string MaxThumbnail { get; set; }
        [DataMember]
        public string Standardthumbnail { get; set; }

        [DataMember]
        public string ChannelId { get; set; }

        [DataMember]
        public bool IsDisplayed { get; set; }

        [DataMember]
        public string VideoCreatedDate { get; set; }

        [DataMember]
        public string CreatedDate { get; set; }

        public YouTubeVideosDTO()
        {
            Id = 0;
            VideoId = "";
            VideoTitle = "";
            VideoDescription = "";
            ChannelId = "";
            DefaultThumbnail = "";
            MediumThumbnail = "";
            HighThumbnail = "";
            MaxThumbnail = "";
            Standardthumbnail = "";
            IsDisplayed = true;
            VideoCreatedDate = "";
            CreatedDate = "";
        }
    }
}
