using GoTSkillZ.Application.Transforms.DTO;
using GoTSkillZ.CoreServices.SiteCoreDataService.Interfaces;
using GoTSkillZ.Models.Donation.Interfaces;
using GoTSkillZ.Models.Donation.Provider;
using GoTSkillZ.Models.Membership.Data;
using GoTSkillZ.Models.Membership.Interfaces;
using GoTSkillZ.Models.Membership.Providers;
using GoTSkillZ.Models.UserDataExtension.Interfaces;
using GoTSkillZ.Models.UserDataExtension.Provider;
using GoTSkillZ.Models.YouTube.Data;
using GoTSkillZ.Models.YouTube.Interfaces;
using GoTSkillZ.Models.YouTube.Provider;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using GoTSkillZ.DataUtilities.Core.Interfaces;
using GoTSkillZ.DataUtilities.Core.Services;

namespace GoTSkillZ.CoreServices.SiteCoreDataService.Services
{
    public class SiteCoreDataService : ISiteCoreDataService
    {
        private readonly IUserProfileExtensionProvider _profileExtensionProvider;
        private readonly IUserProvider _userProvider;
        private readonly IYouTubeChannelStatisticsProvider _youTubeChannelStatisticsProvider;
        private readonly IYouTubeSponsorsProvider _youTubeSponsorsProvider;
        private readonly IYouTubeSubscriberListProvider _youTubeSubscriberList;
        private readonly IDonationProvider _donationProvider;
        private readonly IYouTubeSubscribersProvider _youTubeSubscribersProvider;
        private readonly IDbUtility _dbUtility;

        public SiteCoreDataService()
        {
            _youTubeSubscribersProvider = new YouTubeSubscribersProvider();
            _youTubeSponsorsProvider = new YouTubeSponsorsProvider();
            _userProvider = new UserProvider();
            _profileExtensionProvider = new UserProfileExtensionProvider();
            _youTubeSubscriberList = new YouTubeSubscriberListProvider();
            _youTubeChannelStatisticsProvider = new YouTubeChannelStatisticsProvider();
            _donationProvider = new DonationProvider();
            _dbUtility = new DbUtility();
        }

        public int GetCurrentSubCount()
        {
            var dataTable = _dbUtility.RunSqlQuery("SELECT SubCount FROM YouTubeChannelStatistics WITH(NOLOCK)");

            return (int)dataTable.Rows[0]["SubCount"];

        }

        public int GetCurrentSponsorCount()
        {
            return _youTubeSponsorsProvider.FindBy(x => x.IsSponsor).ToList().Count;
        }

        public List<RecentSubDTO> GetNewSiteUsers()
        {
            var returnList = new List<RecentSubDTO>();
            var subUsers =
                _youTubeSubscribersProvider.FindBy(x => x.IsSubscribed)
                    .OrderByDescending(x => x.SubscribedDate)
                    .ToList().Take(10);

            var youTubeSubscribers = subUsers.ToList();
            if (youTubeSubscribers.Any())
            {
                foreach (var user in youTubeSubscribers)
                {
                    var userAlias = "";
                    var userObj = _userProvider.FindBy(x => x.UserId == user.UserId).FirstOrDefault();
                    var userProfileExtension =
                        _profileExtensionProvider.FindBy(x => x.UserId == user.UserId).FirstOrDefault();
                    if (userProfileExtension != null)
                    {
                        userAlias = userProfileExtension.Alias;
                    }

                    var userDataPath = ConfigurationManager.AppSettings["UserFiles"] + "\\" + user.UserId +
                                       "\\ProfileImage\\ProfileImage.jpg";

                    var recentSubItem = new RecentSubDTO();

                    recentSubItem.UserId = user.UserId;
                    if (userAlias != "")
                    {
                        if (userObj != null)
                            recentSubItem.SubName = userObj.FirstName + " \"<span class='tx-teal'>" + userAlias +
                                                    "\"</span> " + userObj.LastName;
                    }
                    else
                    {
                        if (userObj != null) recentSubItem.SubName = userObj.FirstName + " " + userObj.LastName;
                    }

                    if (user.SubscribedDate != null)
                        recentSubItem.SubDate = user.SubscribedDate.Value.ToString("dd/MM/yyy");

                    if (File.Exists(userDataPath))
                    {
                        recentSubItem.SubImage = "/UserData/" + user.UserId + "/ProfileImage/ProfileImage.jpg";
                    }
                    else
                    {
                        recentSubItem.SubImage = "/CustomContent/Images/profile-image.png";
                    }

                    returnList.Add(recentSubItem);
                }
            }


            return returnList.OrderByDescending(x => x.SubImage.Contains("ProfileImage")).ToList();
        }

        public List<RecentSubDTO> GetRecentSponsors()
        {
            var returnList = new List<RecentSubDTO>();
            var subUsers =
                _youTubeSponsorsProvider.FindBy(x => x.IsSponsor)
                    .OrderByDescending(x => x.SponsorStartDate)
                    .Take(10)
                    .ToList();

            if (subUsers.Any())
            {
                foreach (var user in subUsers)
                {
                    var userAlias = "";
                    var userObj = _userProvider.FindBy(x => x.UserId == user.UserId).FirstOrDefault();
                    var userProfileExtension =
                        _profileExtensionProvider.FindBy(x => x.UserId == user.UserId).FirstOrDefault();
                    if (userProfileExtension != null)
                    {
                        userAlias = userProfileExtension.Alias;
                    }

                    var userDataPath = ConfigurationManager.AppSettings["UserFiles"] + "\\" + user.UserId +
                                       "\\ProfileImage\\ProfileImage.jpg";

                    var recentSubItem = new RecentSubDTO();
                    recentSubItem.UserId = user.UserId;
                    if (userAlias != "")
                    {
                        if (userObj != null)
                            recentSubItem.SubName = userObj.FirstName + " \"<span class='tx-teal'>" + userAlias +
                                                    "\"</span> " + userObj.LastName;
                    }
                    else
                    {
                        if (userObj != null) recentSubItem.SubName = userObj.FirstName + " " + userObj.LastName;
                    }

                    if (user.SponsorStartDate != null)
                        recentSubItem.SubDate = user.SponsorStartDate.Value.ToString("dd/MM/yyy");

                    if (File.Exists(userDataPath))
                    {
                        recentSubItem.SubImage = "/UserData/" + user.UserId + "/ProfileImage/ProfileImage.jpg";
                    }
                    else
                    {
                        recentSubItem.SubImage = "/CustomContent/Images/profile-image.png";
                    }

                    returnList.Add(recentSubItem);
                }
            }


            return returnList;
        }

        public List<RecentSubDTO> GetAllYouTubeSubscribers()
        {
            var returnList = new List<RecentSubDTO>();
            var youtubeSubs = _youTubeSubscriberList.GetAll().OrderByDescending(x => x.CreatedDate).Take(15).ToList();

            if (youtubeSubs.Any())
            {
                foreach (var sub in youtubeSubs)
                {
                    var recentSubItem = new RecentSubDTO();

                    recentSubItem.UserId = 0;
                    recentSubItem.SubName = sub.Name.ToUpperInvariant();
                    recentSubItem.SubImage = "/CustomContent/Images/profile-image.png";

                    returnList.Add(recentSubItem);
                }
            }


            return returnList;
        }

        public string AddYouTubeSubList(List<YouTubeSubscriberListDTO> youTubeSubscriberList)
        {
            var returnString = "success";

            if (youTubeSubscriberList.Any())
            {
                foreach (var sub in youTubeSubscriberList)
                {
                    var existingSub =
                        _youTubeSubscriberList.FindBy(x => x.ChannelId == sub.ChannelId)
                            .FirstOrDefault();

                    if (existingSub == null)
                    {
                        var newSub = new YouTubeSubscriberList
                        {
                            ChannelId = sub.ChannelId,
                            YoutubeId = sub.YoutubeId,
                            Name = sub.Name,
                            CreatedDate = DateTime.Now
                        };

                        _youTubeSubscriberList.Add(newSub);
                    }
                }
            }

            return returnString;
        }

        public string UpdateYouTubeStatistics(YouTubeStatisticsDTO youTubeStatisticsDto)
        {
            var returnString = "success";

            if (youTubeStatisticsDto != null)
            {
                var existingChannelStat =
                    _youTubeChannelStatisticsProvider.FindBy(x => x.Id == 1).FirstOrDefault();

                if (existingChannelStat == null)
                {
                    var youTubeChannelStatObj = new YouTubeChannelStatistic
                    {
                        SubCount = youTubeStatisticsDto.SubCount,
                        VideoCount = youTubeStatisticsDto.VideoCount,
                        ViewCount = youTubeStatisticsDto.ViewCount,
                        CommentCount = youTubeStatisticsDto.CommentCount,
                        HiddenSubCount = youTubeStatisticsDto.HiddenSubCount
                    };

                    _youTubeChannelStatisticsProvider.Add(youTubeChannelStatObj);
                }
                else
                {
                    existingChannelStat.SubCount = youTubeStatisticsDto.SubCount;
                    existingChannelStat.VideoCount = youTubeStatisticsDto.VideoCount;
                    existingChannelStat.ViewCount = youTubeStatisticsDto.ViewCount;
                    existingChannelStat.HiddenSubCount = youTubeStatisticsDto.HiddenSubCount;
                    existingChannelStat.CommentCount = youTubeStatisticsDto.CommentCount;

                    _youTubeChannelStatisticsProvider.Update(existingChannelStat);
                }
            }

            return returnString;
        }

        public List<DonationDTO> GetDonations()
        {
            var returnObj = new List<DonationDTO>();
            var allDonations = _donationProvider.GetAll().OrderByDescending(x => x.DonationDate);

            if (allDonations.Any())
            {
                foreach (var doner in allDonations)
                {
                    var newDonerObj = new DonationDTO();

                    if (doner.UserId != null)
                    {

                        var user = GetUserInfo((int)doner.UserId);

                        if (user != null)
                        {
                            newDonerObj.UserId = doner.UserId;

                            var userProfileExtension = _profileExtensionProvider.FindBy(x => x.UserId == user.UserId).FirstOrDefault();
                            if (userProfileExtension != null)
                            {
                                newDonerObj.Name = user.FirstName + " \"" + userProfileExtension.Alias + " \" " + user.LastName;
                            }
                            else
                            {
                                newDonerObj.Name = user.FirstName + " " + user.LastName;
                            }
                        }

                    }
                    else
                    {
                        newDonerObj.Name = doner.Name;
                    }

                    newDonerObj.Amount = doner.Amount;
                    newDonerObj.CustomImgUrl = doner.CustomImgUrl;
                    newDonerObj.DonationTitle = doner.DonationTitle;
                    newDonerObj.DonationType = doner.DonationType;
                    newDonerObj.DonationDescription = doner.DonationDescription;
                    newDonerObj.IsActive = doner.IsActive;
                    newDonerObj.DonationDate = doner.DonationDate.Value.ToString("dd/MM/yyy"); ;
                    returnObj.Add(newDonerObj);
                }
            }

            return returnObj;
        }

        private Users GetUserInfo(int userId)
        {
            return _userProvider.FindBy(x => x.UserId == userId).FirstOrDefault();
        }
    }
}