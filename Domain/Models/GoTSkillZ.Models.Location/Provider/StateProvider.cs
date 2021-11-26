using GoTSkillZ.Models.Location.Data;
using GoTSkillZ.Models.Location.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Linq.Expressions;

namespace GoTSkillZ.Models.Location.Provider
{
    public class StateProvider : IStateProvider
    {

        private string _connectionString
        {
            get
            {
                var entityBuilder = new EntityConnectionStringBuilder
                {
                    Provider = "System.Data.SqlClient",
                    Metadata = @"res://*/Data.GoTSkillZStateModel.csdl|res://*/Data.GoTSkillZStateModel.ssdl|res://*/Data.GoTSkillZStateModel.msl",
                    ProviderConnectionString = ConfigurationManager.ConnectionStrings["GoTSkillZConnectionString"].ConnectionString
                };

                return entityBuilder.ToString();
            }
        }

        public IEnumerable<States> GetAll()
        {
            using (var context = new GoTSkillZStateEntities(_connectionString))
            {
                return context.States.ToList();
            }
        }

        public IEnumerable<States> FindBy(Expression<Func<States, bool>> predicate)
        {
            using (var context = new GoTSkillZStateEntities(_connectionString))
            {
                return context.States.Where(predicate).ToList();
            }
        }

        public States Get(int entityId)
        {
            using (var context = new GoTSkillZStateEntities(_connectionString))
            {
                return context.States.FirstOrDefault(x => x.id == entityId);
            }
        }

        public States Add(States T)
        {
            throw new NotImplementedException();
        }

        public States Update(States T)
        {
            throw new NotImplementedException();
        }

        public void Delete(int entityId)
        {
            throw new NotImplementedException();
        }
    }
}
