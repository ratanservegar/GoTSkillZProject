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
    public class GiveawayWinnerTopListDTO
    {
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public int GiveawayId { get; set; }
        [DataMember]
        public string EntryCode { get; set; }
        [DataMember]
        public string EntryDate { get; set; }
        [DataMember]
        public int Bucket { get; set; }

    }
}
