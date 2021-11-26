using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using GoTSkillZ.Application.Transforms.DTO;
using GoTSkillZ.CoreServices.GiveawayService.Interfaces;
using GoTSkillZ.DataUtilities.Core.Interfaces;
using GoTSkillZ.DataUtilities.Core.Services;
using GoTSkillZ.Models.Giveaway.Data;
using GoTSkillZ.Models.Giveaway.Interfaces;
using GoTSkillZ.Models.Giveaway.Provider;
using GoTSkillZ.Models.Membership.Interfaces;
using GoTSkillZ.Models.Membership.Providers;
using SqlParameterDTO = GoTSkillZ.DataUtilities.Core.Attribute.SqlParameterDTO;

namespace GoTSkillZ.CoreServices.GiveawayService.Services
{
    public class GiveawayService : IGiveawayService
    {
        private readonly IDbUtility _dbUtility;
        private readonly IGiveawayEntryCodeProvider _giveawayEntryCodeProvider;
        private readonly IGiveawayProvider _giveawayProvider;
        private readonly IGiveawayWinnersProvider _giveawayWinnersProvider;
        private readonly IUserProvider _userProvider;

        public GiveawayService()
        {
            _giveawayProvider = new GiveawayProvider();
            _giveawayEntryCodeProvider = new GiveawayEntryCodeProvider();
            _giveawayWinnersProvider = new GiveawayWinnersProvider();
            _userProvider = new UserProvider();
            _dbUtility = new DbUtility();
        }

        public List<Giveaway> GetAllGiveaways()
        {
            var allGiveaway = _giveawayProvider.GetAll().ToList();

            if (allGiveaway.Any())
                foreach (var giveaway in allGiveaway)
                    giveaway.TotalEntries = GetGiveawayEntryCount(giveaway.Id);

            return allGiveaway;
        }

        public Giveaway SaveGiveaway(GiveawayDTO giveawayObj)
        {
            if (giveawayObj == null) return null;
            var returnObject = new Giveaway();
            //update
            if (giveawayObj.Id != 0)
            {
                var existingGiveaway = _giveawayProvider.FindBy(x => x.Id == giveawayObj.Id).FirstOrDefault();
                if (existingGiveaway != null)
                {
                    existingGiveaway.Title = giveawayObj.Title;
                    existingGiveaway.Description = giveawayObj.Description;
                    existingGiveaway.Rules = giveawayObj.Rules;
                    existingGiveaway.Code = giveawayObj.Code;
                    existingGiveaway.International = giveawayObj.International;
                    existingGiveaway.Active = giveawayObj.Active;
                    existingGiveaway.Sponsored = giveawayObj.Sponsored;
                    existingGiveaway.ImageUrl = giveawayObj.ImageUrl;
                    existingGiveaway.VideoUrl = giveawayObj.VideoUrl;
                    existingGiveaway.TotalEntries = giveawayObj.TotalEntries;
                    existingGiveaway.ModifiedDate = DateTime.Now;
                    returnObject = _giveawayProvider.Update(existingGiveaway);
                }
            }
            else
            {
                // add new

                var newGiveawayObject = new Giveaway
                {
                    Title = giveawayObj.Title,
                    Code = giveawayObj.Code,
                    Description = giveawayObj.Description,
                    ImageUrl = giveawayObj.ImageUrl,
                    Rules = giveawayObj.Rules,
                    TotalEntries = giveawayObj.TotalEntries,
                    VideoUrl = giveawayObj.VideoUrl,
                    International = giveawayObj.International,
                    Sponsored = giveawayObj.Sponsored,
                    Active = giveawayObj.Active,
                    CreatedDate = DateTime.Now
                };

                returnObject = _giveawayProvider.Add(newGiveawayObject);
            }


            return returnObject;
        }

        public string GenerateEntryCode(string giveawayId, string userId)
        {
            if (string.IsNullOrEmpty(userId)) return "Empty UserId Passed";
            if (string.IsNullOrEmpty(giveawayId)) return "Empty GiveawayId Passed";

            var returnString = "";
            //check if the user already has a entry for the given giveaway

            var _giveawayId = int.Parse(giveawayId);
            var _userId = int.Parse(userId);
            var existingEntry = _giveawayEntryCodeProvider
                .FindBy(x => x.GiveawayId == _giveawayId && x.UserId == _userId).FirstOrDefault();

            if (existingEntry != null)
            {
                returnString = existingEntry.GiveawayCode;
            }
            else
            {
                var userData = _userProvider.Get(_userId);
                var giveawayData = _giveawayProvider.FindBy(x => x.Id == _giveawayId).FirstOrDefault();
                if (userData != null && giveawayData != null)
                {
                    returnString = _giveawayId + "-" + giveawayData.Code + "-" + userData.GooglePublicId + "-GS";

                    var newEntry = new GiveawayEntryCode
                    {
                        GiveawayId = _giveawayId,
                        UserId = _userId,
                        GiveawayCode = returnString,
                        CreatedDate = DateTime.Now
                    };
                    _giveawayEntryCodeProvider.Add(newEntry);
                }
            }

            return returnString;
        }

        public List<GiveawayEntryCode> GetAllUserGiveawayEntries(string userId)
        {
            if (string.IsNullOrEmpty(userId)) return null;
            var _userId = int.Parse(userId);
            return _giveawayEntryCodeProvider.FindBy(x => x.UserId == _userId).ToList();
        }

        public List<string> GetGiveawayEntriesByGiveawayId(int giveawayId)
        {
            var returnList = new List<string>();
            if (giveawayId == 0) return returnList;

            var entries = _giveawayEntryCodeProvider.FindBy(x => x.GiveawayId == giveawayId).ToList();


            if (entries.Any())
                foreach (var entry in entries)
                {
                    if (entry.UserId == 13 || entry.UserId == 1032) continue;
                    var user = _userProvider.FindBy(x => x.UserId == entry.UserId).FirstOrDefault();

                    if (user != null)
                    {
                        var name = user.FirstName + " " + user.LastName;

                        returnList.Add(name);
                    }
                }

            return returnList.OrderBy(q => q).ToList();
        }

        public List<GiveawayWinnerTopListDTO> GetGiveAwayWinner(int giveawayId)
        {
            var returnList = new List<GiveawayWinnerTopListDTO>();
            if (giveawayId == 0) return returnList;


            var parameterList = new List<SqlParameterDTO>
            {
                new SqlParameterDTO {Parameter = "@GiveawayId", Value = giveawayId}
            };
            var dt = _dbUtility.RunStoredProcedure("GetGiveawayWinner", parameterList);


            if (dt.Tables.Count == 0) return returnList;
            if (dt.Tables[0].Rows.Count == 0) return returnList;

            //Process Survey History
            var winnerTable = dt.Tables[0];

            returnList = winnerTable.AsEnumerable().Select(x => new GiveawayWinnerTopListDTO
            {
                UserId = x.Field<int>("UserId"),
                Name = x.Field<string>("Name"),
                Email = x.Field<string>("Email"),
                GiveawayId = x.Field<int>("GiveawayId"),
                EntryCode = x.Field<string>("EntryCode"),
                EntryDate = x.Field<DateTime>("EntryDate").ToString("dd-MM-yyyy"),
                Bucket = x.Field<int>("BucketNumber")
            }).ToList();


            return returnList;
        }

        public GiveawayWinnerDTO SaveGiveawayWinner(GiveawayWinnerDTO giveawayObj)
        {
            if (giveawayObj.GiveawayId == 0 && giveawayObj.UserId == 0) return giveawayObj;



            var existingWinner = _giveawayWinnersProvider
                .FindBy(x => x.GiveawayId == giveawayObj.GiveawayId).FirstOrDefault();

            if (existingWinner == null)
            {
                var winnerObj = new GiveawayWinner
                {
                    GiveawayId = giveawayObj.GiveawayId,
                    UserId = giveawayObj.UserId,
                    WinnerImageUrl = giveawayObj.WinnerImageUrl,
                    CreatedDate = DateTime.Now
                };

                var obj = _giveawayWinnersProvider.Add(winnerObj);
                giveawayObj.Id = obj.Id;
            }
            else
            {
                existingWinner.UserId = giveawayObj.UserId;
                existingWinner.WinnerImageUrl = giveawayObj.WinnerImageUrl;
                existingWinner.CreatedDate = DateTime.Now;
                giveawayObj.Id = existingWinner.Id;

                _giveawayWinnersProvider.Update(existingWinner);
            }


            return giveawayObj;
        }

        public GiveawayWinnerDTO CheckIfHasWinner(int giveawayId)
        {
            if (giveawayId == 0) return null;
            var giveawayWinnerObj = new GiveawayWinnerDTO();
            var giveawayWinner = _giveawayWinnersProvider.FindBy(x => x.GiveawayId == giveawayId).FirstOrDefault();

            if (giveawayWinner != null)
            {
                giveawayWinnerObj.Id = giveawayWinner.Id;
                giveawayWinnerObj.GiveawayId = giveawayWinner.GiveawayId;
                giveawayWinnerObj.UserId = giveawayWinner.UserId;
                giveawayWinnerObj.WinnerImageUrl = giveawayWinner.WinnerImageUrl;
                giveawayWinnerObj.WinnerName = GetWinnerName(giveawayWinner.UserId);
                giveawayWinnerObj.WinDate = giveawayWinner.CreatedDate.ToString("dd-MM-yyyy");
            }

            return giveawayWinnerObj;
        }

        public List<GiveawayWinnerDTO> GetGiveAwayWinners()
        {

            var returnList = new List<GiveawayWinnerDTO>();
            var winners = _giveawayWinnersProvider.GetAll().ToList();

            if (winners.Any())
            {
                foreach (var winner in winners)
                {
                    var winnerObj = new GiveawayWinnerDTO
                    {
                        GiveawayTitle = GetGiveawayTitle(winner.GiveawayId),
                        GiveawayId = winner.GiveawayId,
                        Id = winner.Id,
                        UserId = winner.UserId,
                        WinnerName = GetWinnerName(winner.UserId),
                        WinDate = winner.CreatedDate.ToString("dd-MM-yyyy"),
                        WinnerImageUrl = winner.WinnerImageUrl,
                        WinningEntryCode = GetWinningEntryCode(winner.UserId,winner.GiveawayId)
                    };

                    returnList.Add(winnerObj);
                }
            }

            return returnList;
        }

        private int GetGiveawayEntryCount(int giveawayId)
        {
            return _giveawayEntryCodeProvider.FindBy(x => x.GiveawayId == giveawayId).ToList().Count();
        }

        private string GetWinningEntryCode(int userId, int giveawayId)
        {
            return _giveawayEntryCodeProvider.FindBy(x => x.GiveawayId == giveawayId && x.UserId == userId)
                .FirstOrDefault().GiveawayCode;
        }


        private string GetGiveawayTitle(int giveawayId)
        {
            return _giveawayProvider.FindBy(x => x.Id == giveawayId).FirstOrDefault().Title;
        }

        private string GetWinnerName(int userId)
        {
            var name = "";
            var user = _userProvider.FindBy(x => x.UserId == userId).FirstOrDefault();

            if (user != null)
                name = user.FirstName + " " + user.LastName;


            return name;
        }
    }
}