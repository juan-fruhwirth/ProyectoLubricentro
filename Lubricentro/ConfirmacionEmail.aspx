<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConfirmacionEmail.aspx.cs" Inherits="Lubricentro.ConfirmacionEmail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <h1>Confirmacion de correo electronico</h1>

        <div class="form-group mb-3">
            <asp:Label ID="lblMessage" runat="server" Text="Ingrese el codigo enviado por mail: "></asp:Label>
            <asp:Label ID="Label1" runat="server" Text="Longitud: 6 digitos"></asp:Label>
            <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control" Placeholder="Codigo"></asp:TextBox>
            
        </div>

        <div class="form-group mb-3">
            <asp:Button ID="btnConfirmar2" runat= "server"  CssClass="btn btn-primary" Text="Confirmar Codigo" OnClick="ConfirmarCodigoClick"/>
            <asp:Label ID="txtResultadoConfirmacion" runat="server" Text=""></asp:Label>

        </div>

            <div class="form-group mb-3">
            <asp:Label ID="Label2" runat="server" Text="Reenviar el codigo"></asp:Label>
            <asp:Button ID="btnReenviarCodigo" runat= "server"  CssClass="btn btn-primary" Text="Reenviar Codigo" OnClick="ReenviarCodigoClick"/>
            <asp:Label ID="Label3" runat="server" Text=""></asp:Label>

        </div>

        <!-- Comentar el botón deshabilitado
        <div class="form-group mb-3">
        <asp:Button ID="btnConfirmar" runat="server" CssClass="btn btn-primary w-100" Text="Confirmar"/>
        </div>
        -->
        
    </div>
</asp:Content>
