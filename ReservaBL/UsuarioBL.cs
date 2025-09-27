using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReservaEN;
using ReservaDAL;

namespace ReservaBL
{
    public class UsuarioBL
    {
      
        public static UsuarioEN ObtenerUsuarioPorId(int id)
        {

            return UsuarioDAL.ObtenerUsuarioPorId(id);
        }
        public static List<UsuarioEN> MostrarUsuario()
        {
            return UsuarioDAL.MostrarUsuario();
        }
       
        public static int GuardarUsuario(UsuarioEN pusuarioEN)
        {
            return UsuarioDAL.AgregarUsuario(pusuarioEN);
        }
        public static int EliminarUsuario(int Id)
        {
            return UsuarioDAL.EliminarUsuario(Id);
        }
        public static int ModificarUsuario(UsuarioEN pusuarioEN)
        {
            return UsuarioDAL.ModificarUsuario(pusuarioEN);
        }
    }
}
