<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RecuperarContrasenia.aspx.cs" Inherits="Lubricentro.RecuperarContraseña" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <h1>Recuperar contraseña</h1>
        
        <!-- Primer Paso: Ingreso de correo -->
        <asp:Panel ID="PanelEmail" runat="server">
            <div class="form-group mb-3">
                <asp:Label ID="lblMessage" runat="server" Text="Ingrese el correo electrónico para recibir instrucciones para cambiar la contraseña:"></asp:Label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Placeholder="Correo electrónico"></asp:TextBox>
                <asp:Label ID="lblErrorCorreo" Text="" runat ="server"></asp:Label>
            </div>
            <div class="form-group mb-3">
                <asp:Button ID="btnResetPassword" runat="server" CssClass="btn btn-primary w-100" Text="Enviar" OnClick="btnResetPassword_Click" />
            </div>

            <div class="form-group mb-3">
                <asp:Label ID="lblEnvioCorreo" Text="" runat ="server"></asp:Label>
            </div>
        </asp:Panel>

        <!-- Segundo Paso: Ingreso del código -->
        <asp:Panel ID="PanelCode" runat="server" Visible="false">
            <div class="form-group mb-3">
                <asp:Label ID="lblCode" runat="server" Text="Ingrese el código que le hemos enviado al correo:"></asp:Label>
                <asp:TextBox ID="txtCode" runat="server" CssClass="form-control" Placeholder="Código de verificación"></asp:TextBox>
            </div>
            <div class="form-group mb-3">
                <asp:Button ID="btnVerifyCode" runat="server" CssClass="btn btn-primary w-100" Text="Verificar Código" OnClick="btnVerifyCode_Click" />
            </div>
            <div class="form-group mb-3">
               <asp:Label ID="lblConfirmarCodigo" Text="" runat ="server"></asp:Label>
           </div>

        </asp:Panel>

        <!-- Tercer Paso: Ingreso de nueva contraseña -->
        <asp:Panel ID="PanelNewPassword" runat="server" Visible="false">
            <div class="form-group mb-3">
                <asp:Label ID="lblNewPassword" runat="server" Text="Ingrese su nueva contraseña:"></asp:Label>
                <asp:TextBox ID="txtNuevaContrasenia" runat="server" CssClass="form-control" TextMode="Password" Placeholder="Nueva Contraseña"></asp:TextBox>
                <asp:Label ID="lblErrorContraseña" Text="" runat ="server"></asp:Label>
            </div>

            <div class="form-group mb-3">
                <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirme su nueva contraseña:"></asp:Label>
                <asp:TextBox ID="txtConfirmarContrasenia" runat="server" CssClass="form-control" TextMode="Password" Placeholder="Confirmar Contraseña"></asp:TextBox>
                <asp:Label ID="lblErrorConfirmarContraseña" Text="" runat ="server"></asp:Label>
            </div>

            <div class="form-group mb-3">
                <asp:Button ID="btnChangePassword" runat="server" CssClass="btn btn-primary w-100" Text="Cambiar Contraseña" OnClick="btnChangePassword_Click" />
            </div>

            <div class="form-group mb-3">
               <asp:Label ID="lblResultado" Text="" runat ="server"></asp:Label>
           </div>
        </asp:Panel>
    </div>
</asp:Content>
