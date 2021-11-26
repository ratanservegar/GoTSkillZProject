/*jslint browser: true, nomen: true*/


var pmsAPI = '/WCF/PMSAPI.svc/';
var pages = [], currentSitemapData = {}, sitemapData = [], selectedNavigationNodeId = 0;


var GoTSkillZSitemapUtilityFunctions = {
    InitializeSubLeftMenu: function () {
        "use strict";

        $('body').addClass('with-subleft');

        // Showing sub left menu
        $('#showSubLeft').on('click', function (e) {
            e.stopImmediatePropagation();
            e.preventDefault();

            if ($('body').hasClass('show-subleft')) {
                $('body').removeClass('show-subleft');
            } else {
                $('body').addClass('show-subleft');
            }
        });
    }
}


var GoTSkillZSitemapDataFunctions = {
    GetSitemapTree: function () {
        "use strict";
        sitemapData = [];
        $.ajax({
            url: pmsAPI + "GetSitemapTree",
            type: "GET",
            dataType: 'json',
            cache: false,
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                sitemapData = data;
                if (typeof (sitemapData) !== 'undefined') {
                    $('#lt-page-sitemap-item').removeAttr('disabled');
                    $('#lt-link-sitemap-item').removeAttr('disabled');
                }
                GotSkillZSitemapTreeFunctions.RenderTreeView();

            },
            complete: function () {

            }
        });
    },
    GetSitemap: function (sitemapId) {
        "use strict";
        var sitemap = new GoTSkillzEntities.SitemapDTO();
        sitemap.Id = sitemapId;
        $.ajax({
            url: pmsAPI + "GetSitemap",
            type: "POST",
            data: JSON.stringify(sitemap),
            dataType: 'json',
            async: false,
            cache: false,
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                currentSitemapData = data;

                GoTSkillZSitemapFormFunctions.ResetForm();
                GoTSkillZSitemapFormFunctions.BindPagesDDL();

                GoTSkillZSitemapFormFunctions.BindData(currentSitemapData);
            },
            complete: function () {

            }
        });
    },
    GetPages: function () {
        "use strict";
        pages = [];
        $.ajax({
            url: pmsAPI + "GetAllPages",
            type: "GET",
            dataType: 'json',
            cache: false,
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                pages = data;
                GoTSkillZSitemapFormFunctions.BuildPages(pages);
            },
            complete: function () {
            }
        });
    }
}


var GotSkillZSitemapTreeFunctions = {
    RenderTreeView: function () {
        "use strict";
        var treeContainer = $('#treeViewContainer'), treeCheck = $('#treeview').data("kendoTreeView");

        if (treeCheck) {
            treeCheck.destroy();
            treeContainer.html("");
            treeContainer.append('<div id="treeview"></div>');
        }

        $("#treeview").kendoTreeView({
            dataSource: sitemapData,
            select: function (e) {
                $('#lt-edit-button').removeAttr('disabled');
                $('#lt-del-button').removeAttr('disabled');
                $('#lt-page-sitemap-item').removeClass('disabled');
                selectedNavigationNodeId = this.dataItem(e.node).id;
            }
        });
    },
    SitemapTreeNodeSelected: function (e) {
        "use strict";
        $('#lt-edit-button').removeAttr('disabled');
        $('#lt-del-button').removeAttr('disabled');
        selectedNavigationNodeId = this.dataItem(e.node).id;
        if (selectedNavigationNodeId === 0) {
            $('#lt-page-sitemap-item').removeClass('disabled');
            $('#lt-link-sitemap-item').removeClass('disabled');
        }
    }
}


var GoTSkillZSitemapFormFunctions = {
    BuildPages: function (data) {
        "use strict";
        var pageList = [];
        var ddlData = '<option value="">Select one ... </option>';
        pageList = data;
        $("#sitemapPage").html('');

        if (pageList.length > 0) {
            ddlData = ddlData + pageList.map(function (x) {
                return '<option value="' + x.Id + '">' + x.PageName + "</option>";
            }).join("");
        }

        $("#sitemapPage").html(ddlData);
    },
    newForm: function (e) {
        "use strict";
        selectedNavigationNodeId = 0;
        $("#sitemapId").val("");
        $("#sitemapType, #sitemapParent, #sitemapPage, #sitemapUrl").prop("disabled", false);
        $("#sitemapName").val("");
        $("#sitemapDesc").val("");
        $("#sitemapType").val("-1");
        $("#sitemapParent").val("-1");
        $("#sitemapPage").val("");
        $("#sitemapUrl").val("");
        $("#site-icon").val("");
        $("#sitemapSortOrder").val("");
        $("#sitemapIsActive").val("");
        $("#sitemapIsDeleted").val("");
        GoTSkillZSitemapFormFunctions.loadForm(e);
    },
    loadForm: function (e) {
        var type = e.id;
        GoTSkillZSitemapFormFunctions.ResetForm();
        GoTSkillZSitemapFormFunctions.BindPagesDDL();
        $("#sitemapId").val("");
        switch (type) {
            case "1": $("#sitemapType").val("1");
                $("#sitemapType").prop("disabled", true);
                $("#sitemapParent").val(-1);
                $("#sitemapParent").prop("disabled", true);
                $("#sitemapPage").val(0);
                $("#sitemapPage").prop("disabled", true);
                $("#sitemapUrl").prop("disabled", false);
                $("#site-icon").prop("disabled", true);
                break;
            case "2": $("#sitemapType").val("2");
                $("#sitemapType").prop("disabled", true);
                $("#sitemapParent").prop("disabled", false);
                $("#sitemapPage").prop("disabled", false);
                $("#sitemapUrl").prop("disabled", false);
                $("#site-icon").prop("disabled", false);
                break;
            case "3": $("#sitemapType").val("3");
                $("#sitemapType").prop("disabled", true);
                $("#sitemapParent").val(-1);
                $("#sitemapParent").prop("disabled", true);
                $("#sitemapPage").val(0);
                $("#sitemapPage").prop("disabled", true);
                $("#sitemapUrl").prop("disabled", false);
                $("#site-icon").prop("disabled", false);

                break;
        }

    },
    BindData: function (selectedSitemapObj) {
        "use strict";
        var sitemapType = selectedSitemapObj.TypeId;
        $('#sitemapId').val(selectedSitemapObj.Id);
        switch (sitemapType) {
            case 1: $("#sitemapName").val(selectedSitemapObj.Name);
                $("#sitemapDesc").val(selectedSitemapObj.Description);
                $("#sitemapType").val(selectedSitemapObj.TypeId);
                $("#sitemapType").attr("disabled", true);
                $("#sitemapParent").val(0);
                $("#sitemapParent").attr("disabled", true);
                $("#sitemapPage").val(0);
                $("#sitemapPage").attr("disabled", true);
                $("#sitemapUrl").val(selectedSitemapObj.AlternateUrl);
                $("#site-icon").val(selectedSitemapObj.Icon);
                $("#site-icon").attr("disabled", false);
                $("#sitemapSortOrder").val(selectedSitemapObj.SortOrder);
                if (selectedSitemapObj.IsActive) {
                    $("#sitemapIsActive").removeClass("off").addClass("on");
                } else {
                    $("#sitemapIsActive").removeClass("on").addClass("off");
                }
                break;
            case 2: $("#sitemapName").val(selectedSitemapObj.Name);
                $("#sitemapDesc").val(selectedSitemapObj.Description);
                $("#sitemapType").val(selectedSitemapObj.TypeId);
                $("#sitemapType").attr("disabled", true);
                $("#sitemapParent").val(selectedSitemapObj.ParentId);
                $("#sitemapPage").val(selectedSitemapObj.PageId);
                $("#sitemapUrl").val(selectedSitemapObj.AlternateUrl);
                $("#site-icon").attr("disabled", true);
                $("#sitemapSortOrder").val(selectedSitemapObj.SortOrder);

                if (selectedSitemapObj.IsActive) {
                    $("#sitemapIsActive").removeClass("off").addClass("on");
                } else {
                    $("#sitemapIsActive").removeClass("on").addClass("off");
                }
                break;
            case 3: $("#sitemapName").val(selectedSitemapObj.Name);
                $("#sitemapDesc").val(selectedSitemapObj.Description);
                $("#sitemapType").val(selectedSitemapObj.TypeId);
                $("#sitemapType").attr("disabled", true);
                $("#sitemapParent").val(0);
                $("#sitemapParent").attr("disabled", true);
                $("#sitemapPage").val(0);
                $("#sitemapPage").attr("disabled", true);
                $("#sitemapUrl").val(selectedSitemapObj.AlternateUrl);
                $("#site-icon").val(selectedSitemapObj.Icon);

                $("#sitemapSortOrder").val(selectedSitemapObj.SortOrder);
                if (selectedSitemapObj.IsActive) {
                    $("#sitemapIsActive").removeClass("off").addClass("on");
                } else {
                    $("#sitemapIsActive").removeClass("on").addClass("off");
                }
                break;
        }
    },
    ResetForm: function () {
        "use strict";
        $("#sitemapType, #sitemapParent, #sitemapPage, #sitemapUrl").prop("disabled", false);
        $("#sitemapId").val("");
        $("#sitemapName").val("");
        $("#sitemapDesc").val("");
        $("#sitemapType").val("-1");
        $("#sitemapParent").val("-1");
        $("#sitemapPage").val("");
        $("#sitemapUrl").val("");
        $("#site-icon").val("");
        $("#sitemapSortOrder").val("");
        $("#sitemapIsActive").val("");
        $("#sitemapIsDeleted").val("");
    },
    BindPagesDDL: function () {
        "use strict";
        $("#sitemapParent")
            .find("option")
            .remove()
            .end()
            .append('<option value="-1">Select Page</option>')
            .val("-1");
        var list = $("#sitemapParent");
        $.each(sitemapData, function () {
            if (this.typeId !== 3) {
                list.append(new Option(this.text, this.id));
            }
        });
    },
    ShowSitemapEditor: function () {
        "use strict";
        if (selectedNavigationNodeId !== 0) {

            GoTSkillZSitemapDataFunctions.GetSitemap(selectedNavigationNodeId);
        }
    },
    SaveSitemap: function () {
        "use strict";
        var sitemap = new GoTSkillzEntities.SitemapDTO();
        if ($("#sitemapId").val() === "") {
            sitemap.Id = 0;
        } else {
            sitemap.Id = parseInt($("#sitemapId").val());
        }

        sitemap.Name = $("#sitemapName").val();
        sitemap.TypeId = $("#sitemapType").val();
        if (sitemap.TypeId === "1" || sitemap.TypeId === "3") {
            sitemap.ParentId = 0;
            sitemap.PageId = 0;
        } else {
            sitemap.ParentId = $("#sitemapParent").val() === null ? 0 : parseInt($("#sitemapParent").val());
            sitemap.PageId = $("#sitemapPage").val() === "" ? 0 : parseInt($("#sitemapPage").val());
        }
        sitemap.AlternateUrl = $("#sitemapUrl").val();
        sitemap.Icon = $("#site-icon").val();
        sitemap.SortOrder = $("#sitemapSortOrder").val();
        sitemap.CreatedBy = selectedNavigationNodeId === 0 ? -1 : currentSitemapData.CreatedBy;
        sitemap.ModifiedBy = selectedNavigationNodeId !== 0 ? -1 : null;


        if ($("#sitemapIsActive").hasClass("on")) {
            sitemap.IsActive = true;
        } else {
            sitemap.IsActive = false;
        }

        //call ajax
        $.ajax({
            url: pmsAPI + "SaveSitemap",
            type: "POST",
            dataType: "json",
            data: JSON.stringify(sitemap),
            cache: false,
            contentType: "application/json; charset=utf-8",
            success: function () {
                GoTSkillZNotificationControls.ShowNotification("Sitemap Saved Successfully!", "success");
                GoTSkillZSitemapDataFunctions.GetSitemapTree();
            },
            complete: function () {
            },
            error: function () {
                GoTSkillZNotificationControls.ShowNotification("Sitemap Save Failed!", "danger");
            }
        });
    },
    validateSitemapData: function (e) {
        "use strict";
        var isvalidated = false;


        var siteMapType = $("#sitemapType").val();


        if (siteMapType !== "-1") {
            //        NetAppAssetRecoveryUILoader.SetLoaderMessage("Saving Data. Please Wait ... ");
            switch (parseInt(siteMapType)) {
                case 1: if ($("#sitemapName").val() === "") {

                    GoTSkillZNotificationControls.ShowNotification("Enter Title", "danger");
                    isvalidated = true;
                }
                    if ($("#sitemapSortOrder").val() === "") {

                        GoTSkillZNotificationControls.ShowNotification("Enter Sort Id", "danger");
                        isvalidated = true;
                    }

                    break;
                case 2: if ($("#sitemapName").val() === "") {

                    GoTSkillZNotificationControls.ShowNotification("Enter Title", "danger");
                    isvalidated = true;
                }
                    if ($("#sitemapParent").val() === "" || $("#sitemapParent").val() === "-1") {
                        GoTSkillZNotificationControls.ShowNotification("Select Parent Page", "danger");

                        isvalidated = true;
                    }
                    if ($("#sitemapPage").val() === "" || $("#sitemapPage").val() === "-1") {
                        GoTSkillZNotificationControls.ShowNotification("Select Page", "danger");
                        isvalidated = true;
                    }
                    if ($("#sitemapSortOrder").val() === "") {
                        GoTSkillZNotificationControls.ShowNotification("Enter Sort Id", "danger");
                        isvalidated = true;
                    }
                    if ($("#sitemapUrl").val() === "") {

                        GoTSkillZNotificationControls.ShowNotification("Enter Alternate URL", "danger");
                        isvalidated = true;
                    }


                    break;
                case 3: if ($("#sitemapName").val() === "") {
                    GoTSkillZNotificationControls.ShowNotification("Enter Title", "danger");
                    isvalidated = true;
                }
                    if ($("#sitemapUrl").val() === "") {
                        GoTSkillZNotificationControls.ShowNotification("Enter Alternate URL", "danger");
                        isvalidated = true;
                    }
                    if ($("#sitemapSortOrder").val() === "") {
                        GoTSkillZNotificationControls.ShowNotification("Enter Sort Id", "danger");
                        isvalidated = true;
                    }


                    break;
            }
            if (!isvalidated) {
                GoTSkillZSitemapFormFunctions.SaveSitemap(e);
            }
        } else {
            GoTSkillZNotificationControls.ShowNotification("Cannot Save Empty Form", "danger");
        }


    }
}




var GoTSkillZSitemapInitializer = {
    initializeSitemapFunctions: function () {
        "use strict";
        $('#sitemap-content').show();
        GoTSkillZSitemapUtilityFunctions.InitializeSubLeftMenu();
        GoTSkillZSitemapDataFunctions.GetSitemapTree();
        GoTSkillZSitemapDataFunctions.GetPages();
    }
}


$(window).on('load', function () {
    "use strict";


    GoTSkillZGateKeeperFunctions.checkUserHasAccess(GoTSkillZSitemapInitializer.initializeSitemapFunctions);

});


