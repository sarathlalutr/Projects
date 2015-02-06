<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditLocation.aspx.cs" Inherits="NBAD.EditLocation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/NBAD/Validations/EditLocationEntry.js"></script>
     <script src="Scripts/jquery.datetimepicker.js"></script>
    <link href="css/jquery.datetimepicker.css" rel="stylesheet" />
    <link href="css/StyleSheet.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        
         <section>
            <div class="middle_bg fl pr">
    
                <div class="upload_section_cntnr ma">
                     <div class="w944 ma upload_inst">
                    <h1>Edit Location</h1>
                
                </div>
                    
                                      <div class="dataupload_bg ma">
                        <ul>
                            <li >
                                
                                <div class="w100p fl">
                                       
     <label>Location</label><asp:TextBox ID="txtLocation" runat="server"></asp:TextBox>
									   
									   
                                                    </div>
                             </li>
                          
                        </ul>
                            
                    </div>
                    

                    
     <div class="cb10"></div><div class="cb10"></div>
               <div class="cb">
                   <div class="ma" style="width:200px;">
    
    <asp:Button ID="btnSubmit" runat="server" Text="Edit Details" OnClick="btnSubmit_Click"  OnClientClick="return EditLocationEntry();" CssClass="submit fr" Style="background-color: #006533; width: auto; padding-left: 60px; padding-right: 60px;" />
    
      </div>
                            </div>     
                    <div class="cb10"></div><div class="cb10"></div>              
   
                       
                    
                    </div>
                </div>
         </section> 
    </form>
</body>
</html>
