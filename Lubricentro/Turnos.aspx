<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Turnos.aspx.cs" Inherits="Lubricentro.Turnos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class="container" style="padding: 20px; border-radius: 8px;">
        <div class="row">
            <!-- Columna Izquierda: Tabla de "Mis Turnos" -->
            <div class="col-md-6">
                <div class="card p-4 shadow">
                    <h3 class="text-center mb-4">Mis Turnos</h3>
                    <asp:GridView ID="gridTurnos" runat="server" OnRowCommand="gridTurnos_RowCommand" CssClass="table table-bordered table-striped" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="TurnoID" HeaderText="ID" />
                            <asp:BoundField DataField="FechaHora" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="Estado" HeaderText="Estado" />
                            <asp:BoundField DataField="Servicio" HeaderText="Servicio" />
                            <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="${0:F2}" />
                            <asp:BoundField DataField="Vehiculo" HeaderText="Vehículo" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-sm btn-danger" CommandName="Eliminar" CommandArgument='<%# Eval("TurnoID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

            <!-- Columna Derecha: Formulario para Registrar Nuevo Turno -->
            <div class="col-md-6">
                <div class="card p-4 shadow">
                    <h3 class="text-center mb-4">Registrar Nuevo Turno</h3>

                    <div class="form-group mb-3">
                        <label for="inputFechaHora">Fecha y Hora</label>
                        <asp:TextBox ID="inputFechaHora" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:Calendar ID="calendarFechaHora" OnSelectionChanged="calendarFechaHora_SelectionChanged" runat="server"></asp:Calendar>
                    </div>

                    <div class="form-group mb-3">
                        <label for="inputVehiculo">Vehículo</label>
                        <asp:DropDownList ID="inputVehiculo" CssClass="form-control" runat="server" AppendDataBoundItems="true">
                            <asp:ListItem Text="Seleccione un vehículo" Value="" />
                        </asp:DropDownList>
                    </div>

                    <div class="form-group mb-3">
                        <label for="inputServicio">Servicio</label>
                        <asp:DropDownList ID="inputServicio" CssClass="form-control" runat="server" AppendDataBoundItems="true">
                            <asp:ListItem Text="Seleccione un servicio" Value="" />
                        </asp:DropDownList>
                    </div>

                    <div class="text-center">
                        <asp:Label ID="lblStatusTurno" runat="server" ForeColor="Green"></asp:Label>
                        <asp:Button ID="btnRegistrarTurno" CssClass="btn btn-primary w-100" Text="Registrar Turno" OnClick="RegistrarTurno" runat="server" />
                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>
