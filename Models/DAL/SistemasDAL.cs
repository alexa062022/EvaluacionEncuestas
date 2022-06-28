using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EvaluacionServicios.Models.DAL
{
    public class SistemasDAL
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["Cnx"].ToString();

        public IEnumerable<Sistemas> ObtenerSistemas()
        {
            List<Sistemas> lstSistemas = new List<Sistemas>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_Sistemas_Select", con);
                cmd.CommandType = CommandType.StoredProcedure;                
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Sistemas sistemasResult = new Sistemas();
                    sistemasResult.idSistema = rdr["Id_Sistema"].ToString();
                    sistemasResult.NombreSistema = rdr["Nombre_Sistema"].ToString();                                       
                    sistemasResult.InternoExterno = Convert.ToInt32(rdr["Interno_Externo"]);

                    if (sistemasResult.InternoExterno == 0)
                    {
                        sistemasResult.InternoExternoMostrar = "Externo";
                    }
                    else { sistemasResult.InternoExternoMostrar = "Interno"; }
                    sistemasResult.Activo = Convert.ToInt32(rdr["Activo"]);
                    if (sistemasResult.Activo == 0)
                    {
                        sistemasResult.ActivoMostrar = "Desactivo";
                    }
                    else { sistemasResult.ActivoMostrar = "Activo"; }

                    sistemasResult.FechaIngreso = rdr["Fecha_Ingreso"].ToString();
                    sistemasResult.CedulaUsuario = Convert.ToInt32(rdr["Cedula_Usuario"]);

                    lstSistemas.Add(sistemasResult);
                }
                con.Close();
            }
            return lstSistemas;
        }
        public int IgresarSistema(Sistemas sistemas)
        {
            int resultado=0;          
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_Sistemas_Insert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombre_Sistema", sistemas.NombreSistema);
                cmd.Parameters.AddWithValue("@Interno_Externo", sistemas.InternoExterno);
                cmd.Parameters.AddWithValue("@Cedula_Usuario", sistemas.CedulaUsuario);
                cmd.Parameters.AddWithValue("@Activo", sistemas.Activo);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    resultado = Convert.ToInt32(rdr[0]);
                }
                con.Close();
            }
            return resultado;
        }

        public int ModificarSistema(Sistemas sistemas)
        {
            int resultado = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_Sistemas_Update", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_Sistema", Convert.ToInt32(sistemas.idSistema));
                cmd.Parameters.AddWithValue("@Nombre_Sistema", sistemas.NombreSistema);
                cmd.Parameters.AddWithValue("@Interno_Externo", sistemas.InternoExterno);                
                cmd.Parameters.AddWithValue("@Activo", sistemas.Activo);
                cmd.Parameters.AddWithValue("@Cedula_Usuario", sistemas.CedulaUsuario);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    resultado = Convert.ToInt32(rdr[0]);
                }
                con.Close();
            }
            return resultado;
        }
    }
}