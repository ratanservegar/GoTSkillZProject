using GoTSkillZ.Application.Transforms.DTO;
using GoTSkillZ.Models.PMS.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GoTSkillZ.CoreServices.PMSService.Interfaces
{
    public interface IPMSService
    {
        List<Pages> GetAllPages();
        Pages SavePage(Pages pageEntity);

        List<PageRole> SavePageRoles(List<PageRole> pageRoles);
        List<PageRole> GetRolesForPage(Pages page);

        List<PageRole> GetRolesForPageById(int pageId);
        #region Sitemap
        List<Sitemaps> GetAllSitemaps();

        List<Sitemaps> GetSiteMaps(Expression<Func<Sitemaps, bool>> predicate);

        Sitemaps GetSitemap(int siteMapEntityId);
        Sitemaps SaveSitemap(Sitemaps sitemapEntity);

        List<LoadingMessages> GetAllLoadingMessages();


        #endregion
    }
}