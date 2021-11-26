using System;
using System.Runtime.Serialization;

namespace GoTSkillZ.Application.Transforms.DTO
{
    [Serializable]
    [DataContract]
    public class GamePlatformConnectivityDTO
    {
        [DataMember]
        public string SteamId { get; set; }

        [DataMember]
        public string SteamId64 { get; set; }

        [DataMember]
        public string FaceitId { get; set; }

        [DataMember]
        public string SoSotronkId { get; set; }

        [DataMember]
        public string ESEA { get; set; }

        [DataMember]
        public string ESLId { get; set; }

        public GamePlatformConnectivityDTO()
        {
            SteamId = SteamId64 = FaceitId = SoSotronkId = ESEA = ESLId = "";
        }

    }
}
