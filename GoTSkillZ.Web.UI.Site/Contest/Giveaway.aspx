<%@ Page Title="Giveaway" Language="C#" MasterPageFile="~/MasterPages/Site.Master" %>

<asp:Content runat="server" ID="Giveaway" ContentPlaceHolderID="MainContent">
  
    <%: Scripts.Render("~/bundles/summernote") %>
    
    
    <link rel="stylesheet" href="../Content/SummerNote/summernote-lite-flatly.css" type="text/css" />
    
   
    <input type="hidden" id="pageId" value="16"/>

    <div class="br-mainpanel">

        <div class="br-pagetitle">
            <i class="icon icon fas fa-gift " style="font-size: 3em" id="giveaway-icon"></i>
            <div>
                <h3 id="stream-title" style="margin: 0px !important">Giveaway</h3>
            </div>

        </div>
        <div class="tx-right pd-r-30" id="edit-giveaway">
            <div class="t-15 r-25">
                <a href="" class="tx-white-5 hover-info">
                    <i class="icon ion-edit tx-16"></i>
                </a>
            </div>
        </div>
        <div class="br-pagebody">
            <div class="container-fluid">
                <div class="row" id="giveaway-container">
                </div>
            </div>

        </div>


        <div id="enter-giveaway-modal" class="modal fade effect-fall " style="display: none;" aria-hidden="true">
            <div class="modal-dialog modal-lg modal-dialog-centered" role="document">
                <div class="modal-content bd-0 tx-14">
                    <div class="modal-header pd-x-25">
                        <div class="row pd-b-20">
                            <div class="col-md-12">
                                <h6 class="tx-14 mg-b-0 tx-uppercase tx-bold" id="enter-giveaway-title"></h6>
                            </div>
                            <div class="col-md-12">
                                <h6 class="tx-14 mg-b-0 tx-uppercase tx-bold" id="enter-giveaway-description"></h6>
                            </div>
                        </div>

                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">×</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class=" form-layout-1" style="padding: 10px !important;">
                            <div class="row pd-b-20">
                                <div class="col-md-12">
                                    <div class="input-group input-group-dark">

                                        <p class="mg-b-5" id="enter-giveaway-rule"></p>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button id="generate-giveawaycode" type="button" class="btn btn-primary tx-11 tx-uppercase pd-y-12 pd-x-25 tx-mont tx-semibold" style="width: 100%"></button>

                        <div class="input-group mb-3" id="giveaway-code-container" style="display: none;">
                            <input type="text" id="giveaway-code-input" class="form-control" readonly="readonly" style="font-weight: bold; text-align: center;">
                        </div>
                    </div>
                </div>
            </div>
            <!-- modal-dialog -->
        </div>


        <div id="giveaway-add-modal" class="modal fade ">
            <div class="modal-dialog modal-lg pd-10" role="document">
                <div class="modal-content">
                    <div class="modal-header pd-y-20 pd-x-25">
                        <h6 class="tx-14 mg-b-0 tx-white tx-bold">Giveaway Add/Edit</h6>
                    </div>
                    <div class="modal-body">
                        <div class="container-fluid  form-layout-1">
                            <div class="row pd-b-20">
                                <div class="col-md-3">
                                    <div class="input-group input-group-dark">
                                        <label class="form-control-label">Giveaways</label>
                                        <select id="giveaway-list"></select>
                                    </div>

                                </div>
                                <div class="col-md-3">
                                    <div class="input-group input-group-dark">
                                        <a id="add-giveaway" href="#" class="tx-white-5 hover-info "><i class="icon ion-plus tx-12"></i>&nbsp;ADD Giveaway</a>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <label class="form-control-label">Giveaway Id</label>
                                        <input type="text" class="form-control form-control-dark" id="giveaway-id" readonly="readonly">
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <label class="form-control-label">Code</label>
                                        <input type="text" class="form-control form-control-dark " id="giveaway-code">
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <label class="form-control-label">Title</label>
                                        <input type="text" class="form-control form-control-dark" id="giveaway-title">
                                    </div>
                                </div>
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <label class="form-control-label">Description</label>
                                        <input type="text" class="form-control form-control-dark" id="giveaway-desc">
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <label class="form-control-label">Image Url</label>
                                        <input type="text" class="form-control form-control-dark" id="giveaway-imageurl">
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <label class="form-control-label">Video Url</label>
                                        <input type="text" class="form-control form-control-dark" id="giveaway-videourl">
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <label class="form-control-label">Total Entries</label>
                                        <input type="text" class="form-control form-control-dark" id="giveaway-entries">
                                    </div>
                                </div>
                            </div>
                            <div class="row pd-b-20">
                                <div class="col-lg-12 mg-b-5">
                                    <div class="input-group input-group-dark">
                                        <textarea rows="5" class="form-control form-control-dark" id="giveaway-rule" placeholder="Rules"></textarea>
                                    </div>
                                    <!-- input-group -->
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <label class="ckbox">
                                            <input type="checkbox" id="giveaway-sponsored"><span>Sponsored?</span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <label class="ckbox">
                                            <input type="checkbox" id="giveaway-international"><span>International?</span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <label class="ckbox">
                                            <input type="checkbox" id="giveaway-active"><span>Active?</span>
                                        </label>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary tx-11 tx-uppercase pd-y-12 pd-x-25 tx-mont tx-semibold" data-dismiss="modal">Close</button>
                        <button type="button" id="save-giveaway" class="btn btn-teal tx-11 tx-uppercase pd-y-12 pd-x-25 tx-mont tx-semibold" data-id="0">Save</button>
                    </div>
                </div>
            </div>
        </div>


    </div>

    <script src="../CustomScripts/Giveaway/Giveaway.js"></script>
</asp:Content>