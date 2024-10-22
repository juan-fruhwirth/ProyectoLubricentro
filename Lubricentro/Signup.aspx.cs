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

        protected void Registrarse(object sender, EventArgs e)
        {
            // Limpiar errores anteriores
            lblErrorCorreo.Text = "";
            lblErrorNombre.Text = "";
            lblErrorApellido.Text = "";
            lblErrorTelefono.Text = "";
            lblErrorContraseña.Text = "";
            lblErrorConfirmarContraseña.Text = "";

            inputCorreo.Style["border"] = "";
            inputNombre.Style["border"] = "";
            inputApellido.Style["border"] = "";
            inputTelefono.Style["border"] = "";
            inputContraseña.Style["border"] = "";
            inputConfirmarContraseña.Style["border"] = "";

            // Expresiones regulares para validaciones
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$"; // Valida formato de correo electrónico
            string namePattern = @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"; // Solo letras (con tildes y ñ)
            string phonePattern = @"^[0-9]+$"; // Solo números
            string passwordPattern = @"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[\W_]).{6,}$"; // Letras, números y un caracter especial, mínimo 6 caracteres

            bool hayErrores = false;

            // Validar el correo electrónico
            if (!System.Text.RegularExpressions.Regex.IsMatch(inputCorreo.Text, emailPattern))
            {
                lblErrorCorreo.Text = "Por favor, ingresa un correo electrónico válido.";
                inputCorreo.Style["border"] = "2px solid red";
                hayErrores = true;
            }

            // Validar el nombre
            if (!System.Text.RegularExpressions.Regex.IsMatch(inputNombre.Text, namePattern))
            {
                lblErrorNombre.Text = "El nombre solo puede contener letras y espacios.";
                inputNombre.Style["border"] = "2px solid red";
                hayErrores = true;
            }

            // Validar el apellido
            if (!System.Text.RegularExpressions.Regex.IsMatch(inputApellido.Text, namePattern))
            {
                lblErrorApellido.Text = "El apellido solo puede contener letras y espacios.";
                inputApellido.Style["border"] = "2px solid red";
                hayErrores = true;
            }

            // Validar el teléfono
            if (!System.Text.RegularExpressions.Regex.IsMatch(inputTelefono.Text, phonePattern))
            {
                lblErrorTelefono.Text = "El teléfono solo puede contener números.";
                inputTelefono.Style["border"] = "2px solid red";
                hayErrores = true;
            }

            // Validar la contraseña
            if (!System.Text.RegularExpressions.Regex.IsMatch(inputContraseña.Text, passwordPattern))
            {
                lblErrorContraseña.Text = "La contraseña debe tener al menos 6 caracteres, incluir letras, números y un caracter especial.";
                inputContraseña.Style["border"] = "2px solid red";
                hayErrores = true;
            }

            // Validar que las contraseñas coincidan
            if (inputContraseña.Text != inputConfirmarContraseña.Text)
            {
                lblErrorConfirmarContraseña.Text = "Las contraseñas no coinciden.";
                inputConfirmarContraseña.Style["border"] = "2px solid red";
                hayErrores = true;
            }

            // Si hay errores, detener la ejecución
            if (hayErrores)
            {
                return;
            }

            // Si todas las validaciones son correctas, se crea el usuario
            Usuario usuario = new Usuario (inputCorreo.Text, inputTelefono.Text, inputNombre.Text, inputApellido.Text, inputContraseña.Text);
            if (Usuario.Alta(usuario))
            {
                Response.Redirect("ConfirmacionEmail.aspx");
            }
            else
            {
                Response.Redirect("SignUp.aspx");
            }
        }

    }
}