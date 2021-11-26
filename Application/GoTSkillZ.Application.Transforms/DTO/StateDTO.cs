using GoTSkillZ.Models.Location.Data;
using System;
using System.Runtime.Serialization;

namespace GoTSkillZ.Application.Transforms.DTO
{

    [Serializable]
    [DataContract]
    public class StateDTO
    {
        [DataMember]
        public int StateId { get; set; }

        [DataMember]
        public int CountryId { get; set; }

        [DataMember]
        public string StateName { get; set; }


        public StateDTO()
        {
            StateId = CountryId = 0;
            StateName = "";
        }

        public StateDTO(States states)
        {
            StateId = states.id;
            CountryId = states.country_id;
            StateName = states.name;
        }
    }
}
