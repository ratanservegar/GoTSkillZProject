<%@ Page Title="Polling Rate Check" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="MousePollingRateChecker.aspx.cs" Inherits="GoTSkillZ.Web.UI.Site.MouseSettings.MousePollingRateChecker" %>


<asp:Content ID="MousePollingRate" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../CustomScripts/MouseSettings/MousePollingRateChecker.js"></script>
    <input type="hidden" id="pageId" value="10"/>
    <div id="mouse-settings" style="display: none;">
        <div class="br-mainpanel">
            <div class="tab-content br-profile-body">
                <div class="br-pagetitle">
                    <i class="icon ion-mouse"></i>
                    <div>
                        <h4>Mouse Polling Rate Checker</h4>
                        <p class="mg-b-0">Check your mouse polling rate.</p>
                    </div>
                </div>

                <div class="br-pagebody">
                    <div class="row row-xs mg-t-25">
                        <div class="col-sm-4">
                            <div class="tx-center pd-y-15 bd bd-white-1">
                                <p class="mg-b-5 tx-uppercase tx-10 tx-mont tx-semibold">Current</p>
                                <h4 id="current-poll-rate" class="tx-lato tx-white tx-bold mg-b-0">0 <span><font color="teal">Hz</font></span></h4>
                            </div>
                        </div>
                        <!-- col-4 -->
                        <div class="col-sm-4 mg-t-20 mg-sm-t-0">
                            <div class="tx-center pd-y-15 bd bd-white-1">
                                <p class="mg-b-5 tx-uppercase tx-10 tx-mont tx-semibold">Average</p>
                                <h4 id="avg-poll-rate" class="tx-lato tx-white tx-bold mg-b-0">0 <span><font color="teal">Hz</font></span></h4>
                            </div>
                        </div>
                        <!-- col-4 -->
                        <div class="col-sm-2 mg-t-20 mg-sm-t-0">
                            <div class="tx-center pd-y-15 ">

                                <a href="#" class="btn btn-outline-teal btn-icon mg-r-5" data-toggle="tooltip-teal" data-placement="top" title="Test your mouse polling rate. A higher polling rate is always better.">
                                    <div>
                                        <i class="fa fa-info"></i>
                                    </div>
                                </a>
                            </div>
                        </div>
                        <!-- col-4 -->
                    </div>
                    <div id="mouse-poll-test-div" class="br-section-wrapper mg-t-25 ht-590" onmousedown="GoTSkillZMousePollingRateFunctions.bindMousePointerEvent();" oncontextmenu="return false;">
                        <h1 id="mouse-poll-section-text" class="tx-lg-thin tx-gray-500">Click Here To Start polling</h1>
                    </div>

                </div>
            </div>


        </div>
    </div>


</asp:Content>