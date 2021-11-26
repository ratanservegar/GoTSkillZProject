using GoTSkillZ.Application.Transforms.DTO;
using GoTSkillZ.CoreServices.MembershipService.Interfaces;
using GoTSkillZ.Models.Game.Interfaces;
using GoTSkillZ.Models.Game.Provider;
using GoTSkillZ.Models.Location.Interfaces;
using GoTSkillZ.Models.Location.Provider;
using GoTSkillZ.Models.Membership.Data;
using GoTSkillZ.Models.Membership.Interfaces;
using GoTSkillZ.Models.Membership.Providers;
using GoTSkillZ.Models.UserDataExtension.Data;
using GoTSkillZ.Models.UserDataExtension.Interfaces;
using GoTSkillZ.Models.UserDataExtension.Provider;
using GoTSkillZ.Models.UserSetup.Data;
using GoTSkillZ.Models.UserSetup.Interfaces;
using GoTSkillZ.Models.UserSetup.Provider;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace GoTSkillZ.CoreServices.MembershipService.Services
{
    public class MembershipService : IMembershipService
    {
        private readonly ICityProvider _cityProvider;
        private readonly ICountryProvider _countryProvider;
        private readonly IUserProfileExtensionProvider _profileExtensionProvider;
        private readonly IRoleProvider _roleProvider;
        private readonly IStateProvider _stateProvider;
        private readonly IUserAchievementProvider _userAchievementProvider;
        private readonly IUserProvider _userProvider;
        private readonly IUserRoleProvider _userRoleProvider;
        private readonly IUserSocialLinksProvider _userSocialLinksProvider;
        private readonly IYouTubeSponsorsProvider _youTubeSponsorsProvider;
        private readonly IYouTubeSubscribersProvider _youTubeSubscribersProvider;
        private readonly IUserOccupationProvider _userOccupationProvider;
        private readonly IGamePlatformConnectivityProvider _gamePlatformConnectivityProvider;
        private readonly IUserSetupDataProvider _userSetupDataProvider;
        private readonly IUserSetupsProvider _userSetupsProvider;
        private readonly IUserTeamHistoryProvider _userTeamHistoryProvider;
        private readonly ISetupTypesProvider _setupTypesProvider;
        private readonly IUserPeripheralsProvider _userPeripheralsProvider;

        public MembershipService()
        {
            _userProvider = new UserProvider();
            _roleProvider = new RoleProvider();
            _userRoleProvider = new UserRoleProvider();
            _userSocialLinksProvider = new UserSocialLinksProvider();
            _userAchievementProvider = new UserAchievementProvider();
            _youTubeSubscribersProvider = new YouTubeSubscribersProvider();
            _youTubeSponsorsProvider = new YouTubeSponsorsProvider();
            _profileExtensionProvider = new UserProfileExtensionProvider();
            _countryProvider = new CountryProvider();
            _stateProvider = new StateProvider();
            _cityProvider = new CityProvider();
            _userOccupationProvider = new UserOccupationProvider();
            _gamePlatformConnectivityProvider = new GamePlatformConnectivityProvider();
            _userSetupDataProvider = new UserSetupDataProvider();
            _userTeamHistoryProvider = new UserTeamHistoryProvider();
            _userSetupsProvider = new UserSetupsProvider();
            _setupTypesProvider = new SetupTypesProvider();
            _userPeripheralsProvider = new UserPeripheralsProvider();

        }

        public IEnumerable<Users> QueryMembers(Expression<Func<Users, bool>> predicate)
        {
            return _userProvider.FindBy(predicate).ToList();
        }

        public void DeactivateUser(int membershipEntityId)
        {
            _userProvider.Delete(membershipEntityId);
        }

        public Users GetUserByGoogleToken(string googleToken)
        {
            var returnUser = QueryMembers(u => u.GoogleToken == googleToken).FirstOrDefault();
            return returnUser == null ? null : GetMember(returnUser.UserId);
        }

        public Users GetUserByGooglePublicId(string googlePublicId)
        {
            var returnUser = QueryMembers(u => u.GooglePublicId == googlePublicId).FirstOrDefault();
            return returnUser == null ? null : GetMember(returnUser.UserId);
        }

        public IEnumerable<Users> GetUsersInRole(Roles role)
        {
            Expression<Func<UserRoles, bool>> userRoleExpression = ur => ur.RoleId == role.Id;

            var userIds = _userRoleProvider.FindBy(userRoleExpression).Select(x => x.Id).ToList();

            Expression<Func<Users, bool>> userExpression = u => userIds.Contains(u.UserId);

            return _userProvider.FindBy(userExpression).ToList();
        }

        public IEnumerable<Users> GetUsersByRole(string roleName)
        {
            var roleObj = _roleProvider.GetAll().FirstOrDefault(x => x.RoleName.Equals(roleName));

            if (roleObj == null) return null;

            Expression<Func<UserRoles, bool>> userRoleExpression = ur => ur.RoleId == roleObj.Id;

            var userIds = _userRoleProvider.FindBy(userRoleExpression).Select(x => x.UserId).ToList();

            Expression<Func<Users, bool>> userExpression = u => userIds.Contains(u.UserId);

            return _userProvider.FindBy(userExpression).ToList();
        }

        public IEnumerable<Users> GetAllUsers()
        {
            return _userProvider.GetAll().ToList();
        }

        public UserProfileDTO GetUserProfileMetaData(int userId)
        {
            var returnObj = new UserProfileDTO();
            var userMetaData = _userProvider.FindBy(x => x.UserId == userId).FirstOrDefault();
            var userSocialLinks = _userSocialLinksProvider.FindBy(x => x.UserId == userId).FirstOrDefault();
            if (userMetaData != null)
            {
                returnObj.UserId = userMetaData.UserId;
                returnObj.FirstName = userMetaData.FirstName;
                returnObj.LastName = userMetaData.LastName;
                returnObj.Email = userMetaData.Email;
                returnObj.TelNo = userMetaData.TelNo;
                returnObj.Age = userMetaData.Age;
                returnObj.Country = userMetaData.Country;
                returnObj.State = userMetaData.State;
                returnObj.City = userMetaData.City;
                returnObj.PinCode = userMetaData.PinCode;
                returnObj.Address = userMetaData.Address;
                returnObj.IsActive = userMetaData.IsActive;
                returnObj.IsDeleted = userMetaData.IsDeleted;
                returnObj.Gender = userMetaData.Gender;
                returnObj.MaritalStatus = userMetaData.MaritalStatus;
                returnObj.Occupation = userMetaData.Occupation;
            }


            if (userSocialLinks != null)
            {
                returnObj.UserSocialLinks = userSocialLinks;
            }

            return returnObj;
        }


        public Users SaveUser(Users user)
        {
            return _userProvider.Update(user);
        }

        public UserProfileDTO GetUser(int userId, bool readOnly)
        {
            var userObj = new UserProfileDTO();

            var userDataPath = ConfigurationManager.AppSettings["UserFiles"] + "\\" + userId +
                               "\\ProfileImage\\ProfileImage.jpg";

            var returnUser = _userProvider.Get(userId);

            if (returnUser != null)
            {
                userObj.UserId = returnUser.UserId;
                userObj.FirstName = returnUser.FirstName;
                userObj.LastName = returnUser.LastName;
                userObj.Email = returnUser.Email;
                userObj.TelNo = returnUser.TelNo;
                userObj.Age = returnUser.Age;
                userObj.Country = returnUser.Country;
                userObj.State = returnUser.State;
                userObj.City = returnUser.City;
                if (returnUser.DOB != null) userObj.DOB = returnUser.DOB.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                userObj.PinCode = returnUser.PinCode;
                userObj.Address = returnUser.Address;
                userObj.IsActive = returnUser.IsActive;
                userObj.IsRegistered = returnUser.IsRegistered;
                userObj.IsDeleted = returnUser.IsDeleted;
                userObj.UserRoles = GetMemberRoles(userId).ToList();
                userObj.UserSocialLinks = GetUserSocialLinks(userId);
                userObj.UserAchievements = GetUserAchievements(userId);
                userObj.IsSubscriber = CheckIfUserIsASubscriber(userId);
                userObj.IsSponsor = CheckIfUserIsASponsor(userId);
                userObj.IsAdmin = CheckIfUserIsAdmin(userId);
                userObj.UserProfileExtension = GetUserProfileExtension(userId);
                userObj.ShowPersonalInfo = returnUser.ShowPersonalInfo;
                userObj.Gender = returnUser.Gender;
                userObj.MaritalStatus = returnUser.MaritalStatus;
                userObj.Occupation = returnUser.Occupation;
                userObj.GamePlatformConnectivity = GetUserGamePlayformConnectivity(userId);
                userObj.UserSetupData = GetUserSetupData(userId);
                userObj.UserPeripheralData = GetUserPeripheralData(userId);
                userObj.UserSetups = GetUserSetups(userId);
                userObj.UserTeamHistory = GetUserTeamHistory(userId);
                userObj.ReadOnly = readOnly;

                if (File.Exists(userDataPath))
                {
                    userObj.ProfileImage = "/UserData/" + userId + "/ProfileImage/ProfileImage.jpg";
                }
            }



            return userObj;
        }

        public UserProfileDTO GetUserHeaderData(int userId)
        {
            var userObj = new UserProfileDTO();

            var userDataPath = ConfigurationManager.AppSettings["UserFiles"] + "\\" + userId +
                               "\\ProfileImage\\ProfileImage.jpg";

            var returnUser = _userProvider.Get(userId);

            userObj.UserId = returnUser.UserId;
            userObj.FirstName = returnUser.FirstName;
            userObj.LastName = returnUser.LastName;
            userObj.Email = returnUser.Email;
            userObj.IsActive = returnUser.IsActive;
            userObj.IsRegistered = returnUser.IsRegistered;
            userObj.IsDeleted = returnUser.IsDeleted;
            userObj.UserRoles = GetMemberRoles(userId).ToList();
            userObj.IsAdmin = CheckIfUserIsAdmin(userId);
            userObj.UserProfileExtension = GetUserProfileExtension(userId);

            if (File.Exists(userDataPath))
            {
                userObj.ProfileImage = "/UserData/" + userId + "/ProfileImage/ProfileImage.jpg";
            }

            return userObj;
        }


        public List<Roles> GetAllRoles()
        {
            return _roleProvider.GetAll().ToList();
        }

        public Roles GetRole(int roleId)
        {
            return _roleProvider.Get(roleId);
        }



        public List<UserAchievementsDTO> GetUserAchievements(int userId)
        {
            return
                _userAchievementProvider.FindBy(x => x.UserId == userId)
                    .OrderByDescending(x => x.Date)
                    .ToList()
                    .Select(x => new UserAchievementsDTO
                    {
                        Id = x.Id,
                        UserId = x.UserId,
                        Description = x.Description,
                        Name = x.Name,
                        Location = x.Name,
                        Position = x.Position,
                        IsActive = x.IsActive.ToString(),
                        Type = x.Type,
                        Date = x.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
                    }).ToList();
        }

        public UserSocialLinks GetUserSocialLinks(int userId)
        {
            return _userSocialLinksProvider.Get(userId);
        }

        public bool CheckIfUserIsASubscriber(int userId)
        {
            var subOjb = _youTubeSubscribersProvider.FindBy(x => x.UserId == userId).FirstOrDefault();

            if (subOjb != null)
            {
                return subOjb.IsSubscribed;
            }
            return false;
        }

        public bool CheckIfUserIsASponsor(int userId)
        {
            var sponOjb = _youTubeSponsorsProvider.FindBy(x => x.UserId == userId).FirstOrDefault();

            if (sponOjb != null)
            {
                return sponOjb.IsSponsor;
            }

            return false;
        }

        public List<CountryDTO> FindCountries(string searchKey)
        {
            return
                _countryProvider.FindBy(x => x.name.ToLower().StartsWith(searchKey.ToLower()))
                    .Select(x => new CountryDTO
                    {
                        CountryId = x.id,
                        CountryName = x.name
                    }).OrderBy(x => x.CountryName).ToList();
        }

        public List<StateDTO> FindState(string searchKey, int countryId)
        {
            if (countryId != 0)
            {
                return
                    _stateProvider.FindBy(
                        x => x.country_id == countryId && x.name.ToLower().StartsWith(searchKey.ToLower()))
                        .Select(x => new StateDTO
                        {
                            StateId = x.id,
                            CountryId = x.country_id,
                            StateName = x.name
                        }).OrderBy(x => x.StateName).ToList();
            }

            return
                _stateProvider.FindBy(
                    x => x.name.ToLower().StartsWith(searchKey.ToLower()))
                    .Select(x => new StateDTO
                    {
                        StateId = x.id,
                        CountryId = x.country_id,
                        StateName = x.name
                    }).OrderBy(x => x.StateName).ToList();
        }

        public List<CityDTO> FindCity(string searchKey, int countryId, int stateId)
        {
            if (countryId != 0 && stateId != 0)
            {
                return
                    _cityProvider.FindBy(
                        x =>
                            x.country_id == countryId && x.state_id == stateId &&
                            x.name.ToLower().StartsWith(searchKey.ToLower())).Select(x => new CityDTO
                            {
                                CountryId = x.country_id,
                                StateId = x.state_id,
                                CityId = x.id,
                                CityName = x.name
                            }).OrderBy(x => x.CityName).ToList();
            }

            return
                _cityProvider.FindBy(
                    x => x.name.ToLower().StartsWith(searchKey.ToLower())).Select(x => new CityDTO
                    {
                        CountryId = x.country_id,
                        StateId = x.state_id,
                        CityId = x.id,
                        CityName = x.name
                    }).OrderBy(x => x.CityName).ToList();
        }

        public string SaveUserProfileData(UserProfileDTO userProfileDTO)
        {
            var returnString = "failed";
            var existingUser = _userProvider.FindBy(x => x.UserId == userProfileDTO.UserId).FirstOrDefault();

            if (existingUser != null)
            {
                existingUser.FirstName = userProfileDTO.FirstName;
                existingUser.LastName = userProfileDTO.LastName;
                existingUser.Age = userProfileDTO.Age == 0 ? null : userProfileDTO.Age;

                if (userProfileDTO.DOB != "")
                {
                    existingUser.DOB = DateTime.ParseExact(userProfileDTO.DOB, @"dd/MM/yyyy",
                        CultureInfo.InvariantCulture);
                }


                existingUser.Address = userProfileDTO.Address;
                existingUser.Country = userProfileDTO.Country;
                existingUser.City = userProfileDTO.City;
                existingUser.State = userProfileDTO.State;
                existingUser.TelNo = userProfileDTO.TelNo;
                existingUser.ShowPersonalInfo = userProfileDTO.ShowPersonalInfo;
                existingUser.PinCode = userProfileDTO.PinCode;
                existingUser.IsRegistered = true;
                existingUser.ModifiedDate = new DateTime?();
                existingUser.Gender = userProfileDTO.Gender;
                existingUser.MaritalStatus = userProfileDTO.MaritalStatus;
                existingUser.Occupation = userProfileDTO.Occupation;

                var updatedUser = _userProvider.Update(existingUser);

                if (updatedUser != null)
                    returnString = "success";
            }

            // save user social links
            if (userProfileDTO.UserSocialLinks != null)
                returnString = SaveUserSocialLinks(userProfileDTO);

            // save user achievements
            if (userProfileDTO.UserAchievements != null && userProfileDTO.UserAchievements.Count > 0)
                SaveUserAchievements(userProfileDTO);


            // remove user achievements
            if (userProfileDTO.RemoveAchievements.Count > 0)
                returnString = RemoveUserAchievements(userProfileDTO.RemoveAchievements);

            // save user profile extension
            if (userProfileDTO.UserProfileExtension != null)
                returnString = SaveUserProfileExtension(userProfileDTO);

            //team history
            if (userProfileDTO.UserTeamHistory != null && userProfileDTO.UserTeamHistory.Count > 0)
                SaveUserTeamHistory(userProfileDTO);

            // remove team history
            if (userProfileDTO.RemoveTeamHistory.Count > 0)
                returnString = RemoveUserTeamHistory(userProfileDTO.RemoveTeamHistory);

            //save user peripheral data
            if (userProfileDTO.UserPeripheralData != null && userProfileDTO.UserPeripheralData.Count > 0)
                SaveUserPeripheralData(userProfileDTO);


            //remove peripheral
            if (userProfileDTO.RemovePeripherals.Count > 0)
                returnString = RemoveUserPeripherals(userProfileDTO.RemovePeripherals);


            return returnString;
        }

        public List<UserOccupation> GetUserOccupations()
        {
            return _userOccupationProvider.GetAll().OrderBy(x => x.Occupation).ToList();
        }

        public string AddNewUserSetup(List<UserSetupDataDTO> newUserSetupObj)
        {
            var returnString = "failed";
            var userId = newUserSetupObj[0].UserId;
            var setupTypeId = newUserSetupObj[0].SetupTypeId;

            if (setupTypeId != 0 && userId != 0)
            {
                var setupType = _setupTypesProvider.FindBy(x => x.Id == setupTypeId).FirstOrDefault();

                if (setupType != null)
                {
                    var newUserSetup = new UserSetups
                    {
                        UserId = userId,
                        SetupTypeId = setupType.Id,
                        SetupName = newUserSetupObj[0].SetupName
                    };
                    var returnNewSetup = _userSetupsProvider.Add(newUserSetup);

                    if (returnNewSetup != null)
                    {
                        var userSetupId = returnNewSetup.Id;
                        returnString = userSetupId.ToString();
                        foreach (var setupComponent in newUserSetupObj)
                        {
                            var newSetupComopnent = new UserSetupData
                            {
                                UserId = userId,
                                UserSetupId = userSetupId,
                                CompanyName = setupComponent.CompanyName,
                                Component = setupComponent.Component,
                                ProductDetails = setupComponent.ProductDetails,
                                AffiliateLink = setupComponent.AffiliateLink
                            };

                            var newSetupDataOb = _userSetupDataProvider.Add(newSetupComopnent);

                            if (newSetupDataOb == null)
                            {
                                returnString = "failed";
                            }
                        }
                    }
                    else
                    {
                        returnString = "failed";
                    }
                }
                else
                {
                    returnString = "failed";
                }
            }
            else
            {
                returnString = "failed";
            }


            return returnString;
        }

        public string UpdateUserSetup(List<UserSetupDataDTO> updateUserSetup)
        {
            var returnString = "success";

            if (updateUserSetup.Any())
            {
                var setupId = updateUserSetup[0].SetupId;
                var existingSetup = _userSetupsProvider.FindBy(x => x.Id == setupId).FirstOrDefault();
                if (existingSetup != null)
                {
                    existingSetup.SetupName = updateUserSetup[0].SetupName;
                    _userSetupsProvider.Update(existingSetup);
                }

                foreach (var userSetup in updateUserSetup)
                {
                    var existingSetupData = _userSetupDataProvider.FindBy(x => x.Id == userSetup.Id).FirstOrDefault();
                    if (existingSetupData != null)
                    {




                        existingSetupData.CompanyName = userSetup.CompanyName;
                        existingSetupData.ProductDetails = userSetup.ProductDetails;
                        existingSetupData.AffiliateLink = userSetup.AffiliateLink;

                        var updatedSetup = _userSetupDataProvider.Update(existingSetupData);

                        if (updatedSetup == null)
                            returnString = "failed";
                    }
                }
            }

            return returnString;
        }

        public string RemoveUserSetup(string setupId)
        {
            var returnString = "success";

            _userSetupsProvider.DeleteUserSetup(setupId);
            _userSetupDataProvider.DeleteUserSetupData(setupId);


            return returnString;
        }


        public Users GetMember(int userId)
        {
            var returnUser = _userProvider.Get(userId);
            returnUser.UserRoles = GetMemberRoles(returnUser.UserId).ToList();
            returnUser.CoreRoles = GetCoreRoles();
            returnUser.AssignedRoles = GetUserRoles(returnUser.UserId).ToList();
            return returnUser;
        }


        public IEnumerable<UserRoles> GetMemberRoles(int memberEntityId)
        {
            return _userRoleProvider.FindBy(ur => ur.UserId == memberEntityId);
        }

        public List<Roles> GetCoreRoles()
        {
            return _roleProvider.GetAll().ToList();
        }

        public IEnumerable<Roles> GetUserRoles(int membershipEntityId)
        {
            var userRoles = GetMemberRoles(membershipEntityId).Select(x => x.RoleId).ToList();

            Expression<Func<Roles, bool>> roleExpression = r => userRoles.Contains(r.Id);
            return _roleProvider.FindBy(roleExpression);
        }


        private bool CheckIfUserIsAdmin(int userId)
        {
            var isAdmin = false;
            var userRoles = GetMemberRoles(userId).Select(x => x.RoleId).ToList();

            if (userRoles.Contains(1))
                isAdmin = true;


            return isAdmin;
        }

        private UserProfileExtensionDTO GetUserProfileExtension(int userId)
        {
            var returnObj = new UserProfileExtensionDTO();
            var userProfileExtension = _profileExtensionProvider.FindBy(x => x.UserId == userId).FirstOrDefault();

            if (userProfileExtension != null)
            {
                returnObj.Alias = userProfileExtension.Alias;
                returnObj.About = userProfileExtension.About;
                returnObj.Status = userProfileExtension.Status;
                returnObj.PrimaryGameExp = userProfileExtension.PrimaryGameExp;
                returnObj.PrimaryGame = userProfileExtension.PrimaryGame;
                returnObj.SecondaryGame = userProfileExtension.SecondaryGame;
                returnObj.SecondaryGameExp = userProfileExtension.SecondaryGameExp;
                returnObj.PrimaryRole = userProfileExtension.PrimaryRole;
                returnObj.SecondaryRole = userProfileExtension.SecondaryRole;
                returnObj.CurrentTeam = userProfileExtension.CurrentTeam;
            }


            return returnObj;
        }

        private List<UserTeamHistoryDTO> GetUserTeamHistory(int userId)
        {

            return _userTeamHistoryProvider.FindBy(x => x.UserId == userId).Select(x => new UserTeamHistoryDTO
            {
                Id = x.Id,
                UserId = x.UserId,
                TeamName = x.TeamName,
                FromDate = (x.FromDate == null) || (x.FromDate == "") ? "N/A" : x.FromDate,
                ToDate = (x.ToDate == null) || (x.ToDate == "") ? "N/A" : x.ToDate,
            }).OrderByDescending(x => x.ToDate).ThenByDescending(x => x.FromDate).ToList();

        }



        private string SaveUserSocialLinks(UserProfileDTO userProfileDTO)
        {
            var returnString = "";

            var existingUserSocialLinks =
                _userSocialLinksProvider.FindBy(x => x.UserId == userProfileDTO.UserId).FirstOrDefault();

            if (existingUserSocialLinks != null)
            {
                existingUserSocialLinks.YouTube = userProfileDTO.UserSocialLinks.YouTube;
                existingUserSocialLinks.Twitch = userProfileDTO.UserSocialLinks.Twitch;
                existingUserSocialLinks.Facebook = userProfileDTO.UserSocialLinks.Facebook;
                existingUserSocialLinks.Faceit = userProfileDTO.UserSocialLinks.Faceit;
                existingUserSocialLinks.Steam = userProfileDTO.UserSocialLinks.Steam;
                existingUserSocialLinks.Mixer = userProfileDTO.UserSocialLinks.Mixer;
                existingUserSocialLinks.Instagram = userProfileDTO.UserSocialLinks.Instagram;
                existingUserSocialLinks.SoStronk = userProfileDTO.UserSocialLinks.SoStronk;
                existingUserSocialLinks.Twitter = userProfileDTO.UserSocialLinks.Twitter;
                existingUserSocialLinks.Discord = userProfileDTO.UserSocialLinks.Discord;
                var updatedSocialLinks = _userSocialLinksProvider.Update(existingUserSocialLinks);

                if (updatedSocialLinks != null)
                    returnString = "success";
            }
            else
            {
                if (userProfileDTO.UserSocialLinks != null)
                {
                    var newUserSocialLinks = new UserSocialLinks
                    {
                        UserId = userProfileDTO.UserId,
                        Facebook = userProfileDTO.UserSocialLinks.Facebook,
                        YouTube = userProfileDTO.UserSocialLinks.YouTube,
                        Twitter = userProfileDTO.UserSocialLinks.Twitter,
                        Twitch = userProfileDTO.UserSocialLinks.Twitch,
                        Steam = userProfileDTO.UserSocialLinks.Steam,
                        Mixer = userProfileDTO.UserSocialLinks.Mixer,
                        Instagram = userProfileDTO.UserSocialLinks.Instagram,
                        SoStronk = userProfileDTO.UserSocialLinks.SoStronk,
                        Discord = userProfileDTO.UserSocialLinks.Discord
                    };

                    var newSocialLinksObj = _userSocialLinksProvider.Add(newUserSocialLinks);

                    if (newSocialLinksObj != null)
                        returnString = "success";
                }
            }


            return returnString;
        }


        private string SaveUserAchievements(UserProfileDTO userProfileDTO)
        {
            var returnString = "";

            var existingUserAchievements =
                _userAchievementProvider.FindBy(x => x.UserId == userProfileDTO.UserId).ToList();

            // insert new achievements for a user who doesn't have any
            if (!existingUserAchievements.Any())
            {
                foreach (var achievement in userProfileDTO.UserAchievements)
                {
                    var newAchievement = new UserAchievement
                    {
                        UserId = userProfileDTO.UserId,
                        Name = achievement.Name,
                        Description = achievement.Description,
                        Location = achievement.Location,
                        Position = achievement.Position,
                        Type = achievement.Type,
                        Date =
                            DateTime.ParseExact(achievement.Date, @"dd/MM/yyyy",
                                CultureInfo.InvariantCulture),
                        IsActive = true
                    };

                    var newUserAchievementObj = _userAchievementProvider.Add(newAchievement);
                    if (newUserAchievementObj != null)
                    {
                        achievement.Id = newUserAchievementObj.Id;
                        returnString = "success";
                    }

                }
            }
            else
            {
                // update existing achievements
                var existingAchievements = userProfileDTO.UserAchievements.FindAll(x => x.Id != 0).ToList();

                if (existingAchievements.Any())
                {
                    foreach (var currentAchievement in existingAchievements)
                    {
                        var achievement =
                            _userAchievementProvider.FindBy(x => x.Id == currentAchievement.Id).FirstOrDefault();

                        if (achievement != null)
                        {
                            achievement.Name = currentAchievement.Name;
                            achievement.Description = currentAchievement.Description;
                            achievement.Location = currentAchievement.Location;
                            achievement.Position = currentAchievement.Position;
                            achievement.Type = currentAchievement.Type;
                            achievement.Date = DateTime.ParseExact(currentAchievement.Date, @"dd/MM/yyyy",
                                CultureInfo.InvariantCulture);

                            var returnExistingAchievement = _userAchievementProvider.Update(achievement);

                            if (returnExistingAchievement != null)
                                returnString = "success";
                        }
                    }
                }
            }


            // insert all new achievements
            var newAchievements = userProfileDTO.UserAchievements.FindAll(x => x.Id == 0).ToList();
            if (newAchievements.Any())
            {
                foreach (var achievement in newAchievements)
                {
                    var newAchievement = new UserAchievement
                    {
                        UserId = userProfileDTO.UserId,
                        Name = achievement.Name,
                        Description = achievement.Description,
                        Location = achievement.Location,
                        Position = achievement.Position,
                        Type = achievement.Type,
                        Date =
                            DateTime.ParseExact(achievement.Date, @"dd/MM/yyyy",
                                CultureInfo.InvariantCulture),
                        IsActive = true
                    };

                    var newUserAchievementObj = _userAchievementProvider.Add(newAchievement);
                    if (newUserAchievementObj != null)
                        returnString = "success";
                }
            }


            return returnString;
        }


        private string SaveUserPeripheralData(UserProfileDTO userProfileDTO)
        {
            var returnString = "";

            var existingPeripheralData = _userPeripheralsProvider.FindBy(x => x.UserId == userProfileDTO.UserId).ToList();

            // insert new peripherals for a user who doesn't have any
            if (!existingPeripheralData.Any())
            {
                foreach (var peripheral in userProfileDTO.UserPeripheralData)
                {
                    var newPeriheral = new UserPeripherals
                    {
                        UserId = userProfileDTO.UserId,
                        AffiliateLink = peripheral.AffiliateLink,
                        CompanyName = peripheral.CompanyName,
                        PeripheralType = peripheral.PeripheralType,
                        ProductDetails = peripheral.ProductDetails
                    };

                    var newUserPeripheralObj = _userPeripheralsProvider.Add(newPeriheral);
                    if (newUserPeripheralObj != null)
                    {
                        peripheral.Id = newUserPeripheralObj.Id;
                        returnString = "success";
                    }

                }
            }
            else
            {
                // update existing peripherals
                var existingPeripherals = userProfileDTO.UserPeripheralData.FindAll(x => x.Id != 0).ToList();

                if (existingPeripherals.Any())
                {
                    foreach (var currentPeripheral in existingPeripherals)
                    {
                        var peripheral =
                            _userPeripheralsProvider.FindBy(x => x.Id == currentPeripheral.Id).FirstOrDefault();

                        if (peripheral != null)
                        {
                            peripheral.AffiliateLink = currentPeripheral.AffiliateLink;
                            peripheral.CompanyName = currentPeripheral.CompanyName;
                            peripheral.PeripheralType = currentPeripheral.PeripheralType;
                            peripheral.ProductDetails = currentPeripheral.ProductDetails;

                            var returnExistingPeripheral = _userPeripheralsProvider.Update(peripheral);

                            if (returnExistingPeripheral != null)
                                returnString = "success";
                        }
                    }
                }
            }


            // insert all new peripherals
            var newPeripherals = userProfileDTO.UserPeripheralData.FindAll(x => x.Id == 0).ToList();
            if (newPeripherals.Any())
            {
                foreach (var peripheral in newPeripherals)
                {

                    var newPeriheral = new UserPeripherals
                    {
                        UserId = userProfileDTO.UserId,
                        AffiliateLink = peripheral.AffiliateLink,
                        CompanyName = peripheral.CompanyName,
                        PeripheralType = peripheral.PeripheralType,
                        ProductDetails = peripheral.ProductDetails
                    };


                    var newPeripheraltObj = _userPeripheralsProvider.Add(newPeriheral);
                    if (newPeripheraltObj != null)
                        returnString = "success";
                }
            }


            return returnString;
        }


        private string SaveUserTeamHistory(UserProfileDTO userProfileDTO)
        {
            var returnString = "";

            var existingUserTeamHistory = _userTeamHistoryProvider.FindBy(x => x.UserId == userProfileDTO.UserId).ToList();

            // insert new team history for a user who doesn't have any
            if (!existingUserTeamHistory.Any())
            {
                foreach (var teamHistory in userProfileDTO.UserTeamHistory)
                {
                    var newTeamHistoryitem = new UserTeamHistory
                    {
                        UserId = userProfileDTO.UserId,
                        TeamName = teamHistory.TeamName,
                        FromDate = teamHistory.FromDate,
                        ToDate = teamHistory.ToDate
                    };

                    var newUserTeamHistoryObj = _userTeamHistoryProvider.Add(newTeamHistoryitem);
                    if (newUserTeamHistoryObj != null)
                    {
                        teamHistory.Id = newUserTeamHistoryObj.Id;
                        returnString = "success";
                    }

                }
            }
            else
            {
                // update existing user team history
                var existingTeamHistory = userProfileDTO.UserTeamHistory.FindAll(x => x.Id != 0).ToList();

                if (existingTeamHistory.Any())
                {
                    foreach (var currentTeamHistory in existingTeamHistory)
                    {
                        var teamHistory = _userTeamHistoryProvider.FindBy(x => x.Id == currentTeamHistory.Id).FirstOrDefault();

                        if (teamHistory != null)
                        {
                            teamHistory.TeamName = currentTeamHistory.TeamName;
                            teamHistory.FromDate = currentTeamHistory.FromDate;
                            teamHistory.ToDate = currentTeamHistory.ToDate;

                            var returnExitingTeamName = _userTeamHistoryProvider.Update(teamHistory);

                            if (returnExitingTeamName != null)
                                returnString = "success";
                        }
                    }
                }
            }


            // insert all new team history
            var newTeamHistory = userProfileDTO.UserTeamHistory.FindAll(x => x.Id == 0).ToList();
            if (newTeamHistory.Any())
            {
                foreach (var teamhistory in newTeamHistory)
                {
                    var newTeamHistoryItem = new UserTeamHistory
                    {
                        UserId = userProfileDTO.UserId,
                        TeamName = teamhistory.TeamName,
                        FromDate = teamhistory.FromDate,
                        ToDate = teamhistory.ToDate
                    };

                    var newUserTeamHistoryObj = _userTeamHistoryProvider.Add(newTeamHistoryItem);
                    if (newUserTeamHistoryObj != null)
                        returnString = "success";
                }
            }


            return returnString;
        }


        private string RemoveUserAchievements(List<string> removedAchievements)
        {
            var returnString = "";
            if (removedAchievements.Any())
            {
                var ids = string.Join(",", removedAchievements);
                returnString = _userAchievementProvider.DeleteUserAchievements(ids);
            }


            return returnString;
        }


        private string RemoveUserTeamHistory(List<string> removedUserTeamHistory)
        {
            var returnString = "";
            if (removedUserTeamHistory.Any())
            {
                var ids = string.Join(",", removedUserTeamHistory);
                returnString = _userTeamHistoryProvider.DeleteUserTeamHistory(ids);
            }


            return returnString;
        }

        private string RemoveUserPeripherals(List<string> removedUserPeripherals)
        {
            var returnString = "";
            if (removedUserPeripherals.Any())
            {
                var ids = string.Join(",", removedUserPeripherals);
                returnString = _userPeripheralsProvider.DeleteUserPeripheral(ids);
            }


            return returnString;
        }



        private string SaveUserProfileExtension(UserProfileDTO userProfileDTO)
        {
            var returnString = "";

            var existingUserProfileExtensions = _profileExtensionProvider.FindBy(x => x.UserId == userProfileDTO.UserId).FirstOrDefault();

            if (existingUserProfileExtensions != null)
            {
                existingUserProfileExtensions.Alias = userProfileDTO.UserProfileExtension.Alias;
                existingUserProfileExtensions.About = userProfileDTO.UserProfileExtension.About;
                existingUserProfileExtensions.PrimaryGame = userProfileDTO.UserProfileExtension.PrimaryGame;
                existingUserProfileExtensions.SecondaryGame = userProfileDTO.UserProfileExtension.SecondaryGame;
                existingUserProfileExtensions.PrimaryRole = userProfileDTO.UserProfileExtension.PrimaryRole;
                existingUserProfileExtensions.SecondaryRole = userProfileDTO.UserProfileExtension.SecondaryRole;
                existingUserProfileExtensions.Status = userProfileDTO.UserProfileExtension.Status;
                existingUserProfileExtensions.PrimaryGameExp = userProfileDTO.UserProfileExtension.PrimaryGameExp;
                existingUserProfileExtensions.SecondaryGameExp = userProfileDTO.UserProfileExtension.SecondaryGameExp;
                existingUserProfileExtensions.CurrentTeam = userProfileDTO.UserProfileExtension.CurrentTeam;

                var updatedUserProfileExtension = _profileExtensionProvider.Update(existingUserProfileExtensions);

                if (updatedUserProfileExtension != null)
                    returnString = "success";
            }
            else
            {
                if (userProfileDTO.UserProfileExtension != null)
                {
                    var newUserProfileExtension = new UserProfileExtension
                    {
                        UserId = userProfileDTO.UserId,
                        Alias = userProfileDTO.UserProfileExtension.Alias,
                        About = userProfileDTO.UserProfileExtension.About,
                        PrimaryGame = userProfileDTO.UserProfileExtension.PrimaryGame,
                        SecondaryGame = userProfileDTO.UserProfileExtension.SecondaryGame,
                        PrimaryRole = userProfileDTO.UserProfileExtension.PrimaryRole,
                        SecondaryRole = userProfileDTO.UserProfileExtension.SecondaryRole,
                        Status = userProfileDTO.UserProfileExtension.Status,
                        PrimaryGameExp = userProfileDTO.UserProfileExtension.PrimaryGameExp,
                        SecondaryGameExp = userProfileDTO.UserProfileExtension.SecondaryGameExp,
                        CurrentTeam = userProfileDTO.UserProfileExtension.CurrentTeam
                    };



                    var newUserProfileExtn = _profileExtensionProvider.Add(newUserProfileExtension);

                    if (newUserProfileExtn != null)
                        returnString = "success";
                }
            }


            return returnString;
        }

        private GamePlatformConnectivityDTO GetUserGamePlayformConnectivity(int userId)
        {
            return
                _gamePlatformConnectivityProvider.FindBy(x => x.UserId == userId)
                    .Select(x => new GamePlatformConnectivityDTO
                    {
                        SteamId = x.SteamId,
                        ESLId = x.ESLId,
                        SteamId64 = x.SteamId64,
                        FaceitId = x.FaceitId,
                        ESEA = x.ESEA,
                        SoSotronkId = x.SoStronkId
                    }).FirstOrDefault();
        }

        private List<UserSetupDTO> GetUserSetups(int userId)
        {
            return _userSetupsProvider.FindBy(x => x.UserId == userId).Select(x => new UserSetupDTO
            {
                Id = x.Id,
                UserId = x.UserId,
                SetupTypeId = x.SetupTypeId,
                SetupImagePath = GetSetupTopImage(userId, x.Id),
                SetupName = x.SetupName


            }).ToList();


        }


        private string GetSetupTopImage(int userId, int setupId)
        {
            var returnString = "/CustomContent/Images/setupPlaceholder.jpg";

            var userFileLocation = ConfigurationManager.AppSettings["UserFiles"] + userId + "\\SetupImages\\" + setupId + "\\";

            try
            {
                if (Directory.Exists(userFileLocation))
                {
                    var files = Directory.GetFiles(userFileLocation, "*.*", SearchOption.TopDirectoryOnly);

                    if (files.Length > 0)
                    {
                        FileInfo file = new FileInfo(files[0]);
                        returnString = "/UserData/" + userId + "/SetupImages/" + setupId + "/" + file.Name;
                    }

                }
            }
            catch (Exception)
            {
            }
            return returnString;
        }

        private List<UserSetupDataDTO> GetUserSetupData(int userId)
        {

            return _userSetupDataProvider.FindBy(x => x.UserId == userId).Select(x => new UserSetupDataDTO
            {
                Id = x.Id,
                UserId = x.UserId,
                SetupId = x.UserSetupId,
                CompanyName = x.CompanyName,
                Component = x.Component,
                ProductDetails = x.ProductDetails,
                AffiliateLink = x.AffiliateLink
            }).ToList();
        }

        private List<UserPeripheralDTO> GetUserPeripheralData(int userId)
        {

            return _userPeripheralsProvider.FindBy(x => x.UserId == userId).Select(x => new UserPeripheralDTO
            {
                Id = x.Id,
                UserId = x.UserId,
                CompanyName = x.CompanyName,
                PeripheralType = x.PeripheralType,
                ProductDetails = x.ProductDetails,
                AffiliateLink = x.AffiliateLink,
                PeripheralTopimage = GetPeripheralTopImage(userId)
            }).ToList();
        }


        private string GetPeripheralTopImage(int userId)
        {

            var returnString = "/CustomContent/Images/setupPlaceholder.jpg";

            var userFileLocation = ConfigurationManager.AppSettings["UserFiles"] + userId + "\\PeripheralImages\\";

            try
            {
                if (Directory.Exists(userFileLocation))
                {
                    var files = Directory.GetFiles(userFileLocation, "*.*", SearchOption.TopDirectoryOnly);

                    if (files.Length > 0)
                    {
                        FileInfo file = new FileInfo(files[0]);
                        returnString = "/UserData/" + userId + "/PeripheralImages/" + file.Name;
                    }

                }
            }
            catch (Exception)
            {
            }
            return returnString;
        }

    }
}