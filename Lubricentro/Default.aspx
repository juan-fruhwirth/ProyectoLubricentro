<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Lubricentro._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <h1>Pagina Default</h1>
    <br />

    <div class="form-group mb-3">
        <div class ="row">
            <a href="Vehiculos.aspx" style="color: black; text-decoration: none;" onmouseover="this.style.color='darkblue';" 
               onmouseout="this.style.color='black';"> Ir a Vehiculos</a>
        </div>
        
        <div class ="row">
            <a href="Turnos.aspx" style="color: black; text-decoration: none;" onmouseover="this.style.color='darkblue';" 
               onmouseout="this.style.color='black';"> Ir a Turnos </a>
        </div>
        
    </div>

</asp:Content>
