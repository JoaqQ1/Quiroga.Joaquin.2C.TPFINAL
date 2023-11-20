using Entidades.Interfases;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Class
{
    public class UsuarioModelo: IAccionesClasesModelo<Usuario>
    {
        public int Edad { get;  set; }
        public string Genero { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Password { get;  set; }

        public int Id { get;  set; }
        public int IdVuelo { get; set; }


        public Usuario GetElement()
        {            
            Usuario usuario = new Usuario(this.Id, this.Nombre, this.Apellido, this.Edad, this.Genero, this.Correo, this.Password);
            usuario.Id_vuelo = this.IdVuelo;
            return usuario;       
        }     
    }
}
