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
    public class GamePlatformConnectivityProvider : IGamePlatformConnectivityProvider
    {

        private string _connectionString
        {
            get
            {
                var entityBuilder = new EntityConnectionStringBuilder
                {
                    Provider = "System.Data.SqlClient",
                    Metadata = @"res://*/Data.GoTSkillZGamePlatformConnectivityModel.csdl|res://*/Data.GoTSkillZGamePlatformConnectivityModel.ssdl|res://*/Data.GoTSkillZGamePlatformConnectivityModel.msl",
                    ProviderConnectionString = ConfigurationManager.ConnectionStrings["GoTSkillZConnectionString"].ConnectionString
                };

                return entityBuilder.ToString();
            }
        }
        public IEnumerable<GamePlatformConnectivity> GetAll()
        {
            using (var context = new GoTSkillZGamePlatformConnectivityEntities(_connectionString))
            {
                return context.GamePlatformConnectivity.ToList();
            }
        }

        public IEnumerable<GamePlatformConnectivity> FindBy(Expression<Func<GamePlatformConnectivity, bool>> predicate)
        {
            using (var context = new GoTSkillZGamePlatformConnectivityEntities(_connectionString))
            {
                return context.GamePlatformConnectivity.Where(predicate).ToList();
            }
        }

        public GamePlatformConnectivity Get(int entityId)
        {
            using (var context = new GoTSkillZGamePlatformConnectivityEntities(_connectionString))
            {
                return context.GamePlatformConnectivity.FirstOrDefault(x => x.Id == entityId);
            }
        }

        public GamePlatformConnectivity Add(GamePlatformConnectivity gamePlatformConnectivity)
        {
            if (gamePlatformConnectivity == null) return null;

            using (var context = new GoTSkillZGamePlatformConnectivityEntities(_connectionString))
            {
                context.GamePlatformConnectivity.Add(gamePlatformConnectivity);
                context.SaveChanges();
            }

            return gamePlatformConnectivity;
        }

        public GamePlatformConnectivity Update(GamePlatformConnectivity gamePlatformConnectivity)
        {
            if (gamePlatformConnectivity == null) return null;

            using (var context = new GoTSkillZGamePlatformConnectivityEntities(_connectionString))
            {
                var existingGameData = context.GamePlatformConnectivity.FirstOrDefault(x => x.UserId == gamePlatformConnectivity.UserId);

                if (existingGameData == null) return gamePlatformConnectivity;

                existingGameData.ESEA = gamePlatformConnectivity.ESEA;
                existingGameData.FaceitId = gamePlatformConnectivity.FaceitId;
                existingGameData.SteamId = gamePlatformConnectivity.SteamId;
                existingGameData.SteamId64 = gamePlatformConnectivity.SteamId64;
                existingGameData.ESLId = gamePlatformConnectivity.ESLId;
                existingGameData.SoStronkId = gamePlatformConnectivity.SoStronkId;
                context.SaveChanges();
            }

            return gamePlatformConnectivity;
        }

        public void Delete(int entityId)
        {
            throw new NotImplementedException();
        }
    }
}
