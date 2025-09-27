using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaEN
{
    public class ReservacionEN
    {
        public int Id { get; set; }
        public int IdMesa { get; set; }
        public int IdNumeroDeMesa { get; set; }
        public int IdUsuario { get; set; }
        public DateTime FechaDeReservacion { get; set; }
        
    }
}
