<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RecuperarContrasenia.aspx.cs" Inherits="Lubricentro.RecuperarContraseña" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <h1>Recuperar contraseña</h1>
        <div class="form-group mb-3">
            <asp:Label ID="lblMessage" runat="server" Text="Ingrese a que correo quiere que le llegan las indicaciones para cambiar su contraseña:"></asp:Label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Placeholder="Correo electrónico"></asp:TextBox>
        </div>
        <div class="form-group mb-3">
            <asp:Button ID="btnResetPassword" runat="server" CssClass="btn btn-primary w-100" Text="Enviar" OnClick="btnResetPassword_Click" />
        </div>
    </div>
</asp:Content>
