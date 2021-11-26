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
    public class UserSetupDataProvider : IUserSetupDataProvider
    {
        private string _connectionString
        {
            get
            {
                var entityBuilder = new EntityConnectionStringBuilder
                {
                    Provider = "System.Data.SqlClient",
                    Metadata = @"res://*/Data.GoTSkillZUserSetupDataModel.csdl|res://*/Data.GoTSkillZUserSetupDataModel.ssdl|res://*/Data.GoTSkillZUserSetupDataModel.msl",
                    ProviderConnectionString = ConfigurationManager.ConnectionStrings["GoTSkillZConnectionString"].ConnectionString
                };

                return entityBuilder.ToString();
            }
        }


        public IEnumerable<UserSetupData> GetAll()
        {
            using (var context = new GoTSkillZUserSetupDataEntities(_connectionString))
            {
                return context.UserSetupDatas.ToList();
            }
        }

        public IEnumerable<UserSetupData> FindBy(Expression<Func<UserSetupData, bool>> predicate)
        {
            using (var context = new GoTSkillZUserSetupDataEntities(_connectionString))
            {
                return context.UserSetupDatas.Where(predicate).ToList();
            }
        }

        public UserSetupData Get(int entityId)
        {
            using (var context = new GoTSkillZUserSetupDataEntities(_connectionString))
            {
                return context.UserSetupDatas.FirstOrDefault(x => x.Id == entityId);
            }
        }

        public UserSetupData Add(UserSetupData setupDataObj)
        {
            if (setupDataObj == null) return null;

            using (var context = new GoTSkillZUserSetupDataEntities(_connectionString))
            {
                context.UserSetupDatas.Add(setupDataObj);
                context.SaveChanges();
            }

            return setupDataObj;
        }

        public UserSetupData Update(UserSetupData setupDataObj)
        {
            if (setupDataObj == null) return null;

            using (var context = new GoTSkillZUserSetupDataEntities(_connectionString))
            {
                var existingSetup = context.UserSetupDatas.FirstOrDefault(x => x.Id == setupDataObj.Id && x.UserId == setupDataObj.UserId);

                if (existingSetup == null) return setupDataObj;
                existingSetup.CompanyName = setupDataObj.CompanyName;
                existingSetup.ProductDetails = setupDataObj.ProductDetails;
                existingSetup.AffiliateLink = setupDataObj.AffiliateLink;

                context.SaveChanges();
            }

            return setupDataObj;
        }

        public void Delete(int entityId)
        {
            throw new NotImplementedException();
        }

        public string DeleteUserSetupData(string userSetupId)
        {
            var returnString = "";
            try
            {
                using (var context = new GoTSkillZUserSetupsEntities(_connectionString))
                {
                    context.Database.ExecuteSqlCommand("DELETE  FROM UserSetupData WHERE UserSetupId IN (" + userSetupId + ")");
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