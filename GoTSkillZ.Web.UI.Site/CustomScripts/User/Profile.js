var membershipAPI = "/WCF/MembershipAPI.svc/";
var userCoreDataAPI = "/WCF/UserCoreDataAPI.svc/";
var gamerDataAPI = "/WCF/GameDataAPI.svc/";
var partnerAPI = "/WCF/PartnerAPI.svc/";
var fileAPI = "/WCF/FileUploadAPI.svc/";

var userObj, gameTypes, gameRoles, userOccupations;
var removedAchievements = [], removedTeamHistory = [];


var GoTSkillZProfileMetaDataFunctions = {
    getUserProfileMetaData: function () {
        "use strict";

        var profileUid = GoTSkillZCommonUtilityFunctions.getUrlParam(window.location.href, "UID");


        $.ajax({
            url: membershipAPI + "getUserProfileMetaData/" + profileUid,
            type: "GET",
            dataType: "json",
            cache: false,
            contentType: "application/json; charset=utf-8",
            success: function (data) {


                if (data.StateCode === 8) {
                    GoTSkillZGateKeeperFunctions.redirectToHomePage("User Not Found.. Taking you back");
                    return;
                }

                if (data.UserProfile !== null) {

                    userObj = data.UserProfile;


                    if (profileUid.toLowerCase() === "gotskillz" || profileUid.toLowerCase() === "1") {
                        GoTSkillZCommonUtilityFunctions.setProfileUrlLink("gotskillz");
                    } else {
                        GoTSkillZCommonUtilityFunctions.setProfileUrlLink(userObj.UserId);
                    }


                    removedAchievements = [];


                    if (userObj.ReadOnly === true) {
                        $("#profile-header").show();
                        GoTSkillZProfileRegistrationFunctions.hideUserEditModal();
                        GoTSkillZProfileMetaDataFunctions.buildUserMetaProfile(userObj);
                    } else {
                        if (userObj.IsRegistered) {
                            $("#profile-header").show();
                            GoTSkillZProfileRegistrationFunctions.hideUserEditModal();
                            GoTSkillZProfileMetaDataFunctions.buildUserMetaProfile(userObj);
                        } else {
                            $("#profile-header").show();

                            GoTSkillZNotificationControls.ShowNotification("Please Complete Profile Registration", "warning");

                            GoTSkillZProfileRegistrationFunctions.showUserEditModal();
                        }
                    }



                }


            },
            complete: function () {
                $("#setup,#csgo-config").hide();
            },
            error: function () {
                GoTSkillZProfileMetaDataFunctions.setReadonly(true);

                GoTSkillZNotificationControls.ShowNotification("Could Not Get User Profile Meta Data, Please Contact Admin", "danger");

                GoTSkillZGateKeeperFunctions.redirectToHomePage();
            }
        });
    },
    getUserOccupations: function () {
        "use strict";
        $.ajax({
            url: membershipAPI + "GetUserOccupations",
            type: "GET",
            dataType: "json",
            cache: false,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                userOccupations = data;
            },
            error: function () {
                GoTSkillZNotificationControls.ShowNotification("Could Not Get Occupation Data, Please Contact Admin", "danger");
            }
        });

    },
    buildUserMetaProfile: function (userMetaData) {
        "use strict";

        if (userMetaData !== undefined && userMetaData !== null) {

            // set header 
            GoTSkillZProfileMetaDataFunctions.setProfileHeader(userMetaData);

            //set social links
            GoTSkillZProfileMetaDataFunctions.setSocialLinks(userMetaData.UserSocialLinks);

            //set contact info
            GoTSkillZProfileMetaDataFunctions.setContactInfo(userMetaData);

            //set about me
            GoTSkillZProfileMetaDataFunctions.setAboutMe(userMetaData.UserProfileExtension);

            // get player profile
            GoTSkillZPlayProfileFunctions.setPlayProfile(userMetaData.UserProfileExtension);

            //set achievements 
            GoTSkillZProfileAchievementsFunctions.buildUserAchievementsTimeLine(userMetaData.UserAchievements);


            //set team history
            GoTSkillZProfileTeamHistoryFunctions.buildTeamHistoryTimeLine(userMetaData.UserTeamHistory);


            //set setup
            GoTSkillZSetupDisplayFunctions.buildUserSetupContainers(userMetaData.UserSetupData);


            //set peripherals
            GoTSkillZSetupDisplayFunctions.buildUserPeripheralContainer(userMetaData.UserPeripheralData);


            //set CS GO COnfig
            GoTSKillZCSGoConfigInitializer.intializeFunctions(userMetaData);


            //set readonly
            GoTSkillZProfileMetaDataFunctions.setReadonly(userMetaData.ReadOnly);

        } else {
            GoTSkillZNotificationControls.ShowNotification("Unable to get user profile data", "danger");
            GoTSkillZProfileMetaDataFunctions.setReadonly(true);
        }

    },
    setSocialLinks: function (userSocialLinks) {
        "use strict";

        var socialContainer = $("#social-links");
        socialContainer.html("");
        if (userSocialLinks !== undefined && userSocialLinks !== null) {
            socialContainer.first().prepend("&nbsp;&nbsp;&#x2022; &nbsp;");
            if (userSocialLinks.Facebook !== null && userSocialLinks.Facebook !== "") {
                socialContainer.append('<a id="facebook-link" href="' + GoTSkillZCommonUtilityFunctions.formatUrl(userSocialLinks.Facebook) + '" class="tx-teal" target="blank"><i class="fab fa-facebook"></i></a>');
                $("#facebook-link").after("&nbsp;&nbsp;&#x2022; &nbsp;");
            }

            if (userSocialLinks.Mixer !== null && userSocialLinks.Mixer !== "") {
                socialContainer.append('<a id="mixer-link" href="' + GoTSkillZCommonUtilityFunctions.formatUrl(userSocialLinks.Mixer) + '" class="tx-teal" target="blank"><i class="icon-MixerMerge_Black"></i></a>');
                $("#mixer-link").after(" &nbsp;&nbsp;&#x2022; &nbsp;");
            }

            if (userSocialLinks.SoStronk !== null && userSocialLinks.SoStronk !== "") {
                socialContainer.append('<a id="soStronk-link" href="' + GoTSkillZCommonUtilityFunctions.formatUrl(userSocialLinks.SoStronk) + '" class="tx-teal" target="blank"><i class="icon-sostronk"></i></a>');
                $("#soStronk-link").after(" &nbsp;&nbsp;&#x2022; &nbsp;");
            }


            if (userSocialLinks.Twitch !== null && userSocialLinks.Twitch !== "") {
                socialContainer.append('<a id="twitch-link" href="' + GoTSkillZCommonUtilityFunctions.formatUrl(userSocialLinks.Twitch) + '" class="tx-teal" target="blank"><i class="fab fa-twitch"></i></a>');
                $("#twitch-link").after(" &nbsp;&nbsp;&#x2022; &nbsp;");
            }

            if (userSocialLinks.Twitter !== null && userSocialLinks.Twitter !== "") {
                socialContainer.append('<a id="twitter-link" href="' + GoTSkillZCommonUtilityFunctions.formatUrl(userSocialLinks.Twitter) + '" class="tx-teal" target="blank"><i class="fab fa-twitter"></i></a>');
                $("#twitter-link").after(" &nbsp;&nbsp;&#x2022; &nbsp;");
            }

            if (userSocialLinks.YouTube !== null && userSocialLinks.YouTube !== "") {
                socialContainer.append('<a id="youtube-link" href="' + GoTSkillZCommonUtilityFunctions.formatUrl(userSocialLinks.YouTube) + '" class="tx-teal" target="blank"><i class="fab fa-youtube"></i></a>');
                $("#youtube-link").after(" &nbsp;&nbsp;&#x2022; &nbsp;");
            }

            if (userSocialLinks.Steam !== null && userSocialLinks.Steam !== "") {
                socialContainer.append('<a id="steam-link" href="' + GoTSkillZCommonUtilityFunctions.formatUrl(userSocialLinks.Steam) + '" class="tx-teal" target="blank"><i class="fab fa-steam"></i></a>');
                $("#steam-link").after(" &nbsp;&nbsp;&#x2022; &nbsp;");
            }

            if (userSocialLinks.Faceit !== null && userSocialLinks.Faceit !== "") {
                socialContainer.append('<a id="faceit-link" href="' + GoTSkillZCommonUtilityFunctions.formatUrl(userSocialLinks.Faceit) + '" class="tx-teal" target="blank"><i class="icon-Black_Pheasant" style="font-size: 20px;"></i></a>');
                $("#faceit-link").after(" &nbsp;&nbsp;&#x2022; &nbsp;");
            }

            if (userSocialLinks.Instagram !== null && userSocialLinks.Instagram !== "") {
                socialContainer.append('<a id="instagram-link" href="' + GoTSkillZCommonUtilityFunctions.formatUrl(userSocialLinks.Instagram) + '" class="tx-teal" target="blank"><i class="fab fa-instagram"></i></a>');
                $("#instagram-link").after(" &nbsp;&nbsp;&#x2022; &nbsp;");
            }


            if (userSocialLinks.Discord !== null && userSocialLinks.Discord !== "") {
                socialContainer.append('<a id="discord-link" href="' + GoTSkillZCommonUtilityFunctions.formatUrl(userSocialLinks.Discord) + '" class="tx-teal" target="blank"><i class="fab fa-discord"></i></a>');
                $("#discord-link").after(" &nbsp;&nbsp;&#x2022; &nbsp;");
            }
        }
    },
    setContactInfo: function (userMetaData) {
        "use strict";

        if (userMetaData.ShowPersonalInfo) {
            if (userMetaData.Email !== null && userMetaData.Email !== "") {
                $("#info-email").html(userMetaData.Email);
            }

            if (userMetaData.TelNo !== null && userMetaData.TelNo !== "") {
                $("#info-tel").html(userMetaData.TelNo);
            }

            if (userMetaData.Age !== null && userMetaData.Age !== "") {
                $("#info-age").html(userMetaData.Age);
            }

            if (userMetaData.Country !== null && userMetaData.Country !== "") {
                $("#info-country").html(userMetaData.Country);
            } else {
                $("#info-country").html("---");
            }

            if (userMetaData.State !== null && userMetaData.State !== "") {
                $("#info-state").html(userMetaData.State);
            }


            if (userMetaData.City !== null && userMetaData.City !== "") {
                $("#info-city").html(userMetaData.City);
            }

            if (userMetaData.Gender !== null && userMetaData.Gender !== "") {
                $("#info-gender").html(userMetaData.Gender);
            }

            if (userMetaData.MaritalStatus !== null && userMetaData.MaritalStatus !== "") {
                $("#info-marital").html(userMetaData.MaritalStatus);
            }

            if (userMetaData.Occupation !== null && userMetaData.Occupation !== "") {
                $("#info-occupation").html(userMetaData.Occupation);
            }
        } else {
            $("#info-email, #info-tel, #info-age, #info-country, #info-state, #info-city, #info-pincode, #info-address, #info-gender, #info-marital, #info-occupation").html("Hidden");
        }
    },
    setAboutMe: function (userExtensionData) {
        "use strict";

        if (userExtensionData !== undefined && userExtensionData !== null) {
            if (userExtensionData.About !== "" && userExtensionData.About !== null) {
                $("#aboutme-container").html("<h5>" + userExtensionData.About + "</h5>");
            } else {
                $("#aboutme-container").html("<h5 style='text-align: center;vertical-align: middle;line-height: 90px;'>No Information Provided.</h5>");
            }
        } else {
            $("#aboutme-container").html("<h5 style='text-align: center;vertical-align: middle;line-height: 90px;'>No Information Provided.</h5>");
        }

    },
    setProfileHeader: function (profileData) {
        "use strict";
        var profileNameHtml, alias = profileData.UserProfileExtension.Alias;

        if (alias !== "") {
            profileNameHtml = '<h4 class="tx-normal tx-roboto pd-l-30">' + profileData.FirstName + ' "<span id="profile-alias">' + alias + '</></span>" ' + profileData.LastName + " </h4>";
        } else {
            profileNameHtml = '<h4 class="tx-white tx-roboto">' + profileData.FirstName + " " + profileData.LastName + " </h4>";
        }

        $("#profile-image").attr("src", profileData.ProfileImage);

        $("#profile-name").html("").append(profileNameHtml);
    },
    setReadonly: function (readOnly) {
        "use strict";

        if (readOnly) {
            $("#edit-aboutme, #edit-myinfo, #edit-playerProfile, #edit-achievements, #edit-teamhistory, #rt-add-button,.setup-edit,.peripheral-edit").hide();
        } else {
            $("#edit-aboutme, #edit-myinfo, #edit-playerProfile, #edit-achievements, #edit-teamhistory, #rt-add-button,.setup-edit,.peripheral-edit").show();
        }
    }
};

var GoTSkillZPlayProfileFunctions = {
    setPlayProfile: function (userExtensionData) {
        "use strict";
        if (userExtensionData !== undefined && userExtensionData !== null) {

            //set alias
            if (userExtensionData.Alias !== "" && userExtensionData.Alias !== null) {
                $("#player-alias").html("").html(userExtensionData.Alias);
            } else {
                $("#player-alias").html("").html("No Information Provided.");
            }

            //set primary game
            if (userExtensionData.PrimaryGame !== "" && userExtensionData.PrimaryGame !== null) {
                $("#player-primarygame").html("").html(userExtensionData.PrimaryGame);
            } else {
                $("#player-primarygame").html("").html("No Information Provided.");
            }


            //set primary role
            if (userExtensionData.PrimaryRole !== "" && userExtensionData.PrimaryRole !== null) {
                $("#player-primaryrole").html("").html(userExtensionData.PrimaryRole);
            } else {
                $("#player-primaryrole").html("").html("No Information Provided.");
            }

            //set secondary game
            if (userExtensionData.SecondaryGame !== "" && userExtensionData.SecondaryGame !== null) {
                $("#player-secondarygame").html("").html(userExtensionData.SecondaryGame);
            } else {
                $("#player-secondarygame").html("").html("No Information Provided.");
            }

            //set secondary role
            if (userExtensionData.SecondaryRole !== "" && userExtensionData.SecondaryRole !== null) {
                $("#player-secondaryrole").html("").html(userExtensionData.SecondaryRole);
            } else {
                $("#player-secondaryrole").html("").html("No Information Provided.");
            }

            //set status role
            if (userExtensionData.Status !== "" && userExtensionData.Status !== null) {
                $("#player-status").html("").html(userExtensionData.Status);
            } else {
                $("#player-status").html("").html("No Information Provided.");
            }

            //set primarygameexp role
            if (userExtensionData.PrimaryGameExp !== "" && userExtensionData.PrimaryGameExp !== null) {
                $("#player-primarygameexp").html("").html("Has Been Playing For " + userExtensionData.PrimaryGameExp);
            } else {
                $("#player-primarygameexp").html("").html("No Information Provided.");
            }


            //set primarygameexp role
            if (userExtensionData.SecondaryGameExp !== "" && userExtensionData.SecondaryGameExp !== null) {
                $("#player-secondarygameexp").html("").html("Has Been Playing For " + userExtensionData.SecondaryGameExp);
            } else {
                $("#player-secondarygameexp").html("").html("No Information Provided.");
            }

            //set current team
            if (userExtensionData.CurrentTeam !== "" && userExtensionData.CurrentTeam !== null) {
                $("#player-currentteam").html("").html(userExtensionData.CurrentTeam);
            } else {
                $("#player-currentteam").html("").html("No Information Provided.");
            }


        } else {

            $("#player-alias, #player-primarygame, #player-primaryrole, #player-secondarygame, #player-secondaryrole, #player-status, #player-primarygameexp, #player-secondarygameexp, #player-currentteam").html("").html("No Information Provided.");
        }

    }
};

var GoTSkillZGameFunctions = {
    getGameTypes: function () {
        "use strict";

        $.ajax({
            url: gamerDataAPI + "GetAllGameTypes",
            type: "GET",
            dataType: "json",
            cache: false,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                gameTypes = data;
            },
            complete: function () {

            },
            error: function () {
                GoTSkillZNotificationControls.ShowNotification("Could Not Get Game Types Data, Please Contact Admin", "danger");
            }
        });
    },
    getAllGameRoles: function () {
        "use strict";

        $.ajax({
            url: gamerDataAPI + "GetAllGameRoles",
            type: "GET",
            dataType: "json",
            cache: false,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                gameRoles = data;
            },
            complete: function () {

            },
            error: function () {
                GoTSkillZNotificationControls.ShowNotification("Could Not Get Game Roles Data, Please Contact Admin", "danger");
            }
        });
    }
};

var GoTSkillZProfileAchievementsFunctions = {
    initializeAchievementFormDDLs: function (gameTypeId, positionId) {
        "use strict";


        var gameTypeDataSource = _.map(gameTypes, function (value) {
            return { id: value.Id, text: value.GameName };
        });


        gameTypeDataSource.unshift({
            'id': -1,
            'text': "Select Game Type"
        });

        if (gameTypeId !== "") {
            $("#" + gameTypeId).kendoDropDownList({
                dataTextField: "text",
                dataValueField: "id",
                dataSource: gameTypeDataSource
            });
        }

        var positionDataSource = [
            {
                'id': 1,
                'text': "Select Placement Position"
            },
            {
                'id': 1,
                'text': "1st Place"
            },
            {
                'id': 2,
                'text': "2nd Place"
            },
            {
                'id': 3,
                'text': "3rd Place"
            },
            {
                'id': 4,
                'text': "Representation"
            }
        ];

        if (positionId !== "") {
            $("#" + positionId).kendoDropDownList({
                dataTextField: "text",
                dataValueField: "id",
                dataSource: positionDataSource
            });
        }







    },
    userAchievementFormHTML: function (id) {
        "use strict";

        var achievementDiv = $("<div>",
            {
                'id': "achievement-item-" + id,
                'class': "achievement-item"
            });


        var removeBtnDiv = $("<div>",
            {
                "class": "t-15 r-25"
            });

        var removeAchievementBtn = $("<a>", {
            'href': " ",
            "id": "remove-achievement-" + id,
            "class": "tx-white-5 hover-info remove-achivement"
        });

        var removeAchievementBtnIcon = $("<i>", {
            "class": "icon ion-minus tx-12"
        });


        var inputTournamentName = $("<input>", {
            'id': "regi-achievement-name-" + id,
            'type': "text",
            'class': "form-control form-control-dark regi-achievement-name",
            'placeholder': "Tournament Name"

        }).attr("required", true);

        var inputDescription = $("<input>", {
            'id': "regi-achievement-description-" + id,
            'type': "text",
            'class': "form-control form-control-dark regi-achievement-description",
            'placeholder': "Description, Eg: Placed 1st in ESL"
        }).attr("required", true);


        var inputPosition = $("<input>", {
            'id': "regi-achievement-position-" + id,
            'class': "form-control form-control-dark regi-achievement-position",
            'placeholder': "Select Placement Position"
        }).attr("required", true);

        var inputGameType = $("<input>", {
            'id': "regi-achievement-gameType-" + id,
            'class': "form-control form-control-dark regi-achievement-gameType"
        }).attr("required", true);

        var inputDate = $("<input>", {
            'id': "regi-achievement-date-" + id,
            'type': "text",
            'class': "form-control fc-datepicker form-control-dark regi-achievement-date",
            'placeholder': "DD/MM/YYYY"
        }).attr("required", true);

        var inputLocation = $("<input>", {
            'id': "regi-achievement-location-" + id,
            'type': "text",
            'class': "form-control form-control-dark regi-achievement-location",
            'placeholder': "Location, Eg: ESL Mumbai"
        }).attr("required", true);


        var divRow1 = $("<div>", {
            'class': "row mg-b-10"
        });

        var divCol1_1 = $("<div>", {
            'class': "col-lg-4 mg-b-5"
        });

        var divCol1_2 = $("<div>", {
            'class': "col-lg-4 mg-b-5"
        });

        var divCol1_3 = $("<div>", {
            'class': "col-lg-4 mg-b-5"
        });

        var divInputGroup1_1 = $("<div>", {
            'class': "input-group input-group-dark"
        });
        var divInputGroup1_2 = $("<div>", {
            'class': "input-group input-group-dark"
        });

        var divInputGroup1_3 = $("<div>", {
            'class': "input-group input-group-dark"
        });

        divCol1_1.append(divInputGroup1_1.append(inputTournamentName));
        divCol1_2.append(divInputGroup1_2.append(inputDescription));
        divCol1_3.append(divInputGroup1_3.append(inputPosition));
        divRow1.append(divCol1_1).append(divCol1_2).append(divCol1_3);
        achievementDiv.append(divRow1);


        var divRow2 = $("<div>", {
            'class': "row mg-b-10"
        });

        var divCol2_1 = $("<div>", {
            'class': "col-lg-4 mg-b-5"
        });

        var divCol2_2 = $("<div>", {
            'class': "col-lg-4 mg-b-5"
        });

        var divCol2_3 = $("<div>", {
            'class': "col-lg-4 mg-b-5"
        });

        var divInputGroup2_1 = $("<div>", {
            'class': "input-group input-group-dark"
        });

        var divInputGroup2_2 = $("<div>", {
            'class': "input-group input-group-dark"
        });

        var divInputGroup2_3 = $("<div>", {
            'class': "input-group input-group-dark"
        });

        divCol2_1.append(divInputGroup2_1.append(inputGameType));
        divCol2_2.append(divInputGroup2_2.append(inputDate));
        divCol2_3.append(divInputGroup2_3.append(inputLocation));
        divRow2.append(divCol2_1).append(divCol2_2).append(divCol2_3);
        achievementDiv.append(divRow2);

        removeBtnDiv.append(removeAchievementBtn.append(removeAchievementBtnIcon).append("&nbsp;Remove"));

        var divRow3 = $("<div>", {
            'class': "row mg-b-10"
        });

        var divCol3 = $("<div>", {
            'class': "col-lg-12 mg-b-5"
        });
        divCol3.append(removeAchievementBtn);
        divRow3.append(divCol3);

        achievementDiv.append(divRow3);

        return achievementDiv;

    },
    buildUserAchievementForm: function (clean, newDiv, existingAchievementsObj) {
        "use strict";

        var achievementFormContainer = $("#regi-achievements-container");

        if (clean === true)
            achievementFormContainer.html("");

        var currentDivCount = $("#regi-achievements-container").find(".achievement-item").length;


        if (newDiv === true) {
            var currentIndex = currentDivCount + 1;
            achievementFormContainer.prepend(GoTSkillZProfileAchievementsFunctions.userAchievementFormHTML(currentIndex));
            GoTSkillZProfileAchievementsFunctions.initializeAchievementFormDDLs("regi-achievement-gameType-" + currentIndex, "regi-achievement-position-" + currentIndex);
        }


        if (newDiv === false && existingAchievementsObj !== null) {

            $.each(existingAchievementsObj, function (index, item) {

                var currentContainer = GoTSkillZProfileAchievementsFunctions.userAchievementFormHTML(index);
                achievementFormContainer.append(currentContainer);


                GoTSkillZProfileAchievementsFunctions.populateAchievementForm(currentContainer, item);
            });

        }


        $(".regi-achievement-date").datepicker({
            showOtherMonths: true,
            selectOtherMonths: true,
            dateFormat: "dd/mm/yy"
        });




        GoTSkillZProfileBinders.bindAchievementRemoveBtn();

    },
    populateAchievementForm: function (container, achievementObj) {
        "use strict";

        if (container !== undefined && container !== null && achievementObj !== undefined && achievementObj !== null) {
            $(container).attr("data-id", achievementObj.Id);

            //set name
            $(container).find(".regi-achievement-name").val(achievementObj.Name);

            //set description
            $(container).find(".regi-achievement-description").val(achievementObj.Description);

            //set date
            $(container).find(".regi-achievement-date").val(achievementObj.Date);

            //set location
            $(container).find(".regi-achievement-location").val(achievementObj.Location);


            //set position

            $(container).find("input").each(function (index, item) {

                if (item.className.indexOf("regi-achievement-position") > 0) {
                    GoTSkillZProfileAchievementsFunctions.initializeAchievementFormDDLs("", this.id);

                    $(item).data("kendoDropDownList").text(achievementObj.Position);
                }

                if (item.className.indexOf("regi-achievement-gameType") > 0) {
                    GoTSkillZProfileAchievementsFunctions.initializeAchievementFormDDLs(this.id, "");
                    $(item).data("kendoDropDownList").text(achievementObj.Type);
                }

            });


        }

    },
    removeUserAchievementFormDiv: function (e) {
        "use strict";

        var dataId = e.currentTarget.parentElement.parentElement.parentElement.getAttribute("data-id");

        if (dataId !== null) {
            removedAchievements.push(dataId);
        }


        e.target.parentElement.parentElement.parentElement.remove();

    },
    buildUserAchievementsTimeLine: function (userAchievements) {
        "use strict";
        var achievementContainer = $("#achievement-container");

        achievementContainer.html("");
        if (userAchievements != null && userAchievements.length > 0) {

            $.each(userAchievements, function (index, item) {

                if (item.IsActive === "True" && item.Date !== "") {
                    var timeLineDiv = $("<div>", { "class": "timeline" });
                    var timelineIcon = "";
                    var icon = "";
                    var position = item.Position;

                    switch (position) {
                        case "1st Place":
                            timelineIcon = $("<div>", { "class": "timeline-icon" });
                            icon = $("<i>", { "class": "fa fa-trophy" });
                            timelineIcon.append(icon);
                            break;
                        case "2nd Place":
                            timelineIcon = $("<div>", { "class": "timeline-icon" });
                            icon = $("<i>", { "class": "fa fa-medal" });
                            timelineIcon.append(icon);
                            break;
                        case "Representation":
                            timelineIcon = $("<div>", { "class": "timeline-icon" });
                            icon = $("<i>", { "class": "fa fa-award" });
                            timelineIcon.append(icon);
                            break;

                    }

                    var dateMomentObject = moment(item.Date, "DD/MM/YYYY");
                    var dateObject = dateMomentObject.toDate();


                    var span = $("<span>", { "class": "year" }).html(dateObject.getFullYear());

                    var timeLineContentDiv = $("<div>", { "class": "timeline-content" });
                    var title = $("<h5>", { "class": "title" }).html(item.Name + " - " + item.Type);
                    var positionDiv = $("<p>", { "class": "position-sub" }).html("Position: " + item.Position);
                    var description = $("<p>", { "class": "description" }).html(item.Description + "<br />");
                    var descriptionSub = $("<p>", { "class": "description-sub" }).html("Location: " + item.Location);

                    timeLineContentDiv.append(title).append(positionDiv).append(description).append(descriptionSub);


                    timeLineDiv.append(timelineIcon).append(span).append(timeLineContentDiv);
                    achievementContainer.append(timeLineDiv);
                }


            });
        } else {
            achievementContainer.html("<h5 style='text-align: center;vertical-align: middle;line-height: 90px;'>No Information Provided.</h5>");
        }
    }
};

var GoTSkillZProfileTeamHistoryFunctions = {
    userTeamHistoryFormHTML: function (id) {
        "use strict";

        var teamHistoryDiv = $("<div>",
            {
                'id': "teamhistory-item-" + id,
                'class': "teamhistory-item"
            });


        var removeBtnDiv = $("<div>",
            {
                "class": "t-15 r-25"
            });

        var removeTeamHistoryBtn = $("<a>", {
            'href': " ",
            "id": "remove-teamhistory-" + id,
            "class": "tx-white-5 hover-info remove-teamhistory"
        });

        var removeTeamHistoryBtnIcon = $("<i>", {
            "class": "icon ion-minus tx-12"
        });

        //data-toggle="tooltip-primary" data-placement="bottom" title="Since How Long Have You Been Playing This Game"
        var inputTeamName = $("<input>", {
            'id': "regi-teamhistoryname-" + id,
            'type': "text",
            'class': "form-control form-control-dark regi-teamhistoryname",
            'placeholder': "Team Name"
        });

        var inputFromDate = $("<input>", {
            'id': "regi-teamhistoryfromdate-" + id,
            'type': "text",
            'class': "form-control form-control-dark regi-teamhistoryfromdate",
            'placeholder': "From Date",
            'data-toggle': 'tooltip-primary',
            'data-placement': 'top',
            'title': 'From Year'
        });


        var inputToDate = $("<input>", {
            'id': "regi-teamhistorytodate-" + id,
            'type': "text",
            'class': "form-control form-control-dark regi-teamhistorytodate",
            'placeholder': "To Date",
            'data-toggle': 'tooltip-primary',
            'data-placement': 'top',
            'title': 'To Year'
        });




        var divRow1 = $("<div>", {
            'class': "row mg-b-10"
        });

        var divCol1_1 = $("<div>", {
            'class': "col-lg-4 mg-b-5"
        });
        var divCol1_2 = $("<div>", {
            'class': "col-lg-4 mg-b-5"
        });
        var divCol1_3 = $("<div>", {
            'class': "col-lg-4 mg-b-5"
        });

        var divInputGroup1_1 = $("<div>", {
            'class': "input-group input-group-dark"
        });

        var divInputGroup1_2 = $("<div>", {
            'class': "input-group input-group-dark"
        });

        var divInputGroup1_3 = $("<div>", {
            'class': "input-group input-group-dark"
        });



        divCol1_1.append(divInputGroup1_1.append(inputTeamName));
        divCol1_2.append(divInputGroup1_2.append(inputFromDate));
        divCol1_3.append(divInputGroup1_3.append(inputToDate));
        divRow1.append(divCol1_1).append(divCol1_2).append(divCol1_3);
        teamHistoryDiv.append(divRow1);


        removeBtnDiv.append(removeTeamHistoryBtn.append(removeTeamHistoryBtnIcon).append("&nbsp;Remove"));

        var divRow3 = $("<div>", {
            'class': "row mg-b-10"
        });

        var divCol3 = $("<div>", {
            'class': "col-lg-12 mg-b-5"
        });
        divCol3.append(removeTeamHistoryBtn);
        divRow3.append(divCol3);

        teamHistoryDiv.append(divRow3);

        return teamHistoryDiv;

    },
    buildUserTeamHistoryForm: function (clean, newDiv, existingTeamHistoryObj) {
        "use strict";

        var teamhistoryFormContainer = $("#regi-teamhistory-container");

        if (clean === true)
            teamhistoryFormContainer.html("");

        var currentDivCount = $("#regi-teamhistory-container").find(".teamhistory-item").length;


        if (newDiv === true) {
            teamhistoryFormContainer.prepend(GoTSkillZProfileTeamHistoryFunctions.userTeamHistoryFormHTML(currentDivCount + 1));
            GoTSkillZProfileInitializers.initializeTeamHistoryYearPicker();
        }


        if (newDiv === false && existingTeamHistoryObj !== null) {

            $.each(existingTeamHistoryObj, function (index, item) {

                var currentContainer = GoTSkillZProfileTeamHistoryFunctions.userTeamHistoryFormHTML(index);
                teamhistoryFormContainer.append(currentContainer);

                GoTSkillZProfileInitializers.initializeTeamHistoryYearPicker();
                GoTSkillZProfileTeamHistoryFunctions.popluateTeamHistoryForm(currentContainer, item);
            });

        }


        GoTSkillZProfileBinders.bindTeamHistoryRemoveBtn();
        GoTSkillZInitializeCommonControls.initializeColoredToolTip();

    },
    popluateTeamHistoryForm: function (container, teamHistoryObj) {
        "use strict";

        if (container !== undefined && container !== null && teamHistoryObj !== undefined && teamHistoryObj !== null) {
            $(container).attr("data-id", teamHistoryObj.Id);

            //set Team name
            $(container).find(".regi-teamhistoryname").val(teamHistoryObj.TeamName);

            //set From Date
            $(container).find(".regi-teamhistoryfromdate").val(teamHistoryObj.FromDate);

            //set To Date
            $(container).find(".regi-teamhistorytodate").val(teamHistoryObj.ToDate);

        }

    },
    removeUserTeamHistoryFormDiv: function (e) {
        "use strict";

        var dataId = e.currentTarget.parentElement.parentElement.parentElement.getAttribute("data-id");

        if (dataId !== null) {
            removedTeamHistory.push(dataId);
        }


        e.target.parentElement.parentElement.parentElement.remove();

    },
    buildTeamHistoryTimeLine: function (userTeamHistory) {
        "use strict";

        var teamhistoryContainer = $("#teamhistory-container");

        teamhistoryContainer.html("");
        if (userTeamHistory != null && userTeamHistory.length > 0) {
            var timeLineList = $("<ul>", { "class": "teamhistory-timeline" });
            $.each(userTeamHistory, function (index, item) {


                var listItem = $("<li>");

                var teamName = $("<a>").html(item.TeamName);
                var date = $("<a>", { "class": "float-right" }).html(item.FromDate + " &rarr; " + item.ToDate);

                listItem.append(teamName).append(date);
                timeLineList.append(listItem);


            });

            teamhistoryContainer.append(timeLineList);
        } else {
            teamhistoryContainer.html("<h5 style='text-align: center;vertical-align: middle;line-height: 90px;'>No Information Provided.</h5>");
        }

    }
};

var GoTSkillZProfileRegistrationFunctions = {
    showUserEditModal: function () {
        "use strict";

        //init wized
        if (!$("#user-profile-wizard").hasClass("wizard")) {
            $("#user-profile-wizard").steps({
                headerTag: "h3",
                bodyTag: "section",
                autoFocus: true,
                enableAllSteps: true,
                titleTemplate: '<span class="number">#index#</span> <span class="title hidden-xs-down">#title#</span>',
                cssClass: "wizard wizard-style-3",
                onInit: function () {
                    GoTSkillZProfileInitializers.initializeGenderDDL();
                    GoTSkillZProfileInitializers.initializeMaritalDDL();
                    GoTSkillZProfileInitializers.initializeOccupationDDL();
                    GoTSkillZProfileInitializers.initializePrimaryGameTypeDDL();
                    GoTSkillZProfileInitializers.initializeSecondaryGameTypeDDL();
                    GoTSkillZProfileInitializers.initializePlayerStatusDDL();
                    GoTSkillZProfileInitializers.initializePlayerGameExpDDL();
                    GoTSkillZInitializeCommonControls.initializeColoredToolTip();

                    GoTSkillZProfileRegistrationFunctions.setExistingProfileData();

                    // hide default scroll bar
                    $("#user-edit-modal").find(".modal-body").css("overflow-y", "hidden");


                    GoTSkillZProfileBinders.bindAchievementAddBtn();
                    GoTSkillZProfileBinders.bindTeamHistoryAddBtn();


                    //initalize perfectScrollbar 

                    var perfectScrollbar = new PerfectScrollbar(".modal-body", {
                        wheelPropagation: true,
                        minScrollbarLength: 20
                    });
                },
                onStepChanging: function (event, currentIndex, newIndex) {
                    if (currentIndex < newIndex) {
                        // Step 1 form validation
                        if (currentIndex === 0) {
                            return GoTSkillZRegistrationFormValidations.validatePersonalInfo();
                        } else if (currentIndex === 4) { // Step 4 form validation

                            var validatedAchievements = GoTSkillZRegistrationFormValidations.validateAchievementForm();

                            if (validatedAchievements === false) {
                                GoTSkillZNotificationControls.ShowNotification("Achievement Form Incomplete, Please Input All Form Values", "danger");
                            } else {
                                return true;
                            }


                        }
                        else if (currentIndex === 5) { // Step 5 form validation
                            var validatedSocialLinks = GoTSkillZRegistrationFormValidations.validateSocialLinksForm();

                            if (validatedSocialLinks.isValid === false) {
                                GoTSkillZNotificationControls.ShowNotification("Invalid Url, Please Provide Complete url for: <b>" + validatedSocialLinks.linkType + "</b>", "danger");
                            } else {
                                return true;
                            }
                        }
                        else {
                            return true;
                        }

                        // Always allow step back to the previous step even if the current step is not valid.
                    } else {
                        return true;
                    }
                },
                onFinished: function (event, currentIndex, newIndex) {
                    GoTSkillZProfileRegistrationFunctions.saveProfileData();
                }
            });
        }


        $(".fc-datepicker").datepicker({
            showOtherMonths: true,
            selectOtherMonths: true,
            dateFormat: "dd/mm/yy"
        });


        $("#user-edit-modal").modal({ backdrop: "static", keyboard: false });


        GoTSkillZProfileInitializers.getUserProfileImage();
        GoTSkillZProfileInitializers.initializeSummerNote();
        GoTSkillZProfileInitializers.initializeCountryDDL();


    },
    hideUserEditModal: function () {
        "use strict";

        $("#user-edit-modal").modal("hide");
    },
    setExistingProfileData: function () {
        "use strict";

        if (userObj !== undefined) {
            $("#regi-firstname").val(userObj.FirstName);
            $("#regi-lastname").val(userObj.LastName);
            $("#regi-email").val(userObj.Email);
            $("#regi-phonenumber").val(userObj.TelNo);
            $("#regi-age").val(userObj.Age);
            $("#regi-dob").val(userObj.DOB);
            $("#regi-address").val(userObj.Address);
            $("#regi-pincode").val(userObj.PinCode);


            $("#regi-country-ddl").val(userObj.Country);

            $("#regi-state-ddl").val(userObj.State);

            $("#regi-city-ddl").val(userObj.City);


            if (userObj.ShowPersonalInfo) {
                $("#regi-showprofile").attr("checked", "checked");
            }

            //user profile extension data
            if (userObj.UserProfileExtension != null) {
                $("#regi-aboutme").html("").html(userObj.UserProfileExtension.About);

                $("#regi-alias").val(userObj.UserProfileExtension.Alias);


                $("#regi-currentteam").val(userObj.UserProfileExtension.CurrentTeam);


                if (userObj.UserProfileExtension.PrimaryGame !== null && userObj.UserProfileExtension.PrimaryGame !== "")
                    $("#regi-primarygame").data("kendoDropDownList").text(userObj.UserProfileExtension.PrimaryGame);


                if (userObj.UserProfileExtension.SecondaryGame !== null && userObj.UserProfileExtension.SecondaryGame !== "")
                    $("#regi-secondarygame").data("kendoDropDownList").text(userObj.UserProfileExtension.SecondaryGame);


                if (userObj.UserProfileExtension.PrimaryRole !== null && userObj.UserProfileExtension.PrimaryRole !== "")
                    $("#regi-primaryrole").data("kendoDropDownList").text(userObj.UserProfileExtension.PrimaryRole);

                if (userObj.UserProfileExtension.SecondaryRole !== null && userObj.UserProfileExtension.SecondaryRole !== "")
                    $("#regi-secondaryrole").data("kendoDropDownList").text(userObj.UserProfileExtension.SecondaryRole);

                if (userObj.UserProfileExtension.Status !== null && userObj.UserProfileExtension.Status !== "")
                    $("#regi-playerstatus").data("kendoDropDownList").text(userObj.UserProfileExtension.Status);

                if (userObj.UserProfileExtension.PrimaryGameExp !== null && userObj.UserProfileExtension.PrimaryGameExp !== "")
                    $("#regi-primarygameexp").data("kendoDropDownList").text(userObj.UserProfileExtension.PrimaryGameExp);


                if (userObj.UserProfileExtension.SecondaryGameExp !== null && userObj.UserProfileExtension.SecondaryGameExp !== "")
                    $("#regi-secondarygameexp").data("kendoDropDownList").text(userObj.UserProfileExtension.SecondaryGameExp);


            }

            if (userObj.Gender !== null && userObj.Gender !== "")
                $("#regi-gender").data("kendoDropDownList").text(userObj.Gender);


            if (userObj.MaritalStatus !== null && userObj.MaritalStatus !== "")
                $("#regi-marital").data("kendoDropDownList").text(userObj.MaritalStatus);


            if (userObj.Occupation !== null && userObj.Occupation !== "")
                $("#regi-occupation").data("kendoDropDownList").text(userObj.Occupation);


            //user social links
            if (userObj.UserSocialLinks !== null) {
                $("#regi-youtube").val(userObj.UserSocialLinks.YouTube);
                $("#regi-twitch").val(userObj.UserSocialLinks.Twitch);
                $("#regi-faceit").val(userObj.UserSocialLinks.Faceit);
                $("#regi-steam").val(userObj.UserSocialLinks.Steam);
                $("#regi-sostronk").val(userObj.UserSocialLinks.SoStronk);
                $("#regi-instagram").val(userObj.UserSocialLinks.Instagram);
                $("#regi-facebook").val(userObj.UserSocialLinks.Facebook);
                $("#regi-twitter").val(userObj.UserSocialLinks.Twitter);
                $("#regi-mixer").val(userObj.UserSocialLinks.Mixer);
                $("#regi-discord").val(userObj.UserSocialLinks.Discord);
            }

            if (userObj.UserAchievements !== null) {
                GoTSkillZProfileAchievementsFunctions.buildUserAchievementForm(true, false, userObj.UserAchievements);
            }


            //user team history
            if (userObj.UserTeamHistory !== null) {
                GoTSkillZProfileTeamHistoryFunctions.buildUserTeamHistoryForm(true, false, userObj.UserTeamHistory);
            }
        }
    },
    saveProfileData: function () {
        "use strict";

        if (userObj != null) {
            // basic user profle
            userObj.FirstName = $("#regi-firstname").val();
            userObj.LastName = $("#regi-lastname").val();
            userObj.Email = $("#regi-email").val();
            userObj.TelNo = $("#regi-phonenumber").val();
            userObj.Age = $("#regi-age").val() === "" ? "0" : $("#regi-age").val();
            userObj.DOB = $("#regi-dob").val();


            userObj.Country = $("#regi-country-ddl").val();
            userObj.State = $("#regi-state-ddl").val();
            userObj.City = $("#regi-city-ddl").val();


            userObj.Address = $("#regi-address").val();
            userObj.PinCode = $("#regi-pincode").val();
            userObj.ShowPersonalInfo = $("#regi-showprofile").is(":checked");


            if ($("#regi-gender").data("kendoDropDownList").text() !== "Select Gender") {
                userObj.Gender = $("#regi-gender").data("kendoDropDownList").text();
            }

            if ($("#regi-marital").data("kendoDropDownList").text() !== "Select Marital Status") {
                userObj.MaritalStatus = $("#regi-marital").data("kendoDropDownList").text();
            }

            if ($("#regi-occupation").data("kendoDropDownList").text() !== "Select Occupation") {
                userObj.Occupation = $("#regi-occupation").data("kendoDropDownList").text();
            }


            // user Profile Extension
            if (userObj.UserProfileExtension === null) {

                var newUserProfileExtension = new GoTSkillzEntities.UserProfileExtensionDTO();
                newUserProfileExtension.About = $("#regi-aboutme").val();
                newUserProfileExtension.Alias = $("#regi-alias").val();
                newUserProfileExtension.CurrentTeam = $("#regi-currentteam").val();


                if ($("#regi-primarygame").data("kendoDropDownList").text() !== "Select Primary Game") {
                    newUserProfileExtension.PrimaryGame = $("#regi-primarygame").data("kendoDropDownList").text();
                }

                if ($("#regi-secondarygame").data("kendoDropDownList").text() !== "Select Secondary Game") {
                    newUserProfileExtension.SecondaryGame = $("#regi-secondarygame").data("kendoDropDownList").text();
                }


                if ($("#regi-primaryrole").data("kendoDropDownList").text() !== "Select Primary Role") {
                    newUserProfileExtension.PrimaryRole = $("#regi-primaryrole").data("kendoDropDownList").text();
                }

                if ($("#regi-secondaryrole").data("kendoDropDownList").text() !== "Select Secondary Role") {
                    newUserProfileExtension.SecondaryRole = $("#regi-secondaryrole").data("kendoDropDownList").text();
                }

                if ($("#regi-playerstatus").data("kendoDropDownList").text() !== "Select Status") {
                    newUserProfileExtension.Status = $("#regi-playerstatus").data("kendoDropDownList").text();
                }

                if ($("#regi-primarygameexp").data("kendoDropDownList").text() !== "Select Experience") {
                    newUserProfileExtension.PrimaryGameExp = $("#regi-primarygameexp").data("kendoDropDownList").text();
                }

                if ($("#regi-secondarygameexp").data("kendoDropDownList").text() !== "Select Experience") {
                    newUserProfileExtension.SecondaryGameExp = $("#regi-secondarygameexp").data("kendoDropDownList").text();
                }


            } else {
                userObj.UserProfileExtension.About = $("#regi-aboutme").val();
                userObj.UserProfileExtension.Alias = $("#regi-alias").val();
                userObj.UserProfileExtension.CurrentTeam = $("#regi-currentteam").val();


                if ($("#regi-primarygame").data("kendoDropDownList").text() !== "Select Game") {
                    userObj.UserProfileExtension.PrimaryGame = $("#regi-primarygame").data("kendoDropDownList").text();
                }

                if ($("#regi-secondarygame").data("kendoDropDownList").text() !== "Select Game") {
                    userObj.UserProfileExtension.SecondaryGame = $("#regi-secondarygame").data("kendoDropDownList").text();
                }


                if ($("#regi-primaryrole").data("kendoDropDownList").text() !== "Select Primary Role") {
                    userObj.UserProfileExtension.PrimaryRole = $("#regi-primaryrole").data("kendoDropDownList").text();
                }

                if ($("#regi-secondaryrole").data("kendoDropDownList").text() !== "Select Secondary Role") {
                    userObj.UserProfileExtension.SecondaryRole = $("#regi-secondaryrole").data("kendoDropDownList").text();
                }

                if ($("#regi-playerstatus").data("kendoDropDownList").text() !== "Select Status") {
                    userObj.UserProfileExtension.Status = $("#regi-playerstatus").data("kendoDropDownList").text();

                }


                if ($("#regi-primarygameexp").data("kendoDropDownList").text() !== "Select Experience") {
                    userObj.UserProfileExtension.PrimaryGameExp = $("#regi-primarygameexp").data("kendoDropDownList").text();
                }

                if ($("#regi-secondarygameexp").data("kendoDropDownList").text() !== "Select Experience") {
                    userObj.UserProfileExtension.SecondaryGameExp = $("#regi-secondarygameexp").data("kendoDropDownList").text();
                }

            }


            //user social links
            if (userObj.UserSocialLinks !== null) {
                userObj.UserSocialLinks.YouTube = $("#regi-youtube").val();
                userObj.UserSocialLinks.Twitch = $("#regi-twitch").val();
                userObj.UserSocialLinks.Faceit = $("#regi-faceit").val();
                userObj.UserSocialLinks.Steam = $("#regi-steam").val();
                userObj.UserSocialLinks.SoStronk = $("#regi-sostronk").val();
                userObj.UserSocialLinks.Instagram = $("#regi-instagram").val();
                userObj.UserSocialLinks.Facebook = $("#regi-facebook").val();
                userObj.UserSocialLinks.Twitter = $("#regi-twitter").val();
                userObj.UserSocialLinks.Mixer = $("#regi-mixer").val();
                userObj.UserSocialLinks.Discord = $("#regi-discord").val();
            } else {
                var newUserSocialLinksObj = new GoTSkillzEntities.UserSocialLinksDTO();
                newUserSocialLinksObj.userId = userObj.UserId;
                newUserSocialLinksObj.YouTube = $("#regi-youtube").val();
                newUserSocialLinksObj.Twitch = $("#regi-twitch").val();
                newUserSocialLinksObj.Faceit = $("#regi-faceit").val();
                newUserSocialLinksObj.Steam = $("#regi-steam").val();
                newUserSocialLinksObj.SoStronk = $("#regi-sostronk").val();
                newUserSocialLinksObj.Instagram = $("#regi-instagram").val();
                newUserSocialLinksObj.Facebook = $("#regi-facebook").val();
                newUserSocialLinksObj.Twitter = $("#regi-twitter").val();
                newUserSocialLinksObj.Mixer = $("#regi-mixer").val();
                newUserSocialLinksObj.Discord = $("#regi-discord").val();
                userObj.UserSocialLinks = newUserSocialLinksObj;
            }


            if ($("#regi-achievements-container").find(".achievement-item").length > 0) {

                var userAchievementList = [];
                $("#regi-achievements-container").find(".achievement-item").each(function (index, item) {

                    var newAchievementItem = new GoTSkillzEntities.UserAchievementsDTO();
                    var dataId = this.getAttribute("data-id");

                    if (dataId !== null) {
                        newAchievementItem.Id = dataId;
                    } else {
                        newAchievementItem.Id = 0;
                    }

                    newAchievementItem.Name = $(this).find(".regi-achievement-name").val();
                    newAchievementItem.Description = $(this).find(".regi-achievement-description").val();
                    newAchievementItem.Location = $(this).find(".regi-achievement-location").val();
                    newAchievementItem.Date = $(this).find(".regi-achievement-date").val();


                    if ($($(this).find(".regi-achievement-position")[1]).data("kendoDropDownList").text() !== "Select Placement Position") {
                        newAchievementItem.Position = $($(this).find(".regi-achievement-position")[1]).data("kendoDropDownList").text();
                    }


                    if ($($(this).find(".regi-achievement-gameType")[1]).data("kendoDropDownList").text() !== "Select Game Type") {
                        newAchievementItem.Type = $($(this).find(".regi-achievement-gameType")[1]).data("kendoDropDownList").text();
                    }


                    userAchievementList.push(newAchievementItem);
                });


                userObj.UserAchievements = userAchievementList;
            }


            //include remove achievements array
            userObj.RemoveAchievements = removedAchievements;



            if ($("#regi-teamhistory-container").find(".teamhistory-item").length > 0) {

                var userTeamHistoryList = [];
                $("#regi-teamhistory-container").find(".teamhistory-item").each(function (index, item) {

                    var newTeamHistory = new GoTSkillzEntities.UserTeamHistoryDTO();
                    var dataId = this.getAttribute("data-id");

                    if (dataId !== null) {
                        newTeamHistory.Id = dataId;
                    } else {
                        newTeamHistory.Id = 0;
                    }

                    newTeamHistory.TeamName = $(this).find(".regi-teamhistoryname").val();
                    newTeamHistory.FromDate = $(this).find(".regi-teamhistoryfromdate").val();
                    newTeamHistory.ToDate = $(this).find(".regi-teamhistorytodate").val();

                    userTeamHistoryList.push(newTeamHistory);
                });


                userObj.UserTeamHistory = userTeamHistoryList;
            }


            //include remove teamhistory array
            userObj.RemoveTeamHistory = removedTeamHistory;

            $.ajax({
                url: membershipAPI + "SaveUserProfileData",
                type: "POST",
                dataType: "json",
                //global: false,
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(userObj),
                success: function (data) {
                    if (data === "success") {
                        $("#profile-image-upload").data("kendoUpload").upload();
                        GoTSkillZHeaderFunctions.getProfileHeaderMetaData();
                        GoTSkillZProfileMetaDataFunctions.getUserProfileMetaData();
                    }


                },
                complete: function () {
                    GoTSkillZNotificationControls.ShowNotification("Profile Data Saved Successful!", "success");
                },
                error: function (data) {
                    GoTSkillZNotificationControls.ShowNotification("Could Not Save Profile Data, Please Contact Admin", "danger");
                }
            });
        } else {
            GoTSkillZNotificationControls.ShowNotification("Could Not Save Profile Data, Please Contact Admin", "danger");
        }

    }
};

var GoTSkillZRegistrationFormValidations = {
    validatePersonalInfo: function () {
        "use strict";
        var fname = $("#regi-firstname").parsley();
        var lname = $("#regi-lastname").parsley();

        if (fname.isValid() && lname.isValid()) {
            return true;
        } else {
            fname.validate();
            lname.validate();
        }
    },
    validateAchievementForm: function () {
        "use strict";

        var validated = true;

        // validate acheivement name
        if ($(".regi-achievement-name").length !== undefined) {

            if ($(".regi-achievement-name").length === 1) {

                if ($(".regi-achievement-name")[0].value === "") {
                    validated = false;
                }
            }

            if ($(".regi-achievement-name").length > 1) {
                $(".regi-achievement-name").each(function () {
                    if (this.value === "") {
                        validated = false;
                        return false;
                    }
                });
            }
        }


        //validate achievement description
        if ($(".regi-achievement-description").length !== undefined) {

            if ($(".regi-achievement-description").length === 1) {

                if ($(".regi-achievement-description")[0].value === "") {
                    validated = false;
                }
            }

            if ($(".regi-achievement-description").length > 1) {
                $(".regi-achievement-description").each(function () {
                    if (this.value === "") {
                        validated = false;
                        return false;
                    }
                });
            }
        }

        //validate achievement location
        if ($(".regi-achievement-location").length !== undefined) {

            if ($(".regi-achievement-location").length === 1) {

                if ($(".regi-achievement-location")[0].value === "") {
                    validated = false;
                }
            }

            if ($(".regi-achievement-location").length > 1) {
                $(".regi-achievement-location").each(function () {
                    if (this.value === "") {
                        validated = false;
                        return false;
                    }
                });
            }
        }

        //validate achievement date
        if ($(".regi-achievement-date").length !== undefined) {

            if ($(".regi-achievement-date").length > 1) {
                $(".regi-achievement-date").each(function () {
                    if (this.value === "") {
                        validated = false;
                        return false;
                    }
                });
            }
        }

        //validate position
        if ($(".regi-achievement-position").length !== undefined) {

            if ($(".regi-achievement-position").length > 1) {
                $(".regi-achievement-position").each(function () {
                    if ($(this).find("input").data("kendoDropDownList") !== undefined) {
                        if ($(this).find("input").data("kendoDropDownList").text() === "Select Placement Position") {
                            validated = false;
                            return false;
                        }
                    }
                });
            }
        }

        //validate game type
        if ($(".regi-achievement-gameType").length !== undefined) {



            if ($(".regi-achievement-gameType").length > 1) {
                $(".regi-achievement-gameType").each(function () {
                    if ($(this).find("input").data("kendoDropDownList") !== undefined) {
                        if ($(this).find("input").data("kendoDropDownList").text() === "Select Game Type") {
                            validated = false;
                            return false;
                        }
                    }
                });
            }
        }

        return validated;
    },
    validateSocialLinksForm: function () {
        "use strict";

        var validated = { linkType: null, isValid: true };

        //validate youtube url
        var youTubeUrl = $("#regi-youtube").val();
        if (youTubeUrl !== "") {

            if (!GoTSkillZCommonUtilityFunctions.validateUrl(youTubeUrl)) {
                validated.linkType = "YouTube";
                validated.isValid = false;
                return validated;
            }
        }

        //validate Twitch url
        var twitchUrl = $("#regi-twitch").val();
        if (twitchUrl !== "") {

            if (!GoTSkillZCommonUtilityFunctions.validateUrl(twitchUrl)) {
                validated.linkType = "Twitch";
                validated.isValid = false;
                return validated;
            }
        }

        //validate Faceit url
        var faceitUrl = $("#regi-faceit").val();
        if (faceitUrl !== "") {

            if (!GoTSkillZCommonUtilityFunctions.validateUrl(faceitUrl)) {
                validated.linkType = "Faceit";
                validated.isValid = false;
                return validated;
            }
        }

        //validate Steam url
        var steamUrl = $("#regi-steam").val();
        if (steamUrl !== "") {

            if (!GoTSkillZCommonUtilityFunctions.validateUrl(steamUrl)) {
                validated.linkType = "Steam";
                validated.isValid = false;
                return validated;
            }
        }

        //validate SoStronk url
        var soStronkUrl = $("#regi-sostronk").val();
        if (soStronkUrl !== "") {

            if (!GoTSkillZCommonUtilityFunctions.validateUrl(soStronkUrl)) {
                validated.linkType = "SoStronk";
                validated.isValid = false;
                return validated;
            }
        }

        //validate Instagram url
        var instagramUrl = $("#regi-instagram").val();
        if (instagramUrl !== "") {

            if (!GoTSkillZCommonUtilityFunctions.validateUrl(instagramUrl)) {
                validated.linkType = "Instagram";
                validated.isValid = false;
                return validated;
            }
        }

        //validate Facebook url
        var facebookUrl = $("#regi-facebook").val();
        if (facebookUrl !== "") {

            if (!GoTSkillZCommonUtilityFunctions.validateUrl(facebookUrl)) {
                validated.linkType = "Facebook";
                validated.isValid = false;
                return validated;
            }
        }

        //validate Twitter url
        var twitterUrl = $("#regi-twitter").val();
        if (twitterUrl !== "") {

            if (!GoTSkillZCommonUtilityFunctions.validateUrl(twitterUrl)) {
                validated.linkType = "Twitter";
                validated.isValid = false;
                return validated;
            }
        }

        //validate Mixer url
        var mixerUrl = $("#regi-mixer").val();
        if (mixerUrl !== "") {

            if (!GoTSkillZCommonUtilityFunctions.validateUrl(mixerUrl)) {
                validated.linkType = "Mixer";
                validated.isValid = false;
                return validated;
            }
        }

        //validate Discord url
        var discordUrl = $("#regi-discord").val();
        if (discordUrl !== "") {

            if (!GoTSkillZCommonUtilityFunctions.validateUrl(discordUrl)) {
                validated.linkType = "Discord";
                validated.isValid = false;
                return validated;
            }
        }


        return validated;
    }

};

var GoTSkillZProfileInitializers = {
    initializeProfileFunctions: function () {
        "use strict";
        GoTSkillZProfileMetaDataFunctions.getUserProfileMetaData();
        GoTSkillZProfileBinders.bindEditButtons();
        GoTSkillZGameFunctions.getGameTypes();
        GoTSkillZGameFunctions.getAllGameRoles();
        GoTSkillZProfileMetaDataFunctions.getUserOccupations();
    },
    initializeCountryDDL: function () {
        "use strict";

        $("#regi-country-ddl").kendoAutoComplete({
            dataValueField: "CountryId",
            dataTextField: "CountryName",
            template: '<span data-recordid="#= CountryId #"> #= CountryName #</span>',
            filter: "contains",
            minLength: 3,
            placeholder: "Search Country, Eg: Ind...",
            filtering: function (e) {
                if ($("#regi-country-ddl").val().trim() === "") {
                    e.preventDefault();
                    e.sender.dataSource.data([]);
                }
            },
            dataSource: {
                type: "json",
                serverFiltering: true,
                transport: {
                    read: function (options) {
                        $.ajax({
                            url: membershipAPI + "GetCountry/" + $("#regi-country-ddl").val(),
                            dataType: "json",
                            global: false,
                            success: function (result) {
                                // notify the data source that the request succeeded
                                options.success(result);
                            },
                            error: function (result) {
                                // notify the data source that the request failed
                                GoTSkillZNotificationControls.ShowNotification("Uable To Get Country Data, Please Contact System Admin", "danger");
                            }
                        });
                    }
                }

            }
        });

        GoTSkillZProfileInitializers.initializeStateDDL();
    },
    initializeStateDDL: function () {
        "use strict";
        var countryId = "0";


        $("#regi-state-ddl").kendoAutoComplete({
            dataValueField: "StateId",
            dataTextField: "StateName",
            template: '<span data-recordid="#= StateId #"> #= StateName #</span>',
            filter: "contains",
            minLength: 3,
            placeholder: "Search State, Eg: Kar...",
            filtering: function (e) {

                var countryDDL = $("#regi-country-ddl").data("kendoAutoComplete").dataItem();

                if (countryDDL !== undefined) {
                    countryId = $("#regi-country-ddl").data("kendoAutoComplete").dataItem().CountryId;
                } else {
                    GoTSkillZNotificationControls.ShowNotification("Please Select Country", "warning");
                    e.preventDefault();
                    e.sender.dataSource.data([]);
                    return false;
                }


                if ($("#regi-state-ddl").val().trim() === "") {
                    e.preventDefault();
                    e.sender.dataSource.data([]);
                }
            },
            dataSource: {
                type: "json",
                serverFiltering: true,
                transport: {
                    read: function (options) {
                        $.ajax({
                            url: membershipAPI + "GetState/" + $("#regi-state-ddl").val() + "/" + countryId,
                            dataType: "json",
                            global: false,
                            success: function (result) {
                                // notify the data source that the request succeeded
                                options.success(result);
                            },
                            error: function (result) {
                                // notify the data source that the request failed
                                GoTSkillZNotificationControls.ShowNotification("Uable To Get Country Data, Please Contact System Admin", "danger");
                            }
                        });
                    }
                }

            }
        });

        GoTSkillZProfileInitializers.initializeCityDDL();
    },
    initializeCityDDL: function () {
        "use strict";
        var countryId = "0";
        var stateId = "0";


        $("#regi-city-ddl").kendoAutoComplete({
            dataValueField: "CityId",
            dataTextField: "CityName",
            template: '<span data-recordid="#= CityId #"> #= CityName #</span>',
            filter: "contains",
            minLength: 3,
            placeholder: "Search City, Eg: Mang...",
            filtering: function (e) {

                var countryDDL = $("#regi-country-ddl").data("kendoAutoComplete").dataItem();

                if (countryDDL !== undefined) {
                    countryId = $("#regi-country-ddl").data("kendoAutoComplete").dataItem().CountryId;
                } else {
                    GoTSkillZNotificationControls.ShowNotification("Please Select Country", "warning");
                    e.preventDefault();
                    e.sender.dataSource.data([]);
                    return false;
                }

                var stateDDL = $("#regi-state-ddl").data("kendoAutoComplete").dataItem();


                if (stateDDL !== undefined) {
                    stateId = $("#regi-state-ddl").data("kendoAutoComplete").dataItem().StateId;
                } else {
                    GoTSkillZNotificationControls.ShowNotification("Please Select State", "warning");
                    e.preventDefault();
                    e.sender.dataSource.data([]);
                    return false;
                }


                if ($("#regi-city-ddl").val().trim() === "") {
                    e.preventDefault();
                    e.sender.dataSource.data([]);
                }
            },
            dataSource: {
                type: "json",
                serverFiltering: true,
                transport: {
                    read: function (options) {
                        $.ajax({
                            url: membershipAPI + "GetCity/" + $("#regi-city-ddl").val() + "/" + countryId + "/" + stateId,
                            dataType: "json",
                            global: false,
                            success: function (result) {
                                // notify the data source that the request succeeded
                                options.success(result);
                            },
                            error: function (result) {
                                // notify the data source that the request failed
                                GoTSkillZNotificationControls.ShowNotification("Uable To Get Country Data, Please Contact System Admin", "danger");
                            }
                        });
                    }
                }

            }
        });
    },
    initializeGenderDDL: function () {
        "use strict";

        var genderDataSource = [
            {
                'id': -1,
                'text': "Select Gender"
            },
            {
                'id': 1,
                'text': "Male"
            },
            {
                'id': 2,
                'text': "Female"
            },
            {
                'id': 3,
                'text': "Transgender"
            }
        ];

        $("#regi-gender").kendoDropDownList({
            dataTextField: "text",
            dataValueField: "id",
            dataSource: genderDataSource
        });


    },
    initializeMaritalDDL: function () {
        "use strict";

        var maritalDataSource = [
            {
                'id': -1,
                'text': "Select Marital Status"
            },
            {
                'id': 1,
                'text': "Single"
            },
            {
                'id': 2,
                'text': "Married"
            }
        ];

        $("#regi-marital").kendoDropDownList({
            dataTextField: "text",
            dataValueField: "id",
            dataSource: maritalDataSource
        });
    },
    initializeOccupationDDL: function () {
        "use strict";

        var occupationDataSource = _.map(userOccupations, function (value) {
            return { id: value.Id, text: value.Occupation };
        });

        occupationDataSource.unshift = {
            'id': "-1",
            'text': "Select Occupation"
        };
        $("#regi-occupation").kendoDropDownList({
            dataTextField: "text",
            dataValueField: "id",
            dataSource: occupationDataSource
        });


    },
    initializePrimaryGameRoleDDL: function (gameId) {
        "use strict";

        var primaryGameRoleDataSource = [];
        if (gameId !== "" || gameId !== 0) {

            primaryGameRoleDataSource = _.chain(gameRoles).filter(function (x) {
                return x.GameTypeId === parseInt(gameId);
            }).map(function (x) {
                return { id: x.Id, text: x.RoleName };
            }).value();
        }


        primaryGameRoleDataSource.unshift({
            'id': -1,
            'text': "Select Primary Role"
        });


        $("#regi-primaryrole").kendoDropDownList({
            dataTextField: "text",
            dataValueField: "id",
            dataSource: primaryGameRoleDataSource,
            open: function () {

                if ($("#regi-primarygame").data("kendoDropDownList").value() === "-1") {
                    GoTSkillZNotificationControls.ShowNotification("Please Select Primary Game First", "warning");
                }
            }
        });
    },
    initializeSecondaryGameRoleDDL: function (gameId) {
        "use strict";

        var secondaryGameRoleDataSource = [];
        if (gameId !== "" || gameId !== 0) {

            secondaryGameRoleDataSource = _.chain(gameRoles).filter(function (x) {
                return x.GameTypeId === parseInt(gameId);
            }).map(function (x) {
                return { id: x.Id, text: x.RoleName };
            }).value();
        }


        secondaryGameRoleDataSource.unshift({
            'id': -1,
            'text': "Select Secondary Role"
        });


        $("#regi-secondaryrole").kendoDropDownList({
            dataTextField: "text",
            dataValueField: "id",
            dataSource: secondaryGameRoleDataSource,
            open: function () {

                if ($("#regi-secondarygame").data("kendoDropDownList").value() === "-1") {
                    GoTSkillZNotificationControls.ShowNotification("Please Select Secondary Game First", "warning");
                }
            }
        });

    },
    initializePrimaryGameTypeDDL: function () {
        "use strict";

        var gameTypeDataSource = _.map(gameTypes, function (value) {
            return { id: value.Id, text: value.GameName };
        });

        gameTypeDataSource.unshift({
            'id': -1,
            'text': "Select Primary Game"
        });


        $("#regi-primarygame").kendoDropDownList({
            dataTextField: "text",
            dataValueField: "id",
            dataSource: gameTypeDataSource,
            select: function (e) {

                GoTSkillZProfileInitializers.initializePrimaryGameRoleDDL(e.dataItem.id);

            }
        });

        GoTSkillZProfileInitializers.initializePrimaryGameRoleDDL("");
    },
    initializeSecondaryGameTypeDDL: function () {
        "use strict";

        var gameTypeDataSource = _.map(gameTypes, function (value) {
            return { id: value.Id, text: value.GameName };
        });

        gameTypeDataSource.unshift({
            'id': -1,
            'text': "Select Secondary Game"
        });


        $("#regi-secondarygame").kendoDropDownList({
            dataTextField: "text",
            dataValueField: "id",
            dataSource: gameTypeDataSource,
            select: function (e) {
                GoTSkillZProfileInitializers.initializeSecondaryGameRoleDDL(e.dataItem.id);
            }
        });


        GoTSkillZProfileInitializers.initializeSecondaryGameRoleDDL("");
    },
    initializeSummerNote: function () {
        "use strict";
        $("#regi-aboutme").summernote({
            dialogsInBody: true,
            dialogsFade: true,
            disableDragAndDrop: true,
            tabDisable: true,
            placeholder: "Let People know you..",
            toolbar: [
                // [groupName, [list of button]]
                ["style", ["style"]],
                ["font", ["bold", "underline", "clear"]],
                ["fontname", ["fontname"]],
                ["color", ["color"]],
                ["para", ["ul", "ol", "paragraph"]],
                ["view", ["fullscreen", "codeview", "help"]]
            ],
            popover: {
                image: [],
                link: [],
                air: []
            }
        });
    },
    initializePlayerStatusDDL: function () {
        "use strict";

        var playerStatusDataSource = [];

        playerStatusDataSource.push({
            'id': -1,
            'text': "Select Status"
        },
            {
                'id': 0,
                'text': "Benched"
            },
            {
                'id': 1,
                'text': "Active - In Team"
            },
            {
                'id': 2,
                'text': "InActive"
            },
            {
                'id': 3,
                'text': "Looking For Team"
            }
        );


        $("#regi-playerstatus").kendoDropDownList({
            dataTextField: "text",
            dataValueField: "id",
            dataSource: playerStatusDataSource,
            change: function () {

                if ($("#regi-playerstatus").data("kendoDropDownList").value() === "1") {
                    $("#regi-currentteam").prop("readonly", false);
                    $("#regi-currentteam").attr("data-original-title", "Team You Are Currently Playing In");
                } else {
                    $("#regi-currentteam").prop("readonly", true);
                    $("#regi-currentteam").attr("data-original-title", "Please Select Status To Active");
                    $("#regi-currentteam").val("");
                }
            }
        });
    },
    initializePlayerGameExpDDL: function () {
        "use strict";

        var playerGameExpDataSource = [];

        playerGameExpDataSource.push({
            'id': -1,
            'text': "Select Experience"
        },
            {
                'id': 0,
                'text': "1 Month"
            },
            {
                'id': 1,
                'text': "3 Months"
            },
            {
                'id': 2,
                'text': "6 Months"
            },
            {
                'id': 3,
                'text': "12 Months"
            },
            {
                'id': 4,
                'text': "2+ Years"
            },
            {
                'id': 5,
                'text': "3+ Years"
            },
            {
                'id': 6,
                'text': "5+ Years"
            },
            {
                'id': 7,
                'text': "10+ Years"
            },
            {
                'id': 8,
                'text': "15+ Years"
            },
            {
                'id': 9,
                'text': "20+ Years"
            },
            {
                'id': 10,
                'text': "25+ Years"
            },
            {
                'id': 11,
                'text': "30+ Years"
            }
        );


        $("#regi-primarygameexp, #regi-secondarygameexp").kendoDropDownList({
            dataTextField: "text",
            dataValueField: "id",
            dataSource: playerGameExpDataSource
        });
    },
    initializeTeamHistoryYearPicker: function () {
        "use strict";

        $('.regi-teamhistoryfromdate, .regi-teamhistorytodate').yearpicker({
            startYear: 1950
        });
    },
    getUserProfileImage: function () {
        "use strict";
        var initialFile = [];
        $.ajax({
            url: fileAPI + "GetUserProfileImage/" + userObj.UserId,
            type: "GET",
            dataType: "json",
            //global: false,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data) {
                    initialFile = data;
                }
            },
            complete: function () {
                // kendo file upload
                if ($("#profile-image-upload").data("kendoUpload") === undefined) {

                    $('#user-profile-img-container').append(' <input type="file" id="profile-image-upload" aria-label="files" />');
                    $("#profile-image-upload").kendoUpload({
                        "multiple": true,
                        async: {
                            autoUpload: false,
                            saveUrl: fileAPI + "UploadProfileImage",
                            removeUrl: fileAPI + "RemoveProfileImage"
                        },
                        upload: function (e) {
                            e.data = {
                                userId: userObj.UserId
                            };
                        },
                        remove: function (e) {
                            e.data = {
                                userId: userObj.UserId
                            };
                        },
                        validation: {
                            allowedExtensions: [".jpg", ".png", ".jpeg"]
                        },
                        files: initialFile
                    });
                } else {

                    var uploadWidget = $("#profile-image-upload").getKendoUpload();
                    // You won't need to clear the files as the Upload DOM is entirely removed
                    // uploadWidget.clearAllFiles();
                    var uploaderOptions = uploadWidget.options;
                    uploaderOptions.files = [];

                    uploadWidget.destroy();

                    // Get reference to the 'files' <input> element and its .k-upload parent
                    var uploadInput = $("#user-profile-img-container");
                    var wrapper = uploadInput.parents('.k-upload');
                    // Remove the .k-upload from the DOM
                    wrapper.remove();
                    // Re-append the 'files' <input> to the DOM

                    $('#user-profile-img-container').append(uploadInput);
                    $("#profile-image-upload").kendoUpload({
                        "multiple": true,
                        async: {
                            autoUpload: false,
                            saveUrl: fileAPI + "UploadProfileImage",
                            removeUrl: fileAPI + "RemoveProfileImage"
                        },
                        upload: function (e) {
                            e.data = {
                                userId: userObj.UserId
                            };
                        },
                        remove: function (e) {
                            e.data = {
                                userId: userObj.UserId
                            };
                        },
                        validation: {
                            allowedExtensions: [".jpg", ".png", ".jpeg"]
                        },
                        files: initialFile
                    });

                }

                $($("#profile-image-upload").closest(".k-upload").find("span")[0]).text("Upload Profile Picture");
            },
            error: function (data) {
                GoTSkillZNotificationControls.ShowNotification("Could Not Get User Profile Image Data, Please Contact Admin", "danger");
            }
        });

    }
};

var GoTSkillZProfileBinders = {
    bindEditButtons: function () {
        "use strict";

        $("#edit-myinfo").unbind("click").bind("click", function (e) {
            e.preventDefault();
            e.stopImmediatePropagation();
            GoTSkillZProfileRegistrationFunctions.showUserEditModal();

            $("#user-profile-wizard-t-0").get(0).click();
        });


        $("#edit-aboutme").unbind("click").bind("click", function (e) {

            e.preventDefault();
            e.stopImmediatePropagation();

            GoTSkillZProfileRegistrationFunctions.showUserEditModal();

            $("#user-profile-wizard-t-1").get(0).click();
        });


        $("#edit-playerProfile").unbind("click").bind("click", function (e) {

            e.preventDefault();
            e.stopImmediatePropagation();

            GoTSkillZProfileRegistrationFunctions.showUserEditModal();

            $("#user-profile-wizard-t-2").get(0).click();
        });

        $("#edit-teamhistory").unbind("click").bind("click", function (e) {

            e.preventDefault();
            e.stopImmediatePropagation();

            GoTSkillZProfileRegistrationFunctions.showUserEditModal();

            $("#user-profile-wizard-t-3").get(0).click();
        });

        $("#edit-achievements").unbind("click").bind("click", function (e) {

            e.preventDefault();
            e.stopImmediatePropagation();

            GoTSkillZProfileRegistrationFunctions.showUserEditModal();

            $("#user-profile-wizard-t-4").get(0).click();
        });


    },
    bindAchievementAddBtn: function () {
        "use strict";

        $("#add-achievement").unbind("click").bind("click", function (e) {

            e.preventDefault();
            e.stopImmediatePropagation();

            GoTSkillZProfileAchievementsFunctions.buildUserAchievementForm(false, true, null);
        });
    },
    bindTeamHistoryAddBtn: function () {
        "use strict";

        $("#add-teamhistory").unbind("click").bind("click", function (e) {

            e.preventDefault();
            e.stopImmediatePropagation();

            GoTSkillZProfileTeamHistoryFunctions.buildUserTeamHistoryForm(false, true, null);
        });
    },
    bindAchievementRemoveBtn: function () {
        "use strict";

        $(".remove-achivement").unbind("click").bind("click", function (e) {

            e.preventDefault();
            e.stopImmediatePropagation();

            GoTSkillZProfileAchievementsFunctions.removeUserAchievementFormDiv(e);
        });

    },
    bindTeamHistoryRemoveBtn: function () {
        "use strict";

        $(".remove-teamhistory").unbind("click").bind("click", function (e) {

            e.preventDefault();
            e.stopImmediatePropagation();

            GoTSkillZProfileTeamHistoryFunctions.removeUserTeamHistoryFormDiv(e);
        });

    }
};


var GoTSkillZProfileTabFunctions = {
    onTabChange: function () {
        "use strict";
       

        $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
            var activeTab = $(e.target).attr("href");

            if (activeTab === "#player-profile" || activeTab === "#setup" || activeTab === "#csgo-config") {
                switch (activeTab) {
                    case "#player-profile":
                        $("#player-profile").show();
                        $("#setup,#csgo-config").hide();
                        break;
                    case "#setup":
                        $("#setup").show();
                        $("#player-profile,#csgo-config").hide();
                        break;
                    case "#csgo-config":
                        $("#csgo-config").show();
                        $("#player-profile,#setup").hide();

                        $('.CodeMirror').each(function (i, el) {
                            el.CodeMirror.refresh();
                        });
                        break;
                }
            }


        });

    }
};

$(window).on("load", function () {
    "use strict";
    $("#profile-header").parent().removeClass("container-fluid");

    if ($("#achievement-container").length > 0) {
        var perfectScrollbar = new PerfectScrollbar("#achievement-container", {
            wheelPropagation: true,
            minScrollbarLength: 20
        });
    }


    GoTSkillZGateKeeperFunctions.checkUserHasAccess(GoTSkillZProfileInitializers.initializeProfileFunctions);
    GoTSkillZProfileTabFunctions.onTabChange();

});