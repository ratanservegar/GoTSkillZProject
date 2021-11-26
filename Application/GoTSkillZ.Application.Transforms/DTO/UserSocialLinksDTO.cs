using System;
using System.Runtime.Serialization;

namespace GoTSkillZ.Application.Transforms.DTO
{
    [Serializable]
    [DataContract]
    public class UserSocialLinksDTO
    {
        public UserSocialLinksDTO()
        {
            UserId = 0;
            Facebook = Instagram = Twitter = Youtube = Faceit = Steam = Twitch = Mixer = SoStronk = Discord = "";
        }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public string Facebook { get; set; }

        [DataMember]
        public string Instagram { get; set; }

        [DataMember]
        public string Twitter { get; set; }

        [DataMember]
        public string Youtube { get; set; }

        [DataMember]
        public string Faceit { get; set; }

        [DataMember]
        public string Steam { get; set; }

        [DataMember]
        public string Twitch { get; set; }

        [DataMember]
        public string Mixer { get; set; }

        [DataMember]
        public string SoStronk { get; set; }


        [DataMember]
        public string Discord { get; set; }
    }
}