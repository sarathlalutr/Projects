<%@ Page Title="" Language="C#" MasterPageFile="~/NBADMasterPage.Master" AutoEventWireup="true" CodeBehind="BranchReport.aspx.cs" Inherits="NBAD.WebForm3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
    <p>
        <br />
    </p>
    <p>
    </p>
         <fieldset>
              <legend>Branch-wise Report</legend>
             <div>
       <label>Select Branch</label> <asp:DropDownList ID="drpBranch" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpBranch_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:Button ID="btnReport" runat="server" Text="Generate Report" OnClick="btnReport_Click" />
                    </div>
              
       
         </fieldset>
        <asp:Repeater ID="grvExcelData" runat="server">
             <HeaderTemplate>
                                <fieldset>
                                    <legend>Details</legend>
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
                                                <th>SwipeInLocation</th>
                                                <th>SwipeInTime</th>
                                               <th>SwipeOutTime</th>
                                                <th>SwipeOutLocation</th>
                                            </thead>
                                            <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="div_row">
                                    <td><%#Container.ItemIndex + 1 %></td>
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem, "EmployeeId") %>
                                    </td>
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem, "EmployeeName") %>
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
                                        <%# DataBinder.Eval(Container.DataItem, "SwipeInLocation") %>
                                    </td>
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem, "SwipeInTime") %>
                                    </td>
                                     <td>
                                        <%# DataBinder.Eval(Container.DataItem, "SwipeOutTime") %>
                                    </td>
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem, "SwipeOutLocation") %>
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
    </p>
    <p>
    </p>
</asp:Content>
