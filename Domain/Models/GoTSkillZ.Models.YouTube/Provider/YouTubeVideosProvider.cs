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
    public class YouTubeVideosProvider : IYouTubeVideosProvider
    {
        private string _connectionString
        {
            get
            {
                var entityBuilder = new EntityConnectionStringBuilder
                {
                    Provider = "System.Data.SqlClient",
                    Metadata = @"res://*/Data.YouTubeVideosModel.csdl|res://*/Data.YouTubeVideosModel.ssdl|res://*/Data.YouTubeVideosModel.msl",
                    ProviderConnectionString = ConfigurationManager.ConnectionStrings["GoTSkillZConnectionString"].ConnectionString
                };

                return entityBuilder.ToString();
            }
        }


        public IEnumerable<YouTubeVideo> GetAll()
        {
            using (var context = new GoTSkillZYouTubeVideosEntities(_connectionString))
            {
                return context.YouTubeVideos.ToList();
            }
        }

        public IEnumerable<YouTubeVideo> FindBy(Expression<Func<YouTubeVideo, bool>> predicate)
        {
            using (var context = new GoTSkillZYouTubeVideosEntities(_connectionString))
            {
                return context.YouTubeVideos.Where(predicate).ToList();
            }
        }

        public YouTubeVideo Get(int entityId)
        {
            using (var context = new GoTSkillZYouTubeVideosEntities(_connectionString))
            {
                return context.YouTubeVideos.FirstOrDefault(x => x.Id == entityId);
            }
        }

        public YouTubeVideo Add(YouTubeVideo youTubeVideoObj)
        {
            if (youTubeVideoObj == null) return null;

            using (var context = new GoTSkillZYouTubeVideosEntities(_connectionString))
            {
                context.YouTubeVideos.Add(youTubeVideoObj);
                context.SaveChanges();
            }

            return youTubeVideoObj;
        }

        public YouTubeVideo Update(YouTubeVideo youTubeVideoObj)
        {
            if (youTubeVideoObj == null) return null;

            using (var context = new GoTSkillZYouTubeVideosEntities(_connectionString))
            {
                var existingYouTubeVideoObj = context.YouTubeVideos.FirstOrDefault(x => x.Id == youTubeVideoObj.Id);

                if (existingYouTubeVideoObj == null) return youTubeVideoObj;


                existingYouTubeVideoObj.ChannelId = youTubeVideoObj.ChannelId;
                existingYouTubeVideoObj.PlaylistId = youTubeVideoObj.PlaylistId;
                existingYouTubeVideoObj.VideoId = youTubeVideoObj.VideoId;
                existingYouTubeVideoObj.VideoTitle = youTubeVideoObj.VideoTitle;
                existingYouTubeVideoObj.VideoDescription = youTubeVideoObj.VideoDescription;
                existingYouTubeVideoObj.VideoCreatedDate = youTubeVideoObj.VideoCreatedDate;
                existingYouTubeVideoObj.Standardthumbnail = youTubeVideoObj.Standardthumbnail;
                existingYouTubeVideoObj.MediumThumbnail = youTubeVideoObj.MediumThumbnail;
                existingYouTubeVideoObj.DefaultThumbnail = youTubeVideoObj.DefaultThumbnail;
                existingYouTubeVideoObj.HighThumbnail = youTubeVideoObj.HighThumbnail;
                existingYouTubeVideoObj.MaxThumbnail = youTubeVideoObj.MaxThumbnail;
                existingYouTubeVideoObj.IsDisplayed = youTubeVideoObj.IsDisplayed;
                existingYouTubeVideoObj.CreatedDate = DateTime.Now;

                context.SaveChanges();
            }

            return youTubeVideoObj;
        }

        public void Delete(int entityId)
        {
            throw new NotImplementedException();
        }
    }
}