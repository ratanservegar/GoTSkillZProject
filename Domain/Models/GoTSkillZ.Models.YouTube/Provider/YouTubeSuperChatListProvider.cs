using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GoTSkillZ.Models.YouTube.Data;
using GoTSkillZ.Models.YouTube.Interfaces;

namespace GoTSkillZ.Models.YouTube.Provider
{
    public class YouTubeSuperChatListProvider: IYouTubeSuperChatListProvider
    {
        private string _connectionString
        {
            get
            {
                var entityBuilder = new EntityConnectionStringBuilder
                {
                    Provider = "System.Data.SqlClient",
                    Metadata = @"res://*/Data.YouTubeSuperChatListModel.csdl|res://*/Data.YouTubeSuperChatListModel.ssdl|res://*/Data.YouTubeSuperChatListModel.msl",
                    ProviderConnectionString = ConfigurationManager.ConnectionStrings["GoTSkillZConnectionString"].ConnectionString
                };

                return entityBuilder.ToString();
            }
        }

        public IEnumerable<YouTubeSuperChatList> GetAll()
        {
            using (var context = new GoTSkillZYouTubeSuperChatListEntities(_connectionString))
            {
                return context.YouTubeSuperChatLists.ToList();
            }
        }

        public IEnumerable<YouTubeSuperChatList> FindBy(Expression<Func<YouTubeSuperChatList, bool>> predicate)
        {
            using (var context = new GoTSkillZYouTubeSuperChatListEntities(_connectionString))
            {
                return context.YouTubeSuperChatLists.Where(predicate).ToList();
            }
        }

        public YouTubeSuperChatList Get(int entityId)
        {
            using (var context = new GoTSkillZYouTubeSuperChatListEntities(_connectionString))
            {
                return context.YouTubeSuperChatLists.FirstOrDefault(x => x.Id == entityId);
            }
        }

        public YouTubeSuperChatList Add(YouTubeSuperChatList superChatObj)
        {
            if (superChatObj == null) return null;

            using (var context = new GoTSkillZYouTubeSuperChatListEntities(_connectionString))
            {
                context.YouTubeSuperChatLists.Add(superChatObj);
                context.SaveChanges();
            }

            return superChatObj;
        }

        public YouTubeSuperChatList Update(YouTubeSuperChatList superChatObj)
        {
            if (superChatObj == null) return null;

            using (var context = new GoTSkillZYouTubeSuperChatListEntities(_connectionString))
            {
                var existingSuperChatObj = context.YouTubeSuperChatLists.FirstOrDefault(x => x.YouTubeSuperChatId == superChatObj.YouTubeSuperChatId);
                
                if (existingSuperChatObj == null) return superChatObj;

                existingSuperChatObj.Channeld = superChatObj.Channeld;
                existingSuperChatObj.ChannelUrl = superChatObj.ChannelUrl;
                existingSuperChatObj.DisplayName = superChatObj.DisplayName;
                existingSuperChatObj.CommentText = superChatObj.CommentText;
                existingSuperChatObj.AmountMicros = superChatObj.AmountMicros;
                existingSuperChatObj.ProfileImageUrl = superChatObj.ProfileImageUrl;
                existingSuperChatObj.MessageType = superChatObj.MessageType;
                existingSuperChatObj.IsSuperStickerEvent = superChatObj.IsSuperStickerEvent;
                existingSuperChatObj.StickerId = superChatObj.StickerId;
                existingSuperChatObj.altText = superChatObj.altText;
                existingSuperChatObj.ShowSuperChat = superChatObj.ShowSuperChat;
                existingSuperChatObj.CreatedDate = DateTime.Now;
                
                context.SaveChanges();
            }

            return superChatObj;
        }

        public void Delete(int entityId)
        {
            throw new NotImplementedException();
        }
    }
}
