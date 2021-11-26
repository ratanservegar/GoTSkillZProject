var pmsAPI = "/WCF/PMSAPI.svc/";


var GoTSkillZGateKeeperFunctions = {
    checkUserHasAccess: function (callback, reload) {

        var pageId = $("#pageId").val();
        if (pageId !== undefined && pageId !== "") {
            $.ajax({
                url: pmsAPI + "CheckUserPageAccess/" + pageId,
                type: "GET",
                cache: false,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    if (data) {
                      
                        GoTSkillZGateKeeperFunctions.handleAccessControl(data.StateCode, callback, reload);
                    }
                }
            });
        } else {
            //redirect to 404 page
        }
    },
    handleAccessControl: function (stateCode, callback, reload) {
        "use strict";

        switch (stateCode) {
            case 0:
                GoTSkillZNotificationControls.ShowNotification("Please Login", "danger");
                $("#sign-in-modal").modal({ backdrop: "static", keyboard: false });
                GoTSkillNavigationFunctions.hideMenuItems();
                break;
            case 6:
                GoTSkillZNotificationControls.ShowNotification("Session Expired, Please Re-Login", "danger");
                $("#sign-in-modal").modal({ backdrop: "static", keyboard: false });
                GoTSkillNavigationFunctions.hideMenuItems();
            case 1:
                GoTSkillZNotificationControls.ShowNotification("You Need To Be Subscribed To My YouTube Channel To View This Page", "danger");
                $("#subscribe-modal").modal({ backdrop: "static", keyboard: false });
                break;
            case 2:
                GoTSkillZNotificationControls.ShowNotification("You Need To Be A Sponsor On My YouTube Channel To View This Page", "danger");
                break;
            case 3:
                GoTSkillZGateKeeperFunctions.redirectToHomePage("You do not have sufficient permissions to view this page.. Redirecting back to home page");
            case 4:
            case 12:
                GoTSkillZGateKeeperFunctions.redirectToHomePage("You do not have sufficient permissions to view this page.. Redirecting back to home page");
                break;
            case 8:
                GoTSkillZNotificationControls.ShowNotification("Invalid User, Please ReLogin", "danger");
                $("#sign-in-modal").modal({ backdrop: "static", keyboard: false });
                GoTSkillNavigationFunctions.hideMenuItems();
                break;
            case 9:
            case 10:
            case 13:
                GoTSkillZNotificationControls.ShowNotification("Error Occured, Please Report", "danger");
                break;
            case 5:
            case 7:
            case 11:

                if (reload !== undefined && reload === "true")
                    window.location.reload();

                GoTSkillNavigationFunctions.showMenuItems();
                console.log("from gate keeper, showMenuItems");



                if (callback) {
                    callback();
                }

                break;
            default:
                GoTSkillZGateKeeperFunctions.redirectToHomePage("You do not have sufficient permissions to view this page.. Redirecting back to home page");
                break;
        }


    },
    redirectToHomePage: function (message) {
        "use strict";

        if (message !== undefined && message !== "")
            GoTSkillZNotificationControls.ShowNotification(message, "danger");

        setTimeout(function () {

            if (history.length > 3) {
                history.back();
            }

            if (window.location.href.lastIndexOf("/") > 22)
                window.location.href = "/";
        }, 2000);

    }
};


$(window).on("load", function () {
    "use strict";


});