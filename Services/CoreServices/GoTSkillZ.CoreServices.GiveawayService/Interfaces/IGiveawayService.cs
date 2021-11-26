using System.Collections.Generic;
using GoTSkillZ.Application.Transforms.DTO;
using GoTSkillZ.Models.Giveaway.Data;

namespace GoTSkillZ.CoreServices.GiveawayService.Interfaces
{
    public interface IGiveawayService
    {
        List<Giveaway> GetAllGiveaways();
        Giveaway SaveGiveaway(GiveawayDTO giveawayObj);

        string GenerateEntryCode(string giveawayId, string userId);

        List<GiveawayEntryCode> GetAllUserGiveawayEntries(string userId);

        List<string> GetGiveawayEntriesByGiveawayId(int giveawayId);

        List<GiveawayWinnerTopListDTO> GetGiveAwayWinner(int giveawayId);

        GiveawayWinnerDTO SaveGiveawayWinner(GiveawayWinnerDTO giveawayObj);

        GiveawayWinnerDTO CheckIfHasWinner(int giveawayId);

        List<GiveawayWinnerDTO> GetGiveAwayWinners();
    }
}