using System.Collections.Generic;
using GoTSkillZ.Application.Transforms.DTO;
using GoTSkillZ.CoreServices.GiveawayService.Interfaces;
using GoTSkillZ.CoreServices.GiveawayService.Services;
using GoTSkillZ.Models.Giveaway.Data;

namespace GoTSkillZ.Web.UI.Site.WCF
{
  
    public class GiveawayAPI : IGiveawayAPI
    {
        private readonly IGiveawayService _giveawayService;

        public GiveawayAPI()
        {
            _giveawayService = new GiveawayService();
        }

        public List<Giveaway> GetAllGiveaways()
        {
            return _giveawayService.GetAllGiveaways();
        }

        public Giveaway SaveGiveaway(GiveawayDTO giveawayObj)
        {
            return _giveawayService.SaveGiveaway(giveawayObj);
        }

        public string GenerateEntryCode(string giveawayId, string userId)
        {
            return _giveawayService.GenerateEntryCode(giveawayId,userId);
        }

        public List<GiveawayEntryCode> GetAllUserGiveawayEntries(string userId)
        {
            return _giveawayService.GetAllUserGiveawayEntries(userId);
        }

        public List<string> GetGiveawayEntriesByGiveawayId(string giveawayId)
        {
            return _giveawayService.GetGiveawayEntriesByGiveawayId(int.Parse(giveawayId));
        }

        public List<GiveawayWinnerTopListDTO> GetGiveAwayWinner(string giveawayId)
        {
            return _giveawayService.GetGiveAwayWinner(int.Parse(giveawayId));
        }

        public GiveawayWinnerDTO SaveGiveawayWinner(GiveawayWinnerDTO giveawayObj)
        {
            return _giveawayService.SaveGiveawayWinner(giveawayObj);
        }

        public GiveawayWinnerDTO CheckIfHasWinner(string giveawayId)
        {
            return  _giveawayService.CheckIfHasWinner(int.Parse(giveawayId));
        }

        public List<GiveawayWinnerDTO> GetGiveAwayWinners()
        {
            return _giveawayService.GetGiveAwayWinners();
        }
    }
}