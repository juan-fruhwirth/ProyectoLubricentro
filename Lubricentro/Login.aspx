<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Lubricentro.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <h1>Iniciar sesion</h1>
        <div class="form-group mb-3">
            <asp:Label ID="lblMessage" runat="server" Text="Ingrese su usuario y contraseña:" CssClass="mb-2"></asp:Label>
            <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control mb-2" Placeholder="Correo" style="margin-top: 15px;"></asp:TextBox>
            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control mb-2" TextMode="Password" Placeholder="Contraseña"></asp:TextBox>

            <asp:Label ID="lbl1" Text="" runat="server">

            </asp:Label>

        </div>
        <div class="form-group mb-3">
            <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-primary w-100" Text="Entrar" OnClick="btnLogin_Click" style="background-color: darkblue; color: white; border: none; padding: 10px 20px; width: 100%; cursor: pointer;" 
                onmouseover="this.style.backgroundColor='navy';" 
                onmouseout="this.style.backgroundColor='darkblue';"/>
        </div>

        <div class="mt-3">
            <a href="Signup.aspx" style="color: black; text-decoration: none;" onmouseover="this.style.color='darkblue';" 
               onmouseout="this.style.color='black';"
                >¿No tienes cuenta? Regístrate</a><br />
            <br />
            <a href="RecuperarContrasenia.aspx" style="color: black; text-decoration: none;"
                onmouseover="this.style.color='darkblue';" 
               onmouseout="this.style.color='black';"
                >¿Olvidaste tu contraseña?</a>
        </div>

    </div>
</asp:Content>
