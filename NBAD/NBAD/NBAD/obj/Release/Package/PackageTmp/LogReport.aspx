<%@ Page Title="" Language="C#" MasterPageFile="~/NBADMasterPage.Master" AutoEventWireup="true" CodeBehind="LogReport.aspx.cs" Inherits="NBAD.WebForm18" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/NBAD/Validations/LogReport.js"></script>
     <script>
         $(document).ready(function () {

             $('#ContentPlaceHolder1_txtFromDate').datetimepicker({
                 //mask: false,
                 timepicker: false,
                 //value: today,
                 format: 'd/m/Y',
                 closeOnDateSelect: true

                 //,
                 //formatDate: 'Y/m/d',
             });
             $('#ContentPlaceHolder1_txtToDate').datetimepicker({
                 //mask: false,
                 timepicker: false,
                 //value: today,
                 format: 'd/m/Y',
                 closeOnDateSelect: true

                 //,
                 //formatDate: 'Y/m/d',
             });
         });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <section>
            <div class="middle_bg fl pr">
    
                <div class="upload_section_cntnr ma">
                     <div class="w944 ma upload_inst">
                    <h1>Log Report</h1>
                
                </div>
                    
                    <div class="dataupload_bg ma">
                        <ul>
                            <li style="border-right:2px solid #FFFFFF;">
                                
                                <div class="w100p fl">
                      <label>From</label> <div class="cb1"></div><asp:TextBox ID="txtFromDate" runat="server"></asp:TextBox><span class="mandatory_inner">* </span> <div class="cb10"></div>
                                                    </div>
                             </li>
                            <li style="width:373px; margin-left: 45px;">
                                <div class="w100p fl">
                                    <label>To</label> <div class="cb1"></div><asp:TextBox ID="txtToDate" runat="server"></asp:TextBox><span class="mandatory_inner">* </span> <div class="cb10"></div>
                                    </div>
                            </li>
                        </ul>
                            
                    </div>
                    <div class="cb10"></div><div class="cb10"></div>
               <div class="cb">
                   <div class="ma" style="width:200px;">
   
    <asp:Button ID="btnSubmit" runat="server" Text="Generate Report" OnClientClick=" return LogReport();  "  CssClass="submit fr" Style="background-color: #006533; width: auto; padding-left: 60px; padding-right: 60px;"/>
    
   </div>
                            </div> 
                    <div class="cb10"></div><div class="cb10"></div>
    <div id="divDashPop" class="div_transparent">
        <div id="divDashPopHdr">
        </div>
        <div class="dv_close_confirm" onclick=" javascript:return hideDashPop(); ">
            <a href="#" onclick=" javascript:return hideDashPop(); " class="close-button"></a>
        </div>
        <div class="dv-pop-confirm">
            <ul class="ul-menu">

                <li>
                    <asp:LinkButton ID="aToExcel" rel="menu_tile" class="menu-export-excel" OnClientClick=' hideDashPop(); ' OnClick="ExportToExcel" runat="server">Export To Excel</asp:LinkButton>
                    <%--<a href="#" id="aToExcel" rel="menu_tile" class="menu-export-excel" onclick="javascript:return false;">ExportToExcel</a>--%>
                </li>
                <li>
                    <asp:LinkButton ID="aToPdf" rel="menu_tile" class="menu-export-pdf" OnClientClick=' hideDashPop(); ' OnClick="btnSubmit_Click" runat="server">Export To PDF</asp:LinkButton>
                    <%--<a href="#" id="aToPdf" rel="menu_tile" class="menu-export-pdf" onclick="javascript:return false;">ExportToPDF</a>--%>
                </li>
             <%--   <li>
                    <asp:LinkButton ID="aToBrowser" rel="menu_tile" class="menu-view-browser" OnClientClick=' hideDashPop(); ' OnClick="ExportToWord" runat="server">Export To Word</asp:LinkButton>
                </li>--%>
            </ul>
        </div>
    </div>

                </div>
                </div>
         </section>
      <section id="main_footer">
        <div class="footer_txt ma w1172">&copy; National Bank of Abu Dhabi</div>
    </section>

</asp:Content>
