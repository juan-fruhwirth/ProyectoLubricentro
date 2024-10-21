<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConfirmacionEmail.aspx.cs" Inherits="Lubricentro.ConfirmacionEmail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <h1>Confirmacion de correo electronico</h1>
        <div class="form-group mb-3">
            <asp:Label ID="lblMessage" runat="server" Text="Ingrese el codigo enviado por mail:"></asp:Label>
            <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control" Placeholder="Codigo"></asp:TextBox>
        </div>
        <div class="form-group mb-3">
            <asp:Button ID="btnConfirmar" runat="server" CssClass="btn btn-primary w-100" Text="Confirmar"/>
        </div>
    </div>
</asp:Content>
