<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ERP_CMS.Admin.BL.Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContents" runat="server">
    <style type="text/css">
        .style1 {
            width: 100%;
        }

        .Initial {
            display: block;
            padding: 4px 18px 4px 18px;
            float: left;
            background: url("../Images/InitialImage.png") no-repeat right top;
            color: Black;
            font-weight: bold;
        }

            .Initial:hover {
                color: White;
                background: url("../Images/SelectedButton.png") no-repeat right top;
            }

        .Clicked {
            float: left;
            display: block;
            background: url("../Images/SelectedButton.png") no-repeat right top;
            padding: 4px 18px 4px 18px;
            color: Black;
            font-weight: bold;
            color: White;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageHeading" runat="server">
    <h1 class="page-title">Bill of Lading</h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContents" runat="server">
    <form id="form1" class="form-horizontal fv-form fv-form-bootstrap" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
             <ContentTemplate>
            <tr>
                <td>
                    <asp:Button Text="Tab 1" BorderStyle="None" ID="Tab1" CssClass="Initial" runat="server" OnClick="Tab1_Click"/>
                    <asp:Button Text="Tab 2" BorderStyle="None" ID="Tab2" CssClass="Initial" runat="server" OnClick="Tab2_Click"/>
                    <asp:Button Text="Tab 3" BorderStyle="None" ID="Tab3" CssClass="Initial" runat="server" OnClick="Tab3_Click"/>
                    <asp:MultiView ID="MainView" runat="server">
                        <asp:View ID="View1" runat="server">
                            <span>View 1 </span>
                        </asp:View>
                        <asp:View ID="View2" runat="server">
                            <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">
                                <tr>
                                    <td>
                                        <h3>View 2
                                        </h3>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                        <asp:View ID="View3" runat="server">
                            <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">
                                <tr>
                                    <td>
                                        <h3>View 3
                                        </h3>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>
                </td>
            </tr>
             </ContentTemplate>
            </asp:UpdatePanel>   
        </table>
    </form>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterScriptContents" runat="server">
      <script type="text/javascript" src="<%= Page.ResolveClientUrl ("~/Admin/assets/kendo/kendo.all.min.js") %>"></script>
    <script src="<%= Page.ResolveClientUrl ("~/Admin/assets/vendor/formvalidation/formValidation.min.js") %>"></script>
    <script src="<%= Page.ResolveClientUrl ("~/Admin/assets/vendor/formvalidation/framework/bootstrap.min.js") %>"></script>

</asp:Content>
