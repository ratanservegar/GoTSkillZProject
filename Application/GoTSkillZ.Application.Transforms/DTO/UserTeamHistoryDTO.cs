using GoTSkillZ.Models.UserDataExtension.Data;
using System;
using System.Runtime.Serialization;

namespace GoTSkillZ.Application.Transforms.DTO
{

    [Serializable]
    [DataContract]
    public class UserTeamHistoryDTO
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public string TeamName { get; set; }

        [DataMember]
        public string FromDate { get; set; }

        [DataMember]
        public string ToDate { get; set; }

        public UserTeamHistoryDTO()
        {
            Id = 0;
            UserId = 0;
            TeamName = "";
            FromDate = ToDate = null;
        }

        public UserTeamHistoryDTO(UserTeamHistory userTeamHistory)
        {
            Id = userTeamHistory.Id;
            TeamName = userTeamHistory.TeamName;
            UserId = userTeamHistory.UserId;
            FromDate = userTeamHistory.FromDate;
            ToDate = userTeamHistory.ToDate;
        }
    }
}
