<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditBranchTiming.aspx.cs" Inherits="NBAD.EditBranchTiming" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/StyleSheet.css" rel="stylesheet" />
    <script src="Scripts/NBAD/Validations/BranchTimingEntry.js"></script>
    <script src="Scripts/jquery.datetimepicker.js"></script>
    <link href="css/jquery.datetimepicker.css" rel="stylesheet" />
     <script>
         $(document).ready(function () {
            
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
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <section>
            <div class="middle_bg fl pr">
    
                <div class="upload_section_cntnr ma">
                    <div class="w944 ma upload_inst">
                    <h1>Branch Timing Entry</h1>
                
                </div>
                    
                     <div class="dataupload_bg ma">
                          <ul>
                            <li style="border-right:2px solid #FFFFFF;">
                                
                                <div class="w100p fl">
                                         <label>Branch</label><div class="cb1"></div><asp:DropDownList ID="drpBranch" runat="server"></asp:DropDownList><div class="cb10"></div>
                                                    </div>
                             </li>
                            <li style="width:373px; margin-left: 45px;">
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
    
    <asp:Button ID="btnSubmit" runat="server" Text="EditDetails" OnClick="btnSubmit_Click"   CssClass="submit fr" Style="background-color: #006533; width: auto; padding-left: 60px; padding-right: 60px;"/>
      </div>
                            </div>     
                    <div class="cb10"></div><div class="cb10"></div>  
                    
                     <div class="cb10"></div><div class="cb10"></div>              
   
                    
                    
                    </div>
                </div>
        </section>
    </div>
    </form>
</body>
</html>
