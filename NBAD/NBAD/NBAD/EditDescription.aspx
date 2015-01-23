<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditDescription.aspx.cs" Inherits="NBAD.EditDescription" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/NBAD/Validations/EditDescriptionEntry.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <label>Descriptin</label><asp:TextBox ID="txtDescription" runat="server"></asp:TextBox>
    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"  OnClientClick="return EditDescriptionEntry();" />
    </div>
    </form>
</body>
</html>
