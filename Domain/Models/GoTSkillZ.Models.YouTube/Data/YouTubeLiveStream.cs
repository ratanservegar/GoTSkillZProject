//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GoTSkillZ.Models.YouTube.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class YouTubeLiveStream
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string YouTubeChannalId { get; set; }
        public bool IsLive { get; set; }
        public string VideoId { get; set; }
        public string EmbedHTML { get; set; }
        public string StreamTitle { get; set; }
        public string LiveChatId { get; set; }
    }
}