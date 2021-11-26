<%@ Page Title="Live" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="Live.aspx.cs" Inherits="GoTSkillZ.Web.UI.Site.Livestream.Live" %>

<asp:Content ID="Live" ContentPlaceHolderID="MainContent" runat="server">

    <script src="https://www.youtube.com/player_api"></script>
    <script src="../CustomScripts/LiveSream/Livestream.js"></script>

    <div id="youtube-livestream">
        <div class="br-mainpanel">
            <div class="tab-content br-profile-body">
                <div class="br-pagetitle">
                    <img src="/CustomContent/Images/youtube.png" id="youtube-livestream-icon"/>
                    
                    <div>
                        <h3 id="stream-title" style="margin: 0px !important">Not Live</h3>
                    </div>
                </div>

                <div class="br-pagebody" style="height: 100vh">
                    <div class="container-fluid" id="youtube-livestream-container">
                        <div class="row ">
                            <div class="col-sm-8">
                                <div id="youtube-livestream-div" class="embed-responsive embed-responsive-16by9">
                                </div>
                                <div>
                                    <h4>NOTE:If Video Quality is stuck at 360p, please click on the youtube icon in the player to watch on youtube.com in higher quality</h4>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div id="youtube-livestream-chat-div" style="height: 100%;">
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>

</asp:Content>