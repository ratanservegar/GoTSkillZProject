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
    public class GameRolesProvider : IGameRolesProvider
    {
        private string _connectionString
        {
            get
            {
                var entityBuilder = new EntityConnectionStringBuilder
                {
                    Provider = "System.Data.SqlClient",
                    Metadata = @"res://*/Data.GoTSkillZGameRolesModel.csdl|res://*/Data.GoTSkillZGameRolesModel.ssdl|res://*/Data.GoTSkillZGameRolesModel.msl",
                    ProviderConnectionString = ConfigurationManager.ConnectionStrings["GoTSkillZConnectionString"].ConnectionString
                };

                return entityBuilder.ToString();
            }
        }


        public IEnumerable<GameRoles> GetAll()
        {
            using (var context = new GoTSkillZGameRoleEntities(_connectionString))
            {
                return context.GameRoles.ToList();
            }
        }

        public IEnumerable<GameRoles> FindBy(Expression<Func<GameRoles, bool>> predicate)
        {
            using (var context = new GoTSkillZGameRoleEntities(_connectionString))
            {
                return context.GameRoles.Where(predicate).ToList();
            }
        }

        public GameRoles Get(int entityId)
        {
            using (var context = new GoTSkillZGameRoleEntities(_connectionString))
            {
                return context.GameRoles.FirstOrDefault(x => x.Id == entityId);
            }
        }

        public GameRoles Add(GameRoles gameRoles)
        {
            if (gameRoles == null) return null;

            using (var context = new GoTSkillZGameRoleEntities(_connectionString))
            {
                context.GameRoles.Add(gameRoles);
                context.SaveChanges();
            }

            return gameRoles;
        }

        public GameRoles Update(GameRoles gameRole)
        {
            if (gameRole == null) return null;

            using (var context = new GoTSkillZGameRoleEntities(_connectionString))
            {
                var existingGameRole = context.GameRoles.FirstOrDefault(x => x.Id == gameRole.Id);

                if (existingGameRole == null) return gameRole;

                existingGameRole.RoleName = gameRole.RoleName;
                existingGameRole.GameTypeId = gameRole.GameTypeId;
                context.SaveChanges();
            }

            return gameRole;
        }

        public void Delete(int entityId)
        {
            throw new NotImplementedException();
        }
    }
}