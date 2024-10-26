using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace biz
{
    internal class TipoDeCombustible
    {
        public TipoDeCombustible(int id_tipoDeCombustible, string nombre)
        {
            this.id_tipoDeCombustible = id_tipoDeCombustible;
            this.nombre = nombre;
        }
        public int id_tipoDeCombustible { get; set; }
        public string nombre { get; set; }
    }
}
