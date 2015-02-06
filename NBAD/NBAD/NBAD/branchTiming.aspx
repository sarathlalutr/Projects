<%@ Page Title="" Language="C#" MasterPageFile="~/NBADMasterPage.Master" AutoEventWireup="true" CodeBehind="branchTiming.aspx.cs" Inherits="NBAD.WebForm17" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <script src="Scripts/NBAD/Validations/BranchTimingEntry.js"></script>
     <script>
         $(document).ready(function () {
             //$("#tabs").tabs();
             //http://stackoverflow.com/questions/8230280/dynamically-loaded-links-dont-trigger-colorbox-on-click-but-on-second-click
             $(document).delegate(".aEdit", "click", function (e) {
                 $.colorbox({ iframe: false, innerWidth: "80%", innerHeight: "80%", href: this.href });
                 return false;
             });

             $('.aTimepicker').datetimepicker({
                 //mask: false,
                 datepicker: false,
                 //value: today,
                 format: 'H:i',
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
                    <h1>Branch Timing Entry</h1>
                
                </div>
                    
                     <div class="dataupload_bg ma">
                          <ul>
                            <li style="">
                                
                                <div class="w100p fl">
                                         <label>Branch</label><div class="cb1"></div><asp:DropDownList ID="drpBranch" runat="server"></asp:DropDownList> <span class="mandatory_inner">* </span><div class="cb10"></div>
                                                    </div>
                             </li>
                            <li style="width:373px; padding-left: 45px; border-left: 2px solid #FFFFFF;">
                                <div class="w100p fl">
                                    <label>Sunday</label>  <div class="cb1"></div><asp:TextBox ID="txtSundayTimeIn" runat="server" placeholder="TimeIn" CssClass="aTimepicker"></asp:TextBox>  <asp:TextBox ID="txtSundayTimeOut" runat="server" placeholder="TimeOut" CssClass="aTimepicker"></asp:TextBox> <div class="cb10"></div>                  
                                    <label>Monay</label>  <div class="cb1"></div><asp:TextBox ID="txtMondayTimeIn" runat="server" placeholder="TimeIn" CssClass="aTimepicker" ></asp:TextBox>  <asp:TextBox ID="txtMondayTimeOut" runat="server" placeholder="TimeOut" CssClass="aTimepicker"></asp:TextBox> <div class="cb10"></div>                  
                                    <label>Tuesday</label>  <div class="cb1"></div><asp:TextBox ID="txtTuesdayTimeIn" runat="server" placeholder="TimeIn" CssClass="aTimepicker"></asp:TextBox>  <asp:TextBox ID="txtTuesdayTimeOut" runat="server" placeholder="TimeOut" CssClass="aTimepicker"></asp:TextBox> <div class="cb10"></div>                  
                                    <label>Wednesday</label>  <div class="cb1"></div><asp:TextBox ID="txtWednesdayTimeIn" runat="server" placeholder="TimeIn" CssClass="aTimepicker"></asp:TextBox>  <asp:TextBox ID="txtWednesdayTimeOut" runat="server" placeholder="TimeOut" CssClass="aTimepicker"></asp:TextBox> <div class="cb10"></div>                  
                                    <label>Thursday</label>  <div class="cb1"></div><asp:TextBox ID="txtThursdayTimeIn" runat="server" placeholder="TimeIn" CssClass="aTimepicker"></asp:TextBox>  <asp:TextBox ID="txtThursdayTimeOut" runat="server" placeholder="TimeOut" CssClass="aTimepicker"></asp:TextBox> <div class="cb10"></div>                  
                                    <label>Friday</label>  <div class="cb1"></div><asp:TextBox ID="txtFridayTimeIn" runat="server" placeholder="TimeIn" CssClass="aTimepicker"></asp:TextBox>  <asp:TextBox ID="txtFridayTimeOut" runat="server" placeholder="TimeOut" CssClass="aTimepicker"></asp:TextBox> <div class="cb10"></div>                  
                                    <label>Saturday</label>  <div class="cb1"></div><asp:TextBox ID="txtSaturdayTimeIn" runat="server" placeholder="TimeIn" CssClass="aTimepicker"></asp:TextBox>  <asp:TextBox ID="txtSaturdayTimeOut" runat="server" placeholder="TimeOut" CssClass="aTimepicker"></asp:TextBox> <div class="cb10"></div>                  

                       

                                        <%-- <label>Schedule In time</label>--%>  <div class="cb1"></div><asp:TextBox ID="txtScheduleInTime" runat="server" Visible="False"></asp:TextBox>  <div class="cb10"></div>
     <%--<label>Schedule Out Time</label>--%>  <div class="cb1"></div><asp:TextBox ID="txtScheduleOutTime" runat="server" Visible="False"></asp:TextBox>      <div class="cb10"></div>        
                                    </div>
                            </li>
                        </ul>

                         </div>
                    
                        <div class="cb10"></div><div class="cb10"></div>
               <div class="cb">
                   <div class="ma" style="width:200px;">
    
    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"  OnClientClick="return BranchTimingEntry();" CssClass="submit fr" Style="background-color: #006533; width: auto; padding-left: 60px; padding-right: 60px;"/>
      </div>
                            </div>     
                    <div class="cb10"></div><div class="cb10"></div>  
                    
                     <div class="cb10"></div><div class="cb10"></div>              
   
                        <div class="pageheader">
                            <div class="dv_fl_right">
                                <a href="Javascript:void(0);" id="aSearch" class="aSearch">Search</a>
                            </div>
                        </div>

                        <div id="searchDiv" style="display: none;" class="dv_search">
                            <ul class="ul_searchform">
                                <li>
                                    <asp:TextBox ID="txtSearchBranch" runat="server" CssClass="new_textfield_Search" placeholder="Keyword"></asp:TextBox></li>
                                <li>
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="addkey_btn_search" OnClick="btnSearch_Click" />

                                </li>
                            </ul>
                        </div>
    <div class="clear"></div>
                    
                    <asp:GridView ID="gridBranchTiming" EmptyDataText="No Records Found" runat="server" AutoGenerateColumns="False" BorderWidth="0"
                                AllowPaging="true" ShowFooter="false" PageSize="15" Width="100%" OnPageIndexChanging="gridBranchTiming_PageIndexChanging"
                                CssClass="mGrid" OnRowDeleting="gridBranchTiming_RowDeleting"
                                OnRowCommand="gridBranchTiming_RowCommand">
                                <AlternatingRowStyle CssClass="alt" />
                                <PagerStyle CssClass="pgr" />
                                <Columns>
                                    <asp:TemplateField HeaderText="ID" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="BranchTimeEntryID" runat="server" Text='<%#Eval("BranchTimeEntryID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <HeaderStyle Width="10%"></HeaderStyle>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Branch ID">
                                        <ItemTemplate>
                                            <asp:Label ID="BranchID" runat="server" Text='<%#Eval("BranchID") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="50%"></HeaderStyle>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Branch Name">
                                        <ItemTemplate>
                                            <asp:Label ID="BranchName" runat="server" Text='<%#Eval("BranchName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="20%"></HeaderStyle>
                                    </asp:TemplateField>
                                    
                                        <asp:TemplateField HeaderText="SundayIn">
                                        <ItemTemplate>
                                            <asp:Label ID="SundayIn" runat="server" Text='<%#Eval("SundayIn") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="20%"></HeaderStyle>
                                    </asp:TemplateField>
                                    
                                        <asp:TemplateField HeaderText="SundayOut">
                                        <ItemTemplate>
                                            <asp:Label ID="SundayOut" runat="server" Text='<%#Eval("SundayOut") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="20%"></HeaderStyle>
                                    </asp:TemplateField>
                                    
                                     <asp:TemplateField HeaderText="MondayIn">
                                        <ItemTemplate>
                                            <asp:Label ID="MondayIn" runat="server" Text='<%#Eval("MondayIn") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="20%"></HeaderStyle>
                                    </asp:TemplateField>
                                    
                                        <asp:TemplateField HeaderText="MondayOut">
                                        <ItemTemplate>
                                            <asp:Label ID="MondayOut" runat="server" Text='<%#Eval("MondayOut") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="20%"></HeaderStyle>
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="TuesdayIn">
                                        <ItemTemplate>
                                            <asp:Label ID="TuesdayIn" runat="server" Text='<%#Eval("TuesdayIn") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="20%"></HeaderStyle>
                                    </asp:TemplateField>
                                    
                                        <asp:TemplateField HeaderText="TuesdayOut">
                                        <ItemTemplate>
                                            <asp:Label ID="TuesdayOut" runat="server" Text='<%#Eval("TuesdayOut") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="20%"></HeaderStyle>
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="WednesdayIn">
                                        <ItemTemplate>
                                            <asp:Label ID="WednesdayIn" runat="server" Text='<%#Eval("WednesdayIn") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="20%"></HeaderStyle>
                                    </asp:TemplateField>
                                    
                                        <asp:TemplateField HeaderText="WednesdayOut">
                                        <ItemTemplate>
                                            <asp:Label ID="WednesdayOut" runat="server" Text='<%#Eval("WednesdayOut") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="20%"></HeaderStyle>
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="ThursdayIn">
                                        <ItemTemplate>
                                            <asp:Label ID="ThursdayIn" runat="server" Text='<%#Eval("ThursdayIn") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="20%"></HeaderStyle>
                                    </asp:TemplateField>
                                    
                                        <asp:TemplateField HeaderText="ThursdayOut">
                                        <ItemTemplate>
                                            <asp:Label ID="ThursdayOut" runat="server" Text='<%#Eval("ThursdayOut") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="20%"></HeaderStyle>
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="FridayIn">
                                        <ItemTemplate>
                                            <asp:Label ID="FridayIn" runat="server" Text='<%#Eval("FridayIn") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="20%"></HeaderStyle>
                                    </asp:TemplateField>
                                    
                                        <asp:TemplateField HeaderText="FridayOut">
                                        <ItemTemplate>
                                            <asp:Label ID="FridayOut" runat="server" Text='<%#Eval("FridayOut") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="20%"></HeaderStyle>
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="SaturdayIn">
                                        <ItemTemplate>
                                            <asp:Label ID="SaturdayIn" runat="server" Text='<%#Eval("SaturdayIn") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="20%"></HeaderStyle>
                                    </asp:TemplateField>
                                    
                                        <asp:TemplateField HeaderText="SaturdayOut">
                                        <ItemTemplate>
                                            <asp:Label ID="SaturdayOut" runat="server" Text='<%#Eval("SaturdayOut") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="20%"></HeaderStyle>
                                    </asp:TemplateField>


                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <a class="aEdit" title="Edit" href='EditBranchTiming.aspx?Id=<%#Eval("BranchTimeEntryID") %>'>Edit</a>
                                        </ItemTemplate>
                                        <HeaderStyle Width="10%"></HeaderStyle>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDelete" CssClass="aDelete" ToolTip="Delete" runat="server" CommandName="delete" OnClientClick=' javascript:return confirm("Are you sure you want to delete?"); '
                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "BranchTimeEntryID") %>'>Delete</asp:LinkButton>

                                        </ItemTemplate>
                                        <HeaderStyle Width="10%"></HeaderStyle>
                                    </asp:TemplateField>

                                </Columns>

                            </asp:GridView>

                    </div>
                </div>
        </section>
                     
                   <section id="main_footer">
        <div class="footer_txt ma w1172">&copy; National Bank of Abu Dhabi</div>
    </section>  
                                    

                    
 </asp:Content>
