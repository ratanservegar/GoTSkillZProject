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
    public class UserSocialLinksProvider : IUserSocialLinksProvider
    {
        private string _connectionString
        {
            get
            {
                var entityBuilder = new EntityConnectionStringBuilder
                {
                    Provider = "System.Data.SqlClient",
                    Metadata = @"res://*/Data.UserSocialLinksModel.csdl|res://*/Data.UserSocialLinksModel.ssdl|res://*/Data.UserSocialLinksModel.msl",
                    ProviderConnectionString = ConfigurationManager.ConnectionStrings["GoTSkillZConnectionString"].ConnectionString
                };

                return entityBuilder.ToString();
            }
        }

        public IEnumerable<UserSocialLinks> GetAll()
        {
            using (var context = new GoTSkillZUserSocialLinksEntities(_connectionString))
            {
                return context.UserSocialLinks.ToList();
            }
        }

        public IEnumerable<UserSocialLinks> FindBy(Expression<Func<UserSocialLinks, bool>> predicate)
        {
            using (var context = new GoTSkillZUserSocialLinksEntities(_connectionString))
            {
                return context.UserSocialLinks.Where(predicate).ToList();
            }
        }

        public UserSocialLinks Get(int entityId)
        {
            using (var context = new GoTSkillZUserSocialLinksEntities(_connectionString))
            {
                return context.UserSocialLinks.FirstOrDefault(x => x.UserId == entityId);
            }
        }

        public UserSocialLinks Add(UserSocialLinks socialLinkObj)
        {
            if (socialLinkObj == null) return null;

            using (var context = new GoTSkillZUserSocialLinksEntities(_connectionString))
            {
                context.UserSocialLinks.Add(socialLinkObj);
                context.SaveChanges();
            }

            return socialLinkObj;
        }

        public UserSocialLinks Update(UserSocialLinks socialLinkObj)
        {
            if (socialLinkObj == null) return null;

            using (var context = new GoTSkillZUserSocialLinksEntities(_connectionString))
            {
                var existingSocialLink = context.UserSocialLinks.FirstOrDefault(x => x.Id == socialLinkObj.Id);

                if (existingSocialLink == null) return socialLinkObj;

                existingSocialLink.Facebook = socialLinkObj.Facebook;
                existingSocialLink.Instagram = socialLinkObj.Instagram;
                existingSocialLink.Twitter = socialLinkObj.Twitter;
                existingSocialLink.Steam = socialLinkObj.Steam;
                existingSocialLink.YouTube = socialLinkObj.YouTube;
                existingSocialLink.Faceit = socialLinkObj.Faceit;
                existingSocialLink.SoStronk = socialLinkObj.SoStronk;
                existingSocialLink.Twitch = socialLinkObj.Twitch;
                existingSocialLink.Mixer = socialLinkObj.Mixer;
                existingSocialLink.Discord = socialLinkObj.Discord;
                context.SaveChanges();
            }

            return socialLinkObj;
        }

        public void Delete(int entityId)
        {
            throw new NotImplementedException();
        }
    }
}
