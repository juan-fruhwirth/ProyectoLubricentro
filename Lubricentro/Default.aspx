<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Lubricentro._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <div class="titulo-contenedor">
        <h1 class="titulo-pagina">Página Default</h1>
    </div>


    <div class="imagen-contenedor">
    <div>
        <a href="Vehiculos.aspx">
        <img src="https://media.lacapital.com.ar/p/4450c9de18b45422b0454ed04d1e5146/adjuntos/203/imagenes/100/079/0100079307/642x0/smart/imagefiatjpg.jpg" alt="Vehículos" class = "imagen-auto"/>
        </a>
        <div class="texto-bajo-imagen">Ir a Vehículos</div>
    </div>
    <div>
        <a href="Turnos.aspx">
            <img src="https://www.ceac.digital/images/ceac/iconos-head/iconoshead-04.png" alt="Turnos" class="imagen" />
        </a>
        <div class="texto-bajo-imagen">Ir a Turnos</div>
    </div>
</div>


</asp:Content>
