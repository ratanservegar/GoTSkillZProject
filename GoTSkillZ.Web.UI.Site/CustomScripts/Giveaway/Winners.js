var giveawayAPI = "/WCF/GiveawayAPI.svc/";

var isAdmin = false,
    allGiveawayData = [],
    allGiveawayWinners = [],
    allGiveawayEntries = [],
    topWinnersArray = [],
    top10Array = [],
    top5Array = [],
    top3Array = [],
    top2Array = [],
    winnerArray = [];


var GoTSkillZGiveawayWinnersAdminFunctions = {
    checkForAdminUser: function () {
        "use strict";

        $("#edit-giveaway-winners").hide();

        var userId = parseInt($("#hdnUserId").val());
        if (userId !== null && userId === 1 && userId !== NaN) {
            isAdmin = true;
            $("#edit-giveaway-winners").show();

        } else {
            isAdmin = false;
            $("#edit-giveaway-winners").hide();
        }
    },
    getAllGiveaways: function () {
        "use strict";
        $.ajax({
            url: giveawayAPI + "GetAllGiveaways",
            type: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                allGiveawayData = [];
                allGiveawayData = data;
                GoTSkillZGiveawayWinnersAdminFunctions.buildExistingGiveawayList();
            },
            error: function () {
                GoTSkillZNotificationControls.ShowNotification("Could Not Get giveaway Data, Please Contact Admin",
                    "danger");
            }
        });
    },
    buildExistingGiveawayList: function () {
        "use strict";

        if (allGiveawayData !== undefined && allGiveawayData !== null && allGiveawayData.length > 0) {


            let ddlData = '<option value="">Select one ... </option>';

            $("#giveaway-list").html("");


            ddlData = ddlData +
                allGiveawayData.map(function (x) {
                    return `<option value="${x.Id}">${x.Title}</option>`;
                }).join("");


            $("#giveaway-list").html(ddlData);
        }

    },
    bindGiveawayEditModal: function () {
        "use strict";

        $("#edit-giveaway-winners").unbind("click").bind("click",
            function (e) {
                e.preventDefault();
                e.stopImmediatePropagation();
                $("#pick-winner-modal").modal("show");

                $("#pick-winner").unbind("click").bind("click",
                    function () {
                        GoTSkillZGiveawayWinnersAdminFunctions.getAllEntries();
                    });

                $("#save-winner").unbind("click").bind("click",
                    function () {
                        GoTSkillZGiveawayWinnersAdminFunctions.confirmWinner();
                    });
            });


        GoTSkillZGiveawayWinnersAdminFunctions.bindOnGiveawayChange();
    },
    getAllEntries: function () {
        "use strict";

        const giveawayId = $("#giveaway-list").val();

        if (giveawayId === null || giveawayId === "") {
            GoTSkillZNotificationControls.ShowNotification("Please select giveaway", "danger");
            return false;
        }

        $.ajax({
            url: giveawayAPI + "GetGiveawayEntriesByGiveawayId/" + giveawayId,
            type: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                allGiveawayEntries = [];
                allGiveawayEntries = data;

                GoTSkillZGiveawayWinnersAdminFunctions.buildAllEntryNameList(data, "Total Entries:");

                $("#pick-winner").html("Pick Top 10");
                $("#pick-winner").unbind("click").bind("click",
                    function () {
                        GoTSkillZGiveawayWinnersAdminFunctions.pickWinner();
                    });

            },
            error: function () {
                GoTSkillZNotificationControls.ShowNotification(
                    "Could Not Get giveaway Entries Data, Please Contact Admin",
                    "danger");
            }
        });
    },
    buildAllEntryNameList: function (entryList, topDivTxt) {
        "use strict";

        if (entryList === undefined || entryList === null || entryList.length === 0) {

            GoTSkillZNotificationControls.ShowNotification("No Entries List Names Found, Please Contact Admin",
                "danger");
            return null;
        }


        const totalEntries = entryList.length;
        const splitArray = _.chunk(entryList, Math.ceil(entryList.length / 2));


        var randomArray = ["item-1", "item-2", "item-3", "item-4", "item-5"];
        if (splitArray !== null) {


            const leftArray = splitArray[0];
            const rightArray = splitArray[1];

            $("#entry-list-container").html("");


            const firstCol = $("<div>",
                {
                    "class": "col-sm"
                });

            var leftUl = $("<ul>");

            $.each(leftArray,
                function (index, item) {
                    const li = $("<li>",
                        {
                            "class": `giveaway-entry-list ${randomArray[Math.floor(Math.random() * randomArray.length)]
                                }`
                        }).html(item);
                    leftUl.append(li);
                });

            firstCol.append(leftUl);

            const secondCol = $("<div>",
                {
                    "class": "col-sm"
                });

            var rightUl = $("<ul>");


            if (rightArray != null) {


                $.each(rightArray,
                    function (index, item) {
                        const li = $("<li>",
                            {
                                "class": `giveaway-entry-list ${randomArray[Math.floor(Math.random() *
                                    randomArray.length)]}`
                            }).html(item);
                        rightUl.append(li);
                    });

                secondCol.append(rightUl);
            }


            const entryCountDiv = $("<div>",
                {
                    "class": "col-md-12"
                });
            const totalEntriesTag = $("<h3>").html(topDivTxt + totalEntries).append("<br/>");
            entryCountDiv.append(totalEntriesTag);
            $("#entry-list-container").append(entryCountDiv).append(firstCol).append(secondCol);
        }

    },
    pickWinner: function () {
        "use strict";

        const giveawayId = $("#giveaway-list").val();

        if (giveawayId === null || giveawayId === "") {
            GoTSkillZNotificationControls.ShowNotification("Please select giveaway", "danger");
            return false;
        }

        $.ajax({
            url: giveawayAPI + "GetGiveAwayWinner/" + giveawayId,
            type: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                topWinnersArray = [];
                topWinnersArray = data;

                top10Array = top5Array = top3Array = top2Array = winnerArray = [];


                top10Array = _.where(topWinnersArray, { Bucket: 10 });
                top5Array = _.where(topWinnersArray, { Bucket: 5 });
                top3Array = _.where(topWinnersArray, { Bucket: 3 });
                top2Array = _.where(topWinnersArray, { Bucket: 2 });
                winnerArray = _.where(topWinnersArray, { Bucket: 1 });

                GoTSkillZGiveawayWinnersAdminFunctions.buildTop10List(top10Array);
            },
            error: function () {
                GoTSkillZNotificationControls.ShowNotification(
                    "Could Not Get giveaway Entries Data, Please Contact Admin",
                    "danger");
            }
        });
    },
    buildTop10List: function () {
        "use strict";

        if (top10Array.length > 0) {

            var nameList = [];

            $.each(top10Array,
                function (i, item) {
                    nameList.push(item.Name);
                });

            if (nameList.length > 0) {
                GoTSkillZGiveawayWinnersAdminFunctions.buildAllEntryNameList(nameList, "Top ");
            }

            $("#pick-winner").html("Pick Top 5");

            $("#pick-winner").unbind("click").bind("click",
                function () {
                    GoTSkillZGiveawayWinnersAdminFunctions.buildTop5List();
                });

        }

    },
    buildTop5List: function () {
        "use strict";

        if (top5Array.length > 0) {

            var nameList = [];

            $.each(top5Array,
                function (i, item) {
                    nameList.push(item.Name);
                });

            if (nameList.length > 0) {
                GoTSkillZGiveawayWinnersAdminFunctions.buildAllEntryNameList(nameList, "Top ");
            }

            $("#pick-winner").html("Pick Top 3");

            $("#pick-winner").unbind("click").bind("click",
                function () {
                    GoTSkillZGiveawayWinnersAdminFunctions.buildTop3List();
                });

        }
    },
    buildTop3List: function () {
        "use strict";

        if (top3Array.length > 0) {

            var nameList = [];

            $.each(top3Array,
                function (i, item) {
                    nameList.push(item.Name);
                });

            if (nameList.length > 0) {
                GoTSkillZGiveawayWinnersAdminFunctions.buildAllEntryNameList(nameList, "Top ");
            }

            $("#pick-winner").html("Pick Top 2");

            $("#pick-winner").unbind("click").bind("click",
                function () {
                    GoTSkillZGiveawayWinnersAdminFunctions.buildTop2List();
                });

        }
    },
    buildTop2List: function () {
        "use strict";

        if (top2Array.length > 0) {

            var nameList = [];

            $.each(top2Array,
                function (i, item) {
                    nameList.push(item.Name);
                });

            if (nameList.length > 0) {
                GoTSkillZGiveawayWinnersAdminFunctions.buildAllEntryNameList(nameList, "Top ");
            }

            $("#pick-winner").html("Pick Winner");

            $("#pick-winner").unbind("click").bind("click",
                function () {
                    GoTSkillZGiveawayWinnersAdminFunctions.buildWinnerList();
                });

        }

    },
    buildWinnerList: function () {
        "use strict";


        const randomArray = ["item-5"];
        if (winnerArray.length > 0) {


            const winner = winnerArray[0];

            $("#entry-list-container").html("");


            const firstCol = $("<div>",
                {
                    "class": "col-sm"
                });

            const leftUl = $("<ul>");


            const li = $("<li>",
                {
                    "class": `giveaway-entry-list ${randomArray[0]} gradient-border`
                }).css("width", "auto").html(winner.Name);
            leftUl.append(li);

            firstCol.append(leftUl);

            const entryCountDiv = $("<div>",
                {
                    "class": "col-md-12"
                });
            const totalEntriesTag = $("<h3>").html("***CONGRATULATIONS***").css("text-align", "center").append("<br/>");
            entryCountDiv.append(totalEntriesTag);
            $("#entry-list-container").append(entryCountDiv).append(firstCol);
        }


        const siteColors = ["#ffa68d", "#fd3a84"];
        party.registerShape("star",
            '<polygon points="512,197.816 325.961,185.585 255.898,9.569 185.835,185.585 0,197.816 142.534,318.842 95.762,502.431 255.898,401.21 416.035,502.431 369.263,318.842"/>');
        // To use it, we override the 'shape' option of a particle-emitting method.
        party.screen({ shape: "star" });

        party.screen(this,
            {
                color: siteColors,
                size: party.minmax(6, 12),
                count: party.variation(300 * (window.innerWidth / 1980), 0.4),
                angle: -180,
                spread: 80,
                angularVelocity: party.minmax(6, 9)

            });


        $("#pick-winner").html("Save Winner");


        $("#pick-winner").unbind("click").bind("click",
            function () {
                GoTSkillZGiveawayWinnersAdminFunctions.confirmWinner();
            });


    },
    confirmWinner: function () {
        "use strict";
        var giveawayObj = new GoTSkillzEntities.GiveawayWinnerDTO();

        giveawayObj.Id = 0;
        giveawayObj.GiveawayId = winnerArray[0].GiveawayId;
        giveawayObj.UserId = winnerArray[0].UserId;


        if (giveawayObj.GiveawayId !== "") {
            $.ajax({
                url: giveawayAPI + "SaveGiveawayWinner",
                type: "POST",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(giveawayObj),
                success: function (data) {

                    GoTSkillZGiveawayWinnersAdminFunctions.getWinners();
                },
                complete: function () {
                    GoTSkillZNotificationControls.ShowNotification("Giveaway Winner Saved Successful!", "success");
                },
                error: function () {
                    GoTSkillZNotificationControls.ShowNotification("Could Not Save Giveaway Data, Please Contact Admin",
                        "danger");
                }
            });
        } else {
            GoTSkillZNotificationControls.ShowNotification("Please input all fields", "danger");
        }


    },
    bindOnGiveawayChange: function () {
        "use strict";

        $('#giveaway-list').on('change', function () {
            $("#pick-winner").html("Get Entries");
            $("#pick-winner").unbind("click").bind("click",
                function () {
                    GoTSkillZGiveawayWinnersAdminFunctions.getAllEntries();
                });
        });

    },
    getWinners: function () {
        "use strict";

        $.ajax({
            url: giveawayAPI + "GetGiveAwayWinners",
            type: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                allGiveawayWinners = [];
                allGiveawayWinners = data;

                GoTSkillZGiveawayWinnersAdminFunctions.buildWinnerCards(data);
            },
            error: function () {
                GoTSkillZNotificationControls.ShowNotification(
                    "Could Not Get giveaway Winners Data, Please Contact Admin",
                    "danger");
            }
        });

    },
    buildWinnerCards: function (data) {
        "use strict";

        var container = $("#giveaway-winners-container");

        if (data !== undefined && data !== null && data.length > 0) {
            container.html("");
            $.each(data,
                function (index, value) {


                    var divCol = $("<div>",
                        {
                            "class": "col-sm-4 pd-t-10",
                            "giveaway-div-id": value.GiveawayId
                        });

                    var span = "";


                    span = $("<span>",
                        {
                            "class": "bg-success pd-y-3 pd-x-10 tx-white tx-13 tx-roboto"
                        }).html("Congratulations");


                    var divCardImg = $("<div>",
                        {
                            "class": "card bd-0 mg-0"
                        });

                    var figure = $("<figure>",
                        {
                            "class": "card-item-img  rounded-top"
                        });


                    var imgSrc = value.WinnerImageUrl;

                    if (imgSrc === "" || imgSrc === null)
                        imgSrc = "../CustomContent/Images/trophy.jpg";

                    var img = $("<img/>",
                        {
                            "class": "img-fluid rounded-top",
                            "src": imgSrc,
                            "style": "height: 100%; width: 100%; object-fit: contain"
                        });

                    var cardBody = $("<div>",
                        {
                            "class": "card-body pd-25 bd bd-t-0 bd-white-1 rounded-bottom"
                        });

                    var cardTitle = $("<p>",
                        {
                            "class": "tx-11 tx-uppercase tx-mont tx-semibold tx-info"
                        }).html("Prize: " + value.GiveawayTitle);


                    var winningCode = $("<p>",
                        {
                            "class": "tx-11 tx-uppercase tx-mont tx-semibold tx-info"
                        }).html("Entry Code: " + value.WinningEntryCode);


                    var winDate = $("<p>",
                        {
                            "class": "tx-11 tx-uppercase tx-mont tx-semibold tx-info"
                        }).html("Date: " + value.WinDate);


                    var btn = $("<button>",
                        {
                            "class": "giveaway-entry-list item-1 gradient-border winner-text",
                            'id': "winner-" + value.UserId,
                            'userid': value.UserId
                        }).html(value.WinnerName);


                    divCol.append(span);
                    divCol.append(divCardImg.append(figure.append(img))
                        .append(cardBody.append(cardTitle).append(winningCode).append(winDate).append(btn)));


                    container.append(divCol);


                    let siteColors = ['#ffa68d', '#fd3a84'];
                    document.getElementById('winner-' + value.UserId).addEventListener('click', function (e) {
                        e.preventDefault();
                        party.element(this, {
                            color: siteColors,
                            count: party.variation(25, 0.5),
                            size: party.minmax(6, 10),
                            velocity: party.minmax(-300, -600),
                            angularVelocity: party.minmax(6, 9)
                        });
                    });

             

                    $("#winner-" + value.UserId).unbind("click").bind("click",
                        function (e) {
                            e.stopImmediatePropagation();
                            e.preventDefault();


                            var userId = e.currentTarget.getAttribute("userid");

                            if (userId !== null && userId !== "") {
                                setTimeout(function() {
                                        window.open('/User/Profile.aspx?UID=' + userId, '_blank');
                                    },
                                    1000);

                            }

                        });
                });


        } else {
            $("#giveaway-winners-container").html("").html("<div style='margin:auto;vertical-align: middle;height:100vh;'><h1>Winners will be displayed here</h1></div>");
        }
    }
};


var GoTSKillZGiveawayWinnerInitializers = {
    initialize: function () {
        "use strict";

        GoTSkillZGiveawayWinnersAdminFunctions.bindGiveawayEditModal();
        GoTSkillZGiveawayWinnersAdminFunctions.checkForAdminUser();
        GoTSkillZGiveawayWinnersAdminFunctions.getAllGiveaways();
        GoTSkillZGiveawayWinnersAdminFunctions.getWinners();


    }
};


$(window).on("load",
    function () {
        "use strict";

        GoTSkillZGateKeeperFunctions.checkUserHasAccess(GoTSKillZGiveawayWinnerInitializers.initialize);

    });