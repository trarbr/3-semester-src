<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetExchangeRateForm.aspx.cs" Inherits="ValutaWebForm.SetExchangeRateForm" %>

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
        <asp:Button ID="setExchangeRateButton" runat="server" Text="Set Exchange Rate" 
            OnClick="setExchangeRateButton_Click" />
        <br />
        <asp:Label ID="successLabel" runat="server" Text=""></asp:Label>
        <br />
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
            ErrorMessage="Exchange rate must be a number!" ControlToValidate="exchangeRateTextBox" 
            ValidationExpression="\d+(,\d+)?"></asp:RegularExpressionValidator>
        <br />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="exchangeRateTextBox" ErrorMessage="Please fill in an exchange rate!">
        </asp:RequiredFieldValidator>
    </div>
    </form>
</body>
</html>
