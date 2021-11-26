<%@ Page Title="Winners" Language="C#" MasterPageFile="~/MasterPages/Site.Master" %>

<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
    <script src="../CustomScripts/Giveaway/Winners.js"></script>
    <script src="../Scripts/party.min.js"></script>

    <style>

        .winner-text {
            width: 100% !important;
            font-size: 1.5rem !important;
        }
        .giveaway-entry-list {
            width: 300px;
            position: absolute;
        }

        .giveaway-entry-list {
            position: relative;
            display: block;
            border: 1px solid #1caf9a;
            margin-bottom: 5px;
            padding: 10px;
            text-align: center;
            text-transform: uppercase;
            -webkit-animation: fadeIn 0.5s linear;
            animation: fadeIn 0.5s linear;
            -webkit-animation-fill-mode: both;
            animation-fill-mode: both;
        }

        .item-1 {
            -webkit-animation-delay: 0.25s;
            animation-delay: 0.25s;
        }

        .item-2 {
            -webkit-animation-delay: 0.5s;
            animation-delay: 0.5s;
        }

        .item-3 {
            -webkit-animation-delay: 0.75s;
            animation-delay: 0.75s;
        }

        .item-4 {
            -webkit-animation-delay: 1s;
            animation-delay: 1s;
        }

        .item-5 {
            -webkit-animation-delay: 1.25s;
            animation-delay: 1.25s;
        }

        @-webkit-keyframes fadeIn {
            0% {
                opacity: 0;
                top: 100px;
            }

            75% {
                opacity: 0.5;
                top: 0px;
            }

            100% {
                opacity: 1;
            }
        }
    </style>
    <input type="hidden" id="pageId" value="17" />


    <div class="br-mainpanel">

        <div class="br-pagetitle">
            <i class="icon icon fas fa-award " style="font-size: 3em" id="winners-icon"></i>
            <div>
                <h3 id="stream-title" style="margin: 0px !important">Winners</h3>
            </div>

        </div>
        <div class="tx-right pd-r-30" id="edit-giveaway-winners">
            <div class="t-15 r-25">
                <a href="" class="tx-white-5 hover-info">
                    <i class="icon ion-edit tx-16"></i>
                </a>
            </div>
        </div>
        <div class="br-pagebody">
            <div class="container-fluid">
                <div class="row" id="giveaway-winners-container">
                </div>
            </div>

        </div>



        <div id="pick-winner-modal" class="modal fade " data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog modal-lg pd-10" role="document">
                <div class="modal-content">
                    <div class="modal-header pd-y-20 pd-x-25">
                        <h6 class="tx-14 mg-b-0 tx-white tx-bold">Pick Winner</h6>
                    </div>
                    <div class="modal-body">
                        <div class="container-fluid ">
                            <div class="row pd-b-20">
                                <div class="col-md-3">
                                    <div class="input-group input-group-dark">
                                        <label class="form-control-label">Giveaways</label>
                                        <select id="giveaway-list"></select>
                                    </div>
                                </div>
                            </div>
                            <div class="row" id="entry-list-container">

                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary tx-11 tx-uppercase pd-y-12 pd-x-25 tx-mont tx-semibold" data-dismiss="modal">Close</button>
                        <button type="button" id="pick-winner" class="btn btn-teal tx-11 tx-uppercase pd-y-12 pd-x-25 tx-mont tx-semibold">Get Entries</button>
                    </div>
                </div>
            </div>
        </div>


    </div>



</asp:Content>
