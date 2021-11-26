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
    public class YouTubeLiveStreamProvider : IYouTubeLiveStreamProvider
    {
        private string _connectionString
        {
            get
            {
                var entityBuilder = new EntityConnectionStringBuilder
                {
                    Provider = "System.Data.SqlClient",
                    Metadata = @"res://*/Data.YouTubeLiveStreamModel.csdl|res://*/Data.YouTubeLiveStreamModel.ssdl|res://*/Data.YouTubeLiveStreamModel.msl",
                    ProviderConnectionString = ConfigurationManager.ConnectionStrings["GoTSkillZConnectionString"].ConnectionString
                };

                return entityBuilder.ToString();
            }
        }

        public IEnumerable<YouTubeLiveStream> GetAll()
        {
            using (var context = new GoTSkillZYouTubeLiveStreamEntities(_connectionString))
            {
                return context.YouTubeLiveStreams.ToList();
            }
        }

        public IEnumerable<YouTubeLiveStream> FindBy(Expression<Func<YouTubeLiveStream, bool>> predicate)
        {
            using (var context = new GoTSkillZYouTubeLiveStreamEntities(_connectionString))
            {
                return context.YouTubeLiveStreams.Where(predicate).ToList();
            }
        }

        public YouTubeLiveStream Get(int entityId)
        {
            using (var context = new GoTSkillZYouTubeLiveStreamEntities(_connectionString))
            {
                return context.YouTubeLiveStreams.FirstOrDefault(x => x.Id == entityId);
            }
        }

        public YouTubeLiveStream Add(YouTubeLiveStream liveStreamObj)
        {
            if (liveStreamObj == null) return null;

            using (var context = new GoTSkillZYouTubeLiveStreamEntities(_connectionString))
            {
                context.YouTubeLiveStreams.Add(liveStreamObj);
                context.SaveChanges();
            }

            return liveStreamObj;
        }

        public YouTubeLiveStream Update(YouTubeLiveStream liveStreamObj)
        {
            if (liveStreamObj == null) return null;

            using (var context = new GoTSkillZYouTubeLiveStreamEntities(_connectionString))
            {
                var existingYouTubeLivestreamObj = context.YouTubeLiveStreams.FirstOrDefault(x => x.Id == liveStreamObj.Id);

                if (existingYouTubeLivestreamObj == null) return liveStreamObj;

                existingYouTubeLivestreamObj.EmbedHTML = liveStreamObj.EmbedHTML;
                existingYouTubeLivestreamObj.LiveChatId = liveStreamObj.LiveChatId;
                existingYouTubeLivestreamObj.StreamTitle = liveStreamObj.StreamTitle;
                existingYouTubeLivestreamObj.VideoId = liveStreamObj.VideoId;
                existingYouTubeLivestreamObj.IsLive = liveStreamObj.IsLive;

                context.SaveChanges();
            }

            return liveStreamObj;
        }

        public void Delete(int entityId)
        {
            throw new NotImplementedException();
        }
    }
}
