using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EvaluacionServicios.Models.DAL
{
    public class EncuestaDAL
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["Cnx"].ToString();
        public IEnumerable<Encuesta> CreateEncuesta(string idForm)
        {            
            List<Encuesta> lstEncuesta = new List<Encuesta>();            
                        
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("usp_Encuesta_Select", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id_Formulario", Convert.ToInt32(idForm));
                    cmd.Parameters.AddWithValue("@Activo", "A");
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Encuesta encuesta = new Encuesta();
                        encuesta.IdFormulario = Convert.ToInt32(rdr["Id_Formulario"]);
                        //encuesta.IdSistema = Convert.ToInt32(rdr["Id_Sistema"]);
                        encuesta.NombreSistema = rdr["Nombre_Sistema"].ToString();
                        encuesta.Descripcion = (rdr["Descripcion"].ToString());                        
                        encuesta.Color = (rdr["Color"].ToString());

                        encuesta.IdPregunta = Convert.ToInt32(rdr["Id_Pregunta"]);
                        encuesta.IdTipoPregunta = Convert.ToInt32(rdr["Id_Tipo_Pregunta"]);
                        encuesta.DescPregunta = rdr["Desc_Pregunta"].ToString();

                        lstEncuesta.Add(encuesta);
                    }
                    con.Close();
                }           
            return lstEncuesta;
        }

        public IEnumerable<Encuesta> ObtenerEncuestas()
        {
            List<Encuesta> lstEncuestas = new List<Encuesta>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_Encuesta_Select", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_Formulario", Convert.ToInt32(-1));
                cmd.Parameters.AddWithValue("@Activo", "A");
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Encuesta encuesta = new Encuesta();
                    encuesta.IdFormulario = Convert.ToInt32(rdr["Id_Formulario"]); 
                    encuesta.NombreSistema = rdr["Nombre_Sistema"].ToString();                    
                    encuesta.IdTipoPregunta = Convert.ToInt32(rdr["Id_Tipo_Pregunta"]);
                    encuesta.DescPregunta = rdr["Desc_Pregunta"].ToString();

                    lstEncuestas.Add(encuesta);
                }
                con.Close();
            }
            return lstEncuestas;
        }
        //public int IgresarFormulario(Formulario formulario)
        //{
        //    int resultado = 0;
        //    using (SqlConnection con = new SqlConnection(connectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand("usp_Formulario_Insert", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@Id_Sistema", formulario.IdSistema);
        //        cmd.Parameters.AddWithValue("@Descripcion", formulario.Descripcion);
        //        cmd.Parameters.AddWithValue("@Activo", formulario.Activo);
        //        cmd.Parameters.AddWithValue("@Cedula_Usuario", formulario.CedulaUsuario);
        //        cmd.Parameters.AddWithValue("@Color", formulario.Color);

        //        con.Open();
        //        SqlDataReader rdr = cmd.ExecuteReader();

        //        while (rdr.Read())
        //        {
        //            resultado = Convert.ToInt32(rdr[0]);
        //        }
        //        con.Close();
        //    }
        //    return resultado;
        //}

       
    }
}