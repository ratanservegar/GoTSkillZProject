using System;
using System.Runtime.Serialization;

namespace GoTSkillZ.Application.Transforms.DTO
{
    [Serializable]
    [DataContract]
    public class UserAchievementsDTO
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Position { get; set; }

        [DataMember]
        public string Type { get; set; }


        [DataMember]
        public string Location { get; set; }

        [DataMember]
        public string Date { get; set; }

        [DataMember]
        public string IsActive { get; set; }




        public UserAchievementsDTO()
        {
            Id = UserId = 0;
            Name = Description = Position = Type = Location = Date = IsActive = "";
        }
    }
}
