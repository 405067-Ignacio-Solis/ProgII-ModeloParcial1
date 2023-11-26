using ParcialApp.Acceso_a_datos.Implementacion;
using ParcialApp.Acceso_a_datos.Interfaz;
using ParcialApp.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ParcialApp.Servicio.Implementacion
{
    public class Servicio : IServicio
    {
        IOrdenesDao ordenesDao;
        

        public Servicio() {
            ordenesDao= new OrdenesDao();
        }
        public List<Material> traerMateriales()
        {
            return ordenesDao.obtenerMateriales();
        }

        public bool confirmarOrden(string responsable,List<DetalleOrden> detalles)
       {
            return ordenesDao.confirmarOrden(responsable,detalles);
        }
    }
}
