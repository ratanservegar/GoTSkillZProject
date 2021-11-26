/*jslint browser: true, nomen: true*/

var googleAPI = "/WCF/GoogleAPI.svc/";
var googleUser = {}, auth2, checkLoggedInCount = 0;

var GoTSkillZGoogleAuthSignInFunctions = {
    onAppStart: function () {

        var scopes =
            "https://www.googleapis.com/auth/youtube.readonly https://www.googleapis.com/auth/youtube.channel-memberships.creator";
        window.gapi.load("client:auth2", function () {
            // Retrieve the singleton for the GoogleAuth library and set up the client.
            auth2 = window.gapi.auth2.init({
                client_id: "",
                cookiepolicy: "single_host_origin",
                // Request scopes in addition to 'profile' and 'email'
                scope: scopes,
                discoveryDocs: ["https://www.googleapis.com/discovery/v1/apis/youtube/v3/rest"]
            });


            GoTSkillZGoogleAuthSignInFunctions.attachSignin(document.getElementById("googleLoginBtn"));


            if (navigator.cookieEnabled === false) {
                $("#enable-cookie-msg").css("display", "block");

            } else {
                $("#enable-cookie-msg").css("display", "none");
            }
            
            setTimeout(function () { GoTSkillZGoogleUserValidationFunctions.checkIfUserIsAlreadyLoggedIn(); }, 1500);
        });
    },
    attachSignin: function (element) {


        auth2.attachClickHandler(element, {},
            function (googleUser) { //onSuccess
                var profile = googleUser.getBasicProfile();

                // ReSharper disable once PossiblyUnassignedProperty
                var accessToken = googleUser.getAuthResponse().id_token;
                var oAuthAccessToken = googleUser.getAuthResponse().access_token;;



                GoTSkillZGoogleAuthSignInFunctions.authorizeUser(profile, accessToken, oAuthAccessToken);


            }, function () {
                GoTSkillNavigationFunctions.hideMenuItems();
            });




    },
    authorizeUser: function (userData, accessToken, oAuthAccessToken) {
        "use strict";

        $("#sign-in-modal").modal("hide");

        $('#sign-in-modal').on('hidden.bs.modal',
            function () {
                // Load up a new modal...
                $('#signing-in-modal').modal('show');
            });


        if (userData !== undefined && userData !== null && accessToken !== "") {
            var authReq = new GoTSkillZSTO.GoogleSTO();
            authReq.GoogleUserId = userData.getId();
            authReq.GoogleUserEmail = userData.getEmail();
            authReq.GoogleAccessToken = accessToken;
            authReq.GoogleFirstName = userData.getGivenName();
            authReq.GoogleLastName = userData.getFamilyName();
            authReq.GoogleOAuthAccessToken = oAuthAccessToken;

            $.ajax({
                url: googleAPI + "AuthorizeUser",
                type: "POST",
                dataType: "json",
                contentType: "application/json;",
                data: JSON.stringify(authReq),
                success: function (data) {
                    if (data.Success) {


                        window.location.reload();

                    } else {
                        GoTSkillZGateKeeperFunctions.handleAccessControl(data.StateCode);
                    }


                },
                error: function () {
                    GoTSkillNavigationFunctions.hideMenuItems();
                }
            });
        }

    },
    attachSignOut: function () {
        auth2.signOut().then(function () {
            GoTSkillZGoogleAuthSignOutFunctions.deAuthorizeUser();
            GoTSkillNavigationFunctions.hideMenuItems();
        });
    }
};





var GoTSkillZGoogleAuthSignOutFunctions = {
    deAuthorizeUser: function () {
        "use strict";
        $.ajax({
            url: googleAPI + "DeAuthorize",
            type: "GET",
            dataType: "json",
            contentType: "application/json;",
            complete: function () {

                GoTSkillZGateKeeperFunctions.redirectToHomePage();
            }
        });
    }
};


var GoTSkillZGoogleUserValidationFunctions = {
    checkIfUserIsAlreadyLoggedIn: function () {
        "use strict";


        $.ajax({
            url: googleAPI + "AuthenticateUser",
            type: "GET",
            dataType: "json",
            contentType: "application/json;",
            success: function (data) {
                if (data.StateCode === 5) {
                    GoTSkillZYouTubeFunctions.loadYoutubeApi();
                    GoTSkillNavigationFunctions.showMenuItems();
                } else {
                    GoTSkillNavigationFunctions.hideMenuItems();
                }
            }
        });

    }
};


var GoTSkillZGoogleBinders = {
    bindLoginBtn: function () {
        "use strict";

        $("#header-login-btn").unbind("click").bind("click", function () {
            $("#sign-in-modal").modal("show");
        });
    },
    bindLogoutBtn: function () {
        $("#sign-out-btn").unbind("click").bind("click", function () {
            GoTSkillZGoogleAuthSignInFunctions.attachSignOut();
        });
    }
};


$(window).on("load", function () {
    "use strict";


    // keep this and recheck in feb 2020
    document.cookie = 'same-site-cookie=foo; SameSite=Lax';
    document.cookie = 'cross-site-cookie=bar; SameSite=None; Secure';

    GoTSkillZInitializeCommonControls.initalizeAjaxLoader();

    GoTSkillZGoogleAuthSignInFunctions.onAppStart();
    GoTSkillZGoogleBinders.bindLoginBtn();
    GoTSkillZGoogleBinders.bindLogoutBtn();
    GoTSkillZNavigationBuilder.InitializeMenu();


});
