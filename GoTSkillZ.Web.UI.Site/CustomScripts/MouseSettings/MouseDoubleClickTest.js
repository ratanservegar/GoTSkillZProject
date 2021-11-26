var prevClickMicrotime = 0.0,  count = 0;

var GoTSkillZMouseDoubleClickFunctions = {
    resetClickCounter: function() {
        "use strict";
        $("#singleClickCount").html("0");
        $("#dobuleClickCount").html("0");
        $("#mouse-test-div").removeClass("bg-danger");
        $("#section-text").html("Click Here To Start");
    },
    microTime: function() {
        "use strict";

        return new Date().getTime() / 1000;

    },
    bindMouseClick: function(evnt) {
        "use strict";
        var e = evnt || window.event;
        if (e.currentTarget.id === "mouse-test-div" || e.currentTarget.id === "section-text") {

            GoTSkillZMouseDoubleClickFunctions.analyzeClick();

        }
        return false;
    },
    analyzeClick: function() {
        "use strict";

        var singleClickCount = parseInt($("#singleClickCount").html());


        var latestClickMicroTime = GoTSkillZMouseDoubleClickFunctions.microTime();
        var diffClickTime = latestClickMicroTime - prevClickMicrotime;
        if (diffClickTime <= 0.05) { //reduce the response time to lower value later based on research
            $("#mouse-test-div").removeClass("bg-success");
            $("#mouse-test-div").addClass("bg-danger");
            var doubleClickCount = parseInt($("#dobuleClickCount").html());
            doubleClickCount++;
            $("#dobuleClickCount").html(doubleClickCount);
            $("#section-text").html("Double Click Detected");
        } else {
            $("#mouse-test-div").removeClass("bg-danger");
            $("#mouse-test-div").addClass("bg-success");
            $("#section-text").html("GOOD");
        }

        prevClickMicrotime = latestClickMicroTime;
        singleClickCount++;
        $("#singleClickCount").html(singleClickCount);
    }
};





var GoTSkillZMouseInitializer =  {
     //mouse click tester
    intializeMouseSettingFunctions: function (e) {
        $('#mouse-settings').show();
        GoTSkillZMouseDoubleClickFunctions.bindMouseClick(e);
        prevClickMicrotime = GoTSkillZMouseDoubleClickFunctions.microTime();
    }
};



$(window).on("load", function() {
    "use strict";

    GoTSkillZGateKeeperFunctions.checkUserHasAccess(GoTSkillZMouseInitializer.intializeMouseSettingFunctions);
   
   
    $("#reset-counters").unbind("click").bind("click", function() {
        GoTSkillZMouseDoubleClickFunctions.resetClickCounter();
    });

});