using GoTSkillZ.Models.Game.Data;
using GoTSkillZ.Models.Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Linq.Expressions;

namespace GoTSkillZ.Models.Game.Provider
{
    public class GameTypesProvider : IGameTypesProvider
    {
        private string _connectionString
        {
            get
            {
                var entityBuilder = new EntityConnectionStringBuilder
                {
                    Provider = "System.Data.SqlClient",
                    Metadata = @"res://*/Data.GoTSkillZGameTypesModel.csdl|res://*/Data.GoTSkillZGameTypesModel.ssdl|res://*/Data.GoTSkillZGameTypesModel.msl",
                    ProviderConnectionString = ConfigurationManager.ConnectionStrings["GoTSkillZConnectionString"].ConnectionString
                };

                return entityBuilder.ToString();
            }
        }

        public IEnumerable<GameType> GetAll()
        {
            using (var context = new GoTSkillZGameTypeEntities(_connectionString))
            {
                return context.GameTypes.ToList();
            }
        }

        public IEnumerable<GameType> FindBy(Expression<Func<GameType, bool>> predicate)
        {
            using (var context = new GoTSkillZGameTypeEntities(_connectionString))
            {
                return context.GameTypes.Where(predicate).ToList();
            }
        }

        public GameType Get(int entityId)
        {
            using (var context = new GoTSkillZGameTypeEntities(_connectionString))
            {
                return context.GameTypes.FirstOrDefault(x => x.Id == entityId);
            }
        }

        public GameType Add(GameType gameType)
        {
            if (gameType == null) return null;

            using (var context = new GoTSkillZGameTypeEntities(_connectionString))
            {
                context.GameTypes.Add(gameType);
                context.SaveChanges();
            }

            return gameType;
        }

        public GameType Update(GameType gameType)
        {
            if (gameType == null) return null;

            using (var context = new GoTSkillZGameTypeEntities(_connectionString))
            {
                var existingGameType = context.GameTypes.FirstOrDefault(x => x.Id == gameType.Id);

                if (existingGameType == null) return gameType;

                existingGameType.GameName = gameType.GameName;
                context.SaveChanges();
            }

            return gameType;
        }

        public void Delete(int entityId)
        {
            throw new NotImplementedException();
        }
    }
}