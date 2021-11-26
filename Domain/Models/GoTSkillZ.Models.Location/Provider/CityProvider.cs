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
    public class CityProvider : ICityProvider
    {

        private string _connectionString
        {
            get
            {
                var entityBuilder = new EntityConnectionStringBuilder
                {
                    Provider = "System.Data.SqlClient",
                    Metadata = @"res://*/Data.GoTSkillZCityModel.csdl|res://*/Data.GoTSkillZCityModel.ssdl|res://*/Data.GoTSkillZCityModel.msl",
                    ProviderConnectionString = ConfigurationManager.ConnectionStrings["GoTSkillZConnectionString"].ConnectionString
                };

                return entityBuilder.ToString();
            }
        }


        public IEnumerable<City> GetAll()
        {
            using (var context = new GoTSkillZCityEntities(_connectionString))
            {
                return context.Cities.ToList();
            }
        }

        public IEnumerable<City> FindBy(Expression<Func<City, bool>> predicate)
        {
            using (var context = new GoTSkillZCityEntities(_connectionString))
            {
                return context.Cities.Where(predicate).ToList();
            }
        }

        public City Get(int entityId)
        {
            using (var context = new GoTSkillZCityEntities(_connectionString))
            {
                return context.Cities.FirstOrDefault(x => x.id == entityId);
            }
        }

        public City Add(City T)
        {
            throw new NotImplementedException();
        }

        public City Update(City T)
        {
            throw new NotImplementedException();
        }

        public void Delete(int entityId)
        {
            throw new NotImplementedException();
        }
    }
}
