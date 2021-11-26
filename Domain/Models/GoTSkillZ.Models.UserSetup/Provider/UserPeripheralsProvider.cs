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
    public class UserPeripheralsProvider : IUserPeripheralsProvider
    {
        private string _connectionString
        {
            get
            {
                var entityBuilder = new EntityConnectionStringBuilder
                {
                    Provider = "System.Data.SqlClient",
                    Metadata = @"res://*/Data.GoTSkillZUserPeripheralsModel.csdl|res://*/Data.GoTSkillZUserPeripheralsModel.ssdl|res://*/Data.GoTSkillZUserPeripheralsModel.msl",
                    ProviderConnectionString = ConfigurationManager.ConnectionStrings["GoTSkillZConnectionString"].ConnectionString
                };

                return entityBuilder.ToString();
            }
        }

        public IEnumerable<UserPeripherals> GetAll()
        {
            using (var context = new GoTSkillZUserPeripheralsEntities(_connectionString))
            {
                return context.UserPeripherals.ToList();
            }
        }

        public IEnumerable<UserPeripherals> FindBy(Expression<Func<UserPeripherals, bool>> predicate)
        {
            using (var context = new GoTSkillZUserPeripheralsEntities(_connectionString))
            {
                return context.UserPeripherals.Where(predicate).ToList();
            }
        }

        public UserPeripherals Get(int entityId)
        {
            using (var context = new GoTSkillZUserPeripheralsEntities(_connectionString))
            {
                return context.UserPeripherals.FirstOrDefault(x => x.Id == entityId);
            }
        }

        public UserPeripherals Add(UserPeripherals userPeripheralObj)
        {
            if (userPeripheralObj == null) return null;

            using (var context = new GoTSkillZUserPeripheralsEntities(_connectionString))
            {
                context.UserPeripherals.Add(userPeripheralObj);
                context.SaveChanges();
            }

            return userPeripheralObj;
        }

        public UserPeripherals Update(UserPeripherals userPeripheralObj)
        {
            if (userPeripheralObj == null) return null;

            using (var context = new GoTSkillZUserPeripheralsEntities(_connectionString))
            {
                var existingPeipheral = context.UserPeripherals.FirstOrDefault(x => x.Id == userPeripheralObj.Id && x.UserId == userPeripheralObj.UserId);

                if (existingPeipheral == null) return userPeripheralObj;
                existingPeipheral.PeripheralType = userPeripheralObj.PeripheralType;
                existingPeipheral.CompanyName = userPeripheralObj.CompanyName;
                existingPeipheral.ProductDetails = userPeripheralObj.ProductDetails;
                existingPeipheral.AffiliateLink = userPeripheralObj.AffiliateLink;
                context.SaveChanges();
            }

            return userPeripheralObj;
        }

        public void Delete(int entityId)
        {
            throw new NotImplementedException();
        }

        public string DeleteUserPeripheral(string userPeripheralId)
        {
            var returnString = "";
            try
            {
                using (var context = new GoTSkillZUserPeripheralsEntities(_connectionString))
                {
                    context.Database.ExecuteSqlCommand("DELETE FROM UserPeripherals WHERE Id IN (" + userPeripheralId + ")");
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
