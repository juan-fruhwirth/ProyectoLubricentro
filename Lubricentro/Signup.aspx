<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Signup.aspx.cs" Inherits="Lubricentro.Signup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class="container mt-5">
        <div class="row">
            <!-- Columna izquierda: Imagen o fondo con degradado -->
            <div class="col-md-6 d-none d-md-block" style="background: linear-gradient(to bottom right, #0052D4, #65C7F7, #9CECFB); height: 100vh;">
                <!-- Aquí puedes agregar una imagen en lugar del fondo si prefieres -->
                <div class="logo">
                    <img alt="logo lubricentro" height="50" src="https://static.vecteezy.com/system/resources/thumbnails/016/314/904/small/transparent-configuration-gear-icon-free-png.png" width="50"/>
                </div>
                <img src="https://as1.ftcdn.net/v2/jpg/03/39/70/90/1000_F_339709048_ZITR4wrVsOXCKdjHncdtabSNWpIhiaR7.jpg" class="img-fluid" alt="Sign Up" style="max-height: 100%; width: 100%; object-fit: cover;">
            </div>

            <!-- Columna derecha: Formulario de registro -->            
            <div class="col-md-6 p-5">
                <h2 class="text-center mb-4">Crear una cuenta</h2>

                <div class="form-group mb-3">
                    <label for="inputCorreo">Correo electrónico</label>
                    <asp:TextBox ID="inputCorreo" CssClass="form-control" required="required" placeholder="Correo electrónico" runat="server" />
                    <asp:Label ID="lblErrorCorreo" runat="server" ForeColor="Red"></asp:Label>
                </div>

                <div class="form-group mb-3">
                    <label for="inputNombre">Nombre</label>
                    <asp:TextBox ID="inputNombre" runat="server" CssClass="form-control" Placeholder="Nombre" required="required" />
                    <asp:Label ID="lblErrorNombre" runat="server" ForeColor="Red"></asp:Label>
                </div>

                <div class="form-group mb-3">
                    <label for="inputApellido">Apellido</label>
                    <asp:TextBox ID="inputApellido" runat="server" CssClass="form-control" Placeholder="Apellido"  required="required" />
                    <asp:Label ID="lblErrorApellido" runat="server" ForeColor="Red"></asp:Label>
                </div>
               
                <div class="form-group mb-3">
                    <label for="inputTelefono">Teléfono celular</label>
                    <asp:TextBox ID="inputTelefono" CssClass="form-control" required="required" placeholder="Teléfono celular" runat="server" />
                    <asp:Label ID="lblErrorTelefono" runat="server" ForeColor="Red"></asp:Label>
                </div>

                <div class="form-group mb-3">
                    <label for="inputContraseña">Contraseña</label>
                    <asp:TextBox ID="inputContraseña" TextMode="Password" CssClass="form-control" required="required" placeholder="Contraseña" runat="server" />
                    <asp:Label ID="lblErrorContraseña" runat="server" ForeColor="Red"></asp:Label>
                </div>
                <div class="form-group mb-3">
                    <label for="inputConfirmarContraseña">Confirmar contraseña</label>
                    <asp:TextBox ID="inputConfirmarContraseña" TextMode="Password" CssClass="form-control" required="required" placeholder="Confirmar contraseña" runat="server" />
                    <asp:Label ID="lblErrorConfirmarContraseña" runat="server" ForeColor="Red"></asp:Label>
                </div>

                <div class="text-center">
                    <asp:Button  ID="btnRegistrarse" CssClass="btn btn-primary w-100" Text="Registrarse" OnClick="Registrarse" runat="server" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>
