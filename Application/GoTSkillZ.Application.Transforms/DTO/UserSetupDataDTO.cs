using GoTSkillZ.Models.UserSetup.Data;
using System;
using System.Runtime.Serialization;

namespace GoTSkillZ.Application.Transforms.DTO
{
    [Serializable]
    [DataContract]
    public class UserSetupDataDTO
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int UserId { get; set; }


        [DataMember]
        public int SetupId { get; set; }

        [DataMember]
        public string CompanyName { get; set; }

        [DataMember]
        public string ProductDetails { get; set; }

        [DataMember]
        public string Component { get; set; }

        [DataMember]
        public int SetupTypeId { get; set; }

        [DataMember]
        public string SetupTypeName { get; set; }

        [DataMember]
        public string SetupName { get; set; }

        [DataMember]
        public string AffiliateLink { get; set; }


        public UserSetupDataDTO()
        {
            Id = UserId = SetupId = 0;
            SetupTypeId = 0;
            CompanyName = ProductDetails = AffiliateLink = SetupTypeName = SetupName = "";
        }

        public UserSetupDataDTO(UserSetupData userSetup)
        {
            Id = userSetup.Id;
            UserId = userSetup.UserId;
            SetupId = userSetup.UserSetupId;
            CompanyName = userSetup.CompanyName;
            Component = userSetup.Component;
            ProductDetails = userSetup.ProductDetails;
            AffiliateLink = userSetup.AffiliateLink;


        }
    }
}
