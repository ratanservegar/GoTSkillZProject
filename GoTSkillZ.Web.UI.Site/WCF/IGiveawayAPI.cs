using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using GoTSkillZ.Application.Transforms.DTO;
using GoTSkillZ.Models.Giveaway.Data;

namespace GoTSkillZ.Web.UI.Site.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IGiveawayAPI" in both code and config file together.
    [ServiceContract]
    public interface IGiveawayAPI
    {
        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetAllGiveaways",
            ResponseFormat = WebMessageFormat.Json)]
        List<Giveaway> GetAllGiveaways();

     

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveGiveaway",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Giveaway SaveGiveaway(GiveawayDTO giveawayObj);


        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GenerateEntryCode/{giveawayId}/{userId}",
            ResponseFormat = WebMessageFormat.Json)]
        string GenerateEntryCode(string giveawayId, string userId);


        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetAllUserGiveawayEntries/{userId}",
            ResponseFormat = WebMessageFormat.Json)]
        List<GiveawayEntryCode> GetAllUserGiveawayEntries( string userId);



        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetGiveawayEntriesByGiveawayId/{giveawayId}",
            ResponseFormat = WebMessageFormat.Json)]
        List<string> GetGiveawayEntriesByGiveawayId(string giveawayId);

        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetGiveAwayWinner/{giveawayId}",
            ResponseFormat = WebMessageFormat.Json)]
        List<GiveawayWinnerTopListDTO> GetGiveAwayWinner(string giveawayId);


        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveGiveawayWinner",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        GiveawayWinnerDTO SaveGiveawayWinner(GiveawayWinnerDTO giveawayObj);


        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "CheckIfHasWinner/{giveawayId}",
            ResponseFormat = WebMessageFormat.Json)]
        GiveawayWinnerDTO CheckIfHasWinner(string giveawayId);


        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetGiveAwayWinners",
            ResponseFormat = WebMessageFormat.Json)]
        List<GiveawayWinnerDTO> GetGiveAwayWinners();

        

    }
}