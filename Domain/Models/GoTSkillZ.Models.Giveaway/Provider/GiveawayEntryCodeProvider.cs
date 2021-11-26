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
    public class GiveawayEntryCodeProvider:IGiveawayEntryCodeProvider
    {
        private string _connectionString
        {
            get
            {
                var entityBuilder = new EntityConnectionStringBuilder
                {
                    Provider = "System.Data.SqlClient",
                    Metadata = @"res://*/Data.GiveawayEntryCodeModel.csdl|res://*/Data.GiveawayEntryCodeModel.ssdl|res://*/Data.GiveawayEntryCodeModel.msl",
                    ProviderConnectionString = ConfigurationManager.ConnectionStrings["GoTSkillZConnectionString"].ConnectionString
                };

                return entityBuilder.ToString();
            }
        }

        public IEnumerable<GiveawayEntryCode> GetAll()
        {
            using (var context = new GiveawayEntryCodeEntities(_connectionString))
            {
                return context.GiveawayEntryCodes.ToList();
            }
        }

        public IEnumerable<GiveawayEntryCode> FindBy(Expression<Func<GiveawayEntryCode, bool>> predicate)
        {
            using (var context = new GiveawayEntryCodeEntities(_connectionString))
            {
                return context.GiveawayEntryCodes.Where(predicate).ToList();
            }
        }

        public GiveawayEntryCode Get(int entityId)
        {
            using (var context = new GiveawayEntryCodeEntities(_connectionString))
            {
                return context.GiveawayEntryCodes.FirstOrDefault(x => x.Id == entityId);
            }
        }

        public GiveawayEntryCode Add(GiveawayEntryCode giveawayEntryCode)
        {
            if (giveawayEntryCode == null) return null;

            using (var context = new GiveawayEntryCodeEntities(_connectionString))
            {
                context.GiveawayEntryCodes.Add(giveawayEntryCode);
                context.SaveChanges();
            }

            return giveawayEntryCode;
        }

        public GiveawayEntryCode Update(GiveawayEntryCode giveawayEntryCodeObject)
        {
            if (giveawayEntryCodeObject == null) return null;

            using (var context = new GiveawayEntryCodeEntities(_connectionString))
            {
                var existingEntry = context.GiveawayEntryCodes.FirstOrDefault(x => x.UserId == giveawayEntryCodeObject.UserId && x.GiveawayId == giveawayEntryCodeObject.GiveawayId);

                if (existingEntry == null) return giveawayEntryCodeObject;

                existingEntry.GiveawayCode = giveawayEntryCodeObject.GiveawayCode;
                context.SaveChanges();
            }

            return giveawayEntryCodeObject;
        }

        public void Delete(int entityId)
        {
            throw new NotImplementedException();
        }
    }
}