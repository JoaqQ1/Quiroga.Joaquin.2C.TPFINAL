# PARCIAL LABO II

La aplicación que he desarrollado proporciona información actualizada sobre los vuelos disponibles en nuestra aerolínea, brindando a los clientes la capacidad de filtrar datos por destino específico o visualizar todos los vuelos en nuestra base de datos. Además, los usuarios tienen la opción de crear una cuenta personalizada, lo que les permite acceder a funciones exclusivas.

Una vez registrados e iniciada la sesión, los clientes pueden comprar vuelos seleccionando su destino preferido entre las opciones disponibles. La aplicación garantiza una experiencia fluida y segura, evitando que los usuarios compren más de un vuelo para evitar confusiones. Además, proporciona la conveniencia de visualizar los vuelos adquiridos para que los usuarios no olviden sus planes de viaje.

Para aquellos que prefieren explorar como invitados, la aplicación permite acceder y visualizar la información de vuelo sin necesidad de registrarse. Esta flexibilidad garantiza que todos los usuarios, tanto registrados como no registrados, puedan aprovechar al máximo la plataforma y encontrar la información que necesitan para tomar decisiones informadas sobre sus viajes.

## __Utilizacion de Formulario__
![FrmPrincipal](imgForm\frmPrincipal.jpeg)
![FrmCrearUsuario](imgForm\frmCrearUsuario.jpeg)
![FrmLogin](imgForm\frmLogin.jpeg)

## __Utilizacion de _[Archivos-Serializacion-Genericos-Excepciones]___
He implementado un sistema de gestión de usuarios mediante archivos, utilizando una clase estática `genérica` denominada `ManejadorDeArchivos<T>`. Esta clase me ha permitido realizar operaciones como escribir, leer y filtrar datos desde y hacia archivos, utilizando el tipo de dato específico según el contexto. Un ejemplo concreto fue la `serialización` de la clase `Usuario` y la manipulación de una lista de `enteros` que contenía los identificadores (IDs) asociados a los usuarios.
`Quer utilizo a la hora de Crear un usuario.`

En el caso de errores durante la lectura de archivos, he incorporado una excepción personalizada llamada `ArchivoInvalidException`. Esta excepción se lanzará para alertar sobre cualquier problema que pueda surgir durante el proceso de lectura, proporcionando una gestión clara de posibles inconvenientes en la operación. 
 ```
 public static void EscribirArchivo(List<T> objs, string nombreArchivo)
{
    if (objs.Count == 0 || nombreArchivo is null)
    {
        throw new ArgumentNullException("La lista es vacia o el path es nulo");
    }
    else
    {
        string pathDir = 
        string pathArchivo = Path.Combine(pathDir, nombreArchivo);
        JsonSerializerOptions opcion = new JsonSerializerOptions();
        opcion.WriteIndented = true;
       
        using (StreamWriter sw = new StreamWriter(pathArchivo))
        {
            string archivoJson = JsonSerializer.Serialize(objs, opcion);
            sw.WriteLine(archivoJson);
        }              
    }
}
```
```
public static List<T> LeerArchivo(string nombreArchivo)
{
    if(nombreArchivo is null)
    {
        throw new ArgumentNullException("Path nulo");
    }
    else
    {
        string pathDir = ManejadorDeArchivos<T>.CrearCarpeta();
        string pathArchivo = Path.Combine(pathDir, nombreArchivo);
        List<T> elemento;
        try
        {
            using (StreamReader sr = new StreamReader(pathArchivo))
            {
                string archivoJson = sr.ReadToEnd();
                elemento = JsonSerializer.Deserialize<List<T>>(archivoJson);
                return elemento;
            }
        }
        catch (Exception ex)
        {
            throw new ArchivoInvalidException("Error al leer el archivo", ex);
        }
    }            
}
```
El método `ManejadorDeArchivos<T>.CrearCarpeta()` tiene la responsabilidad de crear una carpeta denominada `Archivos json`. En caso de que la carpeta aún no exista, se creará y se devolverá su ruta. Sin embargo, si la carpeta ya existe, simplemente se retornará la ruta del contenedor que almacena los archivos de usuarios e IDs. 

### __Utilizacion de _[Base de datos]___

Utilice base de datos para almacenar los vuelos de la aerolinea.Tambien utilice un clase estatica `DBOAviones` para los aviones y `DBOUsuarios` para los usuario que comprar un vuelo en la aerolinea. Con metodos para leer, escribir, actualizar y filtrar datos de la base de datos. `Utilizo Cuando genero vuelos nuevos, tambien a la hora de mostrarlos, leyendo la base de datos, y cuando estan desactualizados actualizarlos`
```
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
```
```
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
```
```
public static List<Avion> GetVuelosFiltrados(string columna,string value)
{
    List<Avion> aviones = new List<Avion>();
    SqlConnection connection = new SqlConnection(stringConnection);
    List<string> columnasPermitidas = new List<string> {"id_avion","origen", "destino","horas_de_vuelo","costo","hora_de_salida" };
    
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
```
### __Utilizacion de _[ExpresionLambda]___

En el método `BuscarUsuarioEnArchivo`, se utiliza una expresión lambda para buscar coincidencias en la lista de usuarios almacenada en un archivo. `La expresión lambda se emplea como un predicado` que compara el correo electrónico del usuario proporcionado con el correo electrónico de cada usuario en la lista. La función `BuscarCoincidencia` de la clase `ManejadorDeArchivos<Usuario>` acepta este predicado y realiza la búsqueda correspondiente. Si hay una coincidencia, el método devuelve true; de lo contrario, retorna false.` Que se utiliza a la hora de logear al usuario`
```
public static bool BuscarUsuarioEnArchivo(Usuario usuario)
{
    return ManejadorDeArchivos<Usuario>.BuscarCoincidencia(usu => usu.Correo == usuario.Correo, PATH_USUARIO); 
}
```
```
public static bool BuscarCoincidencia(Predicate<T> buscador, string nombreArchivo)
{
    bool rtn = false;
    try
    {
        List<T> lista = ManejadorDeArchivos<T>.LeerArchivo(nombreArchivo);
        foreach (T obj in lista)
        {
            if (buscador(obj))
            {
                rtn = true;
            }
        }
        return rtn;
    }
    catch (ArchivoInvalidException)
    { 
        return rtn;
    }            
}
```

### __Utilizacion de _[Eventos-Delegados]___

Utilize un `delegado` de tipo `Action` creado por mi que `no retorna nada y recibe una lista de aviones como unico parametro`,deste `delegado` sera manejado por el `evento onOptimizacion` ,atributo de la clase `FrmAerolinea`, que manejara el evento `EliminarVuelos` este `recibe una lista con los aviones`,respetando la firma del `DelegateOptimizacion` que se deseen eliminar de la base de datos.
```
  //Delegado
  private delegate void DelegateOptimizacion(List<Avion> lista);
  //Evento
  private event DelegateOptimizacion onOptimizacion;
  ```
  ```
          private void FrmAerolineas_Load(object sender, EventArgs e)
        {
            this.Text = $"Aerolineas -{Environment.UserName}-";
            this.cbOrigen.DataSource = new object[] { "Argentina" };
            this.cbDestino.DataSource = new object[] { "Brasil", "Peru", "Mexico", "Venezuela", "Uruguay", "Paraguay", "España" };
            this.btnComprarVuelo.Enabled = false;
            this.btnMostrarVueloComprado.Enabled = false;
            this.timerTiempo.Interval = 1000;
            this.timerTiempo.Start();
            this.actualizacionDeVuelos = new Task(ActualizarVuelos, this.cancelarActualizacionDeVuelos.Token);
            this.onOptimizacion += EliminarVuelos; 

            Task.Run(() =>
            {
                while (!this.cancelarActualizacionDeVuelos.IsCancellationRequested)
                {
                    this.ActualizarVuelos();
                    Thread.Sleep(5000);
                }
            });
        }
  ```

```
  private void ActualizarVuelos()
{
    try
    {
        this.listaDeAviones = DBOAviones.GetVuelos();
        if (this.listaDeAviones.Count == 0)
        {
            List<Avion>? nuevosVuelos = this.listaDeAviones.GeneradorDeVuelos();
            DBOAviones.AgregarAviones(nuevosVuelos);
        }
        //Si la lista supera los 20 vuelos el evento onOptimizacion se encargara de eliminar los vuelos no disponibles.
        if(this.listaDeAviones.Count > 20 && this.onOptimizacion is not null)
        {
            List<Avion>? vuelosNodisponibles = DBOAviones.GetVuelosFiltrados("disponible","0");
            this.onOptimizacion.Invoke(vuelosNodisponibles);
        }

        .... Resto del codigo...
    }
}
```
### __Utilizacion de _[TareasMultiHilo]___
He implementado el uso de múltiples hilos para llevar a cabo una verificación en segundo plano que asegure la actualización en tiempo real de la información de vuelos. Esta medida preventiva busca evitar posibles problemas de colapso de datos al verificar dinámicamente la cantidad de vuelos en la lista. En caso de que la lista contenga más de 20 vuelos, se procede a eliminar aquellos que ya no se encuentran disponibles, garantizando así la integridad y eficiencia de la información en la aplicación.
 ```
 Task.Run(() =>
 {
     while (!this.cancelarActualizacionDeVuelos.IsCancellationRequested)
     {
         this.ActualizarVuelos();
         Thread.Sleep(5000);
     }
 });
```
```
private void ActualizarVuelos()
{
    try
    {
        this.listaDeAviones = DBOAviones.GetVuelos();
        if (this.listaDeAviones.Count == 0)
        {
            List<Avion>? nuevosVuelos = this.listaDeAviones.GeneradorDeVuelos();
            DBOAviones.AgregarAviones(nuevosVuelos);
        }
        //Si la lista supera los 20 vuelos el evento onOptimizacion se encargara de eliminar los vuelos no disponibles.
        if(this.listaDeAviones.Count > 20 && this.onOptimizacion is not null)
        {
            List<Avion>? vuelosNodisponibles = DBOAviones.GetVuelosFiltrados("disponible","0");
            this.onOptimizacion.Invoke(vuelosNodisponibles);
        }

        DateTime ultimoVuelo = this.listaDeAviones.DevolverHorarioDeUltimoVuelo();
       
        //Si el ultimo vuelo ya salio se actualizara la lista de aviones creando nuevos vuelos
        if (ultimoVuelo < DateTime.Now)
        {
            this.listaDeAviones.ActualizarVuelos(DateTime.Now);

            //Se actualizara el estado de los aviones de disponibles a no disponibles
            DBOAviones.ActualizarVuelos(this.listaDeAviones);
            List<Avion>? nuevosVuelos = this.listaDeAviones.GeneradorDeVuelos();
            DBOAviones.AgregarAviones(nuevosVuelos);
            MessageBox.Show("Vuelos actualizados", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
    catch (Exception)
    {
        MessageBox.Show("Error al actualizar los vuelos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
```
### __Utilizacion de _[TestUnitarios]___
He implementado pruebas unitarias para la función `ManejadorDeArchivos<T>.EscribirArchivo` con el objetivo de verificar su correcto funcionamiento en diferentes escenarios. `Las pruebas cubren casos como enviar una lista vacía, proporcionar un path nulo y asegurarse de que, al enviar una lista con al menos un elemento, se cree una carpeta en el directorio actual llamada 'Archivos json' y que el archivo se haya serializado y guardado en esa carpeta`. Estas pruebas garantizan la robustez y confiabilidad de la funcionalidad de escritura de archivos en el sistema. 
```
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void AlEnviarUnaListaVacia_SeEsperaObetener_UnaExcepcionArgumentNullException()
    {
        //Arrange
        List<string> lista = new List<string>();
        //Act
        ManejadorDeArchivos<string>.EscribirArchivo(lista,"test");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void AlEnviarPathNulo_SeEsperaObetener_UnaExcepcionArgumentNullException()
    {
        //Arrange
        List<string> lista = new List<string>() { "hola"};

        string path = null;
        //Act
        ManejadorDeArchivos<string>.EscribirArchivo(lista, path);
    }

    [TestMethod]      
    public void AlEnviarUnaListaConElemento_SeEsperaObetener_UnaCarpetaEnElDirectorioActualEnUnaCarpetaArchivosJsonConLosElementoSerializados()
    {
        //Arrange
        List<string> lista = new List<string>() { "hola" };

        string path = "test";
        //Act
        ManejadorDeArchivos<string>.EscribirArchivo(lista, path);
        string pathDirArchivos = Path.Combine(".", "Archivos json");
        string pathDelArchivoCreado = Path.Combine(pathDirArchivos, path);
        
        //Assert
        Assert.IsTrue(File.Exists(pathDelArchivoCreado));
    }

```
### __Utilizacion de _[MetodosDeExtension]___
Utilice metodos de extension para una lista de aviones donde le proporcion metodos como el metodo `GeneradorDeVuelos`, que `se encargagar de generar vuelos aleatorios, agregarlos a una lista y retornar esa lista`.

```
        public static List<Avion> GeneradorDeVuelos(this List<Avion> aviones)
        {
            string[] destinos = new string[] { "Brasil", "Peru", "Mexico", "Venezuela", "Uruguay", "Paraguay", "España" };
            Random random = new Random();
            List<Avion> avionesNuevos = new List<Avion>();
            for (int i = 0; i < 10; i++)
            {
                AvionModelo avion = new AvionModelo();
                
                avion.Origen = "Argentina";
                avion.Destino = destinos[random.Next(0, destinos.Count())];
                
                switch (avion.Destino)
                {
                    case "España":
                        avion.HorasDeVuelo = 8;
                        avion.Costo = 4000;
                        break;
                    case "Mexico":
                        avion.HorasDeVuelo = 4;
                        avion.Costo = 3000;

                        break;
                    default:
                        avion.HorasDeVuelo = random.Next(1, 3);
                        avion.Costo = random.Next(1500, 2500);
                        break;
                }
                
                avion.HoraDeSalida = DateTime.Now.AddHours(random.Next(1, 6));
                avion.Disponible = true;

                avionesNuevos.Add(avion.GetElement());
            }
            return avionesNuevos;
        }
```
### __Utilizacion de _[Interfaces]___
He empleado una interfaz genérica denominada `IAccionesClasesModelo<T>` para aplicar métodos y propiedades a las clases `AvionModelo` y `UsuarioModelo`. Esta interfaz proporciona una estructura común que ambas clases implementan, permitiendo la definición y estandarización de operaciones específicas que pueden llevar a cabo.
```
public interface IAccionesClasesModelo<T>
{
    public int Id {get;set;}
    
    T GetElement();      
}

```