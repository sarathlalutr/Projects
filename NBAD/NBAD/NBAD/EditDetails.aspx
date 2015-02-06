<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditDetails.aspx.cs" Inherits="NBAD.EditDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/StyleSheet.css" rel="stylesheet" />
    <script src="Scripts/NBAD/Validations/EditdetailsEntry.js"></script>
    <script src="Scripts/jquery.datetimepicker.js"></script>
    <link href="css/jquery.datetimepicker.css" rel="stylesheet" />
     <script>
         $(document).ready(function () {
            

             $('#txtSwipeInTime').datetimepicker({
                 //mask: false,
                 //timepicker: false,
                 //value: today,
                 format: 'd/m/Y H:i',
                 closeOnDateSelect: true

                 //,
                 //formatDate: 'Y/m/d',
             });
             $('#txtApprovedDate').datetimepicker({
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
</head>
<body>
    <form id="form1" runat="server">
    <div>
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
                                     <label>Employee Id</label>  <div class="cb1"></div><asp:TextBox ID="txtEmployeeId" runat="server"></asp:TextBox>
                                    <div class="cb10"></div>
                                    <%--<input type="submit" class="submit fl" value="Upload" style="background-color:#006533;width:auto;padding-left:60px;padding-right:60px;">--%>
                                     <label>Employee Name</label><div class="cb1"></div><asp:TextBox ID="txtEmployeeName" runat="server"></asp:TextBox><div class="cb10"></div>
    <label>Gender</label><div class="cb1"></div><asp:DropDownList ID="drpGender" runat="server">
        <asp:ListItem>Male</asp:ListItem>
        <asp:ListItem>Female</asp:ListItem>
    </asp:DropDownList><div class="cb10"></div>
                                    <label>Designation</label><div class="cb1"></div><asp:DropDownList ID="drpDesignation" runat="server"></asp:DropDownList><div class="cb10"></div>
    <label>Description</label><div class="cb1"></div><asp:DropDownList ID="drpDescription" runat="server"></asp:DropDownList><div class="cb10"></div>
                          <label>Approved Date</label><div class="cb1"></div><asp:TextBox ID="txtApprovedDate" runat="server"></asp:TextBox><div class="cb10"></div>            
                                    
                                </div>
                             </li>
                            <li style="width:373px; margin-left: 45px;">
                                <div class="w100p fl">
                                    
    <label>Branch</label><div class="cb1"></div><asp:DropDownList ID="drpBranch" runat="server"></asp:DropDownList><div class="cb10"></div>
    <label>Department</label><div class="cb1"></div><asp:DropDownList ID="drpDepartment" runat="server"></asp:DropDownList><div class="cb10"></div>
    
    <label>Swipe Time</label><div class="cb1"></div><asp:TextBox ID="txtSwipeInTime" runat="server"></asp:TextBox><div class="cb10"></div>
    <label>Swipe Location</label><div class="cb1"></div><asp:DropDownList ID="drpSwipeInLocation" runat="server"></asp:DropDownList><div class="cb10"></div>
    <label>Reader Type</label><div class="cb1"></div><asp:DropDownList ID="drpReaderType" runat="server">
        <asp:ListItem>IN</asp:ListItem>
        <asp:ListItem>OUT</asp:ListItem>
    </asp:DropDownList><div class="cb10"></div>
        <%--<label>Swipe Out time</label>--%><asp:TextBox ID="txtSwipeOutTime" runat="server" Visible="False"></asp:TextBox>
    <%--<label>Swipe Out Location</label>--%><asp:DropDownList ID="drpSwipeOutLocation" runat="server" Visible="False"></asp:DropDownList>
    <label>Approved By</label><div class="cb1"></div><asp:TextBox ID="txtAprovedBy" runat="server"></asp:TextBox><div class="cb10"></div>
                                    </div>
                            </li>
                        </ul>
                            
                    </div>
                <div class="cb10"></div><div class="cb10"></div>
               <div class="cb">
                   <div class="ma" style="width:200px;">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Edit Details" OnClick="btnSubmit_Click" OnClientClick="return EditdetailsEntry();" CssClass="submit ma" Style="background-color: #006533; width: auto; padding-left: 60px; padding-right: 60px;"/>
                                </div>
                            </div>                   
   
            <div class="cb10"></div><div class="cb10"></div>              
   
                        

   
   
    
                </div>
            </div>
         </section>

    </div>
    </form>
</body>
</html>
