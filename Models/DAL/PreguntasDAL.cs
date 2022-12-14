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
                    RespuestasResult.Justificar = Convert.ToInt32(rdr["Justificacion"]);
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
        public int IgresarPregunta(string pregunta, int idTipo, int justifica)
        {
            int resultado = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_Preguntas_Insert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Desc_Pregunta", pregunta);
                cmd.Parameters.AddWithValue("@Id_Tipo_Pregunta", idTipo);
                cmd.Parameters.AddWithValue("@Activo", 1); // Nuevas preguntas por default van a ser activas
                cmd.Parameters.AddWithValue("@Justificacion", justifica);
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

        public int BuscarPregunta(int idPregunta) //verifica si la pregunta tiene una respuesta asociada
        {
            int existe = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_Respuestas_VerificaUso", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_Pregunta", idPregunta);                
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    existe = Convert.ToInt32(rdr[0]);
                     
                }
                con.Close();
            }
            return existe;
        }

        public int EditarPregunta(int idPregunta, string pregunta, int estado, int justifica2)
        {
            int resultado = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_Preguntas_Update", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_Pregunta", idPregunta);
                cmd.Parameters.AddWithValue("@Desc_Pregunta", pregunta);                
                cmd.Parameters.AddWithValue("@Activo", estado);
                cmd.Parameters.AddWithValue("@Justificacion", justifica2);
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