using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class DataBaseErrorException : Exception
    {
        public DataBaseErrorException(string? message) : base(message)
        {
        }

        public DataBaseErrorException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
