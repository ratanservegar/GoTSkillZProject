<%@ Page Title="Sensitivity Converter" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="SensitivityConverter.aspx.cs" Inherits="GoTSkillZ.Web.UI.Site.MouseSettings.SensitivityConverter" %>



<asp:Content ID="SensitivityConverterContent" ContentPlaceHolderID="MainContent" runat="server">
    <%: Styles.Render("~/Content/Kendo/css", "~/Content/css") %>
    <%: Scripts.Render("~/bundles/Kendo") %>
    

    <input type="hidden" id="pageId" value="12" />

    <div id="sensi-converter" style="display: none;">
        <div class="br-mainpanel">
            <div class="tab-content br-profile-body">
                <div class="br-pagetitle">
                    <i class="icon ion-mouse"></i>
                    <div>
                        <h4>Mouse Sensitivity Converter</h4>
                        <p class="mg-b-0">Different Game, Same Aim.</p>
                    </div>
                </div>

                <div class="br-pagebody">
                    <div class="container">
                        <div class="row ">
                            <div class="col-md-12">
                                <div class="form-layout form-layout-1">
                                    <div class="row mg-b-25">
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label class="form-control-label">Convert From </label>
                                                <input class="form-control form-control-dark" type="text" id="ddl-convert-from" />
                                            </div>
                                        </div>
                                        <!-- col-4 -->
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label class="form-control-label">Sensitivity</label>
                                                <input class="form-control form-control-dark" type="number" id="main-sensi" />
                                            </div>
                                        </div>
                                        <!-- col-4 -->
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label class="form-control-label">Mouse DPI</label>
                                                <input class="form-control form-control-dark" type="number" id="main-dpi" />
                                            </div>
                                        </div>
                                        <!-- col-4 -->
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label class="form-control-label">Convert To </label>
                                                <input class="form-control form-control-dark" type="text" id="ddl-convert-to" />
                                            </div>
                                        </div>
                                        <div class="col-lg-2" id="fov-div">
                                            <div class="form-group">
                                                <label class="form-control-label">FOV</label>
                                                <input class="form-control form-control-dark" type="number" id="pubg-fov"/>
                                            </div>
                                        </div>
                                        <div class="col-lg-2">
                                            <div class="form-group">
                                                <label class="form-control-label">Sensitivity</label>
                                                <input class="form-control form-control-dark" type="number" id="final-sensi" readonly />
                                            </div>
                                        </div>
                                        <div class="col-lg-2">
                                            <div class="form-group">
                                                <label class="form-control-label">Cm per 360°</label>
                                                <input class="form-control form-control-dark" type="number" id="cm-360" readonly />
                                            </div>
                                        </div>
                                        <div class="col-lg-2">
                                            <div class="form-group">
                                                <label class="form-control-label">In per 360°</label>
                                                <input class="form-control form-control-dark" type="number" id="inch-360" readonly />
                                            </div>
                                        </div>
                                        <!-- col-8 -->

                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="../CustomScripts/MouseSettings/SenstivityConverter.js"></script>
</asp:Content>
