using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Interfases
{
    public interface IAcciones
    {
        public void CrearUsuario();
        public void ModificarUsuario(int idUsuario,string campo);
        public void EliminarUsuario(int idUsuario, string campo);

    }
}
