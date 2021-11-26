using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GoTSkillZ.Application.Transforms.DTO
{
    [Serializable]
    [DataContract]
    public class YouTubeSuperChatDTO
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string YouTubeSuperChatId { get; set; }

        [DataMember]
        public string Channeld { get; set; }

        [DataMember]
        public string ChannelUrl { get; set; }

        [DataMember]
        public string DisplayName { get; set; }

        [DataMember]
        public string CommentText { get; set; }

        [DataMember]
        public string Currency { get; set; }

        [DataMember]
        public string DisplayString { get; set; }
        [DataMember]
        public string AmountMicros { get; set; }
        [DataMember]
        public string ProfileImageUrl { get; set; }
        [DataMember]
        public int? MessageType { get; set; }
        [DataMember]
        public bool? IsSuperStickerEvent { get; set; }
        [DataMember]
        public string StickerId { get; set; }

        [DataMember]
        public string altText { get; set; }

        [DataMember]
        public bool? ShowSuperChat { get; set; }

        [DataMember]
        public string CreatedAt { get; set; }
        [DataMember]
        public string CreatedDate { get; set; }



    }
}
