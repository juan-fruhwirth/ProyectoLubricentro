<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Vehiculos.aspx.cs" Inherits="Lubricentro.Vehiculos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <!-- Columna izquierda: Imagen o fondo con degradado -->
            <div class="col-md-6 d-none d-md-block" style="height: 100vh;">
                <!-- Aquí puedes agregar una imagen en lugar del fondo si prefieres -->
                <div class="logo">
                    <img alt="logo lubricentro" height="50" src="https://static.vecteezy.com/system/resources/thumbnails/016/314/904/small/transparent-configuration-gear-icon-free-png.png" width="50"/>
                </div>
            </div>

            <!-- Columna derecha: Formulario de registro -->            
            <div class="col-md-6 p-5">
                <h2 class="text-center mb-4">Añadir un vehículo</h2>

                <div class="form-group mb-3">
                    <label for="inputMarca">Marca</label>
                    <asp:TextBox ID="inputMarca" CssClass="form-control" required="required" placeholder="Marca" runat="server" />
                    <asp:Label ID="lblErrorMarca" runat="server" ForeColor="Red"></asp:Label>
                </div>

                <div class="form-group mb-3">
                    <label for="inputModelo">Modelo</label>
                    <asp:TextBox ID="inputModelo" runat="server" CssClass="form-control" Placeholder="Modelo" required="required" />
                    <asp:Label ID="lblErrorModelo" runat="server" ForeColor="Red"></asp:Label>
                </div>

                <div class="form-group mb-3">
                    <label for="inputAño">Año</label>
                    <asp:TextBox ID="inputAño" runat="server" CssClass="form-control" Placeholder="Año"  required="required" />
                    <asp:Label ID="lblErrorAño" runat="server" ForeColor="Red"></asp:Label>
                </div>
           
                <div class="form-group mb-3">
                    <label for="inputPatente">Patente</label>
                    <asp:TextBox ID="inputPatente" CssClass="form-control" required="required" placeholder="Patente" runat="server" />
                    <asp:Label ID="lblErrorPatente" runat="server" ForeColor="Red"></asp:Label>
                </div>

                <div class="form-group mb-3">
                    <label for="inputTipoDeCombustible">Tipo de Combustible</label>
                    <asp:DropDownList ID="inputTipoDeCombustible" CssClass="form-control" runat="server" AppendDataBoundItems="true">
                        <asp:ListItem Text="Seleccione un tipo de combustible" Value="" />
                    </asp:DropDownList>
                    <asp:Label ID="lblErrorTipoDeCombustible" runat="server" ForeColor="Red"></asp:Label>
                </div>
                <div class="form-group mb-3">
                    <label for="inputObservaciones">Observaciones</label>
                    <asp:TextBox ID="inputObservaciones" CssClass="form-control" required="required" placeholder="Observaciones" runat="server" />
                    <asp:Label ID="lblErrorObservaciones" runat="server" ForeColor="Red"></asp:Label>
                </div>

                <div class="text-center">
                    <asp:Label ID="lblAñadirVehiculoStatus" runat="server" ForeColor="Green"></asp:Label>
                    <asp:Button  ID="btnAñadirVehiculo" CssClass="btn btn-primary w-100" Text="Añadir Vehículo" OnClick="AñadirVehiculo" runat="server" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
