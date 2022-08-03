using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace EvaluacionServicios.Models.DAL
{
    public class TipoPreguntaDAL
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["Cnx"].ToString();
        public IEnumerable<TipoPregunta> ObtenerTipoPregunta(string estado)
        {
            List<TipoPregunta> lstTipoPreg = new List<TipoPregunta>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_TipoPregunta_Select", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@estado", estado); // A= activo, I= inactivo, T= todos
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    TipoPregunta Result = new TipoPregunta();
                    Result.IdTipoPregunta = Convert.ToInt32(rdr["Id_Tipo_Pregunta"]);
                    Result.DescTipoPregunta = rdr["Desc_Tipo_Pregunta"].ToString();                   
                    Result.Activo = Convert.ToInt32(rdr["Activo"]);
                    if (Result.Activo == 0)
                    {
                        Result.ActivoMostrar = "Desactivo";
                    }
                    else { Result.ActivoMostrar = "Activo"; }
                    Result.FechaIngreso = Convert.ToDateTime(rdr["Fecha_Ingreso"]);
                    Result.CedulaUsuario = Convert.ToInt32(rdr["Cedula_Usuario"]);

                    lstTipoPreg.Add(Result);
                }
                con.Close();
            }
            return lstTipoPreg;
        }
    }
}