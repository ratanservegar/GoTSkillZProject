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
    public class UserAchievementProvider : IUserAchievementProvider
    {

        private string _connectionString
        {
            get
            {
                var entityBuilder = new EntityConnectionStringBuilder
                {
                    Provider = "System.Data.SqlClient",
                    Metadata = @"res://*/Data.UserAchievementsModel.csdl|res://*/Data.UserAchievementsModel.ssdl|res://*/Data.UserAchievementsModel.msl",
                    ProviderConnectionString = ConfigurationManager.ConnectionStrings["GoTSkillZConnectionString"].ConnectionString
                };

                return entityBuilder.ToString();
            }
        }


        public IEnumerable<UserAchievement> GetAll()
        {
            using (var context = new GoTSkillZUserAchievementsEntities(_connectionString))
            {
                return context.UserAchievements.ToList();
            }
        }

        public IEnumerable<UserAchievement> FindBy(Expression<Func<UserAchievement, bool>> predicate)
        {
            using (var context = new GoTSkillZUserAchievementsEntities(_connectionString))
            {
                return context.UserAchievements.Where(predicate).ToList();
            }
        }

        public UserAchievement Get(int entityId)
        {
            using (var context = new GoTSkillZUserAchievementsEntities(_connectionString))
            {
                return context.UserAchievements.FirstOrDefault(x => x.Id == entityId);
            }
        }

        public UserAchievement Add(UserAchievement userAchievementObj)
        {
            if (userAchievementObj == null) return null;

            using (var context = new GoTSkillZUserAchievementsEntities(_connectionString))
            {
                context.UserAchievements.Add(userAchievementObj);
                context.SaveChanges();
            }

            return userAchievementObj;
        }

        public UserAchievement Update(UserAchievement userAchievementObj)
        {
            if (userAchievementObj == null) return null;

            using (var context = new GoTSkillZUserAchievementsEntities(_connectionString))
            {
                var existingAchievement = context.UserAchievements.FirstOrDefault(x => x.Id == userAchievementObj.Id);

                if (existingAchievement == null) return userAchievementObj;

                existingAchievement.Description = userAchievementObj.Description;
                existingAchievement.Description = userAchievementObj.Location;
                existingAchievement.Name = userAchievementObj.Name;
                existingAchievement.Date = userAchievementObj.Date;
                existingAchievement.Position = userAchievementObj.Position;
                existingAchievement.Type = userAchievementObj.Type;
                context.SaveChanges();
            }

            return userAchievementObj;
        }

        public void Delete(int entityId)
        {
            throw new NotImplementedException();
        }

        public string DeleteUserAchievements(string userAchievementIds)
        {
            var returnString = "";
            try
            {
                using (var context = new GoTSkillZUserAchievementsEntities(_connectionString))
                {
                    context.Database.ExecuteSqlCommand("DELETE  FROM UserAchievements WHERE Id IN (" + userAchievementIds + ")");
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
