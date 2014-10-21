<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetExchangeRateForm.aspx.cs" Inherits="ValutaWebForm.FindValutaForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="Set Exchange Rate"></asp:Label>
        <br />
        <asp:Label ID="Label2" runat="server" Text="Select Valuta"></asp:Label>
        <asp:DropDownList ID="valutaDropDown" runat="server" >
        </asp:DropDownList>
        <br />
        <asp:Label ID="Label3" runat="server" Text="Exchange Rate"></asp:Label>
        <asp:TextBox ID="exchangeRateTextBox" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="setExchangeRateButton" runat="server" Text="Button" 
            OnClick="setExchangeRateButton_Click" />
        <br />

    
    </div>
    </form>
</body>
</html>
