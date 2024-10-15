using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biz
{
    public class Usuario
    {
        public Usuario(string usuario, string email, string contraseña)
        {
            this.usuario = usuario;
            this.email = email;
            this.contraseña = contraseña;
            this.nivel = 1;
        }

        private string usuario { get; set; }
        private string email { get; set; }
        private string contraseña { get; set; }
        private string nombre { get; set; }
        private string apellido {  get; set; }
        private int nivel {  get; set; }
    }

    
}
