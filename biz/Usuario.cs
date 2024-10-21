using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biz
{
    public class Usuario
    {
        public Usuario(string nombre_usuario, string email, string contrasenia_ingresada, string nombre, string apellido, string telefono)
        {
            this.nombre_usuario = nombre_usuario;
            this.nivel = 1;
            this.email = email;
            this.contrasenia = new Contrasenia(contrasenia_ingresada);
            this.nombre = nombre;
            this.apellido = apellido;
            this.telefono = telefono;



        }

        public string nombre_usuario { get; set; }
        public int nivel { get; set; }
        public string email { get; set; }
        public Contrasenia contrasenia { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string telefono {get; set;}
        
    }

    
}
