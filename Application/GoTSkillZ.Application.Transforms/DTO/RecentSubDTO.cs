using System;
using System.Runtime.Serialization;

namespace GoTSkillZ.Application.Transforms.DTO
{
    [Serializable]
    [DataContract]
    public class RecentSubDTO
    {
        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public string SubName { get; set; }

        [DataMember]
        public string SubDate { get; set; }

        [DataMember]
        public string SubImage { get; set; }


        public RecentSubDTO()
        {
            UserId = 0;
            SubName = SubImage = SubDate = "";
        }
    }
}
