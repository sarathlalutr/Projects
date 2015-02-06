<%@ Page Title="" Language="C#" MasterPageFile="~/NBADMasterPage.Master" AutoEventWireup="true" CodeBehind="DesignationReport.aspx.cs" Inherits="NBAD.WebForm2" %>
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
        <fieldset>
              <legend>Designation-wise Report</legend>
             <div>
        <label>Select Designation</label><asp:DropDownList ID="drpDesignation" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpDesignation_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" Text="Generate Report" />
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
    <p>
    </p>
</asp:Content>
