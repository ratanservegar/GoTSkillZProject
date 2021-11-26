using GoTSkillZ.Models.Membership.Data;
using GoTSkillZ.Models.Membership.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Linq.Expressions;

namespace GoTSkillZ.Models.Membership.Providers
{
    public class YouTubeChannelStatisticsProvider : IYouTubeChannelStatisticsProvider
    {
        private string _connectionString
        {
            get
            {
                var entityBuilder = new EntityConnectionStringBuilder
                {
                    Provider = "System.Data.SqlClient",
                    Metadata = @"res://*/Data.GoTSkillZYouTubeChannelStatisticsModel.csdl|res://*/Data.GoTSkillZYouTubeChannelStatisticsModel.ssdl|res://*/Data.GoTSkillZYouTubeChannelStatisticsModel.msl",
                    ProviderConnectionString = ConfigurationManager.ConnectionStrings["GoTSkillZConnectionString"].ConnectionString
                };

                return entityBuilder.ToString();
            }
        }

        public IEnumerable<YouTubeChannelStatistic> GetAll()
        {
            using (var context = new GoTSkillZYouTubeChannelStatisticsEntities(_connectionString))
            {
                return context.YouTubeChannelStatistics.ToList();
            }
        }

        public IEnumerable<YouTubeChannelStatistic> FindBy(Expression<Func<YouTubeChannelStatistic, bool>> predicate)
        {
            using (var context = new GoTSkillZYouTubeChannelStatisticsEntities(_connectionString))
            {
                return context.YouTubeChannelStatistics.Where(predicate).ToList();
            }
        }

        public YouTubeChannelStatistic Get(int entityId)
        {
            using (var context = new GoTSkillZYouTubeChannelStatisticsEntities(_connectionString))
            {
                return context.YouTubeChannelStatistics.FirstOrDefault(x => x.Id == entityId);
            }
        }

        public YouTubeChannelStatistic Add(YouTubeChannelStatistic youtubeStatObj)
        {
            if (youtubeStatObj == null) return null;

            using (var context = new GoTSkillZYouTubeChannelStatisticsEntities(_connectionString))
            {
                context.YouTubeChannelStatistics.Add(youtubeStatObj);
                context.SaveChanges();
            }

            return youtubeStatObj;
        }

        public YouTubeChannelStatistic Update(YouTubeChannelStatistic youtubeStatObj)
        {
            if (youtubeStatObj == null) return null;

            using (var context = new GoTSkillZYouTubeChannelStatisticsEntities(_connectionString))
            {
                var existingChannelStats = context.YouTubeChannelStatistics.FirstOrDefault(x => x.Id == 1);

                if (existingChannelStats == null) return youtubeStatObj;

                existingChannelStats.SubCount = youtubeStatObj.SubCount;
                existingChannelStats.ViewCount = youtubeStatObj.ViewCount;
                existingChannelStats.VideoCount = youtubeStatObj.VideoCount;
                existingChannelStats.CommentCount = youtubeStatObj.CommentCount;
                existingChannelStats.HiddenSubCount = youtubeStatObj.HiddenSubCount;

                context.SaveChanges();
            }

            return youtubeStatObj;


        }

        public void Delete(int entityId)
        {
            throw new NotImplementedException();
        }
    }
}