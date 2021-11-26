using GoTSkillZ.Models.Membership.Data;
using GoTSkillZ.Models.Membership.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Linq.Expressions;

namespace GoTSkillZ.Models.Membership.Providers
{
    public class UserRoleProvider : IUserRoleProvider
    {
        private string _connectionString
        {
            get
            {
                var entityBuilder = new EntityConnectionStringBuilder
                {
                    Provider = "System.Data.SqlClient",
                    Metadata = @"res://*/Data.UserRoleModel.csdl|res://*/Data.UserRoleModel.ssdl|res://*/Data.UserRoleModel.msl",
                    ProviderConnectionString = ConfigurationManager.ConnectionStrings["GoTSkillZConnectionString"].ConnectionString
                };

                return entityBuilder.ToString();
            }
        }

        public IEnumerable<UserRoles> GetAll()
        {
            using (var context = new GoTSkillZUserRoleEntities(_connectionString))
            {
                return context.UserRoles.ToList();
            }
        }

        public IEnumerable<UserRoles> FindBy(Expression<Func<UserRoles, bool>> predicate)
        {
            using (var context = new GoTSkillZUserRoleEntities(_connectionString))
            {
                return context.UserRoles.Where(predicate).ToList();
            }
        }

        public UserRoles Get(int entityId)
        {
            using (var context = new GoTSkillZUserRoleEntities(_connectionString))
            {
                return context.UserRoles.FirstOrDefault(x => x.Id == entityId);
            }
        }

        public UserRoles Add(UserRoles userRole)
        {
            if (userRole == null) return null;

            using (var context = new GoTSkillZUserRoleEntities(_connectionString))
            {
                context.UserRoles.Add(userRole);
                context.SaveChanges();
            }

            return userRole;
        }

        public UserRoles Update(UserRoles userRole)
        {
            if (userRole == null) return null;

            using (var context = new GoTSkillZUserRoleEntities(_connectionString))
            {
                var existingUserRole = context.UserRoles.FirstOrDefault(x => x.Id == userRole.Id);

                //TODO:This can be made smarter with context attachments
                if (existingUserRole == null) return userRole;
                existingUserRole.RoleId = userRole.RoleId;
                existingUserRole.UserId = userRole.UserId;
                existingUserRole.IsActive = userRole.IsActive;
                existingUserRole.IsDeleted = userRole.IsDeleted;
                context.SaveChanges();
            }

            return userRole;
        }

        public void Delete(int entityId)
        {
            throw new NotImplementedException();
        }

        public void Delete(int userId, int roleId)
        {
            using (var context = new GoTSkillZUserRoleEntities(_connectionString))
            {
                context.Database.ExecuteSqlCommand("DELETE FROM UserRoles where UserId = " + userId + "AND RoleId = " + roleId);
            }
        }

        public void SaveUserRoles(List<UserRoles> userRoles)
        {
            if (userRoles == null) return;

            int userId = userRoles[0].UserId;
            int[] roleIds = userRoles.AsEnumerable().Select(x => x.RoleId).ToArray();
            string roles = string.Join(",", roleIds);
            using (var context = new GoTSkillZUserRoleEntities(_connectionString))
            {
                context.UpdateUserRoles(userId, roles);
            }

        }
    }
}
