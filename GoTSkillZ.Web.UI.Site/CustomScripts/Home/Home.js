var siteCoreDataAPI = "/WCF/SiteCoreDataAPI.svc/";
var fileAPI = "/WCF/FileUploadAPI.svc/";


var GoTSkillZGoalFunctions = {
    getSponsors: function () {
        "use strict";
        if (window.location.href === "https://www.gotskillz.gg/" || window.location.href === "https://www.gotskillz.gg/#") {
            $.ajax({
                url: siteCoreDataAPI + "GetCurrentSponsorCount",
                type: "GET",
                dataType: "json",
                cache: false,
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    GoTSkillZGoalFunctions.setSponsorGoalData(data);
                },
                error: function () {
                    GoTSkillZNotificationControls.ShowNotification(
                        "Could Not Get Sponsor Count Data, Please Contact Admin",
                        "danger");
                }
            });
        }
    },
    setSubscriberGoalData: function () {
        "use strict";

        if (currentSubCount !== null) {

            $("#sub-count-goal-2,#sub-count-goal-counter-2").html(currentSubCount + "/2000");

            $("#sub-count-goal-1,#sub-count-goal-counter-1").html(1000 + "/1000");


            $("#sub-count-percentage-2").html(GoTSkillZGoalFunctions.getPercentage(currentSubCount, 2000) + "%");


            $("#sub-count-percentage-1").html(GoTSkillZGoalFunctions.getPercentage(1000, 1000) + "%");


        }

        $('.sub-pity-donut').peity('donut');
    },
    setSponsorGoalData: function (count) {
        "use strict";

        if (count !== null) {

            $("#spon-count-goal-1,#spon-count-goal-counter-1").html(count + "/1000");
            $("#spon-count-percentage-1").html(GoTSkillZGoalFunctions.getPercentage(count, 1000) + "%");
        }

        $('.spon-pity-donut').peity('donut');

    },
    getPercentage: function (current, total) {
        "use strict";

        var currentValue = parseInt(current);
        var totalValue = parseInt(total);
        var percentage = 100 * currentValue / totalValue;
        return percentage;

    }
}

var GoTSkillZRecentDataFunctions = {
    getRecentSponsors: function () {
        "use strict";
        if (window.location.href === "https://www.gotskillz.gg/" || window.location.href === "https://www.gotskillz.gg/#") {
            $.ajax({
                url: siteCoreDataAPI + "GetRecentSponsors",
                type: "GET",
                dataType: "json",
                cache: false,
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    GoTSkillZRecentDataFunctions.setRecentSponsors(data);
                },
                error: function () {
                    GoTSkillZNotificationControls.ShowNotification(
                        "Could Not Get Recent Sponsor Data, Please Contact Admin",
                        "danger");
                }
            });
        }

    },
    setNewSiteUsers: function () {

        var tBody = $("#new-site-tbody");

        if (tBody.length > 0) {
            tBody.html("");

            if (newSiteUsers !== undefined && newSiteUsers !== null) {
                $.each(newSiteUsers, function (index, item) {
                    var tableRow = '<tr> <td class="pd-l-20" style="text-align: center;"> <img src="' + item.SubImage + '" class="wd-80 ht-80 rounded-circle" alt="Image" id="recent-spon-img"> </td> <td> <a href="/User/Profile.aspx?UID=' + item.UserId + '" class="tx-white tx-14 tx-medium d-block"target="_blank" id="recent-spon-name">' + item.SubName + '</a> <span class="tx-11 d-block" id="recent-spon-date">' + item.SubDate + '</span> </td> </tr>';
                    tBody.append(tableRow);
                });
            }
        }

        if ($("#siteUsers").length > 0) {
            new PerfectScrollbar("#siteUsers", {
                wheelPropagation: true,
                minScrollbarLength: 20
            });
        }
    },
    setRecentYoutubeSubs: function () {
        "use strict";

        var recentYoutubeSub = RecentYouTubeSubscribers;
        var tBody = $("#recent-sub-tbody");

        if (tBody.length > 0) {
            tBody.html("");

            if (recentYoutubeSub !== undefined && recentYoutubeSub !== null) {
                $.each(recentYoutubeSub, function (index, item) {
                    var tableRow = '<tr> <td class="pd-l-20" style="text-align: center;"> <img src="' + item.SubImage + '" class="wd-80 ht-80 rounded-circle" alt="Image" id="recent-spon-img"> </td> <td> <a href="/#" class="tx-white tx-14 tx-medium d-block" id="recent-spon-name">' + item.SubName + '</a> <span class="tx-11 d-block" id="recent-spon-date">' + item.SubDate + '</span> </td> </tr>';
                    tBody.append(tableRow);
                });
            }
        }

        if ($("#yTsubs").length > 0) {
            new PerfectScrollbar("#yTsubs", {
                wheelPropagation: true,
                minScrollbarLength: 20
            });
        }

    },
    setRecentSponsors: function (recentSponData) {
        "use strict";


        var tBody = $("#recent-spon-tbody");

        if (tBody.length > 0) {
            tBody.html("");

            if (recentSponData !== undefined && recentSponData !== null && recentSponData.length > 0) {
                $.each(recentSponData, function (index, item) {
                    var tableRow = '<tr> <td class="pd-l-20" style="text-align: center;"> <img src="' + item.SubImage + '" class="wd-80 ht-80 rounded-circle" alt="Image" id="recent-sub-img"> </td> <td> <a href="/User/Profile.aspx?UID=' + item.UserId + '" class="tx-white tx-14 tx-medium d-block"target="_blank" id="recent-sub-name">' + item.SubName + '</a> <span class="tx-11 d-block" id="recent-sub-date">' + item.SubDate + '</span> </td> </tr>';
                    tBody.append(tableRow);
                });
            }
            else {
                tBody.append("<h4>No Sponsors &#9785; </h4>");
            }
        }


        if ($("#ytMembers").length > 0) {
            new PerfectScrollbar("#ytMembers", {
                wheelPropagation: true,
                minScrollbarLength: 20
            });
        }

    },
    setDonations: function (donationData) {
        "use strict";

        var donationData = recentDonations;
        var tBody = $("#donation-tbody");

        if (tBody.length > 0) {
            tBody.html("");

            if (donationData !== undefined && donationData !== null) {
                $.each(donationData, function (index, item) {


                    var tableRow = '';
                    if (item.UserId === 0 || item.UserId === null) {
                        tableRow = '<tr> <td class="pd-l-20" style="text-align: center;"> <img src="' +
                            item.CustomImgUrl +
                            '" class="wd-80 ht-80 " alt="Image" id="donation-img"> </td> <td> <a href="/#' +
                            item.UserId +
                            '" class="tx-white tx-14 tx-medium d-block" id="donation-name">' +
                            item.Name +
                            '</a><span class="tx-11 d-block" id="Donation-Title">Donated: ' + item.DonationTitle + '</span>  ' +
                            '<span class="tx-11 d-block" id="donation-amount">Value: ' + item.Amount + '</span> ' +
                            '<span class="tx-11 d-block" id="date-date">' +
                            item.DonationDate +
                            '</span> </td> </tr>';
                    } else {

                        tableRow = '<tr> <td class="pd-l-20" style="text-align: center;"> <img src="' +
                            item.CustomImgUrl +
                            '" class="wd-80 ht-80 " alt="Image" id="donation-img"> </td> <td> <a href="/User/Profile.aspx?UID=' +
                            item.UserId +
                            '" class="tx-white tx-14 tx-medium d-block"target="_blank" id="donation-name">' +
                            item.Name + '</a>' +
                            '<span class="tx-11 d-block" id="donation-title">Donated: ' + item.DonationTitle + '</span> ' +
                            '<span class="tx-11 d-block" id="donation-amount">Value: ' + item.Amount + '</span> ' +
                            '<span class="tx-11 d-block" id="donation-date">' +
                            item.DonationDate +
                            '</span> </td> </tr>';
                    }


                    tBody.append(tableRow);
                });
            }
        }

        if ($("#donations").length > 0) {
            new PerfectScrollbar("#donations", {
                wheelPropagation: true,
                minScrollbarLength: 20
            });
        }

    }
}





var GoTSkillZRHSMenuFunctions = {
    getConfigFiles: function () {
        "use strict";
        $.ajax({
            url: fileAPI + "GetAllUserCSConfigFiles",
            type: "GET",
            dataType: "json",
            cache: false,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data.length > 0) {
                    GoTSkillZRHSMenuFunctions.buildConfigFileItems(data);
                } else {
                    $("#config-container").html("").html('<a href="/CSGOConfigurations/CSGOConfig"><h4>Add CSGO Config(s)</h4></a>');

                }
            },
            error: function () {
                GoTSkillZNotificationControls.ShowNotification("Could Not Get CSGO Config Data, Please Contact Admin", "danger");
            }
        });

    },
    buildConfigFileItems: function (allConfigFiles) {
        "use strict";

        if (allConfigFiles.length > 0) {

            var configContainer = $("#config-container");
            $("#config-container").html("");
            $.each(allConfigFiles, function (index, item) {

                var html = '<a href="' + item.filePath + '" download><div class="media  mg-t-20"><div class="pd-10 bg-gray-500 bg-reef wd-50 ht-60 tx-center d-flex align-items-center justify-content-center"><i class="fas fa-download tx-28 tx-white"/></div><div class="media-body tx-gray-800 mg-l-10 mg-r-auto"><p class="mg-b-0 tx-13" id="quick-down-name">' + item.name + '</p><p class="mg-b-0 tx-12 op-5" id="quick-down-date">' + item.extension + '</p><p class="mg-b-0 tx-12 op-5" id="quick-down-size">' + GoTSkillZCommonUtilityFunctions.formatBytes(item.size) + '</p></div></div></a>';
                configContainer.append(html);
            });
        }
    }
}

$(window).on("load", function () {
    "use strict";


    //    GoTSkillZGoalFunctions.getSponsors();
    GoTSkillZRecentDataFunctions.setRecentYoutubeSubs();
    GoTSkillZRecentDataFunctions.setNewSiteUsers();
    //    GoTSkillZRecentDataFunctions.getRecentSponsors();
    GoTSkillZRecentDataFunctions.setDonations();
    GoTSkillZRHSMenuFunctions.getConfigFiles();
    GoTSkillZGoalFunctions.setSubscriberGoalData();
});



