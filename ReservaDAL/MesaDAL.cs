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
    public class MesaDAL
    {
        public static MesaEN ObtenerMesaPorId(int id)
        {
            MesaEN mesa = null;
            using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
            {
                _conn.Open();
                SqlCommand _comando = new SqlCommand("ObtenerMesaPorId", _conn as SqlConnection);
                _comando.CommandType = CommandType.StoredProcedure;
                _comando.Parameters.Add(new SqlParameter("@Id", id));

                IDataReader _reader = _comando.ExecuteReader();
                if (_reader.Read())
                {
                    mesa = new MesaEN
                    {
                        Id = _reader.GetInt32(0),
                        Nombre = _reader.GetString(1),
                        Personas = _reader.GetString(2)
                    };
                }

                _conn.Close();
            }

            return mesa;

        }
        public static List<MesaEN> MostrarMesa()
        {
            List<MesaEN> _Lista = new List<MesaEN>();
            using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
            {
                _conn.Open();
                SqlCommand _comando = new SqlCommand("MostrarMesa", _conn as SqlConnection);
                _comando.CommandType = CommandType.StoredProcedure;
                IDataReader _reader = _comando.ExecuteReader();
                while (_reader.Read())
                {
                    _Lista.Add(new MesaEN
                    {
                        Id = _reader.GetInt32(0),
                        Nombre = _reader.GetString(1),
                        Personas= _reader.GetString(2)

                    });
                }
                _conn.Close();
            }
            return _Lista;
        }

        public static int AgregarMesa(MesaEN pmesaEn)
        {
            using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
            {
                _conn.Open();
                SqlCommand _comando = new SqlCommand("GuardarMesa", _conn as SqlConnection);
                _comando.CommandType = CommandType.StoredProcedure;
                _comando.Parameters.Add(new SqlParameter("@Nombre", pmesaEn.Nombre));
                _comando.Parameters.Add(new SqlParameter("Personas", pmesaEn.Personas));
                int resultado = _comando.ExecuteNonQuery();
                _conn.Close();
                return resultado;
            }
        }

        public static int EliminarMesa(int Id)
        {
            using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
            {
                _conn.Open();
                SqlCommand _comando = new SqlCommand("EliminarMesa", _conn as SqlConnection);
                _comando.CommandType = CommandType.StoredProcedure;
                _comando.Parameters.Add(new SqlParameter("@Id", Id));
                int resultado = _comando.ExecuteNonQuery();
                _conn.Close();
                return resultado;
            }
        }

        public static int ModificarMesa(MesaEN pmesaEn)
        {
            using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
            {
                _conn.Open();
                SqlCommand _comando = new SqlCommand("ModificarMesa", _conn as SqlConnection);
                _comando.CommandType = CommandType.StoredProcedure;
                _comando.Parameters.Add(new SqlParameter("@Id", pmesaEn.Id));
                _comando.Parameters.Add(new SqlParameter("@Nombre", pmesaEn.Nombre));
                _comando.Parameters.Add(new SqlParameter("Personas",pmesaEn.Personas));
                int resultado = _comando.ExecuteNonQuery();
                _conn.Close();
                return resultado;
            }
        }
    }
}
