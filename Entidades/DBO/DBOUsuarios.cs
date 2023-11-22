using Entidades.Class;
using Entidades.Excepciones;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.DBO
{
    public static class DBOUsuarios
    {
        private static string stringConnection;

        static DBOUsuarios()
        {
            stringConnection = "Server=.;Database=Aerolinea;Trusted_Connection=True;";
        }


        /// <summary>
        /// Elimina un usuario de la base de datos.
        /// </summary>
        /// <param name="usuario">El usuario a ser eliminado.</param>
        /// <exception cref="IndexOutOfRangeException">Se lanza si el usuario es nulo.</exception>
        /// <exception cref="ElementoNoEncontradoException">Se lanza si ocurre un error durante la eliminación.</exception>
        public static void EliminarUsuario(Usuario usuario)
        {
            // Verificar si el usuario es nulo
            if (usuario is null)
            {
                // Lanzar una excepción si el usuario es nulo
                throw new IndexOutOfRangeException("No se aceptan usuarios nulos.");
            }
            else
            {
                try
                {
                    // Conectar a la base de datos y ejecutar la eliminación
                    using (SqlConnection conn = new SqlConnection(stringConnection))
                    {
                        string query = "DELETE FROM Usuarios WHERE id_usuario = @id";

                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@id", usuario.Id);

                        // Abrir la conexión y ejecutar el comando
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception)
                {
                    // Capturar excepciones y lanzar una excepción específica si ocurre un error durante la eliminación
                    throw new ElementoNoEncontradoException("Elemento no encontrado durante la eliminación del usuario.");
                }
            }
        }


        /// <summary>
        /// Busca una coincidencia en la base de datos en función de una columna y un valor proporcionados.
        /// </summary>
        /// <param name="columna">El nombre de la columna en la que buscar la coincidencia.</param>
        /// <param name="value">El valor a buscar en la columna especificada.</param>
        /// <returns>
        /// Devuelve true si se encuentra una coincidencia, de lo contrario, devuelve false.
        /// </returns>
        /// <exception cref="ArgumentNullException">Se lanza si alguno de los parámetros es nulo.</exception>
        public static bool BuscarCoincidencia(string columna, string value)
        {
            // Verificar si alguno de los parámetros es nulo
            if (columna is null || value is null)
            {
                // Lanzar una excepción si alguno de los parámetros es nulo
                throw new ArgumentNullException("Los parámetros no pueden ser nulos.");
            }
            else
            {
                // Obtener un usuario filtrado de la base de datos
                Usuario usuario = DBOUsuarios.GetUsuarioFiltrado(columna, value);

                // Devolver true si se encuentra un usuario, de lo contrario, devolver false
                return usuario is not null;
            }
        }

        /// <summary>
        /// Se encargar de devolverme un usuario con la coincidencia que se le indique en cuanto a la columna y el valor esperado el value
        /// </summary>
        /// <param name="columna">Columna a filtrar</param>
        /// <param name="value">Valor donde deben coincidir las columnas</param>
        /// <returns>Retornara el usuario en caso de encontrarlo o un null del caso contrario.</returns>
        /// <exception cref="DataBaseErrorException"></exception>
        public static Usuario GetUsuarioFiltrado(string columna, string value)
        {
        
            SqlConnection connection = new SqlConnection(stringConnection);
            List<string> columnasPermitidas = new List<string> { "id_usuario", "nombre", "apellido", "genero", "correo", "edad" ,"id_vuelo"};
            
            if (columnasPermitidas.Contains(columna) )
            {
                string sentencia = $"SELECT * FROM Usuarios WHERE {columna} = @value";

                try
                {
                    SqlCommand command = new SqlCommand(sentencia, connection);
                    command.Parameters.AddWithValue("@value", value);

                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if(reader.HasRows)
                    {
                        reader.Read();
                        UsuarioModelo usuario = new UsuarioModelo()
                        {
                            Id = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Apellido = reader.GetString(2),
                            Genero = reader.GetString(3),
                            Correo = reader.GetString(4),
                            Password = reader.GetString(5),
                            Edad = reader.GetInt32(6),
                            IdVuelo = reader.GetInt32(7),
                        };

                        return usuario.GetElement();
                    }
                    return null;
                   
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
        /// Se encarga de retormarme una lista con los usuarios que se encuentran en la base de datos
        /// </summary>
        /// <returns>Retornara una lista de aviones o arrojara una excepcion si ocurre un error al realizar la conexion</returns>
        /// <exception cref="DataBaseErrorException"></exception>
        public static List<Usuario> GetUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();
            SqlConnection connection = new SqlConnection(stringConnection);

            string sentencia = $"SELECT * FROM Usuarios";

            try
            {
                SqlCommand command = new SqlCommand(sentencia, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    UsuarioModelo usuario = new UsuarioModelo()
                    {
                        Id = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Apellido = reader.GetString(2),
                        Genero = reader.GetString(3),
                        Correo = reader.GetString(4),
                        Password = reader.GetString(5),
                        Edad = reader.GetInt32(6),
                        IdVuelo = reader.GetInt32(7),
                    };
                    usuarios.Add(usuario.GetElement());
                }

                return usuarios;
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
        /// <param name="usuario"></param>
        /// <exception cref="DataBaseErrorException">Lanzara una excepcion en el caso de no poder cargar el avion</exception>
        public static void AgregarUnUsuario(Usuario usuario)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(stringConnection))
                {
                    string query = $"INSERT INTO Usuarios (nombre,apellido,genero,correo,password,edad,id_vuelo) VALUES (@nombre,@apellido,@genero,@correo,@password,@edad,@id_vuelo)";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("apellido", usuario.Apellido);
                    cmd.Parameters.AddWithValue("genero", usuario.Genero);
                    cmd.Parameters.AddWithValue("correo", usuario.Correo);
                    cmd.Parameters.AddWithValue("password", usuario.Password);
                    cmd.Parameters.AddWithValue("edad", usuario.Edad);
                    cmd.Parameters.AddWithValue("id_vuelo", usuario.Id_vuelo);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new DataBaseErrorException("Conexion fallida, no se pudo agregar el usuario a la base de datos", ex);
            }
        }
        /// <summary>
        /// Se encarga de agregar una lista de aviones a la base de datos
        /// </summary>
        /// <param name="usuarios"></param>
        /// <returns>Retornara true si funcina con exito o false en el caso de que la lista este vacia</returns>
        public static void AgregarUsuarios(List<Usuario> usuarios)
        {
            if (usuarios.Count > 0)
            {
                foreach (Usuario usu in usuarios)
                {
                    DBOUsuarios.AgregarUnUsuario(usu);
                }
            }
            else
            {
                throw new DataBaseErrorException("Lista vacia");
            }
        }

    }
}
