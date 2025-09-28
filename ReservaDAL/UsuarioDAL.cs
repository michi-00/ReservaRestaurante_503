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
    public class UsuarioDAL
    {
       
        public static UsuarioEN ObtenerUsuarioPorId(int id)
        {
            using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
            {
                _conn.Open();
                SqlCommand _comando = new SqlCommand("ObtenerUsuarioPorId", _conn as SqlConnection);
                _comando.CommandType = CommandType.StoredProcedure;
                _comando.Parameters.Add(new SqlParameter("@Id", id));

                IDataReader _reader = _comando.ExecuteReader();
                if (_reader.Read())
                {
                    return new UsuarioEN
                    {
                        Id = _reader.GetInt32(0),
                        IdRol = _reader.GetInt32(1),
                        Nombre = _reader.GetString(2),
                        Apellido = _reader.GetString(3),
                        Celular = _reader.GetString(4),
                        Cuenta = _reader.GetString(5),
                        Contrasenia = _reader.GetString(6)
                    };
                }
            }
            return null;
        }



        public static List<UsuarioEN> MostrarUsuario()
        {
            List<UsuarioEN> _Lista = new List<UsuarioEN>();
            using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
            {
                _conn.Open();
                SqlCommand _comando =
                new SqlCommand("MostrarUsuarioNombre", _conn as SqlConnection);
                _comando.CommandType = CommandType.StoredProcedure;
                IDataReader _reader = _comando.ExecuteReader();
                while (_reader.Read())
                {
                    _Lista.Add(new UsuarioEN
                    {
                      Id= _reader.GetInt32(0),
                      IdRol= _reader.GetInt32(1),
                      Nombre_Rol=_reader.GetString(2),
                      Nombre= _reader.GetString(3),
                      Apellido= _reader.GetString(4),
                      Celular= _reader.GetString(5),
                      Cuenta = _reader.GetString(6),
                      Contrasenia= _reader.GetString(7)

                    });
                }
                _conn.Close();
            }
            return _Lista;
        }
       
        
        public static int AgregarUsuario(UsuarioEN pusuarioEN)
        {
            using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
            {
                _conn.Open();
                SqlCommand _comando = new SqlCommand("GuardarUsuario", _conn as SqlConnection);
                _comando.CommandType = CommandType.StoredProcedure;
                _comando.Parameters.Add(new SqlParameter("@IdRol", pusuarioEN.IdRol));
                _comando.Parameters.Add(new SqlParameter("@Nombre", pusuarioEN.Nombre));
                _comando.Parameters.Add(new SqlParameter("@Apellido", pusuarioEN.Apellido));
                _comando.Parameters.Add(new SqlParameter("@Celular", pusuarioEN.Celular));
                _comando.Parameters.Add(new SqlParameter("@Cuenta", pusuarioEN.Cuenta));
                _comando.Parameters.Add(new SqlParameter("@Contrasenia", pusuarioEN.Contrasenia));
                int resultado = _comando.ExecuteNonQuery();
                _conn.Close();
                return resultado;
            }

        }
        public static int EliminarUsuario(int Id)
        {
            using (IDbConnection _conn =
                ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
            {
                _conn.Open();
                SqlCommand _comando =
                new SqlCommand("EliminarUsuario", _conn as SqlConnection);
                _comando.CommandType = CommandType.StoredProcedure;
                _comando.Parameters.Add(new SqlParameter("@Id", Id));
                int resultado = _comando.ExecuteNonQuery();
                _conn.Close();
                return resultado;
            }
        }
        public static int ModificarUsuario(UsuarioEN pusuarioEN)
        {
            using (IDbConnection _conn =
                ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
            {
                _conn.Open();
                SqlCommand _comando =
                new SqlCommand("ModificarUsuario", _conn as SqlConnection);
                _comando.CommandType = CommandType.StoredProcedure;
                _comando.Parameters.Add(new SqlParameter("@Id", pusuarioEN.Id));
                _comando.Parameters.Add(new SqlParameter("@IdRol", pusuarioEN.IdRol));
                _comando.Parameters.Add(new SqlParameter("@Nombre", pusuarioEN.Nombre));
                _comando.Parameters.Add(new SqlParameter("@Apellido", pusuarioEN.Apellido));
                _comando.Parameters.Add(new SqlParameter("@Celular", pusuarioEN.Celular));
                _comando.Parameters.Add(new SqlParameter("@Cuenta", pusuarioEN.Cuenta));
                _comando.Parameters.Add(new SqlParameter("@Contrasenia", pusuarioEN.Contrasenia));
                int resultado = _comando.ExecuteNonQuery();
                _conn.Close();
                return resultado;
            }

        }
        public static UsuarioEN ValidarUsuario(string cuenta, string contrasenia)
        {
            using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
            {
                _conn.Open();
                SqlCommand _comando = new SqlCommand("ValidarUsuario", _conn as SqlConnection);
                _comando.CommandType = CommandType.StoredProcedure;
                _comando.Parameters.AddWithValue("@Cuenta", cuenta);
                _comando.Parameters.AddWithValue("@Contrasenia", contrasenia);

                SqlDataReader reader = _comando.ExecuteReader();
                UsuarioEN usuario = null;


                if (reader.Read())
                {
                    usuario = new UsuarioEN
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        IdRol = reader.GetInt32(reader.GetOrdinal("IdRol")),
                        Cuenta = reader.GetString(reader.GetOrdinal("Cuenta")),
                        Contrasenia = reader.GetString(reader.GetOrdinal("Contrasenia"))
                    };
                }

                _conn.Close();
                return usuario;
            }
        }
    }
}
