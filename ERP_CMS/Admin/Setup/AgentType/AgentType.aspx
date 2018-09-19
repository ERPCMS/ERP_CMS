<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Site.Master" AutoEventWireup="true" CodeBehind="AgentType.aspx.cs" Inherits="ERP_CMS.Admin.Setup.AgentType.AgentType" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContents" runat="Server">
    <link href="<%= Page.ResolveClientUrl ("~/Admin/assets/kendo/kendo.common.min.css") %>"
        rel="stylesheet" type="text/css" />
    <link href="<%= Page.ResolveClientUrl ("~/Admin/assets/kendo/kendo.default.min.css") %>"
        rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageHeading" runat="Server">
    <h1 class="page-title">
        Agent Type Management</h1>
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
                                    url: "AgentType_Handler.ashx?action=1",
                                    dataType: "json"
                                },
                                update: {
                                    url: "AgentType_Handler.ashx?action=2",
                                    dataType: "json"
                                },
                                destroy: {
                                    url: "AgentType_Handler.ashx?action=4",
                                    dataType: "json"
                                },
                                create: {
                                    url: "AgentType_Handler.ashx?action=3",
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
                                data: "agentTypeList",
                                errors:"Errors",
                                model: {
                                    id: "AgentTypeID",
                                    fields: {
                                        AgentTypeID: { editable: false, nullable: true },
                                        AgentType1: { validation: { required: true } }
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
                            { field: "AgentType1", title: "Agent Type" },
                            //{ command: ["edit", "destroy"], title: "&nbsp;", width: "250px"}],
                            { command: [
                                {name: "edit"},
                                {name: "Delete",  
                                    click: function(e){  
                                        var tr = $(e.target).closest("tr"); //get the row for deletion
                                        var data = this.dataItem(tr); //get the row data so it can be referred later
                                        bootbox.dialog({
                                            message: "Are you sure to delete AgentType: " + data.BrandName + "?",
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
                    e.container.kendoWindow("title", "Add Agent Type")
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
