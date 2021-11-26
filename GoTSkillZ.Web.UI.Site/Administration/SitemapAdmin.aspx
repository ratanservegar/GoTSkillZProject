<%@ Page Title="Sitemap Admin" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="SitemapAdmin.aspx.cs" Inherits="GoTSkillZ.Web.UI.Site.Administration.SitemapAdmin" %>


<asp:Content ID="SitemapContent" ContentPlaceHolderID="MainContent" runat="server" OnClientClick="return false;">
    <%: Styles.Render("~/Content/Kendo/css", "~/Content/css") %>
    <%: Scripts.Render("~/bundles/Kendo") %>
   
    <input type="hidden" id="pageId" value="1"/>

    <div id="sitemap-content" style="display: none;">
        <div class="br-subleft">
            <div class="pd-l-50 pd-t-10">
                <div class="btn-group" role="group" aria-label="Basic example">
                    <button id="lt-add-button" type="button" class="btn btn-teal  dropdown-toggle" data-toggle="dropdown" title="Add Sitemap">
                        <i class="far fa-plus-square"></i>
                    </button>
                    <ul class="dropdown-menu br-menu-sub" role="menu">
                        <li id="lt-folder-sitemap-item" class="sub-item">
                            <a href="#" id="1" class="sub-link" onclick="GoTSkillZSitemapFormFunctions.newForm(this)">Root Item</a>
                        </li>
                        <li id="lt-page-sitemap-item " class="disabled sub-item">
                            <a href="#" id="2" class="sub-link" onclick="GoTSkillZSitemapFormFunctions.newForm(this)">Page</a>
                        </li>
                        <li id="lt-link-sitemap-item" class="sub-item">
                            <a href="#" id="3" class="sub-link" onclick="GoTSkillZSitemapFormFunctions.newForm(this)">Link</a>
                        </li>
                    </ul>
                    <button type="button" id="lt-edit-button" onclick="GoTSkillZSitemapFormFunctions.ShowSitemapEditor(this)" title="Edit selected object" class="btn btn-teal">
                        <i class="far fa-edit"></i>
                    </button>

                </div>
            </div>
            <div class="col-md-12">
                <h4>Sitemap Tree</h4>
                <div id="treeViewContainer">
                    <div id="treeview"></div>
                </div>
            </div>
        </div>
        <!-- br-subleft -->


        <div class="br-contentpanel">
            <div class="br-pagetitle">
                <i class="icon ion-map"></i>
                <div>
                    <h4>Sitemap Administration</h4>
                    <p class="mg-b-0">Manage Sitemaps.</p>
                </div>
            </div>

            <div class="d-flex align-items-center justify-content-start pd-x-20 pd-sm-x-30 pd-t-25 mg-b-20 mg-sm-b-30">

                <button id="showSubLeft" class="btn btn-secondary mg-r-10 hidden-lg-up">
                    <i class="fa fa-arrow-circle-left"></i>
                </button>


            </div>
            <!-- d-flex -->

            <div class="br-pagebody">
                <div class="br-section-wrapper">
                    <div class="row mg-t-40">
                        <div class="col-xl-12">
                            <div class="form-layout-4">
                                <h6 class="br-section-label">Sitemap Properties</h6>
                                <div class="row">
                                    <input type="hidden" class="form-control" id="sitemapId"/>
                                </div>
                                <div class="row">
                                    <label class="col-sm-4 form-control-label">Title: <span class="tx-danger">*</span></label>
                                    <div class="col-sm-8 mg-t-10 mg-sm-t-0">
                                        <input type="text" id="sitemapName" class="form-control form-control-dark" placeholder="Sitemap Title">
                                    </div>
                                </div>
                                <!-- row -->
                                <div class="row mg-t-20">
                                    <label class="col-sm-4 form-control-label">Sitemap Type: <span class="tx-danger">*</span></label>
                                    <div class="col-sm-8 mg-t-10 mg-sm-t-0">
                                        <select class="form-control" id="sitemapType" required="true">
                                            <option value="-1">Select</option>
                                            <option value="1">Root Item</option>
                                            <option value="2">Page</option>
                                            <option value="3">Link</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="row mg-t-20">
                                    <label class="col-sm-4 form-control-label">Parent: <span class="tx-danger">*</span></label>
                                    <div class="col-sm-8 mg-t-10 mg-sm-t-0">
                                        <select class="form-control" id="sitemapParent" required="true">
                                            <option value="-1">Select</option>
                                            <option value="0">None</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="row mg-t-20">
                                    <label class="col-sm-4 form-control-label">Page: <span class="tx-danger">*</span></label>
                                    <div class="col-sm-8 mg-t-10 mg-sm-t-0">
                                        <select class="form-control" id="sitemapPage" required="true">
                                            <option value="-1">Select</option>
                                            <option value="0">None</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="row mg-t-20">
                                    <label class="col-sm-4 form-control-label">Alternate URL: <span class="tx-danger">*</span></label>
                                    <div class="col-sm-8 mg-t-10 mg-sm-t-0">
                                        <input type="text" id="sitemapUrl" class="form-control form-control-dark" placeholder="Alternate URL">
                                    </div>
                                </div>
                                <div class="row mg-t-20">
                                    <label class="col-sm-4 form-control-label">Icon: <span class="tx-danger">*</span></label>
                                    <div class="col-sm-8 mg-t-10 mg-sm-t-0">
                                        <input type="text" id="site-icon" class="form-control form-control-dark" placeholder="Icon">
                                    </div>
                                </div>
                                <div class="row mg-t-20">
                                    <label class="col-sm-4 form-control-label">Sort Order: <span class="tx-danger">*</span></label>
                                    <div class="col-sm-8 mg-t-10 mg-sm-t-0">
                                        <input type="text" id="sitemapSortOrder" class="form-control form-control-dark" placeholder="Sort Order">
                                    </div>
                                </div>
                                <div class="row mg-t-20">
                                    <label class="col-sm-4 form-control-label">Active?: <span class="tx-danger">*</span></label>
                                    <div class="col-sm-8 mg-t-10 mg-sm-t-0">
                                        <div id="sitemapIsActive" class="br-toggle br-toggle-info off">
                                            <div class="br-toggle-switch"></div>
                                        </div>

                                    </div>
                                </div>
                                <div class="form-layout-footer mg-t-30">
                                    <button type="button" onclick="GoTSkillZSitemapFormFunctions.validateSitemapData(this)" class="btn btn-teal">Save</button>
                                    <button type="button" class="btn btn-secondary">Cancel</button>
                                </div>
                            </div>

                        </div>

                    </div>
                    <!-- row -->
                </div>

            </div>

        </div>

    </div>

    <script src="../CustomScripts/AdminScripts/SitemapAdmin.js"></script>
</asp:Content>