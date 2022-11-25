using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using EvaluacionServicios.Models.Common;

namespace EvaluacionServicios.Models
{

    public class SeguridadDAL
    {
        string connectionString = ConfigurationManager.ConnectionStrings["cnxSeguridad"].ToString();

        public Usuario ObtenerPermisosUsuario(Usuario oUsuario)
        {

            Usuario objUsuario = new Usuario();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("[SP_CONSULTA_Sistemas_POR_USUARIO]", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@USUARIO", oUsuario.CodUsuario);
                cmd.Parameters.AddWithValue("@MODULO", "EVALUACIONSERVICIOS");
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {

                    objUsuario.Nombre = rdr["NOM_USUARIO"].ToString();

                    objUsuario.CodUsuario = rdr["COD_USUARIO"].ToString();

                    //   objUsuario.Correo= rdr["modulo"].ToString();

                }


                con.Close();
            }
            return objUsuario;
        }


        public Usuario ObtenerDetallePermisoUsuario(string CodUsuario)
        {
            bool bTienePermiso = false;


            Usuario objUsuario = new Usuario();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_CONSULTA_PERMISOS_USUARIO_SISTEMA", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@USUARIO", CodUsuario);
                cmd.Parameters.AddWithValue("@MODULO", "EVALUACIONSERVICIOS");
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();


                if (rdr.Read())
                {

                    bTienePermiso = true;
                    objUsuario.Cedula = Convert.ToInt32(rdr["COD_CEDULA"].ToString());
                    objUsuario.CodUsuario = rdr["cod_usuario"].ToString();
                    objUsuario.Nombre = rdr["NOM_USUARIO"].ToString();
                    objUsuario.Rol_id = Convert.ToInt32(rdr["id"].ToString());

                    //Rol oRol = new Rol
                    //{
                    //    Nombre = rdr["COD_ROL"].ToString(),
                    //    id = Convert.ToInt32(rdr["id"].ToString())
                    //};
                }

                con.Close();
            }
            return objUsuario;
        }
    }
}