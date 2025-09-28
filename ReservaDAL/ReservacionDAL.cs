using Microsoft.Data.SqlClient;
using ReservaEN;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaDAL
{
    public class ReservacionDAL
    {
        public static List<ReservacionEN> MostrarReservacion()
        {
            List<ReservacionEN> _Lista = new List<ReservacionEN>();
            using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
            {
                _conn.Open();
                SqlCommand _comando = new SqlCommand("MostrarReservacionNombre", _conn as SqlConnection);
                _comando.CommandType = CommandType.StoredProcedure;
                IDataReader _reader = _comando.ExecuteReader();
                while (_reader.Read())
                {
                    _Lista.Add(new ReservacionEN
                    {
                        Id = _reader.GetInt32(0),
                        IdMesa = _reader.GetInt32(1),
                        Nombre_Mesa=_reader.GetString(2),
                        IdNumeroDeMesa=_reader.GetInt32(3),
                        Nombre_NumeroDeMesa=_reader.GetString(4),
                        IdUsuario=_reader.GetInt32(5),
                        Nombre_Usuario=_reader.GetString(6),
                        Apellido_Usuario= _reader.GetString(7),
                        Numero_Celular=_reader.GetString(8),
                        FechaDeReservacion=_reader.GetDateTime(9)
                  
                    });
                }
                _conn.Close();
            }
            return _Lista;
        }

        public static int AgregarReservacion(ReservacionEN preservacionEN)
        {
            using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
            {
                _conn.Open();
                SqlCommand _comando = new SqlCommand("GuardarReservacion", _conn as SqlConnection);
                _comando.CommandType = CommandType.StoredProcedure;
                _comando.Parameters.Add(new SqlParameter("@IdMesa", preservacionEN.IdMesa));
                _comando.Parameters.Add(new SqlParameter("@IdNumeroDeMesa", preservacionEN.IdNumeroDeMesa));
                _comando.Parameters.Add(new SqlParameter("@IdUsuario", preservacionEN.IdUsuario));
                _comando.Parameters.Add(new SqlParameter("@FechaDeReservacion",(object?)preservacionEN.FechaDeReservacion ?? DBNull.Value));
                
                int resultado = _comando.ExecuteNonQuery();
                _conn.Close();
                return resultado;
            }
        }

        public static int EliminarReservacion(int Id)
        {
            using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
            {
                _conn.Open();
                SqlCommand _comando = new SqlCommand("EliminarReservacion", _conn as SqlConnection);
                _comando.CommandType = CommandType.StoredProcedure;
                _comando.Parameters.Add(new SqlParameter("@Id", Id));
                int resultado = _comando.ExecuteNonQuery();
                _conn.Close();
                return resultado;
            }
        }

        public static int ModificarReservacion(ReservacionEN preservacionEN)
        {
            using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
            {
                _conn.Open();
                SqlCommand _comando = new SqlCommand("ModificarReservacion", _conn as SqlConnection);
                _comando.CommandType = CommandType.StoredProcedure;
                _comando.Parameters.Add(new SqlParameter("@Id", preservacionEN.Id));
                _comando.Parameters.Add(new SqlParameter("@IdMesa", preservacionEN.IdMesa));
                _comando.Parameters.Add(new SqlParameter("@IdNumeroDeMesa", preservacionEN.IdNumeroDeMesa));
                _comando.Parameters.Add(new SqlParameter("@IdUsuario", preservacionEN.IdUsuario));
                _comando.Parameters.Add(new SqlParameter("@FechaDeReservacion", (object?)preservacionEN.FechaDeReservacion ?? DBNull.Value));
                int resultado = _comando.ExecuteNonQuery();
                _conn.Close();
                return resultado;
            }
        }
    }
}
