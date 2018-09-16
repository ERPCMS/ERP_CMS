<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ERP_CMS.Login" Async="true" %>

<
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <title>Login</title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta content="" name="description" />

    <meta content="themes-lab" name="author" />
    <link rel="shortcut icon" href="assets/img/favicon.png">
    <link href="Styles/style.css" rel="stylesheet">
    <link href="Styles/ui.css" rel="stylesheet">
    <link href="Styles/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="Styles/line-icons.css" rel="stylesheet" type="text/css" />
    <link href="Styles/simple-line-icons.css" rel="stylesheet" type="text/css" />


    <%--<link href="assets/plugins/bootstrap-loading/lada.min.css" rel="stylesheet">--%>
</head>
<body class="account2" data-page="login" style="background-color: #001633">
    <form id="form1" runat="server" class="form-signin" role="form">
        <!-- BEGIN LOGIN BOX -->
        <div class="container" id="login-block">
            <i class="user-img icons-faces-users-03"></i>
            <br />
            <div class="account-info" style="background:#11376f;">
                <a href="#" class="logo"></a>
                <ul>
                    <li><i class="icon-arrow-right"></i>Automated Solution</li>
                    <li><i class="icon-arrow-right"></i>Intregated Modules</li>
                    <li><i class="icon-arrow-right"></i>User Friendly Interface</li>
                    <li><i class="icon-arrow-right"></i>Responsive Layout</li>

                </ul>
            </div>
            <div class="account-form">
                <h3><strong>Sign in</strong> to your account</h3>
                <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                    <p class="text-danger">
                        <asp:Literal runat="server" ID="FailureText" />
                    </p>
                </asp:PlaceHolder>
                <div class="append-icon">
                    <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control form-white username" placeholder="Username" ></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtUserName"
                                CssClass="text-danger" ErrorMessage="The UserName is required." />
                    <i class="icon-user"></i>
                </div>
                <div class="append-icon m-b-20">
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control form-white password" placeholder="Password"  TextMode="Password"></asp:TextBox>
                     <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPassword" CssClass="text-danger" ErrorMessage="The password is required." />
                       <i class="icon-lock"></i>
                </div>
                <asp:Button runat="server" type="submit" ID="btnsubmit" OnClick="btnLogin_Click" class="btn btn-lg btn-dark btn-rounded ladda-button" CommandName="Login" Text="Sign In" />

                <div class="form-footer">
                    <div class="clearfix">
                        <p class="new-here"><a href="user-signup-v2.html"></a></p>
                    </div>
                </div>

            </div>
        </div>
        <p class="account-copyright">
           <%-- <span>Copyright © 2016 GCS (Pvt) Ltd. </span><span></span><span>All rights reserved.</span>
      --%>  </p>

    </form>
</body>
</html>
