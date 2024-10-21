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
                    <label for="txtUsuario">Nombre de usuario</label>
                    <asp:TextBox ID="inputUsuario" CssClass="form-control" required="required" placeholder="Nombre de usuario" runat="server" />
                </div>
                <div class="form-group mb-3">
                    <label for="txtEmail">Correo electrónico</label>
                    <asp:TextBox ID="inputEmail" CssClass="form-control" required="required" placeholder="Email" runat="server" />
                </div>

                <div class="form-group mb-3">
                    <label for="txtNombre">Nombre</label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" Placeholder="Nombre" required="required" />
                </div>

                <div class="form-group mb-3">
                    <label for="txtApellido">Apellido</label>
                    <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" Placeholder="Apellido"  required="required" />
                </div>
               
                <div class="form-group mb-3">
                    <label for="txtContraseña">Contraseña</label>
                    <asp:TextBox ID="inputContraseña" CssClass="form-control" required="required" placeholder="Contraseña" runat="server" />
                </div>
                <div class="form-group mb-3">
                    <label for="txtConfirmarContraseña">Confirmar contraseña</label>
                    <asp:TextBox ID="inputConfirmarContraseña" CssClass="form-control" required="required" placeholder="Confirmar contraseña" runat="server" />
                </div>

                <div class="form-group mb-3">
                    <label for="txtTelefono">Telefono</label>
                    <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" Placeholder="Telefono"  required="required" />
                </div>

                <div class="text-center">
                    <asp:Button  ID="btnRegistrarse" CssClass="btn btn-primary w-100" Text="Registrarse" OnClick="Registrarse" runat="server" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>
