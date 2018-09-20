<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Site.Master" AutoEventWireup="true" CodeBehind="Shipper.aspx.cs" Inherits="ERP_CMS.Admin.Setup.Shipper.Shipper" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContents" runat="Server">
    <link href="<%= Page.ResolveClientUrl ("~/Admin/assets/kendo/kendo.common.min.css") %>"
        rel="stylesheet" type="text/css" />
    <link href="<%= Page.ResolveClientUrl ("~/Admin/assets/kendo/kendo.default.min.css") %>"
        rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageHeading" runat="Server">
    <h1 class="page-title">
        Shipper Management</h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContents" runat="Server">
    <div id="example">
        <div id="grid">
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterScriptContents" runat="Server">
    <script type="text/javascript" src="<%= Page.ResolveClientUrl ("~/Admin/assets/kendo/kendo.all.min.js") %>"></script>
    <script type="text/javascript" src="<%= Page.ResolveClientUrl ("~/Admin/assets/vendor/bootbox/bootbox.js") %>"></script>
    <script type="text/javascript" src="<%= Page.ResolveClientUrl ("~/Admin/assets/js/components/bootbox.js") %>"></script>
    <script type="text/javascript">
    $(document).ready(function () {
        dataSource = new kendo.data.DataSource({
            transport: {
                read: {
                    url: "Shipper_Handler.ashx?action=1",
                    dataType: "json"
                },
                update: {
                    url: "Shipper_Handler.ashx?action=2",
                    dataType: "json"
                },
                destroy: {
                    url: "Shipper_Handler.ashx?action=4",
                    dataType: "json"
                },
                create: {
                    url: "Shipper_Handler.ashx?action=3",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation !== "read" && options.models) {
                        return { models: kendo.stringify(options.models) };
                    }
                }
            },
            batch: true,
            //pageSize: 20,
            requestEnd: function (e) {
                if (e.type === "update") {
                    this.read();
                }
                if (e.type === "create") {
                    this.read();
                }
            },
            error: error,
            schema: {
                data: "shipperList",
                errors:"Errors",
                model: {
                    id: "ShipperID",
                    fields: {
                        ShipperID: { editable: false, nullable: true },
                        Shipper_Name: { validation: { required: true }},
                        Shipper_Address: { validation: { required: false }},
                        Shipper_Email: { validation: { required: false }},
                        Contact1: { validation: { required: false }},
                        Contact2: { validation: { required: false }}
                                    }
                                }
                            }
                        });

        $("#grid").kendoGrid({
            dataSource: dataSource,
            sortable: true,
        reorderable: true,
            resizable: true,
        filterable: true,
        columnMenu: true,        
        pageable: {
                    refresh: true,
                    pageSizes: true
                },
            height: 550,
            toolbar: ["create"],
            columns: [
                            { field: "Shipper_Name", title: "Name" },
                             { field: "Shipper_Address", title: "Address" },
                              { field: "Shipper_Email", title: "Email" },
                               { field: "Contact1", title: "Contact No 1" },
                                { field: "Contact2", title: "Contact No 2" },
                                
                            //{ command: ["edit", "destroy"], title: "&nbsp;", width: "250px"}],
                            { command: [
                                {name: "edit"},
                                {name: "Delete",  
                                    click: function(e){  
                                        var tr = $(e.target).closest("tr"); //get the row for deletion
                                        var data = this.dataItem(tr); //get the row data so it can be referred later
                                        bootbox.dialog({
                                            message: "Are you sure to delete Shipper: " + data.Shipper_Name + "?",
                                              title: "Confirm Delete",
                                              buttons: {
                                                danger: {
                                                  label: "Delete",
                                                  className: "btn-danger",
                                                  callback: function() {
                                                    var grid = $("#grid").data("kendoGrid");
                                                    grid.dataSource.remove(data);
                                                    grid.dataSource.sync();
                                                    grid.refresh();
                                                  }
                                                },
                                                main: {
                                                  label: "Cancel",
                                                  className: "btn-primary",
                                                }
                                              }
                                            });
                                    }                              
                                }
                                ], title: "&nbsp;", width: "180px"}],
            editable: "popup",
            edit: function (e) {
                if (e.model.isNew()) {
                    e.container.kendoWindow("title", "Add Shipper")
                }
            },
        });
    });
    </script>
    <script type="text/javascript">
        function error(args) {
            if (args.errors) {
                var grid = $("#grid").data("kendoGrid");
                grid.one("dataBinding", function (ev) {
                    ev.preventDefault();
                    bootbox.alert(args.errors);
                    grid.cancelChanges();
                });
            }
        }


    </script>
</asp:Content>
