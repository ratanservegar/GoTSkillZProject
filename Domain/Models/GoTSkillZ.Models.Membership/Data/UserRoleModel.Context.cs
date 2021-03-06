//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Data.Entity.Core.Objects;

namespace GoTSkillZ.Models.Membership.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class GoTSkillZUserRoleEntities : DbContext
    {
        public GoTSkillZUserRoleEntities()
            : base("name=GoTSkillZUserRoleEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<UserRoles> UserRoles { get; set; }

        public virtual int UpdateUserRoles(Nullable<int> userId, string roleIds)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("userId", userId) :
                new ObjectParameter("userId", typeof(int));

            var roleIdsParameter = roleIds != null ?
                new ObjectParameter("roleIds", roleIds) :
                new ObjectParameter("roleIds", typeof(string));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateUserRoles", userIdParameter, roleIdsParameter);
        }
    }
}
