<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="WebSite_Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="CSS/SubWindow.css" />
    <link rel="stylesheet" type="text/css" href="CSS/Login.css" />
    <link rel="stylesheet" type="text/css" href="CSS/Master.css" />
</head>
<body id="body1">
    <form id="form1" runat="server">
        <section id="topBar">
            <div id="topBarLeft">
                <span id="logoSpan"> <label>DungeonBuddy Online</label></span>
            </div>
            <div id="topBarRight">
                <table role="presentation">
                    <tr>
                        <td><label for="username">Username</label></td>
                        <td><label for="password">Password</label></td>
                    </tr>
                    <tr>
                        <td><asp:TextBox ID="usernameTextBox" name ="username" autocomplete="UserName" runat="server" CssClass="loginTextBox"></asp:TextBox></td>
                        <td><asp:TextBox ID="passwordTextBox"  autocomplete="password" runat="server" CssClass="loginTextBox" TextMode="Password"></asp:TextBox></td>
                        <td><asp:Button ID="loginButton" runat="server" Text="Login" CssClass="loginTextBox" OnClick="loginButton_Click" /></td>
                    </tr>
                </table>
            </div>
        </section>
        <div id="centerDiv" class="highlightDiv">
            <div id="innerDiv">
                <h1 class="pageHeader">Register New Account</h1>
                <table>
                    <tr>
                        <td class="center"><span class="margin">Username</span></td>
                        <td class="center"><span class="margin">Password</span></td>
                    </tr>
                    <tr>
                        <td><asp:TextBox ID="registerUsrNmTextBox" name="RegisterUsr" autocomplete="None" CssClass="roundTextBox" runat="server"></asp:TextBox></td>
                        <td><asp:TextBox ID="registerPasswordTextBox" CssClass="roundTextBox margin" runat="server" TextMode="Password"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td><asp:RequiredFieldValidator ID="registerUsernameFieldValidator" CssClass="center margin" ForeColor="Red" runat="server" ErrorMessage="You must enter a username." ControlToValidate="registerUsrNmTextBox" ValidationGroup="validate"></asp:RequiredFieldValidator></td>
                        <td><asp:RequiredFieldValidator ID="registerPasswordFieldValidator" CssClass="center margin" ForeColor="Red" runat="server" ErrorMessage="You must enter a password." ControlToValidate="registerPasswordTextBox" ValidationGroup="validate"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td class="center"><span class="margin">Email Address</span></td>
                        <td class="center"><span class="margin">Confirm Password</span></td>
                    </tr>
                    <tr>
                        <td><asp:TextBox ID="registerEmailTextBox" CssClass="roundTextBox" runat="server" TextMode="Email"></asp:TextBox></td>
                        <td><asp:TextBox ID="registerConfirmPasswordTextBox" CssClass="roundTextBox margin" runat="server" TextMode="Password"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td><asp:RequiredFieldValidator ID="registerEmailFieldValidator" CssClass="center margin" ForeColor="Red" runat="server" ErrorMessage="You must enter an email." ControlToValidate="registerEmailTextBox" ValidationGroup="validate"></asp:RequiredFieldValidator></td>
                        <td><asp:RequiredFieldValidator ID="registerConfirmPasswordFieldValidator" CssClass="center margin" ForeColor="Red" runat="server" ErrorMessage="You must enter a password." ControlToValidate="registerConfirmPasswordTextBox" ValidationGroup="validate"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td class="center"><span class="margin">Captcha Code</span></td>
                    </tr>
                    <tr>
                        <td><botdetect:webformscaptcha ID="loginCaptcha" UserInputID="captchaTextBox" runat="server" /></td>
                        <td><asp:TextBox ID="captchaTextBox" CssClass="roundTextBox margin" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td><botdetect:captchavalidator ID="loginCaptchaValidator" runat="server"
                    ControlToValidate="captchaTextBox" CaptchaControl="loginCaptcha"
                    ErrorMessage="Retype the characters exactly as they appear in the picture"
                    EnableClientScript="true" SetFocusOnError="true" ForeColor="Red" ValidationGroup="validate">
                Incorrect Captcha code
                </botdetect:captchavalidator></td>
                    </tr>
                    <tr>
                        <td><asp:Button ID="registerButton" CssClass="floatLeft largeButton" runat="server" Text="Register" OnClick="registerButton_Click" ValidationGroup="validate"/></td>
                    </tr>
                </table>
                <br />
                <asp:Label ID="angryLabel" runat="server" ForeColor="Red" Text="&nbsp;"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>