using GoTSkillZ.DataUtilities.Core.Interfaces;
using GoTSkillZ.Models.PMS.Data;

namespace GoTSkillZ.Models.PMS.Interfaces
{
    public interface IPageRoleProvider : IRepositoryModel<PageRole>
    {
        void DeletePageRoles(int pageId);
    }
}