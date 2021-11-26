using GoTSkillZ.Models.YouTube.Data;
using System;
using System.Runtime.Serialization;

namespace GoTSkillZ.Application.Transforms.DTO
{
    [Serializable]
    [DataContract]
    public class YouTubeSubscriberListDTO
    {
        [DataMember]
        public int Id { get; set; }


        [DataMember]
        public string ChannelId { get; set; }

        [DataMember]
        public string YoutubeId { get; set; }


        [DataMember]
        public string Name { get; set; }


        public YouTubeSubscriberListDTO()
        {
            Id = 0;
            ChannelId = YoutubeId = Name = "";
        }

        public YouTubeSubscriberListDTO(YouTubeSubscriberList youTubeSubscriberList)
        {
            Id = youTubeSubscriberList.Id;
            ChannelId = youTubeSubscriberList.ChannelId;
            YoutubeId = youTubeSubscriberList.YoutubeId;
            Name = youTubeSubscriberList.Name;
        }
    }
}
