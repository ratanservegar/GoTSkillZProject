using System;
using System.Runtime.Serialization;

namespace GoTSkillZ.Application.Transforms.DTO
{
    [Serializable]
    [DataContract]
    public class SitemapDTO
    {
        public SitemapDTO()
        {
            Id = 0;
            Name = "";
            ParentId = 0;
            TypeId = 0;
            PageId = 0;
            AlternateUrl = "";
            Icon = "";
            SortOrder = 0;
            CreatedBy = 0;
            ModifiedBy = 0;
            IsActive = false;
        }

        public SitemapDTO(SitemapDTO sitemap)
        {
            if (sitemap == null) return;
            Id = sitemap.Id;
            Name = sitemap.Name;
            ParentId = sitemap.ParentId;
            TypeId = sitemap.TypeId;
            PageId = sitemap.PageId;
            AlternateUrl = sitemap.AlternateUrl;
            SortOrder = sitemap.SortOrder;
            CreatedBy = sitemap.CreatedBy;
            ModifiedBy = sitemap.ModifiedBy;
            IsActive = sitemap.IsActive;
            Icon = sitemap.Icon;
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int TypeId { get; set; }

        [DataMember]
        public int ParentId { get; set; }

        [DataMember]
        public int PageId { get; set; }

        [DataMember]
        public string AlternateUrl { get; set; }
        [DataMember]
        public string Icon { get; set; }

        [DataMember]
        public int SortOrder { get; set; }

        [DataMember]
        public int CreatedBy { get; set; }

        [DataMember]
        public int? ModifiedBy { get; set; }

        [DataMember]
        public bool? IsActive { get; set; }
    }
}