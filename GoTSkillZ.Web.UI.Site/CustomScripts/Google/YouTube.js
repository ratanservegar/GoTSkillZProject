var googleAPI = "/WCF/GoogleAPI.svc/";


var youTubeSubscriberList = [];
var youTubePlayList = [];
var youTubeVideos = [];
var youTubePlayListIds = [];
var superChatList = [];
var youTubeSuperChatIds = [];


var GoTSkillZYouTubeFunctions = {
    loadYoutubeApi: function () {
        "use strict";

        window.gapi.client.setApiKey("");
        return window.gapi.client.load("https://www.googleapis.com/discovery/v1/apis/youtube/v3/rest")
            .then(function () {
            },
                function (err) { console.error("Error loading GAPI client for API", err); });

    },
    getLiveStreamData: function () {
        "use strict";
        if (window.gapi.client.youtube === undefined)
            GoTSkillZYouTubeFunctions.loadYoutubeApi();

        if (window.gapi.client.youtube === undefined) {
            GoTSkillZNotificationControls.ShowNotification("Please Login", "danger");
        }

        return window.gapi.client.youtube.liveBroadcasts.list({
            "part": "snippet,contentDetails,status",
            "broadcastStatus": "active",
            "broadcastType": "all"
        })
            .then(function (response) {
                if (response.result != null) {
                    const youTubeLiveStreamObj = new GoTSkillzEntities.YouTubeLiveStreamDTO();
                    if (response.result.items.length > 0) {

                        if (response.result.items[0].snippet.channelId === "UCdMrMjhgjEm7GJwQM-4Fn0w") {
                            youTubeLiveStreamObj.YouTubeChannelId = response.result.items[0].snippet.channelId;
                            youTubeLiveStreamObj.VideoId = response.result.items[0].id;
                            youTubeLiveStreamObj.IsLive = true;
                            youTubeLiveStreamObj.EmbedHTML =
                                response.result.items[0].contentDetails.monitorStream.embedHtml;
                            youTubeLiveStreamObj.LiveChatId = response.result.items[0].snippet.liveChatId;
                            youTubeLiveStreamObj.StreamTitle = response.result.items[0].snippet.title;

                            GoTSkillZYouTubeFunctions.setLiveStreamData(youTubeLiveStreamObj);
                        }


                    } else {
                        youTubeLiveStreamObj.IsLive = false;
                        GoTSkillZYouTubeFunctions.setLiveStreamData(youTubeLiveStreamObj);
                    }
                }
            },
                function () {
                    GoTSkillZNotificationControls.ShowNotification(
                        "Unable To Retrieve Youtube Live Stream Data, Please Contact Admin",
                        "danger");
                });
    },
    setLiveStreamData: function (youTubeLiveStreamObj) {
        "use strict";

        if (youTubeLiveStreamObj != null) {
            $.ajax({
                url: siteCoreDataAPI + "SetYouTubeIsLive",
                type: "POST",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(youTubeLiveStreamObj)
            });
        }
    },
    getLiveToggleButton: function () {
        "use strict";

        
        if (isLiveStreamOn) {
            $("#youtubeIsLive").bootstrapToggle("on");
        } else {
            $("#youtubeIsLive").bootstrapToggle("off");
        }
        GoTSkillZYouTubeBinders.bindYouTubeLiveSyncBtn();

    },
    getYouTubePlayList: function (pageToken) {
        "use strict";

        if (window.gapi.client.youtube === undefined)
            GoTSkillZYouTubeFunctions.loadYoutubeApi();

        if (window.gapi.client.youtube === undefined) {
            GoTSkillZNotificationControls.ShowNotification("Please Login", "danger");
        }

        youTubePlayList = [];

        return window.gapi.client.youtube.playlists.list({
            "part": "snippet,contentDetails",
            "maxResults": 50,
            "mine": true,
            "pageToken": pageToken
        })
            .then(function (response) {
                if (response.result != null) {

                    const nextPagetoken = response.result.nextPageToken;


                    if (response.result.items.length > 0) {

                        youTubePlayList.push(response.result.items);

                    }
                    if (nextPagetoken !== undefined && nextPagetoken !== null && nextPagetoken !== "") {
                        GoTSkillZYouTubeFunctions.getYouTubePlayList(nextPagetoken);
                    } else {
                        GoTSkillZYouTubeFunctions.addYouTubePlayList();
                    }


                }
            },
                function () {
                    GoTSkillZNotificationControls.ShowNotification(
                        "Unable To Retrieve Youtube PlayList, Please Contact Admin",
                        "danger");
                });

    },
    addYouTubePlayList: function () {
        "use strict";

        var youTubePlayListDTOArray = [];


        if (youTubePlayList.length > 0) {
            youTubePlayListIds = [];
            youTubePlayList = _.flatten(youTubePlayList);
            youTubePlayList = _.uniq(youTubePlayList, "id");

            $.each(youTubePlayList,
                function (index, item) {

                    const youTubePlayListObj = new GoTSkillzEntities.YouTubePlayListDTO();

                    //add all play list ids to playlistId array
                    youTubePlayListIds.push(item.id);


                    youTubePlayListObj.PlaylistId = item.id;

                    if (item.snippet !== null) {
                        youTubePlayListObj.PlaylistTitle = item.snippet.title;
                        youTubePlayListObj.PlayListDescription = item.snippet.description;
                        youTubePlayListObj.ChannelId = item.snippet.channelId;
                        youTubePlayListObj.PlaylistCreatedDate = item.snippet.publishedAt;

                        if (item.snippet.thumbnails !== null) {

                            if (item.snippet.thumbnails.default !== undefined)
                                youTubePlayListObj.DefaultThumbnail = item.snippet.thumbnails.default.url;
                            if (item.snippet.thumbnails.medium !== undefined)
                                youTubePlayListObj.MediumThumbnail = item.snippet.thumbnails.medium.url;
                            if (item.snippet.thumbnails.high !== undefined)
                                youTubePlayListObj.HighThumbnail = item.snippet.thumbnails.high.url;
                            if (item.snippet.thumbnails.maxres !== undefined)
                                youTubePlayListObj.MaxThumbnail = item.snippet.thumbnails.maxres.url;
                        }

                    }

                    if (item.contentDetails !== null) {
                        youTubePlayListObj.PlaylistItemCount = item.contentDetails.itemCount;
                    }

                    youTubePlayListDTOArray.push(youTubePlayListObj);
                });


        }

        if (youTubePlayListDTOArray.length > 0) {
            $.ajax({
                url: siteCoreDataAPI + "AddYouTubePlayListData",
                type: "POST",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(youTubePlayListDTOArray),
                success: function (data) {
                    if (data === "Success") {
                        GoTSkillZYouTubeFunctions.getYouTubePlayLists();
                    }
                },
                complete: function () {
                    GoTSkillZNotificationControls.ShowNotification("YouTube Playlist Data Updated", "success");
                },
                error: function () {
                    GoTSkillZNotificationControls.ShowNotification(
                        "Could Not Update YouTube Playlist Data, Please Contact Admin",
                        "danger");
                }
            });
        }
    },
    getYouTubePlayLists: function () {
        "use strict";

        if (youTubePlayListIds.length > 0) {
            GoTSkillZYouTubeFunctions.getYouTubeVideos(youTubePlayListIds[0], undefined);
        }
    },
    getYouTubeVideos: function (playListId, pageToken) {
        "use strict";

        if (window.gapi.client.youtube === undefined)
            GoTSkillZYouTubeFunctions.loadYoutubeApi();

        if (window.gapi.client.youtube === undefined) {
            GoTSkillZNotificationControls.ShowNotification("Please Login", "danger");
        }


        return window.gapi.client.youtube.playlistItems.list({
            "part": "snippet,contentDetails",
            "maxResults": 50,
            "playlistId": playListId,
            "pageToken": pageToken
        })
            .then(function (response) {
                if (response.result != null) {

                    const nextPagetoken = response.result.nextPageToken;


                    if (response.result.items.length > 0) {

                        youTubeVideos.push(response.result.items);

                    }
                    if (nextPagetoken !== undefined && nextPagetoken !== null && nextPagetoken !== "") {
                        GoTSkillZYouTubeFunctions.getYouTubeVideos(playListId, nextPagetoken);
                    } else {

                        if (youTubePlayListIds.length > 0) {
                            youTubePlayListIds =
                                _.filter(youTubePlayListIds, function (x) { return x !== playListId });

                            if (youTubePlayListIds.length > 0) {
                                GoTSkillZYouTubeFunctions.getYouTubeVideos(youTubePlayListIds[0], undefined);
                            } else {
                                GoTSkillZYouTubeFunctions.addYouTubeVideos();
                                youTubeVideos = [];
                            }

                        }
                    }


                }
            },
                function () {
                    GoTSkillZNotificationControls.ShowNotification(
                        "Unable To Retrieve Youtube Videos, Please Contact Admin",
                        "danger");
                });
    },
    addYouTubeVideos: function () {
        "use strict";

        var youTubVideosDTOArray = [];


        if (youTubeVideos.length > 0) {

            youTubeVideos = _.flatten(youTubeVideos);
            youTubeVideos = _.uniq(youTubeVideos, "id");

            $.each(youTubeVideos,
                function (index, item) {

                    const youTubeVideoObj = new GoTSkillzEntities.YouTubeVideosDTO();


                    if (item.snippet !== null) {
                        youTubeVideoObj.PlaylistId = item.snippet.playlistId;

                        youTubeVideoObj.VideoTitle = item.snippet.title;
                        youTubeVideoObj.VideoDescription = item.snippet.description;
                        youTubeVideoObj.ChannelId = item.snippet.channelId;
                        youTubeVideoObj.PlaylistCreatedDate = item.snippet.publishedAt;

                        if (item.snippet.thumbnails !== null) {

                            if (item.snippet.thumbnails.default !== undefined)
                                youTubeVideoObj.DefaultThumbnail = item.snippet.thumbnails.default.url;
                            if (item.snippet.thumbnails.medium !== undefined)
                                youTubeVideoObj.MediumThumbnail = item.snippet.thumbnails.medium.url;
                            if (item.snippet.thumbnails.high !== undefined)
                                youTubeVideoObj.HighThumbnail = item.snippet.thumbnails.high.url;
                            if (item.snippet.thumbnails.maxres !== undefined)
                                youTubeVideoObj.MaxThumbnail = item.snippet.thumbnails.maxres.url;
                            if (item.snippet.thumbnails.standard !== undefined)
                                youTubeVideoObj.Standardthumbnail = item.snippet.thumbnails.standard.url;

                        }

                    }

                    if (item.contentDetails !== null) {
                        youTubeVideoObj.VideoId = item.contentDetails.videoId;
                        youTubeVideoObj.VideoCreatedDate = item.contentDetails.videoPublishedAt;
                    }

                    youTubVideosDTOArray.push(youTubeVideoObj);
                });


        }

        if (youTubVideosDTOArray.length > 0) {
            $.ajax({
                url: siteCoreDataAPI + "AddYouTubeVideoData",
                type: "POST",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(youTubVideosDTOArray),

                success: function (data) {
                    if (data === "Success") {
                        GoTSkillZNotificationControls.ShowNotification("YouTube Video Data Updated", "success");
                    } else {
                        GoTSkillZNotificationControls.ShowNotification("Could Not Update YouTube Video Data Updated",
                            "danger");
                    }
                },
                error: function () {
                    GoTSkillZNotificationControls.ShowNotification(
                        "Could Not Update YouTube Video Data, Please Contact Admin",
                        "danger");
                }
            });
        }

    },
    getYouTubeSuperChat: function (pageToken) {
        "use strict";

        if (window.gapi.client.youtube === undefined)
            GoTSkillZYouTubeFunctions.loadYoutubeApi();

        if (window.gapi.client.youtube === undefined) {
            GoTSkillZNotificationControls.ShowNotification("Please Login", "danger");
        }

        youTubePlayList = [];

        return window.gapi.client.youtube.superChatEvents.list({
            "maxResults": 50,
            "pageToken": pageToken
        })
            .then(function (response) {
                if (response.result != null) {

                    const nextPageToken = response.result.nextPageToken;


                    if (response.result.items.length > 0) {

                        superChatList.push(response.result.items);

                    }
                    if (nextPageToken !== undefined && nextPageToken !== null && nextPageToken !== "" && response.result.items.length > 0) {
                        GoTSkillZYouTubeFunctions.getYouTubeSuperChat(nextPageToken);
                    } else {
                        GoTSkillZYouTubeFunctions.addYouTubeSuperChat();
                    }


                }
            },
                function () {
                    GoTSkillZNotificationControls.ShowNotification(
                        "Unable To Retrieve Youtube superchat, Please Contact Admin",
                        "danger");
                });

    },
    addYouTubeSuperChat: function () {
        "use strict";


        var youTubeSuperChatDTOArray = [];


        if (superChatList.length > 0) {
            youTubeSuperChatIds = [];
            superChatList = _.flatten(superChatList);
            superChatList = _.uniq(superChatList, "id");

            $.each(superChatList,
                function (index, item) {

                    const youTubeSuperChatObj = new GoTSkillzEntities.YouTubeSuperChatDTO();

                    //add all play list ids to playlistId array
                    youTubeSuperChatIds.push(item.id);


                    youTubeSuperChatObj.YouTubeSuperChatId = item.id;

                    if (item.snippet !== null) {
                        youTubeSuperChatObj.AmountMicros = item.snippet.amountMicros;
                        youTubeSuperChatObj.CommentText = item.snippet.commentText;
                        youTubeSuperChatObj.CreatedAt = item.snippet.createdAt;
                        youTubeSuperChatObj.Currency = item.snippet.currency;
                        youTubeSuperChatObj.DisplayString = item.snippet.displayString;
                        youTubeSuperChatObj.MessageType = item.snippet.messageType;


                        if (item.snippet.supporterDetails !== null) {
                            youTubeSuperChatObj.Channeld = item.snippet.supporterDetails.channelId;
                            youTubeSuperChatObj.ChannelUrl = item.snippet.supporterDetails.channelUrl;
                            youTubeSuperChatObj.DisplayName = item.snippet.supporterDetails.displayName;
                            youTubeSuperChatObj.ProfileImageUrl = item.snippet.supporterDetails.profileImageUrl;
                        }

                        if (item.snippet.isSuperStickerEvent !== undefined) {

                            youTubeSuperChatObj.IsSuperStickerEvent = item.snippet.isSuperStickerEvent;
                            
                            if (item.snippet.superStickerMetadata !== undefined) {
                                youTubeSuperChatObj.StickerId = item.snippet.superStickerMetadata.stickerId;
                                youTubeSuperChatObj.altText = item.snippet.superStickerMetadata.altText;
                              
                            }

                            if (item.snippet.commentText === undefined)
                                youTubeSuperChatObj.CommentText = "";
                        }

                    }

                   

                    youTubeSuperChatDTOArray.push(youTubeSuperChatObj);
                });


        }

        if (youTubeSuperChatDTOArray.length > 0) {
            $.ajax({
                url: siteCoreDataAPI + "AddYouTubeSuperChatData",
                type: "POST",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(youTubeSuperChatDTOArray),
                success: function (data) {
                    if (data === "Success") {
//                        GoTSkillZYouTubeFunctions.getYouTubePlayLists();
                    }
                },
                complete: function () {
                    GoTSkillZNotificationControls.ShowNotification("YouTube SuperChat Data Updated", "success");
                },
                error: function () {
                    GoTSkillZNotificationControls.ShowNotification(
                        "Could Not Update YouTube SuperChat Data, Please Contact Admin",
                        "danger");
                }
            });
        }
    },
};


var GoTSkillZYouTubeSubscriberFunctions = {
    getChannelStatus: function () {
        "use strict";
        return window.gapi.client.youtube.channels.list({
            "part": "statistics",
            "mine": true
        })
            .then(function (response) {
                if (response.result != null) {

                    if (response.result.items.length > 0) {


                        const newStatObj = new GoTSkillzEntities.YouTubeStatisticsDTO();

                        newStatObj.SubCount = response.result.items[0].statistics.subscriberCount;
                        newStatObj.ViewCount = response.result.items[0].statistics.viewCount;
                        newStatObj.CommentCount = response.result.items[0].statistics.commentCount;
                        newStatObj.VideoCount = response.result.items[0].statistics.videoCount;
                        newStatObj.HiddenSubCount = response.result.items[0].statistics.hiddenSubscriberCount;

                        if (newStatObj.SubCount !== 0) {
                            $.ajax({
                                url: siteCoreDataAPI + "UpdateYouTubeStatistics",
                                type: "POST",
                                dataType: "json",
                                contentType: "application/json; charset=utf-8",
                                data: JSON.stringify(newStatObj),
                                success: function () {

                                },
                                complete: function () {
                                    GoTSkillZNotificationControls.ShowNotification(
                                        "YouTube Channel Statistics  Updated",
                                        "success");
                                },
                                error: function () {
                                    GoTSkillZNotificationControls.ShowNotification(
                                        "Could Not Update YouTube Channel Statistics, Please Contact Admin",
                                        "danger");
                                }
                            });
                        }

                    }
                }
            },
                function () {
                    GoTSkillZNotificationControls.ShowNotification(
                        "Unable To Retrive Youtube Channel Statistics, Please Contact Admin",
                        "danger");
                });

    },
    getAllChannelSubscribers: function (pageToken) {
        "use strict";


        if (window.gapi.client.youtube === undefined)
            GoTSkillZYouTubeFunctions.loadYoutubeApi();

        if (window.gapi.client.youtube === undefined) {
            GoTSkillZNotificationControls.ShowNotification("Please Login", "danger");
        }

        return window.gapi.client.youtube.subscriptions.list({
            "part": "subscriberSnippet",
            "maxResults": 50,
            "mySubscribers": true,
            "pageToken": pageToken
        })
            .then(function (response) {
                if (response.result != null) {

                    const nextPagetoken = response.result.nextPageToken;


                    if (response.result.items.length > 0) {

                        youTubeSubscriberList.push(response.result.items);

                    }
                    if (nextPagetoken !== undefined && nextPagetoken !== null && nextPagetoken !== "") {
                        GoTSkillZYouTubeSubscriberFunctions.getAllChannelSubscribers(nextPagetoken);
                    } else {
                        GoTSkillZYouTubeSubscriberFunctions.addYoutubeSubList();
                        youTubeSubscriberList = [];
                    }


                }
            },
                function () {
                    GoTSkillZNotificationControls.ShowNotification(
                        "Unable To Retrieve Youtube Subscription List, Please Contact Admin",
                        "danger");
                });

    },
    addYoutubeSubList: function () {
        "use strict";

        var youTubeSubList = [];

        if (youTubeSubscriberList.length > 0) {

            youTubeSubscriberList = _.flatten(youTubeSubscriberList);
            youTubeSubscriberList = _.uniq(youTubeSubscriberList, "id");

            $.each(youTubeSubscriberList,
                function (index, item) {

                    const youTubeSub = new GoTSkillzEntities.YouTubeSubscriberListDTO();
                    youTubeSub.YoutubeId = item.id;

                    if (item.subscriberSnippet !== null) {
                        youTubeSub.Name = item.subscriberSnippet.title;
                        youTubeSub.ChannelId = item.subscriberSnippet.channelId;
                    }

                    youTubeSubList.push(youTubeSub);
                });


        }

        if (youTubeSubList.length > 0) {
            $.ajax({
                url: siteCoreDataAPI + "AddYouTubeSubList",
                type: "POST",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(youTubeSubList),
                success: function () {

                },
                complete: function () {
                    GoTSkillZNotificationControls.ShowNotification("YouTube Sub List Updated", "success");
                },
                error: function () {
                    GoTSkillZNotificationControls.ShowNotification(
                        "Could Not Update YouTube Sub List, Please Contact Admin",
                        "danger");
                }
            });
        }
    },
    checkAndSubUser: function () {
        "use strict";
        $.ajax({
            url: googleAPI + "CheckAndSub/" + parseInt($("#hdnUserId").val()),
            type: "GET",
            cache: false,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data) {
                    location.reload(true);
                } else {
                    GoTSkillZNotificationControls.ShowNotification("Could Not Verify Channel Subscription, Please Contact Admin", "danger");
                }
            }
        });
    },
};


var GoTSkillZYouTubeBinders = {
    bindSubscribeBtn: function () {
        "use strict";

        $("#sub-refresh").unbind("click").bind("click",
            function (e) {
                e.preventDefault();
                e.stopImmediatePropagation();
                GoTSkillZYouTubeSubscriberFunctions.checkAndSubUser();
            });
    },
    bindQuickSettingSyncSubDataBtn: function () {
        "use strict";
        $("#sync-youtube-data").unbind("click").bind("click",
            function () {
                //get sub list
                GoTSkillZYouTubeSubscriberFunctions.getAllChannelSubscribers();

                GoTSkillZYouTubeSubscriberFunctions.getChannelStatus();
            });

    },
    bindQuickSettingSyncYouTubePlaylistBtn: function () {
        "use strict";
        $("#sync-playlist-data").unbind("click").bind("click",
            function () {
                //get sub list
                GoTSkillZYouTubeFunctions.getYouTubePlayList();
            });

    },
    bindQuickSettingSyncYouTubeSuperChatBtn: function () {
        "use strict";
        $("#sync-superchat-data").unbind("click").bind("click",
            function () {
                //get sub list
                GoTSkillZYouTubeFunctions.getYouTubeSuperChat();
            });

    },
    bindYouTubeLiveSyncBtn: function () {
        "use strict";

        $("#youtubeIsLive").change(function () {

            // user-triggered event
            if (this.checked) {
                GoTSkillZYouTubeFunctions.getLiveStreamData();
            } else {
                const youTubeLiveStreamObj = new GoTSkillzEntities.YouTubeLiveStreamDTO();
                youTubeLiveStreamObj.IsLive = false;
                GoTSkillZYouTubeFunctions.setLiveStreamData(youTubeLiveStreamObj);
            }
        });
    }
};

$(window).on("load",
    function () {
        "use strict";
        GoTSkillZYouTubeBinders.bindSubscribeBtn();
        GoTSkillZYouTubeBinders.bindQuickSettingSyncSubDataBtn();
        GoTSkillZYouTubeBinders.bindQuickSettingSyncYouTubePlaylistBtn();
        GoTSkillZYouTubeBinders.bindQuickSettingSyncYouTubeSuperChatBtn();
        GoTSkillZYouTubeFunctions.getLiveToggleButton();

    });
