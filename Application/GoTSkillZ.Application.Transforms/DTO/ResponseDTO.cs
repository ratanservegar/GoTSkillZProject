using GoTSkillZ.Application.Transforms.Enums;
using System;
using System.Runtime.Serialization;

namespace GoTSkillZ.Application.Transforms.DTO
{
    [Serializable]
    [DataContract]
    public class ResponseDTO
    {
        [DataMember]
        public string ResponseText { get; set; }

        [DataMember]
        public GoTSkillZEnum StateCode { get; set; }

        [DataMember]
        public bool Success { get; set; }

        [DataMember]
        public int UserId { get; set; }


        [DataMember]
        public UserProfileDTO UserProfile { get; set; }

        public ResponseDTO()
        {

            StateCode = GoTSkillZEnum.NotLoggedIn;
            UserId = 0;
            UserProfile = null;
            ResponseText = "";
            Success = false;
        }
    }
}
