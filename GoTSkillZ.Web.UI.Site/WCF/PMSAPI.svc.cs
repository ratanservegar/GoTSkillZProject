using GoTSkillZ.Application.Transforms.DTO;
using GoTSkillZ.Application.Transforms.Enums;
using GoTSkillZ.CoreServices.MembershipService.Interfaces;
using GoTSkillZ.CoreServices.MembershipService.Services;
using GoTSkillZ.CoreServices.PMSService.Interfaces;
using GoTSkillZ.CoreServices.PMSService.Services;
using GoTSkillZ.CoreServices.PMSService.Utilities;
using GoTSkillZ.DataUtilities.Core.Interfaces;
using GoTSkillZ.DataUtilities.Core.Services;
using GoTSkillZ.Models.Membership.Data;
using GoTSkillZ.Models.PMS.Data;
using GoTSkillZ.Security.Services.Interfaces;
using GoTSkillZ.Security.Services.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.ServiceModel.Activation;

namespace GoTSkillZ.Web.UI.Site.WCF
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class PMSAPI : IPMSAPI
    {
        private readonly IDbUtility _dbUtility;
        private readonly IGoTSkillZSecurityService _gotSkillZSecurityService;
        private readonly IMembershipService _membershipService;
        private readonly IPMSService _pmsService;

        public PMSAPI()
        {
            _pmsService = new PMSService();
            _dbUtility = new DbUtility();
            _membershipService = new MembershipService();
            _gotSkillZSecurityService = new GoTSkillZSecurityService();
        }

        public List<SitemapDTO> GetAllSitemaps()
        {
            return _pmsService.GetAllSitemaps()
                .AsEnumerable()
                .Select(x => new SitemapDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    PageId = x.PageId,
                    AlternateUrl = x.AlternateUrl,
                    Icon = x.Icon,
                    CreatedBy = x.CreatedBy,
                    ModifiedBy = x.ModifiedBy,
                    ParentId = x.ParentId,
                    SortOrder = x.SortOrder,
                    IsActive = x.IsActive
                }).OrderBy(x => x.PageId).ToList();
        }

        public List<KendoHierarchicalDTO> GetSitemapTree()
        {
            var rawSitemaps =
                _pmsService.GetAllSitemaps()
                    .AsEnumerable()
                    .OrderBy(x => x.SortOrder)
                    .ThenBy(x => x.Id)
                    .ToList();
            var _processedSitemap = PmsKendoUtility.GetSiteMap(rawSitemaps);
            return _processedSitemap;
        }

        public SitemapDTO GetSitemap(SitemapDTO sitemapDto)
        {
            var sitemapObj = _pmsService.GetSitemap(sitemapDto.Id);
            return new SitemapDTO
            {
                Id = sitemapObj.Id,
                Name = sitemapObj.Name,
                TypeId = sitemapObj.TypeId,
                PageId = sitemapObj.PageId,
                ParentId = sitemapObj.ParentId,
                AlternateUrl = sitemapObj.AlternateUrl,
                Icon = sitemapObj.Icon,
                SortOrder = sitemapObj.SortOrder,
                CreatedBy = sitemapObj.CreatedBy,
                ModifiedBy = sitemapObj.ModifiedBy,
                IsActive = (bool)sitemapObj.IsActive
            };
        }

        public SitemapDTO SaveSitemap(SitemapDTO sitemapDto)
        {
            var sitemapObj = _pmsService.SaveSitemap(new Sitemaps
            {
                Id = sitemapDto.Id,
                Name = sitemapDto.Name,
                TypeId = sitemapDto.TypeId,
                PageId = sitemapDto.PageId,
                ParentId = sitemapDto.ParentId,
                AlternateUrl = sitemapDto.AlternateUrl,
                Icon = sitemapDto.Icon,
                SortOrder = sitemapDto.SortOrder,
                CreatedBy = sitemapDto.CreatedBy,
                CreatedDate = DateTime.Now, //sitemapDto.CreatedDate,
                ModifiedBy = sitemapDto.ModifiedBy,
                ModifiedDate = DateTime.Now,
                IsActive = sitemapDto.IsActive
            });

            if (sitemapObj == null) return null;

            return new SitemapDTO
            {
                Id = sitemapObj.Id,
                Name = sitemapObj.Name,
                TypeId = sitemapObj.TypeId,
                PageId = sitemapObj.PageId,
                ParentId = sitemapObj.ParentId,
                AlternateUrl = sitemapObj.AlternateUrl,
                Icon = sitemapObj.Icon,
                SortOrder = sitemapObj.SortOrder,
                CreatedBy = sitemapObj.CreatedBy,
                ModifiedBy = sitemapObj.ModifiedBy,
                IsActive = sitemapObj.IsActive
            };
        }

        public List<PagesDTO> GetAllPages()
        {
            return
                _pmsService.GetAllPages() == null
                    ? null
                    : _pmsService.GetAllPages()
                        .AsEnumerable()
                        .Select(x => new PagesDTO
                        {
                            Id = x.Id,
                            PageName = x.PageName,
                            BaseUrl = x.BaseUrl,
                            PageRoles = GetRolesCommaSeperatedForPage(x),
                            PageType = x.PageType,
                            IsActive = (bool)x.IsActive,
                            ShowContent = x.ShowContent
                        })
                        .OrderBy(x => x.Id)
                        .ToList();
        }

        public PagesDTO SavePage(PagesDTO pageDto)
        {
            var checkWhetherExists = false;

            if (pageDto.Id > 0)
            {
                var page = _pmsService.SavePage(new Pages
                {
                    Id = pageDto.Id,
                    PageName = pageDto.PageName,
                    BaseUrl = pageDto.BaseUrl,
                    PageType = pageDto.PageType,
                    ShowContent = pageDto.ShowContent,
                    IsActive = pageDto.IsActive
                });

                if (page != null)
                {
                    return new PagesDTO
                    {
                        Id = page.Id,
                        PageName = page.PageName,
                        BaseUrl = page.BaseUrl,
                        PageType = page.PageType,
                        ShowContent = page.ShowContent,
                        IsActive = page.IsActive != null && (bool)page.IsActive
                    };
                }
            }
            else
            {
                if (pageDto.Id == 0)
                {
                    var checkForValidation = _dbUtility.CheckIfExists("Pages", "PageName", pageDto.PageName);
                    if (checkForValidation >= 1)
                    {
                        checkWhetherExists = true;
                        return null;
                    }

                    if (!checkWhetherExists)
                    {
                        //add
                        var page = _pmsService.SavePage(new Pages
                        {
                            Id = pageDto.Id,
                            PageName = pageDto.PageName,
                            ShowContent = pageDto.ShowContent,
                            BaseUrl = pageDto.BaseUrl,
                            PageType = pageDto.PageType,
                            IsActive = pageDto.IsActive
                        });

                        if (page != null)
                        {
                            return new PagesDTO
                            {
                                Id = page.Id,
                                PageName = page.PageName,
                                BaseUrl = page.BaseUrl,
                                PageType = page.PageType,
                                ShowContent = page.ShowContent,
                                IsActive = page.IsActive != null && (bool)page.IsActive
                            };
                        }
                    }
                }
            }

            return pageDto;
        }

        public List<KendoHierarchicalDTO> GetNavigationForUser()
        {
            var responseDTO = _gotSkillZSecurityService.GetUserInternalId();

            if (responseDTO.Success)
            {
                var userId = responseDTO.UserId;
                Expression<Func<Sitemaps, bool>> siteMapExpression = x => (bool)x.IsActive;

                var userRoles = _membershipService.GetMember(userId).UserRoles.Select(x => x.RoleId).ToList();

                var rawSitemaps =
                    _pmsService.GetSiteMaps(siteMapExpression).OrderBy(x => x.SortOrder).ThenBy(x => x.Id).ToList();

                var processedSitemaps = new List<Sitemaps>();
                if (!userRoles.Contains(1))
                {
                    processedSitemaps.AddRange(
                        rawSitemaps.Where(siteItem => !string.IsNullOrEmpty(siteItem.AlternateUrl))
                            .Where(
                                siteItem =>
                                    siteItem.AlternateUrl.ToLower()
                                        .IndexOf("administration", StringComparison.OrdinalIgnoreCase) == -1 &&
                                    siteItem.Name.ToLower()
                                        .IndexOf("administration", StringComparison.OrdinalIgnoreCase) == -1));
                }
                else
                {
                    processedSitemaps = rawSitemaps.ToList();
                }


                return PmsKendoUtility.GetSiteMap(processedSitemaps);
            }


            return BaseNavigationMenu();
        }

        public List<PageRoleDTO> GetPageRoles(PageRoleDTO pageRoleDto)
        {
            var pageRoles = _pmsService.GetRolesForPage(new Pages
            {
                Id = pageRoleDto.PageId
            });

            return pageRoles?.AsEnumerable().Select(x => new PageRoleDTO
            {
                Id = x.Id,
                PageId = x.PageId,
                RoleId = x.RoleId
            }).ToList();
        }

        public List<PageRoleDTO> SavePageRoles(List<PageRoleDTO> pageRoles)
        {
            var pageRoleObj = _pmsService.SavePageRoles(pageRoles.AsEnumerable().Select(x => new PageRole
            {
                Id = x.Id,
                PageId = x.PageId,
                RoleId = x.RoleId
            }).ToList());

            if (pageRoleObj == null) return null;
            return pageRoles;
        }

        public ResponseDTO CheckUserPageAccess(string pageId)
        {
            var responseDTO = new ResponseDTO();

            var responseObj = _gotSkillZSecurityService.GetUserInternalId();

            if (responseObj.Success == false) return responseObj;

            var userId = responseObj.UserId;

            responseDTO.UserId = userId;
            if (pageId == "")
            {
                responseDTO.StateCode = GoTSkillZEnum.InvalidPage;
                responseDTO.ResponseText = Enum.GetName(typeof(GoTSkillZEnum), GoTSkillZEnum.InvalidPage);
                responseDTO.Success = false;
                return responseDTO;
            }

            if (pageId == "0")
            {
                responseDTO.StateCode = GoTSkillZEnum.ValidAccess;
                responseDTO.ResponseText = Enum.GetName(typeof(GoTSkillZEnum), GoTSkillZEnum.ValidAccess);
                responseDTO.Success = true;
                return responseDTO;
            }


            var userObj = _membershipService.GetUser(userId, false);
        
           userObj.Address = "Hidden";


            //check if user is a admin and return
            if (userObj.IsAdmin)
            {
                responseDTO.StateCode = GoTSkillZEnum.ValidAccess;
                responseDTO.ResponseText = Enum.GetName(typeof(GoTSkillZEnum), GoTSkillZEnum.ValidAccess);
                responseDTO.Success = true;
                responseDTO.UserProfile = userObj;
                return responseDTO;
            }

            var pageRoles = _pmsService.GetRolesForPageById(int.Parse(pageId)).ToList();

            if (userObj.IsAdmin == false && pageRoles.Select(x => x.RoleId).ToList().Contains(1))
            {
                responseDTO.StateCode = GoTSkillZEnum.InvalidAccess;
                responseDTO.ResponseText = Enum.GetName(typeof(GoTSkillZEnum), GoTSkillZEnum.InvalidAccess);
                responseDTO.Success = false;
                responseDTO.UserProfile = userObj;
                return responseDTO;
            }

            // check if user is a sub and return

            if (userObj.IsSubscriber == false)
            {
                responseDTO.StateCode = GoTSkillZEnum.NotSubscriber;
                responseDTO.ResponseText = Enum.GetName(typeof(GoTSkillZEnum), GoTSkillZEnum.NotSubscriber);
                responseDTO.Success = false;
                return responseDTO;
            }


            //check if the user is a valid user and return
            if (userObj.IsActive == false || userObj.IsDeleted)
            {
                responseDTO.StateCode = GoTSkillZEnum.InvalidUser;
                responseDTO.ResponseText = Enum.GetName(typeof(GoTSkillZEnum), GoTSkillZEnum.InvalidUser);
                responseDTO.Success = false;
                return responseDTO;
            }


            //check if the current page is a sponsor Only and user for sponsor role
            if (pageRoles.Select(x => x.RoleId).ToList().Contains(6))
            {
                if (userObj.IsSponsor == false)
                {
                    responseDTO.StateCode = GoTSkillZEnum.NotSponsor;
                    responseDTO.ResponseText = Enum.GetName(typeof(GoTSkillZEnum), GoTSkillZEnum.NotSponsor);
                    responseDTO.Success = false;
                    return responseDTO;
                }
                else
                {
                    responseDTO.StateCode = GoTSkillZEnum.ValidAccess;
                    responseDTO.ResponseText = Enum.GetName(typeof(GoTSkillZEnum), GoTSkillZEnum.ValidAccess);
                    responseDTO.Success = true;
                    responseDTO.UserProfile = userObj;
                    return responseDTO;
                }
            }


            responseDTO.StateCode = GoTSkillZEnum.ValidAccess;
            responseDTO.ResponseText = Enum.GetName(typeof(GoTSkillZEnum), GoTSkillZEnum.ValidAccess);
            responseDTO.Success = true;
            responseDTO.UserProfile = userObj;

            return responseDTO;
        }

     


        private string GetRolesCommaSeperatedForPage(Pages page)
        {
            var pageRoles = _pmsService.GetRolesForPage(page);
            if (!pageRoles.Any()) return string.Empty;
            var roles = new List<Roles>();
            foreach (var pageRole in pageRoles)
            {
                roles.Add(_membershipService.GetRole(pageRole.RoleId));
            }

            var lastRole = roles.Last();
            return roles.Where(role => role != null)
                .Aggregate("",
                    (current, role) => current + (role == lastRole ? role.RoleName + " " : role.RoleName + ", "));
        }


        private List<KendoHierarchicalDTO> BaseNavigationMenu()
        {
            Expression<Func<Sitemaps, bool>> siteMapExpression = x => (bool)x.IsActive;

            var rawSitemaps =
                _pmsService.GetSiteMaps(siteMapExpression).OrderBy(x => x.SortOrder).ThenBy(x => x.Id).ToList();

            var processedSitemaps =
                rawSitemaps.Where(siteItem => !string.IsNullOrEmpty(siteItem.AlternateUrl))
                    .Where(
                        siteItem =>
                            siteItem.AlternateUrl.ToLower()
                                .IndexOf("administration", StringComparison.OrdinalIgnoreCase) == -1 &&
                            siteItem.Name.ToLower().IndexOf("administration", StringComparison.OrdinalIgnoreCase) == -1)
                    .ToList();

            return PmsKendoUtility.GetSiteMap(processedSitemaps);
        }
    }
}