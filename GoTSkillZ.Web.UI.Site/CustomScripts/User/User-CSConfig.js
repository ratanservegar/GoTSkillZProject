var gameDataApi = "/WCF/GameDataAPI.svc/";
var fileAPI = "/WCF/FileUploadAPI.svc/";


var userId, userMainConfig, userAutoexecConfig, userPracConfig;
var mainConfigCodeMirror, sensiConfigCodeMirror, bindConfigCodeMirror, viewModelConfigCodeMirror, crosshairConfigCodeMirror, networkConfigCodeMirror, soundConfigCodeMirror, autoexecConfigCodeMirror, pracConfigCodeMirror, videoConfiguration, mouseSettings;

var sensitivityArray = [], bindsArray = [], viewModelArray = [], crosshairArray = [], networkArray = [], soundArray = [];

var GoTSkillZCSGOConfigFunctions = {
    getUserMainConfigFile: function () {
        "use strict";
        $.ajax({
            url: fileAPI + "GetCSGOMainConfigFile/" + userId,
            type: "GET",
            dataType: "json",
            cache: false,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data.length > 0) {
                    userMainConfig = data[0];
                }
            },
            complete: function () {
                GoTSkillZCSGOConfigFunctions.getUserCsgoMainConfig();

            },
            error: function () {
                GoTSkillZNotificationControls.ShowNotification("Could Not Get CSGO Config Data, Please Contact Admin", "danger");
            }
        });
    },
    getUserAutoexecConfigFile: function () {
        "use strict";
        $.ajax({
            url: fileAPI + "GetCSGOAutoexecConfigFile/" + userId,
            type: "GET",
            dataType: "json",
            cache: false,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data.length > 0) {
                    userAutoexecConfig = data[0];
                }
            },
            complete: function () {
                GoTSkillZCSGOConfigFunctions.getUserCsgoAutoexecConfig();

            },
            error: function () {
                GoTSkillZNotificationControls.ShowNotification("Could Not Get CSGO Autoexec Config Data, Please Contact Admin", "danger");
            }
        });
    },
    getUserPracConfigFile: function () {
        "use strict";
        $.ajax({
            url: fileAPI + "GetCSGOPracConfigFIle/" + userId,
            type: "GET",
            dataType: "json",
            cache: false,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data.length > 0) {
                    userPracConfig = data[0];
                }
            },
            complete: function () {
                GoTSkillZCSGOConfigFunctions.getUserCsgoPracConfig();

            },
            error: function () {
                GoTSkillZNotificationControls.ShowNotification("Could Not Get CSGO Prac Config Data, Please Contact Admin", "danger");
            }
        });
    },
    getUserCsgoMainConfig: function () {
        "use strict";
        $.ajax({
            url: gameDataApi + "GetUserCSGOMainConfig/" + userId,
            type: "GET",
            dataType: "json",
            cache: false,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                GoTSkillZCSGOConfigFunctions.initializeMainConfigCodemirror(data);
            },
            error: function () {
                GoTSkillZNotificationControls.ShowNotification("Could Not Get CSGO Config Data, Please Contact Admin", "danger");
            }
        });
    },
    getUserCsgoAutoexecConfig: function () {
        "use strict";
        $.ajax({
            url: gameDataApi + "GetUserCSGOAutoexecConfig/" + userId,
            type: "GET",
            dataType: "json",
            cache: false,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                GoTSkillZCSGOConfigFunctions.initializeAutoexecConfigCodemirror(data);
            },
            error: function () {
                GoTSkillZNotificationControls.ShowNotification("Could Not Get CSGO Autoexec Config Data, Please Contact Admin", "danger");
            }
        });
    },
    getUserCsgoPracConfig: function () {
        "use strict";
        $.ajax({
            url: gameDataApi + "GetUserCSGOPracConfig/" + userId,
            type: "GET",
            dataType: "json",
            cache: false,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                GoTSkillZCSGOConfigFunctions.initializePracConfigCodemirror(data);
            },
            error: function () {
                GoTSkillZNotificationControls.ShowNotification("Could Not Get CSGO Prac Config Data, Please Contact Admin", "danger");
            }
        });
    },
    initializeMainConfigCodemirror: function (data) {
        "use strict";


        var filteredArray = data.filter(function (el) {
            return el !== "";
        });


        mainConfigCodeMirror = CodeMirror(document.getElementById("main-config"), {
            lineNumbers: false,
            theme: "dracula",
            mode: "html",
            scrollbarStyle: "overlay",
            readOnly: "true",
            cursorBlinkRate: -1
        });


        if (filteredArray.length > 0) {
            $("#update-config-btn, #config-delete, #config-copy, #config-download").show();
            $("#add-config-btn").hide();

            if (userMainConfig !== undefined && userMainConfig !== null) {
                $("#config-name").html(userMainConfig.name);
            } else {
                $("#config-name").html("Config");
            }
            GoTSkillZCSGOConfigFunctions.addCodemirrorData(mainConfigCodeMirror, filteredArray);

            GoTSkillZCSGOConfigFunctions.initializeSensiCodemirror(filteredArray);
            GoTSkillZCSGOConfigFunctions.initializeCrosshairCodemirror(filteredArray);
            GoTSkillZCSGOConfigFunctions.initializeBindsCodemirror(filteredArray);
            GoTSkillZCSGOConfigFunctions.initializeViewModelCodemirror(filteredArray);
            GoTSkillZCSGOConfigFunctions.initializeNetworkCodemirror(filteredArray);
            GoTSkillZCSGOConfigFunctions.initializeSoundCodemirror(filteredArray);


        } else {
            $("#update-config-btn, #config-delete, #config-copy, #config-download").hide();
            $("#add-config-btn").show();

            data.push("No Config Added By User");
            GoTSkillZCSGOConfigFunctions.addCodemirrorData(mainConfigCodeMirror, data);
        }


        GoTSkillZCSGOConfigFunctions.getUserAutoexecConfigFile();
        GoTSkillZCSGOConfigFunctions.getUserPracConfigFile();
    },
    initializeAutoexecConfigCodemirror: function (data) {
        "use strict";

        var filteredArray = data.filter(function (el) {
            return el !== "";
        });

        autoexecConfigCodeMirror = CodeMirror(document.getElementById("autoexec-config"), {
            lineNumbers: false,
            theme: "dracula",
            mode: "html",
            scrollbarStyle: "overlay",
            readOnly: "true",
            cursorBlinkRate: -1
        });


        if (filteredArray.length > 0) {
            $("#update-autoexec-config-btn, #autoexec-config-delete, #autoexec-config-copy, #autoexec-config-download").show();
            $("#add-autoexec-config-btn").hide();

            $("#autoexec-name").html("Autoexec.cfg");

            GoTSkillZCSGOConfigFunctions.addCodemirrorData(autoexecConfigCodeMirror, filteredArray);


            var additionalNetworkArray = [], additionalBindsArray = [], additionalViewModelArray = [];
            $.each(filteredArray, function (index, str) {
                if (str.match("^rate") || str.match("^cl_cmdrate") || str.match("^cl_updaterate") || str.match("^cl_interp") || str.match("^cl_interp_ratio") || str.match("^cl_lagcompensation") || str.match("^cl_timeout") || str.match("^mm_dedicated_search_maxping") || str.match("^sv_clockcorrection_msecs") || str.match("^fps_max")) {

                    if (networkArray.indexOf(str) === -1) {
                        additionalNetworkArray.push(str);
                    }

                }

                if (str.match("^alias") || str.match("^bind")) {

                    if (bindsArray.indexOf(str) === -1) {
                        additionalBindsArray.push(str);
                    }

                }

                if (str.match("^viewmodel_recoil") || str.match("^r_drawtracers")) {

                    if (viewModelArray.indexOf(str) === -1) {
                        additionalViewModelArray.push(str);
                    }

                }
            });

            if (additionalNetworkArray.length > 0) {
                GoTSkillZCSGOConfigFunctions.addCodemirrorData(networkConfigCodeMirror, additionalNetworkArray);
            }

            if (additionalBindsArray.length > 0) {
                GoTSkillZCSGOConfigFunctions.addCodemirrorData(bindConfigCodeMirror, additionalBindsArray);
            }

            if (additionalViewModelArray.length > 0) {
                GoTSkillZCSGOConfigFunctions.addCodemirrorData(viewModelConfigCodeMirror, additionalViewModelArray);
            }


        } else {
            $("#update-autoexec-config-btn, #autoexec-config-delete, #autoexec-config-copy, #autoexec-config-download").hide();
            $("#add-autoexec-config-btn").show();

            data.push("No Config Added By User");
            GoTSkillZCSGOConfigFunctions.addCodemirrorData(autoexecConfigCodeMirror, data);
        }

    },
    initializePracConfigCodemirror: function (data) {
        "use strict";

        var filteredArray = data.filter(function (el) {
            return el !== "";
        });


        pracConfigCodeMirror = CodeMirror(document.getElementById("prac-config"), {
            lineNumbers: false,
            theme: "dracula",
            mode: "html",
            scrollbarStyle: "overlay",
            readOnly: "false",
            cursorBlinkRate: -1
        });


        if (filteredArray.length > 0) {
            $("#update-prac-config-btn, #prac-config-delete, #prac-config-copy, #prac-config-download").show();
            $("#add-prac-config-btn").hide();

            if (userPracConfig !== undefined && userPracConfig !== null) {
                $("#practice-name").html("Practice CFG - " + userPracConfig.name);
            } else {
                $("#practice-name").html("Practice CFG");
            }


            GoTSkillZCSGOConfigFunctions.addCodemirrorData(pracConfigCodeMirror, filteredArray);

        } else {
            $("#update-prac-config-btn, #prac-config-delete, #prac-config-copy, #prac-config-download").hide();
            $("#add-prac-config-btn").show();

            data.push("No Config Added By User");
            GoTSkillZCSGOConfigFunctions.addCodemirrorData(pracConfigCodeMirror, data);
        }

    },
    initializeSensiCodemirror: function (data) {
        "use strict";

        sensiConfigCodeMirror = CodeMirror(document.getElementById("sensitivity"), {
            lineNumbers: false,
            theme: "dracula",
            mode: "html",
            scrollbarStyle: "overlay",
            readOnly: "true",
            cursorBlinkRate: -1
        });


        $.each(data, function (index, str) {
            if (str.match("^sensitivity") || str.match("^zoom_sensitivity_ratio_mouse")) {
                sensitivityArray.push(str);
            }
        });

        GoTSkillZCSGOConfigFunctions.addCodemirrorData(sensiConfigCodeMirror, sensitivityArray);
    },
    initializeBindsCodemirror: function (data) {
        "use strict";

        bindConfigCodeMirror = CodeMirror(document.getElementById("binds"), {
            lineNumbers: false,
            theme: "dracula",
            mode: "html",
            scrollbarStyle: "overlay",
            readOnly: "true",
            cursorBlinkRate: -1
        });


        $.each(data, function (index, str) {
            if (str.match("^bind")) {
                bindsArray.push(str);
            }
        });


        GoTSkillZCSGOConfigFunctions.addCodemirrorData(bindConfigCodeMirror, bindsArray);
    },
    initializeViewModelCodemirror: function (data) {
        "use strict";

        viewModelConfigCodeMirror = CodeMirror(document.getElementById("viewmodel"), {
            lineNumbers: false,
            theme: "dracula",
            mode: "html",
            scrollbarStyle: "overlay",
            readOnly: "true",
            cursorBlinkRate: -1
        });


        $.each(data, function (index, str) {
            if (str.match("^cl_bob") || str.match("^viewmodel") || str.match("^cl_viewmodel_shift_left_amt") || str.match("^cl_viewmodel_shift_right_amt") || str.match("^cl_righthand")) {
                viewModelArray.push(str);
            }
        });


        GoTSkillZCSGOConfigFunctions.addCodemirrorData(viewModelConfigCodeMirror, viewModelArray);
    },
    initializeCrosshairCodemirror: function (data) {
        "use strict";

        crosshairConfigCodeMirror = CodeMirror(document.getElementById("crosshair"), {
            lineNumbers: false,
            theme: "dracula",
            mode: "html",
            scrollbarStyle: "overlay",
            readOnly: "true",
            cursorBlinkRate: -1
        });


        $.each(data, function (index, str) {
            if (str.match("^cl_crosshair")) {
                crosshairArray.push(str);
            }
        });


        GoTSkillZCSGOConfigFunctions.addCodemirrorData(crosshairConfigCodeMirror, crosshairArray);
    },
    initializeNetworkCodemirror: function (data) {
        "use strict";

        networkConfigCodeMirror = CodeMirror(document.getElementById("network"), {
            lineNumbers: false,
            theme: "dracula",
            mode: "html",
            scrollbarStyle: "overlay",
            readOnly: "true",
            cursorBlinkRate: -1
        });


        $.each(data, function (index, str) {
            if (str.match("^rate") || str.match("^cl_cmdrate") || str.match("^cl_updaterate") || str.match("^cl_interp") || str.match("^cl_interp_ratio") || str.match("^cl_lagcompensation") || str.match("^cl_predict") || str.match("^cl_predictweapons") || str.match("^cl_timeout") || str.match("^mm_dedicated_search_maxping") || str.match("^sv_clockcorrection_msecs")) {
                networkArray.push(str);
            }
        });


        GoTSkillZCSGOConfigFunctions.addCodemirrorData(networkConfigCodeMirror, networkArray);
    },
    initializeSoundCodemirror: function (data) {
        "use strict";

        soundConfigCodeMirror = CodeMirror(document.getElementById("sound"), {
            lineNumbers: false,
            theme: "dracula",
            mode: "html",
            scrollbarStyle: "overlay",
            readOnly: "true",
            cursorBlinkRate: -1
        });


        $.each(data, function (index, str) {
            if (str.match("^snd")) {
                soundArray.push(str);
            }
        });


        GoTSkillZCSGOConfigFunctions.addCodemirrorData(soundConfigCodeMirror, soundArray);
    },
    addCodemirrorData: function (control, configData) {
        "use strict";
        $.each(configData, function (index, item) {

            var doc = control.getDoc();
            var cursor = doc.getCursor(); // gets the line number in the cursor position
            var line = doc.getLine(cursor.line); // get the line contents
            var pos = {
                line: cursor.line
            };
            if (line.length === 0) {
                // check if the line is empty
                // add the data
                doc.replaceRange(item, pos);
            } else {
                // add a new line and the data
                doc.replaceRange("\n" + item, pos);
            }
        });
    },
    showRedirectModal:function() {
        "use strict";
        GoTSkillZCSGOConfigFunctions.bindRedirectBtn();
        $("#go-to-config-edit-modal").modal("show");
    },
    bindRedirectBtn:function() {
        "use strict";

        $("#redirect-to-config").unbind("click").bind("click", function() {
            window.location.replace("http://www.gotskillz.gg/CSGOConfigurations/CSGOConfig");
        });
    },
    showAddUpdateMainConfigModalBtn: function () {
        "use strict";
        $("#add-config-btn, #update-config-btn, #add-autoexec-config-btn, #update-autoexec-config-btn, #add-prac-config-btn, #update-prac-config-btn,#edit-config-video,#edit-config-mouse").unbind("click").click("click", function (e) {
            e.preventDefault();
            e.stopImmediatePropagation();
            GoTSkillZCSGOConfigFunctions.showRedirectModal();
        });
    },
    downloadMainConfig: function () {
        "use strict";

        $.ajax({
            url: fileAPI + "GetCSGOMainConfigFile/"+userId,
            type: "GET",
            dataType: "json",
            cache: false,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data.length > 0) {
                    window.location = data[0].imagePath;

                }
            }
        });
    },
    downloadAutoexecConfig: function () {
        "use strict";

        $.ajax({
            url: fileAPI + "GetCSGOAutoexecConfigFile/"+ userId,
            type: "GET",
            dataType: "json",
            cache: false,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data.length > 0) {
                    window.location = data[0].imagePath;

                }
            }
        });
    },
    downloadPracConfig: function () {
        "use strict";

        $.ajax({
            url: fileAPI + "GetCSGOPracConfigFIle/" + userId,
            type: "GET",
            dataType: "json",
            cache: false,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data.length > 0) {
                    window.location = data[0].imagePath;

                }
            }
        });
    },
    bindDownloadBtns: function () {
        "use strict";

        $("#config-download").unbind("click").click("click", function () {
            GoTSkillZCSGOConfigFunctions.downloadMainConfig();
        });

        $("#autoexec-config-download").unbind("click").click("click", function () {
            GoTSkillZCSGOConfigFunctions.downloadAutoexecConfig();
        });


        $("#prac-config-download").unbind("click").click("click", function () {
            GoTSkillZCSGOConfigFunctions.downloadPracConfig();
        });
    }
};


var GoTSkillZCSGOVideoSettingFunctions = {
    getCSGOVideoSettings: function () {
        "use strict";

        $.ajax({
            url: gameDataApi + "GetCSGOVideoConfiguration/" + userId,
            type: "GET",
            dataType: "json",
            cache: false,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data !== null) {
                    videoConfiguration = data;
                }
            },
            complete: function () {
                GoTSkillZCSGOVideoSettingFunctions.intializeCSGOVideoSettingsView();
            },
            error: function () {
                GoTSkillZNotificationControls.ShowNotification("Could Not Get CSGO Video Config Data, Please Contact Admin", "danger");
            }
        });

    },
    intializeCSGOVideoSettingsView: function () {
        "use strict";

        if (videoConfiguration !== undefined && videoConfiguration !== null) {
            $("#config-colormode").html(videoConfiguration.ColorMode);
            $("#config-brightness").html(videoConfiguration.Brightness);
            $("#config-ratio").html(videoConfiguration.AspectRatio);
            $("#config-resolution").html(videoConfiguration.Resolution);
            $("#config-gameview").html(videoConfiguration.GameView);
            $("#config-displaynode").html(videoConfiguration.DisplayMode);
            $("#config-laptopowersaving").html(videoConfiguration.LaptopPowerSavings);
            $("#config-globalshadowquality").html(videoConfiguration.GlobalShadowQuality);
            $("#config-texturedetail").html(videoConfiguration.TextureFilteringMode);
            $("#config-effectdetail").html(videoConfiguration.EffectDetail);
            $("#config-shaderdetail").html(videoConfiguration.ShaderDetail);
            $("#config-multicore").html(videoConfiguration.MultiCoreRendering);
            $("#config-aamode").html(videoConfiguration.MultisamplingAntiAliasingMode);
            $("#config-fxaa").html(videoConfiguration.FXXAAAnti_Aliasing);
            $("#config-texturefiltering").html(videoConfiguration.TextureFilteringMode);
            $("#config-vsync").html(videoConfiguration.WaitForVerticalSync);
            $("#config-monitonblue").html(videoConfiguration.MotionBlur);
            $("#config-triplemonitor").html(videoConfiguration.TripleMonitorMode);

        } else {
            $("#config-colormode,#config-brightness,#config-ratio,#config-resolution,#config-gameview,#config-displaynode,#config-laptopowersaving,#config-globalshadowquality,#config-texturedetail,#config-effectdetail,#config-shaderdetail,#config-multicore,#config-aamode,#config-fxaa,#config-texturefiltering,#config-vsync,#config-monitonblue,#config-triplemonitor").html("");
        }
    }
};


var GoTSkillZCSGOMouseSettingsFunctions = {
    getCSGOMouseSettings: function () {
        "use strict";

        $.ajax({
            url: gameDataApi + "GetActiveCSGOSensitivity/" + userId,
            type: "GET",
            dataType: "json",
            cache: false,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data !== null) {
                    mouseSettings = data;
                }
            },
            complete: function () {
                GoTSkillZCSGOMouseSettingsFunctions.intializeCSGOMouseSettingsView();
            },
            error: function () {
                GoTSkillZNotificationControls.ShowNotification("Could Not Get CSGO Mouse Settings Data, Please Contact Admin", "danger");
            }
        });

    },
    intializeCSGOMouseSettingsView: function () {
        "use strict";

        if (mouseSettings !== undefined && mouseSettings !== null && mouseSettings.UserId !== 0) {
            $("#config-sensi").html(parseFloat(mouseSettings.Sensitivity).toFixed(2));
            $("#config-dpi").html(mouseSettings.DPI);
            $("#config-edpi").html(mouseSettings.eDPI);
            $("#config-rawinput").html(mouseSettings.RawInput === true ? "On" : "Off");
            $("#config-window").html(mouseSettings.WindowsSensitivity + "/11");
            $("#config-mousehz").html(mouseSettings.MouseHz);


        } else {
            $("#config-sensi,#config-dpi,#config-edpi,#config-rawinput,#config-window,#config-mousehz").html("");
        }
    },
    setExistingMouseSettings: function () {
        "use strict";

        if (mouseSettings !== undefined && mouseSettings !== null) {


            $("#mouse-settings-sensi").val(parseFloat(mouseSettings.Sensitivity).toFixed(2));
            $("#mouse-settings-DPI").val(mouseSettings.DPI);
            $("#mouse-settings-edpi").val(mouseSettings.eDPI);

            $("#mouse-settings-hz").val(mouseSettings.MouseHz);

            if (mouseSettings.RawInput) {
                $('#mouse-settings-raw').bootstrapToggle('on');
            } else {
                $('#mouse-settings-raw').bootstrapToggle('off');
            }



            var windowsSlider = $("#mouse-settings-windows-slider").data("ionRangeSlider");

            windowsSlider.update({
                from: mouseSettings.WindowsSensitivity
            });

        }
    }
};

var GoTSKillZCSGoConfigInitializer = {
    intializeFunctions: function (userObj) {
        $("#csgo-config").show();

        userId = userObj.UserId;

        GoTSkillZCSGOConfigFunctions.getUserMainConfigFile();
        GoTSkillZCSGOVideoSettingFunctions.getCSGOVideoSettings();

        GoTSkillZCSGOConfigFunctions.showAddUpdateMainConfigModalBtn();
        GoTSkillZCSGOConfigFunctions.bindDownloadBtns();

        //mouse settings
        GoTSkillZCSGOMouseSettingsFunctions.getCSGOMouseSettings();
    }
};
