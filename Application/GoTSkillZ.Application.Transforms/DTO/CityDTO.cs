using GoTSkillZ.Models.Location.Data;
using System;
using System.Runtime.Serialization;

namespace GoTSkillZ.Application.Transforms.DTO
{
    [Serializable]
    [DataContract]
    public class CityDTO
    {
        [DataMember]
        public int CityId { get; set; }

        [DataMember]
        public int StateId { get; set; }

        [DataMember]
        public int CountryId { get; set; }

        [DataMember]
        public string CityName { get; set; }

        public CityDTO()
        {
            CityId = StateId = CountryId = 0;
            CityName = "";
        }

        public CityDTO(City city)
        {
            CityId = city.id;
            StateId = city.state_id;
            CountryId = city.country_id;
            CityName = city.name;
        }
    }
}
