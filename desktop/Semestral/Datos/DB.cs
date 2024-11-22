using Semestral.Models;
using System;
using System.Data;
using Microsoft.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Semestral.Datos
{
    public class DB
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataSet ds;

        public DB() {
            string cadenaConexion = "Data Source=localhost;Initial Catalog=EnerGym;Integrated Security=True;TrustServerCertificate=True;";
            con = new SqlConnection();
            con.ConnectionString = cadenaConexion;
            cmd = new SqlCommand();
            cmd.Connection = con;
        }

        #region USUARIO
        public List<Usuario> ObtenerUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM Usuarios";

                cmd.Connection.Open();
                ds = new DataSet();

                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);

                foreach (DataTable table in ds.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        var usuario = new Usuario()
                        {
                            id = Convert.ToInt32(row["id"].ToString()),
                            nombre = row["nombre"].ToString(),
                            apellido = row["apellido"].ToString(),
                            cedula = row["cedula"].ToString(),
                            estado = row["estado"].ToString(),
                            fechaInscripcion = Convert.ToDateTime(row["fechaInscripcion"]),
                            updatedAt = Convert.ToDateTime(row["updated_at"])
                        };
                        usuarios.Add(usuario);
                    }
                }

            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                cmd.Connection.Close();
            }

            return usuarios;
        }

        public int InsertarUsuario(UsuarioRequest usuario) {
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO Usuarios (nombre, apellido, cedula, estado, edad) VALUES (@n, @a, @c, @e, @edad); SELECT SCOPE_IDENTITY();";
                cmd.Parameters.Add(new SqlParameter("@n", usuario.nombre));
                cmd.Parameters.Add(new SqlParameter("@a", usuario.apellido));
                cmd.Parameters.Add(new SqlParameter("@c", usuario.cedula));
                cmd.Parameters.Add(new SqlParameter("@e", usuario.estado));
                cmd.Parameters.Add(new SqlParameter("@edad", usuario.edad));

                cmd.Connection.Open();
                int insertedId = Convert.ToInt32(cmd.ExecuteScalar());
                if (insertedId > 0)
                    return insertedId;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally { cmd.Connection.Close(); }
            return 0;
        }

        public int ActualizarUsuario(int id, UsuarioRequest usuario)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE Usuarios SET nombre = @n, apellido = @a, cedula = @c, estado = @e, edad = @edad WHERE id = @id";
                cmd.Parameters.Add(new SqlParameter("@n", usuario.nombre));
                cmd.Parameters.Add(new SqlParameter("@a", usuario.apellido));
                cmd.Parameters.Add(new SqlParameter("@c", usuario.cedula));
                cmd.Parameters.Add(new SqlParameter("@e", usuario.estado));
                cmd.Parameters.Add(new SqlParameter("@edad", usuario.edad));
                cmd.Parameters.Add(new SqlParameter("@id", id));

                cmd.Connection.Open();
                int insertedId = Convert.ToInt32(cmd.ExecuteNonQuery());
                if (insertedId > 0)
                    return insertedId;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally { cmd.Connection.Close(); }
            return 0;
        }

        public int BorrarUsuario(int id)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM Usuarios WHERE id = " + id;

                cmd.Connection.Open();
                int insertedId = Convert.ToInt32(cmd.ExecuteNonQuery());
                if (insertedId > 0)
                    return insertedId;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally { cmd.Connection.Close(); }
            return 0;
        }

        public bool ExisteCedula(string cedula)
        {
            try {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT COUNT(*) FROM Usuarios WHERE cedula = @c";
                cmd.Parameters.Add(new SqlParameter("@c", cedula));

                cmd.Connection.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public bool ExisteCedulaParaEditar(int id, string cedula)
        {
            try {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT COUNT(*) FROM Usuarios WHERE cedula = @c AND id != @id";
                cmd.Parameters.Add(new SqlParameter("@c", cedula));
                cmd.Parameters.Add(new SqlParameter("@id", id));

                cmd.Connection.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public List<Usuario> BuscarUsuario(string nombre, string apellido, string cedula)
        {
            List<Usuario> usuarios = new List<Usuario>();
            try {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;

                string query = "SELECT * FROM Usuarios WHERE 1 = 1";
                if (!string.IsNullOrEmpty(nombre)) {
                    query += " AND nombre = @nombre";
                    cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                }
                if (!string.IsNullOrEmpty(apellido)) {
                    query += " AND apellido = @apellido";
                    cmd.Parameters.Add(new SqlParameter("@apellido", apellido));
                }
                if (!string.IsNullOrEmpty(cedula)) {
                    query += " AND cedula = @cedula";
                    cmd.Parameters.Add(new SqlParameter("@cedula", cedula));
                }
                cmd.CommandText = query;
                
                cmd.Connection.Open();
                ds = new DataSet();
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);

                foreach (DataTable table in ds.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        var usuario = new Usuario()
                        {
                            id = Convert.ToInt32(row["id"].ToString()),
                            nombre = row["nombre"].ToString(),
                            apellido = row["apellido"].ToString(),
                            cedula = row["cedula"].ToString(),
                            estado = row["estado"].ToString(),
                            fechaInscripcion = Convert.ToDateTime(row["fechaInscripcion"].ToString()),
                            updatedAt = Convert.ToDateTime(row["updated_at"])
                        };
                        usuarios.Add(usuario);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd.Connection.Close();
            }
            return usuarios;
        }

        public int ActualizarEstado(int id, string nuevoEstado)
        {
            try {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE Usuarios SET estado = @estado WHERE id = @id";
                cmd.Parameters.Add(new SqlParameter("@estado", nuevoEstado));
                cmd.Parameters.Add(new SqlParameter("@id", id));

                cmd.Connection.Open();
                int filas = cmd.ExecuteNonQuery();
                return filas;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally 
            {
                cmd.Connection.Close();
            }
        }

        public int InsertarTarjeta(TarjetaRequest tarjeta)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO Tarjetas (usuarioId, numeroTarjeta, nombreTitular, FechaExpiracionMes, FechaExpiracionYear) VALUES (@uID, @nTAR, @nTITU, @fMES, @fYEAR); SELECT SCOPE_IDENTITY();";
                cmd.Parameters.Add(new SqlParameter("@uID", tarjeta.usuarioID));
                cmd.Parameters.Add(new SqlParameter("@nTAR", tarjeta.numeroTarjeta));
                cmd.Parameters.Add(new SqlParameter("@nTITU", tarjeta.nombreTitular));
                cmd.Parameters.Add(new SqlParameter("@fMES", tarjeta.FechaExpiracionMes));
                cmd.Parameters.Add(new SqlParameter("@fYEAR", tarjeta.FechaExpiracionYear));

                cmd.Connection.Open();
                int insertedId = Convert.ToInt32(cmd.ExecuteScalar());
                if (insertedId > 0)
                    return insertedId;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally { cmd.Connection.Close(); }
            return 0;
        }

        public bool ExisteTarjeta(int usuarioID, string numeroTarjeta)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT COUNT(*) FROM Tarjetas WHERE usuarioId = @usuarioID AND numeroTarjeta = @numeroTarjeta";
                cmd.Parameters.Add(new SqlParameter("@usuarioID", usuarioID));
                cmd.Parameters.Add(new SqlParameter("@numeroTarjeta", numeroTarjeta));

                cmd.Connection.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }
        #endregion

        #region DEUDAS
        public bool ExisteUsuario(int usuarioID)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT COUNT(*) FROM Usuarios WHERE id = @id";
                cmd.Parameters.Add(new SqlParameter("@id", usuarioID));

                cmd.Connection.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public int InsertarDeudas(DeudasRequest request)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO Deudas (usuarioId, monto, estado, fechaPago) VALUES (@uID, @m, 0, NULL);";
                cmd.Parameters.Add(new SqlParameter("@uID", request.usuarioID));
                cmd.Parameters.Add(new SqlParameter("@m", request.monto));

                cmd.Connection.Open();
                int inserted = Convert.ToInt32(cmd.ExecuteNonQuery());
                if (inserted > 0)
                    return inserted;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally { cmd.Connection.Close(); }
            return 0;
        }

        public List<Deudas> ObtenerDeudaPorId(int usuarioId)
        {
            List<Deudas> deudas = new List<Deudas>();
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM Deudas WHERE usuarioId = @usuarioID";
                cmd.Parameters.Add(new SqlParameter("@usuarioID", usuarioId));

                cmd.Connection.Open();
                ds = new DataSet();
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);

                foreach (DataTable table in ds.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        var deuda = new Deudas()
                        {
                            id = Convert.ToInt32(row["id"].ToString()),
                            usuarioID = Convert.ToInt32(row["usuarioId"].ToString()),
                            monto = Convert.ToDouble(row["monto"].ToString()),
                            fechaCreacion = Convert.ToDateTime(row["fechaCreacion"]),
                            estado = Convert.ToInt32(row["estado"].ToString()),
                            updatedAt = Convert.ToDateTime(row["updated_at"])
                        };
                        if (row["fechaPago"] != DBNull.Value)
                        {
                            var xD = 0;
                            deuda.fechaPago = Convert.ToDateTime(row["fechaPago"]);
                        }
                        else {
                            deuda.fechaPago = null;
                        }
                        deudas.Add(deuda);
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                cmd.Connection.Close();
            }
            return deudas;
        }
        #endregion

        #region PAGOS
        public bool RegistrarPago(PagoRequest pago)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO Pagos (usuarioId, monto, fechaPago, metodoPago) VALUES (@uID, @m, @fPAGO, @metodo);";
                cmd.Parameters.Add(new SqlParameter("@uID", pago.usuarioID));
                cmd.Parameters.Add(new SqlParameter("@m", pago.monto));
                cmd.Parameters.Add(new SqlParameter("@fPAGO", DateTime.Now));
                cmd.Parameters.Add(new SqlParameter("@metodo", pago.metodoPago));

                cmd.Connection.Open();
                int inserted = Convert.ToInt32(cmd.ExecuteNonQuery());
                return inserted > 0;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally { cmd.Connection.Close(); }
        }
        #endregion
    }
}