<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditDesignation.aspx.cs" Inherits="NBAD.EditDesignation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <script src="Scripts/NBAD/Validations/EditDesignationEntry.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <label>Designation</label><asp:TextBox ID="txtDesignationName" runat="server"></asp:TextBox>
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"  OnClientClick="return EditDesignationEntry();" />
    </div>
    </form>
</body>
</html>
