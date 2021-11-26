using GoTSkillZ.Models.Partner.Data;
using System.Collections.Generic;

namespace GoTSkillZ.CoreServices.PartnerService.Interfaces
{
    public interface IPartnerService
    {
        List<Companies> GetAllCompanies();
    }
}