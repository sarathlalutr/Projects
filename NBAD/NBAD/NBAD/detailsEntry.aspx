<%@ Page Title="" Language="C#" MasterPageFile="~/NBADMasterPage.Master" AutoEventWireup="true" CodeBehind="detailsEntry.aspx.cs" Inherits="NBAD.WebForm6" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/NBAD/Validations/detailsEntry.js"></script>
    <script>
        $(document).ready(function () {
 
            $('#ContentPlaceHolder1_txtSwipeInTime').datetimepicker({
                //mask: false,
                //timepicker: false,
                //value: today,
                format: 'd/m/Y H:i',
                closeOnDateSelect: true

                //,
                //formatDate: 'Y/m/d',
            });
            $('#ContentPlaceHolder1_txtSwipeOutTime').datetimepicker({
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
    
<label>Employee Id</label><asp:TextBox ID="txtEmployeeId" runat="server"></asp:TextBox>
    <label>Employee Name</label><asp:TextBox ID="txtEmployeeName" runat="server"></asp:TextBox>
    <label>Gender</label><asp:DropDownList ID="drpGender" runat="server">
        <asp:ListItem>Male</asp:ListItem>
        <asp:ListItem>Female</asp:ListItem>
    </asp:DropDownList>
    <label>Designation</label><asp:DropDownList ID="drpDesignation" runat="server"></asp:DropDownList>
    <label>Description</label><asp:DropDownList ID="drpDescription" runat="server"></asp:DropDownList>
    <label>Branch</label><asp:DropDownList ID="drpBranch" runat="server"></asp:DropDownList>
    <label>Department</label><asp:DropDownList ID="drpDepartment" runat="server"></asp:DropDownList>
    
    <label>Swipe In time</label><asp:TextBox ID="txtSwipeInTime" runat="server"></asp:TextBox>
    <label>Swipe In Location</label><asp:DropDownList ID="drpSwipeInLocation" runat="server"></asp:DropDownList>
        <label>Swipe Out time</label><asp:TextBox ID="txtSwipeOutTime" runat="server"></asp:TextBox>
    <label>Swipe Out Location</label><asp:DropDownList ID="drpSwipeOutLocation" runat="server" ></asp:DropDownList>
    <asp:Button ID="btnSubmit" runat="server" Text="Submit" Height="26px" OnClick="btnSubmit_Click" OnClientClick="return detailsEntry();"/>
</asp:Content>
