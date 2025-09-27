using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaEN
{
    public class UsuarioEN
    {
        public int Id { get; set; }
        public int IdRol { get; set; }
        public string Nombre_Rol { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Celular { get; set; } = string.Empty;
        public string Cuenta { get; set; } = string.Empty;
        public string Contrasenia { get; set; } = string.Empty;
        
    }
}
