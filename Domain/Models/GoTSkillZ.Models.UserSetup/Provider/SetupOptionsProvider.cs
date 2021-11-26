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
    public class SetupOptionsProvider : ISetupOptionsProvider
    {
        private string _connectionString
        {
            get
            {
                var entityBuilder = new EntityConnectionStringBuilder
                {
                    Provider = "System.Data.SqlClient",
                    Metadata = @"res://*/Data.GoTSkillZSetupOptionsModel.csdl|res://*/Data.GoTSkillZSetupOptionsModel.ssdl|res://*/Data.GoTSkillZSetupOptionsModel.msl",
                    ProviderConnectionString = ConfigurationManager.ConnectionStrings["GoTSkillZConnectionString"].ConnectionString
                };

                return entityBuilder.ToString();
            }
        }

        public IEnumerable<SetupOption> GetAll()
        {
            using (var context = new GoTSkillZSetupOptionsEntities(_connectionString))
            {
                return context.SetupOptions.ToList();
            }
        }

        public IEnumerable<SetupOption> FindBy(Expression<Func<SetupOption, bool>> predicate)
        {
            using (var context = new GoTSkillZSetupOptionsEntities(_connectionString))
            {
                return context.SetupOptions.Where(predicate).ToList();
            }
        }

        public SetupOption Get(int entityId)
        {
            using (var context = new GoTSkillZSetupOptionsEntities(_connectionString))
            {
                return context.SetupOptions.FirstOrDefault(x => x.Id == entityId);
            }
        }

        public SetupOption Add(SetupOption setupOptionObj)
        {
            if (setupOptionObj == null) return null;

            using (var context = new GoTSkillZSetupOptionsEntities(_connectionString))
            {
                context.SetupOptions.Add(setupOptionObj);
                context.SaveChanges();
            }

            return setupOptionObj;
        }

        public SetupOption Update(SetupOption setupOptionObj)
        {
            if (setupOptionObj == null) return null;

            using (var context = new GoTSkillZSetupOptionsEntities(_connectionString))
            {
                var existingSetupOption = context.SetupOptions.FirstOrDefault(x => x.Id == setupOptionObj.Id);

                if (existingSetupOption == null) return setupOptionObj;
                existingSetupOption.SetupOption1 = setupOptionObj.SetupOption1;
                existingSetupOption.Icon = setupOptionObj.Icon;

                context.SaveChanges();
            }

            return setupOptionObj;
        }

        public void Delete(int entityId)
        {
            throw new NotImplementedException();
        }
    }
}
