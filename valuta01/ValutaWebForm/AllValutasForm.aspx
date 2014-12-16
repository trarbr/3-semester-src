<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AllValutasForm.aspx.cs" Inherits="ValutaWebForm.AllValutasForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="All valutas"></asp:Label>
        <br />
        <asp:ListBox ID="ListBox1" runat="server" Width="200" Height="300"></asp:ListBox>
        <br />
        <asp:Button ID="Button1" runat="server" Text="Set exchange rate" OnClick="Button1_Click" />
    </div>
    </form>
</body>
</html>
