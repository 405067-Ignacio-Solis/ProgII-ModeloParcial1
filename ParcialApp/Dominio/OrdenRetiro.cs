using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcialApp.Dominio
{
    public class OrdenRetiro
    {
        public int nroOrden { get; set; }
        public DateTime fecha { get; set; }
        public string responsable { get; set; }

        public List<DetalleOrden> detalles { get; set; }

        public OrdenRetiro(string responsable, List<DetalleOrden> detalles) {
            this.nroOrden = 1;
            this.fecha = DateTime.Now;
            this.responsable = responsable;
            this.detalles = detalles;
        }



    }
}
