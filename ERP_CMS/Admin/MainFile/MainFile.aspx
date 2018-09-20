<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Site.Master" AutoEventWireup="true" CodeBehind="MainFile.aspx.cs" Inherits="ERP_CMS.Admin.MainFile.MainFile" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContents" runat="Server">
    <%-- <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    --%>
    <link href="<%= Page.ResolveClientUrl ("~/Admin/assets/vendor/bootstrap-datepicker/bootstrap-datepicker.css") %>"
        rel="stylesheet" type="text/css" />
    <link href="<%= Page.ResolveClientUrl ("~/Admin/assets/vendor/select2/select2.css") %>"
        rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/Admin/assets/vendor/jquery-ui/jquery-ui.css") %>" />

    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageHeading" runat="Server">
    <h1 class="page-title">Manage File</h1>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContents" runat="Server">

    <form class="form-horizontal fv-form fv-form-bootstrap" id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <%-- <div class="panel">--%>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Label ID="lblStatus" runat="server"></asp:Label>
                <%--<div class="panel-body">--%>
                <div id="tabs" style="height: auto">
                    <ul>
                        <li><a href="#mainfile">Main File</a></li>
                        <li><a href="#blfile">BL File</a></li>
                        <li><a href="#container">Container info</a></li>
                    </ul>
                    <div id="mainfile" class ="form-group">
                        <div class ="form-group">
                            <label class="col-sm-2 control-label">
                                File Name:</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtMainFileName" name="txtMainFileName" runat="server"
                                    CssClass="form-control"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ID="rv5" runat="server" ControlToValidate="txtMainFileName"
                                    ErrorMessage="required!" ForeColor="Red" ValidationGroup="eml" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                            </div>
                            <label class="col-sm-2 control-label">
                                STI No:</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtSTI_No" name="txtSTI_No" runat="server"
                                    CssClass="form-control"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSTI_No"
                                    ErrorMessage="required!" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                            </div>
                        </div>
                        <div class="form-group">

                            <label class="col-sm-2 control-label">
                                Bill No:</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtBillNo" name="txtBillNo" runat="server"
                                    CssClass="form-control"></asp:TextBox>
                                <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtBillNo"
                                    ErrorMessage="required!" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                            </div>
                            <label class="col-sm-2 control-label">
                                Bill Date: <span style="color: Red;">*</span></label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtBillDate" name="txtBillDate" runat="server" data-fv-field="txtBillDate" CssClass="form-control"
                                    MaxLength="11" data-plugin="datepicker" data-format="dd-M-yyyy"></asp:TextBox>
                                <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtBillDate"
                                    ErrorMessage="Required!" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                --%>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">
                                Free Days:</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtFreeDays" name="txtFreeDays" runat="server"
                                    CssClass="form-control"></asp:TextBox>
                                <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtFreeDays"
                                    ErrorMessage="required!" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                            </div>
                            <label class="col-sm-2 control-label">
                                Doc Received By:</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtDocReceivedBy" name="txtDocReceivedBy" runat="server"
                                    CssClass="form-control"></asp:TextBox>
                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtDocReceivedBy"
                                    ErrorMessage="required!" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                            </div>
                        </div>
                        <div class="text-left col-sm-6">
                            <asp:Button ID="btnPrvBL" runat="server" Text="Previous" CssClass="btn btn-primary btnPrev mover"  />
                        </div>
                        <div class="text-right col-sm-6">
                            <asp:Button ID="btnNextBL" runat="server" Text="Next" CssClass="btn btn-primary btnNext mover"  />
                        </div>
                    </div>
                    <div id="blfile" class ="form-group">
                        <div class="text-left col-sm-6">
                            <asp:Button ID="Button2" runat="server" Text="Previous" CssClass="btn btn-primary btnPrev mover"  />
                        </div>
                       <div class="text-right col-sm-6">
                            <asp:Button ID="Button1" runat="server" Text="Next " CssClass="btn btn-primary btnNext mover"  />
                        </div>
                    </div>
                    <div id="container" class="form-group">
                        <div class="text-left col-sm-6">
                            <asp:Button ID="Button3" runat="server" Text="Previous" CssClass="btn btn-primary btnPrev mover"  />
                        </div>
                       <div class="text-right col-sm-6">
                            <asp:Button ID="Button4" runat="server" Text="Next " CssClass="btn btn-primary btnNext mover"  />
                        </div>
                     </div>
                </div>
                <%-- </div>--%>
            </ContentTemplate>
        </asp:UpdatePanel>
        <%--</div>--%>
    </form>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterScriptContents" runat="server">
    <script type="text/javascript" src="<%= Page.ResolveClientUrl ("~/Admin/assets/jquery/scripts/jquery-ui.js") %>"></script>
    <%--<script type="text/javascript" src="<%= Page.ResolveClientUrl ("~/Admin/assets/jquery/scripts/jquery-1.11.3.js") %>"></script>--%>
    <%--<script type="text/javascript" src="<%= Page.ResolveClientUrl ("~/Admin/assets/jquery/scripts/jquery-1.11.3.min.js") %>"></script>--%>
    <script type="text/javascript" src="<%= Page.ResolveClientUrl ("~/Admin/assets/js/components/panel.js") %>"></script>
    <script type="text/javascript" src="<%= Page.ResolveClientUrl("~/Admin/assets/vendor/jquery-ui/jquery-ui.js") %>"></script>
    <script type="text/javascript" src="<%= Page.ResolveClientUrl ("~/Admin/assets/js/components/bootstrap-datepicker.js") %>"></script>
    <script type="text/javascript" src="<%= Page.ResolveClientUrl ("~/Admin/assets/vendor/select2/select2.js") %>"></script>

    <script type="text/javascript">
        
        $(document).ready(function () {

            var $tabs = $('#tabs').tabs();

            $(".ui-tabs-panel").each(function (i) {

                var totalSize = $(".ui-tabs-panel").size() - 1;

                if (i != 0)
                {
                    document.getElementById('<%=btnPrvBL.ClientID %>').style.visibility = "Hidden";
                    document.getElementById('<%=btnNextBL.ClientID %>').style.visibility = "visible";
                }
                if (i != totalSize)
                {
                    document.getElementById('<%=Button3.ClientID %>').style.visibility = "visible";
                    document.getElementById('<%=Button4.ClientID %>').style.visibility = "Hidden";
                }

            });

            $(".btnNext").click(function () {
                $tabs.tabs("option", "active", $("#tabs").tabs('option', 'active') + 1);
                return false;
            });
            $(".btnPrev").click(function () {
                $tabs.tabs("option", "active", $("#tabs").tabs('option', 'active') - 1);
                return false;
            });
            
        });


    </script>

    <script type="text/javascript">
        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                jQuery("#<%=txtBillDate.ClientID %>").datepicker();
            }
        }

    </script>
</asp:Content>
