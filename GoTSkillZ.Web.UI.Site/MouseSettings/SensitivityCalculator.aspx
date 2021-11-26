<%@ Page Title="Sensitivity Calculator" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="SensitivityCalculator.aspx.cs" Inherits="GoTSkillZ.Web.UI.Site.MouseSettings.SensitivityCalculator" %>


<asp:Content ID="SensitivityCalculatorContent" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="../Content/ionRangeSlider/ion.rangeSlider.css" type="text/css" />

    <input type="hidden" id="pageId" value="11" />

    <div id="sensi-calculator" style="display: none;">
        <div class="br-mainpanel">
            <div class="tab-content br-profile-body">
                <div class="br-pagetitle">
                    <i class="icon ion-mouse"></i>
                    <div>
                        <h4>Mouse Sensitivity Calculator</h4>
                        <p class="mg-b-0">Find Your Prefect Mouse Sensitivity.</p>
                    </div>
                </div>

                <div class="br-pagebody">
                    <div class="container">
                        <div class="row ">
                            <div class="col-md-6">
                                <h1 class="tx-teal" style="text-align: center;">eDPI</h1>
                                <div class="form-layout form-layout-1">
                                    <div class="form-group" data-toggle="tooltip-teal" data-placement="top" title="Dots per inch (DPI) is a measurement of how sensitive a mouse is. The higher a mouse’s DPI, the farther the cursor on your screen will move when you move the mouse. A mouse with a higher DPI setting detects and reacts to smaller movements.">
                                        <div class="input-group input-group-dark">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text"><i class="fas fa-mouse tx-16 lh-0 op-6"></i></span>
                                            </div>
                                            <input type="number" class="form-control" placeholder="DPI" id="edpi-DPI">
                                        </div>
                                    </div>
                                    <div class="form-group" data-toggle="tooltip-teal" data-placement="top" title="Enter your current game sensitivity">
                                        <div class="input-group input-group-dark">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text"><i class="fas fa-crosshairs tx-16 lh-0 op-6"></i></span>
                                            </div>
                                            <input type="number" class="form-control" placeholder="Sensitivity" id="edpi-sensi">
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <p class="br-section-label-sm">Raw Input?</p>
                                        <div class="input-group input-group-dark" data-toggle="tooltip-teal" data-placement="top" title='Raw Input "On" in CSGO ignores the Windows mouse settings.'>
                                            <input type="checkbox" id="edpi-raw" checked data-toggle="toggle" data-onstyle="info" data-offstyle="light">
                                        </div>
                                    </div>

                                    <div class="form-group" data-toggle="tooltip-teal" data-placement="top" title='This is your windows mouse speed, which can be found in Control panel -> Mouse -> Pointer Options.'>
                                        <p class="br-section-label-sm">Windows Sensitivity</p>

                                        <input type="text" class="js-range-slider" id="windows-slider" name="my_range" data-extra-classes="irs-outline irs-teal" value="" />
                                    </div>


                                    <hr style="border-top: 1px solid #17a2b8;" />
                                    <div class="form-group" data-toggle="tooltip-teal" data-placement="top" title='This is your final eDPI.'>
                                        <div class="input-group input-group-dark">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text"><i class="fas fa-ruler-combined tx-16 lh-0 op-6"></i></span>
                                            </div>
                                            <input type="number" class="form-control" placeholder="eDPI" readonly="" id="final-edpi">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <h1 class="tx-teal" style="text-align: center;">Sensitivity</h1>
                                <div class="form-layout form-layout-1">
                                    <div class="form-group" data-toggle="tooltip-teal" data-placement="top" title="Dots per inch (DPI) is a measurement of how sensitive a mouse is. The higher a mouse’s DPI, the farther the cursor on your screen will move when you move the mouse. A mouse with a higher DPI setting detects and reacts to smaller movements.">
                                        <div class="input-group input-group-dark">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text"><i class="fas fa-mouse tx-16 lh-0 op-6"></i></span>
                                            </div>
                                            <input type="number" class="form-control" placeholder="DPI" id="sensi-dpi">
                                        </div>
                                    </div>
                                    <div class="form-group" data-toggle="tooltip-teal" data-placement="top" title='eDPI stands for Effective DPI. This number describes your "real" sensitivity; use the eDPI calculator to find your eDPI.'>
                                        <div class="input-group input-group-dark">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text"><i class="fas fa-ruler-combined tx-16 lh-0 op-6"></i></span>
                                            </div>
                                            <input type="text" class="form-control" placeholder="eDPI" id="sensi-eDPI">
                                        </div>
                                    </div>
                                    <hr style="border-top: 1px solid #17a2b8;" />
                                    <div class="form-group" data-toggle="tooltip-teal" data-placement="top" title='This is your final game sensitivity.'>
                                        <div class="input-group input-group-dark">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text"><i class="fas fa-crosshairs tx-16 lh-0 op-6"></i></span>
                                            </div>
                                            <input type="number" class="form-control tx-teal" placeholder="Sensitivity" id="final-sensi" readonly="">
                                        </div>
                                    </div>
                                </div>
                                <div class="form-layout form-layout-1 mg-t-20">
                                    <div class="form-group" data-toggle="tooltip-teal" data-placement="top" title="Enter your mouse hz (polling rate)">
                                        <div class="input-group input-group-dark">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text"><i class="fab fa-galactic-republic tx-16 lh-0 op-6"></i></span>
                                            </div>
                                            <input type="number" class="form-control" placeholder="Mouse Hz (Polling Rate)" id="mouse-hz">
                                        </div>
                                    </div>
                                    <div class="form-group" data-toggle="tooltip-teal" data-placement="top" title="Saves your sensitivity, which can be showcased on your profile and  used for faceit match anaylsis">
                                        <div class="input-group input-group-dark">
                                            <button class="btn btn-teal btn-block mg-b-10" id="save-sensi-btn">Save CS:GO Sensitivity</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    <script src="../CustomScripts/MouseSettings/SensitivityCalculator.js"></script>
    <script src="../Scripts/ionRangeSlider/ion.rangeSlider.js"></script>
</asp:Content>
