var giveawayAPI = "/WCF/GiveawayAPI.svc/";

var isAdmin = false, allGiveawayData = [], allUserGiveawayEntries = [];

var GoTSkillZGiveawayAnnouncementFunctions = {
    getAllGiveaways: function () {
        "use strict";
        $.ajax({
            url: giveawayAPI + "GetAllGiveaways",
            type: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                allGiveawayData = data;
                GoTSkillZGiveawayAnnouncementFunctions.buildGiveawayCards(data);
                GoTSkillZGiveawayAdminFunctions.buildExistingGiveawayList();
                GoTSkillZGiveawayAnnouncementFunctions.getAllUserGiveawayEntries();
            },
            error: function () {
                GoTSkillZNotificationControls.ShowNotification("Could Not Get giveaway Data, Please Contact Admin", "danger");
            }
        });
    },
    buildGiveawayCards: function (data) {
        "use strict";

        var container = $("#giveaway-container");

        if (data !== undefined && data !== null && data.length > 0) {
            container.html("");
            $.each(data,
                function (index, value) {


                    var divCol = $("<div>",
                        {
                            "class": "col-sm-4 pd-t-10",
                            "giveaway-div-id": value.Id
                        });

                    var span = "";

                    if (value.Active) {
                        span = $("<span>",
                            {
                                "class": "bg-teal pd-y-3 pd-x-10 tx-white tx-11 tx-roboto"
                            }).html("Entries Open");
                    } else {
                        span = $("<span>",
                            {
                                "class": "bg-danger pd-y-3 pd-x-10 tx-white tx-11 tx-roboto"
                            }).html("Entries Closed");
                    }


                    var divCardImg = $("<div>",
                        {
                            "class": "card bd-0 mg-0"
                        });

                    var figure = $("<figure>",
                        {
                            "class": "card-item-img  rounded-top"
                        });

                    var img = $("<img/>",
                        {
                            "class": "img-fluid rounded-top",
                            "src": value.ImageUrl
                        });

                    var cardBody = $("<div>",
                        {
                            "class": "card-body pd-25 bd bd-t-0 bd-white-1 rounded-bottom"
                        });

                    var cardTitle = $("<p>",
                        {
                            "class": "tx-11 tx-uppercase tx-mont tx-semibold tx-info"
                        }).html(value.Title);

                    var cardDes1 = $("<h5>",
                        {
                            "class": "tx-normal tx-roboto lh-3 mg-b-15"
                        }).html(value.Description);


                    var totalEntries = $("<h6>",
                        {
                            "class": "tx-normal tx-roboto lh-3 mg-t-15"
                        }).html("Total Entries:" + value.TotalEntries);

                    var btn = "";
                    if (value.Active) {
                        btn = $("<button>",
                            {
                                "class": "btn glow-on-hover enter-giveaway-btn",
                                "type": "button",
                                "style": "width: 100%",
                                "giveaway-btn-id": value.Id
                            }).html("ENTER NOW");
                    } else {
                        btn = $("<button>",
                            {
                                "class": "btn btn-danger enter-giveaway-btn",
                                "type": "button",
                                "style": "width: 100%",
                                "giveaway-btn-id": value.Id
                            }).html("SEE WINNER");
                    }
                    
                    divCol.append(span);
                    divCol.append(divCardImg.append(figure.append(img)).append(cardBody.append(cardTitle).append(cardDes1).append(btn).append(totalEntries)));


                    container.append(divCol);
                });

            $(".enter-giveaway-btn").unbind("click").bind("click",
                function (e) {
                    var giveawayId = this.getAttribute("giveaway-btn-id");

                    var currentText = e.currentTarget.innerText;

                    if (currentText.toLowerCase() === "enter now") {
                        GoTSkillZGiveawayAnnouncementFunctions.showEnterGiveawayModal(giveawayId);
                    } else {
                        var win = window.open("/winners", '_blank');
                        win.focus();
                    }
                });
        }


    },
    getAllUserGiveawayEntries() {
        "use strict";
       
        $.ajax({
            url: giveawayAPI + "GetAllUserGiveawayEntries/" + parseInt($("#hdnUserId").val()),
            type: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                allUserGiveawayEntries = data;
            },
            error: function () {
                GoTSkillZNotificationControls.ShowNotification("Could Not Get giveaway Entries Data, Please Contact Admin", "danger");
            }
        });

    },
    showEnterGiveawayModal(giveawayId) {
        "use strict";

        if (giveawayId !== "") {

            var giveawayObj = _.chain(allGiveawayData).filter(function (x) {
                return x.Id === parseInt(giveawayId);
            }).first().value();

            if (giveawayObj !== undefined && giveawayObj !== null && giveawayObj.Active) {

                $("#enter-giveaway-title").html(giveawayObj.Title);
                $("#enter-giveaway-description").html(giveawayObj.Description);
                $("#enter-giveaway-rule").html(giveawayObj.Rules);
                $("#generate-giveawaycode").attr("giveaway-entry-id", giveawayObj.Id);

                $("#enter-giveaway-modal").modal("show");


                $("#generate-giveawaycode").unbind("click").bind("click",
                    function (e) {
                        var giveawayId = this.getAttribute("giveaway-entry-id");
                        GoTSkillZGiveawayAnnouncementFunctions.generateEntryCode(giveawayId);
                    });
            }

            GoTSkillZGiveawayAnnouncementFunctions.showHideCodeButtons(giveawayId, "");

        }
    },
    showHideCodeButtons: function (giveawayId, code) {
        "use strict";

        // check for already entered

        if (code === "") {
            var existingEntry = _.chain(allUserGiveawayEntries).filter(function (x) {
                return x.GiveawayId === parseInt(giveawayId);
            }).first().value();


            if (existingEntry !== undefined && existingEntry !== null) {
                $("#generate-giveawaycode").css("display", "none");
                $("#giveaway-code-container").css("display", "flex");
                $("#giveaway-code-input").val(existingEntry.GiveawayCode);


            } else {
                $("#giveaway-code-container").css('display', "none");
                $("#generate-giveawaycode").css("display", "block");

                $("#generate-giveawaycode").html("Get Code");
            }
        } else {
            $("#generate-giveawaycode").css("display", "none");
            $("#giveaway-code-container").css("display", "flex");
            $("#giveaway-code-input").val(code);
        }

    },
    generateEntryCode: function (giveawayId) {
        "use strict";

        var userId = parseInt($("#hdnUserId").val());
        if (userId !== null && userId !== "" && userId !== "" && userId  !== NaN) {

            $("#generate-giveawaycode").html("").html('<i class="fa fa-spinner fa-spin"></i><span >');

            $.ajax({
                url: giveawayAPI + "GenerateEntryCode/" + giveawayId + "/" + userId,
                type: "GET",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    GoTSkillZGiveawayAnnouncementFunctions.showHideCodeButtons("", data);
                    GoTSkillZGiveawayAnnouncementFunctions.getAllUserGiveawayEntries();
                },
                error: function () {
                    GoTSkillZNotificationControls.ShowNotification("Could Not Get giveaway Entry Code Data, Please Contact Admin", "danger");
                }
            });

        }
    }
}


var GoTSkillZGiveawayAdminFunctions = {
    checkForAdminUser: function () {
        "use strict";
        var userId = parseInt($("#hdnUserId").val());
        if (userId !== null && userId === 1 && userId !== NaN) {
            isAdmin = true;
            $("#edit-giveaway").show();

        } else {
            isAdmin = false;
            $("#edit-giveaway").hide();
        }
    },
    buildExistingGiveawayList: function () {
        "use strict";

        if (allGiveawayData !== undefined && allGiveawayData !== null && allGiveawayData.length > 0) {


            var ddlData = '<option value="">Select one ... </option>';

            $("#giveaway-list").html("");


            ddlData = ddlData + allGiveawayData.map(function (x) {
                return '<option value="' + x.Id + '">' + x.Title + "</option>";
            }).join("");


            $("#giveaway-list").html(ddlData);
        }

        $("#giveaway-list").on("change",
            function () {
                GoTSkillZGiveawayAdminFunctions.onGiveawayTitleChange(this.value);
            });

    },
    bindGiveawayEditModal: function () {
        "use strict";

        $("#edit-giveaway").unbind("click").bind("click",
            function (e) {
                e.preventDefault();
                e.stopImmediatePropagation();
                $("#giveaway-add-modal").modal("show");

                $("#add-giveaway").unbind("click").bind("click",
                    function () {
                        GoTSkillZGiveawayAdminFunctions.clearGiveawayForm();
                    });


                $("#save-giveaway").unbind("click").bind("click",
                    function () {
                        GoTSkillZGiveawayAdminFunctions.saveGiveaway();
                    });
            });
    },
    onGiveawayTitleChange: function (giveawayId) {
        "use strict";

        if (giveawayId !== undefined && giveawayId !== null && giveawayId !== "" && giveawayId !== 0) {

            var giveawayObj = _.chain(allGiveawayData).filter(function (x) {
                return x.Id === parseInt(giveawayId);
            }).first().value();


            if (giveawayObj !== undefined && giveawayObj !== null) {
                GoTSkillZGiveawayAdminFunctions.setGiveawayModalFormValues(giveawayObj);
            } 

            $("#giveaway-rule").summernote({
                dialogsInBody: true,
                dialogsFade: true,
                disableDragAndDrop: true,
                tabDisable: true,
                placeholder: "Giveaway rule",
                popover: {
                    image: [],
                    link: [],
                    air: []
                }

            });
            
         

        }
    },
    setGiveawayModalFormValues: function (giveawayObj) {
        "use strict";

        $("#giveaway-id").val(giveawayObj.Id);
        $("#giveaway-code").val(giveawayObj.Code);
        $("#giveaway-title").val(giveawayObj.Title);
        $("#giveaway-desc").val(giveawayObj.Description);
        $("#giveaway-rule").val(giveawayObj.Rules);
        $("#giveaway-imageurl").val(giveawayObj.ImageUrl);
        $("#giveaway-videourl").val(giveawayObj.VideoUrl);
        $("#giveaway-entries").val(giveawayObj.TotalEntries);
        $("#giveaway-sponsored").prop('checked', giveawayObj.Sponsored);
        $("#giveaway-international").prop('checked', giveawayObj.International);
        $("#giveaway-active").prop('checked', giveawayObj.Active);


      

      


    },
    clearGiveawayForm() {
        "use strict";
        $("#giveaway-id").val("");
        $("#giveaway-code").val("");
        $("#giveaway-title").val("");
        $("#giveaway-desc").val("");
        $("#giveaway-rule").val("");
        $("#giveaway-rule").summernote('reset');
        $("#giveaway-imageurl").val("");
        $("#giveaway-videourl").val("");
        $("#giveaway-entries").val("");
        $("#giveaway-sponsored").prop('checked', false);
        $("#giveaway-international").prop('checked', false);
        $("#giveaway-active").prop('checked', false);
    },
    saveGiveaway: function () {
        "use strict";

        var giveawayObj = new GoTSkillzEntities.GiveawayDTO();

        giveawayObj.Id = $("#giveaway-id").val() === "" ? 0 : parseInt($("#giveaway-id").val());
        giveawayObj.Title = $("#giveaway-title").val();
        giveawayObj.Description = $("#giveaway-desc").val();
        giveawayObj.Code = $("#giveaway-code").val();
        giveawayObj.Rules = $("#giveaway-rule").val();
        giveawayObj.ImageUrl = $("#giveaway-imageurl").val();
        giveawayObj.VideoUrl = $("#giveaway-videourl").val();
        giveawayObj.International = $("#giveaway-international").is(":checked");
        giveawayObj.Sponsored = $("#giveaway-sponsored").is(":checked");
        giveawayObj.TotalEntries = $("#giveaway-entries").val() === "" ? 0 : parseInt($("#giveaway-entries").val());;
        giveawayObj.Active = $("#giveaway-active").is(":checked");


        if (giveawayObj.Title !== "") {
            $.ajax({
                url: giveawayAPI + "SaveGiveaway",
                type: "POST",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(giveawayObj),
                success: function(data) {
                    if (data !== null)
                        location.reload();
                },
                complete: function() {
                    GoTSkillZNotificationControls.ShowNotification("Giveaway Saved Successful!", "success");
                },
                error: function() {
                    GoTSkillZNotificationControls.ShowNotification("Could Not Save Giveaway Data, Please Contact Admin",
                        "danger");
                }
            });
        } else {
            GoTSkillZNotificationControls.ShowNotification("Please input all fields", "danger");
        }

     

    }
}


var GoTSKillZGiveawayInitializers = {
    initialize: function () {
        "use strict";

        GoTSkillZGiveawayAdminFunctions.bindGiveawayEditModal();
        GoTSkillZGiveawayAdminFunctions.checkForAdminUser();
        GoTSkillZGiveawayAnnouncementFunctions.getAllGiveaways();

    }
}



$(window).on("load", function () {
    "use strict";

    GoTSkillZGateKeeperFunctions.checkUserHasAccess(GoTSKillZGiveawayInitializers.initialize);

});