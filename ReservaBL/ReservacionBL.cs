using ReservaDAL;
using ReservaEN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaBL
{
    public class ReservacionBL
    {
        public static List<ReservacionEN> MostrarReservacion()
        {
            return ReservacionDAL.MostrarReservacion();
        }
        public static int GuardarReservacion(ReservacionEN preservacionEN)
        {
            return ReservacionDAL.AgregarReservacion(preservacionEN);
        }
        public static int EliminarReservacion(int Id)
        {
            return ReservacionDAL.EliminarReservacion(Id);
        }
        public static int ModificarReservacion(ReservacionEN preservacionEN)
        {
            return ReservacionDAL.ModificarReservacion(preservacionEN);
        }
    }
}
