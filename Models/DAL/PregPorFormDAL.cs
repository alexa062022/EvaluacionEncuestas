using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EvaluacionServicios.Models.DAL
{
    public class PregPorFormDAL
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["Cnx"].ToString();
        public IEnumerable<Formulario> ObtenerFormulariosActivos()
        {
            List<Formulario> lstFormulario = new List<Formulario>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_Form_Sin_Preg_Select", con);
                cmd.CommandType = CommandType.StoredProcedure;                
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Formulario FormularioResult = new Formulario();
                    FormularioResult.IdFormulario = Convert.ToInt32(rdr["Id_Formulario"]);                    
                    FormularioResult.NombreSistema = rdr["Nombre_Sistema"].ToString(); 
                    lstFormulario.Add(FormularioResult);
                }
                con.Close();
            }
            return lstFormulario;
        }
        public int IgresarPregPorForm(string preguntas, string idForm)
        {
            int resultado = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_Preguntas_X_Formularios_Insert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_Formulario", idForm);
                cmd.Parameters.AddWithValue("@Preguntas", preguntas);
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