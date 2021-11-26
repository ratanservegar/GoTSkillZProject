using GoTSkillZ.Models.PMS.Data;
using GoTSkillZ.Models.PMS.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Linq.Expressions;

namespace GoTSkillZ.Models.PMS.Providers
{
    public class LoadingMessagesProvider : ILoadingMessagesProvider
    {
        private string _connectionString
        {
            get
            {
                var entityBuilder = new EntityConnectionStringBuilder
                {
                    Provider = "System.Data.SqlClient",
                    Metadata = @"res://*/Data.LoadingMessagesModel.csdl|res://*/Data.LoadingMessagesModel.ssdl|res://*/Data.LoadingMessagesModel.msl",
                    ProviderConnectionString = ConfigurationManager.ConnectionStrings["GoTSkillZConnectionString"].ConnectionString
                };

                return entityBuilder.ToString();
            }
        }

        public IEnumerable<LoadingMessages> GetAll()
        {
            using (var context = new GoTSkillZLoadingMessagesEntities(_connectionString))
            {
                return context.LoadingMessages.ToList();
            }
        }

        public IEnumerable<LoadingMessages> FindBy(Expression<Func<LoadingMessages, bool>> predicate)
        {
            using (var context = new GoTSkillZLoadingMessagesEntities(_connectionString))
            {
                return context.LoadingMessages.Where(predicate).ToList();
            }
        }

        public LoadingMessages Get(int entityId)
        {
            using (var context = new GoTSkillZLoadingMessagesEntities(_connectionString))
            {
                return context.LoadingMessages.FirstOrDefault(x => x.Id == entityId);
            }
        }

        public LoadingMessages Add(LoadingMessages loadingMessageobj)
        {
            if (loadingMessageobj == null) return null;

            using (var context = new GoTSkillZLoadingMessagesEntities(_connectionString))
            {
                context.LoadingMessages.Add(loadingMessageobj);
                context.SaveChanges();
            }

            return loadingMessageobj;
        }

        public LoadingMessages Update(LoadingMessages loadingMessageobj)
        {
            if (loadingMessageobj == null) return null;

            using (var context = new GoTSkillZLoadingMessagesEntities(_connectionString))
            {
                var existinloadingMessage = context.LoadingMessages.FirstOrDefault(x => x.Id == loadingMessageobj.Id);

                if (existinloadingMessage == null) return loadingMessageobj;

                existinloadingMessage.TagLine = loadingMessageobj.TagLine;
                existinloadingMessage.Author = loadingMessageobj.Author;
                existinloadingMessage.Active = loadingMessageobj.Active;
                context.SaveChanges();
            }

            return loadingMessageobj;
        }

        public void Delete(int entityId)
        {
            if (entityId < 1) return;

            using (var context = new GoTSkillZLoadingMessagesEntities(_connectionString))
            {
                var existingLoading = context.LoadingMessages.FirstOrDefault(x => x.Id == entityId);

                if (existingLoading == null) return;

                existingLoading.Active = false;
                context.SaveChanges();
            }
        }
    }
}
