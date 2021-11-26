<%@ Page Title="Vod's" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="VODS.aspx.cs" Inherits="GoTSkillZ.Web.UI.Site.Livestream.VODS" %>

<asp:Content ID="VODS" ContentPlaceHolderID="MainContent" runat="server">
    
    <link rel="stylesheet" href="/Content/lightgallery/css/lightgallery.min.css" type="text/css" />
    <link rel="stylesheet" href="/Content/lightgallery/css/lg-transitions.min.css" type="text/css" />
    
 

    <div id="youtube-vods">
        <div class="br-mainpanel">
            <div class="tab-content br-profile-body">
                <div class="br-pagetitle">
                   <img src="/CustomContent/Images/youtube.png"/>
                    <div>
                        <h4 style="margin: 0px !important">Video's On Demand</h4>
                    </div>
                </div>

                <div class="br-pagebody" >

                    <div class="row text-center text-lg-left" id="playlist-grid-container">
                    </div>

                </div>
            </div>
        </div>
    </div>
    
    <script src="../CustomScripts/LiveSream/Vods.js"></script>
    <script src="../Scripts/Lightgallery/lightgallery.min.js" ></script>
    <script src="../Scripts/Lightgallery/lg-thumbnail.min.js" ></script>
    <script src="../Scripts/Lightgallery/lg-video.min.js" ></script>
</asp:Content>