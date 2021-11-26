using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Linq.Expressions;
using GoTSkillZ.Models.Giveaway.Data;
using GoTSkillZ.Models.Giveaway.Interfaces;

namespace GoTSkillZ.Models.Giveaway.Provider
{
    public class GiveawayProvider:IGiveawayProvider
    {

        private string _connectionString
        {
            get
            {
                var entityBuilder = new EntityConnectionStringBuilder
                {
                    Provider = "System.Data.SqlClient",
                    Metadata = @"res://*/Data.GoTSkillZGiveawayModel.csdl|res://*/Data.GoTSkillZGiveawayModel.ssdl|res://*/Data.GoTSkillZGiveawayModel.msl",
                    ProviderConnectionString = ConfigurationManager.ConnectionStrings["GoTSkillZConnectionString"].ConnectionString
                };

                return entityBuilder.ToString();
            }
        }

        public IEnumerable<Data.Giveaway> GetAll()
        {
            using (var context = new GoTSkillZGiveawayEntities(_connectionString))
            {
                return context.Giveaways.ToList();
            }
        }

        public IEnumerable<Data.Giveaway> FindBy(Expression<Func<Data.Giveaway, bool>> predicate)
        {
            using (var context = new GoTSkillZGiveawayEntities(_connectionString))
            {
                return context.Giveaways.Where(predicate).ToList();
            }
        }

        public Data.Giveaway Get(int entityId)
        {
            using (var context = new GoTSkillZGiveawayEntities(_connectionString))
            {
                return context.Giveaways.FirstOrDefault(x => x.Id == entityId);
            }
        }

        public Data.Giveaway Add(Data.Giveaway giveawayObj)
        {
            if (giveawayObj == null) return null;

            using (var context = new GoTSkillZGiveawayEntities(_connectionString))
            {
                context.Giveaways.Add(giveawayObj);
                context.SaveChanges();
            }

            return giveawayObj;
        }

        public Data.Giveaway Update(Data.Giveaway giveawayObj)
        {
            if (giveawayObj == null) return null;

            using (var context = new GoTSkillZGiveawayEntities(_connectionString))
            {
                var existingGiveaway = context.Giveaways.FirstOrDefault(x => x.Id == giveawayObj.Id);

                if (existingGiveaway == null) return giveawayObj;

                existingGiveaway.Title = giveawayObj.Title;
                existingGiveaway.Code = giveawayObj.Code;
                existingGiveaway.Description = giveawayObj.Description;
                existingGiveaway.Rules = giveawayObj.Rules;
                existingGiveaway.Active = giveawayObj.Active;
                existingGiveaway.ImageUrl = giveawayObj.ImageUrl;
                existingGiveaway.Sponsored = giveawayObj.Sponsored;
                existingGiveaway.VideoUrl = giveawayObj.VideoUrl;
                existingGiveaway.International = giveawayObj.International;
                existingGiveaway.CreatedDate = giveawayObj.CreatedDate;
                existingGiveaway.ModifiedDate = DateTime.Now;
                existingGiveaway.TotalEntries = giveawayObj.TotalEntries;
                context.SaveChanges();
            }

            return giveawayObj;
        }

        public void Delete(int entityId)
        {
            throw new NotImplementedException();
        }
    }
}