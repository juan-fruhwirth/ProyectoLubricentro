<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Lubricentro.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <h1>Iniciar sesion</h1>
        <div class="form-group mb-3">
            <asp:Label ID="lblMessage" runat="server" Text="Ingrese su usuario y contraseña:"></asp:Label>
            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" Placeholder="Usuario"></asp:TextBox>
            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" Placeholder="Contraseña"></asp:TextBox>
        </div>
        <div class="form-group mb-3">
            <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-primary w-100" Text="Entrar" OnClick="btnLogin_Click" />
        </div>
    </div>
</asp:Content>
