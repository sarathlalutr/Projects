<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditBranch.aspx.cs" Inherits="NBAD.EditBranch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <title></title>
    <script src="Scripts/NBAD/Validations/EditBranchEntry.js"></script>
    <script src="Scripts/jquery.datetimepicker.js"></script>
    <link href="css/jquery.datetimepicker.css" rel="stylesheet" />
    <link href="css/StyleSheet.css" rel="stylesheet" />

         <script>
             $(document).ready(function () {
                 

                 $('#txtScheduleInTime').datetimepicker({
                     //mask: false,
                     //timepicker: false,
                     //value: today,
                     format: 'H:i',
                     closeOnDateSelect: true

                     //,
                     //formatDate: 'Y/m/d',
                 });
                 $('#txtScheduleOutTime').datetimepicker({
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
</head>
<body>
    <form id="form1" runat="server">
    <div>
      
      <section>
            <div class="middle_bg fl pr">
    
                <div class="upload_section_cntnr ma">
                     <div class="w944 ma upload_inst">
                    <h1>Edit Branch</h1>
                
                </div>
                    
                                      <div class="dataupload_bg ma">
                        <ul>
                            <li style="border-right:2px solid #FFFFFF;">
                                
                                <div class="w100p fl">
                                       
									   
									    <label>Branch ID</label><asp:TextBox ID="txtBranchID" runat="server"></asp:TextBox>
                                                    </div>
                             </li>
                            <li style="width:373px; margin-left: 45px;">
                                <div class="w100p fl">
                                   
								    <label>Branch Name</label><asp:TextBox ID="txtBranchName" runat="server"></asp:TextBox>
								   
								   
                                    </div>
                            </li>
                        </ul>
                            
                    </div>
                    

                    
     <div class="cb10"></div><div class="cb10"></div>
               <div class="cb">
                   <div class="ma" style="width:200px;">
    
    <asp:Button ID="btnEditSubmit" runat="server" Text="Edit Branch" OnClientClick="return EditBranchEntry(); " OnClick="btnEditSubmit_Click"  CssClass="submit fr" Style="background-color: #006533; width: auto; padding-left: 60px; padding-right: 60px;" />
    
      </div>
                            </div>     
                    <div class="cb10"></div><div class="cb10"></div>              
   
                       
                    
                    </div>
                </div>
         </section>    
    
   
        <%--<label>Schedule In time</label>--%><asp:TextBox ID="txtScheduleInTime" runat="server" Visible="False"></asp:TextBox>
     <%--<label>Schedule Out Time</label>--%><asp:TextBox ID="txtScheduleOutTime" runat="server" Visible="False"></asp:TextBox>
    </div>
    </form>
</body>
</html>
