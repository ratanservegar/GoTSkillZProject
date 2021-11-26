using GoTSkillZ.Models.Membership.Data;
using GoTSkillZ.Models.UserDataExtension.Data;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GoTSkillZ.Application.Transforms.DTO
{
    [Serializable]
    [DataContract]
    public class UserProfileDTO
    {
        public UserProfileDTO()
        {
            UserId = 0;
            FirstName = "";
            LastName = "";
            Email = "";
            TelNo = "";
            Age = 0;
            Country = "";
            State = "";
            City = "";
            PinCode = "";
            Address = "";
            IsActive = true;
            IsDeleted = false;
            UserSocialLinks = null;
            UserRoles = null;
            LoggedIn = false;
            IsSubscriber = false;
            IsSponsor = false;
            IsRegistered = false;
            IsAdmin = false;
            UserAchievements = null;
            DOB = "";
            ShowPersonalInfo = false;
            UserProfileExtension = null;
            ProfileImage = "/CustomContent/Images/profile-image.png";
            RemoveAchievements = new List<string>();
            RemovePeripherals = new List<string>();
            RemoveTeamHistory = new List<string>();
            Gender = "";
            MaritalStatus = "";
            Occupation = "";
            UserSetups = null;
            UserSetupData = null;
            UserPeripheralData = null;
            ReadOnly = false;
            UserTeamHistory = null;
        }


        public UserProfileDTO(Users user)
        {
            UserId = user.UserId;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            TelNo = user.TelNo;
            DOB = user.DOB.ToString();
            Age = user.Age;
            Country = user.Country;
            State = user.State;
            City = user.City;
            ShowPersonalInfo = user.ShowPersonalInfo;
            PinCode = user.PinCode;
            Address = user.Address;
            IsActive = user.IsActive;
            IsDeleted = user.IsDeleted;
            IsRegistered = user.IsRegistered;
            Gender = user.Gender;
            MaritalStatus = user.MaritalStatus;
            Occupation = user.Occupation;
        }

        [DataMember]
        public int UserId { get; set; }


        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }


        [DataMember]
        public string Email { get; set; }


        [DataMember]
        public string TelNo { get; set; }

        [DataMember]
        public int? Age { get; set; }

        [DataMember]
        public string DOB { get; set; }


        [DataMember]
        public string Country { get; set; }

        [DataMember]
        public string State { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string PinCode { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public bool ShowPersonalInfo { get; set; }


        [DataMember]
        public bool IsActive { get; set; }

        [DataMember]
        public bool IsDeleted { get; set; }

        [DataMember]
        public UserSocialLinks UserSocialLinks { get; set; }

        [DataMember]
        public List<UserAchievementsDTO> UserAchievements { get; set; }




        [DataMember]
        public bool LoggedIn { get; set; }

        [DataMember]
        public bool IsSubscriber { get; set; }

        [DataMember]
        public bool IsSponsor { get; set; }

        [DataMember]
        public bool IsRegistered { get; set; }

        [DataMember]
        public List<UserRoles> UserRoles { get; set; }

        [DataMember]
        public bool IsAdmin { get; set; }


        [DataMember]
        public UserProfileExtensionDTO UserProfileExtension { get; set; }


        [DataMember]
        public GamePlatformConnectivityDTO GamePlatformConnectivity { get; set; }

        [DataMember]
        public string ProfileImage { get; set; }

        [DataMember]
        public List<string> RemoveAchievements { get; set; }

        [DataMember]
        public List<string> RemovePeripherals { get; set; }

        [DataMember]
        public List<string> RemoveTeamHistory { get; set; }

        [DataMember]
        public string Gender { get; set; }

        [DataMember]
        public string MaritalStatus { get; set; }

        [DataMember]
        public string Occupation { get; set; }

        [DataMember]
        public List<UserSetupDTO> UserSetups { get; set; }

        [DataMember]
        public List<UserSetupDataDTO> UserSetupData { get; set; }

        [DataMember]
        public List<UserPeripheralDTO> UserPeripheralData { get; set; }


        [DataMember]
        public bool ReadOnly { get; set; }

        [DataMember]
        public List<UserTeamHistoryDTO> UserTeamHistory { get; set; }

    }
}