using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Entidades.Excepciones;
using System.Collections;
using Entidades.Class;

namespace Entidades.DBO
{
    public static class DBOAviones
    {
        private static string stringConnection;

        static DBOAviones()
        {
            stringConnection = "Server=.;Database=Aerolinea;Trusted_Connection=True;";
        }
        /// <summary>
        /// Este metodo se encargar a de eliminar el vuelo con el id del avion pasado por parametro
        /// </summary>
        /// <param name="avion"></param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        /// <exception cref="ElementoNoEncontradoException"></exception>
        private static void EliminarVuelo(Avion avion)
        {
            if (avion is null)
            {
                throw new IndexOutOfRangeException("No se aceptan ids negativos");
            }
            else
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(stringConnection))
                    {
                        string query = $"DELETE FROM Aviones WHERE id_avion = @id";

                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("id", avion.Id);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception)
                {
                    throw new ElementoNoEncontradoException("Elemento no encontrado");
                }

            }
        }
        public static void EliminarVuelos(List<Avion> aviones)
        {
            foreach (Avion avion in aviones)
            {
                if (aviones.Count == 0)
                {
                    throw new ArgumentNullException("Lista vacia");
                }
                else
                {
                    DBOAviones.EliminarVuelo(avion);
                }
            }
        }
        /// <summary>
        /// Esta funcion se encarga de actualizar el vuelo con el id del mismo, con los nuevos datos
        /// </summary>
        /// <param name="avion"></param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        /// <exception cref="ElementoNoEncontradoException"></exception>
        private static void ActualizarVuelo(Avion avion)
        {
            if (avion is null)
            {
                throw new IndexOutOfRangeException("No se aceptan ids negativos");
            }
            else
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(stringConnection))
                    {
                        string query = $@"
                                    UPDATE Aviones 
                                    SET 
                                    origen = @origen,
                                    destino=@destino,
                                    horas_de_vuelo=@horas_de_vuelo,
                                    costo=@costo,
                                    hora_de_salida=@hora_de_salida,
                                    disponible = @disponible 
                                    WHERE id_avion = @id";

                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("id", avion.Id);
                        cmd.Parameters.AddWithValue("origen", avion.Origen);
                        cmd.Parameters.AddWithValue("destino", avion.Destino);
                        cmd.Parameters.AddWithValue("horas_de_vuelo", avion.HorasDeVuelo);
                        cmd.Parameters.AddWithValue("costo", avion.Costo);
                        cmd.Parameters.AddWithValue("hora_de_salida", avion.HoraDeSalida.ToString("HH:mm:ss"));
                        cmd.Parameters.AddWithValue("disponible", avion.Disponible);
                        
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception)
                {
                    throw new ElementoNoEncontradoException("Elemento no encontrado");
                }

            }
        }
        public static void ActualizarVuelos(List<Avion> aviones)
        {
            if(aviones.Count == 0)
            {
                throw new ArgumentNullException("Lista vacia");
            }
            else
            {
                foreach (Avion avion in aviones)
                {
                    DBOAviones.ActualizarVuelo(avion);
                }
            }
        }
        /// <summary>
        /// Se encarga de buscar en la base de datos un avion con el mismo Id y retornarlo con todos los sus datos
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna el avion con la coincidencia o lanzara una excepcion en el caso de recibir negativos o no encontrar el elemento con el id pasado por parametro</returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        /// <exception cref="ElementoNoEncontradoException"></exception>
        public static Avion ObtenerVueloPorId(int id)
        {
            if(id < 0)
            {
                throw new IndexOutOfRangeException("No se aceptan ids negativos");
            }
            else
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(stringConnection))
                    {
                        string query = $"SELECT * FROM Aviones WHERE id_avion = @id";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("id", id);
                        
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        
                        reader.Read();
                        AvionModelo avion = new AvionModelo()
                        {
                            Id = reader.GetInt32(0),
                            Origen = reader.GetString(1),
                            Destino = reader.GetString(2),
                            HorasDeVuelo = reader.GetInt32(3),
                            Costo = reader.GetDouble(4),
                            HoraDeSalida = reader.GetDateTime(5),
                            Disponible = reader.GetBoolean(6)
                        };
                        return avion.GetElement();                   
                    }
                }
                catch (Exception)
                {
                    throw new ElementoNoEncontradoException("Elemento no encontrado");
                }
            }


        }
        /// <summary>
        /// Este metodo se encarga de devolver una lista filtrada de aviones segun la columna y el valor deseado.
        /// </summary>
        /// <param name="columna"></param>
        /// <param name="value"></param>
        /// <returns>Retornara una lista de aviones con las coincidencia que se pidio</returns>
        /// <exception cref="DataBaseErrorException"></exception>
        public static List<Avion> GetVuelosFiltrados(string columna,string value)
        {
            List<Avion> aviones = new List<Avion>();
            SqlConnection connection = new SqlConnection(stringConnection);
            List<string> columnasPermitidas = new List<string> {"id_avion","origen", "destino","horas_de_vuelo","costo","hora_de_salida","disponible" };
            
            if (columnasPermitidas.Contains(columna))
            {
                string sentencia = $"SELECT * FROM Aviones WHERE {columna} = @value ORDER BY hora_de_salida";               

                try
                {
                    SqlCommand command = new SqlCommand(sentencia, connection);
                    command.Parameters.AddWithValue("@value", value);


                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        AvionModelo avion = new AvionModelo()
                        {
                            Id = reader.GetInt32(0),
                            Origen = reader.GetString(1),
                            Destino = reader.GetString(2),
                            HorasDeVuelo = reader.GetInt32(3),
                            Costo = reader.GetDouble(4),
                            HoraDeSalida = reader.GetDateTime(5),
                            Disponible = reader.GetBoolean(6)                                                        
                        };                        
                        aviones.Add(avion.GetElement());
                    }

                    return aviones;
                }
                catch (Exception)
                {
                    throw new DataBaseErrorException("No se pudieron filtrar los datos");
                }
                finally
                {
                    if (connection != null && connection.State == System.Data.ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
            else
            {
                throw new DataBaseErrorException("Columna incorrecta");

            }
        }

        /// <summary>
        /// Se encarga de retormarme una lista con los aviones que se encuentran en la base de datos
        /// </summary>
        /// <returns>Retornara una lista de aviones o arrojara una excepcion si ocurre un error al realizar la conexion</returns>
        /// <exception cref="DataBaseErrorException"></exception>
        public static List<Avion> GetVuelos()
        {
            List<Avion> aviones = new List<Avion>();
            SqlConnection connection = new SqlConnection(stringConnection);

            string sentencia = $"SELECT * FROM Aviones";

            try
            {
                SqlCommand command = new SqlCommand(sentencia, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    AvionModelo avion = new AvionModelo()
                    {
                        Id = reader.GetInt32(0),
                        Origen = reader.GetString(1),
                        Destino = reader.GetString(2),
                        HorasDeVuelo = reader.GetInt32(3),
                        Costo = reader.GetDouble(4),
                        HoraDeSalida = reader.GetDateTime(5),
                        Disponible = reader.GetBoolean(6)
                    };
                    aviones.Add(avion.GetElement());
                }

                return aviones;
            }
            catch (Exception)
            {
                throw new DataBaseErrorException("Error al leer la base de datos");
            }
            finally
            { if (connection != null && connection.State == System.Data.ConnectionState.Open) { connection.Close(); } }
        }
        /// <summary>
        /// Agrega un avion parametrizado a la base de datos
        /// </summary>
        /// <param name="avion"></param>
        /// <exception cref="DataBaseErrorException">Lanzara una excepcion en el caso de no poder cargar el avion</exception>
        public static void AgregarUnAvion(Avion avion)
        {            
            try
            {
                using (SqlConnection conn = new SqlConnection(stringConnection))
                {

                    string query = $"INSERT INTO Aviones (origen,destino,horas_de_vuelo,costo,hora_de_salida,disponible) VALUES (@origen,@destino,@horas_de_vuelo,@costo,@hora_de_salida,@disponible)";
                    SqlCommand cmd = new SqlCommand(query, conn);           
                    cmd.Parameters.AddWithValue("origen", avion.Origen);
                    cmd.Parameters.AddWithValue("destino", avion.Destino);
                    cmd.Parameters.AddWithValue("horas_de_vuelo", avion.HorasDeVuelo);
                    cmd.Parameters.AddWithValue("costo", avion.Costo);
                    cmd.Parameters.AddWithValue("hora_de_salida", avion.HoraDeSalida);
                    cmd.Parameters.AddWithValue("disponible",avion.Disponible);

                    conn.Open();
                    cmd.ExecuteNonQuery();                    
                }
            }
            catch (Exception ex)
            {
                throw new DataBaseErrorException("Conexion fallida, no se pudo agregar el avion a la base de datos", ex);
            }
        }
        /// <summary>
        /// Se encarga de agregar una lista de aviones a la base de datos
        /// </summary>
        /// <param name="aviones"></param>
        /// <returns>Retornara true si funcina con exito o false en el caso de que la lista este vacia</returns>
        public static void AgregarAviones(List<Avion> aviones)
        {
            if (aviones.Count > 0)
            {
                foreach (Avion av in aviones)
                {
                    DBOAviones.AgregarUnAvion(av);
                }
            }
            else
            {
                throw new DataBaseErrorException("Lista vacia");
            }            
        }
    }
}
