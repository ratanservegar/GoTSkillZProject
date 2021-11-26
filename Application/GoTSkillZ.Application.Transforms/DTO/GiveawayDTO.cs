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
    public class GiveawayDTO
    {
     
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Rules { get; set; }
        [DataMember]
        public bool International { get; set; }
        [DataMember]
        public bool Sponsored { get; set; }
        [DataMember]
        public Nullable<int> TotalEntries { get; set; }
        [DataMember]
        public string ImageUrl { get; set; }
        [DataMember]
        public string VideoUrl { get; set; }
        [DataMember]
        public bool Active { get; set; }
        [DataMember]
        public string Code { get; set; }

        public GiveawayDTO()
        {
            Id = 0;
            Code = Title = Description = Rules = ImageUrl = VideoUrl = string.Empty;
            International = false;
            Sponsored = false;
            TotalEntries = 0;
            Active = false;

        }

    }
}
