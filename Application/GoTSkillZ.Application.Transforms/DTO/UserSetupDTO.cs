using GoTSkillZ.Models.UserSetup.Data;
using System;
using System.Runtime.Serialization;

namespace GoTSkillZ.Application.Transforms.DTO
{
    [Serializable]
    [DataContract]
    public class UserSetupDTO
    {

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public int SetupTypeId { get; set; }

        [DataMember]
        public string SetupImagePath { get; set; }

        [DataMember]
        public string SetupName { get; set; }


        public UserSetupDTO()
        {
            Id = UserId = SetupTypeId = 0;
            SetupImagePath = "/CustomContent/Images/setupPlaceholder.jpg";
            SetupName = "";
        }

        public UserSetupDTO(UserSetups userSetup)
        {
            Id = userSetup.Id;
            UserId = userSetup.UserId;
            SetupTypeId = userSetup.SetupTypeId;
            SetupImagePath = userSetup.SetupImagePath;
            SetupName = userSetup.SetupName;
        }
    }
}
