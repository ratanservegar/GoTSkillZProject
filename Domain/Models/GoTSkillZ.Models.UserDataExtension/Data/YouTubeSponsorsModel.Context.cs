﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GoTSkillZ.Models.UserDataExtension.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class GoTSkillZYouTubeSponsorsEntities : DbContext
    {
        public GoTSkillZYouTubeSponsorsEntities()
            : base("name=GoTSkillZYouTubeSponsorsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<YouTubeSponsors> YouTubeSponsors { get; set; }
    }
}
