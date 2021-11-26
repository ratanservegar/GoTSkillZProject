<%@ Page Title="CS GO Config" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="CSGOConfig.aspx.cs" Inherits="GoTSkillZ.Web.UI.Site.CSGOConfigurations.CSGOConfig" %>

<asp:Content ID="CSGOConfigContent" ContentPlaceHolderID="MainContent" runat="server">
    <%: Styles.Render("~/Content/Kendo/css", "~/Content/css") %>
    <%: Scripts.Render("~/bundles/codemirror") %>
    <%: Scripts.Render("~/bundles/Kendo") %>



    <link rel="stylesheet" href="../Scripts/codemirror/lib/codemirror.css" type="text/css" />
    <link rel="stylesheet" href="../Scripts/codemirror/theme/dracula.css" type="text/css" />
    <link rel="stylesheet" href="../Scripts/codemirror/addon/scroll/simplescrollbars.css" type="text/css" />
    <link rel="stylesheet" href="../Content/ionRangeSlider/ion.rangeSlider.css" type="text/css" />




    <input type="hidden" id="pageId" value="13" />


    <div id="csgo-config" style="display: none;">
        <div class="br-mainpanel">
            <div class="tab-content br-profile-body">
                <div class="br-pagetitle">
                    <i class="icon icon-ezgifcom-gif-maker"></i>
                    <div>
                        <h4>CS:GO Config Builder</h4>
                        <p class="mg-b-0">All Your Configurations in once place.</p>
                    </div>
                </div>

                <div class="br-pagebody">
                    <div class="container-fluid">
                        <div class="row ">
                            <div class="col-md-6 mg-b-20">
                                <div class="card bd-0 rounded-0 mg-b-20">
                                    <div class="card-header pd-0 bd bd-b-0 rounded-0 d-flex align-items-center">
                                        <div class="col-md-6 pd-25">
                                            <h5 class="tx-white" id="config-name" style="margin: 0;">Add CONFIG</h5>
                                        </div>
                                        <div class="col-md-6  ht-100p tx-center bd-r bd-white-1">
                                            <a href="#/" class="tx-white-7 hover-info mg-l-20" style="float: right;" id="add-config-btn"><i class="icon ion-plus tx-12 lh-0 mg-r-5"></i>Add</a>
                                            <a href="#/" class="tx-white-7 hover-info mg-l-20" style="float: right;" id="update-config-btn"><i class="icon  ion-arrow-up-a tx-12 lh-0 mg-r-5"></i>Update</a>
                                        </div>
                                    </div>
                                    <!-- card-header -->
                                    <div id="main-config" class="card-body ht-650 pd-0 codemirror-bracket-dark bd bd-white-1"></div>
                                    <div class="card-footer bd bd-t-0 tx-13 pd-y-15">
                                        <a href="#\" class="tx-white-7 hover-info mg-l-20" style="float: left;" id="config-delete"><i class="icon  ion-ios-trash tx-18 lh-0 mg-r-5"></i>Delete</a>
                                        <a href="#\" class="tx-white-7 hover-info mg-l-20" style="float: right;" id="config-download"><i class="icon  ion-ios-cloud-download tx-18 lh-0 mg-r-5"></i>Download</a>
                                    </div>
                                    <!-- card-footer -->
                                </div>
                                <div class="card bd-0 rounded-0 mg-b-20">
                                    <div class="card-header pd-0 bd bd-b-0 rounded-0 d-flex align-items-center">
                                        <div class="col-md-6 pd-25">
                                            <h5 class="tx-white" id="autoexec-name" style="margin: 0;">Add Autoexec.cfg</h5>
                                        </div>
                                        <div class="col-md-6  ht-100p tx-center bd-r bd-white-1">
                                            <a href="#/" class="tx-white-7 hover-info mg-l-20" style="float: right;" id="add-autoexec-config-btn"><i class="icon ion-plus tx-12 lh-0 mg-r-5"></i>Add</a>
                                            <a href="#/" class="tx-white-7 hover-info mg-l-20" style="float: right;" id="update-autoexec-config-btn"><i class="icon  ion-arrow-up-a tx-12 lh-0 mg-r-5"></i>Update</a>
                                        </div>
                                    </div>
                                    <!-- card-header -->
                                    <div id="autoexec-config" class="card-body ht-400 pd-0 codemirror-bracket-dark bd bd-white-1"></div>
                                    <div class="card-footer bd bd-t-0 tx-13 pd-y-15">
                                        <a href="#\" class="tx-white-7 hover-info mg-l-20" style="float: left;" id="autoexec-config-delete"><i class="icon  ion-ios-trash tx-18 lh-0 mg-r-5"></i>Delete</a>
                                        <a href="#\" class="tx-white-7 hover-info mg-l-20" style="float: right;" id="autoexec-config-download"><i class="icon  ion-ios-cloud-download tx-18 lh-0 mg-r-5"></i>Download</a>
                                    </div>
                                    <!-- card-footer -->
                                </div>
                                <div class="card bd-0 rounded-0 mg-b-20">
                                    <div class="card-header pd-0 bd bd-b-0 rounded-0 d-flex align-items-center">
                                        <div class="col-md-6 pd-25">
                                            <h5 class="tx-white" id="practice-name" style="margin: 0;">Add Practice.cfg</h5>
                                        </div>
                                        <div class="col-md-6  ht-100p tx-center bd-r bd-white-1">
                                            <a href="#/" class="tx-white-7 hover-info mg-l-20" style="float: right;" id="add-prac-config-btn"><i class="icon ion-plus tx-12 lh-0 mg-r-5"></i>Add</a>
                                            <a href="#/" class="tx-white-7 hover-info mg-l-20" style="float: right;" id="update-prac-config-btn"><i class="icon  ion-arrow-up-a tx-12 lh-0 mg-r-5"></i>Update</a>
                                        </div>
                                    </div>
                                    <!-- card-header -->
                                    <div id="prac-config" class="card-body ht-400 pd-0 codemirror-bracket-dark bd bd-white-1"></div>
                                    <div class="card-footer bd bd-t-0 tx-13 pd-y-15">
                                        <a href="#\" class="tx-white-7 hover-info mg-l-20" style="float: left;" id="prac-config-delete"><i class="icon  ion-ios-trash tx-18 lh-0 mg-r-5"></i>Delete</a>
                                        <a href="#\" class="tx-white-7 hover-info mg-l-20" style="float: right;" id="prac-config-download"><i class="icon  ion-ios-cloud-download tx-18 lh-0 mg-r-5"></i>Download</a>
                                    </div>
                                    <!-- card-footer -->
                                </div>

                            </div>
                            <div class="col-md-6">
                                <div class="card bg-br-primary rounded mg-b-30">
                                    <div class="row pd-t-30">
                                        <div class="col-sm-12">
                                            <div class="pd-l-30" style="float: left;">
                                                <h6 class="tx-teal tx-uppercase tx-13">Video Settings</h6>
                                            </div>
                                            <div class="tx-right pd-r-30" style="float: right;">
                                                <div class="t-15 r-25">
                                                    <a id="edit-config-video" href="" class="tx-white-5 hover-info">
                                                        <i class="icon ion-edit tx-16"></i>
                                                    </a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row ">
                                        <div class="col-md-12">
                                            <div id="config-video-container" style="height: auto; overflow-y: hidden;">
                                                <div class="form-layout-4" style="border: 0px !important;">
                                                    <div class="row">
                                                        <label class="col-sm-6 form-control-label tx-12 tx-mont  tx-spacing-1 mg-b-2"><span class="fas fa-swatchbook" style="font-size: 15px; padding-right: 5px"></span>Color Mode</label>
                                                        <label id="config-colormode" class="col-sm-6 form-control-label tx-white mg-b-25"></label>
                                                    </div>
                                                    <div class="row mg-t-20">
                                                        <label class="col-sm-6 form-control-label tx-12 tx-mont  tx-spacing-1 mg-b-2"><span class="fas fa-sun" style="font-size: 15px; padding-right: 5px"></span>Brightness</label>
                                                        <label id="config-brightness" class="col-sm-6 form-control-label tx-white mg-b-25"></label>
                                                    </div>
                                                    <!-- row -->
                                                    <div class="row mg-t-20">
                                                        <label class="col-sm-6 form-control-label tx-12 tx-mont  tx-spacing-1 mg-b-2"><span class="fas fa-percentage" style="font-size: 15px; padding-right: 5px"></span>Aspect Ratio</label>
                                                        <label id="config-ratio" class="col-sm-6 form-control-label tx-white mg-b-25"></label>
                                                    </div>
                                                    <!-- row -->
                                                    <div class="row mg-t-20">
                                                        <label class="col-sm-6 form-control-label tx-12 tx-mont  tx-spacing-1 mg-b-2"><span class="far fa-window-maximize" style="font-size: 15px; padding-right: 5px"></span>Resolution</label>
                                                        <label id="config-resolution" class="col-sm-6 form-control-label tx-white mg-b-25"></label>
                                                    </div>
                                                    <!-- row -->
                                                    <div class="row mg-t-20">
                                                        <label class="col-sm-6 form-control-label tx-12 tx-mont  tx-spacing-1 mg-b-2"><span class="far fa-eye" style="font-size: 15px; padding-right: 5px"></span>Game View</label>
                                                        <label id="config-gameview" class="col-sm-6 form-control-label tx-white mg-b-25"></label>
                                                    </div>
                                                    <!-- row -->
                                                    <div class="row mg-t-20">
                                                        <label class="col-sm-6 form-control-label tx-12 tx-mont  tx-spacing-1 mg-b-2"><span class="icon-monitor-svgrepo-com" style="font-size: 15px; padding-right: 5px"></span>Display Mode</label>
                                                        <label id="config-displaynode" class="col-sm-6 form-control-label tx-white mg-b-25"></label>
                                                    </div>
                                                    <div class="row mg-t-20">
                                                        <label class="col-sm-6 form-control-label tx-12 tx-mont  tx-spacing-1 mg-b-2"><span class="fas fa-battery-three-quarters" style="font-size: 15px; padding-right: 5px"></span>Laptop Power Savings</label>
                                                        <label id="config-laptopowersaving" class="col-sm-6 form-control-label tx-white mg-b-25"></label>
                                                    </div>
                                                    <!-- row -->
                                                    <div class="row mg-t-20">
                                                        <label class="col-sm-6 form-control-label tx-12 tx-mont  tx-spacing-1 mg-b-2"><span class="fas fa-globe-europe" style="font-size: 15px; padding-right: 5px"></span>Global Shadow Quality</label>
                                                        <label id="config-globalshadowquality" class="col-sm-6 form-control-label tx-white mg-b-25"></label>
                                                    </div>
                                                    <!-- row -->
                                                    <div class="row mg-t-20">
                                                        <label class="col-sm-6 form-control-label tx-12 tx-mont  tx-spacing-1 mg-b-2"><span class="ionicons ionicons ion-bonfire" style="font-size: 15px; padding-right: 5px"></span>Model/Texture Detail</label>
                                                        <label id="config-texturedetail" class="col-sm-6 form-control-label tx-white mg-b-25"></label>
                                                    </div>
                                                    <!-- row -->
                                                    <div class="row mg-t-20">
                                                        <label class="col-sm-6 form-control-label tx-12 tx-mont  tx-spacing-1 mg-b-2"><span class="fab fa-deviantart" style="font-size: 15px; padding-right: 5px"></span>Effect Detail</label>
                                                        <label id="config-effectdetail" class="col-sm-6 form-control-label tx-white mg-b-25"></label>
                                                    </div>
                                                    <!-- row -->
                                                    <div class="row mg-t-20">
                                                        <label class="col-sm-6 form-control-label tx-12 tx-mont  tx-spacing-1 mg-b-2"><span class="fas fa-umbrella-beach" style="font-size: 15px; padding-right: 5px"></span>Shader Detail</label>
                                                        <label id="config-shaderdetail" class="col-sm-6 form-control-label tx-white mg-b-25"></label>
                                                    </div>
                                                    <!-- row -->
                                                    <div class="row mg-t-20">
                                                        <label class="col-sm-6 form-control-label tx-12 tx-mont  tx-spacing-1 mg-b-2"><span class="fas fa-cookie" style="font-size: 15px; padding-right: 5px"></span>Multicore Rendering</label>
                                                        <label id="config-multicore" class="col-sm-6 form-control-label tx-white mg-b-25"></label>
                                                    </div>
                                                    <!-- row -->
                                                    <div class="row mg-t-20">
                                                        <label class="col-sm-6 form-control-label tx-12 tx-mont  tx-spacing-1 mg-b-2"><span class="fas fa-drafting-compass" style="font-size: 15px; padding-right: 5px"></span>Multisampling Anti-Aliasing Mode</label>
                                                        <label id="config-aamode" class="col-sm-6 form-control-label tx-white mg-b-25"></label>
                                                    </div>
                                                    <!-- row -->
                                                    <div class="row mg-t-20">
                                                        <label class="col-sm-6 form-control-label tx-12 tx-mont  tx-spacing-1 mg-b-2"><span class="fab fa-fantasy-flight-games" style="font-size: 15px; padding-right: 5px"></span>FXAA Anti-Aliasing</label>
                                                        <label id="config-fxaa" class="col-sm-6 form-control-label tx-white mg-b-25"></label>
                                                    </div>
                                                    <!-- row -->
                                                    <div class="row mg-t-20">
                                                        <label class="col-sm-6 form-control-label tx-12 tx-mont  tx-spacing-1 mg-b-2"><span class="fas fa-filter" style="font-size: 15px; padding-right: 5px"></span>Texture Filtering Mode</label>
                                                        <label id="config-texturefiltering" class="col-sm-6 form-control-label tx-white mg-b-25"></label>
                                                    </div>
                                                    <!-- row -->
                                                    <div class="row mg-t-20">
                                                        <label class="col-sm-6 form-control-label tx-12 tx-mont  tx-spacing-1 mg-b-2"><span class="fas fa-grip-vertical" style="font-size: 15px; padding-right: 5px"></span>Wait for Vertical Sync</label>
                                                        <label id="config-vsync" class="col-sm-6 form-control-label tx-white mg-b-25"></label>
                                                    </div>
                                                    <!-- row -->
                                                    <div class="row mg-t-20">
                                                        <label class="col-sm-6 form-control-label tx-12 tx-mont  tx-spacing-1 mg-b-2"><span class="fas fa-compact-disc" style="font-size: 15px; padding-right: 5px"></span>Motion Blur</label>
                                                        <label id="config-monitonblue" class="col-sm-6 form-control-label tx-white mg-b-25"></label>
                                                    </div>
                                                    <!-- row -->
                                                    <div class="row mg-t-20">
                                                        <label class="col-sm-6 form-control-label tx-12 tx-mont  tx-spacing-1 mg-b-2"><span class="fas fa-network-wired" style="font-size: 15px; padding-right: 5px"></span>Triple-Monitor Mode</label>
                                                        <label id="config-triplemonitor" class="col-sm-6 form-control-label tx-white mg-b-25"></label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div class="card bg-br-primary rounded mg-b-30">
                                    <div class="row pd-t-30">
                                        <div class="col-sm-12">
                                            <div class="pd-l-30" style="float: left;">
                                                <h6 class="tx-teal tx-uppercase tx-13">Mouse Settings</h6>
                                            </div>
                                            <div class="tx-right pd-r-30" style="float: right;">
                                                <div class="t-15 r-25">
                                                    <a id="edit-config-mouse" href="" class="tx-white-5 hover-info">
                                                        <i class="icon ion-edit tx-16"></i>
                                                    </a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row ">
                                        <div class="col-md-12">
                                            <div id="config-sensi-container" style="height: auto; overflow-y: hidden;">
                                                <div class="form-layout-4" style="border: 0px !important;">
                                                    <div class="row">
                                                        <label class="col-sm-6 form-control-label tx-12 tx-mont  tx-spacing-1 mg-b-2"><span class="fas fa-crosshairs" style="font-size: 15px; padding-right: 5px"></span>Sensitivity</label>
                                                        <label id="config-sensi" class="col-sm-6 form-control-label tx-white mg-b-25"></label>
                                                    </div>
                                                    <div class="row  mg-t-20">
                                                        <label class="col-sm-6 form-control-label tx-12 tx-mont  tx-spacing-1 mg-b-2"><span class="fas fa-mouse" style="font-size: 15px; padding-right: 5px"></span>DPI</label>
                                                        <label id="config-dpi" class="col-sm-6 form-control-label tx-white mg-b-25"></label>
                                                    </div>
                                                    <div class="row  mg-t-20">
                                                        <label class="col-sm-6 form-control-label tx-12 tx-mont  tx-spacing-1 mg-b-2"><span class="fas fa-ruler-combined" style="font-size: 15px; padding-right: 5px"></span>eDPI</label>
                                                        <label id="config-edpi" class="col-sm-6 form-control-label tx-white mg-b-25"></label>
                                                    </div>
                                                    <div class="row  mg-t-20">
                                                        <label class="col-sm-6 form-control-label tx-12 tx-mont  tx-spacing-1 mg-b-2"><span class="fab fa-rockrms" style="font-size: 15px; padding-right: 5px"></span>Raw Input</label>
                                                        <label id="config-rawinput" class="col-sm-6 form-control-label tx-white mg-b-25"></label>
                                                    </div>
                                                    <div class="row  mg-t-20">
                                                        <label class="col-sm-6 form-control-label tx-12 tx-mont  tx-spacing-1 mg-b-2"><span class="fab fa-windows" style="font-size: 15px; padding-right: 5px"></span>Windows Sensitivity</label>
                                                        <label id="config-window" class="col-sm-6 form-control-label tx-white mg-b-25"></label>
                                                    </div>
                                                    <div class="row  mg-t-20">
                                                        <label class="col-sm-6 form-control-label tx-12 tx-mont  tx-spacing-1 mg-b-2"><span class="fab fa-galactic-republic" style="font-size: 15px; padding-right: 5px"></span>Mouse Hz (polling rate)</label>
                                                        <label id="config-mousehz" class="col-sm-6 form-control-label tx-white mg-b-25"></label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6 mg-b-20 p-1" style="padding: 0">
                                        <div class="card bd-0 rounded-0">
                                            <div class="card-header pd-0 bd bd-b-0 rounded-0 d-flex align-items-center">
                                                <div class="col-md-6 pd-25">
                                                    <h5 class="tx-white" style="margin: 0;">Sensitivity</h5>
                                                </div>
                                            </div>
                                            <!-- card-header -->
                                            <div id="sensitivity" class="card-body ht-200  pd-0 codemirror-bracket-dark bd bd-white-1"></div>
                                        </div>
                                    </div>
                                    <div class="col-md-6 mg-b-20 p-1" style="padding: 0">
                                        <div class="card bd-0 rounded-0">
                                            <div class="card-header pd-0 bd bd-b-0 rounded-0 d-flex align-items-center">
                                                <div class="col-md-6 pd-25">
                                                    <h5 class="tx-white" style="margin: 0;">Crosshair</h5>
                                                </div>
                                            </div>
                                            <!-- card-header -->
                                            <div id="crosshair" class="card-body ht-200   pd-0 codemirror-bracket-dark bd bd-white-1"></div>
                                        </div>
                                    </div>
                                    <div class="col-md-6 mg-b-20 p-1" style="padding: 0">
                                        <div class="card bd-0 rounded-0">
                                            <div class="card-header pd-0 bd bd-b-0 rounded-0 d-flex align-items-center">
                                                <div class="col-md-6 pd-25">
                                                    <h5 class="tx-white" style="margin: 0;">Viewmodel</h5>
                                                </div>
                                            </div>
                                            <!-- card-header -->
                                            <div id="viewmodel" class="card-body ht-200   pd-0 codemirror-bracket-dark bd bd-white-1"></div>
                                        </div>
                                    </div>
                                    <div class="col-md-6 mg-b-20 p-1" style="padding: 0">
                                        <div class="card bd-0 rounded-0">
                                            <div class="card-header pd-0 bd bd-b-0 rounded-0 d-flex align-items-center">
                                                <div class="col-md-6 pd-25">
                                                    <h5 class="tx-white" style="margin: 0;">Binds</h5>
                                                </div>
                                            </div>
                                            <!-- card-header -->
                                            <div id="binds" class="card-body ht-200   pd-0 codemirror-bracket-dark bd bd-white-1"></div>
                                        </div>
                                    </div>
                                    <div class="col-md-6 mg-b-20 p-1" style="padding: 0">
                                        <div class="card bd-0 rounded-0">
                                            <div class="card-header pd-0 bd bd-b-0 rounded-0 d-flex align-items-center">
                                                <div class="col-md-6 pd-25">
                                                    <h5 class="tx-white" style="margin: 0;">Network</h5>
                                                </div>
                                            </div>
                                            <!-- card-header -->
                                            <div id="network" class="card-body ht-200   pd-0 codemirror-bracket-dark bd bd-white-1"></div>
                                        </div>
                                    </div>
                                    <div class="col-md-6 mg-b-20 p-1" style="padding: 0">
                                        <div class="card bd-0 rounded-0">
                                            <div class="card-header pd-0 bd bd-b-0 rounded-0 d-flex align-items-center">
                                                <div class="col-md-6 pd-25">
                                                    <h5 class="tx-white" style="margin: 0;">Sound Settings</h5>
                                                </div>
                                            </div>
                                            <!-- card-header -->
                                            <div id="sound" class="card-body ht-200  pd-0 codemirror-bracket-dark bd bd-white-1"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <div id="add-config-modal" class="modal fade effect-sign mg-t-100">
            <div class="modal-dialog modal-lg pd-10" role="document">
                <div class="modal-content" style="height: 90%">
                    <div class="modal-header pd-y-20 pd-x-25">
                        <h6 class="tx-14 mg-b-0 tx-white tx-bold" id="add-config-title">Add CS:GO Config</h6>
                    </div>
                    <div class="modal-body peripheral-modal-body">
                        <div class="container-fluid form-layout-1">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="form-group" id="add-config-container">
                                        <label class="form-control-label">ADD Config</label>
                                        <input type="file" id="add-config" class="form-control form-control-dark" name="csgo-config">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary tx-11 tx-uppercase pd-y-12 pd-x-25 tx-mont tx-semibold" data-dismiss="modal">Close</button>
                        <button type="button" id="save-config" class="btn btn-teal tx-11 tx-uppercase pd-y-12 pd-x-25 tx-mont tx-semibold" data-id="0">Save</button>
                    </div>
                </div>
            </div>
        </div>


        <div id="csgo-video-modal" data-id="0" class="modal fade effect-sign mg-t-100">
            <div class="modal-dialog modal-lg pd-10" role="document" style="height: 90%">
                <div class="modal-content" style="height: 90%">
                    <div class="modal-header pd-y-20 pd-x-25">
                        <h6 class="tx-14 mg-b-0 tx-white tx-bold">Video Settings</h6>
                    </div>
                    <div class="modal-body csgo-video-modal-body">
                        <div class="container-fluid form-layout-1">
                            <div class="row">
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <label class="form-control-label">Color Mode</label>
                                        <input type="text" class="form-control form-control-dark" id="csgo-colormode">
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <label class="form-control-label">Brightness</label>
                                        <input type="text" class="js-range-slider" id="brightness-slider" name="my_range" data-extra-classes="irs-outline irs-teal" value="" />
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <label class="form-control-label">Aspect Ratio</label>
                                        <input type="text" class="form-control form-control-dark " id="csgo-aspectratio">
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <label class="form-control-label">Resolution</label>
                                        <input type="text" class="form-control form-control-dark" id="csgo-resolution">
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <label class="form-control-label">Game View</label>
                                        <input type="text" class="form-control form-control-dark " id="csgo-gameview">
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <label class="form-control-label">Display Mode</label>
                                        <input type="text" class="form-control form-control-dark" id="csgo-displaymode">
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <label class="form-control-label">Global Shadow Quality</label>
                                        <input type="text" class="form-control form-control-dark" id="csgo-globalshadowquality">
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <label class="form-control-label">Model/Texture Detail</label>
                                        <input type="text" class="form-control form-control-dark" id="csgo-modeltexturedetail">
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <label class="form-control-label">Effect Detail</label>
                                        <input type="text" class="form-control form-control-dark " id="csgo-effectdetail">
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <label class="form-control-label">Shader Detail</label>
                                        <input type="text" class="form-control form-control-dark" id="csgo-shaderdetail">
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <label class="form-control-label">Multicore Rendering</label>
                                        <input type="text" class="form-control form-control-dark" id="csgo-multicorerendering">
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <label class="form-control-label">Multisampling Anti-Aliasing Mode</label>
                                        <input type="text" class="form-control form-control-dark " id="csgo-multisamplingantialiasingmode">
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <label class="form-control-label">FXAA Anti-Aliasing</label>
                                        <input type="text" class="form-control form-control-dark" id="csgo-fxaaantialiasing">
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <label class="form-control-label">Texture Filtering Mode</label>
                                        <input type="text" class="form-control form-control-dark" id="csgo-texturefilteringmode">
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <label class="form-control-label">Wait for Vertical Sync</label>
                                        <input type="text" class="form-control form-control-dark " id="csgo-waitforverticalsync">
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <label class="form-control-label">Motion Blur</label>
                                        <input type="text" class="form-control form-control-dark" id="csgo-motionblur">
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <label class="form-control-label">Triple-Monitor Mode</label>
                                        <input type="text" class="form-control form-control-dark" id="csgo-triplemonitormode">
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <label class="form-control-label">Laptop Power Savings</label>
                                        <input type="text" class="form-control form-control-dark " id="csgo-powersavings">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary tx-11 tx-uppercase pd-y-12 pd-x-25 tx-mont tx-semibold" data-dismiss="modal">Close</button>
                        <button type="button" id="delete-video-settings-save" class="btn btn-danger tx-11 tx-uppercase pd-y-12 pd-x-25 tx-mont tx-semibold" data-id="0">Delete</button>
                        <button type="button" id="save-video-settings-save" class="btn btn-teal tx-11 tx-uppercase pd-y-12 pd-x-25 tx-mont tx-semibold" data-id="0">Save</button>
                    </div>
                </div>
            </div>
        </div>


        <div id="csgo-mouse-settings-modal" class="modal fade effect-sign mg-t-100">
            <div class="modal-dialog modal-lg pd-10" role="document">
                <div class="modal-content" style="height: 90%">
                    <div class="modal-header pd-y-20 pd-x-25">
                        <h6 class="tx-14 mg-b-0 tx-white tx-bold" id="add-config-mouse-setting-title">Add CS:GO Config</h6>
                    </div>
                    <div class="modal-body csgo-mouse-settings-modal-body">
                        <div class="container-fluid  form-layout-1">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="form-group" data-toggle="tooltip-teal" data-placement="top" title="Enter your current game sensitivity">
                                        <div class="input-group input-group-dark">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text"><i class="fas fa-crosshairs tx-16 lh-0 op-6"></i></span>
                                            </div>
                                            <input type="number" class="form-control tx-white" placeholder="Sensitivity" id="mouse-settings-sensi">
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-12">
                                    <div class="form-group" data-toggle="tooltip-teal" data-placement="top" title="Dots per inch (DPI) is a measurement of how sensitive a mouse is. The higher a mouse’s DPI, the farther the cursor on your screen will move when you move the mouse. A mouse with a higher DPI setting detects and reacts to smaller movements.">
                                        <div class="input-group input-group-dark">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text"><i class="fas fa-mouse tx-16 lh-0 op-6"></i></span>
                                            </div>
                                            <input type="number" class="form-control tx-white" placeholder="DPI" id="mouse-settings-DPI">
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-12">
                                    <div class="form-group" data-toggle="tooltip-teal" data-placement="top" title='eDPI stands for Effective DPI. This number describes your "real" sensitivity; use the eDPI calculator to find your eDPI.'>
                                        <div class="input-group input-group-dark">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text"><i class="fas fa-ruler-combined tx-16 lh-0 op-6"></i></span>
                                            </div>
                                            <input type="number" class="form-control tx-white" placeholder="eDPI" id="mouse-settings-edpi">
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-12">
                                    <div class="form-group" data-toggle="tooltip-teal" data-placement="top" title="Enter your mouse hz (polling rate)">
                                        <div class="input-group input-group-dark">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text"><i class="fab fa-galactic-republic tx-16 lh-0 op-6"></i></span>
                                            </div>
                                            <input type="number" class="form-control tx-white" placeholder="Mouse Hz (Polling Rate)" id="mouse-settings-hz">
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <p class="br-section-label-sm">Raw Input?</p>
                                        <div class="input-group input-group-dark" data-toggle="tooltip-teal" data-placement="top" title='Raw Input "On" in CSGO ignores the Windows mouse settings.'>
                                            <input type="checkbox" id="mouse-settings-raw" checked data-toggle="toggle" data-onstyle="info" data-offstyle="light">
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="form-group" data-toggle="tooltip-teal" data-placement="top" title="This is your windows mouse speed, which can be found in Control panel -> Mouse -> Pointer Options.">
                                        <p class="br-section-label-sm">Windows Sensitivity</p>
                                        <input type="text" class="js-range-slider" id="mouse-settings-windows-slider" name="my_range" data-extra-classes="irs-outline irs-teal" value="" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary tx-11 tx-uppercase pd-y-12 pd-x-25 tx-mont tx-semibold" data-dismiss="modal">Close</button>
                        <button type="button" id="save-mouse-settings-save" class="btn btn-teal tx-11 tx-uppercase pd-y-12 pd-x-25 tx-mont tx-semibold" data-id="0">Save</button>
                    </div>
                </div>
            </div>
        </div>


    </div>

    <script src="../CustomScripts/CSGOConfigurations/CSGOConfig.js"></script>
    <script src="../Scripts/ionRangeSlider/ion.rangeSlider.js"></script>
</asp:Content>
