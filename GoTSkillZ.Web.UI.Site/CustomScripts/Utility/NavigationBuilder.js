/*jslint browser: true, nomen: true*/

var pmsAPI = "/WCF/PMSAPI.svc/";
var membershipAPI = "/WCF/MembershipAPI.svc/";

var GoTSkillZNavTemplates = {
    getMenuItemTemplate: function(hrefValue, textValue, icon, pageId) {
        "use strict";


        var isActive = "";

        if (textValue.toLowerCase() === "home")
            isActive = "active";

        if (hrefValue === "")
            hrefValue = "#";

        var menuItemTemplate = $('<li class="br-menu-item"><a href="' + hrefValue + '" class="br-menu-link ' + isActive + '" page-id="' + pageId + '" ><i class="menu-item-icon icon ' + icon + ' tx-24" style="padding-right:20px;"/><span class="menu-item-label">' + textValue + "</span></a></li>");


        return menuItemTemplate;

    },
    getMenuItemWithSubLinkTemplate: function(textValue, subLinks, icon) {
        "use strict";
        var menuItemSubLink = $('<li class="br-menu-item"> <a href="#" class="br-menu-link with-sub"> <i class="menu-item-icon icon ' + icon + ' tx-20"></i> <span class="menu-item-label">' + textValue + "</span> </a> </li>");


        var subMenu = $('<ul class="br-menu-sub">');

        if (subLinks !== undefined) {
            if (subLinks !== null && subLinks.length > 0) {

                $.each(subLinks, function(index, value) {
                    subMenu.append('<li class="sub-item"><a href="' + value.Url + '" class="sub-link" page-id="' + value.id + '">' + value.text + "</a></li>");
                });
            }
        }
        menuItemSubLink.append(subMenu);
        return menuItemSubLink;

    }
};

var GoTSkillZNavigationBuilder = {
    InitializeMenu: function() {
        "use strict";
        GoTSkillZNavigationBuilder.GetNavigationData();


    },
    GetNavigationData: function() {
        "use strict";
        $.ajax({
            url: pmsAPI + "GetNavigationForUser",
            type: "GET",
            cache: false,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function(data) {

                GoTSkillZNavigationBuilder.BuildNavigationMenu(data);

                GoTSkillZHeaderFunctions.getProfileHeaderMetaData();

            }
        });
    },
    BuildNavigationMenu: function(navData) {
        "use struct";
        var menuBar = $("#root-menu");
        $(menuBar).html("");

        $.each(navData, function(index, navItem) {


            if (navItem.items.length === 0) {

                var menuItem = GoTSkillZNavTemplates.getMenuItemTemplate(navItem.Url, navItem.text, navItem.icon, navItem.id);
                $(menuBar).append(menuItem);
            } else {
                var subItems = this.items;
                var menuItemWithSubLink = GoTSkillZNavTemplates.getMenuItemWithSubLinkTemplate(navItem.text, subItems, navItem.icon);

                $(menuBar).append(menuItemWithSubLink);
            }

        });

        //SET ACTIVE PAGE MENU ITEM
        GoTSkillNavigationFunctions.setCurrentActivePageLink();


        //get live 
        GoTSkillNavigationFunctions.setLiveStreamMenu();


    }
};


var GoTSkillZHeaderFunctions = {
    getProfileHeaderMetaData() {
        "use strict";

        $.ajax({
            url: membershipAPI + "GetUserProfileHeaderData",
            type: "GET",
            dataType: "json",
            cache: false,
            contentType: "application/json; charset=utf-8",
            success: function(data) {

                if (data.UserProfile !== null) {
                    GoTSkillZHeaderFunctions.setTopProfileHeader(data.UserProfile);

                    if (data.UserProfile.IsAdmin) {
                        GoTSkillNavigationFunctions.showAdminQuickSettingsFlyOutMenu();
                    } else {
                        GoTSkillNavigationFunctions.hideAdminQuickSettingsFlyOutMenu();
                    }
                }
            },
            error: function(data) {
                GoTSkillZNotificationControls.ShowNotification("Could Not Get User Profile Header Data, Please Contact Admin", "danger");
            }
        });
    },
    setTopProfileHeader: function(profileData) {
        "use strict";

        var profileNameHtml, alias = profileData.UserProfileExtension.Alias;

        //reset
        $("#header-profile-image, #menu-profileimage, #logged-fullname-header, #logged-fullname-header, #logged-fullname-menu, #logged-email").html("");

        $("#header-profile-image, #menu-profileimage, #logged-fullname-header").attr("src", profileData.ProfileImage);

        if (alias !== "" && alias !== null) {
            $("#logged-fullname-header, #logged-fullname-menu").html(alias);
        } else {
            $("#logged-fullname-header, #logged-fullname-menu").html(profileData.FirstName + " " + profileData.LastName);
        }


        $("#logged-email").html(profileData.Email);


    }
};
var GoTSkillNavigationFunctions = {
    setCurrentActivePageLink: function() {
        "use strict";


        $("#root-menu").find("li").each(function() {
            if ($(this).find("a").hasClass("active")) {

                var menuName = $(this)[0].innerText;
                var pageName = $("#hdnPageName").val().toLowerCase();

                if (menuName.toLowerCase().indexOf(pageName) > -1) {
                    return;
                } else {

                    $(this).find("a").removeClass("active");

                    $("#root-menu").find("li").each(function(x) {
                        if ($(this).text().toLowerCase() === pageName) {

                            $(this).find("a").addClass("active");

                            $($(this).parent().parent().find("a")[0]).addClass("active show-sub");

                        }
                    });

                }
            }
        });
    },
    setLiveStreamMenu: function() {
        "use strict";

        $("#root-menu").find("li").each(function() {

            var menuName = $(this).find("span").text();
            if (menuName.toLowerCase() === "livestream") {

                if (isLiveStreamOn) {

                    //set root menu icon
                    $($($(this)[0]).find("i")[0]).css("color", "red");

                    //set sub menu
                    $($($(this)[0]).find("a")[1]).css("color", "red");

                } else {
                    //set root menu icon
                    $($($(this)[0]).find("i")[0]).css("color", "");

                    //set sub menu
                    $($($(this)[0]).find("a")[1]).css("color", "");
                }

            }

        });
    },
    hideMenuItems: function() {
        "use strict";
        $("#right-setting-icon, #user-header-profile, #header-notification, #header-message").hide();
        $("#header-login-btn").show();
    },
    showMenuItems: function() {
        "use strict";
        $("#right-setting-icon, #user-header-profile, #header-notification, #header-message").show();
        $("#header-login-btn").hide();
    },
    hideAdminQuickSettingsFlyOutMenu: function() {
        "use strict";
        $("#quick-settings").hide();
    },
    showAdminQuickSettingsFlyOutMenu: function() {
        "use strict";

        $("#quick-settings").show();
    }
};

$(window).on("load", function() {
    "use strict";


});