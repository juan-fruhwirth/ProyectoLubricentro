<%@ Page Title="Administración de Usuarios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="Lubricentro.Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row" style="text-align: center; margin-top: 20px;">
        <h2>Administración</h2>

        <!-- Botones para alternar entre paneles -->
    <div class="container text-center mt-4">
        <div class="d-flex justify-content-center mt-3 mb-5">
    <asp:Button ID="btnUsuarios" runat="server" Text="Usuarios" CssClass="btn btn-primary mx-2" OnClick="btnUsuarios_Click" />
    <asp:Button ID="btnNiveles" runat="server" Text="Niveles" CssClass="btn btn-secondary mx-2" OnClick="btnNiveles_Click" />
    <asp:Button ID="btnTurnos" runat="server" Text="Turnos" CssClass="btn btn-success mx-2" OnClick="btnTurnos_Click" />
        </div>
    </div>


        <!-- Panel Usuarios -->
        <asp:Panel ID="panelUsuarios" runat="server" Visible="false" CssClass="panel mx-auto my-4" Style="max-width: 80%;">
            <asp:GridView ID="UsuariosGridView" runat="server" AutoGenerateColumns="False" 
                          DataKeyNames="Correo"
                          OnRowEditing="UsuariosGridView_RowEditing"
                          OnRowCancelingEdit="UsuariosGridView_RowCancelingEdit"
                          OnRowUpdating="UsuariosGridView_RowUpdating">
                <Columns>
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" ReadOnly="True" />
                    <asp:BoundField DataField="Correo" HeaderText="Correo Electrónico" ReadOnly="True" />
                    <asp:TemplateField HeaderText="Nivel de Autorización">
                        <ItemTemplate>
                            <asp:Label ID="NivelLabel" runat="server" Text='<%# Eval("NivelUsuario") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="NivelDropDownList" runat="server">
                                <asp:ListItem Text="Seleccione un nivel" Value=""></asp:ListItem>
                                <asp:ListItem Text="1(Básico)" Value="1"></asp:ListItem>
                                <asp:ListItem Text="2(Administrador)" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowEditButton="True" />
                </Columns>
            </asp:GridView>
        </asp:Panel>

        <!-- Panel Niveles -->
        <asp:Panel ID="panelNiveles" runat="server" Visible="false" CssClass="panel mx-auto my-4" Style="max-width: 80%;">
            <asp:GridView ID="NivelesGridView" runat="server" AutoGenerateColumns="False" 
                          DataKeyNames="NivelUsuario,Pagina"
                          OnRowEditing="NivelesGridView_RowEditing"
                          OnRowCancelingEdit="NivelesGridView_RowCancelingEdit"
                          OnRowUpdating="NivelesGridView_RowUpdating">
                <Columns>
                    <asp:BoundField DataField="NivelUsuario" HeaderText="Nivel de Autorización" ReadOnly="True" />
                    <asp:BoundField DataField="Pagina" HeaderText="Página" ReadOnly="True" />
                    <asp:TemplateField HeaderText="Permiso para la página">
                        <ItemTemplate>
                            <asp:Label ID="EstadoIDLabel" runat="server" Text='<%# Eval("EstadoID") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="EstadoIDDropDownList" runat="server">
                                <asp:ListItem Text="Seleccione un nivel" Value=""></asp:ListItem>
                                <asp:ListItem Text="0 (Sin Acceso)" Value="0"></asp:ListItem>
                                <asp:ListItem Text="1 (Tiene Acceso)" Value="1"></asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowEditButton="True" />
                </Columns>
            </asp:GridView>
        </asp:Panel>

        <!-- Panel Turnos -->
        <asp:Panel ID="panelTurnos" runat="server" Visible="false" CssClass="panel mx-auto my-4" Style="max-width: 80%;">
            <asp:GridView ID="TurnosGridView" runat="server" AutoGenerateColumns="False" 
                          DataKeyNames="TurnoID"
                          OnRowEditing="TurnosGridView_RowEditing"
                          OnRowCancelingEdit="TurnosGridView_RowCancelingEdit"
                          OnRowUpdating="TurnosGridView_RowUpdating">
                <Columns>
                    <asp:BoundField DataField="TurnoID" HeaderText="TurnoID" ReadOnly="True" />
                    <asp:BoundField DataField="UsuarioID" HeaderText="UsuarioID" ReadOnly="True" />
                    <asp:BoundField DataField="VehiculoID" HeaderText="VehiculoID" ReadOnly="True" />
                    <asp:BoundField DataField="ServicioID" HeaderText="ServicioID" ReadOnly="True" />
                    <asp:BoundField DataField="TurnoID" HeaderText="Turno ID" ReadOnly="True" />
                    <asp:BoundField DataField="ServicioNombre" HeaderText="Servicio" ReadOnly="True" />
                    <asp:BoundField DataField="Precio" HeaderText="Precio"  ReadOnly="True"/>
                    <asp:BoundField DataField="MinutosEstimados" HeaderText="Minutos Estimados" ReadOnly="True"/>
                    <asp:BoundField DataField="FechaHora" HeaderText="Fecha y Hora" ReadOnly="True" />
                    <asp:TemplateField HeaderText="Estado de Turno">
                        <ItemTemplate>
                            <asp:Label ID="EstadoTurnoIDLabel" runat="server" Text='<%# Eval("EstadoTurnoID") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                <asp:DropDownList ID="EstadoTurnoIDDropDownList" runat="server">
                    <asp:ListItem Text="Seleccione un nivel" Value=""></asp:ListItem>
                    <asp:ListItem Text="1 (Pendiente)" Value="1"></asp:ListItem>
                    <asp:ListItem Text="2 (Confirmado)" Value="2"></asp:ListItem>
                    <asp:ListItem Text="3 (Cancelado)" Value="3"></asp:ListItem>
                </asp:DropDownList>
                        </EditItemTemplate>
                        
                    </asp:TemplateField>
                    <asp:BoundField DataField="EstadoNombre" HeaderText="Descripcion" ReadOnly="True" />
                    <asp:CommandField ShowEditButton="True" />
                </Columns>
            </asp:GridView>
        </asp:Panel>
    </div>
</asp:Content>
