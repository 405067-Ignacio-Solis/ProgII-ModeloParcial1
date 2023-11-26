using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcialApp.Acceso_a_datos
{
    internal class Parametro
    {
        public string nombre { get; set; }
        public object valor { get; set; }

        public Parametro(string nombre, object valor) {
            this.nombre = nombre;
            this.valor = valor;
        }
    }
}
