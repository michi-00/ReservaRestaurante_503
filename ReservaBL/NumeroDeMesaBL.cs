using ReservaDAL;
using ReservaEN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaBL
{
    public class NumeroDeMesaBL
    {
        public static List<NumeroDeMesaEN> MostrarNumeroDeMesa()
        {
            return NumeroDeMesaDAL.MostrarNumeroDeMesa();
        }
        public static int GuardarNumeroDeMesa(NumeroDeMesaEN pnumerodemesa)
        {
            return NumeroDeMesaDAL.AgregarNumeroDeMesa(pnumerodemesa);
        }
        public static int EliminarNumeroDeMesa(int Id)
        {
            return NumeroDeMesaDAL.EliminarNumeroDeMesa(Id);
        }
        public static int ModificarNumeroDeMesa(NumeroDeMesaEN pnumerodemesa)
        {
            return NumeroDeMesaDAL.ModificarNumeroDeMesa(pnumerodemesa);
        }
    }
}
