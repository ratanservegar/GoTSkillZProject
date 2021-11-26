using GoTSkillZ.CoreServices.PartnerService.Interfaces;
using GoTSkillZ.Models.Partner.Data;
using GoTSkillZ.Models.Partner.Interfaces;
using GoTSkillZ.Models.Partner.Providers;
using System.Collections.Generic;
using System.Linq;

namespace GoTSkillZ.CoreServices.PartnerService.Services
{
    public class PartnerService : IPartnerService
    {

        private readonly ICompaniesProvider _companiesProvider;

        public PartnerService()
        {
            _companiesProvider = new CompaniesProvider();
        }

        public List<Companies> GetAllCompanies()
        {
            return _companiesProvider.GetAll().OrderBy(x => x.CompanyName).ToList();
        }
    }
}
