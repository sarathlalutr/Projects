<%@ Page Title="" Language="C#" MasterPageFile="~/NBADMasterPage.Master" AutoEventWireup="true" CodeBehind="UploadData.aspx.cs" Inherits="NBAD.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
    
    <%--<link href="//cdn.datatables.net/1.10.4/css/jquery.dataTables.min.css" rel="stylesheet" />--%>
   
    <script type="text/javascript">
        $(document).ready(function () {
            $('.table-grid').dataTable(
                //{
                //'bLengthChange': false,
                //'bPaginate': true,
                //'sPaginationType': 'full_numbers',
                //'iDisplayLength': 10,
                //'bInfo': true,
                //'oLanguage':
                //{
                //    'sSearch': 'Search',

                //}
                //}
            );
        });
    </script>  


   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="cb">
    <fieldset>
                        <legend>Instructions</legend>
                        <div>
                            <ul class='ulStyle'>
                                <li>
                                    <div id="container">
                                        Upload only CSV file 
                                        <%--<img class="popover" src="images/inner_page/helpIcon.ico" height="16px" />--%>
                                    </div>
                                </li>
                               <%-- <li>Import Employee details</li>--%>
                                   <%--<%--<li>Last imported excel data only can be roll back. So please check KMS if imported data are reflected. If there is any mismatch, please roll back immediately.</li>--%>
                            </ul>
                        </div>
                    </fieldset>
        <fieldset>
                        <legend>Upload Excel</legend>


                        <%--<div>Please Select Excel File: 
                            <label class="lbl_browse">
                               <asp:FileUpload ID="fileuploadExcel" runat="server" />
                            <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" OnClientClick="showDivPageLoading();" CssClass="addkey_btn" />
                            </label>
                        </div>--%>

                        <div>
                            Please Select CSV File: 
                            
                               <asp:FileUpload ID="fileuploadExcel" runat="server" />
                            <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" OnClientClick="showDivPageLoading();" CssClass="addkey_btn" />

                        </div>
                    </fieldset>
    
    </div>
        <asp:Repeater ID="grvExcelData" runat="server">
             <HeaderTemplate>
                                <fieldset>
                                    <legend>Imported Excel Data. Please verify</legend>
                                    <div>
                                        <table class="table-grid">
                                            <thead>
                                                <th>Sl</th>
                                                <th>EmployeeId</th>
                                                <th>EmployeeName</th>
                                                <th>Gender</th>
                                                <th>Designation</th>
                                                <th>Description</th>
                                                <th>Branch</th>
                                                <th>Department</th>
                                                <th>Location</th>
                                                <th>AccessTime</th>
                                               
                                            </thead>
                                            <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="div_row">
                                    <td><%#Container.ItemIndex + 1 %></td>
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem, "Person ID") %>
                                    </td>
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem, "Who") %>
                                    </td>
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem, "Gender") %>
                                    </td>
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem, "Designation") %>
                                    </td>
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem, "Description") %>
                                    </td>
                                         <td>
                                        <%# DataBinder.Eval(Container.DataItem, "Branch") %>
                                    </td>
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem, "Department") %>
                                    </td>
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem, "Location") %>
                                    </td>
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem, "Date/Time") %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                    </table>
                            </div>
                    </fieldset>
                            </FooterTemplate>
        </asp:Repeater>
       
</asp:Content>
