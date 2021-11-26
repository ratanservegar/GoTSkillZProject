using GoTSkillZ.Models.Location.Data;
using System;
using System.Runtime.Serialization;

namespace GoTSkillZ.Application.Transforms.DTO
{
    [Serializable]
    [DataContract]
    public class CountryDTO
    {
        public CountryDTO()
        {
            CountryId = 0;
            CountryName = "";
        }

        public CountryDTO(Countries countries)
        {
            CountryId = countries.id;
            CountryName = countries.name;
        }

        [DataMember]
        public int CountryId { get; set; }


        [DataMember]
        public string CountryName { get; set; }


    }
}
