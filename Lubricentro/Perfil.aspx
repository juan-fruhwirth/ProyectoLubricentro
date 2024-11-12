<%@ Page Language="C#"  MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="Lubricentro.Perfil" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Perfil de Usuario</h2>
    
    <asp:Label ID="lblMensaje" runat="server" ForeColor="Green"></asp:Label>

    <asp:Label ID="lblNombre" runat="server" Text="Nombre: "></asp:Label>
    <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
    <br />

    <asp:Label ID="lblApellido" runat="server" Text="Apellido: "></asp:Label>
    <asp:TextBox ID="txtApellido" runat="server"></asp:TextBox>
    <br />

    <asp:Label ID="lblTelefono" runat="server" Text="Teléfono: "></asp:Label>
    <asp:TextBox ID="txtTelefono" runat="server"></asp:TextBox>
    <br />



    <asp:Button ID="btnGuardar" runat="server" Text="Guardar Cambios" OnClick="btnGuardar_Click" />
</asp:Content>