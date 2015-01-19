<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logon.aspx.cs" Inherits="NBAD.Logon" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">


.login_field {
    margin: 60px auto auto;
    width: 320px;
}

    h2 {
    background: transparent;
    border: 0;
    font-size: 100%;
    margin: 0;
    outline: 0;
    padding: 0;
}

        .auto-style1 {
            font-size: 100%;
            border-style: none;
            border-color: inherit;
            border-width: 0;
            margin: 0;
            padding: 0;
            background:;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <div class="auto-style1" style="outline: 0;">
            <h2>&nbsp;</h2>
            <div class="auto-style1" style="outline: 0;">
                <asp:Login ID="Login1" runat="server" FailureText="Username or password is incorrect" OnAuthenticate="Login1_Authenticate">
                    <LayoutTemplate>
                        <div class="cb">
                            <asp:TextBox ID="UserName" runat="server" Class="un_field" placeholder="username"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" CssClass="required_span" ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                        </div>
                        <div class="cb">
                            <asp:TextBox ID="Password" runat="server" Class="pw_field" placeholder="password" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" CssClass="required_span" ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                        </div>
                                        <%--<input type="text" placeholder="username" class="un_field" />--%>
                                      
                                        <%--<input type="text" placeholder="password" class="pw_field" />--%>
                                            <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>

                                        <%--<input type="submit" value="Login" />--%>
                                        <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log In" ValidationGroup="Login1" />
                    </LayoutTemplate>
                </asp:Login>
            </div>
        </div>
    
    </div>
    </form>
</body>
</html>
