<%@ Page Title="FaceIT" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="FaceIT.aspx.cs" Inherits="GoTSkillZ.Web.UI.Site.CSGOConfigurations.FaceIT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jsrsasign/8.0.12/jsrsasign-all-min.js"></script>
    <script src="https://cdn.faceit.com/oauth/faceit-oauth-sdk-1.2.7.min.js"
        type="text/javascript"></script>
    <script src="/CustomScripts/CSGOConfigurations/Faceit.js"></script>
    <div class="br-mainpanel ">
        <div class="br-profile-body">

            <div class="br-pagetitle">
                <i class="icon-Black_Pheasant tx-teal" style="font-size: 50px"></i>
                <div>
                    <h3>Faceit  - Get Good</h3>
                </div>
            </div>


            <div class="br-pagebody" >
                <div id="faceit-container">
                    <div class="container">
                        <div class="row">
                            <div class="col-md-12">
                              
                                    <div class="card card-body  tx-white bd-0">
                                        <h3 class="card-text tx-teal ">Take Your Game To The Next Level</h3>
                                        <p>With faceit being the benchmark for high-level games it only makes sense that you have true insignt on your game statistics to help you point in the right direction to become a better player.</p>
                                        <p>We will enable you to not only view your face it game statistics but also give you insights on the following:</p>
                                        <ul>
                                            <li>Track how many Games have you won/lost with your current game sensitivity</li>
                                            <li>Get detailed insight on headshot and first shot accuracy with your current game sensitivity</li>
                                            <li>Get Detailed ELO progress</li>
                                        </ul>
                                        <p>and more....</p>
                                    </div>
                                
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12 pd-b-25">
                                <div class="card bd-gray-400 mg-t-20">
                                    <div class="card-header bg-transparent pd-x-25 pd-y-15 bd-b-0 d-flex justify-content-between align-items-center">
                                        <h6 class="card-title tx-uppercase tx-12 tx-white mg-b-0">Faceit - UNLOCK</h6>
                                    </div>
                                    <!-- card-header -->
                                    <div class="card-body pd-x-25 pd-b-25 pd-t-0">
                                        <div class="row no-gutters">
                                            <div class="col-md-12">
                                                <div id="faceit-goal-5000" class="card card-body rounded-0">
                                                    <h6 class="tx-teal tx-14 mg-b-5">Progress</h6>
                                                    <span class="tx-12">Started: 26/1/2020</span>
                                                    <span class="tx-12" style="display: none;"></span>
                                                    <div class="tx-center mg-y-20">
                                                        <span class="peity-donut faceit-pity-donut" data-peity="{ &quot;fill&quot;: [&quot;#17A2B8&quot;, &quot;#E9ECEF&quot;],  &quot;innerRadius&quot;: 50, &quot;radius&quot;: 80 }" style="display: none;" id="faceit-unlock-goal-1"></span>
                                                    </div>
                                                    <p class="tx-10 tx-uppercase tx-medium mg-b-0 tx-spacing-1">Current Subscriber Count</p>
                                                    <h2 class="tx-white tx-bold tx-lato">
                                                        <span id="faceit-unlock-goal-counter-1"></span>
                                                    </h2>
                                                    <div class="d-flex justify-content-between tx-12">
                                                        <div>
                                                            <span class="square-10 bg-info mg-r-5"></span>Current
                                                    <h5 class="mg-b-0 mg-t-5 tx-bold tx-white tx-lato" id="faceit-count-percentage-1"></h5>
                                                        </div>
                                                        <div>
                                                            <span class="square-10 bg-gray-300 mg-r-5"></span>Goal
                                                    <h5 class="mg-b-0 mg-t-5 tx-bold tx-white tx-lato">100%</h5>
                                                        </div>
                                                    </div>
                                                    <!-- d-flex -->
                                                </div>
                                                <!-- card -->
                                            </div>

                                            <!-- col-3 -->
                                        </div>
                                        <!-- row -->
                                    </div>
                                    <!-- card-body -->
                                </div>
                                <!-- card -->
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>




















        <%--            <div class="container">--%>
        <%--                <div class="row" style="padding-bottom: 25px;">--%>
        <%--                    <div class="col-lg-6 pd-b-25">--%>
        <%--                        <div class="card bd-gray-400 mg-t-20">--%>
        <%----%>
        <%--                            <!-- card-header -->--%>
        <%--                            <div class="card-body pd-x-25 pd-b-25 pd-t-0">--%>
        <%--                                <div class="row no-gutters">--%>
        <%--                                    <div class="col-md-6">--%>
        <%----%>
        <%--$1$                                   <div id="faceitLogin"></div>#1#--%>
        <%--                                        --%>
        <%--                                       --%>
        <%--                                        <!-- card -->--%>
        <%--                                    </div>--%>
        <%----%>
        <%----%>
        <%--                                    <!-- col-3 -->--%>
        <%----%>
        <%--                                </div>--%>
        <%--                                <!-- row -->--%>
        <%--                            </div>--%>
        <%--                            <!-- card-body -->--%>
        <%--                        </div>--%>
        <%--                        <!-- card -->--%>
        <%--                    </div>--%>
        <%----%>
        <%----%>
        <%--                    <!-- col-lg-8 -->--%>
        <%--                </div>--%>
        <%----%>
        <%--                <!-- row -->--%>
        <%--            </div>--%>
    </div>



</asp:Content>
