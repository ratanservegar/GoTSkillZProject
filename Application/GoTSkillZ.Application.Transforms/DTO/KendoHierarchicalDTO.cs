using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GoTSkillZ.Application.Transforms.DTO
{
    [Serializable]
    [DataContract]
    public class KendoHierarchicalDTO
    {
        [DataMember(Name = "id")]
        public int Id;
        [DataMember(Name = "text")]
        public String Text;
        [DataMember(Name = "typeId")]
        public int typeId;
        [DataMember(Name = "imageUrl")]
        public String ImageUrl;
        [DataMember(Name = "icon")]
        public String Icon;
        [DataMember(Name = "Url")]
        public string Url;
        [DataMember(Name = "items")]
        public List<KendoHierarchicalDTO> Items;

        public KendoHierarchicalDTO()
        {
            Id = 0;
            Text = "";
            ImageUrl = "";
            Icon = "";
            Url = "";
            typeId = 0;
            Items = null;
        }

        public KendoHierarchicalDTO(int id, int typeid, String text, String imageUrl, String icon, string url, List<KendoHierarchicalDTO> items)
        {
            Id = id;
            typeId = typeid;
            Text = text;
            ImageUrl = imageUrl;
            Icon = icon;
            Url = url;
            Items = items;
        }
    }
}
