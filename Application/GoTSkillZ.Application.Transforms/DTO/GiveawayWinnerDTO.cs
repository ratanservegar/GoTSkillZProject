using System;
using System.Runtime.Serialization;

namespace GoTSkillZ.Application.Transforms.DTO
{
    [Serializable]
    [DataContract]
    public class GiveawayWinnerDTO
    {
        [DataMember] public int Id { get; set; }

        [DataMember] public int GiveawayId { get; set; }
        [DataMember] public string GiveawayTitle { get; set; }

        [DataMember] public int UserId { get; set; }

        [DataMember] public string WinnerImageUrl { get; set; }

        [DataMember] public string WinDate { get; set; }

        [DataMember] public string  WinnerName { get; set; }

        [DataMember] public string WinningEntryCode { get; set; }
    }
}