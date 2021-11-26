using GoTSkillZ.CoreServices.PartnerService.Interfaces;
using GoTSkillZ.CoreServices.PartnerService.Services;
using GoTSkillZ.Models.Partner.Data;
using System.Collections.Generic;

namespace GoTSkillZ.Web.UI.Site.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "PartnerAPI" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select PartnerAPI.svc or PartnerAPI.svc.cs at the Solution Explorer and start debugging.
    public class PartnerAPI : IPartnerAPI
    {
        private readonly IPartnerService _partnerService;

        public PartnerAPI()
        {
            _partnerService = new PartnerService();
        }

        public List<Companies> GetCompanies()
        {
            return _partnerService.GetAllCompanies();
        }
    }
}
