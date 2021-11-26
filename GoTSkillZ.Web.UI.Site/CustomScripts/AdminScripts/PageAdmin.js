/*jslint browser: true, nomen: true*/
var pmsAPI = "/WCF/PMSAPI.svc/";
var membershipAPI = "/WCF/MembershipAPI.svc/";

var _pagesList = {}, _rolesList = {}, _pageRolesList = {}, _roleAccessList = {}, _selectedPage = {};
var _pageIdNamePair = [], _roleIdNamePair = [];
var _oldPageCellValue = _oldRoleAccessDdlValue = "";
var _selectedPageModule = 0;


var GoTSkillZPageRoleFunctions = {
    GetActiveRoles: function () {
        "use strict";
        $.ajax({
            url: membershipAPI + "GetAllActiveRoles",
            type: "GET",
            dataType: "json",
            cache: false,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                _rolesList = data;
            },
            complete: function () {
              
            },
            error: function () {

            }
        });
    },
    GetPageRoles: function (pageId) {
        "use strict";
        var pageRoleDto = new GoTSkillzEntities.PageRoleDTO();
        pageRoleDto.PageId = parseInt(pageId);
        $.ajax({
            url: pmsAPI + "GetPageRoles",
            type: "POST",
            data: JSON.stringify(pageRoleDto),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                _pageRolesList = data;
                GoTSkillZPageRoleFunctions.DrawPageRoleDialog();
            },
            complete: function () {
            },
            error: function () {
            }
        });
    },
    DrawPageRoleForm: function () {
        "use strict";
        var formContainer = $("<div>", { 'class': "row" }),
            checkboxListContainer = $("<div>", { 'class': "col-md-12" }),
            checkbox,
            checkBoxLabel,
            checkBoxListField;
        //build the cbk dynamically

        $.each(_rolesList, function () {
            checkbox = $("<div>", { 'class': "col-md-4" });
            checkBoxLabel = $("<label>", { 'for': this.RoleName });
            checkBoxListField = $("<input>", {
                'type': "checkbox",
                'name': this.RoleName,
                'value': this.Id,
                'class': "checkbox-control"
            });
            if (GoTSkillZPageRoleFunctions.IsRoleInPage(_selectedPage.Id, this.Id)) {
                checkBoxListField.attr("checked", "checked");
            }
            $(checkbox).append(checkBoxLabel).append(checkBoxListField).append(this.RoleName);
            $(checkboxListContainer).append(checkbox);
        });
        $(formContainer).append(checkboxListContainer);
        return $(formContainer);
    },
    ReadPageRoles: function () {
        "use strict";
        var cbxList = $("#large-modal").find(".modal-body").find("input:checked"), newPageRoles = [], pageRole;

        if (cbxList.length !== 0) {
            $.each(cbxList, function () {
                pageRole = new GoTSkillzEntities.PageRoleDTO();
                pageRole.PageId = _selectedPage.Id;
                pageRole.RoleId = parseInt(this.value);
                newPageRoles.push(pageRole);
            });
        } else {
            pageRole = new GoTSkillzEntities.PageRoleDTO();
            pageRole.PageId = _selectedPage.Id;
            newPageRoles.push(pageRole);
        }
        GoTSkillZPageRoleFunctions.SavePageRoles(newPageRoles);
        GoTSkillZUIFunctions.HideModal();
    },
    DrawPageRoleDialog: function () {
        "use strict";
        var modalProperties = new GoTSkillZUIFunctions.ModalProperties();
        modalProperties.title = "Page Role Associations";
        modalProperties.body = GoTSkillZPageRoleFunctions.DrawPageRoleForm();
        $("#large-modal-submit-btn").unbind().bind("click", GoTSkillZPageRoleFunctions.ReadPageRoles);
        GoTSkillZUIFunctions.ShowModal("Large", modalProperties);
    },
    ManagePageRoles: function () {
        "use strict";
        GoTSkillZPageRoleFunctions.GetPageRoles(_selectedPage.Id);
    },
    IsRoleInPage: function (pageId, roleId) {
        "use strict";
        return _.filter(_pageRolesList, { PageId: pageId, RoleId: roleId }).length > 0 ? true : false;
    },
    SavePageRoles: function (pageRolesObj) {
        "use strict";

        //NetAppAssetRecoveryUILoader.SetLoaderMessage("Saving Data. Please Wait ... ");
        $.ajax({
            url: pmsAPI + "SavePageRoles",
            type: "POST",
            data: JSON.stringify(pageRolesObj),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function () {
                GoTSkillZNotificationControls.ShowNotification("Page Roles Saved successfully", "success");
                $("#pageAdminGrid").data("kendoGrid").dataSource.read();
                $("#pageAdminGrid").data("kendoGrid").refresh();
            },
            complete: function () {
            },
            error: function () {

            }
        });
    }
}



var GoTSkillZPageAdminGridFunctions = {
    initializeGrid: function () {
        "use strict";
        var columnSchema = [];
        columnSchema.push({ hidden: true, field: "Id", title: "Page ID" });
        columnSchema.push({ field: "PageName", title: "Page Name", width: 70 });
        columnSchema.push({ field: "BaseUrl", title: "BaseUrl", width: 100 });
        columnSchema.push({ field: "PageRoles", title: "Roles", width: 100 });
        columnSchema.push({ field: "IsActive", title: "Active", width: 50 });
        columnSchema.push({ field: "ShowContent", title: "Show Content", width: 50 });
        columnSchema.push({
            command: [
                {
                    name: "edit",
                    text: "",
                    click: function (e) {
                        e.preventDefault();
                        GoTSkillZPageAdminGridFunctions.onChangeForPageName(this);


                    }
                },
                {
                    name: "managepageroles",
                    iconClass: "fa fa-map-marker",
                    text: "",
                    click: function (e) {
                        var tr = $(e.target).closest("tr");
                        var data = this.dataItem(tr);
                        _selectedPage = data;
                        GoTSkillZPageRoleFunctions.ManagePageRoles();
                    }
                }
            ],
            title: "Actions",
            width: 180
        });

        var initialGridState = {
            columns: columnSchema,
            dataSource: {
                transport: {
                    read: {
                        url: pmsAPI + "GetAllPages",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        cache: false,
                        success: function () {

                        },
                        complete: function (data) {
                            _pagesList = data.responseJSON;
                        },
                        error: function () {

                        }
                    },
                    update: {
                        url: pmsAPI + "SavePage",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        type: "POST"
                    },
                    create: {
                        url: pmsAPI + "SavePage",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        type: "POST",
                        complete: function (data) {
                            if (data.responseJSON == undefined) {
                                GoTSkillZNotificationControls.ShowNotification("Page Name already exists", "danger");
                                _pagesList = data.responseJSON;
                            } else {
                                _pagesList = data.responseJSON;
                            }
                        }
                    },
                    parameterMap: function (data, operation) {
                        if (operation === "read") {
                            return JSON.stringify(data);
                        } else {
                            return JSON.stringify(data);
                        }
                    }
                },
                pageSize: 100,
                schema: {
                    model: {
                        id: "Id",
                        fields: {
                            Id: { type: "number", editable: false },
                            PageName: { type: "string", editable: true },
                            BaseUrl: { type: "string", validation: { required: { message: "Base Url is required" } } },
                            PageRoles: { type: "string", editable: false },
                            IsActive: { type: "boolean", editable: true, defaultValue: true },
                            ShowContent: { type: "boolean", editable: true, defaultValue: false }
                        }
                    }
                }
            },
            pageable: {
                refresh: true,
                pageSizes: [10, 25, 50, 100],
                buttonCount: 10
            },
            editable: {
                mode: "popup"
            },
            sortable: true,
            toolbar: [
                "create",
                {
                    template:
                        '<a class="k-button k-grid-custom-command k-grid-clear-page-filter" href="\\#"><span class="k-icon k-cancel"></span>CLEAR FILTERS</a>'
                }
            ],
            filterable: {
                extra: false,
                operators: {
                    string: {
                        contains: "Contains",
                        doesnotcontain: "Does Not Contain",
                        endswith: "Ends With",
                        eq: "Is Equal To",
                        neq: "Is Not Equal To",
                        startswith: "Starts with"
                    },
                    date: {
                        eq: "Is equal to",
                        lte: "Is before or equal to",
                        gte: "Is after or equal to"

                    }
                }
            },
            resizable: false,
            columnMenu: true,
            save: function (e) {
                var pageNames = [];
                var pageName = e.model.PageName;
                if (pageName == undefined || pageName.trim() === "" ) {
                    GoTSkillZNotificationControls.ShowNotification("Page Name is required", "danger");
                    e.preventDefault();
                } else if (!(/^[a-zA-Z]/.test(pageName))) {
                    GoTSkillZNotificationControls.ShowNotification("Page Name can contain only characters", "danger");
                    e.preventDefault();
                } else {
                    if (e.model.isNew() === true) {
                      
                        $.each(_pagesList, function () {
                            pageNames.push(this.PageName.toLowerCase());
                        });
                        var contains = _.contains(pageNames, pageName.toLowerCase());

                        if (contains === true) {
                            GoTSkillZNotificationControls.ShowNotification("Page Name Already Exists", "danger");
                            e.preventDefault();
                        } else {
                            GoTSkillZNotificationControls.ShowNotification("Saved Successfully", "success");
                        }
                    } else {
                        if (_oldPageCellValue !== pageName) {
                             pageNames = [];
                            $.each(_pagesList, function () {
                                pageNames.push(this.PageName.toLowerCase());
                            });
                            var contains = _.contains(pageNames, pageName.toLowerCase());

                            if (contains === true) {
                                GoTSkillZNotificationControls.ShowNotification("Page Name Already Exists", "danger");
                                e.preventDefault();
                            } else {
                                GoTSkillZNotificationControls.ShowNotification("Saved Successfully", "success");
                            }
                        } else {
                            GoTSkillZNotificationControls.ShowNotification("Saved Successfully", "success");
                        }
                    }
                }
            },
            batch: false,
            dataBound: GoTSkillZPageAdminGridFunctions.onDataBound,
            height: $(window).height() - 270
        };

        var gridElement = $("#pageAdminGrid").kendoGrid(initialGridState);
        gridElement.find("thead").kendoTooltip({
            filter: "th",
            content: function (e) {
                var target = e.target;
                var tval = $(target).find(".k-link").text();
                if (tval !== "")
                    return $(target).find(".k-link").text();
                else
                    return "Actions";
            }
        });
    },
    onDataBound: function () {
        "use strict";
        var filter = this.dataSource.filter();
        this.thead.find(".k-header-column-menu.k-state-active").removeClass("k-state-active");
        if (filter) {
            var filteredMembers = {};
            GoTSkillZPageAdminGridFunctions.setFilteredMembers(filter, filteredMembers);
            this.thead.find("th[data-field]").each(function () {
                var cell = $(this);
                var filtered = filteredMembers[cell.data("field")];
                if (filtered) {
                    cell.find(".k-header-column-menu").addClass("k-state-active");
                }
            });
        }

        //edit icon tooltip
        $("a.k-grid-edit").each(function () {
            $(this).attr("data-toggle", "tooltip");
            $(this).attr("data-placement", "bottom");
            $(this).attr("title", "Edit Page");
        });

        $("a.k-grid-edit").tooltip({ 'trigger': "hover" });

        //Manage Roles tooltip
        $("a.k-grid-managepageroles").each(function () {
            $(this).html('<span class="fa fa-user-alt"></span>');
            $(this).attr("data-toggle", "tooltip");
            $(this).attr("data-placement", "bottom");
            $(this).attr("title", "Manage Page Roles");
            $(this).tooltip({ 'trigger': "hover" });
        });


    },
    setFilteredMembers: function (filter, members) {
        "use strict";
        if (filter.filters) {
            for (var i = 0; i < filter.filters.length; i++) {
                GoTSkillZPageAdminGridFunctions.setFilteredMembers(filter.filters[i], members);
            }
        } else {
            members[filter.field] = true;
        }
    },
    onChangeForPageName: function (e) {
        "use strict";
        var cell = e._editContainer;
        _oldPageCellValue = cell.find("input").val();


        var grid = $("#pageAdminGrid").data("kendoGrid");

        grid._editContainer.find(".k-grid-cancel").bind("click", function () {

            setTimeout(function () { grid.dataSource.sync(); }, 10);

        });
    },
    onChangeForRoleAccessName: function (e) {
        "use strict";
        var cell = e._editContainer;
        _oldRoleAccessDdlValue = cell.find("input").val();
    },
    onPageGridFilterClear: function () {
        "use strict";
        var datasource = $("#pageAdminGrid").data("kendoGrid").dataSource,
            filters = datasource.filter();


        if (filters) {
            datasource.filter([]);
        }


        $("#pageAdminGrid").data("kendoGrid").dataSource.read();
        $("#pageAdminGrid").data("kendoGrid").refresh();


    }
}



var GoTSkillZPageInitializer = {
    initializePageFunctions: function () {
        "use strict";

        GoTSkillZPageAdminGridFunctions.initializeGrid();
        GoTSkillZPageRoleFunctions.GetActiveRoles();
    }
}

$(window).on('load', function () {
    "use strict";

    GoTSkillZGateKeeperFunctions.checkUserHasAccess(GoTSkillZPageInitializer.initializePageFunctions);
   

    $("a.k-grid-clear-page-filter").click(function () {
        GoTSkillZPageAdminGridFunctions.onPageGridFilterClear();
    });
});

