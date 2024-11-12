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
                if (Session["Usuario"]!= null)
                {
                    Usuario usuarioActual = (Usuario)Session["Usuario"];
                    int nivel_actual = usuarioActual.nivel;


                    if (usuarioActual.confirmado == false)
                    {
                        Response.Redirect("ConfirmacionEmail.aspx");
                    }
                    else Response.Redirect("Default.aspx");
                }
               
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["inputContraseña"] = inputContraseña.Text;
            ViewState["inputConfirmarContraseña"] = inputConfirmarContraseña.Text;
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

            Usuario usuario = new Usuario(inputCorreo.Text, inputTelefono.Text, inputNombre.Text, inputApellido.Text, inputContraseña.Text);
            try
            {

            
                // Si todas las validaciones son correctas, se crea el usuario
                
                if (Usuario.Alta(usuario))
                {
                    usuario.id_usuario = Usuario.TraerID(usuario);
                    Session["Usuario"] = usuario;
                    usuarioActual = (Usuario)Session["Usuario"];

                    int codigo_actual = (Registro.SendConfirmationEmail(usuarioActual.correo));
                    if (codigo_actual!= -1)
                    {
                        if (Registro.GuardarCodigoEnBaseDeDatos(usuarioActual.id_usuario, codigo_actual))
                        {
                            Session["Usuario"] = usuarioActual;
                            resultadoRegistro.Text = "Usuario ingresado correctamente, ahora debe confirmarlo con el codigo que se envio a su mail";
                            ClientScript.RegisterStartupScript(this.GetType(), "Redirect", "setTimeout(function(){ window.location.href = 'ConfirmacionEmail.aspx'; }, 1000);", true);
                            //Response.AddHeader("Refresh", "1;url=ConfirmacionEmail.aspx");
                            Response.Redirect("ConfirmacionEmail.aspx", false);
                            return;
                        }
                        else
                        {
                            Usuario.Baja(usuario);
                            resultadoRegistro.Text = "Error, no se cargo el usuario correctamente";
                            fn_logout();
                            return;
                        }
                    }
                    else
                    {
                        Usuario.Baja(usuario);
                        resultadoRegistro.Text = "Error, no se cargo el usuario correctamente";
                        fn_logout();
                        return;
                    }

                }

                else
                {
                    Usuario.Baja(usuario);
                    resultadoRegistro.Text = "Error, no se cargo el usuario correctamente";
                    fn_logout();
                    return;
                }

            }
            catch(Exception error)
            {
                Console.WriteLine(error);
                Usuario.Baja(usuario);
                resultadoRegistro.Text = "Error, no se cargo el usuario correctamente";
                fn_logout();
                return;
            }

        }

        protected void ToggleContraseñaClick(object sender, EventArgs e)
        {
            string textoActual = inputContraseña.Text;
            //inputContraseña.Text = ViewState["inputContraseña"]?.ToString();

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
            inputContraseña.Text = textoActual;
 
        }

        protected void ToggleConfirmarContraseñaClick(object sender, EventArgs e)
        {
            string textoActual = inputConfirmarContraseña.Text;
            //inputConfirmarContraseña.Text = ViewState["inputConfirmarContraseña"]?.ToString();

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
            inputConfirmarContraseña.Text = textoActual;
        }

        public void fn_logout()
        {
            Session.Clear();
            Session.Abandon();
            

        }

    }
}