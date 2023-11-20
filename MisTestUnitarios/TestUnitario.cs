using Entidades;
using Entidades.Class;
using Entidades.Class.Archivos;
using Entidades.DBO;
using Entidades.Excepciones;

namespace MisTestUnitarios
{
    [TestClass]
    public class TestUnitario
    {

        //###########################################TEST DEL METODO ObtenerAvionPorId() DE CLASE ESTATICA DBOAviones###########################################//    
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void AlInvocarAlMetodoObtenerVueloPorId_SiSeLePasaUnValorNegativoSeEsperaObetener_UnaExcepcionIndexOutOfRangeException()
        {
            //Arrangue
            int id = -1;

            //Act
            Avion resultado = DBOAviones.ObtenerVueloPorId(id);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ElementoNoEncontradoException))]
        public void AlInvocarAlMetodoObtenerVueloPorId_SiSeLePasaUnIdQueNoSeEncuentraEnLaBaseDeDatosSeEsperaObetener_UnaExcepcionElementoNoEncontradoException()
        {
            //Arrangue
            int id = 100;
            //Act
            Avion resultado = DBOAviones.ObtenerVueloPorId(id);

        }
        
        [TestMethod]
        public void AlInvocarAlMetodoObtenerVueloPorId_SiSeLePasaUnIdQueSeEncuentraEnLaBaseDeDatosSeEsperaObetener_UnaAvionInstanciadoConElMismoId()
        {
            //Arrangue
            int id = 231;            
            //Act
            Avion resultado = DBOAviones.ObtenerVueloPorId(id);
            //Assert          
            Assert.AreEqual( id,resultado.Id);
        }

        //###########################################TEST DEL METODO EscribirArchivo() DE CLASE ESTATICA ManejadorDeArchivos###########################################//    

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
    }
}