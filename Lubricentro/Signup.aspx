<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Signup.aspx.cs" Inherits="Lubricentro.Signup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Registrarse</h1>
    <div>
        <asp:TextBox ID="inputUsuario" placeholder="Nombre de usuario" runat="server" />
        <asp:TextBox ID="inputEmail" placeholder="Nombre de usuario" runat="server" />
        <asp:TextBox ID="inputContraseña" placeholder="Nombre de usuario" runat="server" />
        <asp:TextBox ID="inputConfirmarContraseña" placeholder="Nombre de usuario" runat="server" />
        <button ID="btnRegistrarse" onclick="">Registrarse</button>
    </div>
</asp:Content>
