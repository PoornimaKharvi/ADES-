<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BOMPrint.aspx.cs" Inherits="ADES_22.BOMPrint" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style id="table_style" type="text/css">
        .Container {
            overflow-y: auto;
            overflow-x: hidden;
            padding: 0px;
            margin: auto;
            margin-top: 10px;
        }

        .filterdiv {
            text-align: center;
            width: 50%;
            margin-right: 300px;
        }

        .listContainer {
            width: 100%;
            padding-left: 15px;
            padding-top: 5px;
            height: 90vh;
            overflow: auto
        }

        .grid {
            overflow: auto;
            width: 99%;
            border: 1px solid;
            padding: 15px
        }

            .grid th {
                padding: 10px;
                font-size: 15px;
                text-align: center;
                font-weight: bold;
                color: black;
                background-color: #EEEEEE;
            }

            .grid td {
                padding: 5px;
                font-size: 14px;
            }

            .grid th, .grid td {
                border: unset;
                border-bottom: 0.5px solid #cdcdcd !important;
                border-right: 0.5px solid #cdcdcd !important;
                border-top: 0.5px solid #cdcdcd !important;
                border-bottom: 0.5px solid #cdcdcd !important;
            }

            .grid tr th {
                position: sticky;
                top: -1px;
                left: -1px;
                right: -1px;
                z-index: 1;
            }
            .button{
                width:60px;
                height:30px;
                background-color:InfoBackground;
                color:white;
                border-radius:5px;
            }
    </style>
    <script type="text/javascript">
        function printFun() {
            var divContainer = document.getElementById("<%=lvContainer.ClientID %>");
            var printWindow = window.open('', 'display:block', 'height=600,width=900');
            printWindow.document.write('<style type ="text/css">');
            var table_style = document.getElementById("table_style").innerHTML;
            printWindow.document.write(table_style);
            printWindow.document.write('</style>');
            printWindow.document.write(divContainer.innerHTML);
            printWindow.document.close();
           // setTimeout(function () {
                printWindow.print();
           // }, 100);
            //printWindow.print();
            //printWindow.close();
            // window.print(); 
            return false;
        }
        function ReturntoPage() {
            window.navigate("POAssociate.aspx");
        }
    </script>
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="Container">
            <table class="filterDiv">
                <tr>
                    <td style="padding-left: 15px">
                       <%-- <asp:Button  runat="server" ID="btnBack" CssClass="button btn-info glyphicon glyphicon-arrow-left" OnClick="btnBack_Click"/>--%>
                       <%-- <button runat="server"  type="button" class="btn btn-info btn-md" onserverclick="btnback_ServerClick">
                             <span class="glyphicon glyphicon-arrow-left ">&nbsp;</span>
                        </button>--%>
                        <a href="POAssociate.aspx" class="btn btn-info btn-md " >
                            <span class="glyphicon glyphicon-arrow-left ">&nbsp;</span>
                        </a>
                    </td>
                    <td style="position: absolute;right: 15px;">
                        <%-- <asp:Label runat="server">Print:</asp:Label>--%>
                        <%--    <asp:LinkButton runat="server" ID="LinkButton1" CssClass=" glyphicon glyphicon-print" ClientIDMode="Static"  OnClientClick="return printFun();" ToolTip="Print"></asp:LinkButton>--%>
                        <a href="#" class="btn btn-info btn-md " onclick="return printFun();"><%--return printFun();--%>
                            <span class="glyphicon glyphicon-print">&nbsp;</span>Print
                        </a>
                    </td>
                </tr>
            </table>

            <div id="lvContainer" runat="server" class="listContainer">
                <asp:ListView runat="server" ID="lvBOMPrint" ItemPlaceholderID="placeHolderReport">
                    <LayoutTemplate>
                        <table class="grid">
                            <tr>
                                <asp:PlaceHolder runat="server" ID="placeHolderReport" />
                            </tr>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr runat="server" visible='<%# Eval("HeaderVisibility1") %>'>
                            <%-- <th colspan="3">Customer & PO name</th>--%>
                            <th colspan="7">
                                <asp:Label runat="server" Text='<%# String.Format("Customer =  {0} & PONO =  {1}", Eval("Cust"), Eval("Pono")) %>'></asp:Label></th>
                        </tr>
                        <tr runat="server" visible='<%# Eval("HeaderVisibility2") %>'>
                            <th>SL NO.</th>
                            <th>BOM</th>
                            <th>Part Details</th>
                            <th>Part Numbers</th>
                            <th>Description</th>
                            <th>Qty</th>
                           <%-- <th>Availability/Remarks</th>--%>

                        </tr>
                        <tr runat="server" visible='<%# Eval("AccessoriesHeader") %>'>
                            <th colspan="12">Accessories List</th>
                        </tr>
                        <tr>
                            <td runat="server" visible='<%# Eval("KitVisiblility")%>' rowspan='<%# Eval("PresentRowSpan") %>'>
                                <asp:Label ID="lblISLNo" runat="server" Text='<%# Eval("id") %>' ></asp:Label></td>

                            <td runat="server" rowspan='<%# Eval("PresentRowSpan") %>' visible='<%# Eval("KitVisiblility")%>'>

                                <asp:Label ID="lblKName" runat="server" Text='<%# Eval("Kitname") %>'></asp:Label>
                            </td>

                            <td>
                                <asp:Label ID="lblIPartName" runat="server" Text='<%# Eval("Itemname") %>'></asp:Label>
                            </td>

                            <td>
                                <asp:Label ID="lblIPartNo" runat="server" Text='<%# Eval("PartNo") %>'></asp:Label>
                            </td>

                            <td>
                                <asp:Label ID="lblIDescription" runat="server" Text='<%# Eval("ItemDesc") %>'></asp:Label>
                            </td>

                            <td>
                                <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Qty") %>'></asp:Label>
                            </td>

                           <%-- <td>
                                <asp:Label ID="lblShortage" Text='<%# Eval("Shortage") %>' runat="server"></asp:Label>
                                <asp:TextBox ID="tvtRemark" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>--%>
                           
                        </tr>

                        <%--<tr  runat="server" visible='<%# Eval("HeaderVisibility3") %>'>
                            <th colspan="4">Document Check list</th>
                            <th colspan="3">To Share with</th>
                        </tr>
                        <tr  runat="server" visible='<%# Eval("HeaderVisibility4") %>'>
                            <td colspan="4"><asp:Label runat="server" Text="Packing list"></asp:Label></td>
                            <td colspan="3"><asp:Label runat="server" Text="customer"></asp:Label></td>
                        </tr>
                        <tr  runat="server" visible='<%# Eval("HeaderVisibility4") %>'>
                            <td colspan="4"><asp:Label runat="server" Text="Soft copy of Packing list"></asp:Label></td>
                            <td colspan="3"><asp:Label runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr  runat="server" visible='<%# Eval("HeaderVisibility4") %>'>
                            <td colspan="4"><asp:Label runat="server" Text="Soft copy of Invoice"></asp:Label></td>
                            <td colspan="3"><asp:Label runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr  runat="server" visible='<%# Eval("HeaderVisibility4") %>'>
                            <td colspan="4"><asp:Label runat="server" Text="Consignment copy"></asp:Label></td>
                            <td colspan="3"><asp:Label runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr  runat="server" visible='<%# Eval("HeaderVisibility4") %>'>
                            <td colspan="4"><asp:Label runat="server" Text="Invoice copy-original"></asp:Label></td>
                            <td colspan="3"><asp:Label runat="server" Text="customer"></asp:Label></td>
                        </tr>
                        <tr  runat="server" visible='<%# Eval("HeaderVisibility4") %>'>
                            <td colspan="4"><asp:Label runat="server" Text="Packed By"></asp:Label></td>
                            <td colspan="3"><asp:Label runat="server" Text="Verified By"></asp:Label></td>
                        </tr>--%>
                    </ItemTemplate>
                </asp:ListView>
            </div>

        </div>
    </form>
</body>
</html>
