using GoTSkillZ.Models.CSGO.Data;
using System;
using System.Runtime.Serialization;

namespace GoTSkillZ.Application.Transforms.DTO
{
    [Serializable]
    [DataContract]
    public class CSGOSensitivityDTO
    {
        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public string Sensitivity { get; set; }
        [DataMember]
        public int DPI { get; set; }
        [DataMember]
        public int eDPI { get; set; }
        [DataMember]
        public bool RawInput { get; set; }
        [DataMember]
        public int WindowsSensitivity { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }

        [DataMember]
        public System.DateTime? EndDate { get; set; }
        [DataMember]
        public long StartUnixDatetime { get; set; }

        [DataMember]
        public long? EndUnixDatetime { get; set; }
        [DataMember]
        public int MouseHz { get; set; }

        [DataMember]
        public bool Active { get; set; }

        public CSGOSensitivityDTO()
        {
            UserId = 0;
            Sensitivity = "0";
            DPI = 0;
            eDPI = 0;
            RawInput = true;
            WindowsSensitivity = 6;
            CreatedDate = DateTime.Now;
            EndDate = null;
            StartUnixDatetime = 0;
            EndUnixDatetime = 0;
            Active = true;
            MouseHz = 0;

        }

        public CSGOSensitivityDTO(CSGOSensitivity csgoSensitivity)
        {
            UserId = csgoSensitivity.UserId;
            Sensitivity = csgoSensitivity.Sensitivity.ToString();
            DPI = csgoSensitivity.DPI;
            eDPI = csgoSensitivity.eDPI;
            RawInput = csgoSensitivity.RawInput;
            WindowsSensitivity = csgoSensitivity.WindowsSensitivity;
            CreatedDate = csgoSensitivity.CreatedDate;
            StartUnixDatetime = csgoSensitivity.StartUnixDatetime;
            EndUnixDatetime = csgoSensitivity.EndUnixDatetime;
            MouseHz = csgoSensitivity.MouseHz;
            Active = csgoSensitivity.Active;
            EndDate = csgoSensitivity.EndDate;

        }
    }

}
