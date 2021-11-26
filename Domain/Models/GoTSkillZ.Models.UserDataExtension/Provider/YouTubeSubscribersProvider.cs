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
    public class YouTubeSubscribersProvider : IYouTubeSubscribersProvider
    {
        private string _connectionString
        {
            get
            {
                var entityBuilder = new EntityConnectionStringBuilder
                {
                    Provider = "System.Data.SqlClient",
                    Metadata = @"res://*/Data.YouTubeSubscribersModel.csdl|res://*/Data.YouTubeSubscribersModel.ssdl|res://*/Data.YouTubeSubscribersModel.msl",
                    ProviderConnectionString = ConfigurationManager.ConnectionStrings["GoTSkillZConnectionString"].ConnectionString
                };

                return entityBuilder.ToString();
            }
        }


        public IEnumerable<YouTubeSubscribers> GetAll()
        {
            using (var context = new GoTSkillZYouTubeSubscribersEntities(_connectionString))
            {
                return context.YouTubeSubscribers.ToList();
            }
        }

        public IEnumerable<YouTubeSubscribers> FindBy(Expression<Func<YouTubeSubscribers, bool>> predicate)
        {
            using (var context = new GoTSkillZYouTubeSubscribersEntities(_connectionString))
            {
                return context.YouTubeSubscribers.Where(predicate).ToList();
            }
        }

        public YouTubeSubscribers Get(int entityId)
        {
            using (var context = new GoTSkillZYouTubeSubscribersEntities(_connectionString))
            {
                return context.YouTubeSubscribers.FirstOrDefault(x => x.Id == entityId);
            }
        }

        public YouTubeSubscribers Add(YouTubeSubscribers subObj)
        {
            if (subObj == null) return null;

            using (var context = new GoTSkillZYouTubeSubscribersEntities(_connectionString))
            {
                context.YouTubeSubscribers.Add(subObj);
                context.SaveChanges();
            }

            return subObj;
        }

        public YouTubeSubscribers Update(YouTubeSubscribers subOj)
        {
            if (subOj == null) return null;

            using (var context = new GoTSkillZYouTubeSubscribersEntities(_connectionString))
            {
                var existingSub = context.YouTubeSubscribers.FirstOrDefault(x => x.Id == subOj.Id);

                if (existingSub == null) return subOj;

                existingSub.IsSubscribed = subOj.IsSubscribed;
                existingSub.SubscribedDate = subOj.SubscribedDate;
                existingSub.UnSubscribedDate = subOj.UnSubscribedDate;
                context.SaveChanges();
            }

            return subOj;
        }

        public void Delete(int entityId)
        {
            throw new NotImplementedException();
        }
    }
}
