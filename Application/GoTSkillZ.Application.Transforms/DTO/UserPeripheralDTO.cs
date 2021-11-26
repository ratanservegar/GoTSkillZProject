using GoTSkillZ.Models.UserSetup.Data;
using System;
using System.Runtime.Serialization;

namespace GoTSkillZ.Application.Transforms.DTO
{
    [Serializable]
    [DataContract]
    public class UserPeripheralDTO
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public string CompanyName { get; set; }

        [DataMember]
        public string ProductDetails { get; set; }

        [DataMember]
        public string PeripheralType { get; set; }

        [DataMember]
        public string AffiliateLink { get; set; }



        [DataMember]
        public string PeripheralTopimage { get; set; }



        public UserPeripheralDTO()
        {
            Id = UserId = 0;
            PeripheralTopimage = "/CustomContent/Images/setupPlaceholder.jpg";
            CompanyName = ProductDetails = AffiliateLink = PeripheralType = "";
        }

        public UserPeripheralDTO(UserPeripherals userPhPeripherals)
        {
            Id = userPhPeripherals.Id;
            UserId = userPhPeripherals.UserId;
            CompanyName = userPhPeripherals.CompanyName;
            ProductDetails = userPhPeripherals.ProductDetails;
            AffiliateLink = userPhPeripherals.AffiliateLink;
            PeripheralType = userPhPeripherals.PeripheralType;
        }

    }
}
