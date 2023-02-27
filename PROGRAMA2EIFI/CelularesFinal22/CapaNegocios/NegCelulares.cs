using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Entidades;
using CapaDatos;

namespace CapaNegocios
{
    public class NegCelulares
    {
        AdminisCelulares DatosObjCelulares = new AdminisCelulares();


        public int abmCelulares(string accion, Celulares objProfesional)
        {
            return DatosObjCelulares.abmCelulares(accion, objProfesional);
        }
        public DataSet listadoCelulares(string cual)
        {
            return DatosObjCelulares.listadoCelulares(cual);
        }

        public DataSet listadoCelularesCodigo(string cual)
        {
            return DatosObjCelulares.listadoCelularesCodigo(cual);
        }

        public DataSet listadoCelularesMarca(string cuall)
        {
            return DatosObjCelulares.listadoCelularesModelo(cuall);
        }

        public DataSet listadoCelularesModelo(string cual)
        {
            return DatosObjCelulares.listadoCelularesModelo(cual);
        }

    }
}
