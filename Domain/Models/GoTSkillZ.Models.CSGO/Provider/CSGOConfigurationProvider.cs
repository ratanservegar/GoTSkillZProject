using GoTSkillZ.Models.CSGO.Data;
using GoTSkillZ.Models.CSGO.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Linq.Expressions;

namespace GoTSkillZ.Models.CSGO.Provider
{
    public class CSGOConfigurationProvider : ICSGOVideoConfigurationProvider
    {

        private string _connectionString
        {
            get
            {
                var entityBuilder = new EntityConnectionStringBuilder
                {
                    Provider = "System.Data.SqlClient",
                    Metadata = @"res://*/Data.GoTSkillZCSGOVideoConfigurationModel.csdl|res://*/Data.GoTSkillZCSGOVideoConfigurationModel.ssdl|res://*/Data.GoTSkillZCSGOVideoConfigurationModel.msl",
                    ProviderConnectionString = ConfigurationManager.ConnectionStrings["GoTSkillZConnectionString"].ConnectionString
                };

                return entityBuilder.ToString();
            }
        }

        public IEnumerable<CSGOVideoConfiguration> GetAll()
        {
            using (var context = new GoTSkillZCSGOVideoConfigurationEntities(_connectionString))
            {
                return context.CSGOVideoConfigurations.ToList();
            }
        }

        public IEnumerable<CSGOVideoConfiguration> FindBy(Expression<Func<CSGOVideoConfiguration, bool>> predicate)
        {
            using (var context = new GoTSkillZCSGOVideoConfigurationEntities(_connectionString))
            {
                return context.CSGOVideoConfigurations.Where(predicate).ToList();
            }
        }

        public CSGOVideoConfiguration Get(int entityId)
        {
            using (var context = new GoTSkillZCSGOVideoConfigurationEntities(_connectionString))
            {
                return context.CSGOVideoConfigurations.FirstOrDefault(x => x.Id == entityId);
            }
        }

        public CSGOVideoConfiguration Add(CSGOVideoConfiguration csgoVideoConfiguration)
        {
            if (csgoVideoConfiguration == null) return null;

            using (var context = new GoTSkillZCSGOVideoConfigurationEntities(_connectionString))
            {
                context.CSGOVideoConfigurations.Add(csgoVideoConfiguration);
                context.SaveChanges();
            }

            return csgoVideoConfiguration;
        }

        public CSGOVideoConfiguration Update(CSGOVideoConfiguration csgoVideoConfiguration)
        {
            if (csgoVideoConfiguration == null) return null;

            using (var context = new GoTSkillZCSGOVideoConfigurationEntities(_connectionString))
            {
                var existingVideoConfig = context.CSGOVideoConfigurations.FirstOrDefault(x => x.UserId == csgoVideoConfiguration.UserId);

                if (existingVideoConfig == null) return csgoVideoConfiguration;

                existingVideoConfig.ColorMode = csgoVideoConfiguration.ColorMode;
                existingVideoConfig.Brightness = csgoVideoConfiguration.Brightness;
                existingVideoConfig.AspectRatio = csgoVideoConfiguration.AspectRatio;
                existingVideoConfig.Resolution = csgoVideoConfiguration.Resolution;
                existingVideoConfig.DisplayMode = csgoVideoConfiguration.DisplayMode;
                existingVideoConfig.LaptopPowerSavings = csgoVideoConfiguration.LaptopPowerSavings;
                existingVideoConfig.GlobalShadowQuality = csgoVideoConfiguration.GlobalShadowQuality;
                existingVideoConfig.ModelTextureDetail = csgoVideoConfiguration.ModelTextureDetail;
                existingVideoConfig.EffectDetail = csgoVideoConfiguration.EffectDetail;
                existingVideoConfig.ShaderDetail = csgoVideoConfiguration.ShaderDetail;
                existingVideoConfig.MultiCoreRendering = csgoVideoConfiguration.MultiCoreRendering;
                existingVideoConfig.MultisamplingAntiAliasingMode = csgoVideoConfiguration.MultisamplingAntiAliasingMode;
                existingVideoConfig.FXXAAAnti_Aliasing = csgoVideoConfiguration.FXXAAAnti_Aliasing;
                existingVideoConfig.TextureFilteringMode = csgoVideoConfiguration.TextureFilteringMode;
                existingVideoConfig.WaitForVerticalSync = csgoVideoConfiguration.WaitForVerticalSync;
                existingVideoConfig.MotionBlur = csgoVideoConfiguration.MotionBlur;
                existingVideoConfig.TripleMonitorMode = csgoVideoConfiguration.TripleMonitorMode;
                existingVideoConfig.GameView = csgoVideoConfiguration.GameView;
                existingVideoConfig.ModifiedDate = DateTime.Now;
                context.SaveChanges();
            }

            return csgoVideoConfiguration;
        }

        public void Delete(int entityId)
        {
            throw new NotImplementedException();
        }


        public string DeleteCSGOVideoConfiguration(int userId)
        {
            var returnString = "";
            try
            {
                using (var context = new GoTSkillZCSGOVideoConfigurationEntities(_connectionString))
                {
                    context.Database.ExecuteSqlCommand("DELETE  FROM CSGOVideoConfiguration WHERE UserId IN (" + userId + ")");
                    returnString = "success";
                }
            }
            catch (Exception)
            {

                returnString = "failed";
            }

            return returnString;
        }
    }
}