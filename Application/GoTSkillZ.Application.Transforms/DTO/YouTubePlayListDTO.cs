using System;
using System.Runtime.Serialization;

namespace GoTSkillZ.Application.Transforms.DTO
{
    [Serializable]
    [DataContract]
    public class YouTubePlayListDTO
    {

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string PlaylistId { get; set; }
        [DataMember]
        public string PlaylistTitle { get; set; }
        [DataMember]
        public string PlayListDescription { get; set; }
        [DataMember]
        public string ChannelId { get; set; }
        [DataMember]
        public string DefaultThumbnail { get; set; }
        [DataMember]
        public string MediumThumbnail { get; set; }
        [DataMember]
        public string HighThumbnail { get; set; }
        [DataMember]
        public string MaxThumbnail { get; set; }
        [DataMember]
        public string CustomThumbail { get; set; }
        [DataMember]
        public int PlaylistItemCount { get; set; }
        [DataMember]
        public bool PlaylistActive { get; set; }
        [DataMember]
        public string PlaylistCreatedDate { get; set; }
        [DataMember]
        public string CreatedDate { get; set; }


        public YouTubePlayListDTO()
        {
            Id = 0;
            PlaylistId = "";
            PlaylistTitle = "";
            PlayListDescription = "";
            ChannelId = "";
            DefaultThumbnail = "";
            MediumThumbnail = "";
            HighThumbnail = "";
            MaxThumbnail = "";
            CustomThumbail = "";
            PlaylistItemCount = 0;
            PlaylistActive = true;
            CreatedDate = "";
            PlaylistCreatedDate = "";
        }

    }
}