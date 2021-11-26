var partnerAPI = "/WCF/PartnerAPI.svc/";


var setupTypes, setupOptions, setupCompanies;
var removedSetups = [], removedPeripherals = [];
var imageSetupId = "";

var GoTSkillZSetupDataFunctions = {
    getSetupTypes: function () {
        "use strict";

        $.ajax({
            url: membershipAPI + "GetSetupTypes",
            type: "GET",
            dataType: "json",
            cache: false,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                setupTypes = data;
            },
            error: function (data) {
                GoTSkillZNotificationControls.ShowNotification("Could Not Get Setup Types Data, Please Contact Admin", "danger");
            }
        });
    },
    getSetupOptions: function () {
        "use strict";

        $.ajax({
            url: membershipAPI + "GetSetupOptions",
            type: "GET",
            dataType: "json",
            cache: false,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                setupOptions = data;
            },
            error: function (data) {
                GoTSkillZNotificationControls.ShowNotification("Could Not Get Setup Options Data, Please Contact Admin", "danger");
            }
        });
    },
    getSetupCompanies: function () {
        "use strict";

        $.ajax({
            url: partnerAPI + "GetCompanies",
            type: "GET",
            dataType: "json",
            cache: false,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                setupCompanies = data;
            },
            error: function (data) {
                GoTSkillZNotificationControls.ShowNotification("Could Not Get Setup Companies Data, Please Contact Admin", "danger");
            }
        });
    },
};
var GoTSkillZPeripheralFormFunctions = {
    userPeripheralsFormHTML: function (id) {
        "use strict";

        var peripheralDiv = $("<div>",
        {
            'id': "peripheral-item-" + id,
            'class': "peripheral-item"
        });


        var removeBtnDiv = $("<div>",
        {
            "class": "t-15 r-25"
        });

        var removePeripheralBtn = $("<a>", {
            'href': " ",
            "id": "remove-peripheral-" + id,
            "class": "tx-white-5 hover-info remove-peripheral"
        });

        var removePeripheralBtnIcon = $("<i>", {
            "class": "icon ion-minus tx-12"
        });


        var inputPeripheralType = $("<input>", {
            'id': "regi-peripheral-type-" + id,
            'class': "form-control form-control-dark regi-peripheral-type",
            'placeholder': "Select Peripheral Type"
        });


        var inputPeripheralCompany = $("<input>", {
            'id': "regi-peripheral-company-" + id,
            'class': "form-control form-control-dark regi-peripheral-company",
            'placeholder': "Select Brand"
        });

        var inputPeripheralDescription = $("<input>", {
            'id': "regi-peripheral-description-" + id,
            'type': "text",
            'class': "form-control form-control-dark regi-peripheral-description",
            'placeholder': "Product Description"
        });


        var inputPeripheralImage = $("<input>", {
            'id': "regi-peripheral-affiliate-" + id,
            'type': "text",
            'class': "form-control form-control-dark  regi-peripheral-affiliate",
            'placeholder': "www.Amazon.in...."
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

        divCol1_1.append(divInputGroup1_1.append(inputPeripheralType));
        divCol1_2.append(divInputGroup1_2.append(inputPeripheralCompany));
        divCol1_3.append(divInputGroup1_3.append(inputPeripheralDescription));
        divRow1.append(divCol1_1).append(divCol1_2).append(divCol1_3);
        peripheralDiv.append(divRow1);


        var divRow2 = $("<div>", {
            'class': "row mg-b-10"
        });

        var divCol2_1 = $("<div>", {
            'class': "col-lg-4 mg-b-5"
        });


        divCol2_1.append(inputPeripheralImage);


        divRow2.append(divCol2_1);
        peripheralDiv.append(divRow2);

        removeBtnDiv.append(removePeripheralBtn.append(removePeripheralBtnIcon).append("&nbsp;Remove"));

        var divRow3 = $("<div>", {
            'class': "row mg-b-10"
        });

        var divCol3 = $("<div>", {
            'class': "col-lg-12 mg-b-5"
        });
        divCol3.append(removeBtnDiv);
        divRow3.append(divCol3);
        peripheralDiv.append(divRow3);

        return peripheralDiv;

    },
    buildUserPeripheralsForm: function (clean, newDiv, existingSetupsObj) {
        "use strict";

        var peripheralsFormContainer = $("#regi-peripheral-container");

        if (clean === true)
            peripheralsFormContainer.html("");

        var currentDivCount = $("#regi-peripheral-container").find(".peripheral-item").length;


        if (newDiv === true) {
            var currentIndex = currentDivCount + 1;
            peripheralsFormContainer.prepend(GoTSkillZPeripheralFormFunctions.userPeripheralsFormHTML(currentIndex));
            GoTSkillZSetupInitializers.initializePeripheralOptionDDL("regi-peripheral-type-" + currentIndex);

            GoTSkillZSetupImageFunctions.initializePeripheralImageUpload();
            
        }


        if (newDiv === false && existingSetupsObj !== null) {

            $.each(existingSetupsObj, function (index, item) {

                var currentContainer = GoTSkillZPeripheralFormFunctions.userPeripheralsFormHTML(index);
                peripheralsFormContainer.append(currentContainer);

                GoTSkillZPeripheralFormFunctions.popluatePeripheralForm(currentContainer, item);

               
            });
            GoTSkillZSetupImageFunctions.getPeriheralImagesForEditModel();
              
        }

        GoTSkillZSetupBinders.bindPeripheralRemoveBtn();

        $("#user-peripheral-add-modal").find(".modal-body").css("overflow-y", "hidden");


        GoTSkillZSetupBinders.bindAddPeriheralItemBtn();


        var perfectScrollbar = new PerfectScrollbar(".peripheral-modal-body", {
            wheelPropagation: true,
            minScrollbarLength: 20
        });

        GoTSkillZSetupInitializers.initializePeripheralCompanyDDL();
        GoTSkillZSetupBinders.bindPeripheralSaveBtn();

        GoTSkillZSetupImageFunctions.initializePeripheralImageUpload();

        $("#user-peripheral-add-modal").modal("show");
    },
    popluatePeripheralForm: function (container, peripheralObj) {
        "use strict";

        if (container !== undefined && container !== null && peripheralObj !== undefined && peripheralObj !== null) {
            $(container).attr("data-id", peripheralObj.Id);


            //set company
            $(container).find(".regi-peripheral-company").val(peripheralObj.CompanyName);

            //set product description
            $(container).find(".regi-peripheral-description").val(peripheralObj.ProductDetails);

            //set affilaiate
            $(container).find(".regi-peripheral-affiliate").val(peripheralObj.AffiliateLink);


            //set type
            $(container).find("input").each(function (index, item) {

                if (item.className.indexOf("regi-peripheral-type") > 0) {
                    GoTSkillZSetupInitializers.initializePeripheralOptionDDL(this.id);
                    $(item).data("kendoDropDownList").text(peripheralObj.PeripheralType);
                }
            });


        }

    },
    removeUserPeripheralsFormDiv: function (e) {
        "use strict";

        var dataId = e.currentTarget.parentElement.parentElement.parentElement.parentElement.getAttribute("data-id");

        if (dataId !== null) {
            removedPeripherals.push(dataId);
        }


        e.target.parentElement.parentElement.parentElement.parentElement.remove();

    }
};




var GoTSkillZSetupDisplayFunctions = {
    buildUserPeripheralContainer: function (userPeripherals) {
        "use strict";

        var peripheralContainer = $("#setup-container");

      

        if (userPeripherals != null && userPeripherals.length > 0) {


            peripheralContainer.append(GoTSkillZSetupDisplayFunctions.buildMainPeripheralCard(userPeripherals));

        }


        $(".peripheral-edit").unbind("click").bind("click", function (e) {
            e.preventDefault();
            e.stopImmediatePropagation();

            GoTSkillZPeripheralFormFunctions.buildUserPeripheralsForm(true, false, userPeripherals);

        });

        GoTSkillZSetupBinders.bindLightGallery();
    },
    buildUserSetupContainers: function (userSetups) {
        "use strict";

        var setupContainer = $("#setup-container");


        if (userSetups != null && userSetups.length > 0) {

            setupContainer.html("");
            var groupedArray = _.groupBy(userSetups, "SetupId");

            $.each(groupedArray, function (index, item) {

                var setupType = _.filter(userObj.UserSetups, function (x) {
                    return x.Id === item[0].SetupId;
                });

                if (setupType.length > 0) {
                    setupContainer.append(GoTSkillZSetupDisplayFunctions.buildMainSetupCard(item, setupType[0].SetupTypeId, setupType[0].SetupImagePath, setupType[0].SetupName));
                }

            });
        }

        $(".setup-edit").unbind("click").bind("click", function (e) {
            e.preventDefault();
            e.stopImmediatePropagation();

            var setupId = e.currentTarget.parentElement.parentElement.parentElement.parentElement.parentElement.parentElement.getAttribute("data-id");

            if (setupId !== "")
                GoTSkillZSetupModelFunctions.showUserSetupEditModal(setupId);

        });

        GoTSkillZSetupBinders.bindLightGallery();





    },
    buildMainSetupCard: function (setupItems, typeId, setupImagePath, setupName) {
        "use strict";
        var divTopCol = $("<div>", {
            "class": "col-md-6"
        });

        var divContainer = $("<div>", {
            "data-id": setupItems[0].SetupId,
            "class": "card card-block bd-gray-400 pd-20 mg-t-20"
        });


        if (setupItems !== undefined && setupItems !== null && setupItems.length > 0) {
            var setupTypeName = "";
         
            if (typeId !== undefined && typeId !== null) {
                var setupObj = _.chain(setupTypes).filter(function (x) {
                    return (x.Id === typeId);
                }).value();

                if (setupObj.length > 0)
                    setupTypeName = setupObj[0].SetupType1;
            }


            if (setupItems.length > 0) {

                var divHeader = $("<div>", {
                    "class": "card-header",
                    "style": "background:none !Important;"
                });

                var divRow = $("<div>", {
                    "class": "row"
                });
                var divCol = $("<div>", {
                    "class": "col-sm-12"
                });


                if (setupName === "") {
                    var divTitle = $("<div>", {
                        "class": "card-title tx-teal",
                        "style": "float:left"
                    }).html(setupTypeName);
                } else {
                    var divTitle = $("<div>", {
                        "class": "card-title tx-teal",
                        "style": "float:left"
                    }).html(setupTypeName + " - " + setupName);
                }
           

                var divEditContainer = $("<div>",
                {
                    "class": "tx-right",
                    "style": "float:right;"
                });

                var divEditBtnChild = $("<div>",
                {
                    "class": "t-15 r-25"
                });

                var divEditBtn = $("<a>",
                {
                    "class": "tx-white-5 hover-info setup-edit",
                    "href": "#"
                }).append('<i class="icon ion-edit tx-16"></i>');


                divEditBtnChild.append(divEditBtn);
                divEditContainer.append(divEditBtnChild);

                divCol.append(divTitle);
                divCol.append(divEditContainer);
                divRow.append(divCol);
                divHeader.append(divRow);
                divContainer.append(divHeader);

                var divLightBox = '<div id="lightgallery-' + setupItems[0].SetupId + '" class="lightbox" data-id=' + setupItems[0].SetupId + ' type="setup"><a href="' + setupImagePath + '"><img src="' + setupImagePath + '" class="img-fluid"/></a></div>';

                divContainer.append(divLightBox);

                var divFlex = $("<div>",
                {
                    "class": "d-flex justify-content-between align-items-center mg-t-20"
                });

                var emptyDiv = $("<div>",
                {
                    "class": "col-md-12"
                });

                var divContentheader = $("<h6>", {
                    "class": "tx-14 tx-normal mg-b-2"
                }).append('<a href="#" class="tx-teal">Specifications</a>');

                var divSetupContainer = $("<div>", {
                    "id": "edit-setup-container",
                    "class": "form-layout form-layout-4 edit-setup-container",
                    "style": "border: 0px !important;"
                });

                emptyDiv.append(divContentheader);


                $.each(setupItems, function (index, value) {

                    var divSetupRow = $("<div>", {
                        "class": "row mg-t-20"
                    });

                    var divLabel1 = $("<label>",
                    {
                        "class": "col-sm-6 form-control-label tx-12 tx-mont  tx-spacing-1 mg-b-2"

                    }).append('<span class="' + GoTSkillZSetupDisplayFunctions.getSetupIcon(value.Component) + '" style="font-size: 20px; padding-right: 5px"></span>' + value.Component);

                    var divLabelValue = $("<label>", {
                        "setupDataId": value.Id,
                        "class": "col-sm-6 form-control-label tx-white mg-b-25 " + value.Component
                    });

                    if (value.AffiliateLink !== "") {
                        var link = GoTSkillZCommonUtilityFunctions.formatUrl(value.AffiliateLink);
                        divLabelValue.html('<a href="' + link + '"  target="_blank">' + value.ProductDetails + "</a>");
                    } else {
                        var componentValue = value.ProductDetails === "" ? "No Data Provided" : value.ProductDetails;
                        divLabelValue.html(componentValue);
                    }


                    divSetupRow.append(divLabel1).append(divLabelValue);
                    divSetupContainer.append(divSetupRow);
                    emptyDiv.append(divSetupContainer);
                    divFlex.append(emptyDiv);
                });

                divContainer.append(divFlex);
            }

        }

        divTopCol.append(divContainer);

        return divTopCol;
    },
    buildMainPeripheralCard: function (userPeripherals) {
        "use strict";
        var divTopCol = $("<div>", {
            "class": "col-md-6"
        });

        var divContainer = $("<div>", {
            "class": "card card-block bd-gray-400 pd-20 mg-t-20"
        });


        if (userPeripherals !== undefined && userPeripherals !== null && userPeripherals.length > 0) {

            var divHeader = $("<div>", {
                "class": "card-header",
                "style": "background:none !Important;"
            });

            var divRow = $("<div>", {
                "class": "row"
            });
            var divCol = $("<div>", {
                "class": "col-sm-12"
            });

            var divTitle = $("<div>", {
                "class": "card-title tx-teal",
                "style": "float:left"
            }).html("Peripherals");

            var divEditContainer = $("<div>",
            {
                "class": "tx-right",
                "style": "float:right;"
            });

            var divEditBtnChild = $("<div>",
            {
                "class": "t-15 r-25"
            });

            var divEditBtn = $("<a>",
            {
                "class": "tx-white-5 hover-info peripheral-edit",
                "href": "#"
            }).append('<i class="icon ion-edit tx-16"></i>');


            divEditBtnChild.append(divEditBtn);
            divEditContainer.append(divEditBtnChild);

            divCol.append(divTitle);
            divCol.append(divEditContainer);
            divRow.append(divCol);
            divHeader.append(divRow);
            divContainer.append(divHeader);


//            var divLightBox = '<div id="lightgallery-' + userPeripherals[0].UserId + '" class="lightbox" data-id=' + userPeripherals[0].UserId + ' type="peripheral"><a href="/CustomContent/Images/88511_thumb.jpg"><img src="/CustomContent/Images/88796_thumb.jpg" class="img-fluid"/></a></div>';
           

            var divLightBox = '<div id="lightgallery-' + userPeripherals[0].UserId + '" class="lightbox" data-id=' + userPeripherals[0].UserId + ' type="peripheral"><a href="' + userPeripherals[0].PeripheralTopimage + '"><img src="' + userPeripherals[0].PeripheralTopimage + '" class="img-fluid"/></a></div>';
            divContainer.append(divLightBox);

            var divFlex = $("<div>",
            {
                "class": "d-flex justify-content-between align-items-center mg-t-20"
            });

            var emptyDiv = $("<div>",
            {
                "class": "col-md-12"
            });

            var divSetupContainer = $("<div>", {
                "id": "edit-peripheral-container",
                "class": "form-layout form-layout-4 edit-peripheral-container",
                "style": "border: 0px !important;"
            });



            $.each(userPeripherals, function (index, value) {

                var divSetupRow = $("<div>", {
                    "class": "row mg-t-20"
                });

                var divLabel1 = $("<label>",
                {
                    "class": "col-sm-6 form-control-label tx-12 tx-mont  tx-spacing-1 mg-b-2"

                }).append('<span class="' + GoTSkillZSetupDisplayFunctions.getSetupIcon(value.PeripheralType) + '" style="font-size: 20px; padding-right: 5px"></span>' + value.PeripheralType);

                var divLabelValue = $("<label>", {
                    "peripheralDataId": value.Id,
                    "class": "col-sm-6 form-control-label tx-white mg-b-25 " + value.PeripheralType
                });

                if (value.AffiliateLink !== "") {
                    var link = GoTSkillZCommonUtilityFunctions.formatUrl(value.AffiliateLink);
                    divLabelValue.html('<a href="' + link + '"  target="_blank">' + value.ProductDetails + "</a>");
                } else {
                    var componentValue = value.ProductDetails === "" ? "No Data Provided" : value.ProductDetails;
                    divLabelValue.html(componentValue);
                }


                divSetupRow.append(divLabel1).append(divLabelValue);
                divSetupContainer.append(divSetupRow);
                emptyDiv.append(divSetupContainer);
                divFlex.append(emptyDiv);
            });

            divContainer.append(divFlex);

        }

        divTopCol.append(divContainer);

        return divTopCol;
    },
    getSetupIcon: function (component) {
        "use strict";
        var returnIcon = "";

        if (component !== "") {
            var item = _.filter(setupOptions, function (x) {
                return x.SetupOption1.trim().toLowerCase() === component.trim().toLowerCase();
            });

            if (item.length > 0)
                returnIcon = item[0].Icon;
        }

        return returnIcon;
    },
    initializeSetupLightGallery: function (e) {
        "use strict";


        var setupDataId = e.currentTarget.getAttribute("data-id");
        var elementId = e.currentTarget.getAttribute("id");
        var type = e.currentTarget.getAttribute("type");
        var kendoFileDTO = [];
        var imageArray = [];
        if (type === "setup") {
            if (setupDataId !== null && setupDataId !== "0") {
                $.ajax({
                    url: fileAPI + "GetSetupImages/" + userObj.UserId + "/" + setupDataId,
                    type: "GET",
                    dataType: "json",
                    //global: false,
                    contentType: "application/json; charset=utf-8",
                    success: function(data) {
                        if (data) {
                            kendoFileDTO = data;

                        }
                    },
                    complete: function(data) {

                        if (kendoFileDTO.length > 0) {
                            $.each(kendoFileDTO, function(index, item) {
                                var newImageItem = {
                                    "src": item.imagePath,
                                    "thumb": item.imagePath
                                };
                                imageArray.push(newImageItem);
                            });
                            $("#" + elementId).lightGallery({
                                mode: "lg-fade",
                                cssEasing: "cubic-bezier(0.25, 0, 0.25, 1)",
                                dynamic: true,
                                dynamicEl: imageArray
                            });

                        } else {

                            if (userObj.ReadOnly === false) {
                                GoTSkillZNotificationControls.ShowNotification("Please Add Setup Images", "info");
                                GoTSkillZSetupModelFunctions.showUserSetupEditModal(setupDataId);
                            } else {
                                GoTSkillZNotificationControls.ShowNotification("No Images Added", "info");
                            }
                          
                        }

                    },
                    error: function(data) {
                        GoTSkillZNotificationControls.ShowNotification("Could Not Get Setup Image Data, Please Contact Admin", "danger");
                    }
                });
            }
        } else {
               $.ajax({
                   url: fileAPI + "GetPeripheralImages/" + userObj.UserId,
                    type: "GET",
                    dataType: "json",
                    //global: false,
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        if (data) {
                            kendoFileDTO = data;

                        }
                    },
                    complete: function (data) {

                        if (kendoFileDTO.length > 0) {
                            $.each(kendoFileDTO, function (index, item) {
                                var newImageItem = {
                                    "src": item.imagePath,
                                    "thumb": item.imagePath
                                };
                                imageArray.push(newImageItem);
                            });
                            $("#" + elementId).lightGallery({
                                mode: "lg-fade",
                                cssEasing: "cubic-bezier(0.25, 0, 0.25, 1)",
                                dynamic: true,
                                dynamicEl: imageArray
                            });

                        } else {

                            if (userObj.ReadOnly === false) {
                                GoTSkillZNotificationControls.ShowNotification("Please Add Peripheral Images", "info");
                                GoTSkillZPeripheralFormFunctions.buildUserPeripheralsForm(true, false, userObj.UserPeripheralData);
                            } else {
                                GoTSkillZNotificationControls.ShowNotification("No Images Added", "info");
                            }

                          
                        }

                    },
                    error: function (data) {
                        GoTSkillZNotificationControls.ShowNotification("Could Not Get Peripheral Image Data, Please Contact Admin", "danger");
                    }
                });
        }



    }
};

var GoTSkillZSetupModelFunctions = {
    clearModalValues: function () {
        "use strict";

        $("#setup-name").val("");
        $("#regi-setup-type").data("kendoDropDownList").text("Select Setup Type");
        $("#setup-cpu-brand, #setup-cpu-des, #setup-cpu-affiliate").val("");
        $("#setup-mobo-brand, #setup-mobo-des, #setup-mobo-affiliate").val("");
        $("#setup-ram-brand, #setup-ram-des, #setup-ram-affiliate").val("");
        $("#setup-gpu-brand, #setup-gpu-des, #setup-gpu-affiliate").val("");
        $("#setup-psu-brand, #setup-psu-des, #setup-psu-affiliate").val("");
        $("#setup-ssd-brand, #setup-ssd-des, #setup-ssd-affiliate").val("");
        $("#setup-hdd-brand, #setup-hdd-des, #setup-hdd-affiliate").val("");
        $("#setup-case-brand, #setup-case-des, #setup-case-affiliate").val("");
        $("#setup-cooler-brand, #setup-cooler-des, #setup-cooler-affiliate").val("");
        $("#setup-fan-brand, #setup-fan-des, #setup-fan-affiliate").val("");
        $("#setup-capture-brand, #setup-capture-des, #setup-capture-affiliate").val("");
        $("#setup-sound-brand, #setup-sound-des, #setup-sound-affiliate").val("");
        $("#setup-monitor-brand, #setup-monitor-des, #setup-monitor-affiliate").val("");
        $("#setup-img").val("");
    },
    showUserSetupAddModal: function () {
        "use strict";

        GoTSkillZSetupInitializers.initializeSetupTypeDDL();
        GoTSkillZSetupInitializers.initializeSetupCompanyDDL();
        GoTSkillZSetupModelFunctions.clearModalValues();
        $("#user-setup-add-modal").find(".modal-body").css("overflow-y", "hidden");


        //initalize perfectScrollbar 
        var perfectScrollbar = new PerfectScrollbar(".setup-modal-body", {
            wheelPropagation: true,
            minScrollbarLength: 20
        });

        $("#save-setup, #delete-setup").attr("data-id", "0");
        $("#delete-setup").hide();
        $("#regi-setup-type").data("kendoDropDownList").enable(true);

        //initialize kendo fileUpload
        GoTSkillZSetupImageFunctions.initializeSetupImageUpload();
        $("#user-setup-add-modal").modal("show");
        GoTSkillZSetupBinders.bindSetupSaveBtn();

    },
    showUserSetupEditModal: function (setupId) {
        "use strict";

        GoTSkillZSetupInitializers.initializeSetupTypeDDL();
        GoTSkillZSetupModelFunctions.clearModalValues();
        $("#user-setup-add-modal").find(".modal-body").css("overflow-y", "hidden");

        //initalize perfectScrollbar 
        var perfectScrollbar = new PerfectScrollbar(".setup-modal-body", {
            wheelPropagation: true,
            minScrollbarLength: 20
        });

        GoTSkillZSetupInitializers.initializeSetupCompanyDDL();
        $("#regi-setup-type").data("kendoDropDownList").enable(false);
        GoTSkillZSetupModelFunctions.popluateSetupEditModal(setupId);
        $("#save-setup, #delete-setup").attr("data-id", setupId);

        //initialize kendo fileUpload and get setup image files
        GoTSkillZSetupImageFunctions.getSetupImagesForEditModel(setupId);


        $("#delete-setup").show();
        $("#user-setup-add-modal").modal("show");
        GoTSkillZSetupBinders.bindSetupRemoveBtn();
        GoTSkillZSetupBinders.bindSetupSaveBtn();
    },
    popluateSetupEditModal: function (setupId) {
        "use strict";

        if (setupId !== "") {
            var userSetup = _.filter(userObj.UserSetupData, function (x) { return x.SetupId === parseInt(setupId) });

            if (userSetup.length > 0) {

                var setupTypeName = "";

                var setupObj = _.chain(userObj.UserSetups).filter(function (x) {
                    return (x.Id === parseInt(setupId));
                }).value();

                if (setupObj !== null) {
                    var setupType = _.filter(setupTypes, function (x) {
                        return x.Id === setupObj[0].SetupTypeId;
                    });

                    setupTypeName = setupType[0].SetupType1;

                    $("#setup-name").val(setupObj[0].SetupName);
                }

                $("#regi-setup-type").data("kendoDropDownList").text(setupTypeName);


                $.each(userSetup, function (index, item) {


                    if (item.Component.trim().toLowerCase() === "cpu") {

                        $("#setup-cpu-brand").val(item.CompanyName).attr("component-id", item.Id);
                        $("#setup-cpu-des").val(item.ProductDetails);
                        $("#setup-cpu-affiliate").val(item.AffiliateLink);
                    }

                    if (item.Component.trim().toLowerCase() === "motherboard") {
                        $("#setup-mobo-brand").val(item.CompanyName).attr("component-id", item.Id);;
                        $("#setup-mobo-des").val(item.ProductDetails);
                        $("#setup-mobo-affiliate").val(item.AffiliateLink);
                    }

                    if (item.Component.trim().toLowerCase() === "ram") {
                        $("#setup-ram-brand").val(item.CompanyName).attr("component-id", item.Id);;
                        $("#setup-ram-des").val(item.ProductDetails);
                        $("#setup-ram-affiliate").val(item.AffiliateLink);
                    }

                    if (item.Component.trim().toLowerCase() === "gpu") {
                        $("#setup-gpu-brand").val(item.CompanyName).attr("component-id", item.Id);;
                        $("#setup-gpu-des").val(item.ProductDetails);
                        $("#setup-gpu-affiliate").val(item.AffiliateLink);
                    }

                    if (item.Component.trim().toLowerCase() === "psu") {
                        $("#setup-psu-brand").val(item.CompanyName).attr("component-id", item.Id);;
                        $("#setup-psu-des").val(item.ProductDetails);
                        $("#setup-psu-affiliate").val(item.AffiliateLink);
                    }

                    if (item.Component.trim().toLowerCase() === "cpu cooler") {
                        $("#setup-cooler-brand").val(item.CompanyName).attr("component-id", item.Id);;
                        $("#setup-cooler-des").val(item.ProductDetails);
                        $("#setup-cooler-affiliate").val(item.AffiliateLink);
                    }

                    if (item.Component.trim().toLowerCase() === "case") {
                        $("#setup-case-brand").val(item.CompanyName).attr("component-id", item.Id);;
                        $("#setup-case-des").val(item.ProductDetails);
                        $("#setup-case-affiliate").val(item.AffiliateLink);
                    }

                    if (item.Component.trim().toLowerCase() === "ssd") {
                        $("#setup-ssd-brand").val(item.CompanyName).attr("component-id", item.Id);;
                        $("#setup-ssd-des").val(item.ProductDetails);
                        $("#setup-ssd-affiliate").val(item.AffiliateLink);
                    }

                    if (item.Component.trim().toLowerCase() === "hdd") {
                        $("#setup-hdd-brand").val(item.CompanyName).attr("component-id", item.Id);;
                        $("#setup-hdd-des").val(item.ProductDetails);
                        $("#setup-hdd-affiliate").val(item.AffiliateLink);
                    }

                    if (item.Component.trim().toLowerCase() === "fan") {
                        $("#setup-fan-brand").val(item.CompanyName).attr("component-id", item.Id);;
                        $("#setup-fan-des").val(item.ProductDetails);
                        $("#setup-fan-affiliate").val(item.AffiliateLink);
                    }

                    if (item.Component.trim().toLowerCase() === "fan") {
                        $("#setup-fan-brand").val(item.CompanyName).attr("component-id", item.Id);;
                        $("#setup-fan-des").val(item.ProductDetails);
                        $("#setup-fan-affiliate").val(item.AffiliateLink);
                    }


                    if (item.Component.trim().toLowerCase() === "capture card") {
                        $("#setup-capture-brand").val(item.CompanyName).attr("component-id", item.Id);;
                        $("#setup-capture-des").val(item.ProductDetails);
                        $("#setup-capture-affiliate").val(item.AffiliateLink);
                    }

                    if (item.Component.trim().toLowerCase() === "sound card") {
                        $("#setup-sound-brand").val(item.CompanyName).attr("component-id", item.Id);;
                        $("#setup-sound-des").val(item.ProductDetails);
                        $("#setup-sound-affiliate").val(item.AffiliateLink);
                    }

                });
            }
        }

    }
};



var GoTSkillZSetupImageFunctions = {
    initializeSetupImageUpload: function () {
        "use strict";

        // kendo file upload
        if ($("#setup-img").data("kendoUpload") === undefined) {
            $("#setup-img").kendoUpload({
                "multiple": true,
                async: {
                    autoUpload: false,
                    saveUrl: fileAPI + "UploadSetupImages",
                    removeUrl: fileAPI + "RemoveSetupImages"
                },
                upload: function (e) {
                    e.data = {
                        userId: userObj.UserId,
                        setupId: imageSetupId
                    };
                },
                remove: function (e) {
                    e.data = {
                        userId: userObj.UserId,
                        setupId: imageSetupId
                    };
                },
                validation: {
                    allowedExtensions: [".jpg", ".png", ".jpeg"]
                }
            });
        }


    },
    getSetupImagesForEditModel: function (setupId) {
        "use strict";
        var initialFile = [];
        $.ajax({
            url: fileAPI + "GetSetupImages/" + userObj.UserId + "/" + setupId,
            type: "GET",
            dataType: "json",
            //global: false,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data) {
                    initialFile = data;
                }
            },
            complete: function (data) {
                // kendo file upload
                if ($("#setup-img").data("kendoUpload") === undefined) {
                    $("#setup-img").kendoUpload({
                        "multiple": true,
                        async: {
                            autoUpload: false,
                            saveUrl: fileAPI + "UploadSetupImages",
                            removeUrl: fileAPI + "RemoveSetupImages"
                        },
                        upload: function (e) {
                              GoTSkillZUIFunctions.ShowLoader("Uploading Images, Please Wait...");
                            e.data = {
                                userId: userObj.UserId,
                                setupId: setupId
                            };
                        },
                        remove: function (e) {
                            e.data = {
                                userId: userObj.UserId,
                                setupId: setupId
                            };
                        },
                        validation: {
                            allowedExtensions: [".jpg", ".png", ".jpeg"]
                        },
                        files: initialFile
                    });
                }
            },
            error: function (data) {
                GoTSkillZNotificationControls.ShowNotification("Could Not Get Setup Image Data, Please Contact Admin", "danger");
            }
        });

    },
    initializePeripheralImageUpload: function () {
        "use strict";

        // kendo file upload
        if ($("#peripheral-img").data("kendoUpload") === undefined) {
            $("#peripheral-img").kendoUpload({
                "multiple": true,
                async: {
                    autoUpload: false,
                    saveUrl: fileAPI + "UploadPeripheralImages",
                    removeUrl: fileAPI + "RemovePeripheralImages"
                },
                upload: function (e) {
                    GoTSkillZUIFunctions.ShowLoader("Uploading Images, Please Wait...");
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
                }
            });
        }


    },
    getPeriheralImagesForEditModel: function (setupId) {
        "use strict";
        var initialFile = [];
        $.ajax({
            url: fileAPI + "GetPeripheralImages/" + userObj.UserId,
            type: "GET",
            dataType: "json",
            //global: false,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data) {
                    initialFile = data;
                }
            },
            complete: function (data) {
                // kendo file upload
                if ($("#peripheral-img").data("kendoUpload") === undefined) {
                    $("#peripheral-img").kendoUpload({
                        "multiple": true,
                        async: {
                            autoUpload: false,
                            saveUrl: fileAPI + "UploadPeripheralImages",
                            removeUrl: fileAPI + "RemovePeripheralImages"
                        },
                        upload: function (e) {
                            GoTSkillZUIFunctions.ShowLoader("Uploading Images, Please Wait...");
                            e.data = {
                                userId: userObj.UserId
                            };
                        },
                        remove: function(e) {
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
                 
                    var uploadWidget = $("#peripheral-img").getKendoUpload();
                    // You won't need to clear the files as the Upload DOM is entirely removed
                    // uploadWidget.clearAllFiles();
                    var uploaderOptions = uploadWidget.options;
                    uploaderOptions.files = [];

                    uploadWidget.destroy();

                    // Get reference to the 'files' <input> element and its .k-upload parent
                    var uploadInput = $("#peripheral-img");
                    var wrapper = uploadInput.parents('.k-upload');
                    // Remove the .k-upload from the DOM
                    wrapper.remove();
                    // Re-append the 'files' <input> to the DOM
                   
                   $('#peripheral-img-container').append(uploadInput);
                    $("#peripheral-img").kendoUpload({
                        "multiple": true,
                        async: {
                            autoUpload: false,
                            saveUrl: fileAPI + "UploadPeripheralImages",
                            removeUrl: fileAPI + "RemovePeripheralImages"
                        },
                        upload: function (e) {

                            GoTSkillZUIFunctions.ShowLoader("Uploading Images, Please Wait...");
                            e.data = {
                                userId: userObj.UserId
                            };
                        },
                        remove: function (e) {
                            e.data = {
                                userId: userObj.UserId
                            };
                        },
                        complete:function(e) {
                            setTimeout(function () {
                                window.location.reload();
                            }, 500);
                        },
                        validation: {
                            allowedExtensions: [".jpg", ".png", ".jpeg"]
                        },
                        files: initialFile
                    });
                          
                }
            },
            error: function (data) {
                GoTSkillZNotificationControls.ShowNotification("Could Not Get Peripheral Image Data, Please Contact Admin", "danger");
            }
        });

    }
};


var GoTSkillZSetupSaveFunctions = {
    saveSetupData: function (e) {
        "use strict";

        var userId = userObj.UserId;

        var dataId = e.currentTarget.getAttribute("data-id");
        var newUserSetups = [];
        var editUserSetup = [];
        if (dataId === "0") { //add

            if ($("#regi-setup-type").val() === "-1") {
                GoTSkillZNotificationControls.ShowNotification("Please Select Setup Type!", "danger");
                return;
            }
            var setupType = $("#regi-setup-type").data("kendoDropDownList").text();

            //cpu
            var newCpuSetupData = new GoTSkillzEntities.UserSetupDataDTO();
            newCpuSetupData.SetupName = $("#setup-name").val();
            newCpuSetupData.SetupTypeId = $("#regi-setup-type").val();
            newCpuSetupData.SetupTypeName = setupType;
            newCpuSetupData.CompanyName = $("#setup-cpu-brand").val();
            newCpuSetupData.Component = "CPU";
            newCpuSetupData.ProductDetails = $("#setup-cpu-des").val();
            newCpuSetupData.AffiliateLink = $("#setup-cpu-affiliate").val();
            newCpuSetupData.UserId = userId;
            newUserSetups.push(newCpuSetupData);

            //mobo
            var newMoboSetupData = new GoTSkillzEntities.UserSetupDataDTO();
            newMoboSetupData.SetupName = $("#setup-name").val();
            newMoboSetupData.SetupTypeId = $("#regi-setup-type").val();
            newMoboSetupData.SetupTypeName = setupType;
            newMoboSetupData.CompanyName = $("#setup-mobo-brand").val();
            newMoboSetupData.Component = "Motherboard";
            newMoboSetupData.ProductDetails = $("#setup-mobo-des").val();
            newMoboSetupData.AffiliateLink = $("#setup-mobo-affiliate").val();
            newMoboSetupData.UserId = userId;
            newUserSetups.push(newMoboSetupData);

            //ram
            var newRamSetupData = new GoTSkillzEntities.UserSetupDataDTO();
            newRamSetupData.SetupName = $("#setup-name").val();
            newRamSetupData.SetupTypeId = $("#regi-setup-type").val();
            newRamSetupData.SetupTypeName = setupType;
            newRamSetupData.CompanyName = $("#setup-ram-brand").val();
            newRamSetupData.Component = "Ram";
            newRamSetupData.ProductDetails = $("#setup-ram-des").val();
            newRamSetupData.AffiliateLink = $("#setup-ram-affiliate").val();
            newRamSetupData.UserId = userId;
            newUserSetups.push(newRamSetupData);


            //gpu
            var newGpuSetupData = new GoTSkillzEntities.UserSetupDataDTO();
            newGpuSetupData.SetupName = $("#setup-name").val();
            newGpuSetupData.SetupTypeId = $("#regi-setup-type").val();
            newGpuSetupData.SetupTypeName = setupType;
            newGpuSetupData.CompanyName = $("#setup-gpu-brand").val();
            newGpuSetupData.Component = "GPU";
            newGpuSetupData.ProductDetails = $("#setup-gpu-des").val();
            newGpuSetupData.AffiliateLink = $("#setup-gpu-affiliate").val();
            newGpuSetupData.UserId = userId;
            newUserSetups.push(newGpuSetupData);


            //psu
            var newPsuSetupData = new GoTSkillzEntities.UserSetupDataDTO();
            newPsuSetupData.SetupName = $("#setup-name").val();
            newPsuSetupData.SetupTypeId = $("#regi-setup-type").val();
            newPsuSetupData.SetupTypeName = setupType;
            newPsuSetupData.CompanyName = $("#setup-psu-brand").val();
            newPsuSetupData.Component = "PSU";
            newPsuSetupData.ProductDetails = $("#setup-psu-des").val();
            newPsuSetupData.AffiliateLink = $("#setup-psu-affiliate").val();
            newUserSetups.push(newPsuSetupData);

            //cooler
            var newCoolerSetupData = new GoTSkillzEntities.UserSetupDataDTO();
            newCoolerSetupData.SetupName = $("#setup-name").val();
            newCoolerSetupData.SetupTypeId = $("#regi-setup-type").val();
            newCoolerSetupData.SetupTypeName = setupType;
            newCoolerSetupData.CompanyName = $("#setup-cooler-brand").val();
            newCoolerSetupData.Component = "CPU Cooler";
            newCoolerSetupData.ProductDetails = $("#setup-cooler-des").val();
            newCoolerSetupData.AffiliateLink = $("#setup-cooler-affiliate").val();
            newCoolerSetupData.UserId = userId;
            newUserSetups.push(newCoolerSetupData);

            //case
            var newCaseSetupData = new GoTSkillzEntities.UserSetupDataDTO();
            newCaseSetupData.SetupName = $("#setup-name").val();
            newCaseSetupData.SetupTypeId = $("#regi-setup-type").val();
            newCaseSetupData.SetupTypeName = setupType;
            newCaseSetupData.CompanyName = $("#setup-case-brand").val();
            newCaseSetupData.Component = "Case";
            newCaseSetupData.ProductDetails = $("#setup-case-des").val();
            newCaseSetupData.AffiliateLink = $("#setup-case-affiliate").val();
            newCaseSetupData.UserId = userId;
            newUserSetups.push(newCaseSetupData);

            //ssd
            var newSsdSetupData = new GoTSkillzEntities.UserSetupDataDTO();
            newSsdSetupData.SetupName = $("#setup-name").val();
            newSsdSetupData.SetupTypeId = $("#regi-setup-type").val();
            newSsdSetupData.SetupTypeName = setupType;
            newSsdSetupData.CompanyName = $("#setup-ssd-brand").val();
            newSsdSetupData.Component = "SSD";
            newSsdSetupData.ProductDetails = $("#setup-ssd-des").val();
            newSsdSetupData.AffiliateLink = $("#setup-ssd-affiliate").val();
            newSsdSetupData.UserId = userId;
            newUserSetups.push(newSsdSetupData);

            //hdd
            var newHddSetupData = new GoTSkillzEntities.UserSetupDataDTO();
            newHddSetupData.SetupName = $("#setup-name").val();
            newHddSetupData.SetupTypeId = $("#regi-setup-type").val();
            newHddSetupData.SetupTypeName = setupType;
            newHddSetupData.CompanyName = $("#setup-hdd-brand").val();
            newHddSetupData.Component = "HDD";
            newHddSetupData.ProductDetails = $("#setup-hdd-des").val();
            newHddSetupData.AffiliateLink = $("#setup-hdd-affiliate").val();
            newHddSetupData.UserId = userId;
            newUserSetups.push(newHddSetupData);


            //fan
            var newFanSetupData = new GoTSkillzEntities.UserSetupDataDTO();
            newFanSetupData.SetupName = $("#setup-name").val();
            newFanSetupData.SetupTypeId = $("#regi-setup-type").val();
            newFanSetupData.SetupTypeName = setupType;
            newFanSetupData.CompanyName = $("#setup-fan-brand").val();
            newFanSetupData.Component = "Fan";
            newFanSetupData.ProductDetails = $("#setup-fan-des").val();
            newFanSetupData.AffiliateLink = $("#setup-fan-affiliate").val();
            newFanSetupData.UserId = userId;
            newUserSetups.push(newFanSetupData);

            //capture
            var newcaptureSetupData = new GoTSkillzEntities.UserSetupDataDTO();
            newcaptureSetupData.SetupName = $("#setup-name").val();
            newcaptureSetupData.SetupTypeId = $("#regi-setup-type").val();
            newcaptureSetupData.SetupTypeName = setupType;
            newcaptureSetupData.CompanyName = $("#setup-capture-brand").val();
            newcaptureSetupData.Component = "Capture Card";
            newcaptureSetupData.ProductDetails = $("#setup-capture-des").val();
            newcaptureSetupData.AffiliateLink = $("#setup-capture-affiliate").val();
            newcaptureSetupData.UserId = userId;
            newUserSetups.push(newcaptureSetupData);

            //sound
            var newSoundSetupData = new GoTSkillzEntities.UserSetupDataDTO();
            newSoundSetupData.SetupName = $("#setup-name").val();
            newSoundSetupData.SetupTypeId = $("#regi-setup-type").val();
            newSoundSetupData.SetupTypeName = setupType;
            newSoundSetupData.CompanyName = $("#setup-sound-brand").val();
            newSoundSetupData.Component = "Sound Card";
            newSoundSetupData.ProductDetails = $("#setup-sound-des").val();
            newSoundSetupData.AffiliateLink = $("#setup-sound-affiliate").val();
            newSoundSetupData.UserId = userId;
            newUserSetups.push(newSoundSetupData);

            if (newUserSetups.length > 0) {
                $.ajax({
                    url: membershipAPI + "AddNewUserSetup",
                    type: "POST",
                    dataType: "json",
                    //global: false,
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify(newUserSetups),
                    success: function (data) {
                        if (data !== "failed") {

                            imageSetupId = data;
                            $("#setup-img").data("kendoUpload").upload();
                            $("#user-setup-add-modal").modal("hide");
                            GoTSkillZProfileMetaDataFunctions.getUserProfileMetaData();
                        }
                    },
                    complete: function () {
                        GoTSkillZNotificationControls.ShowNotification("Setup Saved Successful!", "success");
                    },
                    error: function (data) {
                        GoTSkillZNotificationControls.ShowNotification("Could Not Save Setup Data, Please Contact Admin", "danger");
                    }
                });

            } else {
                GoTSkillZNotificationControls.ShowNotification("Could Not Save Setup Data, Please Contact Admin", "danger");
            }
        } else {

            var setupTypeEdit = $("#regi-setup-type").data("kendoDropDownList").text();


           

            //cpu
            var editCpuSetupData = new GoTSkillzEntities.UserSetupDataDTO();
            editCpuSetupData.SetupName = $("#setup-name").val();
            editCpuSetupData.Id = $("#setup-cpu-brand")[0].getAttribute("component-id");
            editCpuSetupData.SetupId = dataId;
            editCpuSetupData.SetupTypeId = $("#regi-setup-type").val();
            editCpuSetupData.SetupTypeName = setupTypeEdit;
            editCpuSetupData.CompanyName = $("#setup-cpu-brand").val();
            editCpuSetupData.Component = "CPU";
            editCpuSetupData.ProductDetails = $("#setup-cpu-des").val();
            editCpuSetupData.AffiliateLink = $("#setup-cpu-affiliate").val();
            editCpuSetupData.UserId = userId;
            editUserSetup.push(editCpuSetupData);

            //mobo
            var editMoboSetupData = new GoTSkillzEntities.UserSetupDataDTO();
            editMoboSetupData.SetupName = $("#setup-name").val();
            editMoboSetupData.Id = $("#setup-mobo-brand")[0].getAttribute("component-id");
            editMoboSetupData.SetupId = dataId;
            editMoboSetupData.SetupTypeId = $("#regi-setup-type").val();
            editMoboSetupData.SetupTypeName = setupTypeEdit;
            editMoboSetupData.CompanyName = $("#setup-mobo-brand").val();
            editMoboSetupData.Component = "Motherboard";
            editMoboSetupData.ProductDetails = $("#setup-mobo-des").val();
            editMoboSetupData.AffiliateLink = $("#setup-mobo-affiliate").val();
            editMoboSetupData.UserId = userId;
            editUserSetup.push(editMoboSetupData);

            //ram
            var editRamSetupData = new GoTSkillzEntities.UserSetupDataDTO();
            editRamSetupData.SetupName = $("#setup-name").val();
            editRamSetupData.Id = $("#setup-ram-brand")[0].getAttribute("component-id");
            editRamSetupData.SetupId = dataId;
            editRamSetupData.SetupTypeId = $("#regi-setup-type").val();
            editRamSetupData.SetupTypeName = setupTypeEdit;
            editRamSetupData.CompanyName = $("#setup-ram-brand").val();
            editRamSetupData.Component = "Ram";
            editRamSetupData.ProductDetails = $("#setup-ram-des").val();
            editRamSetupData.AffiliateLink = $("#setup-ram-affiliate").val();
            editRamSetupData.UserId = userId;
            editUserSetup.push(editRamSetupData);


            //gpu
            var editGpuSetupData = new GoTSkillzEntities.UserSetupDataDTO();
            editGpuSetupData.SetupName = $("#setup-name").val();
            editGpuSetupData.Id = $("#setup-gpu-brand")[0].getAttribute("component-id");
            editGpuSetupData.SetupId = dataId;
            editGpuSetupData.SetupTypeId = $("#regi-setup-type").val();
            editGpuSetupData.SetupTypeName = setupTypeEdit;
            editGpuSetupData.CompanyName = $("#setup-gpu-brand").val();
            editGpuSetupData.Component = "GPU";
            editGpuSetupData.ProductDetails = $("#setup-gpu-des").val();
            editGpuSetupData.AffiliateLink = $("#setup-gpu-affiliate").val();
            editGpuSetupData.UserId = userId;
            editUserSetup.push(editGpuSetupData);


            //psu
            var editPsuSetupData = new GoTSkillzEntities.UserSetupDataDTO();
            editPsuSetupData.SetupName = $("#setup-name").val();
            editPsuSetupData.Id = $("#setup-psu-brand")[0].getAttribute("component-id");
            editPsuSetupData.SetupId = dataId;
            editPsuSetupData.SetupTypeId = $("#regi-setup-type").val();
            editPsuSetupData.SetupTypeName = setupTypeEdit;
            editPsuSetupData.CompanyName = $("#setup-psu-brand").val();
            editPsuSetupData.Component = "PSU";
            editPsuSetupData.ProductDetails = $("#setup-psu-des").val();
            editPsuSetupData.AffiliateLink = $("#setup-psu-affiliate").val();
            editUserSetup.push(editPsuSetupData);

            //cooler
            var editCoolerSetupData = new GoTSkillzEntities.UserSetupDataDTO();
            editCoolerSetupData.SetupName = $("#setup-name").val();
            editCoolerSetupData.Id = $("#setup-cooler-brand")[0].getAttribute("component-id");
            editCoolerSetupData.SetupId = dataId;
            editCoolerSetupData.SetupTypeId = $("#regi-setup-type").val();
            editCoolerSetupData.SetupTypeName = setupTypeEdit;
            editCoolerSetupData.CompanyName = $("#setup-cooler-brand").val();
            editCoolerSetupData.Component = "CPU Cooler";
            editCoolerSetupData.ProductDetails = $("#setup-cooler-des").val();
            editCoolerSetupData.AffiliateLink = $("#setup-cooler-affiliate").val();
            editCoolerSetupData.UserId = userId;
            editUserSetup.push(editCoolerSetupData);

            //case
            var editCaseSetupData = new GoTSkillzEntities.UserSetupDataDTO();
            editCaseSetupData.SetupName = $("#setup-name").val();
            editCaseSetupData.Id = $("#setup-case-brand")[0].getAttribute("component-id");
            editCaseSetupData.SetupId = dataId;
            editCaseSetupData.SetupTypeId = $("#regi-setup-type").val();
            editCaseSetupData.SetupTypeName = setupTypeEdit;
            editCaseSetupData.CompanyName = $("#setup-case-brand").val();
            editCaseSetupData.Component = "Case";
            editCaseSetupData.ProductDetails = $("#setup-case-des").val();
            editCaseSetupData.AffiliateLink = $("#setup-case-affiliate").val();
            editCaseSetupData.UserId = userId;
            editUserSetup.push(editCaseSetupData);

            //ssd
            var editSsdSetupData = new GoTSkillzEntities.UserSetupDataDTO();
            editSsdSetupData.SetupName = $("#setup-name").val();
            editSsdSetupData.Id = $("#setup-ssd-brand")[0].getAttribute("component-id");
            editSsdSetupData.SetupId = dataId;
            editSsdSetupData.SetupTypeId = $("#regi-setup-type").val();
            editSsdSetupData.SetupTypeName = setupTypeEdit;
            editSsdSetupData.CompanyName = $("#setup-ssd-brand").val();
            editSsdSetupData.Component = "SSD";
            editSsdSetupData.ProductDetails = $("#setup-ssd-des").val();
            editSsdSetupData.AffiliateLink = $("#setup-ssd-affiliate").val();
            editSsdSetupData.UserId = userId;
            editUserSetup.push(editSsdSetupData);

            //hdd
            var editHddSetupData = new GoTSkillzEntities.UserSetupDataDTO();
            editHddSetupData.SetupName = $("#setup-name").val();
            editHddSetupData.Id = $("#setup-hdd-brand")[0].getAttribute("component-id");
            editHddSetupData.SetupId = dataId;
            editHddSetupData.SetupTypeId = $("#regi-setup-type").val();
            editHddSetupData.SetupTypeName = setupTypeEdit;
            editHddSetupData.CompanyName = $("#setup-hdd-brand").val();
            editHddSetupData.Component = "HDD";
            editHddSetupData.ProductDetails = $("#setup-hdd-des").val();
            editHddSetupData.AffiliateLink = $("#setup-hdd-affiliate").val();
            editHddSetupData.UserId = userId;
            editUserSetup.push(editHddSetupData);


            //fan
            var editFanSetupData = new GoTSkillzEntities.UserSetupDataDTO();
            editFanSetupData.SetupName = $("#setup-name").val();
            editFanSetupData.Id = $("#setup-fan-brand")[0].getAttribute("component-id");
            editFanSetupData.SetupId = dataId;
            editFanSetupData.SetupTypeId = $("#regi-setup-type").val();
            editFanSetupData.SetupTypeName = setupTypeEdit;
            editFanSetupData.CompanyName = $("#setup-fan-brand").val();
            editFanSetupData.Component = "Fan";
            editFanSetupData.ProductDetails = $("#setup-fan-des").val();
            editFanSetupData.AffiliateLink = $("#setup-fan-affiliate").val();
            editFanSetupData.UserId = userId;
            editUserSetup.push(editFanSetupData);

            //capture
            var editcaptureSetupData = new GoTSkillzEntities.UserSetupDataDTO();
            editcaptureSetupData.SetupName = $("#setup-name").val();
            editcaptureSetupData.Id = $("#setup-capture-brand")[0].getAttribute("component-id");
            editcaptureSetupData.SetupId = dataId;
            editcaptureSetupData.SetupTypeId = $("#regi-setup-type").val();
            editcaptureSetupData.SetupTypeName = setupTypeEdit;
            editcaptureSetupData.CompanyName = $("#setup-capture-brand").val();
            editcaptureSetupData.Component = "Capture Card";
            editcaptureSetupData.ProductDetails = $("#setup-capture-des").val();
            editcaptureSetupData.AffiliateLink = $("#setup-capture-affiliate").val();
            editcaptureSetupData.UserId = userId;
            editUserSetup.push(editcaptureSetupData);

            //sound
            var editSoundSetupData = new GoTSkillzEntities.UserSetupDataDTO();
            editSoundSetupData.SetupName = $("#setup-name").val();
            editSoundSetupData.Id = $("#setup-sound-brand")[0].getAttribute("component-id");
            editSoundSetupData.SetupId = dataId;
            editSoundSetupData.SetupTypeId = $("#regi-setup-type").val();
            editSoundSetupData.SetupTypeName = setupTypeEdit;
            editSoundSetupData.CompanyName = $("#setup-sound-brand").val();
            editSoundSetupData.Component = "Sound Card";
            editSoundSetupData.ProductDetails = $("#setup-sound-des").val();
            editSoundSetupData.AffiliateLink = $("#setup-sound-affiliate").val();
            editSoundSetupData.UserId = userId;
            editUserSetup.push(editSoundSetupData);


            if (editUserSetup.length > 0) {
                $.ajax({
                    url: membershipAPI + "UpdateUserSetup",
                    type: "POST",
                    dataType: "json",
                    //global: false,
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify(editUserSetup),
                    success: function (data) {
                        if (data === "success") {

                            imageSetupId = dataId;

                            $("#setup-img").data("kendoUpload").upload();

                         
                            GoTSkillZProfileMetaDataFunctions.getUserProfileMetaData();
                        }


                    },
                    complete: function () {
                        $("#user-setup-add-modal").modal("hide");
                        GoTSkillZNotificationControls.ShowNotification("Setup Saved Successful!", "success");
                    },
                    error: function (data) {
                        GoTSkillZNotificationControls.ShowNotification("Could Not Save Setup Data, Please Contact Admin", "danger");
                    }
                });

            } else {
                GoTSkillZNotificationControls.ShowNotification("Could Not Save Setup Data, Please Contact Admin", "danger");
            }

        }


    }
};


var GoTSkillZPeripheralSaveFunctions = {
    savePeripheralData: function (e) {
        "use strict";

        if ($("#regi-peripheral-container").find(".peripheral-item").length > 0) {

            var userPeripheralList = [];
            $("#regi-peripheral-container").find(".peripheral-item").each(function (index, item) {

                var newPeripheralItem = new GoTSkillzEntities.userPeripheralDataDTO();
                var dataId = this.getAttribute("data-id");

                if (dataId !== null) {
                    newPeripheralItem.Id = dataId;
                } else {
                    newPeripheralItem.Id = 0;
                }

                if ($($(this).find(".regi-peripheral-type")[1]).data("kendoDropDownList").text() !== "Select Peripheral") {
                    newPeripheralItem.PeripheralType = $($(this).find(".regi-peripheral-type")[1]).data("kendoDropDownList").text();
                }

                if ($($(this).find(".regi-peripheral-company")[2]).val() !== "") {
                    newPeripheralItem.CompanyName = $($(this).find(".regi-peripheral-company")[1]).val();
                }


                newPeripheralItem.ProductDetails = $(this).find(".regi-peripheral-description").val();
                newPeripheralItem.AffiliateLink = $(this).find(".regi-peripheral-affiliate").val();

                userPeripheralList.push(newPeripheralItem);
            });


            userObj.UserPeripheralData = userPeripheralList;
        }


        //include remove peripherals array
        userObj.RemovePeripherals = removedPeripherals;


        $.ajax({
            url: membershipAPI + "SaveUserProfileData",
            type: "POST",
            dataType: "json",
            //global: true,
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(userObj),
            success: function (data) {
                if (data === "success") {
                    if ($("#peripheral-img").data("kendoUpload").getFiles().length > 0) {
                        $("#peripheral-img").data("kendoUpload").upload();
                    } else {
                        setTimeout(function () {
                            window.location.reload();
                        }, 500);
                    }
                }
            },
            complete: function () {
                  $("#user-peripheral-add-modal").modal("hide");
                GoTSkillZNotificationControls.ShowNotification("Peripheral Data Saved Successful!", "success");
            },
            error: function (data) {
                GoTSkillZNotificationControls.ShowNotification("Could Not Save Peripheral Data, Please Contact Admin", "danger");
            }
        });

    }
};


var GoTSkillZSetupRemoveFunctions = {
    removeSetup: function (e) {
        "use strict";

        var setupId = e.currentTarget.getAttribute("data-id");

        if (setupId !== "0") {
            $.ajax({
                url: membershipAPI + "RemoveSetup/" + setupId,
                type: "GET",
                dataType: "json",
                //global: false,
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    if (data === "success") {
                        setTimeout(function () {
                            window.location.reload();
                        }, 500);
                    }
                     
                },
                complete: function () {
                    
                    GoTSkillZNotificationControls.ShowNotification("Setup Deleted Successful!", "success");
                },
                error: function (data) {
                    GoTSkillZNotificationControls.ShowNotification("Could Not Delete Setup Data, Please Contact Admin", "danger");
                }
            });

        } else {
            GoTSkillZNotificationControls.ShowNotification("Could Not Delete Setup Data, Please Contact Admin", "danger");
        }
    }
};
var GoTSkillZSetupBinders = {
    bindAddFirstSetupBtn: function () {
        "use strict";

        $("#add-first-setup").unbind("click").bind("click", function (e) {

            e.preventDefault();
            e.stopImmediatePropagation();

            GoTSkillZSetupModelFunctions.showUserSetupAddModal(false, true, null);
        });
    },
    bindPeripheralAddBtn: function () {
        "use strict";

        $("#add-first-peripheral").unbind("click").bind("click", function (e) {

            e.preventDefault();
            e.stopImmediatePropagation();

            GoTSkillZPeripheralFormFunctions.buildUserPeripheralsForm(true, true, null);
        });
    },
    bindAddPeriheralItemBtn: function () {
        "use strict";

        $("#add-peripheral").unbind("click").bind("click", function (e) {

            e.preventDefault();
            e.stopImmediatePropagation();

            GoTSkillZPeripheralFormFunctions.buildUserPeripheralsForm(false, true, null);
        });
    },
    bindPeripheralRemoveBtn: function () {
        "use strict";

        $(".remove-peripheral").unbind("click").bind("click", function (e) {

            e.preventDefault();
            e.stopImmediatePropagation();

            GoTSkillZPeripheralFormFunctions.removeUserPeripheralsFormDiv(e);
        });

    },
    bindPeripheralSaveBtn: function () {
        "use strict";
        $("#save-peripheral").unbind("click").bind("click", function (e) {
            GoTSkillZPeripheralSaveFunctions.savePeripheralData(e);
        });
    },
    bindSetupSaveBtn: function () {
        "use strict";
        $("#save-setup").unbind("click").bind("click", function (e) {
            GoTSkillZSetupSaveFunctions.saveSetupData(e);
        });
    },
    bindSetupRemoveBtn: function () {
        "use strict";
        $("#delete-setup").unbind("click").bind("click", function (e) {
            GoTSkillZSetupRemoveFunctions.removeSetup(e);
        });
    },
    bindLightGallery: function () {
        "use strict";
        $(".lightbox").unbind("click").bind("click", function (e) {
            e.preventDefault();
            e.stopImmediatePropagation();

            GoTSkillZSetupDisplayFunctions.initializeSetupLightGallery(e);

        });
    }
};




var GoTSkillZSetupInitializers = {
    initializeSetupFunctions: function () {
        "use strict";
        GoTSkillZSetupDataFunctions.getSetupTypes();
        GoTSkillZSetupDataFunctions.getSetupOptions();
        GoTSkillZSetupDataFunctions.getSetupCompanies();

        GoTSkillZSetupBinders.bindAddFirstSetupBtn();
        GoTSkillZSetupBinders.bindPeripheralAddBtn();
    },
    initializeSetupTypeDDL: function () {
        "use strict";

        var filteredArray = _.chain(setupTypes).filter(function (x) {
            return (x.Id !== 6 && x.Id !== 9);
        }).value();

        var setupTypeDataSource = _.map(filteredArray, function (value) {
            return { id: value.Id, text: value.SetupType1 };
        });

        setupTypeDataSource.unshift({
            'id': "-1",
            'text': "Select Setup Type"
        });

        $("#regi-setup-type").kendoDropDownList({
            dataTextField: "text",
            dataValueField: "id",
            dataSource: setupTypeDataSource
        });

    },
    initializePeripheralOptionDDL: function (peripheralinputId) {
        "use strict";
        var filteredArray = _.chain(setupOptions).filter(function (x) {
            return (x.SetupTypeId !== 1);
        }).value();

        var peripheralOptionDataSource = _.map(filteredArray, function (value) {
            return { id: value.Id, text: value.SetupOption1 };
        });

        peripheralOptionDataSource.unshift({
            'id': "-1",
            'text': "Select Peripheral"
        });


        if (peripheralinputId !== "") {
            $("#" + peripheralinputId).each(function () {
                if ($(this).find("input").data("kendoDropDownList") === undefined) {
                    $(this).kendoDropDownList({
                        dataTextField: "text",
                        dataValueField: "id",
                        dataSource: peripheralOptionDataSource
                    });
                }
            });
        }

    },
    initializeSetupCompanyDDL: function () {
        "use strict";

        var setupCompanyDataSource = _.map(setupCompanies, function (value) {
            return { id: value.Id, text: value.CompanyName };
        });

        if ($($(".setup-brand")[0]).attr("class").indexOf("k-autocomplete") === -1) {
            $(".setup-brand").kendoAutoComplete({
                dataValueField: "id",
                dataTextField: "text",
                template: '<span data-recordid="#= id #"> #= text #</span>',
                filter: "contains",
                minLength: 0,
                placeholder: "Search Brand, Eg: Asus",
                dataSource: setupCompanyDataSource
            });
        }
    },
    initializePeripheralCompanyDDL: function () {
        "use strict";

        var setupCompanyDataSource = _.map(setupCompanies, function (value) {
            return { id: value.Id, text: value.CompanyName };
        });


        $(".regi-peripheral-company").each(function () {

            if ($(this).data("kendoAutoComplete") === undefined) {
                $(this).kendoAutoComplete({
                    dataValueField: "id",
                    dataTextField: "text",
                    template: '<span data-recordid="#= id #"> #= text #</span>',
                    filter: "contains",
                    minLength: 0,
                    placeholder: "Search Brand, Eg: Asus",
                    dataSource: setupCompanyDataSource
                });
            }

        });
    }
};


$(window).on("load", function () {
    "use strict";
    GoTSkillZSetupInitializers.initializeSetupFunctions();
});