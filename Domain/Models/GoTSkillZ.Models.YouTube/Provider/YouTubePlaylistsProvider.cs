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
    public class YouTubePlaylistsProvider : IYouTubePlaylistsProvider
    {

        private string _connectionString
        {
            get
            {
                var entityBuilder = new EntityConnectionStringBuilder
                {
                    Provider = "System.Data.SqlClient",
                    Metadata = @"res://*/Data.YouTubePlaylistsModel.csdl|res://*/Data.YouTubePlaylistsModel.ssdl|res://*/Data.YouTubePlaylistsModel.msl",
                    ProviderConnectionString = ConfigurationManager.ConnectionStrings["GoTSkillZConnectionString"].ConnectionString
                };

                return entityBuilder.ToString();
            }
        }


        public IEnumerable<YouTubePlaylist> GetAll()
        {
            using (var context = new GoTSkillZYouTubePlaylistsEntities(_connectionString))
            {
                return context.YouTubePlaylists.ToList();
            }
        }

        public IEnumerable<YouTubePlaylist> FindBy(Expression<Func<YouTubePlaylist, bool>> predicate)
        {
            using (var context = new GoTSkillZYouTubePlaylistsEntities(_connectionString))
            {
                return context.YouTubePlaylists.Where(predicate).ToList();
            }
        }

        public YouTubePlaylist Get(int entityId)
        {
            using (var context = new GoTSkillZYouTubePlaylistsEntities(_connectionString))
            {
                return context.YouTubePlaylists.FirstOrDefault(x => x.Id == entityId);
            }
        }

        public YouTubePlaylist Add(YouTubePlaylist playListObj)
        {
            if (playListObj == null) return null;

            using (var context = new GoTSkillZYouTubePlaylistsEntities(_connectionString))
            {
                context.YouTubePlaylists.Add(playListObj);
                context.SaveChanges();
            }

            return playListObj;
        }

        public YouTubePlaylist Update(YouTubePlaylist playListObj)
        {
            if (playListObj == null) return null;

            using (var context = new GoTSkillZYouTubePlaylistsEntities(_connectionString))
            {
                var existingYouTubePlayListObj = context.YouTubePlaylists.FirstOrDefault(x => x.Id == playListObj.Id);

                if (existingYouTubePlayListObj == null) return playListObj;

                existingYouTubePlayListObj.ChannelId = playListObj.ChannelId;
                existingYouTubePlayListObj.PlayListDescription = playListObj.PlayListDescription;
                existingYouTubePlayListObj.PlaylistTitle = playListObj.PlaylistTitle;
                existingYouTubePlayListObj.PlaylistActive = playListObj.PlaylistActive;
                existingYouTubePlayListObj.PlaylistCreatedDate = playListObj.PlaylistCreatedDate;
                existingYouTubePlayListObj.CustomThumbail = playListObj.CustomThumbail;
                existingYouTubePlayListObj.DefaultThumbnail = playListObj.DefaultThumbnail;
                existingYouTubePlayListObj.MediumThumbnail = playListObj.MediumThumbnail;
                existingYouTubePlayListObj.HighThumbnail = playListObj.HighThumbnail;
                existingYouTubePlayListObj.MaxThumbnail = playListObj.MaxThumbnail;
                existingYouTubePlayListObj.PlaylistItemCount = playListObj.PlaylistItemCount;
                existingYouTubePlayListObj.CreatedDate = DateTime.Now;

                context.SaveChanges();
            }

            return playListObj;
        }

        public void Delete(int entityId)
        {
            throw new NotImplementedException();
        }
    }
}