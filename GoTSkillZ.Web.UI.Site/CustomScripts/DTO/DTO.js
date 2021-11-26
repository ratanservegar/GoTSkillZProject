/*jslint browser: true, nomen: true*/
var GoTSkillzEntities = {
    SitemapDTO: function () {
        "use strict";
        this.Id = 0;
        this.Name = "";
        this.TypeId = 0;
        this.ParentId = 0;
        this.PageId = 0;
        this.AlternateUrl = "";
        this.Icon = "";
        this.SortOrder = 0;
        this.CreatedBy = 0;
        this.CreatedDate = null;
        this.ModifiedBy = 0;
        this.ModifiedDate = null;
        this.IsActive = true;
    },
    PageRoleDTO: function () {
        "use strict";
        this.Id = 0;
        this.PageId = 0;
        this.RoleId = 0;
    },
    UserSocialLinksDTO: function () {
        "use strict";

        this.UserId = 0;
        this.Facebook = "";
        this.Instagram = "";
        this.Twitter = "";
        this.Youtube = "";
        this.Faceit = "";
        this.Steam = "";
        this.Twitch = "";
        this.Mixer = "";
        this.SoStronk = "";
        this.Discord = "";
    },
    UserAchievementsDTO: function () {
        "use strict";

        this.Id = 0;
        this.UserId = 0;
        this.Name = "";
        this.Description = "";
        this.Position = "";
        this.Type = "";
        this.Location = "";
        this.Date = "";
        this.IsActive = "";
    },
    UserProfileExtensionDTO: function () {
        "use strict";

        this.Alias = "";
        this.About = "";
        this.PrimaryGame = "";
        this.SecondaryGame = "";
        this.PrimaryRole = "";
        this.SecondaryRole = "";
        this.Status = "";
        this.PrimaryGameExp = "";
        this.SecondaryGameExp = "";
    },
    UserSetupDataDTO: function () {
        "use strict";
        this.Id = 0;
        this.CompanyName = "No Information Provided";
        this.SetupTypeName = "No Information Provided";
        this.SetupName = "";
        this.SetupTypeId = 0;
        this.Component = "No Information Provided";
        this.ProductDetails = "No Information Provided";
        this.AffiliateLink = "No Information Provided";
        this.SetupId = 0;
        this.UserId = 0;
    },
    UserTeamHistoryDTO: function () {
        "use strict";
        this.Id = 0;
        this.TeamName = "";
        this.FromDate = "";
        this.ToDate = "";
    },
    userPeripheralDataDTO: function () {
        "use strict";
        this.Id = 0;
        this.CompanyName = "No Information Provided";
        this.PeripheralType = "No Information Provided";
        this.ProductDetails = "No Information Provided";
        this.AffiliateLink = "No Information Provided";
        this.UserId = 0;
    },
    YouTubeSubscriberListDTO: function () {
        "use strict";
        this.Id = 0;
        this.ChannelId = "";
        this.YoutubeId = "";
        this.Name = "";
    },
    YouTubeStatisticsDTO: function () {
        "use strict";
        this.Id = 0;
        this.SubCount = 0;
        this.ViewCount = 0;
        this.CommentCount = 0;
        this.HiddenSubCount = 0;
        this.VideoCount = 0;
    },
    CSGOVideoConfigurationDTO: function () {
        "use strict";
        this.Id = 0;
        this.UserId = 0;
        this.ColorMode = "";
        this.Brightness = "";
        this.AspectRatio = "";
        this.Resolution = "";
        this.DisplayMode = "";
        this.LaptopPowerSavings = "";
        this.GlobalShadowQuality = "";
        this.ModelTextureDetail = "";
        this.EffectDetail = "";
        this.ShaderDetail = "";
        this.MultiCoreRendering = "";
        this.MultisamplingAntiAliasingMode = "";
        this.FXXAAAnti_Aliasing = "";
        this.TextureFilteringMode = "";
        this.WaitForVerticalSync = "";
        this.MotionBlur = "";
        this.TripleMonitorMode = "";
        this.GameView = "";
    },
    CSGOSensitivityDTO: function () {
        "use strict";

        this.UserId = 0;
        this.Sensitivity = "";
        this.DPI = 0;
        this.eDPI = 0;
        this.RawInput = true;
        this.WindowsSensitivity = 6;
        this.MouseHz = 0;
        this.Sensitivity = 0;
        this.Active = true;
        this.CreateDate = null;
        this.EndDate = null;
        this.StartUnixDatetime = 0;
        this.EndUnixDatetime = 0;
    },
    YouTubeLiveStreamDTO: function () {
        "use strict";

        this.UserId = 1;
        this.YouTubeChannelId = "";
        this.VideoId = "";
        this.EmbedHTML = "";
        this.StreamTitle = "";
        this.LiveChatId = "";
        this.IsLive = false;
    },
    YouTubePlayListDTO: function () {
        "use strict";

        this.PlaylistId = "";
        this.PlaylistTitle = "";
        this.PlayListDescription = "";
        this.ChannelId = "";
        this.DefaultThumbnail = "";
        this.MediumThumbnail = "";
        this.HighThumbnail = "";
        this.MaxThumbnail = "";
        this.CustomThumbail = "";
        this.PlaylistItemCount = 0;
        this.PlaylistActive = true;
        this.CreatedDate = "";
        this.PlaylistCreatedDate = "";
    },
    YouTubeVideosDTO: function () {
        "use strict";
        this.VideoId = "";
        this.VideoTitle = "";
        this.VideoDescription = "";
        this.ChannelId = "";
        this.DefaultThumbnail = "";
        this.MediumThumbnail = "";
        this.HighThumbnail = "";
        this.MaxThumbnail = "";
        this.Standardthumbnail = "";
        this.IsDisplayed = true;
        this.VideoCreatedDate = "";
        this.CreatedDate = "";
    },
    GiveawayDTO: function () {
        "use strict";
        this.Id = 0;
        this.Title = "";
        this.Description = "";
        this.Code = "";
        this.Rules = "";
        this.ImageUrl = "";
        this.VideoUrl = "";
        this.International = false;
        this.Sponsored = false;
        this.TotalEntries = 0;
        this.Active = false;
    },
    GiveawayWinnerDTO: function() {
        "use strict";
        this.Id = 0;
        this.GiveawayId = 0;
        this.Userid = 0;
        this.WinnerImageUrl = "";
        this.WinDate = "";
    },
    YouTubeSuperChatDTO: function () {
        "use strict";

        this.Id = 0;
        this.YouTubeSuperChatId = "";
        this.Channeld = "";
        this.ChannelUrl = "";
        this.DisplayName = "";
        this.CommentText = "";
        this.Currency = "";
        this.DisplayString = "";
        this.AmountMicros = "";
        this.ProfileImageUrl = "";
        this.MessageType = 0;
        this.IsSuperStickerEvent = false;
        this.StickerId = "";
        this.ShowSuperChat = false;
        this.CreatedAt = "";
        this.altText = "";
        this.CreatedDate = "";
      
    }


}