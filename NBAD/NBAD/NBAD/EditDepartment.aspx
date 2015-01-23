<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditDepartment.aspx.cs" Inherits="NBAD.EditDepartment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/NBAD/Validations/EditDepartmentEntry.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <label>Department Name</label><asp:TextBox ID="txtDepartmentName" runat="server"></asp:TextBox>
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"  OnClientClick="return EditDepartmentEntry();" />
    </div>
    </form>
</body>
</html>
