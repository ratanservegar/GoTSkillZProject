using GoTSkillZ.Application.Transforms.DTO;
using GoTSkillZ.CoreServices.PMSService.Interfaces;
using GoTSkillZ.Models.PMS.Data;
using GoTSkillZ.Models.PMS.Interfaces;
using GoTSkillZ.Models.PMS.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GoTSkillZ.CoreServices.PMSService.Services
{
    public class PMSService : IPMSService
    {
        private readonly IPageProvider _pageProvider;
        private readonly IPageRoleProvider _pageRoleProvider;
        private readonly ISitemapProvider _sitemapProvider;
        private readonly ILoadingMessagesProvider _loadingMessagesProvider;

        public PMSService()
        {
            _pageProvider = new PageProvider();
            _sitemapProvider = new SitemapProvider();
            _pageRoleProvider = new PageRoleProvider();
            _loadingMessagesProvider = new LoadingMessagesProvider();
        }


        public List<Pages> GetAllPages()
        {
            return _pageProvider.GetAll().ToList();
        }

        public Pages SavePage(Pages pageEntity)
        {
            if (pageEntity == null) return null;

            return pageEntity.Id != 0 ? _pageProvider.Update(pageEntity) : _pageProvider.Add(pageEntity);
        }

        public List<PageRole> SavePageRoles(List<PageRole> pageRoles)
        {
            if (pageRoles.Count == 0) return null;

            _pageRoleProvider.DeletePageRoles(pageRoles[0].PageId);
            foreach (var pageRole in pageRoles)
            {
                _pageRoleProvider.Add(pageRole);
            }
            return pageRoles;
        }

        public List<PageRole> GetRolesForPage(Pages page)
        {
            if (page == null) return null;
            var pageId = _pageProvider.FindBy(u => u.Id == page.Id).AsEnumerable().Select(x => x.Id).FirstOrDefault();
            return _pageRoleProvider.FindBy(u => u.PageId == pageId).ToList();
        }

        public List<PageRole> GetRolesForPageById(int pageId)
        {
            if (pageId == 0) return null;

            return _pageRoleProvider.FindBy(x => x.PageId == pageId).ToList();
        }

        public List<Sitemaps> GetAllSitemaps()
        {
            return _sitemapProvider.GetAll().ToList();
        }

        public List<Sitemaps> GetSiteMaps(Expression<Func<Sitemaps, bool>> predicate)
        {
            return _sitemapProvider.FindBy(predicate).ToList();
        }

        public Sitemaps GetSitemap(int siteMapEntityId)
        {
            Expression<Func<Sitemaps, bool>> expression = u => u.Id == siteMapEntityId;
            return _sitemapProvider.FindBy(expression).FirstOrDefault();
        }

        public Sitemaps SaveSitemap(Sitemaps sitemapEntity)
        {
            if (sitemapEntity == null) return null;

            return sitemapEntity.Id != 0 ? _sitemapProvider.Update(sitemapEntity) : _sitemapProvider.Add(sitemapEntity);
        }

        public List<LoadingMessages> GetAllLoadingMessages()
        {
            return _loadingMessagesProvider.FindBy(x => x.Active).ToList();
        }
    }
}