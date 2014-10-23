<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddValutaForm.aspx.cs" Inherits="ValutaWebForm.AddValutaForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label1" runat="server" Text="Add Valuta"></asp:Label>
        <br />
        <asp:Label ID="Label2" runat="server" Text="Name"></asp:Label>
        <asp:TextBox ID="nameTextBox" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label3" runat="server" Text="ISO code"></asp:Label>
        <asp:TextBox ID="isoTextBox" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label4" runat="server" Text="Exchange Rate"></asp:Label>
        <asp:TextBox ID="exchangeRateTextBox" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="addValutaButton" runat="server" Text="Add Valuta" 
            OnClick="addValutaButton_Click" />
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
