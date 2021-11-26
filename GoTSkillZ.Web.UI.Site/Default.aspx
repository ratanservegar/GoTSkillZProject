<%@ Page Title="Home" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GoTSkillZ.Web.UI.Site._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="br-mainpanel ">
        <div class="br-profile-body">
            <div class="container-fluid">
                <div class="row" style="padding-top: 35px;">
                    <div class="col-md-12" style="padding-bottom: 25px;">
                        <div class="card pd-0">
                            <div class="row no-gutters">
                                <div class="col-md-12 col-lg-2 pd-20-force">
                                    <a href="/User/Profile?UID=1" target="_blank">
                                        <img src="/CustomContent/Images/myprofile.jpg" class=" wd-md-100p" style="width: 12em; height: 13em;" alt="">
                                    </a>
                                </div>
                                <div class="col-md-9 col-lg-9 pd-20-force">
                                    <div>
                                        <h3 class="card-text tx-teal ">You GotSkillZ?</h3>
                                    </div>
                                    <h5 class="tx-normal mg-y-5">
                                        <a href="" class="tx-white">Welcome,</a>
                                    </h5>
                                    <p>
                                        gotskillz.gg was created as an additional companion tool for all my stream subscribers and sponsors.
                                        I set out building the website with the intention to build a community around my stream viewers to showcase their skillz, gaming setups & help them get to the next level,
                                        The site is solely being designed and built by myself during my free time and would continue to add more tools and features as the channel grows with your support.
                                    </p>
                                    <ul>
                                        <li>To know more about me you can go to <a href="/User/Profile?UID=1" target="_blank" class="tx-teal" style="font-size: 20px">My Profile</a> or check out the <a href="/Footer/FAQ.aspx" target="_blank" class="tx-teal" style="font-size: 20px">FAQ</a> section.</li>
                                    </ul>
                                    <ul>
                                        <li>To know more about the upcoming <a href="/Footer/Roadmap.aspx" target="_blank" class="tx-teal" style="font-size: 20px">Features & Roadmap </a>of the site please see the roadmap section.</li>
                                    </ul>
                                    <p style="color: teal; font-size: 15px; font-weight: bolder;">Thank you to all who subscribed to the channel & supported me&#128149;</p>
                                </div>
                                <!-- col-5 -->
                            </div>
                            <!-- row -->
                        </div>
                    </div>
                </div>
                <div class="row" style="padding-bottom: 25px;">
                    <div class="col-md-3 pd-b-30">
                        <div class="card bd-gray-400 mg-t-20">
                            <div class="card-header bg-transparent pd-x-25 pd-y-15 bd-b-0 d-flex justify-content-between align-items-center">
                                <h6 class="card-title tx-uppercase tx-12 tx-white mg-b-0">Subscriber Goals</h6>
                            </div>
                            <!-- card-header -->
                            <div class="card-body pd-x-25 pd-b-25 pd-t-0">
                                <div class="row no-gutters">
                                    <div class="col-md-12">
                                        <div id="sub-goal-2000" class="card card-body rounded-0">
                                            <h6 class="tx-teal tx-14 mg-b-5">Active Goal</h6>
                                            <span class="tx-12">Started: 24/3/2021</span>
                                            <span class="tx-12" style="display: none;"></span>
                                            <div class="tx-center mg-y-20">
                                                <span class="peity-donut sub-pity-donut" data-peity="{ &quot;fill&quot;: [&quot;#17A2B8&quot;, &quot;#E9ECEF&quot;],  &quot;innerRadius&quot;: 45, &quot;radius&quot;: 80 }" style="display: none;" id="sub-count-goal-2"></span>
                                            </div>
                                            <p class="tx-10 tx-uppercase tx-medium mg-b-0 tx-spacing-1">Current Subscriber Count</p>
                                            <h2 class="tx-white tx-bold tx-lato">
                                                <span id="sub-count-goal-counter-2"></span>
                                            </h2>
                                            <div class="d-flex justify-content-between tx-12">
                                                <div>
                                                    <span class="square-10 bg-info mg-r-5"></span>Current
                                                    <h5 class="mg-b-0 mg-t-5 tx-bold tx-white tx-lato" id="sub-count-percentage-2"></h5>
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
                                </div>
                                <!-- row -->
                            </div>
                            <!-- card-body -->
                        </div>
                    </div>
                    <div class="col-md-3 pd-b-30">
                        <div class="card bd-gray-400 mg-t-20">
                            <div class="card-header bg-transparent pd-x-25 pd-y-15 bd-b-0 d-flex justify-content-between align-items-center">
                                <h6 class="card-title tx-uppercase tx-12 tx-white mg-b-0">Subscriber Goals</h6>
                            </div>
                            <!-- card-header -->
                            <div class="card-body pd-x-25 pd-b-25 pd-t-0">
                                <div class="row no-gutters">
                                    <div class="col-md-12">
                                        <div id="sub-goal-1000" class="card card-body rounded-0">
                                            <h6 class="tx-teal tx-14 mg-b-5">Completed Goal</h6>
                                            <span class="tx-12">Started: 26/1/2020 | Achieved: 24/3/2021</span>
                                            <span class="tx-12" style="display: none;"></span>
                                            <div class="tx-center mg-y-20">
                                                <span class="peity-donut sub-pity-donut" data-peity="{ &quot;fill&quot;: [&quot;#17A2B8&quot;, &quot;#E9ECEF&quot;],  &quot;innerRadius&quot;: 45, &quot;radius&quot;: 80 }" style="display: none;" id="sub-count-goal-1"></span>
                                            </div>
                                            <p class="tx-10 tx-uppercase tx-medium mg-b-0 tx-spacing-1">Current Subscriber Count</p>
                                            <h2 class="tx-white tx-bold tx-lato">
                                                <span id="sub-count-goal-counter-1"></span>
                                            </h2>
                                            <div class="d-flex justify-content-between tx-12">
                                                <div>
                                                    <span class="square-10 bg-info mg-r-5"></span>Current
                                                    <h5 class="mg-b-0 mg-t-5 tx-bold tx-white tx-lato" id="sub-count-percentage-1"></h5>
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
                                </div>
                                <!-- row -->
                            </div>
                            <!-- card-body -->
                        </div>
                    </div>
                    <div class="col-md-3 pd-b-30">
                        <div class="card bd-gray-400 mg-t-20" id="yTsubs">
                            <div class="card-header bg-transparent pd-20 bd-white-1">
                                <h6 class="card-title tx-uppercase tx-12 tx-white mg-b-0">Recent Subscribers</h6>
                            </div>
                            <!-- card-header -->
                            <table class="table mg-b-0 tx-12">
                                <tbody id="recent-sub-tbody">
                                </tbody>
                            </table>
                        </div>
                        <!-- card -->
                    </div>
                    <div class="col-md-3 pd-b-30">
                        <div class="card bd-gray-400 mg-t-20" id="donations">
                            <div class="card-header bg-transparent pd-20 bd-white-1">
                                <h6 class="card-title tx-uppercase tx-12 tx-white mg-b-0">Donations</h6>
                            </div>
                            <!-- card-header -->
                            <table class="table mg-b-0 tx-12">
                                <tbody id="donation-tbody">
                                </tbody>
                            </table>
                        </div>
                        <!-- card -->
                    </div>
                    <div class="col-md-3 pd-b-30">
                        <div class="card bd-gray-400 mg-t-20" id="siteUsers">
                            <div class="card-header bg-transparent pd-20 bd-white-1">
                                <h6 class="card-title tx-uppercase tx-12 tx-white mg-b-0">New Site Users</h6>
                            </div>
                            <!-- card-header -->
                            <table class="table mg-b-0 tx-12">
                                <tbody id="new-site-tbody">
                                </tbody>
                            </table>
                        </div>
                        <!-- card -->
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>