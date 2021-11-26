using GoTSkillZ.Models.UserDataExtension.Data;
using GoTSkillZ.Models.UserDataExtension.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Linq.Expressions;

namespace GoTSkillZ.Models.UserDataExtension.Provider
{
    public class UserTeamHistoryProvider : IUserTeamHistoryProvider
    {
        private string _connectionString
        {
            get
            {
                var entityBuilder = new EntityConnectionStringBuilder
                {
                    Provider = "System.Data.SqlClient",
                    Metadata = @"res://*/Data.GoTSkillZUserTeamHistoryModel.csdl|res://*/Data.GoTSkillZUserTeamHistoryModel.ssdl|res://*/Data.GoTSkillZUserTeamHistoryModel.msl",
                    ProviderConnectionString = ConfigurationManager.ConnectionStrings["GoTSkillZConnectionString"].ConnectionString
                };

                return entityBuilder.ToString();
            }
        }


        public IEnumerable<UserTeamHistory> GetAll()
        {
            using (var context = new GoTSkillZUserTeamHistoryEntities(_connectionString))
            {
                return context.UserTeamHistories.ToList();
            }
        }

        public IEnumerable<UserTeamHistory> FindBy(Expression<Func<UserTeamHistory, bool>> predicate)
        {
            using (var context = new GoTSkillZUserTeamHistoryEntities(_connectionString))
            {
                return context.UserTeamHistories.Where(predicate).ToList();
            }
        }

        public UserTeamHistory Get(int entityId)
        {
            using (var context = new GoTSkillZUserTeamHistoryEntities(_connectionString))
            {
                return context.UserTeamHistories.FirstOrDefault(x => x.Id == entityId);
            }
        }

        public UserTeamHistory Add(UserTeamHistory userTeamHistory)
        {
            if (userTeamHistory == null) return null;

            using (var context = new GoTSkillZUserTeamHistoryEntities(_connectionString))
            {
                context.UserTeamHistories.Add(userTeamHistory);
                context.SaveChanges();
            }

            return userTeamHistory;
        }

        public UserTeamHistory Update(UserTeamHistory userTeamHistory)
        {
            if (userTeamHistory == null) return null;

            using (var context = new GoTSkillZUserTeamHistoryEntities(_connectionString))
            {
                var existingTeamHistory = context.UserTeamHistories.FirstOrDefault(x => x.Id == userTeamHistory.Id);

                if (existingTeamHistory == null) return userTeamHistory;

                existingTeamHistory.TeamName = userTeamHistory.TeamName;
                existingTeamHistory.FromDate = userTeamHistory.FromDate;
                existingTeamHistory.ToDate = userTeamHistory.ToDate;

                context.SaveChanges();
            }

            return userTeamHistory;
        }

        public void Delete(int entityId)
        {
            throw new NotImplementedException();
        }

        public string DeleteUserTeamHistory(string userTeamHistoryId)
        {
            var returnString = "";
            try
            {
                using (var context = new GoTSkillZUserTeamHistoryEntities(_connectionString))
                {
                    context.Database.ExecuteSqlCommand("DELETE  FROM UserTeamHistory WHERE Id IN (" + userTeamHistoryId + ")");
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
