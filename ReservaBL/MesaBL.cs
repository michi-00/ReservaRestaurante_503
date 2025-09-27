using ReservaDAL;
using ReservaEN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaBL
{
    public class MesaBL
    {
        public static List<MesaEN> MostrarMesa()
        {
            return MesaDAL.MostrarMesa();
        }
        public static int GuardarMesa(MesaEN pmesaEn)
        {
            return MesaDAL.AgregarMesa(pmesaEn);
        }
        public static int EliminarMesa(int Id)
        {
            return MesaDAL.EliminarMesa(Id);
        }
        public static int ModificarMesa(MesaEN pmesaEn)
        {
            return MesaDAL.ModificarMesa(pmesaEn);
        }
    }
}
