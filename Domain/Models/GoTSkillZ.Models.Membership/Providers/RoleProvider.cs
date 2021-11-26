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
    public class RoleProvider : IRoleProvider
    {
        private string _connectionString
        {
            get
            {
                var entityBuilder = new EntityConnectionStringBuilder
                {
                    Provider = "System.Data.SqlClient",
                    Metadata = @"res://*/Data.RoleModel.csdl|res://*/Data.RoleModel.ssdl|res://*/Data.RoleModel.msl",
                    ProviderConnectionString = ConfigurationManager.ConnectionStrings["GoTSkillZConnectionString"].ConnectionString
                };

                return entityBuilder.ToString();
            }
        }

        public IEnumerable<Roles> GetAll()
        {
            using (var context = new GoTSkillZRoleEntities(_connectionString))
            {
                return context.Roles.ToList();
            }
        }

        public IEnumerable<Roles> FindBy(Expression<Func<Roles, bool>> predicate)
        {
            using (var context = new GoTSkillZRoleEntities(_connectionString))
            {
                return context.Roles.Where(predicate).ToList();
            }
        }

        public Roles Get(int entityId)
        {
            using (var context = new GoTSkillZRoleEntities(_connectionString))
            {
                return context.Roles.FirstOrDefault(x => x.Id == entityId);
            }
        }

        public Roles Add(Roles T)
        {
            throw new NotImplementedException();
        }

        public Roles Update(Roles T)
        {
            throw new NotImplementedException();
        }

        public void Delete(int entityId)
        {
            throw new NotImplementedException();
        }
    }
}
