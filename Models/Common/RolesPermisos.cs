using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EvaluacionServicios.Models.Common
{
    public enum RolesPermisos
    {
        #region OpcionesLitigioso
        //Pantalla Expediente
        Expediente_Puede_Guardar_Encabezado = 68,//GUARDAR EXPEDIENTE TOTAL
        Expediente_Puede_Guardar_Detalle = 69,//GUARDAR DETALLE EXPEDIENTE
        Expediente_Puede_Agregar_Procurador = 70,//AGREGAR PROCURADOR
        Expediente_Puede_DesAsignar_Procurador = 71,//DESASIGNAR PROCURADOR
        Expediente_Puede_Visualizar = 116,
        Expediente_Puede_Agregar_Instit = 118,
        Expediente_Puede_Eliminar_Instit = 119,

        //Pantalla Recepcion Documentos
        RecepcionDoc_Puede_Guardar = 115,//GUARDAR 
        RecepcionDoc_Puede_Visualizar = 117,

        //Pantalla Catalogos
        Catalogos_Puede_Visualizar = 120,

        //Pantalla Observaciones por Expediente
        Observaciones_Por_Expediente_Puede_Visualizar = 121,

        //Pantalla Acumulacion Expedientes
        Acumulacion_Expediente_Puede_Visualizar = 122,

        //Pantalla Desacumulacion Expedientes
        Desacumulacion_Expediente_Puede_Visualizar = 123,

        //Pantalla Desacumulacion Expedientes
        Asignacion_Expediente_Puede_Visualizar = 124,

        //Pantalla Actualizacion Expedientes
        Actualizacion_Expediente_Puede_Visualizar = 125
        #endregion
    }
}