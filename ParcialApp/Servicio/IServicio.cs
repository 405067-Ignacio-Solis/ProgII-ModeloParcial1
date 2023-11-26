using ParcialApp.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcialApp.Servicio
{
    public interface IServicio
    {
        List<Material> traerMateriales();
        bool confirmarOrden(string responsable, List<DetalleOrden> detalles);
    }
}
