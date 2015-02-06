<%@ Page Title="" Language="C#" MasterPageFile="~/NBADMasterPage.Master" AutoEventWireup="true" CodeBehind="branchEntry.aspx.cs" Inherits="NBAD.WebForm7" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/NBAD/Validations/BranchEntry.js"></script>
    <script src="Scripts/NBAD/Validations/EditBranchEntry.js"></script>
    <script>
        $(document).ready(function () {
            //$("#tabs").tabs();
            //http://stackoverflow.com/questions/8230280/dynamically-loaded-links-dont-trigger-colorbox-on-click-but-on-second-click
            $(document).delegate(".aEdit", "click", function (e) {
                $.colorbox({ iframe: false, innerWidth: "80%", innerHeight: "80%", href: this.href });
                return false;
            });

            $('#ContentPlaceHolder1_txtScheduleInTime').datetimepicker({
                //mask: false,
                //timepicker: false,
                //value: today,
                format: 'H:i',
                closeOnDateSelect: true

                //,
                //formatDate: 'Y/m/d',
            });
            $('#ContentPlaceHolder1_txtScheduleOutTime').datetimepicker({
                //mask: false,
                //timepicker: false,
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
                    <h1>Branch Entry</h1>
                
                </div>
                    
                                      <div class="dataupload_bg ma">
                        <ul>
                            <li style="border-right:2px solid #FFFFFF;">
                                
                                <div class="w100p fl">
                                         <label>Branch ID</label>  <div class="cb1"></div><asp:TextBox ID="txtBranchID" runat="server"></asp:TextBox><span class="mandatory_inner">* </span> <div class="cb10"></div>
                                                    </div>
                             </li>
                            <li style="width:373px; margin-left: 45px;">
                                <div class="w100p fl">
                                    <label>Branch Name</label>  <div class="cb1"></div><asp:TextBox ID="txtBranchName" runat="server"></asp:TextBox> <span class="mandatory_inner">* </span>  <div class="cb10"></div>                  
                       

                                        <%-- <label>Schedule In time</label>--%>  <div class="cb1"></div><asp:TextBox ID="txtScheduleInTime" runat="server" Visible="False"></asp:TextBox>  <div class="cb10"></div>
     <%--<label>Schedule Out Time</label>--%>  <div class="cb1"></div><asp:TextBox ID="txtScheduleOutTime" runat="server" Visible="False"></asp:TextBox>      <div class="cb10"></div>        
                                    </div>
                            </li>
                        </ul>
                            
                    </div>
                    

                    
     <div class="cb10"></div><div class="cb10"></div>
               <div class="cb">
                   <div class="ma" style="width:200px;">
    
    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"  OnClientClick="return BranchEntry();" CssClass="submit fr" Style="background-color: #006533; width: auto; padding-left: 60px; padding-right: 60px;"/>
      </div>
                            </div>     
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
                    
                    <asp:GridView ID="gridBranch" EmptyDataText="No Records Found" runat="server" AutoGenerateColumns="False" BorderWidth="0"
                                AllowPaging="true" ShowFooter="false" PageSize="15" Width="100%" OnPageIndexChanging="gridBranch_PageIndexChanging"
                                CssClass="mGrid" OnRowDeleting="gridBranch_RowDeleting"
                                OnRowCommand="gridBranch_RowCommand">
                                <AlternatingRowStyle CssClass="alt" />
                                <PagerStyle CssClass="pgr" />
                                <Columns>
                                    <asp:TemplateField HeaderText="ID" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="BranchEntryId" runat="server" Text='<%#Eval("BranchEntryId") %>'></asp:Label>
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
                                    
                                       <%-- <asp:TemplateField HeaderText="ScheduleInTime">
                                        <ItemTemplate>
                                            <asp:Label ID="ScheduleInTime" runat="server" Text='<%#Eval("ScheduleInTime") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="20%"></HeaderStyle>
                                    </asp:TemplateField>
                                    
                                        <asp:TemplateField HeaderText="ScheduleOutTime">
                                        <ItemTemplate>
                                            <asp:Label ID="ScheduleOutTime" runat="server" Text='<%#Eval("ScheduleOutTime") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="20%"></HeaderStyle>
                                    </asp:TemplateField>--%>

                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <a class="aEdit" title="Edit" href='EditBranch.aspx?Id=<%#Eval("BranchEntryId") %>'>Edit</a>
                                        </ItemTemplate>
                                        <HeaderStyle Width="10%"></HeaderStyle>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDelete" CssClass="aDelete" ToolTip="Delete" runat="server" CommandName="delete" OnClientClick=' javascript:return confirm("Are you sure you want to delete?"); '
                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "BranchEntryId") %>'>Delete</asp:LinkButton>

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
