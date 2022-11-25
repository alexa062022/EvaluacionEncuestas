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
    }
}