using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Entidades.Excepciones;

namespace Entidades.DBO
{
    internal static class GestorSQL<T>
    {
        //internal static T ObtenerElementoPorId(int id,string connection,string query)
        //{
        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(connection))
        //        {
        //            SqlCommand cmd = new SqlCommand(query,conn);
        //            re
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ConnectionInvalidException("Conexion fallida",ex);
        //    }
        //}
    }
}
