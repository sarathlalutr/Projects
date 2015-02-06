<%@ Page Title="" Language="C#" MasterPageFile="~/NBADMasterPage.Master" AutoEventWireup="true" CodeBehind="allSwipeReport1.aspx.cs" Inherits="NBAD.WebForm12" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/NBAD/Validations/allswipeReport.js"></script>
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
    <label>All Swipe Report</label>
    <label>From</label><asp:TextBox ID="txtFromDate" runat="server"></asp:TextBox>
    <label>To</label><asp:TextBox ID="txtToDate" runat="server"></asp:TextBox>
    <asp:Button ID="btnSubmit" runat="server" Text="Generate Report" OnClientClick=" return allswipeReport(); "/>
</asp:Content>
