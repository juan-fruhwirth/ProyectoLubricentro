<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Vehiculos.aspx.cs" Inherits="Lubricentro.Vehiculos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Gestión de Vehículos</h2>

    <asp:Panel ID="pnlForm" runat="server">
        <div class="form-group">
            <label for="txtMarca">Marca</label>
            <asp:TextBox ID="txtMarca" runat="server" CssClass="form-control" />
        </div>
        <div class="form-group">
            <label for="txtModelo">Modelo</label>
            <asp:TextBox ID="txtModelo" runat="server" CssClass="form-control" />
        </div>
        <div class="form-group">
            <label for="txtAño">Año</label>
            <asp:TextBox ID="txtAño" runat="server" CssClass="form-control" />
        </div>
        <div class="form-group">
            <label for="txtPatente">Patente</label>
            <asp:TextBox ID="txtPatente" runat="server" CssClass="form-control" />
        </div>
        <div class="form-group">
            <label for="ddlTipoCombustible">Tipo de Combustible</label>
            <asp:DropDownList ID="ddlTipoCombustible" runat="server" CssClass="form-control"></asp:DropDownList>
        </div>
        <div class="form-group">
            <label for="txtObservaciones">Observaciones</label>
            <asp:TextBox ID="txtObservaciones" runat="server" TextMode="MultiLine" CssClass="form-control" />
        </div>
        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" />
    </asp:Panel>
</asp:Content>
