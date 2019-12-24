<%@ Page Title="Calculator" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Calculator.aspx.cs" Inherits="WebApplication1.Calculator"%>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>

    <asp:TextBox ID="result" runat="server" Text="0" Height="34px" Width="216px" ReadOnly="true"></asp:TextBox>

    <br />

    <br />

    <asp:Button ID="Button1" runat="server" Text="1" Height="36px" Width="41px" OnClick="Button_Click"/>
    <asp:Button ID="Button2" runat="server" Text="2" Height="36px" Width="41px" OnClick="Button_Click"/>
    <asp:Button ID="Button3" runat="server" Text="3" Height="36px" Width="41px" OnClick="Button_Click"/>
    <asp:Button ID="Button_plus" runat="server" Text="+" Height="36px" Width="41px" OnClick="Operator_Click"/>

    <br />
    <br />

    <asp:Button ID="Button4" runat="server" Text="4" Height="36px" Width="41px" OnClick="Button_Click"/>
    <asp:Button ID="Button5" runat="server" Text="5" Height="36px" Width="41px" OnClick="Button_Click"/>
    <asp:Button ID="Button6" runat="server" Text="6" Height="36px" Width="41px" OnClick="Button_Click"/>
    <asp:Button ID="Button_minus" runat="server" Text="-" Height="36px" Width="41px" OnClick="Operator_Click"/>

    <br />
    <br />

    <asp:Button ID="Button7" runat="server" Text="7" Height="36px" Width="41px" OnClick="Button_Click"/>
    <asp:Button ID="Button8" runat="server" Text="8" Height="36px" Width="41px" OnClick="Button_Click"/>
    <asp:Button ID="Button9" runat="server" Text="9" Height="36px" Width="41px" OnClick="Button_Click"/>
    <asp:Button ID="Button_multiplier" runat="server" Text="x" Height="36px" Width="41px" OnClick="Operator_Click"/>

    <br />
    <br />

    <asp:Button ID="Button_c" runat="server" Text="C" Height="36px" Width="41px" OnClick="Clear_Click"/>
    <asp:Button ID="Button0" runat="server" Text="0" Height="36px" Width="41px" OnClick="Button_Click"/>
    <asp:Button ID="Button_equal" runat="server" Text="=" Height="36px" Width="41px" OnClick="Equal_Click"/>
    <asp:Button ID="Button_divide" runat="server" Text="/" Height="36px" Width="41px"  OnClick="Operator_Click"/>

    <br />
    <br />

    <asp:Button ID="Button_CE" runat="server" Text="CE" Height="36px" Width="41px" OnClick="Clear_Click"/>



</asp:Content>

