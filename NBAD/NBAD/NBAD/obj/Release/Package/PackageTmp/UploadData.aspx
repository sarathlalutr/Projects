<%@ Page Title="" Language="C#" MasterPageFile="~/NBADMasterPage.Master" AutoEventWireup="true" CodeBehind="UploadData.aspx.cs" Inherits="NBAD.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


    <%--<link href="//cdn.datatables.net/1.10.4/css/jquery.dataTables.min.css" rel="stylesheet" />--%>

    <script type="text/javascript">
        $(document).ready(function () {
            var dataTable = $('.table-grid').dataTable(
                {
                    'bLengthChange': false,
                    'bPaginate': true,
                    'sPaginationType': 'full_numbers',
                    'iDisplayLength': 10,
                    'bInfo': true,
                    'oLanguage':
                    {
                        'sSearch': 'Search',

                    }
                }
            );


            $("#searchbox").keyup(function () {
                dataTable.fnFilter(this.value);
            });
        });
    </script>



    <style type="text/css">
        .addkey_btn {
        }
    </style>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section>
        <div class="middle_bg fl pr">

            <div class="upload_section_cntnr ma">

                <div class="w944 ma upload_inst">
                    <h1>Instructions</h1>
                    <ul>
                        <li class="fl">
                            <img align="bottom" width="14" height="12" align="middle" src="Images/tick.jpg" alt="yes" title="yes">
                        </li>
                        <li class="fl">Upload only CSV files
                            </li>
                    </ul>
                </div>

                <div class="up_file_head_bg ma">
                    <ul>
                        <li class="fl w175" style="text-decoration: underline;">Upload CSV
                             </li>
                        <li class="fl csv_info">
                            <div class="fl">Upload only CSV files</div>
                            <div class="fl browse ">

                                <asp:FileUpload ID="fileuploadExcel" runat="server"  CssClass="fl"/>
                            </div>
                        </li>
                        <li class="fl w175">
                            <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" OnClientClick="showDivPageLoading();" CssClass="submit fr" Style="background-color: #006533; width: auto; padding-left: 60px; padding-right: 60px;" />

                            <%--<input type="submit" class="submit fr" value="Upload" style="background-color:#006533;width:auto;padding-left:60px;padding-right:60px;">--%>
                            </li>
                    </ul>

                </div>

                <asp:Repeater ID="grvExcelData" runat="server">
                    <HeaderTemplate>
                        <div class="csv_file_head_bg ma">
                            <span>CSV Files</span>
                            <div class="fr" style="padding-top: 20px;">
                                <input type="text" id="searchbox" name="keyword" style="height: 20px; width: 100px;">
                                <%--<input type="submit" class="submit fl" style="height: 23px; float: right; font-family: 'Opensans-Regular'; font-size: 14px;" value="Search">--%>
                                <%--<span class="submit fl" style="height: 23px; float: right; font-family: 'Opensans-Regular'; font-size: 14px;" ></span>--%>
                                <a href="javascript:void(0);" class="submit fl" style="height: 23px; float: right; font-family: 'Opensans-Regular'; font-size: 14px; cursor: inherit; text-decoration: none; text-align: center;" >Search</a>
                            </div>
                        </div>
                        <div class="up_file_head_bg1 ma">
                            <div class="cb">

                                <div>
                                    <table class="table-grid">
                                        <thead class="cat_tr_head">
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
                                            <th>ReaderType</th>
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
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "Reader") %>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                    </table>
                            </div>
                   
                    </div>
                  </div>
                    </FooterTemplate>
                </asp:Repeater>

            </div>
        </div>
    </section>
    <section id="main_footer">
        <div class="footer_txt ma w1172">&copy; National Bank of Abu Dhabi</div>
    </section>


</asp:Content>
