using System;
using System.Runtime.Serialization;

namespace GoTSkillZ.Application.Transforms.DTO
{
    [Serializable]
    [DataContract]
    public class PageRoleDTO
    {
        public PageRoleDTO()
        {
            Id = 0;
            PageId = 0;
            RoleId = 0;
        }

        public PageRoleDTO(PageRoleDTO pageRole)
        {
            Id = pageRole?.Id ?? 0;
            PageId = pageRole?.PageId ?? 0;
            RoleId = pageRole?.RoleId ?? 0;
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int PageId { get; set; }

        [DataMember]
        public int RoleId { get; set; }
    }
}