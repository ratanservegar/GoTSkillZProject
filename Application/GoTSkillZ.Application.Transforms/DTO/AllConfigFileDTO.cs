using System;
using System.Runtime.Serialization;

namespace GoTSkillZ.Application.Transforms.DTO
{
    [Serializable]
    [DataContract]
    public class AllConfigFileDTO
    {
        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string extension { get; set; }

        [DataMember]
        public string size { get; set; }

        [DataMember]
        public string filePath { get; set; }

        [DataMember]
        public int userId { get; set; }

        [DataMember]
        public string configType { get; set; }


        public AllConfigFileDTO()
        {
            name = extension = size = filePath = configType = "";
            userId = 0;
        }
    }
}
