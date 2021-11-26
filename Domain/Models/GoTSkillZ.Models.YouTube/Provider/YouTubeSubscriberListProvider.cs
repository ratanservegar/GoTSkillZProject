using GoTSkillZ.Models.YouTube.Data;
using GoTSkillZ.Models.YouTube.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Linq.Expressions;


namespace GoTSkillZ.Models.YouTube.Provider
{
    public class YouTubeSubscriberListProvider : IYouTubeSubscriberListProvider
    {
        private string _connectionString
        {
            get
            {
                var entityBuilder = new EntityConnectionStringBuilder
                {
                    Provider = "System.Data.SqlClient",
                    Metadata = @"res://*/Data.GoTSkillZYouTubeSubscriberListModel.csdl|res://*/Data.GoTSkillZYouTubeSubscriberListModel.ssdl|res://*/Data.GoTSkillZYouTubeSubscriberListModel.msl",
                    ProviderConnectionString = ConfigurationManager.ConnectionStrings["GoTSkillZConnectionString"].ConnectionString
                };

                return entityBuilder.ToString();
            }
        }

        public IEnumerable<YouTubeSubscriberList> GetAll()
        {
            using (var context = new GoTSkillZYouTubeSubscriberListEntities(_connectionString))
            {
                return context.YouTubeSubscriberList.ToList();
            }
        }

        public IEnumerable<YouTubeSubscriberList> FindBy(Expression<Func<YouTubeSubscriberList, bool>> predicate)
        {
            using (var context = new GoTSkillZYouTubeSubscriberListEntities(_connectionString))
            {
                return context.YouTubeSubscriberList.Where(predicate).ToList();
            }
        }

        public YouTubeSubscriberList Get(int entityId)
        {
            using (var context = new GoTSkillZYouTubeSubscriberListEntities(_connectionString))
            {
                return context.YouTubeSubscriberList.FirstOrDefault(x => x.Id == entityId);
            }
        }

        public YouTubeSubscriberList Add(YouTubeSubscriberList youtubeSubObj)
        {
            if (youtubeSubObj == null) return null;

            using (var context = new GoTSkillZYouTubeSubscriberListEntities(_connectionString))
            {
                context.YouTubeSubscriberList.Add(youtubeSubObj);
                context.SaveChanges();
            }

            return youtubeSubObj;
        }

        public YouTubeSubscriberList Update(YouTubeSubscriberList T)
        {
            throw new NotImplementedException();
        }

        public void Delete(int entityId)
        {
            throw new NotImplementedException();
        }
    }
}