using ParcialApp.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ParcialApp.Servicio
{
    public abstract class ServicioFactory
    {
        public abstract IServicio crearServicio();

    }
}
