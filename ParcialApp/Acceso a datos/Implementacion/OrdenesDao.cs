using ParcialApp.Acceso_a_datos.Interfaz;
using ParcialApp.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace ParcialApp.Acceso_a_datos.Implementacion
{
    internal class OrdenesDao : IOrdenesDao
    {

        SqlConnection connexion = HelperDao.obtenerInstancia().ObtenerConexion();
        public List<Material> obtenerMateriales() {
            
            DataTable tabla = HelperDao.obtenerInstancia().consultar("SP_CONSULTAR_MATERIALES");
            List<Material> listMateriales = new List<Material>();
            foreach (DataRow row in tabla.Rows) {
                int codigo = Convert.ToInt32(row["codigo"]);
                string nombre = row["nombre"].ToString();
                int stock = Convert.ToInt32(row["stock"]);
                listMateriales.Add(new Material(codigo, nombre, stock));
            }
            return listMateriales;
        }

        public bool confirmarOrden(string responsable,List<DetalleOrden> detalles) {
            bool success =
            HelperDao.obtenerInstancia().confirmarOrden(responsable, detalles);
            
            return success;

        }
    }


}
