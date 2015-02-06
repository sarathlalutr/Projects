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
<body class="login_bg">
    <form id="form1" runat="server">
    <section id="login_form" class="w1172 ma">
         <div class="admin_login_form w330 ma">	
           <div class="admin-login-form-image w330">
               <img width="205" height="81" align="middle" src="images/login -logo.png" alt="Login" title="Login">
           </div>
             
                <div class="admin-login-form-heading w330">
                 Login on webapp
                </div>
             <asp:Login ID="Login1" runat="server" FailureText="Username or password is incorrect" OnAuthenticate="Login1_Authenticate">
                 <LayoutTemplate>
                <table cellspacing="0" cellpadding="0" style="border-collapse:collapse;" >
                    <tbody>
                        <tr>
                            <td>
                                <div class="fl mtb10 pr">
                                    <%--<input type="text" placeholder="username" id="Login1_UserName" value="Username" name="Login1$UserName">--%>
                                     <asp:TextBox ID="UserName" runat="server"  placeholder="username" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" CssClass="mandatory" ControlToValidate="UserName"  ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                    <%--<span style="visibility:hidden;" class="mandatory" title="User Name is required." id="Login1_UserNameRequired">*</span>--%>
                                </div>
                                <div class="fl pr">
                                    <%--<input type="password" placeholder="password" id="Login1_Password" name="Login1$Password">--%> 
                                     <asp:TextBox ID="Password" runat="server" placeholder="password" TextMode="Password"></asp:TextBox> 
                                    <%--<span class="mandatory" title="Password is required." id="Login1_PasswordRequired">*</span>--%>
                                     <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" CssClass="mandatory" ControlToValidate="Password" ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="w330 mt10 fl">
                                     <%--<input type="submit" class="w80 ma submit" id="Login1_LoginButton" onclick="javascript:WebForm_DoPostBackWithOptions(new WebForm_PostBackOptions(&quot;Login1$LoginButton&quot;, &quot;&quot;, true, &quot;Login1&quot;, &quot;&quot;, false, false))" value="Login" name="Login1$LoginButton">--%>
                                    <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log In" ValidationGroup="Login1" CssClass="w80 ma submit" />
                                </div>
                            </td>
                      </tr>
                     </tbody>
                </table>
                     </LayoutTemplate>
                 </asp:Login>
        </div>
      </section>
        <section id="footer">
            <div class="w1172 ma">
            <div class="fr footer_txt">&copy; National Bank of Abu Dhabi</div>
            </div>
        </section>
    </form>
</body>
</html>
