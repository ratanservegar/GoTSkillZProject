using GoTSkillZ.Application.Transforms.DTO;
using GoTSkillZ.Models.PMS.Data;
using System.Collections.Generic;
using System.Linq;

namespace GoTSkillZ.CoreServices.PMSService.Utilities
{
    public static class PmsKendoUtility
    {
        /// <summary>
        /// Function to create Hierarchical Kendo Object for API consumption using Sitemaps
        /// </summary>
        /// <param name="siteMaps">List of type <see cref="Sitemap"/></param>
        /// <returns>List of type <see cref="KendoHierarchicalDTO"/></returns>
        public static List<KendoHierarchicalDTO> GetSiteMap(List<Sitemaps> siteMaps)
        {
            var returnSiteMap = new List<KendoHierarchicalDTO>();

            if (siteMaps == null) return returnSiteMap;
            if (!siteMaps.Any()) return returnSiteMap;

            returnSiteMap = siteMaps.AsEnumerable()
                .Where(x => x.ParentId == 0)
                .Select(x => new KendoHierarchicalDTO
                {
                    Id = x.Id,
                    typeId = x.TypeId,
                    Text = x.Name,
                    ImageUrl = "",
                    Icon = x.Icon,
                    Url = x.AlternateUrl
                })
                .ToList();

            foreach (var parentSiteMap in returnSiteMap)
                parentSiteMap.Items = ProcessSiteMapNode(parentSiteMap, siteMaps);

            return returnSiteMap;
        }

        private static List<KendoHierarchicalDTO> ProcessSiteMapNode(KendoHierarchicalDTO currentSiteMap,
            List<Sitemaps> siteMaps)
        {
            var childElements = siteMaps.AsEnumerable()
                .Where(x => x.ParentId == currentSiteMap.Id)
                .Select(x => new KendoHierarchicalDTO
                {
                    Id = x.Id,
                    typeId = x.TypeId,
                    Text = x.Name,
                    ImageUrl = "",
                    Icon = x.Icon,
                    Url = x.AlternateUrl
                })
                .ToList();

            if (childElements.Any())
                foreach (var childElement in childElements)
                    childElement.Items = ProcessSiteMapNode(childElement, siteMaps);

            return childElements;
        }
    }
}
