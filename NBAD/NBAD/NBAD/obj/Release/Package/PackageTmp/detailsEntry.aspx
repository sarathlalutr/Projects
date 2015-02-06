<%@ Page Title="" Language="C#" MasterPageFile="~/NBADMasterPage.Master" AutoEventWireup="true" CodeBehind="detailsEntry.aspx.cs" Inherits="NBAD.WebForm6" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/NBAD/Validations/detailsEntry.js"></script>
    <script>
        $(document).ready(function () {
            //$("#tabs").tabs();
            //http://stackoverflow.com/questions/8230280/dynamically-loaded-links-dont-trigger-colorbox-on-click-but-on-second-click
            $(document).delegate(".aEdit", "click", function (e) {
                $.colorbox({ iframe: false, innerWidth: "80%", innerHeight: "80%", href: this.href });
                return false;
            });
 
            $('#ContentPlaceHolder1_txtSwipeInTime').datetimepicker({
                //mask: false,
                //timepicker: false,
                //value: today,
                format: 'd/m/Y H:i',
                closeOnDateSelect: true

                //,
                //formatDate: 'Y/m/d',
            });
            $('#ContentPlaceHolder1_txtApprovedDate').datetimepicker({
                //mask: false,
                //timepicker: false,
                //value: today,
                format: 'd/m/Y H:i',
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
                    <h1>Details Entry</h1>
                
                </div>
                
                  <div class="dataupload_bg ma">
                        <ul>
                            <li style="border-right:2px solid #FFFFFF;">
                                
                                <div class="w100p fl">
                                     <label>Employee Id</label>  <div class="cb1"></div><asp:TextBox ID="txtEmployeeId" runat="server"></asp:TextBox><span class="mandatory_inner">* </span>
                                    <div class="cb10"></div>
                                    <%--<input type="submit" class="submit fl" value="Upload" style="background-color:#006533;width:auto;padding-left:60px;padding-right:60px;">--%>
                                     <label>Employee Name</label><div class="cb1"></div><asp:TextBox ID="txtEmployeeName" runat="server"></asp:TextBox><span class="mandatory_inner">* </span><div class="cb10"></div>
    <label>Gender</label><div class="cb1"></div><asp:DropDownList ID="drpGender" runat="server">
        <asp:ListItem>Male</asp:ListItem>
        <asp:ListItem>Female</asp:ListItem>
    </asp:DropDownList><span class="mandatory_inner">* </span><div class="cb10"></div>
                                    <label>Designation</label><div class="cb1"></div><asp:DropDownList ID="drpDesignation" runat="server"></asp:DropDownList><span class="mandatory_inner">* </span><div class="cb10"></div>
    <label>Description</label><div class="cb1"></div><asp:DropDownList ID="drpDescription" runat="server"></asp:DropDownList><span class="mandatory_inner">* </span><div class="cb10"></div>
                          <label>Approved Date</label><div class="cb1"></div><asp:TextBox ID="txtApprovedDate" runat="server"></asp:TextBox><span class="mandatory_inner">* </span><div class="cb10"></div>            
                                    
                                </div>
                             </li>
                            <li style="width:373px; margin-left: 45px;">
                                <div class="w100p fl">
                                    
    <label>Branch</label><div class="cb1"></div><asp:DropDownList ID="drpBranch" runat="server"></asp:DropDownList><span class="mandatory_inner">* </span><div class="cb10"></div>
    <label>Department</label><div class="cb1"></div><asp:DropDownList ID="drpDepartment" runat="server"></asp:DropDownList><span class="mandatory_inner">* </span><div class="cb10"></div>
    
    <label>Swipe Time</label><div class="cb1"></div><asp:TextBox ID="txtSwipeInTime" runat="server"></asp:TextBox><span class="mandatory_inner">* </span><div class="cb10"></div>
    <label>Swipe Location</label><div class="cb1"></div><asp:DropDownList ID="drpSwipeInLocation" runat="server"></asp:DropDownList><span class="mandatory_inner">* </span><div class="cb10"></div>
    <label>Reader Type</label><div class="cb1"></div><asp:DropDownList ID="drpReaderType" runat="server">
        <asp:ListItem>IN</asp:ListItem>
        <asp:ListItem>OUT</asp:ListItem>
    </asp:DropDownList><div class="cb10"></div>
        <%--<label>Swipe Out time</label>--%><asp:TextBox ID="txtSwipeOutTime" runat="server" Visible="False"></asp:TextBox>
    <%--<label>Swipe Out Location</label>--%><asp:DropDownList ID="drpSwipeOutLocation" runat="server" Visible="False"></asp:DropDownList>
    <label>Approved By</label><div class="cb1"></div><asp:TextBox ID="txtAprovedBy" runat="server"></asp:TextBox><span class="mandatory_inner">* </span><div class="cb10"></div>
                                    </div>
                            </li>
                        </ul>
                            
                    </div>
                <div class="cb10"></div><div class="cb10"></div>
               <div class="cb">
                   <div class="ma" style="width:200px;">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" OnClientClick="return detailsEntry();" CssClass="submit ma" Style="background-color: #006533; width: auto; padding-left: 60px; padding-right: 60px;"/>
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
                    
                    <asp:GridView ID="gridDetails" EmptyDataText="No Records Found" runat="server" AutoGenerateColumns="False" BorderWidth="0"
                                AllowPaging="true" ShowFooter="false" PageSize="15" Width="100%" OnPageIndexChanging="gridDetails_PageIndexChanging"
                                CssClass="mGrid" OnRowDeleting="gridDetails_RowDeleting"
                                OnRowCommand="gridDetails_RowCommand">
                                <AlternatingRowStyle CssClass="alt" />
                                <PagerStyle CssClass="pgr" />
                                <Columns>
                                    <asp:TemplateField HeaderText="ID" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="BranchEntryId" runat="server" Text='<%#Eval("NBADId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <HeaderStyle Width="10%"></HeaderStyle>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="EmployeeId">
                                        <ItemTemplate>
                                            <asp:Label ID="EmployeeId" runat="server" Text='<%#Eval("EmployeeId") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="50%"></HeaderStyle>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="EmployeeName">
                                        <ItemTemplate>
                                            <asp:Label ID="EmployeeName" runat="server" Text='<%#Eval("EmployeeName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="20%"></HeaderStyle>
                                    </asp:TemplateField>
                                    
                                        <asp:TemplateField HeaderText="Gender">
                                        <ItemTemplate>
                                            <asp:Label ID="Gender" runat="server" Text='<%#Eval("Gender") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="20%"></HeaderStyle>
                                    </asp:TemplateField>
                                    
                                        <asp:TemplateField HeaderText="Designation">
                                        <ItemTemplate>
                                            <asp:Label ID="Designation" runat="server" Text='<%#Eval("Designation") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="20%"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:Label ID="Description" runat="server" Text='<%#Eval("Description") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="20%"></HeaderStyle>
                                    </asp:TemplateField>
                                    
                                        <asp:TemplateField HeaderText="Branch">
                                        <ItemTemplate>
                                            <asp:Label ID="Branch" runat="server" Text='<%#Eval("Branch") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="20%"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Department">
                                        <ItemTemplate>
                                            <asp:Label ID="Department" runat="server" Text='<%#Eval("Department") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="20%"></HeaderStyle>
                                    </asp:TemplateField>
                                    
                                        <asp:TemplateField HeaderText="Location">
                                        <ItemTemplate>
                                            <asp:Label ID="Location" runat="server" Text='<%#Eval("Location") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="20%"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="AccessTime">
                                        <ItemTemplate>
                                            <asp:Label ID="AccessTime" runat="server" Text='<%#Eval("AccessTime") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="20%"></HeaderStyle>
                                    </asp:TemplateField>
                                    
                                        <asp:TemplateField HeaderText="ReaderType">
                                        <ItemTemplate>
                                            <asp:Label ID="ReaderType" runat="server" Text='<%#Eval("ReaderType") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="20%"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ApprovedBy">
                                        <ItemTemplate>
                                            <asp:Label ID="ApprovedBy" runat="server" Text='<%#Eval("ApprovedBy") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="20%"></HeaderStyle>
                                    </asp:TemplateField>
                                    
                                        <asp:TemplateField HeaderText="ApprovedDate">
                                        <ItemTemplate>
                                            <asp:Label ID="ApprovedDate" runat="server" Text='<%#Eval("ApprovedDate") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="20%"></HeaderStyle>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <a class="aEdit" title="Edit" href='EditDetails.aspx?Id=<%#Eval("NBADId") %>'>Edit</a>
                                        </ItemTemplate>
                                        <HeaderStyle Width="10%"></HeaderStyle>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDelete" CssClass="aDelete" ToolTip="Delete" runat="server" CommandName="delete" OnClientClick=' javascript:return confirm("Are you sure you want to delete?"); '
                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "NBADId") %>'>Delete</asp:LinkButton>

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
