using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcialApp.Servicio
{
    public class ServicioFactoryImp : ServicioFactory
    {
        public override IServicio crearServicio()
        {
            return new Servicio.Implementacion.Servicio();
        }
    }
}
