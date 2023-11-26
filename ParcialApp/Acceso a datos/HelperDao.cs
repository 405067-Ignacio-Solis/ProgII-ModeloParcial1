using ParcialApp.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcialApp.Acceso_a_datos
{
    internal class HelperDao
    {
        private SqlConnection conexion;
        private string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog = db_ordenes; Integrated Security = True";
        private static HelperDao instancia;

        private HelperDao (){
            conexion = new SqlConnection(connectionString);

        }

        public static HelperDao obtenerInstancia() {
            if (instancia == null) {
                instancia = new HelperDao();
            }
            return instancia;


        }
        public SqlConnection ObtenerConexion()
        {
            return this.conexion;
        }


        internal DataTable consultar(string nombreSP)
        {
            conexion.Open();
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = nombreSP;
            DataTable tabla = new DataTable();
            tabla.Load(comando.ExecuteReader());
            conexion.Close();
            return tabla;
        }

        internal DataTable consultar(string nombreSP, List<Parametro> lParams)
        {
            conexion.Open();
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = nombreSP;
            foreach (Parametro p in lParams)
            {
                comando.Parameters.AddWithValue(p.nombre, p.valor);
            }
            DataTable tabla = new DataTable();
            tabla.Load(comando.ExecuteReader());
            conexion.Close();
            return tabla;
        }

        internal bool confirmarOrden(string responsable, List<DetalleOrden> detalles) {
            bool success = true;
            SqlTransaction transaction = null;

            try
            {
                conexion.Open();
                transaction = conexion.BeginTransaction();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexion;
                cmd.Transaction = transaction;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_INSERTAR_ORDEN";
                cmd.Parameters.AddWithValue("@responsable", responsable);
                


                SqlParameter outputParam = new SqlParameter();
                outputParam.ParameterName = "@nro";
                outputParam.SqlDbType = SqlDbType.Int;
                outputParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outputParam);
                cmd.ExecuteNonQuery();

                int orden = (int)outputParam.Value;
                
                int detalleNro = 1;

                SqlCommand cmdDetalle;
                foreach (DetalleOrden detalle in detalles)
                {
                    
                    cmdDetalle = new SqlCommand("SP_INSERTAR_DETALLES", conexion, transaction);
                    cmdDetalle.CommandType = CommandType.StoredProcedure;
                    cmdDetalle.Parameters.AddWithValue("@nro_orden", orden);
                    cmdDetalle.Parameters.AddWithValue("@detalle", detalleNro);
                    cmdDetalle.Parameters.AddWithValue("@codigo", detalle.material.codigo);
                    cmdDetalle.Parameters.AddWithValue("@cantidad", detalle.cantidad);

                    
                    cmdDetalle.ExecuteNonQuery();
                    detalleNro++;

                }
                transaction.Commit();

            }
            catch
            {
                transaction.Rollback();

                success = false;

            }
            finally
            {

                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }

            }



            return success;


        }
    }
    }


