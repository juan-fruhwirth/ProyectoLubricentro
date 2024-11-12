<%@ Page Title="Administración de Usuarios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="Lubricentro.Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row" style="text-align: center; margin-top: 20px;">
        <h2>Administración de Usuarios</h2>

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
        <hr style="margin: 20px 0;">


        <asp:GridView ID="NivelesGridView" runat="server" AutoGenerateColumns="False" 
              DataKeyNames="NivelUsuario,Pagina"
              OnRowEditing="NivelesGridView_RowEditing"
              OnRowCancelingEdit="NivelesGridView_RowCancelingEdit"
              OnRowUpdating="NivelesGridView_RowUpdating">
    

    <Columns>
        <asp:BoundField DataField="NivelUsuario" HeaderText="Nivel de Autorizacion" ReadOnly="True" />
        <asp:BoundField DataField="Pagina" HeaderText="Pagina" ReadOnly="True" />
        <asp:TemplateField HeaderText="Permiso para la pagina">
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
    </div>
</asp:Content>