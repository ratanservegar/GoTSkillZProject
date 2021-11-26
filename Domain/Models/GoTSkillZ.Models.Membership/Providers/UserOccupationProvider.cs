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
    public class UserOccupationProvider : IUserOccupationProvider
    {

        private string _connectionString
        {
            get
            {
                var entityBuilder = new EntityConnectionStringBuilder
                {
                    Provider = "System.Data.SqlClient",
                    Metadata = @"res://*/Data.UserOccupationModel.csdl|res://*/Data.UserOccupationModel.ssdl|res://*/Data.UserOccupationModel.msl",
                    ProviderConnectionString = ConfigurationManager.ConnectionStrings["GoTSkillZConnectionString"].ConnectionString
                };

                return entityBuilder.ToString();
            }
        }

        public IEnumerable<UserOccupation> GetAll()
        {
            using (var context = new GoTSkillZUserOccupationEntities(_connectionString))
            {
                return context.UserOccupation.ToList();
            }
        }

        public IEnumerable<UserOccupation> FindBy(Expression<Func<UserOccupation, bool>> predicate)
        {
            using (var context = new GoTSkillZUserOccupationEntities(_connectionString))
            {
                return context.UserOccupation.Where(predicate).ToList();
            }
        }

        public UserOccupation Get(int entityId)
        {
            using (var context = new GoTSkillZUserOccupationEntities(_connectionString))
            {
                return context.UserOccupation.FirstOrDefault(x => x.Id == entityId);
            }
        }

        public UserOccupation Add(UserOccupation userOccupation)
        {
            if (userOccupation == null) return null;

            using (var context = new GoTSkillZUserOccupationEntities(_connectionString))
            {
                context.UserOccupation.Add(userOccupation);
                context.SaveChanges();
            }

            return userOccupation;
        }

        public UserOccupation Update(UserOccupation T)
        {
            throw new NotImplementedException();
        }

        public void Delete(int entityId)
        {
            throw new NotImplementedException();
        }
    }
}
