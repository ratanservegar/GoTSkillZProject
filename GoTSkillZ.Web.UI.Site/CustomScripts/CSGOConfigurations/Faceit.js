var siteCoreDataAPI = "/WCF/SiteCoreDataAPI.svc/";


var GoTSkillZFaceitGoals = {
   setFaceitSubscriberGoalData: function () {

        if (currentSubCount !== null) {

            $("#faceit-unlock-goal-1,#faceit-unlock-goal-counter-1").html(currentSubCount + "/5000");

            $("#faceit-count-percentage-1").html(GoTSkillZFaceitGoals.getPercentage(currentSubCount, 5000) + "%");
        }

     $('.faceit-pity-donut').peity('donut');
    },
    getPercentage: function (current, total) {
        "use strict";

        var currentValue = parseInt(current);
        var totalValue = parseInt(total);
        var percentage = 100 * currentValue / totalValue;
        return percentage;

    }
}




$(window).on("load", function () {
    "use strict";
    function callback(response) {
        if (response.isIdTokenValid === true) {
            return;
        }
        alert('The id token is not valid, something went wrong');
    }
    var initParams = {
        client_id: '3b26c48d-8b75-466f-9da3-b96c996071af',
        response_type: 'token',
        redirect_popup: true,

        debug: true
    };
    FACEIT.init(initParams, callback);


    GoTSkillZFaceitGoals.setFaceitSubscriberGoalData();

});