using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EvaluacionServicios.Models.DAL
{
    public class PreguntasDAL
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["Cnx"].ToString();
        public IEnumerable<Preguntas> ObtenerPreguntas(string activo)
        {
            List<Preguntas> lstPreguntas = new List<Preguntas>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_Preguntas_Select", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_Pregunta", -1);
                cmd.Parameters.AddWithValue("@estado", activo);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Preguntas RespuestasResult = new Preguntas();
                    RespuestasResult.IdPregunta = Convert.ToInt32(rdr["Id_Pregunta"]);
                    RespuestasResult.DescPregunta = rdr["Desc_Pregunta"].ToString();
                    RespuestasResult.TipoPregunta = rdr["Desc_Tipo_Pregunta"].ToString();
                    RespuestasResult.Activo = Convert.ToInt32(rdr["Activo"]);
                    if (RespuestasResult.Activo == 0)
                    {
                        RespuestasResult.ActivoMostrar = "Desactivo";
                    }
                    else { RespuestasResult.ActivoMostrar = "Activo"; }

                    lstPreguntas.Add(RespuestasResult);
                }
                con.Close();
            }
            return lstPreguntas;
        }
        public int IgresarPregunta(string pregunta, int idTipo)
        {
            int resultado = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_Preguntas_Insert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Desc_Pregunta", pregunta);
                cmd.Parameters.AddWithValue("@Id_Tipo_Pregunta", idTipo);
                 cmd.Parameters.AddWithValue("@Activo", 1); // Nuevas preguntas por default van a ser activas
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

        public int EditarPregunta(int idPregunta, string pregunta, int estado)
        {
            int resultado = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_Preguntas_Update", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_Pregunta", idPregunta);
                cmd.Parameters.AddWithValue("@Desc_Pregunta", pregunta);                
                cmd.Parameters.AddWithValue("@Activo", estado); 
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