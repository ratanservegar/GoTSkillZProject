using GoTSkillZ.Models.UserDataExtension.Data;
using System;
using System.Runtime.Serialization;

namespace GoTSkillZ.Application.Transforms.DTO
{
    [Serializable]
    [DataContract]
    public class UserProfileExtensionDTO
    {
        public UserProfileExtensionDTO()
        {
            Alias = About = PrimaryGame = SecondaryGame = PrimaryRole = SecondaryRole = Status = PrimaryGameExp = SecondaryGameExp = CurrentTeam = "";
        }

        public UserProfileExtensionDTO(UserProfileExtension userProfileExtension)
        {
            Alias = userProfileExtension.Alias;
            About = userProfileExtension.About;
            PrimaryGame = userProfileExtension.PrimaryGame;
            SecondaryGame = userProfileExtension.SecondaryGame;
            PrimaryRole = userProfileExtension.PrimaryRole;
            SecondaryRole = userProfileExtension.SecondaryRole;
            Status = userProfileExtension.Status;
            PrimaryGameExp = userProfileExtension.PrimaryGameExp;
            SecondaryGameExp = userProfileExtension.SecondaryGameExp;
            CurrentTeam = userProfileExtension.CurrentTeam;
        }

        [DataMember]
        public string Alias { get; set; }

        [DataMember]
        public string About { get; set; }

        [DataMember]
        public string PrimaryGame { get; set; }

        [DataMember]
        public string SecondaryGame { get; set; }

        [DataMember]
        public string PrimaryRole { get; set; }

        [DataMember]
        public string SecondaryRole { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public string PrimaryGameExp { get; set; }

        [DataMember]
        public string SecondaryGameExp { get; set; }


        [DataMember]
        public string CurrentTeam { get; set; }


    }
}