/*jslint browser: true, nomen: true*/

var pmsAPI = "/WCF/PMSAPI.svc/";


var GoTSkillZInitializeCommonControls = {
    initializeButtonTogglers: function () {
        "use strict";
        // Toggles
        $(".br-toggle").on("click", function (e) {
            e.preventDefault();
            $(this).toggleClass("on");
        });
    },
    initalizeAjaxLoader: function () {
        "use strict";
        //Modal Load event
        $(document).ajaxStart(function () {
            "use strict";

            GoTSkillZUIFunctions.getLoadingMessages();

        }).ajaxComplete(function () {
            "use strict";
            GoTSkillZUIFunctions.HideLoader();
        });
    },
    initializeColoredToolTip: function () {
        "use strict";

        // colored tooltip
        $('[data-toggle="tooltip-primary"]').tooltip({
            template: '<div class="tooltip tooltip-primary" role="tooltip"><div class="arrow"></div><div class="tooltip-inner"></div></div>'
        });

        $('[data-toggle="tooltip-success"]').tooltip({
            template: '<div class="tooltip tooltip-success" role="tooltip"><div class="arrow"></div><div class="tooltip-inner"></div></div>'
        });

        $('[data-toggle="tooltip-warning"]').tooltip({
            template: '<div class="tooltip tooltip-warning" role="tooltip"><div class="arrow"></div><div class="tooltip-inner"></div></div>'
        });

        $('[data-toggle="tooltip-danger"]').tooltip({
            template: '<div class="tooltip tooltip-danger" role="tooltip"><div class="arrow"></div><div class="tooltip-inner"></div></div>'
        });

        $('[data-toggle="tooltip-info"]').tooltip({
            template: '<div class="tooltip tooltip-info" role="tooltip"><div class="arrow"></div><div class="tooltip-inner"></div></div>'
        });

        $('[data-toggle="tooltip-indigo"]').tooltip({
            template: '<div class="tooltip tooltip-indigo" role="tooltip"><div class="arrow"></div><div class="tooltip-inner"></div></div>'
        });

        $('[data-toggle="tooltip-purple"]').tooltip({
            template: '<div class="tooltip tooltip-purple" role="tooltip"><div class="arrow"></div><div class="tooltip-inner"></div></div>'
        });

        $('[data-toggle="tooltip-teal"]').tooltip({
            template: '<div class="tooltip tooltip-teal" role="tooltip"><div class="arrow"></div><div class="tooltip-inner"></div></div>'
        });

        $('[data-toggle="tooltip-orange"]').tooltip({
            template: '<div class="tooltip tooltip-orange" role="tooltip"><div class="arrow"></div><div class="tooltip-inner"></div></div>'
        });

        $('[data-toggle="tooltip-pink"]').tooltip({
            template: '<div class="tooltip tooltip-pink" role="tooltip"><div class="arrow"></div><div class="tooltip-inner"></div></div>'
        });


    }
};

var loadingTexts = [];

var GoTSkillZNotificationControls = {
    ShowNotification: function (message, type) {
        "use strict";

        var hulla = new hullabaloo({
            ele: "body",
            offset: {
                from: "top",
                amount: 20
            },
            align: "right",
            delay: 5000,
            allow_dismiss: true,
            stackup_spacing: 10,
            icon: {
                success: "fa fa-check-circle",
                info: "fa fa-info-circle",
                warning: "fa fa-life-ring",
                danger: "fa fa-exclamation-circle",
                light: "fa fa-sun",
                dark: "fa fa-moon"
            }
        });
        hulla.send(message, type);

    }
};

var GoTSkillZUIFunctions = {
    getLoadingMessages: function () {



        GoTSkillZUIFunctions.ShowLoader(loadingMessages);

    },
    ShowLoader: function (customMsg) {
        "use strict";

        var randomQuote = customMsg[Math.floor(Math.random() * customMsg.length)];

        if (randomQuote !== undefined && randomQuote !== null) {
            $("#loading-quote").html(randomQuote.TagLine);
            $("#loading-sub").html(randomQuote.Author);
        } else {
            $("#loading-quote").html("Do You Have What It Takes?");
            $("#loading-sub").html("");
        }

        $("#loading-modal").modal("show");
    },
    HideLoader: function () {
        "use strict";
        setTimeout(function () {
            $("#loading-modal").modal("hide");
        }, 4000);
    },
    HideModal: function () {
        "use strict";
        $("#large-modal, #small-modal").modal("hide");
    },
    ShowModal: function (modalType, modalProperties) {
        "use strict";
        var container = "";
        if (modalType.toLowerCase() === "large") {
            container = $("#large-modal");
        } else {
            container = $("#small-modal");
        }

        $(container).find(".modal-title").html(modalProperties.title);
        $(container).find(".modal-body").html(modalProperties.body);

        $(container).modal("show");
    },
    ModalProperties: function () {
        "use strict";
        this.title = "";
        this.body = "";
    },
    revokeAccess: function () {
        "use strict";


        $("#revoke-modal").modal("show");
        $("#revoke-modal-submit-btn").unbind("click").bind("click",
            function () {
                window.open('https://myaccount.google.com/permissions?pli=1', '_blank');
                GoTSkillZGoogleAuthSignInFunctions.attachSignOut();
                location.reload();
            });
    }
};






var GoTSkillZSiteWideLHSMenuFunctions = {
    hideLHSMenu: function () {
        "use strict";

        $(window).resize(function () {
            minimizeMenu();
        });

        minimizeMenu();

        function minimizeMenu() {

            $('.menu-item-label,.menu-item-arrow').addClass('op-lg-0-force d-lg-none');
            $('body').addClass('collapsed-menu');
            $('.show-sub + .br-menu-sub').slideUp();
        }

    }
}

$(window).on("load", function () {
    "use strict";
    GoTSkillZInitializeCommonControls.initalizeAjaxLoader();
    GoTSkillZSiteWideLHSMenuFunctions.hideLHSMenu();
    GoTSkillZInitializeCommonControls.initializeButtonTogglers();

    GoTSkillZInitializeCommonControls.initializeColoredToolTip();

    $("#revoke-btn").bind("click").bind("click",
        function () {
            GoTSkillZUIFunctions.revokeAccess();
        });
});