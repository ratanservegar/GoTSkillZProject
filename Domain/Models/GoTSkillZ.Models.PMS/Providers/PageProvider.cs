using GoTSkillZ.Models.PMS.Data;
using GoTSkillZ.Models.PMS.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Linq.Expressions;

namespace GoTSkillZ.Models.PMS.Providers
{
    public class PageProvider : IPageProvider
    {
        private string _connectionString
        {
            get
            {
                var entityBuilder = new EntityConnectionStringBuilder
                {
                    Provider = "System.Data.SqlClient",
                    Metadata = @"res://*/Data.PageModel.csdl|res://*/Data.PageModel.ssdl|res://*/Data.PageModel.msl",
                    ProviderConnectionString = ConfigurationManager.ConnectionStrings["GoTSkillZConnectionString"].ConnectionString
                };

                return entityBuilder.ToString();
            }
        }

        public IEnumerable<Pages> GetAll()
        {
            using (var context = new GoTSkillZPageEntities(_connectionString))
            {
                return context.Pages.ToList();
            }
        }

        public IEnumerable<Pages> FindBy(Expression<Func<Pages, bool>> predicate)
        {
            using (var context = new GoTSkillZPageEntities(_connectionString))
            {
                return context.Pages.Where(predicate).ToList();
            }
        }

        public Pages Get(int entityId)
        {
            using (var context = new GoTSkillZPageEntities(_connectionString))
            {
                return context.Pages.FirstOrDefault(x => x.Id == entityId);
            }
        }

        public Pages Add(Pages page)
        {
            if (page == null) return null;

            using (var context = new GoTSkillZPageEntities(_connectionString))
            {
                context.Pages.Add(page);
                context.SaveChanges();
            }

            return page;
        }

        public Pages Update(Pages page)
        {
            if (page == null) return null;

            using (var context = new GoTSkillZPageEntities(_connectionString))
            {
                var existingPage = context.Pages.FirstOrDefault(x => x.Id == page.Id);

                if (existingPage == null) return page;

                existingPage.PageName = page.PageName;
                existingPage.BaseUrl = page.BaseUrl;
                existingPage.IsActive = page.IsActive;
                existingPage.ShowContent = page.ShowContent;
                context.SaveChanges();
            }

            return page;
        }

        public void Delete(int entityId)
        {
            if (entityId < 1) return;

            using (var context = new GoTSkillZPageEntities(_connectionString))
            {
                var existingPage = context.Pages.FirstOrDefault(x => x.Id == entityId);

                if (existingPage == null) return;

                existingPage.IsActive = false;
                context.SaveChanges();
            }
        }
    }
}
