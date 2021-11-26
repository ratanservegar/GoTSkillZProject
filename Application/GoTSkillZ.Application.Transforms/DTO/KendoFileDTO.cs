using System;
using System.Runtime.Serialization;

namespace GoTSkillZ.Application.Transforms.DTO
{
    [Serializable]
    [DataContract]
    public class KendoFileDTO
    {
        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string extension { get; set; }

        [DataMember]
        public string size { get; set; }

        [DataMember]
        public string imagePath { get; set; }

        [DataMember]
        public int userId { get; set; }


        public KendoFileDTO()
        {
            name = extension = size = imagePath = "";
            userId = 0;
        }
    }
}
