<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditLocation.aspx.cs" Inherits="NBAD.EditLocation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/NBAD/Validations/EditLocationEntry.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <label>Location</label><asp:TextBox ID="txtLocation" runat="server"></asp:TextBox>
    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"  OnClientClick="return EditLocationEntry();" />
    
    </div>
    </form>
</body>
</html>
