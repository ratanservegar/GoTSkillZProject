using GoTSkillZ.Models.PMS.Data;
using GoTSkillZ.Models.PMS.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Linq.Expressions;

namespace GoTSkillZ.Models.PMS.Providers
{
    public class SitemapProvider : ISitemapProvider
    {
        private string _connectionString
        {
            get
            {
                var entityBuilder = new EntityConnectionStringBuilder
                {
                    Provider = "System.Data.SqlClient",
                    Metadata = @"res://*/Data.SitemapModel.csdl|res://*/Data.SitemapModel.ssdl|res://*/Data.SitemapModel.msl",
                    ProviderConnectionString = ConfigurationManager.ConnectionStrings["GoTSkillZConnectionString"].ConnectionString
                };

                return entityBuilder.ToString();
            }
        }


        public IEnumerable<Sitemaps> GetAll()
        {
            using (var context = new GoTSkillZSitemapEntities(_connectionString))
            {
                return context.Sitemaps.AsEnumerable().ToList();
            }
        }

        public IEnumerable<Sitemaps> FindBy(Expression<Func<Sitemaps, bool>> predicate)
        {
            using (var context = new GoTSkillZSitemapEntities(_connectionString))
            {
                return context.Sitemaps.Where(predicate).ToList();
            }
        }

        public Sitemaps Get(int entityId)
        {
            using (var context = new GoTSkillZSitemapEntities(_connectionString))
            {
                return context.Sitemaps.FirstOrDefault(x => x.Id == entityId);
            }
        }

        public Sitemaps Add(Sitemaps sitemap)
        {
            if (sitemap == null) return null;
            using (var context = new GoTSkillZSitemapEntities(_connectionString))
            {
                sitemap.CreatedDate = DateTime.Now;
                sitemap.ModifiedDate = SqlDateTime.MinValue.Value;
                context.Sitemaps.Add(sitemap);
                context.SaveChanges();
            }
            return sitemap;
        }

        public Sitemaps Update(Sitemaps sitemap)
        {
            if (sitemap == null) return null;
            using (var context = new GoTSkillZSitemapEntities(_connectionString))
            {
                var existingSitemap = context.Sitemaps.FirstOrDefault(x => x.Id == sitemap.Id);
                if (existingSitemap == null) { return sitemap; }
                existingSitemap.Name = sitemap.Name;

                existingSitemap.ParentId = sitemap.ParentId;
                sitemap.TypeId = sitemap.TypeId;
                sitemap.PageId = sitemap.PageId;
                existingSitemap.AlternateUrl = sitemap.AlternateUrl;
                existingSitemap.Icon = sitemap.Icon;
                existingSitemap.SortOrder = sitemap.SortOrder;
                existingSitemap.CreatedBy = sitemap.CreatedBy;
                existingSitemap.CreatedDate = DateTime.Parse(existingSitemap.CreatedDate.ToString());//sitemap.CreatedDate;
                existingSitemap.ModifiedBy = sitemap.ModifiedBy;
                existingSitemap.ModifiedDate = DateTime.Now;
                existingSitemap.IsActive = sitemap.IsActive;
                context.SaveChanges();
            }
            return sitemap;
        }

        public void Delete(int entityId)
        {
            if (entityId < 1) return;
            using (var context = new GoTSkillZSitemapEntities(_connectionString))
            {
                var existingSitemap = context.Sitemaps.FirstOrDefault(x => x.Id == entityId);
                if (existingSitemap == null) { return; }
                existingSitemap.IsActive = false;
                context.SaveChanges();
            }
        }
    }
}
