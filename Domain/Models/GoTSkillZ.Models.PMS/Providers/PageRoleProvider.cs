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
    public class PageRoleProvider : IPageRoleProvider
    {
        private string _connectionString
        {
            get
            {
                var entityBuilder = new EntityConnectionStringBuilder
                {
                    Provider = "System.Data.SqlClient",
                    Metadata = @"res://*/Data.PageRoleModel.csdl|res://*/Data.PageRoleModel.ssdl|res://*/Data.PageRoleModel.msl",
                    ProviderConnectionString = ConfigurationManager.ConnectionStrings["GoTSkillZConnectionString"].ConnectionString
                };

                return entityBuilder.ToString();
            }
        }


        public IEnumerable<PageRole> GetAll()
        {
            using (var context = new GoTSkillZPageRoleEntities(_connectionString))
            {
                return context.PageRoles.ToList();
            }
        }

        public IEnumerable<PageRole> FindBy(Expression<Func<PageRole, bool>> predicate)
        {
            using (var context = new GoTSkillZPageRoleEntities(_connectionString))
            {
                return context.PageRoles.Where(predicate).ToList();
            }
        }

        public PageRole Get(int entityId)
        {
            using (var context = new GoTSkillZPageRoleEntities(_connectionString))
            {
                return context.PageRoles.FirstOrDefault(x => x.Id == entityId);
            }
        }

        public PageRole Add(PageRole pageRole)
        {
            if (pageRole == null) return null;
            using (var context = new GoTSkillZPageRoleEntities(_connectionString))
            {
                context.PageRoles.Add(pageRole);
                context.SaveChanges();
            }
            return pageRole;
        }

        public PageRole Update(PageRole pageRole)
        {
            if (pageRole == null) return null;

            using (var context = new GoTSkillZPageRoleEntities(_connectionString))
            {
                var existingPageRole = context.PageRoles.FirstOrDefault(x => x.Id == pageRole.Id);

                //TODO:This can be made smarter with context attachments
                if (existingPageRole == null) return pageRole;
                existingPageRole.Id = pageRole.Id;
                existingPageRole.PageId = pageRole.PageId;
                existingPageRole.RoleId = pageRole.RoleId;
                context.SaveChanges();
            }
            return pageRole;
        }

        public void Delete(int entityId)
        {
            if (entityId < 1) return;

            using (var context = new GoTSkillZPageRoleEntities(_connectionString))
            {
                var existingPageRole = context.PageRoles.FirstOrDefault(x => x.Id == entityId);

                if (existingPageRole == null) return;
                context.PageRoles.Remove(existingPageRole);
                context.SaveChanges();
            }
        }

        public void DeletePageRoles(int pageId)
        {
            using (var context = new GoTSkillZPageRoleEntities(_connectionString))
            {
                context.Database.ExecuteSqlCommand("delete from PageRoles where PageId = " + pageId);
            }
        }
    }
}
