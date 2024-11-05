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
        Usuario usuarioActual;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                /*if (Session["Usuario"]!= null)
                {
                    Response.Redirect("Default.aspx");
                }
                */
            }
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

            try
            {

            
                // Si todas las validaciones son correctas, se crea el usuario
                Usuario usuario = new Usuario (inputCorreo.Text, inputTelefono.Text, inputNombre.Text, inputApellido.Text, inputContraseña.Text);
                if (Usuario.Alta(usuario))
                {
                    Session["Usuario"] = usuario;
                    usuarioActual = (Usuario)Session["Usuario"];

                    string token_actual = (Registro.SendConfirmationEmail(usuarioActual.correo));
                    if (token_actual!= "")
                    {
                        int id_token= Registro.GuardarTokenEnBaseDeDatos(token_actual);
                        if (id_token != -1)
                        {
                            usuarioActual.token_id = id_token;
                            Session["Usuario"] = usuarioActual;
                            Response.Redirect("ConfirmacionEmail.aspx");
                        }
                        else Response.Redirect("Default.aspx");
                    }
                    else
                    {
                        Response.Redirect("Default.aspx");
                    }

                   Response.Redirect("ConfirmacionEmail.aspx");
                }

                else
                {
                    Response.Redirect("Default.aspx");
                }

            }
            catch(Exception error)
            {
                Response.Redirect("Default.aspx");
                throw error;
            }

        }

        protected void ToggleContraseñaClick(object sender, EventArgs e)
        {
            if (inputContraseña.TextMode == TextBoxMode.Password)
            {
                inputContraseña.TextMode = TextBoxMode.SingleLine; // Cambiar a texto visible
                btnToggleContraseña.Text = "Ocultar contraseña";
            }
            else
            {
                inputContraseña.TextMode = TextBoxMode.Password; // Cambiar a modo contraseña
                btnToggleContraseña.Text = "Ver contraseña";
            }
        }

        protected void ToggleConfirmarContraseñaClick(object sender, EventArgs e)
        {
            if (inputConfirmarContraseña.TextMode == TextBoxMode.Password)
            {
                inputConfirmarContraseña.TextMode = TextBoxMode.SingleLine;
                btnToggleConfirmarContraseña.Text = "Ocultar contraseña";
            }
            else
            {
                inputConfirmarContraseña.TextMode = TextBoxMode.Password;
                btnToggleConfirmarContraseña.Text = "Ver contraseña";
            }
        }

    }
}