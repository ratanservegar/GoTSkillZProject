using GoTSkillZ.Models.Membership.Data;
using System;
using System.Runtime.Serialization;

namespace GoTSkillZ.Application.Transforms.DTO
{
    [Serializable]
    [DataContract]
    public class RoleDTO
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string RoleName { get; set; }

        [DataMember]
        public string RoleDescription { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

        [DataMember]
        public bool IsDeleted { get; set; }

        public RoleDTO()
        {
            Id = 0;
            IsActive = IsDeleted = false;
            RoleName = RoleDescription = "";
        }

        public RoleDTO(Roles role)
        {
            Id = role?.Id ?? 0;
            RoleName = (role == null) ? "" : role.RoleName;
            RoleDescription = (role == null) ? "" : role.RoleDescription;

            IsActive = (role != null) && role.IsActive;
            IsDeleted = (role != null) && role.IsDeleted;
        }


    }
}
