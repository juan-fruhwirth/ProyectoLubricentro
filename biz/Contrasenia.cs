using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biz
{
    public class Contrasenia
    {

        public Contrasenia(string contrasenia_ingresada)

        {
            (this.hash, this.salt) = HasherContrasenia.Hashear_contrasenia(contrasenia_ingresada);
        }

        public int id_contrasenia { get; set; }
        public string hash { get; set; }
        public string salt { get; set; }
        public int nombre_usuario { get; set; }
    }


}
