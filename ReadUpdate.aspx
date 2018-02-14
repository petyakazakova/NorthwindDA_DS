<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReadUpdate.aspx.cs" Inherits="NorthwindDA.ReadUpdate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Read Update</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="GridViewShippers" runat="server" OnSelectedIndexChanged="GridViewShippers_SelectedIndexChanged">
                <Columns>
                    <asp:CommandField ShowSelectButton="True" />
                </Columns>
            </asp:GridView>
            <br />
            <asp:TextBox ID="TextBoxCompany" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="LabelCompany" runat="server" Text="CompanyName"></asp:Label>
            <br />
            <br />
            <asp:TextBox ID="TextBoxPhone" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="LabelPhone" runat="server" Text="Phone"></asp:Label>
            <br />
            <br />
            <asp:Label ID="LabelMessage" runat="server" Text="No message"></asp:Label>
            <br />
            <br />
            <asp:Button ID="ButtonUpdate" runat="server" OnClick="ButtonUpdate_Click" Text="Update" />
        </div>
    </form>
</body>
</html>
