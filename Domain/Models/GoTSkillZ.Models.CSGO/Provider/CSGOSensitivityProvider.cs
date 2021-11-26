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
    public class CSGOSensitivityProvider : ICSGOSensitivityProvider
    {
        private string _connectionString
        {
            get
            {
                var entityBuilder = new EntityConnectionStringBuilder
                {
                    Provider = "System.Data.SqlClient",
                    Metadata = @"res://*/Data.GoTSkillZCSGOSensitivityModel.csdl|res://*/Data.GoTSkillZCSGOSensitivityModel.ssdl|res://*/Data.GoTSkillZCSGOSensitivityModel.msl",
                    ProviderConnectionString = ConfigurationManager.ConnectionStrings["GoTSkillZConnectionString"].ConnectionString
                };

                return entityBuilder.ToString();
            }
        }


        public IEnumerable<CSGOSensitivity> GetAll()
        {
            using (var context = new GoTSkillZCSGOSensitivityEntities(_connectionString))
            {
                return context.CSGOSensitivities.ToList();
            }
        }

        public IEnumerable<CSGOSensitivity> FindBy(Expression<Func<CSGOSensitivity, bool>> predicate)
        {
            using (var context = new GoTSkillZCSGOSensitivityEntities(_connectionString))
            {
                return context.CSGOSensitivities.Where(predicate).ToList();
            }
        }

        public CSGOSensitivity Get(int entityId)
        {
            using (var context = new GoTSkillZCSGOSensitivityEntities(_connectionString))
            {
                return context.CSGOSensitivities.FirstOrDefault(x => x.Id == entityId);
            }
        }

        public CSGOSensitivity Add(CSGOSensitivity csgoSensitivity)
        {
            if (csgoSensitivity == null) return null;

            using (var context = new GoTSkillZCSGOSensitivityEntities(_connectionString))
            {
                context.CSGOSensitivities.Add(csgoSensitivity);
                context.SaveChanges();
            }

            return csgoSensitivity;
        }

        public CSGOSensitivity Update(CSGOSensitivity csgoSensitivity)
        {
            if (csgoSensitivity == null) return null;

            using (var context = new GoTSkillZCSGOSensitivityEntities(_connectionString))
            {
                var existingSensi = context.CSGOSensitivities.FirstOrDefault(x => x.UserId == csgoSensitivity.UserId && x.Active);

                if (existingSensi == null) return csgoSensitivity;

                existingSensi.Sensitivity = csgoSensitivity.Sensitivity;
                existingSensi.DPI = csgoSensitivity.DPI;
                existingSensi.eDPI = csgoSensitivity.eDPI;
                existingSensi.RawInput = csgoSensitivity.RawInput;
                existingSensi.WindowsSensitivity = csgoSensitivity.WindowsSensitivity;
                existingSensi.MouseHz = csgoSensitivity.MouseHz;
                existingSensi.Active = csgoSensitivity.Active;
                existingSensi.EndUnixDatetime = csgoSensitivity.EndUnixDatetime;
                existingSensi.EndDate = csgoSensitivity.EndDate;


                context.SaveChanges();
            }

            return csgoSensitivity;
        }

        public void Delete(int entityId)
        {
            throw new NotImplementedException();
        }
    }
}