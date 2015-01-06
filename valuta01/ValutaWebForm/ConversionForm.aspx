<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConversionForm.aspx.cs" Inherits="ValutaWebForm.ConversionForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label5" runat="server" Text="Valuta Conversion"></asp:Label>
        <br />
        <asp:Label ID="Label1" runat="server" Text="Convert"></asp:Label>
        <asp:TextBox ID="amountTextBox" runat="server"></asp:TextBox>
        <asp:DropDownList ID="FromIsoDropDown" runat="server">
        </asp:DropDownList>
        <asp:Label ID="Label2" runat="server" Text="to"></asp:Label>
        <asp:DropDownList ID="ToIsoDropDown" runat="server">
        </asp:DropDownList>
        <asp:Button ID="convertButton" runat="server" Text="Do conversion!" 
            OnClick="convertButton_Click" />
        <asp:Label ID="Label3" runat="server" Text="Result: "></asp:Label>
        <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
        <br />
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
            ErrorMessage="Amount must be a number!" ControlToValidate="amountTextBox" 
            ValidationExpression="\d+(,\d+)?"></asp:RegularExpressionValidator>
        <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="CustomValidator"></asp:CustomValidator>
        <br />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="amountTextBox" ErrorMessage="Please fill in an amount!">
        </asp:RequiredFieldValidator>
    </div>
    </form>
</body>
</html>
