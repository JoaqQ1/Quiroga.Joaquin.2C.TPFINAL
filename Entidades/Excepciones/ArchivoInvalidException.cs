using System.Runtime.Serialization;

namespace Entidades.Excepciones
{

    public class ArchivoInvalidException : Exception
    {
        public ArchivoInvalidException(string? message) : base(message)
        {
        }
        public ArchivoInvalidException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}