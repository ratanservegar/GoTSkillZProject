<%@ Page Title="Profile" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="GoTSkillZ.Web.UI.Site.User.Profile" %>


<asp:Content ID="Profile" ContentPlaceHolderID="MainContent" runat="server">
    <%: Styles.Render("~/Content/Kendo/css", "~/Content/css") %>
    <%: Scripts.Render("~/bundles/Kendo") %>
    <%: Scripts.Render("~/bundles/codemirror") %>
    <%: Scripts.Render("~/bundles/summernote") %>

    <link rel="stylesheet" href="/Scripts/codemirror/lib/codemirror.css" type="text/css" />
    <link rel="stylesheet" href="/Scripts/codemirror/theme/dracula.css" type="text/css" />
    <link rel="stylesheet" href="/Scripts/codemirror/addon/scroll/simplescrollbars.css" type="text/css" />
    <link rel="stylesheet" href="/Content/lightgallery/css/lightgallery.min.css" type="text/css" />
    <link rel="stylesheet" href="/Content/lightgallery/css/lg-transitions.min.css" type="text/css" />
    <link rel="stylesheet" href="/Content/TimeLine/Timeline.css" type="text/css" />
    <link rel="stylesheet" href="/Content/SummerNote/summernote-lite-flatly.css" type="text/css" />



    <input type="hidden" id="pageId" value="7" />

    <div id="profile-header" class="br-mainpanel br-profile-page">
        <div class="card widget-4 bd-0 rounded-0">
            <div class="card-header ht-75">
            </div>
            <!-- card-header -->
            <div class="card-body" id="profile-header-image" style="background-image: url('../CustomContent/Images/header-background.png'); padding-top: 40px !important;">
                <div class="card-profile-img">
                    <img id="profile-image" class="wd-100 ht-100" src="" alt="">
                </div>
                <!-- card-profile-img -->

                <div id="profile-name"></div>

                <p id="social-links" class="mg-b-0 tx-24"></p>
            </div>
            <!-- card-body -->
        </div>
        <!-- card -->

        <div class="ht-70 bg-black-1 pd-x-20 d-flex align-items-center justify-content-center bd-b bd-white-1">
            <ul class="nav nav-outline active-primary align-items-center flex-row" role="tablist">
                <li class="nav-item">
                    <a class="nav-link active " data-toggle="tab" href="#player-profile" role="tab">Profile</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link " data-toggle="tab" href="#setup" role="tab">My Setup</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link " data-toggle="tab" href="#csgo-config" role="tab">CS:GO Config</a>
                </li>
            </ul>
        </div>

        <div class="tab-content br-profile-body">
            <!-- tab-pane -->
            <div class="tab-pane fade active show" id="player-profile">
                <div class="container">
                    <div class="row" style="padding-bottom: 25px;">
                        <div class="col-lg-12 pd-b-25">
                            <div class="card bg-br-primary rounded">
                                <div class="row pd-t-30">
                                    <div class="col-sm-12">
                                        <div class="pd-l-30" style="float: left;">
                                            <h6 class="tx-teal tx-uppercase tx-13">About Me</h6>
                                        </div>
                                        <div class="tx-right pd-r-30" style="float: right;">
                                            <div class="t-15 r-25">
                                                <a id="edit-aboutme" href="" class="tx-white-5 hover-info profile-edit">
                                                    <i class="icon ion-edit tx-16"></i>
                                                </a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row pd-t-40">
                                    <div class="col-md-12">
                                        <div id="aboutme-container" class="pd-l-50 pd-r-50 pd-b-50 tx-white mg-b-25" style="height: auto; overflow-y: hidden;">
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <!-- card -->
                        </div>
                        <!-- col-lg-4 -->
                        <div class="col-lg-5 pd-b-25">
                            <div class="card bg-br-primary rounded mg-b-30">
                                <div class="row pd-t-30">
                                    <div class="col-sm-12">
                                        <div class="pd-l-30" style="float: left;">
                                            <h6 class="tx-teal tx-uppercase tx-13">My Information</h6>
                                        </div>
                                        <div class="tx-right pd-r-30" style="float: right;">
                                            <div class="t-15 r-25">
                                                <a id="edit-myinfo" href="" class="tx-white-5 hover-info profile-edit">
                                                    <i class="icon ion-edit tx-16"></i>
                                                </a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div id="myinfo-container" style="height: auto; overflow: hidden;">
                                            <div class="form-layout-4" style="border: 0px !important;">
                                                <div class="row">
                                                    <label class="col-sm-6 form-control-label tx-12 tx-mont  tx-spacing-1 mg-b-2"><span class="ionicons ion-at" style="font-size: 15px; padding-right: 5px"></span>Email Address</label>
                                                    <label id="info-email" class="col-sm-6 form-control-label tx-white mg-b-25"></label>
                                                </div>
                                                <div class="row mg-t-20">
                                                    <label class="col-sm-6 form-control-label tx-12 tx-mont  tx-spacing-1 mg-b-2"><span class="ionicons ion-android-call" style="font-size: 15px; padding-right: 5px"></span>Phone Number</label>
                                                    <label id="info-tel" class="col-sm-6 form-control-label tx-info mg-b-25"></label>
                                                </div>
                                                <!-- row -->
                                                <div class="row mg-t-20">
                                                    <label class="col-sm-6 form-control-label tx-12 tx-mont  tx-spacing-1 mg-b-2"><span class="fas fa-baby-carriage" style="font-size: 15px; padding-right: 5px"></span>Age</label>
                                                    <label id="info-age" class="col-sm-6 form-control-label tx-white mg-b-25"></label>
                                                </div>
                                                <div class="row mg-t-20">
                                                    <label class="col-sm-6 form-control-label tx-12 tx-mont  tx-spacing-1 mg-b-2"><span class="fas fa-venus-mars" style="font-size: 15px; padding-right: 5px"></span>Gender</label>
                                                    <label id="info-gender" class="col-sm-6 form-control-label tx-white mg-b-25"></label>
                                                </div>
                                                <div class="row mg-t-20">
                                                    <label class="col-sm-6 form-control-label tx-12 tx-mont  tx-spacing-1 mg-b-2"><span class="fas fa-ring" style="font-size: 15px; padding-right: 5px"></span>Marital Status</label>
                                                    <label id="info-marital" class="col-sm-6 form-control-label tx-white mg-b-25"></label>
                                                </div>
                                                <div class="row mg-t-20">
                                                    <label class="col-sm-6 form-control-label tx-12 tx-mont  tx-spacing-1 mg-b-2"><span class="fas fa-briefcase" style="font-size: 15px; padding-right: 5px"></span>Occupation</label>
                                                    <label id="info-occupation" class="col-sm-6 form-control-label tx-white mg-b-25"></label>
                                                </div>
                                                <!-- row -->
                                                <div class="row mg-t-20">
                                                    <label class="col-sm-6 form-control-label tx-12 tx-mont  tx-spacing-1 mg-b-2"><span class="fas fa-globe-americas" style="font-size: 15px; padding-right: 5px"></span>Country</label>
                                                    <label id="info-country" class="col-sm-6 form-control-label tx-white mg-b-25"></label>
                                                </div>
                                                <!-- row -->
                                                <div class="row mg-t-20">
                                                    <label class="col-sm-6 form-control-label tx-12 tx-mont  tx-spacing-1 mg-b-2"><span class="fas fa-sign" style="font-size: 15px; padding-right: 5px"></span>State</label>
                                                    <label id="info-state" class="col-sm-6 form-control-label tx-white mg-b-25"></label>
                                                </div>
                                                <!-- row -->
                                                <div class="row mg-t-20">
                                                    <label class="col-sm-6 form-control-label tx-12 tx-mont  tx-spacing-1 mg-b-2"><span class="fas fa-city" style="font-size: 15px; padding-right: 5px"></span>City</label>
                                                    <label id="info-city" class="col-sm-6 form-control-label tx-white mg-b-25"></label>
                                                </div>


                                                <!-- row -->
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="card bg-br-primary rounded mg-b-30">
                                <div class="row pd-t-30">
                                    <div class="col-sm-12">
                                        <div class="pd-l-30" style="float: left;">
                                            <h6 class="tx-teal tx-uppercase tx-13">Player Profile</h6>
                                        </div>
                                        <div class="tx-right pd-r-30" style="float: right;">
                                            <div class="t-15 r-25">
                                                <a id="edit-playerProfile" href="" class="tx-white-5 hover-info profile-edit">
                                                    <i class="icon ion-edit tx-16"></i>
                                                </a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row ">
                                    <div class="col-md-12">
                                        <div id="playerprofile-container" style="height: auto; overflow-y: hidden;">
                                            <div class="form-layout-4" style="border: 0px !important;">
                                                <div class="row">
                                                    <label class="col-sm-6 form-control-label tx-12 tx-mont  tx-spacing-1 mg-b-2"><span class="fas fa-user-ninja" style="font-size: 15px; padding-right: 5px"></span>Alias</label>
                                                    <label id="player-alias" class="col-sm-6 form-control-label tx-white mg-b-25"></label>
                                                </div>
                                                <div class="row mg-t-20">
                                                    <label class="col-sm-6 form-control-label tx-12 tx-mont  tx-spacing-1 mg-b-2"><span class="fas fa-traffic-light" style="font-size: 15px; padding-right: 5px"></span>Status</label>
                                                    <label id="player-status" class="col-sm-6 form-control-label tx-white mg-b-25"></label>
                                                </div>
                                                <!-- row -->
                                                <div class="row mg-t-20">
                                                    <label class="col-sm-6 form-control-label tx-12 tx-mont  tx-spacing-1 mg-b-2"><span class="ionicons ion-ios-people" style="font-size: 15px; padding-right: 5px"></span>Current Team</label>
                                                    <label id="player-currentteam" class="col-sm-6 form-control-label tx-white mg-b-25"></label>
                                                </div>
                                                <!-- row -->
                                                <div class="row mg-t-20">
                                                    <label class="col-sm-6 form-control-label tx-12 tx-mont  tx-spacing-1 mg-b-2"><span class="ionicons ionicons ion-mouse" style="font-size: 15px; padding-right: 5px"></span>Primary Game</label>
                                                    <label id="player-primarygame" class="col-sm-6 form-control-label tx-white mg-b-25"></label>
                                                </div>
                                                <!-- row -->
                                                <div class="row mg-t-20">
                                                    <label class="col-sm-6 form-control-label tx-12 tx-mont  tx-spacing-1 mg-b-2"><span class="ionicons ionicons ion-bonfire" style="font-size: 15px; padding-right: 5px"></span>Primary Game Role</label>
                                                    <label id="player-primaryrole" class="col-sm-6 form-control-label tx-white mg-b-25"></label>
                                                </div>
                                                <div class="row mg-t-20">
                                                    <label class="col-sm-6 form-control-label tx-12 tx-mont  tx-spacing-1 mg-b-2"><span class="ionicons ion-ios-star" style="font-size: 15px; padding-right: 5px"></span>Primary Game Exp.</label>
                                                    <label id="player-primarygameexp" class="col-sm-6 form-control-label tx-white mg-b-25"></label>
                                                </div>
                                                <!-- row -->
                                                <div class="row mg-t-20">
                                                    <label class="col-sm-6 form-control-label tx-12 tx-mont  tx-spacing-1 mg-b-2"><span class="ionicons ion-ios-game-controller-b" style="font-size: 15px; padding-right: 5px"></span>Secondary Game</label>
                                                    <label id="player-secondarygame" class="col-sm-6 form-control-label tx-white mg-b-25"></label>
                                                </div>
                                                <!-- row -->
                                                <div class="row mg-t-20">
                                                    <label class="col-sm-6 form-control-label tx-12 tx-mont  tx-spacing-1 mg-b-2"><span class="ionicons ionicons ion-bonfire" style="font-size: 15px; padding-right: 5px"></span>Secondary Game Role</label>
                                                    <label id="player-secondaryrole" class="col-sm-6 form-control-label tx-white mg-b-25"></label>
                                                </div>
                                                <!-- row -->
                                                <div class="row mg-t-20">
                                                    <label class="col-sm-6 form-control-label tx-12 tx-mont  tx-spacing-1 mg-b-2"><span class="ionicons ion-ios-star" style="font-size: 15px; padding-right: 5px"></span>Secondary Game Exp.</label>
                                                    <label id="player-secondarygameexp" class="col-sm-6 form-control-label tx-white mg-b-25"></label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- card -->
                            <div class="card bg-br-primary rounded mg-b-30">
                                <div class="row pd-t-30">
                                    <div class="col-sm-12">
                                        <div class="pd-l-30" style="float: left;">
                                            <h6 class="tx-teal tx-uppercase tx-13">Team History</h6>
                                        </div>
                                        <div class="tx-right pd-r-30" style="float: right;">
                                            <div class="t-15 r-25">
                                                <a id="edit-teamhistory" href="" class="tx-white-5 hover-info profile-edit">
                                                    <i class="icon ion-edit tx-16"></i>
                                                </a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row ">
                                    <div id="teamhistory-container" class="col-md-12" style="height: auto; overflow-y: hidden;">
                                    </div>
                                </div>
                            </div>
                            <!-- card -->
                        </div>
                        <!-- col-lg-4 -->
                        <div class="col-lg-7">
                            <div class="card bg-br-primary rounded">
                                <div class="row pd-t-30">
                                    <div class="col-sm-12">
                                        <div class="pd-l-30" style="float: left;">
                                            <h6 class="tx-teal tx-uppercase tx-13">Achievements</h6>
                                        </div>
                                        <div class="tx-right pd-r-30" style="float: right;">
                                            <div class="t-15 r-25">
                                                <a id="edit-achievements" href="" class="tx-white-5 hover-info profile-edit">
                                                    <i class="icon ion-edit tx-16"></i>
                                                </a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div id="achievement-container" class="main-timeline7  pd-l-50 pd-r-50  pd-b-50" style="height: 60.3em; overflow-y: hidden;">
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <!-- card -->
                        </div>
                        <!-- col-lg-8 -->
                    </div>
                    <!-- row -->
                </div>

            </div>
            <!-- tab-pane -->
            <div class="tab-pane fade " id="setup">
                <div class="container">
                    <div class="row" style="padding-bottom: 25px;">
                        <div class="col-sm-12">
                            <div class="pd-r-30">
                                <div class="btn-group show" role="group" aria-label="Basic example">
                                    <button id="rt-add-button" type="button" class="btn btn-teal  dropdown-toggle" data-toggle="dropdown" title="Add Setup/Peripheral" aria-expanded="true">
                                        <i class="far fa-plus-square"></i>
                                    </button>
                                    <ul class="dropdown-menu br-menu-sub" role="menu" x-placement="bottom-start" style="position: absolute; will-change: transform; top: 0px; left: 0px; transform: translate3d(0px, 44px, 0px);">
                                        <li id="lt-folder-sitemap-item" class="sub-item">
                                            <a href="#/" class="sub-link" id="add-first-setup">Add Setup</a>
                                        </li>
                                        <li id="lt-page-sitemap-item " class="disabled sub-item">
                                            <a href="#/" class="sub-link" id="add-first-peripheral">Add Peripheral</a>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="setup-container">
                        <h5 style='text-align: center; vertical-align: middle; line-height: 90px;'>No Data Provided.</h5>
                    </div>

                </div>
                <!-- row -->
            </div>
            <!-- tab-pane -->
            <div class="tab-pane fade " id="csgo-config">
                <div class="container">
                    <div class="row ">
                        <div class="col-md-6 mg-b-20">
                            <div class="card bd-0 rounded-0 mg-b-20">
                                <div class="card-header pd-0 bd bd-b-0 rounded-0 d-flex align-items-center">
                                    <div class="col-md-6 pd-25">
                                        <h5 class="tx-white" id="config-name" style="margin: 0;">CONFIG</h5>
                                    </div>
                                </div>
                                <!-- card-header -->
                                <div id="main-config" class="card-body ht-650 pd-0 codemirror-bracket-dark bd bd-white-1"></div>
                                <div class="card-footer bd bd-t-0 tx-13 pd-y-15">
                                    <a href="#\" class="tx-white-7 hover-info mg-l-20" style="float: right;" id="config-download"><i class="icon  ion-ios-cloud-download tx-18 lh-0 mg-r-5"></i>Download</a>
                                </div>
                                <!-- card-footer -->
                            </div>
                            <div class="card bd-0 rounded-0 mg-b-20">
                                <div class="card-header pd-0 bd bd-b-0 rounded-0 d-flex align-items-center">
                                    <div class="col-md-6 pd-25">
                                        <h5 class="tx-white" id="autoexec-name" style="margin: 0;">Autoexec.cfg</h5>
                                    </div>
                                </div>
                                <!-- card-header -->
                                <div id="autoexec-config" class="card-body ht-400 pd-0 codemirror-bracket-dark bd bd-white-1"></div>
                                <div class="card-footer bd bd-t-0 tx-13 pd-y-15">
                                    <a href="#\" class="tx-white-7 hover-info mg-l-20" style="float: right;" id="autoexec-config-download"><i class="icon  ion-ios-cloud-download tx-18 lh-0 mg-r-5"></i>Download</a>
                                </div>
                                <!-- card-footer -->
                            </div>
                            <div class="card bd-0 rounded-0 mg-b-20">
                                <div class="card-header pd-0 bd bd-b-0 rounded-0 d-flex align-items-center">
                                    <div class="col-md-6 pd-25">
                                        <h5 class="tx-white" id="practice-name" style="margin: 0;">Practice.cfg</h5>
                                    </div>
                                </div>
                                <!-- card-header -->
                                <div id="prac-config" class="card-body ht-400 pd-0 codemirror-bracket-dark bd bd-white-1"></div>
                                <div class="card-footer bd bd-t-0 tx-13 pd-y-15">
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
                                    </div>
                                </div>
                                <div class="row ">
                                    <div class="col-md-12">
                                        <div id="config-sensi-container" style="height: auto; overflow-y: hidden;">
                                            <div class=" form-layout-4" style="border: 0px !important;">
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
                                <div class="col-md-12 mg-b-20 p-1" style="padding: 0">
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
                                <div class="col-md-12 mg-b-20 p-1" style="padding: 0">
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
                                <div class="col-md-12 mg-b-20 p-1" style="padding: 0">
                                    <div class="card bd-0 rounded-0">
                                        <div class="card-header pd-0 bd bd-b-0 rounded-0 d-flex align-items-center">
                                            <div class="col-md-6 pd-25">
                                                <h5 class="tx-white" style="margin: 0;">Viewmodel</h5>
                                            </div>
                                            <div class="col-md-6  ht-100p tx-center bd-r bd-white-1">
                                            </div>
                                        </div>
                                        <!-- card-header -->
                                        <div id="viewmodel" class="card-body ht-200   pd-0 codemirror-bracket-dark bd bd-white-1"></div>
                                    </div>
                                </div>
                                <div class="col-md-12 mg-b-20 p-1" style="padding: 0">
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
                                <div class="col-md-12 mg-b-20 p-1" style="padding: 0">
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
                                <div class="col-md-12 mg-b-20 p-1" style="padding: 0">
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


        <div id="user-edit-modal" class="modal fade effect-sign mg-t-100">
            <div class="modal-dialog modal-lg pd-10" role="document">
                <div class="modal-content">
                    <div class="modal-body">
                        <div id="user-profile-wizard" class="col-md-12">
                            <h3>Personal Information</h3>
                            <section>
                                <div class="container-fluid">
                                    <div class="row mg-b-10 ">
                                        <div class="col-lg-4 mg-b-5">
                                            <div class="input-group input-group-dark">
                                                <input type="text" class="form-control form-control-dark" id="regi-firstname" placeholder="First Name" required>
                                            </div>
                                            <!-- input-group -->
                                        </div>
                                        <div class="col-lg-4 mg-b-5 ">
                                            <div class="input-group input-group-dark">

                                                <input type="text" class="form-control form-control-dark" id="regi-lastname" placeholder="Last Name" required>
                                            </div>
                                            <!-- input-group -->
                                        </div>
                                        <div class="col-lg-4 mg-b-5">
                                            <div class="input-group input-group-dark">
                                                <input type="text" class="form-control form-control-dark" id="regi-email" placeholder="Email" readonly data-toggle="tooltip-primary" data-placement="top" title="Google Email - ReadOnly">
                                            </div>
                                            <!-- input-group -->
                                        </div>
                                    </div>
                                    <div class="row mg-b-10">
                                        <div class="col-lg-4 mg-b-5">
                                            <div class="input-group input-group-dark">
                                                <input type="text" class="form-control form-control-dark" id="regi-phonenumber" placeholder="Phone Number">
                                            </div>
                                            <!-- input-group -->
                                        </div>
                                        <div class="col-lg-4 mg-b-5">
                                            <div class="input-group input-group-dark">
                                                <input type="text" class="form-control form-control-dark" id="regi-age" placeholder="Age">
                                            </div>
                                            <!-- input-group -->
                                        </div>
                                        <div class="col-lg-4 mg-b-5">
                                            <div class="input-group input-group-dark">
                                                <input type="text" class="form-control fc-datepicker form-control-dark" id="regi-dob" placeholder="DOB: DD/MM/YYYY">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row  mg-b-10">
                                        <div class="col-lg-4 mg-b-5">
                                            <div class="input-group input-group-dark">
                                                <input class="form-control   form-control-dark" id="regi-country-ddl" data-placeholder="Country">
                                            </div>

                                        </div>
                                        <div class="col-lg-4 mg-b-5">
                                            <div class="input-group input-group-dark">
                                                <input class="form-control    form-control-dark" id="regi-state-ddl" data-placeholder="State">
                                            </div>

                                        </div>
                                        <div class="col-lg-4 mg-b-5">
                                            <div class="input-group input-group-dark">
                                                <input class="form-control    form-control-dark" id="regi-city-ddl" data-placeholder="City">
                                            </div>

                                        </div>
                                    </div>
                                    <div class="row mg-b-10 ">
                                        <div class="col-lg-4 mg-b-5">
                                            <div class="input-group input-group-dark">
                                                <textarea rows="3" class="form-control form-control-dark" id="regi-address" placeholder="Address" data-toggle="tooltip-primary" data-placement="bottom" title="Won't Be Displayed On Your Profile"></textarea>
                                            </div>
                                            <!-- input-group -->
                                        </div>
                                        <div class="col-lg-4 mg-b-5">
                                            <div class="input-group input-group-dark">
                                                <input type="text" class="form-control form-control-dark" id="regi-pincode" placeholder="Pin Code" data-toggle="tooltip-primary" data-placement="bottom" title="Won't Be Displayed On Your Profile">
                                            </div>
                                            <!-- input-group -->
                                        </div>
                                        <div class="col-lg-4 mg-b-5" id="user-profile-img-container">
                                       
                                        </div>
                                    </div>
                                </div>
                                <!-- form-layout -->
                            </section>
                            <h3>About Me</h3>
                            <section>
                                <div class="container-fluid ">
                                    <div class="row mg-b-10 ">
                                        <div class="col-lg-12 mg-b-10">
                                            <div class="input-group input-group-dark">
                                                <textarea rows="3" class="form-control form-control-dark" id="regi-aboutme" placeholder="About Me"></textarea>
                                            </div>
                                            <!-- input-group -->
                                        </div>
                                    </div>
                                    <div class="row mg-b-10 ">
                                        <div class="col-lg-4 mg-b-5">
                                            <div class="input-group input-group-dark">
                                                <input class="form-control  form-control-dark" id="regi-gender" data-placeholder="Gender">
                                            </div>
                                            <!-- input-group -->
                                        </div>
                                        <div class="col-lg-4 mg-b-5">
                                            <div class="input-group input-group-dark">
                                                <input class="form-control  form-control-dark" id="regi-marital" data-placeholder="Marital Status">
                                            </div>
                                            <!-- input-group -->
                                        </div>
                                        <div class="col-lg-4 mg-b-5">
                                            <div class="input-group input-group-dark">
                                                <input class="form-control    form-control-dark" id="regi-occupation" data-placeholder="Occupation">
                                            </div>
                                            <!-- input-group -->
                                        </div>
                                    </div>
                                </div>
                            </section>
                            <h3>Player Profile</h3>
                            <section>
                                <div class="container-fluid">
                                    <div class="row mg-b-10 ">
                                        <div class="col-lg-4 mg-b-5">
                                            <div class="input-group input-group-dark">
                                                <input type="text" class="form-control form-control-dark" id="regi-alias" placeholder="Alias/Gamer Tag">
                                            </div>
                                            <!-- input-group -->
                                        </div>
                                        <div class="col-lg-4 mg-b-5">
                                            <div class="input-group input-group-dark">
                                                <input class="form-control form-control-dark" id="regi-playerstatus" placeholder="Status">
                                            </div>
                                            <!-- input-group -->
                                        </div>
                                        <div class="col-lg-4 mg-b-5">
                                            <div class="input-group input-group-dark">
                                                <input class="form-control form-control-dark" id="regi-currentteam" placeholder="Current Team" data-toggle="tooltip-primary" data-placement="top" title="Team You Are Currently Playing In" readonly>
                                            </div>
                                            <!-- input-group -->
                                        </div>
                                    </div>
                                    <div class="row mg-b-10">
                                        <div class="col-lg-4 mg-b-5">
                                            <div class="input-group input-group-dark">
                                                <input class="form-control  form-control-dark" id="regi-primarygame" placeholder="Primary Esports Game">
                                            </div>
                                            <!-- input-group -->
                                        </div>
                                        <div class="col-lg-4 mg-b-5">
                                            <div class="input-group input-group-dark">
                                                <input class="form-control  form-control-dark" id="regi-primaryrole" placeholder="Primary Role">
                                            </div>
                                            <!-- input-group -->
                                        </div>
                                        <div class="col-lg-4 mg-b-5">
                                            <div class="input-group input-group-dark">
                                                <input class="form-control  form-control-dark" id="regi-primarygameexp" placeholder="Total Exp." data-toggle="tooltip-primary" data-placement="top" title="Since How Long Have You Been Playing This Game">
                                            </div>
                                            <!-- input-group -->
                                        </div>
                                    </div>
                                    <div class="row mg-b-10 ">
                                        <div class="col-lg-4 mg-b-5">
                                            <div class="input-group input-group-dark">
                                                <input class="form-control  form-control-dark" id="regi-secondarygame" placeholder="Secondary Esports Game">
                                            </div>
                                            <!-- input-group -->
                                        </div>
                                        <div class="col-lg-4 mg-b-5">
                                            <div class="input-group input-group-dark">
                                                <input class="form-control  form-control-dark" id="regi-secondaryrole" placeholder="Primary Role">
                                            </div>
                                            <!-- input-group -->
                                        </div>
                                        <div class="col-lg-4 mg-b-5">
                                            <div class="input-group input-group-dark">
                                                <input class="form-control  form-control-dark" id="regi-secondarygameexp" placeholder="Total Exp." data-toggle="tooltip-primary" data-placement="bottom" title="Since How Long Have You Been Playing This Game">
                                            </div>
                                            <!-- input-group -->
                                        </div>
                                    </div>
                                </div>
                            </section>
                            <h3>Team History</h3>
                            <section>
                                <div class="container-fluid">
                                    <div class=" t-15 r-25">
                                        <a id="add-teamhistory" href="#" class="tx-white-5 hover-info profile-edit"><i class="icon ion-plus tx-12"></i>&nbsp;ADD</a>
                                    </div>
                                    <div id="regi-teamhistory-container">
                                    </div>
                                </div>
                            </section>
                            <h3>Achievements</h3>
                            <section>
                                <div class="container-fluid">
                                    <div class=" t-15 r-25">
                                        <a id="add-achievement" href="#" class="tx-white-5 hover-info profile-edit"><i class="icon ion-plus tx-12"></i>&nbsp;ADD</a>
                                    </div>
                                    <div id="regi-achievements-container">
                                    </div>
                                </div>
                            </section>
                            <h3>Social Information<span data-toggle="tooltip-primary" data-placement="right" title="Please Input Complete URL; eg: https://www.instagram.com/igotskillzgaming"> <i class="fa fa-info-circle"></i></span></h3>
                            <section>
                                <div class=" container-fluid">
                                    <div class="row mg-b-10 ">
                                        <div class="col-lg-4 mg-b-5">
                                            <div class="input-group input-group-dark">
                                                <input type="text" id="regi-youtube" class="form-control form-control-dark" placeholder="Youtube">
                                            </div>
                                            <!-- input-group -->
                                        </div>
                                        <div class="col-lg-4 mg-b-5 ">
                                            <div class="input-group input-group-dark">
                                                <input type="text" id="regi-twitch" class="form-control form-control-dark" placeholder="Twitch">
                                            </div>
                                            <!-- input-group -->
                                        </div>
                                        <div class="col-lg-4 mg-b-5">
                                            <div class="input-group input-group-dark">
                                                <input type="text" id="regi-faceit" class="form-control form-control-dark" placeholder="Faceit">
                                            </div>
                                            <!-- input-group -->
                                        </div>
                                    </div>
                                    <div class="row mg-b-10">
                                        <div class="col-lg-4 mg-b-5">
                                            <div class="input-group input-group-dark">
                                                <input type="text" id="regi-steam" class="form-control form-control-dark" placeholder="Steam">
                                            </div>
                                            <!-- input-group -->
                                        </div>
                                        <div class="col-lg-4 mg-b-5">
                                            <div class="input-group input-group-dark">
                                                <input type="text" id="regi-sostronk" class="form-control form-control-dark" placeholder="SoStronk">
                                            </div>
                                            <!-- input-group -->
                                        </div>
                                        <div class="col-lg-4 mg-b-5">
                                            <div class="input-group input-group-dark">
                                                <input type="text" id="regi-instagram" class="form-control form-control-dark" placeholder="Instagram">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mg-b-10">
                                        <div class="col-lg-4 mg-b-5">
                                            <div class="input-group input-group-dark">
                                                <input type="text" id="regi-facebook" class="form-control form-control-dark" placeholder="Facebook">
                                            </div>
                                            <!-- input-group -->
                                        </div>
                                        <div class="col-lg-4 mg-b-5">
                                            <div class="input-group input-group-dark">
                                                <input type="text" id="regi-twitter" class="form-control form-control-dark" placeholder="Twitter">
                                            </div>
                                            <!-- input-group -->
                                        </div>
                                        <div class="col-lg-4 mg-b-5">
                                            <div class="input-group input-group-dark">
                                                <input type="text" id="regi-mixer" class="form-control form-control-dark" placeholder="Mixer">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mg-b-10">
                                        <div class="col-lg-4 mg-b-5">
                                            <div class="input-group input-group-dark">
                                                <input type="text" id="regi-discord" class="form-control form-control-dark" placeholder="Discord">
                                            </div>
                                            <!-- input-group -->
                                        </div>
                                    </div>
                                </div>
                            </section>
                            <h3>Account Settings</h3>
                            <section>
                                <div class=" container-fluid">
                                    <div class="row mg-b-10 ">
                                        <div class="col-lg-4 mg-b-5">
                                            <div class="input-group input-group-dark" style="padding-top: 20px;">
                                                <label class="ckbox">
                                                    <input type="checkbox" id="regi-showprofile"><span>Display Personal Info On Profile Page?</span>
                                                </label>
                                            </div>
                                            <!-- input-group -->
                                        </div>
                                    </div>

                                </div>
                            </section>
                        </div>
                    </div>
                    <!-- modal-body -->
                </div>
            </div>
        </div>

        <div id="user-setup-add-modal" class="modal fade effect-sign mg-t-100">
            <div class="modal-dialog modal-lg pd-10" role="document" style="height: 90%">
                <div class="modal-content" style="height: 90%">
                    <div class="modal-header pd-y-20 pd-x-25">
                        <h6 class="tx-14 mg-b-0 tx-white tx-bold">Add Setup</h6>
                    </div>
                    <div class="modal-body setup-modal-body">
                        <div class="container-fluid form-layout-1">
                            <div class="row pd-b-20">
                                <div class="col-md-3">
                                    <div class="input-group input-group-dark">
                                        <input class="form-control form-control-dark regi-setup-type" id="regi-setup-type" data-placeholder="Select Setup Type">
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <input type="text" class="form-control form-control-dark setup-name" id="setup-name" placeholder="Setup Name" required>
                                    </div>
                                </div>
                            </div>
                            <div id="regi-setup-container">
                                <div id="setup-add" class="pd-5" setup-id="0">
                                    <hr class="bd-t bd-primary ">
                                    <div class="row">
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label class="form-control-label">CPU</label>
                                                <input type="text" class="form-control form-control-dark setup-brand" id="setup-cpu-brand" placeholder="Brand" required>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label class="form-control-label">Production Description</label>
                                                <input type="text" class="form-control form-control-dark setup-cpu-des" id="setup-cpu-des" placeholder="Intel i9 9900k 5.0Ghz.." required>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label class="form-control-label">Product Link</label>
                                                <input type="text" class="form-control form-control-dark setup-cpu-affiliate" id="setup-cpu-affiliate" placeholder="www.amazon.in..." required>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label class="form-control-label">MOTHERBOARD</label>
                                                <input type="text" class="form-control form-control-dark setup-brand" id="setup-mobo-brand" placeholder="Brand" required>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label class="form-control-label">Production Description</label>
                                                <input type="text" class="form-control form-control-dark setup-mobo-des" id="setup-mobo-des" placeholder="Asus Rampave V..." required>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label class="form-control-label">Product Link</label>
                                                <input type="text" class="form-control form-control-dark setup-mobo-affiliate" id="setup-mobo-affiliate" placeholder="www.amazon.in..." required>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label class="form-control-label">RAM</label>
                                                <input type="text" class="form-control form-control-dark setup-brand" id="setup-ram-brand" placeholder="Brand" required>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label class="form-control-label">Production Description</label>
                                                <input type="text" class="form-control form-control-dark setup-ram-des" id="setup-ram-des" placeholder="Corsair Vengeance..." required>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label class="form-control-label">Product Link</label>
                                                <input type="text" class="form-control form-control-dark setup-ram-affiliate" id="setup-ram-affiliate" placeholder="www.amazon.in..." required>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label class="form-control-label">GPU</label>
                                                <input type="text" class="form-control form-control-dark setup-brand" id="setup-gpu-brand" placeholder="Brand" required>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label class="form-control-label">Production Description</label>
                                                <input type="text" class="form-control form-control-dark setup-gpu-des" id="setup-gpu-des" placeholder="NVIDIA RTX 280..." required>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label class="form-control-label">Product Link</label>
                                                <input type="text" class="form-control form-control-dark setup-gpu-affiliate" id="setup-gpu-affiliate" placeholder="www.amazon.in..." required>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label class="form-control-label">PSU</label>
                                                <input type="text" class="form-control form-control-dark setup-brand" id="setup-psu-brand" placeholder="Brand" required>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label class="form-control-label">Production Description</label>
                                                <input type="text" class="form-control form-control-dark setup-psu-des" id="setup-psu-des" placeholder="Corsair AX1200i" required>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label class="form-control-label">Product Link</label>
                                                <input type="text" class="form-control form-control-dark setup-psu-affiliate" id="setup-psu-affiliate" placeholder="www.amazon.in..." required>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label class="form-control-label">SSD</label>
                                                <input type="text" class="form-control form-control-dark setup-brand" id="setup-ssd-brand" placeholder="Brand" required>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label class="form-control-label">Production Description</label>
                                                <input type="text" class="form-control form-control-dark setup-ssd-des" id="setup-ssd-des" placeholder="Samsung Evo..." required>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label class="form-control-label">Product Link</label>
                                                <input type="text" class="form-control form-control-dark setup-ssd-affiliate" id="setup-ssd-affiliate" placeholder="www.amazon.in..." required>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label class="form-control-label">HDD</label>
                                                <input type="text" class="form-control form-control-dark setup-brand" id="setup-hdd-brand" placeholder="Brand" required>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label class="form-control-label">Production Description</label>
                                                <input type="text" class="form-control form-control-dark setup-hdd-des" id="setup-hdd-des" placeholder="Western Digital..." required>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label class="form-control-label">Product Link</label>
                                                <input type="text" class="form-control form-control-dark setup-hdd-affiliate" id="setup-hdd-affiliate" placeholder="www.amazon.in..." required>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label class="form-control-label">CASE</label>
                                                <input type="text" class="form-control form-control-dark setup-brand" id="setup-case-brand" placeholder="Brand" required>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label class="form-control-label">Production Description</label>
                                                <input type="text" class="form-control form-control-dark setup-case-des" id="setup-case-des" placeholder="Corsair Crystal 680..." required>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label class="form-control-label">Product Link</label>
                                                <input type="text" class="form-control form-control-dark setup-case-affiliate" id="setup-case-affiliate" placeholder="www.amazon.in..." required>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label class="form-control-label">COOLING</label>
                                                <input type="text" class="form-control form-control-dark setup-brand" id="setup-cooler-brand" placeholder="Brand" required>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label class="form-control-label">Production Description</label>
                                                <input type="text" class="form-control form-control-dark setup-cooler-des" id="setup-cooler-des" placeholder="Corsair H150..." required>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label class="form-control-label">Product Link</label>
                                                <input type="text" class="form-control form-control-dark setup-cooler-affiliate" id="setup-cooler-affiliate" placeholder="www.amazon.in..." required>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label class="form-control-label">CASE FANS</label>
                                                <input type="text" class="form-control form-control-dark setup-brand" id="setup-fan-brand" placeholder="Brand" required>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label class="form-control-label">Production Description</label>
                                                <input type="text" class="form-control form-control-dark setup-fan-des" id="setup-fan-des" placeholder="Corsair 120LL.." required>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label class="form-control-label">Product Link</label>
                                                <input type="text" class="form-control form-control-dark setup-fan-affiliate" id="setup-fan-affiliate" placeholder="www.amazon.in..." required>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label class="form-control-label">Capture CARD</label>
                                                <input type="text" class="form-control form-control-dark setup-brand" id="setup-capture-brand" placeholder="Brand" required>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label class="form-control-label">Production Description</label>
                                                <input type="text" class="form-control form-control-dark setup-caputer-des" id="setup-capture-des" placeholder="Elgato..." required>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label class="form-control-label">Product Link</label>
                                                <input type="text" class="form-control form-control-dark setup-caputer-affiliate" id="setup-capture-affiliate" placeholder="www.amazon.in..." required>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label class="form-control-label">SOUND CARD</label>
                                                <input type="text" class="form-control form-control-dark setup-brand" id="setup-sound-brand" placeholder="Brand" required>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label class="form-control-label">Production Description</label>
                                                <input type="text" class="form-control form-control-dark setup-sound-des" id="setup-sound-des" placeholder="Creative..." required>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label class="form-control-label">Product Link</label>
                                                <input type="text" class="form-control form-control-dark setup-sound-affiliate" id="setup-sound-affiliate" placeholder="www.amazon.in..." required>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="form-group">
                                                <label class="form-control-label">SETUP PICTURES</label>
                                                <input type="file" id="setup-img" class="form-control form-control-dark" name="setup-images" accept="image/*">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary tx-11 tx-uppercase pd-y-12 pd-x-25 tx-mont tx-semibold" data-dismiss="modal">Close</button>
                        <button type="button" id="delete-setup" class="btn btn-danger tx-11 tx-uppercase pd-y-12 pd-x-25 tx-mont tx-semibold" style="display: none;" data-id="0">Delete</button>
                        <button type="button" id="save-setup" class="btn btn-teal tx-11 tx-uppercase pd-y-12 pd-x-25 tx-mont tx-semibold" data-id="0">Save</button>
                    </div>
                </div>
            </div>
        </div>


        <div id="user-peripheral-add-modal" class="modal fade effect-sign mg-t-100">
            <div class="modal-dialog modal-lg pd-10" role="document" style="height: 90%">
                <div class="modal-content" style="height: 90%">
                    <div class="modal-header pd-y-20 pd-x-25">
                        <h6 class="tx-14 mg-b-0 tx-white tx-bold">Peripheral</h6>
                    </div>
                    <div class="modal-body peripheral-modal-body">
                        <div class="container-fluid  form-layout-1">
                            <div class=" t-15 r-25">
                                <a id="add-peripheral" href="#" class="tx-white-5 hover-info  regi-peripheral-add"><i class="icon ion-plus tx-12"></i>&nbsp;ADD Peripheral</a>
                            </div>
                            <div id="regi-peripheral-container">
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="form-group" id="peripheral-img-container">
                                        <label class="form-control-label">SETUP PICTURES</label>
                                        <input type="file" id="peripheral-img" class="form-control form-control-dark" name="setup-images" accept="image/*">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary tx-11 tx-uppercase pd-y-12 pd-x-25 tx-mont tx-semibold" data-dismiss="modal">Close</button>
                        <button type="button" id="save-peripheral" class="btn btn-teal tx-11 tx-uppercase pd-y-12 pd-x-25 tx-mont tx-semibold" data-id="0">Save</button>
                    </div>
                </div>
            </div>
        </div>



    </div>


    <script src="/Scripts/parsleyjs/parsley.min.js"></script>
    <script src="/Scripts/YearPicker/yearpicker.js"></script>
    <script src="/Scripts/jquery-steps/jquery.steps.min.js"></script>
    <script src="/CustomScripts/User/Profile.js"></script>
    <script src="/CustomScripts/User/Setup.js"></script>
    <script src="/CustomScripts/User/User-CSConfig.js"></script>
    <script src="/Scripts/Lightgallery/lightgallery.min.js"></script>
    <script src="/Scripts/Lightgallery/lg-thumbnail.min.js"></script>



</asp:Content>
