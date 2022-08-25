using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EvaluacionServicios.Models.DAL
{
    public class FormularioDAL
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["Cnx"].ToString();
        public IEnumerable<Formulario> ObtenerFormularios(string estado)
        {
            List<Formulario> lstFormulario = new List<Formulario>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_Formularios_Select", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_Sistema", -1);
                cmd.Parameters.AddWithValue("@estado", estado); // A= activo, I= inactivo, T= todos
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Formulario FormularioResult = new Formulario();
                    FormularioResult.IdFormulario = Convert.ToInt32(rdr["Id_Formulario"]);
                    FormularioResult.IdSistema = Convert.ToInt32(rdr["Id_Sistema"]);
                    FormularioResult.NombreSistema = rdr["Nombre_Sistema"].ToString();
                    FormularioResult.Descripcion = (rdr["Descripcion"].ToString());
                    FormularioResult.Activo = Convert.ToInt32(rdr["Activo"]);
                    FormularioResult.FechaIngreso = Convert.ToDateTime(rdr["Fecha_Ingreso"]);
                    FormularioResult.CedulaUsuario = Convert.ToInt32(rdr["Cedula_Usuario"]);
                    if (FormularioResult.Activo == 0)
                    {
                        FormularioResult.ActivoMostrar = "Desactivo";
                    }
                    else { FormularioResult.ActivoMostrar = "Activo"; }
                                        

                    lstFormulario.Add(FormularioResult);
                }
                con.Close();
            }
            return lstFormulario;
        }

        public int IgresarFormulario(Formulario formulario)
        {
            int resultado = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_Formulario_Insert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_Sistema", formulario.IdSistema);
                cmd.Parameters.AddWithValue("@Descripcion", formulario.Descripcion);
                cmd.Parameters.AddWithValue("@Activo", formulario.Activo);
                cmd.Parameters.AddWithValue("@Cedula_Usuario", formulario.CedulaUsuario);
                cmd.Parameters.AddWithValue("@Color", formulario.Color);

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

        public int ModificarFormulario(Formulario formulario)
        {
            int resultado = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_Formularios_Update", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_Formulario", formulario.IdFormulario);
                cmd.Parameters.AddWithValue("@Id_Sistema", formulario.IdSistema);
                cmd.Parameters.AddWithValue("@Descripcion", formulario.Descripcion);
                cmd.Parameters.AddWithValue("@Activo", formulario.Activo);
                //cmd.Parameters.AddWithValue("@Cedula_Usuario", formulario.CedulaUsuario);

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