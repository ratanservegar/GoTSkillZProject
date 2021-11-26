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
    showAddUpdateMainConfigModal: function (e) {
        "use strict";
        var initialFile = [];

        var endPoint = "";
        var kendoAddFileEndpoint = "";
        var kendoremoveFileEndpoint = "";

        switch (e.currentTarget.id) {


            case "update-config-btn":
            case "add-config-btn":
                endPoint = "GetCSGOMainConfigFile/" + userId;
                kendoAddFileEndpoint = "UploadCSGOMainConfig";
                kendoremoveFileEndpoint = "RemoveCSGOMainConfig";
                break;
            case "add-autoexec-config-btn":
            case "update-autoexec-config-btn":
                endPoint = "GetCSGOAutoexecConfigFile/" + userId;
                kendoAddFileEndpoint = "UploadCSGOAutoexecConfig";
                kendoremoveFileEndpoint = "RemoveCSGOAutoexecConfig";
                break;
            case "add-prac-config-btn":
            case "update-prac-config-btn":
                endPoint = "GetCSGOPracConfigFIle/" + userId;
                kendoAddFileEndpoint = "UploadCSGOPracConfig";
                kendoremoveFileEndpoint = "RemoveCSGOPracConfig";
                break;
            default:
                endPoint = "GetCSGOMainConfigFile";
                break;
        }


        if (e.currentTarget.innerText === "Update") {
            $("#add-config-title").html("Update CS:GO Config");
        } else {
            $("#add-config-title").html("Add CS:GO Config");
        }



        $.ajax({
            url: fileAPI + endPoint,
            type: "GET",
            dataType: "json",
            cache: false,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data.length > 0) {
                    initialFile = data;
                }
            },
            complete: function () {
                // kendo file upload
                if ($("#add-config").data("kendoUpload") === undefined) {
                    $("#add-config").kendoUpload({
                        "multiple": false,
                        async: {
                            autoUpload: false,
                            saveUrl: fileAPI + kendoAddFileEndpoint,
                            removeUrl: fileAPI + kendoremoveFileEndpoint
                        },
                        upload: function (e) {
                            GoTSkillZUIFunctions.ShowLoader("Uploading Config, Please Wait...");
                            e.data = {
                                userId: userId
                            };
                        },
                        complete: function () {
                            setTimeout(function () {
                                window.location.reload();
                            }, 500);
                        },
                        remove: function (e) {
                            e.data = {
                                userId: userId
                            };
                        },
                        validation: {
                            allowedExtensions: [".cfg"]
                        },
                        files: initialFile
                    });
                } else {

                    var uploadWidget = $("#add-config").getKendoUpload();

                    var uploaderOptions = uploadWidget.options;
                    uploaderOptions.files = [];

                    uploadWidget.destroy();


                    var uploadInput = $("#add-config");
                    var wrapper = uploadInput.parents(".k-upload");

                    wrapper.remove();


                    $("#add-config-container").append(uploadInput);
                    $("#add-config").kendoUpload({
                        "multiple": false,
                        async: {
                            autoUpload: false,
                            saveUrl: fileAPI + kendoAddFileEndpoint,
                            removeUrl: fileAPI + kendoremoveFileEndpoint
                        },
                        upload: function (e) {

                            GoTSkillZUIFunctions.ShowLoader("Uploading Config, Please Wait...");
                            e.data = {
                                userId: userId
                            };
                        },
                        remove: function (e) {
                            e.data = {
                                userId: userId
                            };
                        },
                        complete: function () {
                            setTimeout(function () {
                                window.location.reload();
                            }, 500);
                        },
                        validation: {
                            allowedExtensions: [".cfg"]
                        },
                        files: initialFile
                    });

                }
                GoTSkillZCSGOConfigFunctions.bindUpdateAddBtn();
                $("#add-config-modal").modal("show");
            },
            error: function () {
                GoTSkillZNotificationControls.ShowNotification("Could Not Get CSGO Config Data, Please Contact Admin", "danger");
            }
        });
    },
    addUpdateMainConfig: function () {
        if ($("#add-config").data("kendoUpload").getFiles().length > 0) {
            $("#add-config").data("kendoUpload").upload();
        } else {
            GoTSkillZNotificationControls.ShowNotification("Please Add Config File", "danger");
        }

    },
    removeMainConfig: function () {
        "use strict";

        if (userMainConfig !== undefined && userMainConfig !== null) {
            $.ajax({
                url: fileAPI + "RemoveCSGOMainConfigForUser/" + userMainConfig.name,
                type: "GET",
                dataType: "json",
                cache: false,
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    if (data === "success") {
                        window.location.reload(true);
                    }
                },
                error: function () {
                    GoTSkillZNotificationControls.ShowNotification("Could Not Delete CSGO Config , Please Contact Admin", "danger");
                }
            });
        }

    },
    removeAutoexecConfig: function () {
        "use strict";

        if (userAutoexecConfig !== undefined && userAutoexecConfig !== null) {
            $.ajax({
                url: fileAPI + "RemoveCSGOAutoexecConfigForUser/" + userAutoexecConfig.name,
                type: "GET",
                dataType: "json",
                cache: false,
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    if (data === "success") {
                        window.location.reload(true);
                    }
                },
                error: function () {
                    GoTSkillZNotificationControls.ShowNotification("Could Not Delete CSGO Autoexec Config , Please Contact Admin", "danger");
                }
            });
        }

    },
    removePracConfig: function () {
        "use strict";

        if (userPracConfig !== undefined && userPracConfig !== null) {
            $.ajax({
                url: fileAPI + "RemoveCSGOPracConfigForUser/" + userPracConfig.name,
                type: "GET",
                dataType: "json",
                cache: false,
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    if (data === "success") {
                        window.location.reload(true);
                    }
                },
                error: function () {
                    GoTSkillZNotificationControls.ShowNotification("Could Not Delete CSGO Practice Config , Please Contact Admin", "danger");
                }
            });
        }

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
            GoTSkillZCSGOConfigFunctions.initializNetworkCodemirror(filteredArray);
            GoTSkillZCSGOConfigFunctions.initializeSoundCodemirror(filteredArray);


        } else {
            $("#update-config-btn, #config-delete, #config-copy, #config-download").hide();
            $("#add-config-btn").show();

            data.push("Click on Add Config Button To Add Config");
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

            data.push("Click on Add Config Button To Add Autoexec Config");
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
            readOnly: "true",
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

            data.push("Click on Add Config Button To Add Practice Config");
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
    initializNetworkCodemirror: function (data) {
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
    showAddUpdateMainConfigModalBtn: function () {
        "use strict";
        $("#add-config-btn, #update-config-btn, #add-autoexec-config-btn, #update-autoexec-config-btn, #add-prac-config-btn, #update-prac-config-btn").unbind("click").click("click", function (e) {
            GoTSkillZCSGOConfigFunctions.showAddUpdateMainConfigModal(e);
        });
    },
    downloadMainConfig: function () {
        "use strict";

        $.ajax({
            url: fileAPI + "GetCSGOMainConfigFile/" + userId,
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
            url: fileAPI + "GetCSGOAutoexecConfigFile/" + userId,
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
    bindConfigRemoveBtns: function () {
        "use strict";
        $("#config-delete").unbind("click").click("click", function () {
            GoTSkillZCSGOConfigFunctions.removeMainConfig();
        });

        $("#autoexec-config-delete").unbind("click").click("click", function () {
            GoTSkillZCSGOConfigFunctions.removeAutoexecConfig();
        });

        $("#prac-config-delete").unbind("click").click("click", function () {
            GoTSkillZCSGOConfigFunctions.removePracConfig();
        });
    },
    bindUpdateAddBtn: function () {
        "use strict";

        $("#save-config").unbind("click").click("click", function () {
            GoTSkillZCSGOConfigFunctions.addUpdateMainConfig();
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
    },
    bindVideoEditButton: function () {
        "use strict";

        $("#edit-config-video").unbind("click").click("click", function (e) {
            e.preventDefault();
            e.stopImmediatePropagation();
            GoTSkillZCSGOVideoSettingFunctions.showVideoSettingsModal();
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
    },
    showVideoSettingsModal: function () {
        "use strict";


        GoTSkillZCSGOVideoSettingFunctions.iniCommonVideoDDL();
        GoTSkillZCSGOVideoSettingFunctions.iniAspectRatioDDL();
        GoTSkillZCSGOVideoSettingFunctions.initResolutionDDL();
        GoTSkillZCSGOVideoSettingFunctions.iniGameViewDDL();
        GoTSkillZCSGOVideoSettingFunctions.iniColoModeDDL();
        GoTSkillZCSGOVideoSettingFunctions.iniDisplayModeDDL();
        GoTSkillZCSGOVideoSettingFunctions.iniShaderDetailDDL();
        GoTSkillZCSGOVideoSettingFunctions.iniModelAndModelTexturelDDL();
        GoTSkillZCSGOVideoSettingFunctions.iniGlobalShadowDDL();
        GoTSkillZCSGOVideoSettingFunctions.iniMSAADDL();
        GoTSkillZCSGOVideoSettingFunctions.iniTextureFilteringDDL();
        GoTSkillZCSGOVideoSettingFunctions.iniVsyncDDL();
        GoTSKillZCSGoConfigInitializer.initializeBrightnessSlider();

        if (videoConfiguration !== undefined && videoConfiguration !== null) {
            if (videoConfiguration.Id !== undefined && videoConfiguration.Id !== null && videoConfiguration.Id !== 0)
                $("#csgo-video-modal").attr("data-id", videoConfiguration.Id);
        }


        $("#csgo-video-modal").find(".modal-body").css("overflow-y", "hidden");

        var perfectScrollbar = new PerfectScrollbar(".csgo-video-modal-body", {
            wheelPropagation: true,
            minScrollbarLength: 20
        });


        GoTSkillZCSGOVideoSettingFunctions.setExistingVideoConfig();
        $("#csgo-video-modal").modal("show");
    },
    setExistingVideoConfig() {
        "use strict";

        if (videoConfiguration !== undefined && videoConfiguration !== null) {

            $("#csgo-colormode").data("kendoDropDownList").search(videoConfiguration.ColorMode);


            var brightnessSlider = $("#brightness-slider").data("ionRangeSlider");

            brightnessSlider.update({
                from: parseInt(videoConfiguration.Brightness.replace("%", ""))
            });


            $("#csgo-aspectratio").data("kendoDropDownList").search(videoConfiguration.AspectRatio);
            $("#csgo-displaymode").data("kendoDropDownList").search(videoConfiguration.DisplayMode);
            $("#csgo-powersavings").data("kendoDropDownList").search(videoConfiguration.LaptopPowerSavings);
            $("#csgo-globalshadowquality").data("kendoDropDownList").search(videoConfiguration.GlobalShadowQuality);
            $("#csgo-modeltexturedetail").data("kendoDropDownList").search(videoConfiguration.ModelTextureDetail);
            $("#csgo-effectdetail").data("kendoDropDownList").search(videoConfiguration.EffectDetail);
            $("#csgo-shaderdetail").data("kendoDropDownList").search(videoConfiguration.ShaderDetail);
            $("#csgo-multicorerendering").data("kendoDropDownList").search(videoConfiguration.MultiCoreRendering);
            $("#csgo-multisamplingantialiasingmode").data("kendoDropDownList").search(videoConfiguration.MultisamplingAntiAliasingMode);
            $("#csgo-fxaaantialiasing").data("kendoDropDownList").search(videoConfiguration.FXXAAAnti_Aliasing);
            $("#csgo-texturefilteringmode").data("kendoDropDownList").search(videoConfiguration.TextureFilteringMode);
            $("#csgo-waitforverticalsync").data("kendoDropDownList").search(videoConfiguration.WaitForVerticalSync);
            $("#csgo-motionblur").data("kendoDropDownList").search(videoConfiguration.MotionBlur);
            $("#csgo-triplemonitormode").data("kendoDropDownList").search(videoConfiguration.TripleMonitorMode);
            $("#csgo-gameview").data("kendoDropDownList").search(videoConfiguration.GameView);
            $("#csgo-resolution").data("kendoDropDownList").text(videoConfiguration.Resolution);
        }
    },
    saveVideoSettings: function () {
        "use strict";
        var isValid = GoTSkillZCSGOVideoSettingFunctions.validateCSGOVideoConfigSave();

        if (isValid) {




            var csgoVideoConfig = new GoTSkillzEntities.CSGOVideoConfigurationDTO();

            if ($("#csgo-video-modal").attr("data-id") !== "0") {
                csgoVideoConfig.Id = $("#csgo-video-modal").attr("data-id");
            };


            csgoVideoConfig.UserId = userId;
            csgoVideoConfig.ColorMode = $("#csgo-colormode").data("kendoDropDownList").text();
            csgoVideoConfig.Brightness = $("#brightness-slider").val() + "%";
            csgoVideoConfig.AspectRatio = $("#csgo-aspectratio").data("kendoDropDownList").text();
            csgoVideoConfig.Resolution = $("#csgo-resolution").data("kendoDropDownList").text();
            csgoVideoConfig.DisplayMode = $("#csgo-displaymode").data("kendoDropDownList").text();
            csgoVideoConfig.LaptopPowerSavings = $("#csgo-powersavings").data("kendoDropDownList").text();
            csgoVideoConfig.GlobalShadowQuality = $("#csgo-globalshadowquality").data("kendoDropDownList").text();
            csgoVideoConfig.ModelTextureDetail = $("#csgo-modeltexturedetail").data("kendoDropDownList").text();
            csgoVideoConfig.EffectDetail = $("#csgo-effectdetail").data("kendoDropDownList").text();
            csgoVideoConfig.ShaderDetail = $("#csgo-shaderdetail").data("kendoDropDownList").text();
            csgoVideoConfig.MultiCoreRendering = $("#csgo-multicorerendering").data("kendoDropDownList").text();
            csgoVideoConfig.MultisamplingAntiAliasingMode = $("#csgo-multisamplingantialiasingmode").data("kendoDropDownList").text();
            csgoVideoConfig.FXXAAAnti_Aliasing = $("#csgo-fxaaantialiasing").data("kendoDropDownList").text();
            csgoVideoConfig.TextureFilteringMode = $("#csgo-texturefilteringmode").data("kendoDropDownList").text();
            csgoVideoConfig.WaitForVerticalSync = $("#csgo-waitforverticalsync").data("kendoDropDownList").text();
            csgoVideoConfig.MotionBlur = $("#csgo-motionblur").data("kendoDropDownList").text();
            csgoVideoConfig.TripleMonitorMode = $("#csgo-triplemonitormode").data("kendoDropDownList").text();
            csgoVideoConfig.GameView = $("#csgo-gameview").data("kendoDropDownList").text();



            $.ajax({
                url: gameDataApi + "SaveCSGOVideoConfiguration",
                type: "POST",
                dataType: "json",
                //global: false,
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(csgoVideoConfig),
                success: function (data) {
                    if (data.indexOf("Failed") === -1) {
                        $("#csgo-video-modal").modal("hide");
                        GoTSkillZCSGOVideoSettingFunctions.getCSGOVideoSettings();
                    }
                },
                complete: function () {
                    GoTSkillZNotificationControls.ShowNotification("Video Configuration Saved Successful!", "success");
                },
                error: function () {
                    GoTSkillZNotificationControls.ShowNotification("Could Not Save CSGO Video Configuration Data, Please Contact Admin", "danger");
                }
            });

        }
    },
    deleteVideoSettings: function () {
        "use strict";

        $.ajax({
            url: gameDataApi + "DeleteCSGOVideoConfiguration/" + userId,
            type: "GET",
            dataType: "json",
            cache: false,
            contentType: "application/json; charset=utf-8",
            success: function () {

            },
            complete: function (data) {

                if (data.statusText === "success") {
                    videoConfiguration = null;
                    GoTSkillZCSGOVideoSettingFunctions.intializeCSGOVideoSettingsView();
                    GoTSkillZNotificationControls.ShowNotification("Deleted CSGO Video Config Data", "success");
                    $("#csgo-video-modal").modal("hide");
                }

            },
            error: function () {
                GoTSkillZNotificationControls.ShowNotification("Could Not Delete CSGO Video Config Data, Please Contact Admin", "danger");
            }
        });
    },
    validateCSGOVideoConfigSave: function () {
        "use strict";

        var valid = true;


        if (userId == undefined || userId == null || userId === 0) {
            GoTSkillZNotificationControls.ShowNotification("Invalid UserId, Please Contact Admin", "danger");
            return valid = false;
        }

        if ($("#csgo-colormode").data("kendoDropDownList").value() === "-1") {
            GoTSkillZNotificationControls.ShowNotification("Please Select Color Mode", "danger");
            return valid = false;
        }

        if ($("#csgo-aspectratio").data("kendoDropDownList").value() === "-1") {
            GoTSkillZNotificationControls.ShowNotification("Please Select Aspect Ratio", "danger");
            return valid = false;
        }


        if ($("#csgo-resolution").data("kendoDropDownList").text() === "Select" || $("#csgo-resolution").data("kendoDropDownList").text() === "") {
            GoTSkillZNotificationControls.ShowNotification("Please Select Resolution", "danger");
            return valid = false;
        }

        if ($("#csgo-displaymode").data("kendoDropDownList").value() === "-1") {
            GoTSkillZNotificationControls.ShowNotification("Please Select Display Mode", "danger");
            return valid = false;
        }

        if ($("#csgo-powersavings").data("kendoDropDownList").value() === "-1") {
            GoTSkillZNotificationControls.ShowNotification("Please Select Laptop Power Saving Mode", "danger");
            return valid = false;
        }

        if ($("#csgo-globalshadowquality").data("kendoDropDownList").value() === "-1") {
            GoTSkillZNotificationControls.ShowNotification("Please Select Global Shadow Quality", "danger");
            return valid = false;
        }

        if ($("#csgo-modeltexturedetail").data("kendoDropDownList").value() === "-1") {
            GoTSkillZNotificationControls.ShowNotification("Please Select Model/Texture Detail", "danger");
            return valid = false;
        }

        if ($("#csgo-effectdetail").data("kendoDropDownList").value() === "-1") {
            GoTSkillZNotificationControls.ShowNotification("Please Select Effect Detail", "danger");
            return valid = false;
        }

        if ($("#csgo-shaderdetail").data("kendoDropDownList").value() === "-1") {
            GoTSkillZNotificationControls.ShowNotification("Please Select Shader Detail", "danger");
            return valid = false;
        }

        if ($("#csgo-multicorerendering").data("kendoDropDownList").value() === "-1") {
            GoTSkillZNotificationControls.ShowNotification("Please Select Multicore Rendering", "danger");
            return valid = false;
        }

        if ($("#csgo-multisamplingantialiasingmode").data("kendoDropDownList").value() === "-1") {
            GoTSkillZNotificationControls.ShowNotification("Please Select Multisampling Anti-Aliasing Mode", "danger");
            return valid = false;
        }

        if ($("#csgo-fxaaantialiasing").data("kendoDropDownList").value() === "-1") {
            GoTSkillZNotificationControls.ShowNotification("Please Select FXAA Anti-Aliasing", "danger");
            return valid = false;
        }

        if ($("#csgo-texturefilteringmode").data("kendoDropDownList").value() === "-1") {
            GoTSkillZNotificationControls.ShowNotification("Please Select Texture Filtering Mode", "danger");
            return valid = false;
        }

        if ($("#csgo-waitforverticalsync").data("kendoDropDownList").value() === "-1") {
            GoTSkillZNotificationControls.ShowNotification("Please Select Wait for Vertical Sync Mode", "danger");
            return valid = false;
        }

        if ($("#csgo-motionblur").data("kendoDropDownList").value() === "-1") {
            GoTSkillZNotificationControls.ShowNotification("Please Select Motion Blur Mode", "danger");
            return valid = false;
        }

        if ($("#csgo-triplemonitormode").data("kendoDropDownList").value() === "-1") {
            GoTSkillZNotificationControls.ShowNotification("Please Select Triple-Monitor Mode", "danger");
            return valid = false;
        }

        if ($("#csgo-gameview").data("kendoDropDownList").value() === "-1") {
            GoTSkillZNotificationControls.ShowNotification("Please Select Game View", "danger");
            return valid = false;
        }

        return valid;
    },
    bindVideoSettingsSavebtn: function () {
        "use strict";

        $("#save-video-settings-save").unbind("click").bind("click", function () {
            GoTSkillZCSGOVideoSettingFunctions.saveVideoSettings();
        });
    },
    bindVideoSettingsDeleteBtn: function () {
        "use strict";

        $("#delete-video-settings-save").unbind("click").bind("click", function () {
            GoTSkillZCSGOVideoSettingFunctions.deleteVideoSettings();
        });
    },
    iniCommonVideoDDL: function () {
        "use strict";


        var dataSource = [
            {
                'id': -1,
                'text': "Select"
            },
            {
                'id': 1,
                'text': "Disabled"
            },
            {
                'id': 2,
                'text': "Enabled"
            }
        ];

        $("#csgo-powersavings, #csgo-multicorerendering, #csgo-fxaaantialiasing, #csgo-triplemonitormode,#csgo-motionblur").kendoDropDownList({
            dataTextField: "text",
            dataValueField: "id",
            dataSource: dataSource
        });
    },
    iniDisplayModeDDL: function () {
        "use strict";

        var dataSource = [
            {
                'id': -1,
                'text': "Select"
            },
            {
                'id': 1,
                'text': "Windowed"
            },
            {
                'id': 2,
                'text': "Fullscreen"
            },
            {
                'id': 3,
                'text': "Fullscreen Windowed"
            }
        ];

        $("#csgo-displaymode").kendoDropDownList({
            dataTextField: "text",
            dataValueField: "id",
            dataSource: dataSource
        });
    },
    iniGameViewDDL: function () {
        "use strict";

        var dataSource = [
            {
                'id': -1,
                'text': "Select"
            },
            {
                'id': 1,
                'text': "Stretched"
            },
            {
                'id': 2,
                'text': "Black Bars"
            }
        ];

        $("#csgo-gameview").kendoDropDownList({
            dataTextField: "text",
            dataValueField: "id",
            dataSource: dataSource
        });
    },
    iniColoModeDDL: function () {
        "use strict";

        var dataSource = [
            {
                'id': -1,
                'text': "Select"
            },
            {
                'id': 1,
                'text': "Computer Monitor"
            },
            {
                'id': 2,
                'text': "Television"
            }
        ];

        $("#csgo-colormode").kendoDropDownList({
            dataTextField: "text",
            dataValueField: "id",
            dataSource: dataSource
        });
    },
    iniShaderDetailDDL: function () {
        "use strict";

        var dataSource = [
            {
                'id': -1,
                'text': "Select"
            },
            {
                'id': 1,
                'text': "Low"
            },
            {
                'id': 2,
                'text': "Medium"
            },
            {
                'id': 3,
                'text': "High"
            },
            {
                'id': 4,
                'text': "Very High"
            }
        ];

        $("#csgo-shaderdetail").kendoDropDownList({
            dataTextField: "text",
            dataValueField: "id",
            dataSource: dataSource
        });
    },
    iniGlobalShadowDDL: function () {
        "use strict";

        var dataSource = [
            {
                'id': -1,
                'text': "Select"
            },
            {
                'id': 1,
                'text': "Very Low"
            },
            {
                'id': 2,
                'text': "Low"
            },
            {
                'id': 3,
                'text': "Medium"
            },
            {
                'id': 4,
                'text': "High"
            }
        ];

        $("#csgo-globalshadowquality").kendoDropDownList({
            dataTextField: "text",
            dataValueField: "id",
            dataSource: dataSource
        });
    },
    iniMSAADDL: function () {
        "use strict";

        var dataSource = [
            {
                'id': -1,
                'text': "Select"
            },
            {
                'id': 1,
                'text': "None"
            },
            {
                'id': 2,
                'text': "2x MSAA"
            },
            {
                'id': 3,
                'text': "4x MSAA"
            },
            {
                'id': 4,
                'text': "8x MSAA"
            }
        ];

        $("#csgo-multisamplingantialiasingmode").kendoDropDownList({
            dataTextField: "text",
            dataValueField: "id",
            dataSource: dataSource
        });
    },
    iniTextureFilteringDDL: function () {
        "use strict";

        var dataSource = [
            {
                'id': -1,
                'text': "Select"
            },
            {
                'id': 1,
                'text': "Bilinear"
            },
            {
                'id': 2,
                'text': "Trilinear"
            },
            {
                'id': 3,
                'text': "Anisotropic 2x"
            },
            {
                'id': 4,
                'text': "Anisotropic 4x"
            },
            {
                'id': 5,
                'text': "Anisotropic 8x"
            },
            {
                'id': 6,
                'text': "Anisotropic 16x"
            }

        ];

        $("#csgo-texturefilteringmode").kendoDropDownList({
            dataTextField: "text",
            dataValueField: "id",
            dataSource: dataSource
        });
    },
    iniModelAndModelTexturelDDL: function () {
        "use strict";

        var dataSource = [
            {
                'id': -1,
                'text': "Select"
            },
            {
                'id': 1,
                'text': "Low"
            },
            {
                'id': 2,
                'text': "Medium"
            },
            {
                'id': 3,
                'text': "High"
            }
        ];

        $("#csgo-effectdetail,#csgo-modeltexturedetail").kendoDropDownList({
            dataTextField: "text",
            dataValueField: "id",
            dataSource: dataSource
        });
    },
    iniVsyncDDL: function () {
        "use strict";

        var dataSource = [
            {
                'id': -1,
                'text': "Select"
            },
            {
                'id': 1,
                'text': "Disabled"
            },
            {
                'id': 2,
                'text': "Double Buffered"
            },
            {
                'id': 3,
                'text': "Triple Buffered"
            }
        ];

        $("#csgo-waitforverticalsync").kendoDropDownList({
            dataTextField: "text",
            dataValueField: "id",
            dataSource: dataSource
        });
    },
    iniAspectRatioDDL: function () {
        "use strict";

        var dataSource = [
            {
                'id': -1,
                'text': "Select"
            },
            {
                'id': 1,
                'text': "Normal 4:3"
            },
            {
                'id': 2,
                'text': "Widescreen 16:9"
            },
            {
                'id': 3,
                'text': "Widescreen 16:10"
            }
        ];

        $("#csgo-aspectratio").kendoDropDownList({
            dataTextField: "text",
            dataValueField: "id",
            dataSource: dataSource
        });
    },
    initResolutionDDL: function () {
        "use strict";

        var dataSource = [];

        $("#csgo-resolution").kendoDropDownList({
            dataTextField: "text",
            dataValueField: "id",
            dataSource: dataSource,
            open: function (e) {

                var aspectRatio = $("#csgo-aspectratio").data("kendoDropDownList").value();
                if (aspectRatio === "-1") {
                    GoTSkillZNotificationControls.ShowNotification("Please Select Aspect Ratio", "warning");
                    $("#csgo-resolution").data("kendoDropDownList").dataSource.data([]);
                } else {

                    switch (aspectRatio) {
                        case "1": //4:3
                            dataSource = [
                                {
                                    'id': -1,
                                    'text': "Select"
                                },
                                {
                                    'id': 1,
                                    'text': "640×480"
                                },
                                {
                                    'id': 2,
                                    'text': "800×600"
                                },
                                {
                                    'id': 3,
                                    'text': "960×720"
                                },
                                {
                                    'id': 4,
                                    'text': "1024×768"
                                },
                                {
                                    'id': 5,
                                    'text': "1280×960"
                                },
                                {
                                    'id': 6,
                                    'text': "1400×1050"
                                },
                                {
                                    'id': 7,
                                    'text': "1440×1080"
                                },
                                {
                                    'id': 8,
                                    'text': "1600×1200"
                                },
                                {
                                    'id': 9,
                                    'text': "1856×1392"
                                },
                                {
                                    'id': 10,
                                    'text': "1920×1440"
                                },
                                {
                                    'id': 11,
                                    'text': "2048×1536"
                                }
                            ];
                            break;
                        case "2": //16:9
                            dataSource = [
                                {
                                    'id': -1,
                                    'text': "Select"
                                },
                                {
                                    'id': 11,
                                    'text': "1024×576"
                                },
                                {
                                    'id': 12,
                                    'text': "1152×648"
                                },
                                {
                                    'id': 13,
                                    'text': "1280×720"
                                },
                                {
                                    'id': 14,
                                    'text': "1366×768"
                                },
                                {
                                    'id': 15,
                                    'text': "1600×900"
                                },
                                {
                                    'id': 16,
                                    'text': "1920×1080"
                                },
                                {
                                    'id': 17,
                                    'text': "2560×1440"
                                },
                                {
                                    'id': 18,
                                    'text': "3840×2160"
                                }
                            ];
                            break;
                        case "3": //16:10
                            dataSource = [
                                {
                                    'id': -1,
                                    'text': "Select"
                                },
                                {
                                    'id': 19,
                                    'text': "1280×800"
                                },
                                {
                                    'id': 20,
                                    'text': "1440×900"
                                },
                                {
                                    'id': 21,
                                    'text': "1680×1050"
                                },
                                {
                                    'id': 22,
                                    'text': "1920×1200"
                                },
                                {
                                    'id': 23,
                                    'text': "2560×1600"
                                }];
                            break;
                    }
                    $("#csgo-resolution").data("kendoDropDownList").setDataSource(dataSource);
                    $("#csgo-resolution").data("kendoDropDownList").dataSource.read();
                }
            }
        });




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
            error: function (data) {
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
    showMouseSettingsModal: function () {
        "use strict";


        GoTSkillZCSGOMouseSettingsFunctions.initializeMouseSettingsWindowsSlider();

        $("#csgo-mouse-settings-modal").find(".modal-body").css("overflow-y", "hidden");

        var perfectScrollbar = new PerfectScrollbar(".csgo-mouse-settings-modal-body", {
            wheelPropagation: true,
            minScrollbarLength: 20
        });


        GoTSkillZCSGOMouseSettingsFunctions.setExistingMouseSettings();
        $("#csgo-mouse-settings-modal").modal("show");
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
    },
    saveSensitivity: function () {
        "use strict";


        var isValid = GoTSkillZCSGOMouseSettingsFunctions.validateSensiSave();

        if (isValid) {
            var newSensiObj = new GoTSkillzEntities.CSGOSensitivityDTO();


            newSensiObj.UserId = parseInt($("#hdnUserId").val());
            newSensiObj.DPI = $("#mouse-settings-DPI").val();
            newSensiObj.Sensitivity = $("#mouse-settings-sensi").val();
            newSensiObj.eDPI = Math.round($("#mouse-settings-edpi").val());
            newSensiObj.RawInput = $("#mouse-settings-raw").is(":checked");
            newSensiObj.WindowsSensitivity = $("#mouse-settings-windows-slider").val();
            newSensiObj.MouseHz = $("#mouse-settings-hz").val();
            $("#csgo-mouse-settings-modal").modal("hide");

            $.ajax({
                url: gameDataApi + "SaveCSGOSensitivity",
                type: "POST",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(newSensiObj),
                success: function (data) {
                    if (data.indexOf("Failed") > -1) {
                        GoTSkillZNotificationControls.ShowNotification("Could Not Save CS:GO Sensitivity, Please Contact Admin", "danger");
                    } else {
                        GoTSkillZCSGOMouseSettingsFunctions.getCSGOMouseSettings();
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


        if ($("#mouse-settings-DPI").val() === "") {
            GoTSkillZNotificationControls.ShowNotification("Please input DPI value", "danger");

            return returnCondition = false;
        }


        if ($("#mouse-settings-sensi").val() === "") {
            GoTSkillZNotificationControls.ShowNotification("Please Input Sensivity", "danger");
            return returnCondition = false;
        }


        if ($("#mouse-settings-edpi").val() === "") {
            GoTSkillZNotificationControls.ShowNotification("Please Input eDPI", "danger");
            return returnCondition = false;
        }

        if ($("#mouse-settings-hz").val() === "") {
            GoTSkillZNotificationControls.ShowNotification("Please Input Mouse Hz (polling rate)", "danger");
            return returnCondition = false;
        }

        return returnCondition;
    },
    bindMouseSettingsEditButton: function () {
        "use strict";

        $("#edit-config-mouse").unbind("click").click("click", function (e) {
            e.preventDefault();
            e.stopImmediatePropagation();
            GoTSkillZCSGOMouseSettingsFunctions.showMouseSettingsModal();
        });
    },
    bindMouseSettingsSaveButton: function () {
        "use strict";


        $("#save-mouse-settings-save").unbind("click").bind("click", function () {
            GoTSkillZCSGOMouseSettingsFunctions.saveSensitivity();
        });
    },
    initializeMouseSettingsWindowsSlider: function () {
        "use strict";

        //init slider
        $("#mouse-settings-windows-slider").ionRangeSlider({
            min: 1,
            max: 11,
            from: 6
        });
    }
};

var GoTSKillZCSGoConfigInitializer = {
    intializeFunctions: function () {
        $("#csgo-config").show();

        userId = parseInt($("#hdnUserId").val());

        GoTSkillZCSGOConfigFunctions.getUserMainConfigFile();
        GoTSkillZCSGOVideoSettingFunctions.getCSGOVideoSettings();
        GoTSkillZCSGOConfigFunctions.showAddUpdateMainConfigModalBtn();
        GoTSkillZCSGOConfigFunctions.bindConfigRemoveBtns();
        GoTSkillZCSGOConfigFunctions.bindDownloadBtns();
        GoTSkillZCSGOConfigFunctions.bindVideoEditButton();
        GoTSkillZCSGOVideoSettingFunctions.bindVideoSettingsSavebtn();
        GoTSkillZCSGOVideoSettingFunctions.bindVideoSettingsDeleteBtn();



        //mouse settings
        GoTSkillZCSGOMouseSettingsFunctions.getCSGOMouseSettings();
        GoTSkillZCSGOMouseSettingsFunctions.bindMouseSettingsEditButton();
        GoTSkillZCSGOMouseSettingsFunctions.bindMouseSettingsSaveButton();
    },
    initializeBrightnessSlider: function () {

        //init slider
        $("#brightness-slider").ionRangeSlider({

            min: 80,
            max: 130,
            from: 80,
            postfix: "%"
        });


    }
};
$(window).on("load", function () {
    "use strict";
    GoTSkillZGateKeeperFunctions.checkUserHasAccess(GoTSKillZCSGoConfigInitializer.intializeFunctions);
});
