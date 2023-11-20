using Entidades.Interfases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Class
{
    public class Usuario : Persona 
    {
        private int id;
        public int Id { get { return this.id; }  set { this.id = value; } }
        public int Id_vuelo { get;set; }

        public Usuario(int id,string nombre, string apellido, int edad, string genero,string correo,string password)
            : base(nombre, apellido, edad, genero)
        {

            this.Correo = correo;
            this.Password = password;
            this.Id = id;            
        }       
        public static bool operator ==(Usuario a, Usuario b)
        {
            return a.Correo == b.Correo && a.Password == b.Password;
        }
        public static bool operator !=(Usuario a, Usuario b)
        {
            return !(a == b);
        }
    }
}
