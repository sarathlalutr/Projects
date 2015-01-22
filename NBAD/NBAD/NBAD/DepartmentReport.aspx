<%@ Page Title="" Language="C#" MasterPageFile="~/NBADMasterPage.Master" AutoEventWireup="true" CodeBehind="DepartmentReport.aspx.cs" Inherits="NBAD.WebForm4" %>
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
              <legend>Department-wise Report</legend>
             <div>
               <label>Select Department</label> <asp:DropDownList ID="drpDepartment" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpDepartment_SelectedIndexChanged">
        </asp:DropDownList>   <asp:Button ID="btnReport" runat="server" Text="Generate Report" OnClick="btnReport_Click" />
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
                                                <th>Location</th>
                                                <th>AccessTime</th>
                                               
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
                                        <%# DataBinder.Eval(Container.DataItem, "Location") %>
                                    </td>
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem, "AccessTime") %>
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
    <p>
    </p>
</asp:Content>
