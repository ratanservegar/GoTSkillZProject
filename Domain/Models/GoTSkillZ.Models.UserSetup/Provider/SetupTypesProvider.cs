using GoTSkillZ.Models.UserSetup.Data;
using GoTSkillZ.Models.UserSetup.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Linq.Expressions;

namespace GoTSkillZ.Models.UserSetup.Provider
{

    public class SetupTypesProvider : ISetupTypesProvider
    {

        private string _connectionString
        {
            get
            {
                var entityBuilder = new EntityConnectionStringBuilder
                {
                    Provider = "System.Data.SqlClient",
                    Metadata = @"res://*/Data.GoTSkillZSetupTypesModel.csdl|res://*/Data.GoTSkillZSetupTypesModel.ssdl|res://*/Data.GoTSkillZSetupTypesModel.msl",
                    ProviderConnectionString = ConfigurationManager.ConnectionStrings["GoTSkillZConnectionString"].ConnectionString
                };

                return entityBuilder.ToString();
            }
        }


        public IEnumerable<SetupType> GetAll()
        {
            using (var context = new GoTSkillZSetupTypesEntities(_connectionString))
            {
                return context.SetupTypes.ToList();
            }
        }

        public IEnumerable<SetupType> FindBy(Expression<Func<SetupType, bool>> predicate)
        {
            using (var context = new GoTSkillZSetupTypesEntities(_connectionString))
            {
                return context.SetupTypes.Where(predicate).ToList();
            }
        }

        public SetupType Get(int entityId)
        {
            using (var context = new GoTSkillZSetupTypesEntities(_connectionString))
            {
                return context.SetupTypes.FirstOrDefault(x => x.Id == entityId);
            }
        }

        public SetupType Add(SetupType setupTypeObj)
        {
            if (setupTypeObj == null) return null;

            using (var context = new GoTSkillZSetupTypesEntities(_connectionString))
            {
                context.SetupTypes.Add(setupTypeObj);
                context.SaveChanges();
            }

            return setupTypeObj;
        }

        public SetupType Update(SetupType T)
        {
            throw new NotImplementedException();
        }

        public void Delete(int entityId)
        {
            throw new NotImplementedException();
        }
    }
}
