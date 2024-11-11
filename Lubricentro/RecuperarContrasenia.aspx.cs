using biz;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Lubricentro
{
    public partial class RecuperarContraseña : System.Web.UI.Page
    {
        protected void btnResetPassword_Click(object sender, EventArgs e)
        {


            try
            {
                bool hayErrores = false;
                string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$"; // Valida formato de correo electrónico
                lblErrorCorreo.Text = "";  //limpia errores anteriores
                txtEmail.Style["border"] = "";

                if (!System.Text.RegularExpressions.Regex.IsMatch(txtEmail.Text, emailPattern))
                {

                    lblErrorCorreo.Text = "Por favor, ingresa un correo electrónico válido.";
                    txtEmail.Style["border"] = "2px solid red";
                    hayErrores = true;

                }

                if (hayErrores)
                {
                    return;
                }

                int codigo = Registro.SendConfirmationEmail(txtEmail.Text.ToString());
                if (codigo == -1)
                {
                    lblEnvioCorreo.Text = "ERROR, no se genero el codigo correctamente";
                    return;
                }
                string correo = txtEmail.Text;

                int id_usuario = Usuario.TraerIDPorCorreo(correo);
                if (id_usuario == -1)
                {
                    lblEnvioCorreo.Text = "ERROR, no existe ningun usuario con este correo";
                    return;
                }
                if (Registro.GuardarCodigoEnBaseDeDatos(id_usuario, codigo) == false)
                {
                    lblEnvioCorreo.Text = "ERROR, no se genero el codigo correctamente";
                }

                lblEnvioCorreo.Text = "Se envio el codigo a su correo correctamente";
                ViewState["id_usuario"] = id_usuario;
                ViewState["codigo"] = codigo;
                ViewState["correo"] = correo;

                // Mostrar el Panel de Código
                PanelEmail.Visible = false;
                PanelCode.Visible = true;
            }
            catch (Exception error)
            {
                Console.WriteLine(error);
                lblEnvioCorreo.Text = "Se envio el codigo a su correo correctamente";
                return;

            }

        }

        protected void btnVerifyCode_Click(object sender, EventArgs e)
        {
            // Validar el código ingresado (aquí deberías verificar si el código es correcto)
            int codigo = Convert.ToInt32(txtCode.Text);
            int id_usuario = Convert.ToInt32(ViewState["id_usuario"]);
            if (confirmarCodigo(codigo, id_usuario) == false)
            {
                lblConfirmarCodigo.Text = "Error, el codigo ingresado es incorrecto";
                return;
            }

            // Si el código es válido, mostrar el Panel de Contraseña
            lblConfirmarCodigo.Text = "Codigo correcto";
            PanelCode.Visible = false;
            PanelNewPassword.Visible = true;
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            try
            {

                if (ViewState["confirmado"] != null && (bool)ViewState["confirmado"] == true)
                {
                    // Ya confirmado, no hacer nada
                    return;
                }
                // Aquí va la lógica para cambiar la contraseña (validar que las contraseñas coincidan)

                // Si las contraseñas son válidas, cambiarla y mostrar un mensaje de éxito
                // Limpiar errores anteriores

                lblErrorContraseña.Text = "";
                lblErrorConfirmarContraseña.Text = "";
                txtNuevaContrasenia.Style["border"] = "";
                txtConfirmarContrasenia.Style["border"] = "";

                // Expresiones regulares para validaciones

                string passwordPattern = @"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[\W_]).{6,}$"; // Letras, números y un caracter especial, mínimo 6 caracteres

                bool hayErrores = false;

                // Validar la contraseña
                if (!System.Text.RegularExpressions.Regex.IsMatch(txtNuevaContrasenia.Text, passwordPattern))
                {
                    lblErrorContraseña.Text = "La contraseña debe tener al menos 6 caracteres, incluir letras, números y un caracter especial.";
                    txtNuevaContrasenia.Style["border"] = "2px solid red";
                    hayErrores = true;
                }

                // Validar que las contraseñas coincidan
                if (txtNuevaContrasenia.Text != txtConfirmarContrasenia.Text)
                {
                    lblErrorConfirmarContraseña.Text = "Las contraseñas no coinciden.";
                    txtConfirmarContrasenia.Style["border"] = "2px solid red";
                    hayErrores = true;
                }

                if (hayErrores)
                {
                    return;
                }

                string nueva_contrasenia = txtNuevaContrasenia.Text;
                string correo = (ViewState["correo"]).ToString();

                int id_usuario = Convert.ToInt32(ViewState["id_usuario"]);

                if (Contrasenia.cambiarContrasenia(correo, nueva_contrasenia, id_usuario) == false)
                {
                    lblResultado.Text = "Error, no se cambio la contraseña";
                    return;
                }

                lblResultado.Text = "Contraseña cambiada correctamente";
                ViewState["confirmado"] = true;
                Response.AddHeader("Refresh", "1;url=Login.aspx");


            }
            catch (Exception error)
            {
                Console.WriteLine(error);
                return;
            }
        }


        private bool confirmarCodigo(int codigo_ingresado, int id_usuario)
        {
            bool confirmacionExitosa = false;
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["JUAN-LAPTOP"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ISNULL(COUNT(*), 0) FROM Codigos WHERE Codigo = @Codigo AND UsuarioID = @UsuarioID AND Expiracion > GETDATE()";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Codigo", codigo_ingresado);
                command.Parameters.AddWithValue("@UsuarioID", id_usuario);

                try
                {
                    connection.Open();
                    int count = (int)command.ExecuteScalar();

                    if (count > 0)
                    {
                        // Marca el codigo como ya usado. Deberiamos agregar un atributo BIT Confirmado a la tabla Codigos.
                        //query = "UPDATE Codigos SET Usado = 1 WHERE Codigo = @Codigo";
                        //command.CommandText = query;
                        //command.ExecuteNonQuery();

                        confirmacionExitosa = true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al confirmar el email: " + ex.Message);
                }
            }
            return confirmacionExitosa;
        }
    }
}
