<%@ Page Title="Road Map" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="Roadmap.aspx.cs" Inherits="GoTSkillZ.Web.UI.Site.Roadmap.Roadmap" %>



<asp:Content ID="RoadmapContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="br-mainpanel">
        <div class="br-pagetitle">
            <i class="fas fa-road tx-teal" style="font-size: 50px;"></i>
            <div>
                <h3 style="margin-top: 0;">Roadmap</h3>
                <p class="mg-b-0">Last Updated On 13/02/2020.</p>
            </div>
        </div>

        <div class="br-pagebody">
            <div id="privacy-container">
                <div class="container">
                    <div class="row">
                        <div class="col-md-12">
                            <div>
                                <div class="card card-body  tx-white bd-0">
                                    <h3 class="card-text tx-teal ">Future Features</h3>
                                    <p>Following are some features that are either currently released or currently in development.</p>
                                    <p>Please be informed things might change or be cancelled entirely depening on various factors like technical drawbacks or other, without any advance notice.</p>
                                    <p>Some features might be unlocked with respective goals being met.</p>


                                    <h3 class="card-text tx-teal ">Features</h3>
                                    <ul>
                                        <li>User Profile 
                                            <ul>
                                                <li>About me - <b class="tx-success">RELEASED</b></li>
                                                <li>Achievement Showcase - <b class="tx-success">RELEASED</b></li>
                                                <li>Contact Info - <b class="tx-success">RELEASED</b></li>
                                                <li>Player Profile - <b class="tx-success">RELEASED</b></li>
                                                <li>Team History - <b class="tx-success">RELEASED</b></li>
                                                <li>Setup Showcase - <b class="tx-success">RELEASED</b></li>
                                                <li>Peripheral Showcase - <b class="tx-success">RELEASED</b></li>
                                                <li>Game Settings Showcase - <b class="tx-success">RELEASED</b></li>
                                                <li>Profile Sharing - <b class="tx-danger">UNRELEASED</b></li>
                                            </ul>
                                        </li>
                                        <li>Mouse Tools
                                             <ul>
                                                 <li>Double Click Test - <b class="tx-success">RELEASED</b></li>
                                                 <li>Polling Rate Test - <b class="tx-success">RELEASED</b></li>
                                                 <li>Sensitivity Calculator - <b class="tx-success">RELEASED</b></li>
                                             </ul>
                                        </li>
                                        <li>CS:GO
                                             <ul>
                                                 <li>CS:GO Config Parser - <b class="tx-success">RELEASED</b></li>
                                                 <li>CS:GO Sensitivity insignts - <b class="tx-danger">UNRELEASED</b></li>
                                                 <li>Faceit insignts - <b class="tx-danger">UNRELEASED</b></li>
                                             </ul>
                                        </li>
                                        <li>Scheduler
                                             <ul>
                                                 <li>Stream scheduler - <b class="tx-danger">UNRELEASED</b></li>
                                                 <li>Solo Practice scheduler - <b class="tx-danger">UNRELEASED</b></li>
                                                 <li>Team Practice Scheduler - <b class="tx-danger">UNRELEASED</b></li>
                                                 <li>Team Match Scheduler - <b class="tx-danger">UNRELEASED</b></li>
                                                 <li>Event Calendar - <b class="tx-danger">UNRELEASED</b></li>
                                             </ul>
                                        </li>
                                        <li>Misc
                                            <ul>
                                                <li>Gamer Spotlights/highlights - <b class="tx-danger">UNRELEASED</b></li>
                                                <li>Streamer Spotlights/highlights - <b class="tx-danger">UNRELEASED</b></li>
                                                <li>Team Profiles - <b class="tx-danger">UNRELEASED</b></li>
                                                <li>Active Player Team search - <b class="tx-danger">UNRELEASED</b></li>
                                                <li>Site Latest updates notifications/newsletter - <b class="tx-danger">UNRELEASED</b></li>
                                                <li>Giveaway spotlight - <b class="tx-danger">UNRELEASED</b></li>
                                                <li>Site wide chat - <b class="tx-danger">UNRELEASED</b></li>
                                                <li>Guides - <b class="tx-danger">UNRELEASED</b></li>
                                                <li>Pro match tickers - <b class="tx-danger">UNRELEASED</b></li>
                                                <li>Gaming news - <b class="tx-danger">UNRELEASED</b></li>
                                            </ul>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
