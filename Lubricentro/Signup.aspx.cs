using biz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lubricentro
{
    public partial class Signup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public bool Registrarse(object sender, EventArgs e)
        {

            try
            {
                // Validar que todos los campos tengan valores antes de crear el usuario
                if (string.IsNullOrWhiteSpace(inputUsuario.Text) ||
                    string.IsNullOrWhiteSpace(inputEmail.Text) ||
                    string.IsNullOrWhiteSpace(inputContraseña.Text) ||
                    string.IsNullOrWhiteSpace(inputConfirmarContraseña.Text) ||
                    string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    string.IsNullOrWhiteSpace(txtApellido.Text) ||
                    string.IsNullOrWhiteSpace(txtTelefono.Text))
                {
                    // Muestra un error indicando que faltan campos obligatorios
                    throw new Exception("Todos los campos deben ser completados.");
                }

                // Verificar si las contraseñas coinciden
                if (inputContraseña.Text != inputConfirmarContraseña.Text)
                {
                    // ERROR: las contraseñas no coinciden
                    throw new Exception("Las contraseñas no coinciden.");
                }

                // Si todo está bien, crear el objeto Usuario
                Usuario usuario = new Usuario(inputUsuario.Text, inputEmail.Text, inputContraseña.Text, txtNombre.Text, txtApellido.Text, txtTelefono.Text);

                return biz.Registro.Confirmar_registro(usuario);
            
            }
            catch (Exception ex)
            {
                // Muestra el mensaje de error al usuario (puedes mostrarlo en un label, por ejemplo)
                Console.WriteLine(ex.Message); // o puedes mostrar un mensaje de error en la interfaz
                return false;
            }


   
            
            {
          
             //Response.Redirect("ConfirmacionEmail.aspx");
            }
           
        }
    }
}