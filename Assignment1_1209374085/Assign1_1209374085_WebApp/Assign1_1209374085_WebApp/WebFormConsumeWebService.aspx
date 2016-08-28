<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebFormConsumeWebService.aspx.cs" Inherits="Assign1_1209374085_WebApp.WebFormConsumeWebService" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
           
        Convert Celsius to Farenheit&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TextBox1" runat="server">0</asp:TextBox>
&nbsp;&nbsp;
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="C2F" />
&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label1" runat="server" Text="Result C2F"></asp:Label>
           
    </div>
        <p>
&nbsp;Convert Farenheit to Celsius&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TextBox2" runat="server">0</asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="F2C" />
&nbsp;
            <asp:Label ID="Label2" runat="server" Text="Result F2C"></asp:Label>
        </p>
    </form>
</body>
</html>
