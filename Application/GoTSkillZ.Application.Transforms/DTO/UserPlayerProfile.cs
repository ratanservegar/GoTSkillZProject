using System;
using System.Runtime.Serialization;

namespace GoTSkillZ.Application.Transforms.DTO
{
    [Serializable]
    [DataContract]
    public class UserPlayerProfile
    {
        [DataMember]
        public string Alias { get; set; }

        [DataMember]
        public string PrimaryGame { get; set; }

        [DataMember]
        public string SecondaryGame { get; set; }

        [DataMember]
        public string Role { get; set; }

        public int TotalExperience { get; set; }

    }
}
