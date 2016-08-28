<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="new_user.aspx.cs" Inherits="Assign3Part2WebApp.new_user" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            text-align: center;
            font-size: large;
        }
        .auto-style2 {
            font-weight: bold;
            font-size: large;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="auto-style1">
    
        <strong style="font-size: xx-large">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        <br />
        Welcome new User
        <br />
        <br />
        </strong>Username&nbsp;&nbsp; <span style="font-size: xx-large">
        <asp:TextBox ID="TextBox1" runat="server" CssClass="auto-style2"></asp:TextBox>
        </span><strong>
        <br />
        </strong>Password&nbsp;&nbsp;&nbsp; <span style="font-size: xx-large">
        <asp:TextBox ID="TextBox2" runat="server" CssClass="auto-style2"></asp:TextBox>
        </span><strong>
        <br />
        </strong>Name&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="font-size: xx-large">
        <asp:TextBox ID="TextBox3" runat="server" CssClass="auto-style2"></asp:TextBox>
        </span>
        <br />
        Phone&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="font-size: xx-large">
        <asp:TextBox ID="TextBox4" runat="server" CssClass="auto-style2"></asp:TextBox>
        </span><strong>
        <br />
        </strong>Email&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="font-size: xx-large">
        <asp:TextBox ID="TextBox5" runat="server" CssClass="auto-style2"></asp:TextBox>
        </span><strong style="font-size: xx-large">
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" Height="32px" OnClick="Button1_Click" Text="Register" Width="100px" />
        </strong>
    
    </div>
    </form>
</body>
</html>
