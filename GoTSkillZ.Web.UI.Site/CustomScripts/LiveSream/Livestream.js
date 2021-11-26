var siteCoreDataAPI = "/WCF/SiteCoreDataAPI.svc/";


var GoTSkillZLiveStreamFunctions = {
    getYouTubeLiveStreamData: function() {
        "use strict";

        $.ajax({
            url: siteCoreDataAPI + "GetYouTubeLiveStreamData",
            type: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function(data) {
                GoTSkillZLiveStreamFunctions.setUpLiveStream(data);
            },
            error: function() {
                GoTSkillZNotificationControls.ShowNotification(
                    "Could Not Get YouTube Livestream Data, Please Contact Admin",
                    "danger");
            }
        });
    },
    setUpLiveStream: function(liveStreamData) {
        "use strict";

        if (liveStreamData !== undefined && liveStreamData !== null) {
            if (liveStreamData.IsLive) {
                GoTSkillZLiveStreamFunctions.goLive(liveStreamData);
            } else {
                GoTSkillZLiveStreamFunctions.goOffline();
            }
        }
    },
    goLive: function(liveStreamData) {
        "use strict";

        //set icon
        $("#youtube-livestream-icon").css("color", "red");

        //set title
        $("#stream-title").html(liveStreamData.StreamTitle);

        //set up video
        GoTSkillZLiveStreamFunctions.setupVideo(liveStreamData);

        //set up chat
        GoTSkillZLiveStreamFunctions.setupChat(liveStreamData);
        $(".br-pagetitle,#youtube-livestream-container").show();
        document.body.style.backgroundImage = 'url("../CustomContent/Images/gotskillz-background.jpg")';
    },
    goOffline: function() {
        "use strict";


        //set icon
        $("#youtube-livestream-icon").css("color", "grey");

        $("#stream-title").html("NOT LIVE");
        $(".br-pagetitle,#youtube-livestream-container").hide();
        $("#youtube-livestream-div,#youtube-livestream-chat-div").html("");

        document.body.style.backgroundImage = 'url("../CustomContent/Images/offline-edit2.png")';
    },
    setupVideo: function(liveStreamData) {

        new YT.Player("youtube-livestream-div",
            {
                height: "650vh",
                width: "680px",
                enablejsapi: 1,
                videoId: liveStreamData.VideoId
            });

    },
    setupChat: function(liveStreamData) {
        "use strict";
        const frame = document.createElement("iframe");
        frame.referrerPolicy = "origin";
        frame.src =
            `https://www.youtube.com/live_chat?v=${liveStreamData.VideoId}&embed_domain=${window.location.hostname}`;
        frame.frameBorder = "0";
        frame.allowFullscreen = true;
        frame.id = "chat-embed";
        frame.style = "height:100%;width:100%";
        const wrapper = document.getElementById("youtube-livestream-chat-div");
        wrapper.appendChild(frame);
    }
};

setInterval(function() { GoTSkillZLiveStreamFunctions.getYouTubeLiveStreamData(); }, 900000);

$(window).on("load",
    function() {
        "use strict";
        GoTSkillZLiveStreamFunctions.getYouTubeLiveStreamData();


    });