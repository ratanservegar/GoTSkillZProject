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
    public class GiveawayWinnersProvider:IGiveawayWinnersProvider
    {

        private string _connectionString
        {
            get
            {
                var entityBuilder = new EntityConnectionStringBuilder
                {
                    Provider = "System.Data.SqlClient",
                    Metadata = @"res://*/Data.GoTSkillZGiveawayWinnersModel.csdl|res://*/Data.GoTSkillZGiveawayWinnersModel.ssdl|res://*/Data.GoTSkillZGiveawayWinnersModel.msl",
                    ProviderConnectionString = ConfigurationManager.ConnectionStrings["GoTSkillZConnectionString"].ConnectionString
                };

                return entityBuilder.ToString();
            }
        }

        public IEnumerable<GiveawayWinner> GetAll()
        {
            using (var context = new GoTSkillZGiveawayWinnersEntities(_connectionString))
            {
                return context.GiveawayWinners.ToList();
            }
        }

        public IEnumerable<GiveawayWinner> FindBy(Expression<Func<GiveawayWinner, bool>> predicate)
        {
            using (var context = new GoTSkillZGiveawayWinnersEntities(_connectionString))
            {
                return context.GiveawayWinners.Where(predicate).ToList();
            }
        }

        public GiveawayWinner Get(int entityId)
        {
            using (var context = new GoTSkillZGiveawayWinnersEntities(_connectionString))
            {
                return context.GiveawayWinners.FirstOrDefault(x => x.Id == entityId);
            }
        }

        public GiveawayWinner Add(GiveawayWinner giveawayWinnerObj)
        {
            if (giveawayWinnerObj == null) return null;

            using (var context = new GoTSkillZGiveawayWinnersEntities(_connectionString))
            {
                context.GiveawayWinners.Add(giveawayWinnerObj);
                context.SaveChanges();
            }

            return giveawayWinnerObj;
        }

        public GiveawayWinner Update(GiveawayWinner giveawayWinnerObj)
        {
            if (giveawayWinnerObj == null) return null;

            using (var context = new GoTSkillZGiveawayWinnersEntities(_connectionString))
            {
                var existingGiveawayWinner = context.GiveawayWinners.FirstOrDefault(x => x.GiveawayId == giveawayWinnerObj.GiveawayId);

                if (existingGiveawayWinner == null) return giveawayWinnerObj;

                existingGiveawayWinner.UserId = giveawayWinnerObj.UserId;
                existingGiveawayWinner.WinnerImageUrl = giveawayWinnerObj.WinnerImageUrl;
                existingGiveawayWinner.CreatedDate = DateTime.Now;
                context.SaveChanges();
            }

            return giveawayWinnerObj;
        }

        public void Delete(int entityId)
        {
            throw new NotImplementedException();
        }
    }
}