var fps = 4, count = 0, tick = 0, total = 0, timer = null;


var GoTSkillZMousePollingRateFunctions = {
    bindMousePointerEvent: function() {
        "use strict";

        count = 0;
        tick = 0;
        total = 0;

        if (timer) {
            clearInterval(timer);
            document["removeEventListener"]("pointermove", GoTSkillZMousePollingRateFunctions.getMouseEvents);

            timer = null;
        } else {

            document["addEventListener"]("pointermove", GoTSkillZMousePollingRateFunctions.getMouseEvents);

            if (timer) {
                clearInterval(timer);
            }
            timer = setInterval(GoTSkillZMousePollingRateFunctions.UpdateCounter, 1000 / fps);

            $("#mouse-poll-section-text").html("Move your mouse around as fast as possible ");
        }
    },
    getMouseEvents: function(e) {
        "use strict";

        e = "getCoalescedEvents" in e ? e.getCoalescedEvents() : [e];
        for (var event in e) {
            count++;
        }
    },
    UpdateCounter: function() {
        "use strict";
        tick++;
        total += count;

        var currentPollRate = count * fps;
        var average = Math.ceil(fps * total / tick);

        $("#avg-poll-rate").html(average + " <font color='teal'>Hz</font>");
        $("#current-poll-rate").html(currentPollRate + " <font color='teal'>Hz</font>");


        count = 0;
        if (tick > 3 * fps) {
            tick = 0;
            total = 0;
        }
    }
};



var GoTSkillZMousePollingRateInitializer =  {
     //mouse click tester
    intializeMouseDoubleClickFunctions: function () {
        $('#mouse-settings').show();
    }
};



$(window).on("load", function() {
    "use strict";

    GoTSkillZGateKeeperFunctions.checkUserHasAccess(GoTSkillZMousePollingRateInitializer.intializeMouseDoubleClickFunctions);
   
   
    $("#reset-counters").unbind("click").bind("click", function() {
        GoTSkillZMouseDoubleClickFunctions.resetClickCounter();
    });

});