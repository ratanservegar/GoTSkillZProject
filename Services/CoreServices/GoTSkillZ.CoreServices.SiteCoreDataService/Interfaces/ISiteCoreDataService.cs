using GoTSkillZ.Application.Transforms.DTO;
using System.Collections.Generic;

namespace GoTSkillZ.CoreServices.SiteCoreDataService.Interfaces
{
    public interface ISiteCoreDataService
    {
        int GetCurrentSubCount();

        int GetCurrentSponsorCount();

        List<RecentSubDTO> GetNewSiteUsers();

        List<RecentSubDTO> GetRecentSponsors();



        List<RecentSubDTO> GetAllYouTubeSubscribers();


        string AddYouTubeSubList(List<YouTubeSubscriberListDTO> youTubeSubscriberList);

        string UpdateYouTubeStatistics(YouTubeStatisticsDTO youTubeStatisticsDto);

        List<DonationDTO> GetDonations();

    }
}