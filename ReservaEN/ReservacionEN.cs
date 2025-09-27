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
        public string Nombre_Mesa { get; set; } = string.Empty;
        public int IdNumeroDeMesa { get; set; }
        public string Nombre_NumeroDeMesa { get; set; }= string.Empty;
        public int IdUsuario { get; set; }
        public string Nombre_Usuario { get; set; }=string.Empty;
        public string Apellido_Usuario { get; set; } = string.Empty;
        public string Numero_Celular { get; set; }= string.Empty ;
        public DateTime FechaDeReservacion { get; set; }
        
    }
}
