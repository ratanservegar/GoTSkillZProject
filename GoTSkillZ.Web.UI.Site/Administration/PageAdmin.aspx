<%@ Page Title="Page Admin" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="PageAdmin.aspx.cs" Inherits="GoTSkillZ.Web.UI.Site.Administration.PageAdmin" %>



<asp:Content ID="PageAdminContent" ContentPlaceHolderID="MainContent" runat="server">
    <%: Styles.Render("~/Content/Kendo/css", "~/Content/css") %>
    <%: Scripts.Render("~/bundles/Kendo") %>
  
  
    <input type="hidden" id="pageId" value="2"/>
    <div class="br-mainpanel">
        <div class="br-pagetitle">
            <i class="icon ion-ios-book-outline"></i>
            <div>
                <h4>Page Administration</h4>
                <p class="mg-b-0">Manage All Pages.</p>
            </div>
        </div>

        <div class="br-pagebody">
            <div id="pageAdminGrid"></div>
        </div>

      

    </div>
     
    <script src="../CustomScripts/AdminScripts/PageAdmin.js"></script>

</asp:Content>
