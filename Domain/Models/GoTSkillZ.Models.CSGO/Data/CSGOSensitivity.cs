//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GoTSkillZ.Models.CSGO.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class CSGOSensitivity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Sensitivity { get; set; }
        public int DPI { get; set; }
        public int eDPI { get; set; }
        public bool RawInput { get; set; }
        public int WindowsSensitivity { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int MouseHz { get; set; }
        public bool Active { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public long StartUnixDatetime { get; set; }
        public Nullable<long> EndUnixDatetime { get; set; }
    }
}