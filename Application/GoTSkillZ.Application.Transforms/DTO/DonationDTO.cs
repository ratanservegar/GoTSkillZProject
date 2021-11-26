using System;
using System.Runtime.Serialization;

namespace GoTSkillZ.Application.Transforms.DTO
{
    [Serializable]
    [DataContract]
    public class DonationDTO
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int? UserId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string DonationType { get; set; }



        [DataMember]
        public string DonationTitle { get; set; }


        [DataMember]
        public string DonationDescription { get; set; }

        [DataMember]
        public string Amount { get; set; }

        [DataMember]
        public string CustomImgUrl { get; set; }

        [DataMember]
        public string DonationDate { get; set; }

        [DataMember]
        public bool IsActive { get; set; }



        public DonationDTO()
        {
            Id = 0;
            UserId = 0;
            DonationType = "";
            DonationTitle = "";
            DonationDescription = "";
            Name = "";
            Amount = "";
            CustomImgUrl = "";
            IsActive = true;
            DonationDate = "";
        }

    }
}
