<%@ Page Title="Double Click Test" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="MouseDoubleClickTest.aspx.cs" Inherits="GoTSkillZ.Web.UI.Site.MouseSettings.MouseDoubleClickTest" %>


<asp:Content ID="MouseDoubleClickTest" ContentPlaceHolderID="MainContent" runat="server">

    <script src="../CustomScripts/MouseSettings/MouseDoubleClickTest.js"></script>

    <input type="hidden" id="pageId" value="9"/>


    <div id="mouse-settings" style="display: none;">
        <div class="br-mainpanel">
            <div class="tab-content br-profile-body">
                <div class="br-pagetitle">
                    <i class="icon ion-mouse"></i>
                    <div>
                        <h4>Mouse Double Click Test</h4>
                        <p class="mg-b-0">Test your mouse for double click issue.</p>
                    </div>
                </div>

                <div class="br-pagebody">
                    <div class="row row-xs mg-t-25">
                        <div class="col-sm-4">
                            <div class="tx-center pd-y-15 bd bd-white-1">
                                <p class="mg-b-5 tx-uppercase tx-10 tx-mont tx-semibold">Clicks</p>
                                <h4 id="singleClickCount" class="tx-lato tx-white tx-bold mg-b-0">0</h4>
                            </div>
                        </div>
                        <!-- col-4 -->
                        <div class="col-sm-4 mg-t-20 mg-sm-t-0">
                            <div class="tx-center pd-y-15 bd bd-white-1">
                                <p class="mg-b-5 tx-uppercase tx-10 tx-mont tx-semibold">Double Click Count</p>
                                <h4 id="dobuleClickCount" class="tx-lato tx-white tx-bold mg-b-0">0</h4>
                            </div>
                        </div>
                        <!-- col-4 -->
                        <div class="col-sm-2 mg-t-20 mg-sm-t-0">
                            <div class="tx-center pd-y-15 ">
                                <a href="#" id="reset-counters" class="btn btn-outline-teal btn-icon mg-r-5" data-toggle="tooltip-teal" data-placement="top" title="Click To Reset Counters">
                                    <div>
                                        <i class="fa fa-redo"></i>
                                    </div>
                                </a>
                                <a href="#" class="btn btn-outline-teal btn-icon mg-r-5" data-toggle="tooltip-teal" data-placement="top" title="This will help you test your mouse for double click issues, click on the section below and continue clicking, if your mouse double clicks, it will detect it and show you.">
                                    <div>
                                        <i class="fa fa-info"></i>
                                    </div>
                                </a>
                            </div>
                        </div>
                        <!-- col-4 -->
                    </div>
                    <div id="mouse-test-div" class="br-section-wrapper mg-t-25 ht-590" onmousedown="GoTSkillZMouseDoubleClickFunctions.bindMouseClick();" oncontextmenu="return false;">
                        <h1 id="section-text" class="tx-lg-thin tx-gray-500">Click Here To Start</h1>
                    </div>

                </div>
            </div>
        </div>
    </div>

</asp:Content>