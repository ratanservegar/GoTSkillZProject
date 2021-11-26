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
    public class UserSetupsProvider : IUserSetupsProvider
    {

        private string _connectionString
        {
            get
            {
                var entityBuilder = new EntityConnectionStringBuilder
                {
                    Provider = "System.Data.SqlClient",
                    Metadata = @"res://*/Data.GoTSkillZUserSetupsModel.csdl|res://*/Data.GoTSkillZUserSetupsModel.ssdl|res://*/Data.GoTSkillZUserSetupsModel.msl",
                    ProviderConnectionString = ConfigurationManager.ConnectionStrings["GoTSkillZConnectionString"].ConnectionString
                };

                return entityBuilder.ToString();
            }
        }


        public IEnumerable<UserSetups> GetAll()
        {
            using (var context = new GoTSkillZUserSetupsEntities(_connectionString))
            {
                return context.UserSetups.ToList();
            }
        }

        public IEnumerable<UserSetups> FindBy(Expression<Func<UserSetups, bool>> predicate)
        {
            using (var context = new GoTSkillZUserSetupsEntities(_connectionString))
            {
                return context.UserSetups.Where(predicate).ToList();
            }
        }

        public UserSetups Get(int entityId)
        {
            using (var context = new GoTSkillZUserSetupsEntities(_connectionString))
            {
                return context.UserSetups.FirstOrDefault(x => x.Id == entityId);
            }
        }

        public UserSetups Add(UserSetups userSetupsObj)
        {
            if (userSetupsObj == null) return null;

            using (var context = new GoTSkillZUserSetupsEntities(_connectionString))
            {
                context.UserSetups.Add(userSetupsObj);
                context.SaveChanges();
            }

            return userSetupsObj;
        }

        public UserSetups Update(UserSetups userSetupsObj)
        {
            if (userSetupsObj == null) return null;

            using (var context = new GoTSkillZUserSetupsEntities(_connectionString))
            {
                var existingSetup = context.UserSetups.FirstOrDefault(x => x.Id == userSetupsObj.Id && x.UserId == userSetupsObj.UserId);

                if (existingSetup == null) return userSetupsObj;
                existingSetup.SetupTypeId = userSetupsObj.SetupTypeId;
                existingSetup.SetupImagePath = userSetupsObj.SetupImagePath;
                existingSetup.SetupName = userSetupsObj.SetupName;

                context.SaveChanges();
            }

            return userSetupsObj;
        }

        public void Delete(int entityId)
        {
            throw new NotImplementedException();
        }

        public string DeleteUserSetup(string userSetupId)
        {
            var returnString = "";
            try
            {
                using (var context = new GoTSkillZUserSetupsEntities(_connectionString))
                {
                    context.Database.ExecuteSqlCommand("DELETE  FROM UserSetups WHERE Id IN (" + userSetupId + ")");
                    returnString = "success";
                }
            }
            catch (Exception)
            {

                returnString = "failed";
            }

            return returnString;
        }
    }
}
