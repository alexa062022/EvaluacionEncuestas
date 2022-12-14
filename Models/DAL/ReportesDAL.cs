using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EvaluacionServicios.Models.DAL
{
    public class ReportesDAL
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["Cnx"].ToString();
        public IEnumerable<Reportes> ObtenerFormulariosConResp() //obtener lista de formularios con respuestas asociadas
        {
            List<Reportes> lstReportes = new List<Reportes>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_Form_Con_Respuestas_Select", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Reportes ReportesResult = new Reportes();
                    ReportesResult.IdFormulario = Convert.ToInt32(rdr["Id_Formulario"]);
                    ReportesResult.NombreSistema = rdr["Nombre_Sistema"].ToString();
                    ReportesResult.FechaFormulario = Convert.ToDateTime(rdr["Fecha_Ingreso"]);
                    lstReportes.Add(ReportesResult);
                }
                con.Close();
            }
            return lstReportes;
        }

        public IEnumerable<Reportes> CreateReporte(string idForm, string tipo, string inicio, string fin)
        {
            int filtroFechas = 0;
            DateTime fechaInicio = DateTime.Now;
            DateTime fechaFin = DateTime.Now;            
            
            //se asignan las fechas en caso que sea por rango
            if (tipo == "rango")
            {
                filtroFechas = 1;
                fechaInicio = Convert.ToDateTime(inicio);
                fechaFin = Convert.ToDateTime(fin);                
            }          

            List<Reportes> lstReportes = new List<Reportes>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_Respuestas_Select_Estadisticas", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_Formulario", Convert.ToInt32(idForm));
                cmd.Parameters.AddWithValue("@SeleccionFechas", filtroFechas);
                cmd.Parameters.AddWithValue("@FechaDesde", fechaInicio);
                cmd.Parameters.AddWithValue("@FechaHasta", fechaFin);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Reportes reporte = new Reportes();                    
                    reporte.IdPregunta = Convert.ToInt32(rdr["Id_Pregunta"]);
                    reporte.IdTipoPregunta = Convert.ToInt32(rdr["Id_Tipo_Pregunta"]);
                    reporte.DescPregunta = rdr["Desc_Pregunta"].ToString();
                    reporte.opcion1 = Convert.ToInt32(rdr["Valor_1"]);
                    reporte.opcion2 = Convert.ToInt32(rdr["Valor_2"]);
                    reporte.opcion3 = Convert.ToInt32(rdr["Valor_3"]);
                    reporte.opcion4 = Convert.ToInt32(rdr["Valor_4"]);
                    reporte.opcion5 = Convert.ToInt32(rdr["Valor_5"]);
                    reporte.valorRespuesta = rdr["Valor_Respuesta"].ToString();                    
                    reporte.Justificacion = rdr["Justificacion_Respuesta"].ToString();
                    lstReportes.Add(reporte);
                }
                con.Close();
            }
            return lstReportes;
        }
    }
}