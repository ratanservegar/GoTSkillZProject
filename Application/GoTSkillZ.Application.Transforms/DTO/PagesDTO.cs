using System;
using System.Runtime.Serialization;

namespace GoTSkillZ.Application.Transforms.DTO
{
    [Serializable]
    [DataContract]
    public class PagesDTO
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string PageName { get; set; }

        [DataMember]
        public string PageRoles { get; set; }

        [DataMember]
        public int PageType { get; set; }
        [DataMember]
        public string BaseUrl { get; set; }

        [DataMember]
        public bool ShowContent { get; set; }


        [DataMember]
        public bool IsActive { get; set; }


        public PagesDTO()
        {
            Id = PageType = 0;
            PageName = BaseUrl = PageRoles = "";
            IsActive = false;
            ShowContent = false;


        }

        public PagesDTO(PagesDTO pageDto)
        {
            Id = (pageDto == null) ? 0 : pageDto.Id;
            PageName = (pageDto == null) ? "" : pageDto.PageName;
            ShowContent = (pageDto != null) && pageDto.ShowContent;
            BaseUrl = (pageDto == null) ? "" : pageDto.BaseUrl;
            PageRoles = (pageDto == null) ? "" : pageDto.PageRoles;
            PageType = (pageDto == null) ? 0 : pageDto.PageType;
            IsActive = (pageDto != null) && pageDto.IsActive;

        }
    }
}
