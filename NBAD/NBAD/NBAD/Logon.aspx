<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logon.aspx.cs" Inherits="NBAD.Logon" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <link href="css/StyleSheet.css" rel="stylesheet" />
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
    
        
          <div class="admin_login_form w340 ma">	
           <div class="admin-login-form-image w340">
               <img title="Login" alt="Login" align="center" width="48" height="48" src="Images/login-copy_03.png">

           </div>
              <div class="admin-login-form-image_welcome w340">
                   <img title="welcome" alt="Welcome" align="center" width="132" height="25" src="Images/login-copy_07.png">
                  </div>
                <asp:Login ID="Login1" runat="server" FailureText="Username or password is incorrect" OnAuthenticate="Login1_Authenticate">
                    <LayoutTemplate>
                        <div class="fl mtb10 pr">
                            <asp:TextBox ID="UserName" runat="server"  placeholder="username"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" CssClass="mandatory" ControlToValidate="UserName"  ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                        </div>
                       <div class="fl mt5 pr">
                            <asp:TextBox ID="Password" runat="server" placeholder="password" TextMode="Password"></asp:TextBox> 
                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" CssClass="mandatory" ControlToValidate="Password" ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                     </div>
                                        <%--<input type="text" placeholder="username" class="un_field" />--%>
                                      
                                        <%--<input type="text" placeholder="password" class="pw_field" />--%>
                                            <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>

                                        <%--<input type="submit" value="Login" />--%>
                        <div class="fl mt20">
                                        <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log In" ValidationGroup="Login1" CssClass="submit" />
                            </div>
                    </LayoutTemplate>
                </asp:Login>
       
      </div>
    
    </div>
    </form>
</body>
</html>
