using GoTSkillZ.Models.Partner.Data;
using GoTSkillZ.Models.Partner.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Linq.Expressions;

namespace GoTSkillZ.Models.Partner.Providers
{
    public class CompaniesProvider : ICompaniesProvider
    {
        private string _connectionString
        {
            get
            {
                var entityBuilder = new EntityConnectionStringBuilder
                {
                    Provider = "System.Data.SqlClient",
                    Metadata = @"res://*/Data.GoTSkillZCompaniesModel.csdl|res://*/Data.GoTSkillZCompaniesModel.ssdl|res://*/Data.GoTSkillZCompaniesModel.msl",
                    ProviderConnectionString = ConfigurationManager.ConnectionStrings["GoTSkillZConnectionString"].ConnectionString
                };

                return entityBuilder.ToString();
            }
        }

        public IEnumerable<Companies> GetAll()
        {
            using (var context = new GoTSkillZCompaniesEntities(_connectionString))
            {
                return context.Companies.ToList();
            }
        }

        public IEnumerable<Companies> FindBy(Expression<Func<Companies, bool>> predicate)
        {
            using (var context = new GoTSkillZCompaniesEntities(_connectionString))
            {
                return context.Companies.Where(predicate).ToList();
            }
        }

        public Companies Get(int entityId)
        {
            using (var context = new GoTSkillZCompaniesEntities(_connectionString))
            {
                return context.Companies.FirstOrDefault(x => x.Id == entityId);
            }
        }

        public Companies Add(Companies companyObj)
        {
            if (companyObj == null) return null;

            using (var context = new GoTSkillZCompaniesEntities(_connectionString))
            {
                context.Companies.Add(companyObj);
                context.SaveChanges();
            }

            return companyObj;
        }

        public Companies Update(Companies companyObj)
        {
            if (companyObj == null) return null;

            using (var context = new GoTSkillZCompaniesEntities(_connectionString))
            {
                var existingCompany = context.Companies.FirstOrDefault(x => x.Id == companyObj.Id);

                if (existingCompany == null) return companyObj;

                existingCompany.CompanyName = companyObj.CompanyName;
                existingCompany.CompanyURL = companyObj.CompanyURL;
                context.SaveChanges();
            }

            return companyObj;
        }

        public void Delete(int entityId)
        {
            throw new NotImplementedException();
        }
    }
}