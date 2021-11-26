
var gameDDLObj = [{ id: "CS:GO", text: "CS:GO" }, { id: "PUBG", text: "PUBG" }, { id: "Overwatch", text: "Overwatch" }, { id: "Apex Legends", text: "Apex Legends" }, { id: "Fortnite", text: "Fortnite" }, { id: "Quake 3", text: "Quake 3" }];


var games = {
    "Apex Legends": { degreesPerDot: 0.022, info: "Uses the Source game engine, so shares settings with other Source games." },
    "CS:GO": { degreesPerDot: 0.022, info: "Uses the Source game engine, so shares settings with other Source games." },
    "Fortnite": { degreesPerDot: 0.5715 },
    "Overwatch": { degreesPerDot: 0.0066 },
    "PUBG": { degreesPerDot: 2.22222 },
    "Quake 3": { degreesPerDot: 0.022 }
};
var GoTSKillZSensiConverterFunctions = {
    calculateSensi: function() {
        "use strict";

        var targetSens = 0;
        var sourceGame = $("#ddl-convert-from").val();
        var targetGame = $("#ddl-convert-to").val();
        var sourceDegreesPerDot = games[sourceGame].degreesPerDot;
        var targetDegreesPerDot = games[targetGame].degreesPerDot;
        var sourceDpi = $("#main-dpi").val();
        var sourceSens = $("#main-sensi").val();



        // Convert to linear sensitivity from actual sens
        if (sourceGame === "PUBG") {
            sourceSens = 0.00203 * Math.pow(10, sourceSens / 50); 
            sourceSens = sourceSens * $("#pubg-fov").val() / 80; 
        }



        // Adjust for the degreesPerDot-difference between the two games.
        targetSens = sourceSens * (sourceDegreesPerDot / targetDegreesPerDot);


        // Convert from linear sensitivity to actual sens
        if (targetGame === "PUBG") {
            targetSens = targetSens * 80 / $("#pubg-fov").val(); // Adjust for FOV
            targetSens = 50 * Math.log10(targetSens / 0.00203); // Transform from cfg sens to in-game
        }


        var in360 = 360 / (sourceDpi * sourceDegreesPerDot * sourceSens);
        var cm360 = in360 * 2.54;

        $("#final-sensi").val(GoTSkillZSensiConverterHelperFunctions.round(targetSens, 6));


        $("#cm-360").val(GoTSkillZSensiConverterHelperFunctions.round(cm360, 3));
        $("#inch-360").val(GoTSkillZSensiConverterHelperFunctions.round(in360, 3));
    }


};
var GoTSkillZSensiConverterHelperFunctions =
{
    ConvertFromDDL: function() {
        "use strict";

        $("#ddl-convert-from, #ddl-convert-to").kendoDropDownList({
            dataTextField: "text",
            dataValueField: "id",
            dataSource: gameDDLObj,
            select: GoTSkillZSensiConverterHelperFunctions.bindChange
        });

        GoTSkillZSensiConverterHelperFunctions.setDefault();
        GoTSKillZSensiConverterFunctions.calculateSensi();
    },
    bindChange: function(e) {
        "use strict";
        if (e.dataItem) {
            var dataItem = e.dataItem;

            if ($(e.sender.element)[0].id === "ddl-convert-to") {
                if (dataItem !== null) {
                    var gameText = dataItem.text;


                    $("#ddl-convert-to").data('kendoDropDownList').value(gameText);
                    if (gameText === "PUBG") {
                        $("#fov-div").show();
                    } else {
                        $("#fov-div").hide();
                    }
                }
            } else {
                $("#fov-div").hide();
            }

            GoTSKillZSensiConverterFunctions.calculateSensi();
        }
    },
    round: function(num, decimals) {
        "use strict";
        if (num === Infinity) {
            return "";
        }
        var rounding = Math.pow(10, decimals);
        return Math.round(num * rounding) / rounding;
    },
    setDefault: function () {
        "use strict";
        // Default settings
        $("#main-sensi").val("1.2");
        $("#main-dpi").val("800");
        $("#pubg-fov").val(103);
        $("#ddl-convert-from").data('kendoDropDownList').value("CS:GO");
        $("#ddl-convert-to").data('kendoDropDownList').value("PUBG");



        $("#main-sensi, #main-dpi, #pubg-fov ").on("input", function() {
            GoTSKillZSensiConverterFunctions.calculateSensi();
        });

    }
};
var GoTSkillZSensitivityConverterInitializer = {
    //mouse click tester
    intializeSensitivityConverterFunctions: function() {
        $("#sensi-converter").show();

        GoTSkillZSensiConverterHelperFunctions.ConvertFromDDL();
    }
};


$(window).on("load", function() {
    "use strict";

    GoTSkillZGateKeeperFunctions.checkUserHasAccess(GoTSkillZSensitivityConverterInitializer.intializeSensitivityConverterFunctions);


});