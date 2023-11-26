using ParcialApp.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcialApp.Acceso_a_datos.Interfaz
{
    internal interface IOrdenesDao
    {
        List<Material> obtenerMateriales();

        bool confirmarOrden(string responsable,List<DetalleOrden> detalles);
    }
}
