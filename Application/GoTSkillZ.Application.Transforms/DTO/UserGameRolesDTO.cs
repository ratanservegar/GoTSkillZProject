using System;
using System.Runtime.Serialization;

namespace GoTSkillZ.Application.Transforms.DTO
{
    [Serializable]
    [DataContract]
    public class UserGameRolesDTO
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public int RoleId { get; set; }

        [DataMember]
        public bool PrimaryRole { get; set; }

        public UserGameRolesDTO()
        {
            Id = 0;
            UserId = 0;
            RoleId = 0;
            PrimaryRole = false;
        }
    }
}
