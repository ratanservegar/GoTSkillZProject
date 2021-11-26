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
    public class UserProfileExtensionProvider : IUserProfileExtensionProvider
    {
        private string _connectionString
        {
            get
            {
                var entityBuilder = new EntityConnectionStringBuilder
                {
                    Provider = "System.Data.SqlClient",
                    Metadata = @"res://*/Data.UserProfileExtensionModel.csdl|res://*/Data.UserProfileExtensionModel.ssdl|res://*/Data.UserProfileExtensionModel.msl",
                    ProviderConnectionString = ConfigurationManager.ConnectionStrings["GoTSkillZConnectionString"].ConnectionString
                };

                return entityBuilder.ToString();
            }
        }


        public IEnumerable<UserProfileExtension> GetAll()
        {
            using (var context = new GoTSkillZUserProfileExtensionEntities(_connectionString))
            {
                return context.UserProfileExtension.ToList();
            }
        }

        public IEnumerable<UserProfileExtension> FindBy(Expression<Func<UserProfileExtension, bool>> predicate)
        {
            using (var context = new GoTSkillZUserProfileExtensionEntities(_connectionString))
            {
                return context.UserProfileExtension.Where(predicate).ToList();
            }
        }

        public UserProfileExtension Get(int entityId)
        {
            using (var context = new GoTSkillZUserProfileExtensionEntities(_connectionString))
            {
                return context.UserProfileExtension.FirstOrDefault(x => x.Id == entityId);
            }
        }

        public UserProfileExtension Add(UserProfileExtension userExtensionObj)
        {
            if (userExtensionObj == null) return null;

            using (var context = new GoTSkillZUserProfileExtensionEntities(_connectionString))
            {
                context.UserProfileExtension.Add(userExtensionObj);
                context.SaveChanges();
            }

            return userExtensionObj;
        }

        public UserProfileExtension Update(UserProfileExtension userExtensionObj)
        {
            if (userExtensionObj == null) return null;

            using (var context = new GoTSkillZUserProfileExtensionEntities(_connectionString))
            {
                var existingUserProfile = context.UserProfileExtension.FirstOrDefault(x => x.UserId == userExtensionObj.UserId);

                if (existingUserProfile == null) return userExtensionObj;

                existingUserProfile.Alias = userExtensionObj.Alias;
                existingUserProfile.About = userExtensionObj.About;
                existingUserProfile.PrimaryGame = userExtensionObj.PrimaryGame;
                existingUserProfile.SecondaryGame = userExtensionObj.SecondaryGame;
                existingUserProfile.PrimaryRole = userExtensionObj.PrimaryRole;
                existingUserProfile.SecondaryRole = userExtensionObj.SecondaryRole;
                existingUserProfile.Status = userExtensionObj.Status;
                existingUserProfile.PrimaryGameExp = userExtensionObj.PrimaryGameExp;
                existingUserProfile.SecondaryGameExp = userExtensionObj.SecondaryGameExp;
                existingUserProfile.CurrentTeam = userExtensionObj.CurrentTeam;
                context.SaveChanges();
            }

            return userExtensionObj;
        }

        public void Delete(int entityId)
        {
            throw new NotImplementedException();
        }
    }
}
