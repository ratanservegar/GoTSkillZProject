var gameDataApi = "/WCF/GameDataAPI.svc/";

var winMultipliers = { 1: 0.03, 2: 0.06, 3: 0.25, 4: 0.5, 5: 0.75, 6: 1.0, 7: 1.5, 8: 2.0, 9: 2.5, 10: 3.0, 11: 3.5 };


var GoTSkillZMouseCalFunctions = {
    calcSens: function (edpi, dpi, win, raw) { // sensi calculation function
        var sens = 0, winMultiplier = 1.0;

        if (!raw) {
            winMultiplier = winMultipliers[win];
        }

        sens = edpi / dpi / winMultiplier;
        return sens;
    },
    calcEdpi: function (dpi, sens, win, raw) {   // eDPI calculation function
        var edpi = 0,
            winMultiplier = 1.0;

        if (!raw) {
            winMultiplier = winMultipliers[win];
        }

        edpi = dpi * sens * winMultiplier;
        return edpi;
    },
    calcAndUpdateEdpi: function () {
        var dpi = $("#edpi-DPI").val();

        var sens = $("#edpi-sensi").val();


        var windowsSensi = $("#windows-slider").val();
        var isRawChecked = $("#edpi-raw").is(":checked");

        GoTSkillZMouseCalFunctions.updateEdpi(GoTSkillZMouseCalFunctions.calcEdpi(dpi, sens, windowsSensi, isRawChecked));
    },
    calcAndUpdateSens: function () {
        var dpi = $("#sensi-dpi").val();
        var edpi = $("#sensi-eDPI").val();

        GoTSkillZMouseCalFunctions.updateSens(GoTSkillZMouseCalFunctions.calcSens(edpi, dpi, 6, true));
    },
    updateEdpi: function (value) {
        if (value > 0) {
            $("#final-edpi").val(value);
        }
    },
    updateSens: function (value) {
        if (value > 0) {
            $("#final-sensi").val(value);
        }
    },
    bindInputChanges: function () {
        $("#edpi-raw").change(function () {

            if (this.checked) {
                $("#windows-slider").data("ionRangeSlider").options.disable = true;

            } else {
                $("#windows-slider").data("ionRangeSlider").options.disable = false;
            }

            $("#windows-slider").data("ionRangeSlider").update();
            GoTSkillZMouseCalFunctions.calcAndUpdateEdpi();
        });

        $("#edpi-DPI").keyup(function () { GoTSkillZMouseCalFunctions.calcAndUpdateEdpi(); });
        $("#edpi-sensi").keyup(function () { GoTSkillZMouseCalFunctions.calcAndUpdateEdpi(); });
        $("#windows-slider").change(function () { GoTSkillZMouseCalFunctions.calcAndUpdateEdpi(); });



        $("#sensi-dpi").keyup(function () { GoTSkillZMouseCalFunctions.calcAndUpdateSens(); });
        $("#sensi-eDPI").keyup(function () { GoTSkillZMouseCalFunctions.calcAndUpdateSens(); });

    }
}



var GoTSkillZMouseSensiSaveFunctions = {
    saveSensitivity: function () {
        "use strict";


        var isValid = GoTSkillZMouseSensiSaveFunctions.validateSensiSave();

        if (isValid) {
            var newSensiObj = new GoTSkillzEntities.CSGOSensitivityDTO();

            newSensiObj.UserId = parseInt($("#hdnUserId").val());
            newSensiObj.DPI = ($("#edpi-DPI").val() || $("#sensi-dpi").val());
            newSensiObj.Sensitivity = ($("#edpi-sensi").val() || $("f#inal-sensi").val());
            newSensiObj.eDPI = Math.round(($("#final-edpi").val() || $("#sensi-eDPI").val()));
            newSensiObj.RawInput = $("#edpi-raw").is(":checked");
            newSensiObj.WindowsSensitivity = $("#windows-slider").val();
            newSensiObj.MouseHz = $("#mouse-hz").val();


            $.ajax({
                url: gameDataApi + "SaveCSGOSensitivity",
                type: "POST",
                dataType: "json",
                //global: false,
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(newSensiObj),
                success: function (data) {
                    if (data.indexOf("Failed") > -1) {
                        GoTSkillZNotificationControls.ShowNotification("Could Not Save CS:GO Sensitivity, Please Contact Admin", "danger");
                    } else {
                        GoTSkillZNotificationControls.ShowNotification("CS:GO Sensitvity Configuration Saved Successful!", "success");
                    }
                },
                error: function () {
                    GoTSkillZNotificationControls.ShowNotification("Could Not Save CS:GO Sensitivity Data, Please Contact Admin", "danger");
                }
            });


        }




    },
    validateSensiSave: function () {
        "use strict";

        var returnCondition = true;


        if ($("#edpi-DPI").val() === "" && $("#sensi-dpi").val() === "") {
            GoTSkillZNotificationControls.ShowNotification("Please input DPI value", "danger");

            return returnCondition = false;
        }


        if ($("#sensi-eDPI").val() === "" && $("#edpi-sensi").val() === "") {
            GoTSkillZNotificationControls.ShowNotification("Please Input or calculate Sensivity", "danger");
            return returnCondition = false;
        }


        if ($("#final-edpi").val() === "" && $("#sensi-eDPI").val() === "") {
            GoTSkillZNotificationControls.ShowNotification("Please Input or calculate eDPI", "danger");
            return returnCondition = false;
        }

        if ($("#mouse-hz").val() === "") {
            GoTSkillZNotificationControls.ShowNotification("Please Input Mouse Hz (polling rate)", "danger");
            return returnCondition = false;
        }

        return returnCondition;
    }
}



var GoTSkillZSensitivityInitializer = {
    //mouse click tester
    intializeSensitivityCalFunctions: function () {
        $('#sensi-calculator').show();


        //init slider
        $("#windows-slider").ionRangeSlider({
            disable: true,
            min: 1,
            max: 11,
            from: 6
        });

        GoTSkillZMouseCalFunctions.bindInputChanges();


        $("#save-sensi-btn").unbind("click").bind("click", function (e) {
            e.preventDefault();
            e.stopImmediatePropagation();
            GoTSkillZMouseSensiSaveFunctions.saveSensitivity();
        });
    }
};



$(window).on("load", function () {
    "use strict";

    GoTSkillZGateKeeperFunctions.checkUserHasAccess(GoTSkillZSensitivityInitializer.intializeSensitivityCalFunctions);


});