using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcialApp.Dominio
{
    public class Material
    {
        public int codigo { get; set; }
        public string nombre { get; set; }
        public int stock { get; set; }

        public Material(int codigo, string nombre, int stock)
        {
            this.codigo = codigo;
            this.nombre = nombre;
            this.stock = stock;
        }
        public override string ToString() {
            return nombre;
        }
    }

}
