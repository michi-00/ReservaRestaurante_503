using ReservaDAL;
using ReservaEN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaBL
{
    public class RolBL
    {
        public static List<RolEN> MostrarRol()
        {
            return RolDAL.MostrarRol();
        }
        public static int GuardarRol(RolEN prolEN)
        {
            return RolDAL.AgregarRol(prolEN);
        }
        public static int EliminarRol(int Id)
        {
            return RolDAL.EliminarRol(Id);
        }
        public static int ModificarRol(RolEN prolEN)
        {
            return RolDAL.ModificarRol(prolEN);
        }
    }
}
