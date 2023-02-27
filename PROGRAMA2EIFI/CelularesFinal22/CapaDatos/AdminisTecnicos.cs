﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Entidades; 

namespace CapaDatos
{
    public class AdminisTecnicos : DatosConexion
    {
        public int abmTecnicos(string accion, Tecnicos objTecnicos)
        { 

            
            int resultado = -1;
            string orden = string.Empty;
            if (accion == "Alta")
                orden = $"insert into Tecnicos values ({objTecnicos.P_Dni},'{objTecnicos.P_Nombre}','{objTecnicos.p_Apellido}' );";
             if (accion == "Modificar")
                  orden =  $"Update Tecnicos set Nombre = '{objTecnicos.P_Nombre}', Apellido = '{objTecnicos.p_Apellido}' Where Dni_Tecnico = {objTecnicos.P_Dni};  ";
              if (accion == "Baja")

                  orden = $"delete from Tecnicos where Dni_Tecnico = {objTecnicos.P_Dni}; ";
            
            SqlCommand cmd = new SqlCommand(orden, conexion);
            try
            {
                Abrirconexion();
                resultado = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception($"Errror al tratar de guardar,borrar o modificar ", e);
            }
            finally
            {
                Cerrarconexion();
                cmd.Dispose();
            }
            return resultado;

        }

        public DataSet listadoTecnicos(string cual)
        {
            string orden = string.Empty;
            if (cual != "Todos")
                orden = "select * from Tecnicos where Dni_Tecnico = " + int.Parse(cual) + ";";
            else
                orden = "select * from Tecnicos;";
            SqlCommand cmd = new SqlCommand(orden, conexion);
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                Abrirconexion();
                cmd.ExecuteNonQuery();
                da.SelectCommand = cmd;
                da.Fill(ds);
            }
            catch (Exception e)
            {
                throw new Exception("Error al listar Tecnicos", e);
            }
            finally
            {
                Cerrarconexion();
                cmd.Dispose();
            }
            return ds;
        }


        //public List<Tecnicos> ObtenerTecnicos()
        //{
        //    List<Tecnicos> listaTecnicosCB = new List<Tecnicos>();
        //    string orden = "Select Dni_Tecnico From Tecnicos";
        //    SqlCommand cmd = new SqlCommand(orden, conexion);
        //    SqlDataReader dr;
        //    try
        //    {
        //        Abrirconexion();
        //        dr = cmd.ExecuteReader();
        //        while (dr.Read())
        //        {

        //            string Dni = dr.GetString(1);

        //            Tecnicos T = new Tecnicos();// instancio R como un objeto Rubro
        //            T.P_Dni = dr.GetInt32(0); //se puede acceder a los campos con el tipo de dato, en este caso un entero

        //            listaTecnicosCB.Add(T);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("Error al listar Rubros", e);
        //    }
        //    finally
        //    {
        //        Cerrarconexion();
        //        cmd.Dispose();
        //    }
        //    return listaTecnicosCB;
        //}






    }
}
