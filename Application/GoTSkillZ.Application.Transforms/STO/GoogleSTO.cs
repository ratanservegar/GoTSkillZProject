using System;
using System.Runtime.Serialization;

namespace GoTSkillZ.Application.Transforms.STO
{
    [Serializable]
    [DataContract]
    public class GoogleSTO
    {
        [DataMember]
        public string GoogleUserId { get; set; }

        [DataMember]
        public string GoogleAccessToken { get; set; }

        [DataMember]
        public string GoogleUserEmail { get; set; }

        [DataMember]
        public string GoogleFirstName { get; set; }


        [DataMember]
        public string GoogleLastName { get; set; }


        [DataMember]
        public string GoogleOAuthAccessToken { get; set; }

    }
}
