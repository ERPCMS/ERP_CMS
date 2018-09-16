<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ERP_CMS.Admin.Default" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContents" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageHeading" runat="Server">
    <h1 class="page-title">
        Dashboard</h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContents" runat="Server">
    <form class="form-horizontal fv-form fv-form-bootstrap" id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="panel">
        <div class="panel-body">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="col-sm-3" id="divBLDashboard" runat="server" style="height: 200px; border: black solid;
                        text-align: center; padding: 0; margin-right: 15px; margin-bottom: 15px;cursor:pointer;" onclick="fnDashboardIconClick(1);">
                        <div class="row" style="height: 65%; margin-right: 0; margin-left: 0;">
                            <span class="glyphicon glyphicon-list-alt" style="font-size: 100px; padding-top: 15px;">
                            </span>
                        </div>
                        <div class="row" style="height: 35%; margin-right: 0; margin-left: 0; border-top: solid 1px;
                            padding-top: 20px; color: rgba(163, 175, 183, .9); background: #001633;">
                            <span style="font-size: 20px;">Bill of Lading</span></div>
                    </div>
                    <div class="col-sm-3" id="divManageUsersDashboard" runat="server" style="height: 200px; border: black solid;
                        text-align: center; padding: 0; margin-right: 15px; margin-bottom: 15px;cursor:pointer;" onclick="fnDashboardIconClick(2);">
                        <div class="row" style="height: 65%; margin-right: 0; margin-left: 0;">
                            <span class="glyphicon glyphicon-saved" style="font-size: 100px; padding-top: 15px;">
                            </span>
                        </div>
                        <div class="row" style="height: 35%; margin-right: 0; margin-left: 0; border-top: solid 1px;
                            padding-top: 20px; color: rgba(163, 175, 183, .9); background: #001633;">
                            <span style="font-size: 20px;">Manage Users</span></div>
                    </div>
                    <div class="col-sm-3" id="divManageRightsDashboard" runat="server" style="height: 200px; border: black solid;
                        text-align: center; padding: 0; margin-right: 15px; margin-bottom: 15px;cursor:pointer;" onclick="fnDashboardIconClick(3);">
                        <div class="row" style="height: 65%; margin-right: 0; margin-left: 0;">
                            <span class="glyphicon glyphicon-log-out" style="font-size: 100px; padding-top: 15px;">
                            </span>
                        </div>
                        <div class="row" style="height: 35%; margin-right: 0; margin-left: 0; border-top: solid 1px;
                            padding-top: 20px; color: rgba(163, 175, 183, .9); background: #001633;">
                            <span style="font-size: 20px;">Manage Rights</span></div>
                    </div>
                    <div class="col-sm-3" id="divChangePasswordDashboard" runat="server" style="height: 200px; border: black solid;
                        text-align: center; padding: 0; margin-right: 15px; margin-bottom: 15px;cursor:pointer;" onclick="fnDashboardIconClick(4);">
                        <div class="row" style="height: 65%; margin-right: 0; margin-left: 0;">
                            <span class="glyphicon glyphicon-stats" style="font-size: 100px; padding-top: 15px;">
                            </span>
                        </div>
                        <div class="row" style="height: 35%; margin-right: 0; margin-left: 0; border-top: solid 1px;
                            padding-top: 20px; color: rgba(163, 175, 183, .9); background: #001633;">
                            <span style="font-size: 20px;">Change Password</span></div>
                    </div>
                   <%-- <div class="col-sm-3" id="divINRDashboard" runat="server" style="height: 200px; border: black solid;
                        text-align: center; padding: 0; margin-right: 15px; margin-bottom: 15px;cursor:pointer;" onclick="fnDashboardIconClick(5);">
                        <div class="row" style="height: 65%; margin-right: 0; margin-left: 0;">
                            <span class="glyphicon glyphicon-refresh" style="font-size: 100px; padding-top: 15px;">
                            </span>
                        </div>
                        <div class="row" style="height: 35%; margin-right: 0; margin-left: 0; border-top: solid 1px;
                            padding-top: 20px; color: rgba(163, 175, 183, .9); background: #001633;">
                            <span style="font-size: 20px;">Issue Note Return</span></div>
                    </div>--%>
                  <%--  <div class="col-sm-3" id="divSDListDashboard" runat="server" style="height: 200px; border: black solid;
                        text-align: center; padding: 0; margin-right: 15px; margin-bottom: 15px;cursor:pointer;" onclick="fnDashboardIconClick(6);">
                        <div class="row" style="height: 65%; margin-right: 0; margin-left: 0;">
                            <span class="glyphicon glyphicon-align-justify" style="font-size: 100px; padding-top: 15px;">
                            </span>
                        </div>
                        <div class="row" style="height: 35%; margin-right: 0; margin-left: 0; border-top: solid 1px;
                            padding-top: 20px; color: rgba(163, 175, 183, .9); background: #001633;">
                            <span style="font-size: 20px;">Stock Details List</span></div>
                    </div>--%>
                    <br />
                    <div id="dual_y_div" style="width: 100%; height: 500px; font-weight: bold;">
                    </div>
                    <div id="piechart" style="width: 900px; height: 500px;">
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    </form>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterScriptContents" runat="Server">
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript">
        function fnDashboardIconClick(i){
            if (i == 1) {
                window.location.href = '<%= Page.ResolveClientUrl("~/Admin/") %>';
            }
            else if (i == 2) {
                window.location.href = '<%= Page.ResolveClientUrl("~/Admin/Users/ListUsers.aspx") %>';
            }
            else if (i == 3) {
                window.location.href = '<%= Page.ResolveClientUrl("~/Admin/Rights/Default.aspx") %>';
            }
            else if (i == 4) {
                window.location.href = '<%= Page.ResolveClientUrl("~/Admin/Settings/ChangePassword.aspx") %>';
            }
        <%--    else if (i == 5) {
                window.location.href = '<%= Page.ResolveClientUrl("~/Admin/IssueNotesReturn/IssueNotesReturn.aspx") %>';
            }
            else if (i == 6) {
                window.location.href = '<%= Page.ResolveClientUrl("~/Admin/Stock/StockProductsDetailsList.aspx") %>';
            }--%>
        }
    </script>
    <script type="text/javascript">
        //google.charts.load('current', { 'packages': ['bar', 'corechart'] });
        //google.charts.setOnLoadCallback(drawStuff);

        function drawStuff() {

            var dataTable = new google.visualization.DataTable();

            var newData = new Array();

            newData[0] = new Array();
            newData[0].push('Date', 'Total', 'Approved', 'Pending', 'Rejected');
            //newData[1] = new Array();
            //newData[1].push('22-Feb-2017', 12, 8, 3, 9);
            //newData[2] = new Array();
            //newData[2].push('23-Feb-2017', 8, 5, 2, 5);
            //newData[3] = new Array();
            //newData[3].push('24-Feb-2017', 9, 6, 1, 2);
            //newData[4] = new Array();
            //newData[4].push('25-Feb-2017', 18, 12, 3, 3);
            //newData[5] = new Array();
            //newData[5].push('26-Feb-2017', 20, 5, 10, 5);

            var monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];

            jQuery.ajax({
                type: "POST",
                url: "defaultHandler.ashx?action=1",
                //data: JSON.stringify(data),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dataReturn) {
                    if (dataReturn != "") {
                        var result = dataReturn;
                        //console.log(result);
                        var rowNum = 1;
                        jQuery.map(result.List, function (item) {
                            var total = 0, totalApproved = 0, totalRejected = 0, totalPending = 0;
                            var dd = new Date(item.InternalRequisitionDate);
                            //var dd = new Date(parseInt(item.InternalRequisitionDate.replace('/Date(', '')));                            
                            //var activeDate = dd.getDate() + "-" + monthNames[dd.getMonth()] + "-" + dd.getFullYear();
                            var activeDate = dd.getDate() + "-" + monthNames[dd.getMonth()];
                            if (item.Type == 1) {
                                total += parseFloat(item.Total);
                            }

                            else if (item.Type == 2 || item.Type == 5) {
                                totalApproved += parseFloat(item.Total);
                            }

                            else if (item.Type == 3 || item.Type == 6) {
                                totalRejected += parseFloat(item.Total);
                            }

                            else if (item.Type == 4 || item.Type == 7) {
                                totalPending += parseFloat(item.Total);
                            }
                            newData[rowNum] = new Array();
                            newData[rowNum].push(activeDate, total, totalApproved, totalPending, totalRejected);
                            rowNum++;
                        });


                        GetColumnChart(dataTable, newData);
                    }

                }
            });

            //GetColumnChart(dataTable, newData);
        }

        function GetColumnChart(dataTable, newData) {
            var xaxis = new Array(0, 5, 10, 15, 20);
            var numRows = newData.length;
            //console.log("2: " + numRows);
            dataTable.addColumn('string', newData[0][0]);
            dataTable.addColumn('number', newData[0][1]);
            dataTable.addColumn('number', newData[0][2]);
            dataTable.addColumn('number', newData[0][3]);
            dataTable.addColumn('number', newData[0][4]);
            //console.log(newData);
            for (var i = 1; i < numRows; i++) {
                if (newData[i].constructor === Array) {
                    //console.log(newData[i]);
                    dataTable.addRow(newData[i]);
                }
            }

            var options = {
                width: 900,
                chart: {
                    title: 'Internal Requisitions in Last 15 Days',                    
                    subtitle: '',
                    titleTextStyle: {
                        color: 'red',    // any HTML string color ('red', '#cc00cc')
                        //fontName: <string>, // i.e. 'Times New Roman'
                        fontSize: 18, // 12, 18 whatever you want (don't specify px)
                        bold: true,    // true or false
                        italic: true   // true of false
                    },
                },
                

               series: {
                    0: { axis: 'xaxis' }//, // Bind series 0 to an axis named 'distance'.
                    //1: { axis: 'xaxis' } // Bind series 1 to an axis named 'brightness'.
                },
                axes: {
                    y: {
                        //xaxis: { label: 'No\'s' }, // Left y-axis.
                        //brightness: { side: 'right', label: 'apparent magnitude'} // Right y-axis.
                    }
                },


                //is3D: true
                colors: ['rgb(66, 133, 244)', 'rgb(15, 157, 88)', 'rgb(244, 180, 0)', 'rgb(219, 68, 55)']
            };

            var chart = new google.charts.Bar(document.getElementById('dual_y_div'));
            //chart.draw(data, options);
            chart.draw(dataTable, options);
        }
    </script>
    <script type="text/javascript">
        //google.charts.setOnLoadCallback(drawChart);

        function drawChart() {

            var dataTable = new google.visualization.DataTable();


            var newData = new Array();
            newData[0] = new Array();
            newData[0].push('Status', 'Total');




            jQuery.ajax({
                type: "POST",
                url: "defaultHandler.ashx?action=1",
                //data: JSON.stringify(data),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (dataReturn) {
                    if (dataReturn != "") {
                        var result = dataReturn;
                        //console.log(result);
                        var rowNum = 1, totalApproved = 0, totalPending = 0, totalRejected = 0;
                        jQuery.map(result.List, function (item) {
                            if (item.Type == 2 || item.Type == 5) {
                                //console.log("Item: " + item.Total);
                                //newData[rowNum][0] = 'Approved';
                                //newData[rowNum][1] = item.Total;
                                //newData[rowNum] = new Array();
                                //newData[rowNum].push('Approved', item.Total);
                                totalApproved += parseFloat(item.Total);
                            }

                            else if (item.Type == 3 || item.Type == 6) {
                                //newData[rowNum][0] = 'Rejected';
                                //newData[rowNum][1] = item.Total;
                                //newData[rowNum] = new Array();
                                //newData[rowNum].push('Rejected', item.Total);
                                //console.log("Item: " + item);
                                totalRejected += parseFloat(item.Total);
                            }

                            else if (item.Type == 4 || item.Type == 7) {
                                //newData[rowNum][0] = 'Pending';
                                //newData[rowNum][1] = item.Total;
                                //newData[rowNum] = new Array();
                                //newData[rowNum].push('Pending', item.Total);
                                //console.log("Item: " + item);
                                totalPending += parseFloat(item.Total);
                            }
                        });

                        newData[1] = new Array();
                        newData[1].push('Approved', totalApproved);
                        newData[2] = new Array();
                        newData[2].push('Pending', totalPending);
                        newData[3] = new Array();
                        newData[3].push('Rejected', totalRejected);
                        GetPieChart(dataTable, newData);
                    }

                }
            });


        }

        function GetPieChart(dataTable, newData) {

            var numRows = newData.length;
            //console.log("2: " + numRows);
            dataTable.addColumn('string', newData[0][0]);
            dataTable.addColumn('number', newData[0][1]);
            //console.log(newData);
            for (var i = 1; i < numRows; i++) {
                if (newData[i].constructor === Array) {
                    //console.log(newData[i]);
                    dataTable.addRow(newData[i]);
                }
            }

            //dataTable.addRows(newData);



            var options = {
                title: 'Internal Requisitons Status for Last 15 Days',
                is3D: true
            };

            var chart = new google.visualization.PieChart(document.getElementById('piechart'));

            //chart.draw(data, options);
            chart.draw(dataTable, options);
        }
    </script>
</asp:Content>

