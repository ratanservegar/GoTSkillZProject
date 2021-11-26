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
    public class CountryProvider : ICountryProvider
    {

        private string _connectionString
        {
            get
            {
                var entityBuilder = new EntityConnectionStringBuilder
                {
                    Provider = "System.Data.SqlClient",
                    Metadata = @"res://*/Data.GoTSkillZCountryModel.csdl|res://*/Data.GoTSkillZCountryModel.ssdl|res://*/Data.GoTSkillZCountryModel.msl",
                    ProviderConnectionString = ConfigurationManager.ConnectionStrings["GoTSkillZConnectionString"].ConnectionString
                };

                return entityBuilder.ToString();
            }
        }
        public IEnumerable<Countries> GetAll()
        {
            using (var context = new GoTSkillZCountryEntities(_connectionString))
            {
                return context.Countries.ToList();
            }
        }

        public IEnumerable<Countries> FindBy(Expression<Func<Countries, bool>> predicate)
        {
            using (var context = new GoTSkillZCountryEntities(_connectionString))
            {
                return context.Countries.Where(predicate).ToList();
            }
        }

        public Countries Get(int entityId)
        {
            using (var context = new GoTSkillZCountryEntities(_connectionString))
            {
                return context.Countries.FirstOrDefault(x => x.id == entityId);
            }
        }

        public Countries Add(Countries T)
        {
            throw new NotImplementedException();
        }

        public Countries Update(Countries T)
        {
            throw new NotImplementedException();
        }

        public void Delete(int entityId)
        {
            throw new NotImplementedException();
        }
    }
}
