<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditBranch.aspx.cs" Inherits="NBAD.EditBranch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <title></title>
    <script src="Scripts/NBAD/Validations/EditBranchEntry.js"></script>
        
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <label>Branch ID</label><asp:TextBox ID="txtBranchID" runat="server"></asp:TextBox>
    <label>Branch Name</label><asp:TextBox ID="txtBranchName" runat="server"></asp:TextBox>
    <asp:Button ID="btnEditSubmit" runat="server" Text="Edit Branch" OnClientClick="return EditBranchEntry(); " OnClick="btnEditSubmit_Click"  />
    </div>
    </form>
</body>
</html>
