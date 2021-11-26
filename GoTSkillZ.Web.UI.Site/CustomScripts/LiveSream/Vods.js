var youTubeAPI = "/WCF/YouTubeDataAPI.svc/";


var playListData, videoData;
var GoTSkillZVodDataFunctions = {
    getPlaylists: function () {
        "use strict";

        $.ajax({
            url: youTubeAPI + "GetYouTubePlaylists",
            type: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                playListData = data;
            },
            complete: function () {
                GoTSkillZVodDataFunctions.getVideos();
            },
            error: function () {
                GoTSkillZNotificationControls.ShowNotification("Could Not Get YouTube Playlist Data, Please Contact Admin", "danger");
            }
        });
    },
    getVideos: function () {
        "use strict";

        $.ajax({
            url: youTubeAPI + "GetYouTubeVideos",
            type: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                videoData = data;
            },
            complete: function () {
                GoTSkillZGridFunctions.buildGridderDivsForPlaylist();
            },
            error: function () {
                GoTSkillZNotificationControls.ShowNotification("Could Not Get YouTube Videos Data, Please Contact Admin", "danger");
            }
        });
    }
}


var GoTSkillZGridFunctions = {
    buildGridderDivsForPlaylist: function () {
        "use strict";

        if (playListData !== undefined && playListData !== null && playListData.length > 0) {


            var playListContainer = $("#playlist-grid-container");
            $.each(playListData, function (index, playlistItem) {

                var colDiv = $("<div>", {
                    "class": "col-lg-3 col-md-4 col-6 playlist-item",
                    "id": playlistItem.PlaylistId
                });

                var cardDiv = $("<div>", {
                    "class": "d-block mb-4 h-100"
                });

                var imgTag = $("<img>", {
                    "class": "img-fluid img-thumbnail playlist-thumbnail",
                    "src": playlistItem.MaxThumbnail
                });


                if (playlistItem.PlaylistTitle.toLowerCase() === "streams")
                    playlistItem.PlaylistTitle = "All Past Streams";

                playListContainer.append(colDiv.append(cardDiv.append(imgTag).append("<h3 style='text-align:center;'>" + playlistItem.PlaylistTitle + "</h3>")));


            });


            $(".playlist-item").unbind("click").bind("click", function (e) {
                var playlistId = e.currentTarget.id;

                GoTSkillZGridFunctions.showPlayListItems(playlistId);
            });

        }
    },
    showPlayListItems: function (playlistId) {
        "use strict";

        if (playlistId !== undefined && playlistId !== null && playlistId !== "") {

            var playListItems = _.filter(videoData, function (x) {
                return x.PlaylistId === playlistId;
            });


            var lightGalleryArray = [];

            $.each(playListItems, function (index, videoItem) {
                var arrayObj = { 'src': 'https://www.youtube.com/watch?v=' + videoItem.VideoId, 'thumb': videoItem.HighThumbnail, 'subHtml': '<h3>' + videoItem.VideoTitle.toString() + '</h3>' }
                lightGalleryArray.push(arrayObj);
            });

            if (lightGalleryArray.length > 0) {
                $("#" + playlistId).lightGallery({
                    galleryId: playlistId,
                    videoMaxWidth:"1300px",
                    dynamic: true,
                    dynamicEl: lightGalleryArray,
                    youtubePlayerParams: {
                        autoplay:1
                    }
                });

            }
        }
    }
}


var GoTSkillZVodInitializers = {
    initializeFunctions: function () {
        "use strict";
        GoTSkillZVodDataFunctions.getPlaylists();
    }
}



$(window).on("load", function () {
    "use strict";
    GoTSkillZVodInitializers.initializeFunctions();
});