using Semestral.Models;
using System;
using System.Data;
using Microsoft.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;
using ZXing;

namespace Semestral.Datos
{
    public class DB
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataSet ds;

        public DB() {
            string cadenaConexion = "Data Source=bdenergym.cj2sks08yo4o.us-east-2.rds.amazonaws.com;Initial Catalog=EnerGym;User ID=admin;Password=skizpkzi2-A;TrustServerCertificate=True;";
            con = new SqlConnection();
            con.ConnectionString = cadenaConexion;
            cmd = new SqlCommand();
            cmd.Connection = con;
        }

        Seguridad seguridad = new Seguridad();
        
        #region CREATES
        public int InsertarUsuario(UsuarioRequest usuario)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "InsertarUsuario";
                cmd.Parameters.Add(new SqlParameter("@nombre", usuario.nombre));
                cmd.Parameters.Add(new SqlParameter("@apellido", usuario.apellido));
                cmd.Parameters.Add(new SqlParameter("@contraseña", usuario.contraseña));
                cmd.Parameters.Add(new SqlParameter("@cedula", usuario.cedula));
                cmd.Parameters.Add(new SqlParameter("@estado", usuario.estado));
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

        public int GuardarClaseGrupal(ClaseRequest request)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO Clases (nombre, entrenador, estado, horarioInicio, horarioFinal) VALUES (@n, @e, 0, @hINI, @hFIN); SELECT SCOPE_IDENTITY();";
                cmd.Parameters.Add(new SqlParameter("@n", request.nombre));
                cmd.Parameters.Add(new SqlParameter("@e", request.entrenador));
                cmd.Parameters.Add(new SqlParameter("@hINI", request.horarioInicio));
                cmd.Parameters.Add(new SqlParameter("@hFIN", request.horarioFinal));

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

        public bool RegistrarEntrada(int id, string tipo, string estado)
        {
            try {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO Accesos (usuarioId, tipo) VALUES (@usuarioID, @tipo);";

                cmd.Parameters.Add(new SqlParameter("@usuarioID", id));
                cmd.Parameters.Add(new SqlParameter("@tipo", tipo));

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

        public bool RegistrarSalida(int id, string tipo, string estado)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO Accesos (usuarioId, tipo) VALUES (@usuarioID, @tipo);";

                cmd.Parameters.Add(new SqlParameter("@usuarioID", id));
                cmd.Parameters.Add(new SqlParameter("@tipo", tipo));
                cmd.Parameters.Add(new SqlParameter("@estado", estado));

                cmd.Connection.Open();
                int inserted = Convert.ToInt32(cmd.ExecuteNonQuery());
                return inserted > 0;
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

        public bool CrearReserva(ReservaRequest reserva)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO Reservas(usuarioId, fechaReserva, estado) VALUES (@usuarioId, @fechaReserva, 'Activa')";
                cmd.Parameters.Add(new SqlParameter("@usuarioId", reserva.usuarioId));
                cmd.Parameters.Add(new SqlParameter("@fechaReserva", reserva.fechaReserva));

                cmd.Connection.Open();
                int filas = cmd.ExecuteNonQuery();

                return filas > 0;
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

        #region READS
        public List<Usuario> BuscarUsuario(string nombre, string apellido, string cedula)
        {
            List<Usuario> usuarios = new List<Usuario>();
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;

                string query = "SELECT * FROM Usuarios WHERE 1 = 1";
                if (!string.IsNullOrEmpty(nombre))
                {
                    query += " AND nombre = @nombre";
                    cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                }
                if (!string.IsNullOrEmpty(apellido))
                {
                    query += " AND apellido = @apellido";
                    cmd.Parameters.Add(new SqlParameter("@apellido", apellido));
                }
                if (!string.IsNullOrEmpty(cedula))
                {
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
                            contraseña = row["contraseña"].ToString(),
                            cedula = row["cedula"].ToString(),
                            edad = Convert.ToInt32(row["edad"].ToString()),
                            estado = row["estado"].ToString(),
                            QRcodigo = row["QRcodigo"].ToString(),
                            QRinvitado = row["QRinvitado"].ToString(),
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

        public bool ExisteCedula(string cedula)
        {
            try
            {
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
            try
            {
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
                            deuda.fechaPago = Convert.ToDateTime(row["fechaPago"]);
                        }
                        else
                        {
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

        public int GimnasioOcupacionActual()
        {
            int capacidad;
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT ocupacionActual FROM Gimnasio";

                cmd.Connection.Open();

                capacidad = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd.Connection.Close();
            }
            return capacidad;
        }

        public string GimnasioOcupacionPorcentual()
        {
            string capacidad;
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT CAST((CAST(ocupacionActual AS FLOAT) / capacidadMaxima) * 100 AS VARCHAR(10)) + '%' FROM Gimnasio";

                cmd.Connection.Open();

                capacidad = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd.Connection.Close();
            }
            return capacidad;
        }

        public List<Clase> ObtenerClases()
        {
            List<Clase> clases = new List<Clase>();
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = 
                    "SELECT " +
                    "    c.nombre AS clase, " +
                    "    e.nombre + ' ' + e.apellido AS entrenador, " +
                    "    'Pendiente' AS estado, " +
                    "    CONVERT(NVARCHAR(25), c.horarioInicio) AS fechaInicio, " +
                    "    CONVERT(NVARCHAR(25), c.horarioFinal) AS fechaFinal FROM  " +
                    "    Clases c " +
                    "JOIN  " +
                    "    Entrenadores e ON c.entrenador = e.id " +
                    "WHERE " +
                    "    c.estado = 0 " +
                    "ORDER BY  " +
                    "    c.horarioInicio;";

                cmd.Connection.Open();
                ds = new DataSet();
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);

                foreach (DataTable table in ds.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        var clase = new Clase()
                        {
                            clase = row["clase"].ToString(),
                            entrenador = row["entrenador"].ToString(),
                            estado = row["estado"].ToString(),
                            fechaInicio = row["fechaInicio"].ToString(),
                            fechaFinal = row["fechaFinal"].ToString()
                        };
                        clases.Add(clase);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally {
                cmd.Connection.Close();
            }
            return clases;
        }

        public List<Usuario> BuscarUsuarioPorQR(string qrCodigo)
        {
            List<Usuario> usuarios = new List<Usuario>();
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM Usuarios WHERE QRcodigo = @QRcodigo;";
                cmd.Parameters.Add(new SqlParameter("@QRcodigo", qrCodigo));

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
                            contraseña = row["contraseña"].ToString(),
                            cedula = row["cedula"].ToString(),
                            edad = Convert.ToInt32(row["edad"].ToString()),
                            estado = row["estado"].ToString(),
                            QRcodigo = row["QRcodigo"].ToString(),
                            QRinvitado = row["QRinvitado"].ToString(),
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

        public List<Usuario> ObtenerUsuarioPorCredenciales(string cedula, string contraseña)
        {
            List<Usuario> usuarios = new List<Usuario>();
            try {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM Usuarios WHERE cedula = @cedula";
                cmd.Parameters.Add(new SqlParameter("@cedula", cedula));

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
                            contraseña = row["contraseña"].ToString(),
                            cedula = row["cedula"].ToString(),
                            edad = Convert.ToInt32(row["edad"].ToString()),
                            estado = row["estado"].ToString(),
                            QRcodigo = row["QRcodigo"].ToString(),
                            QRinvitado = row["QRinvitado"].ToString(),
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

        public int ValidarSalida(int usuarioID)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT TOP 1 id, created_at FROM Accesos WHERE usuarioId = @usuarioID AND tipo = 'Entrada' AND created_at >= DATEADD(HOUR, -5, GETDATE()) ORDER BY created_at DESC";
                cmd.Parameters.Add(new SqlParameter("@usuarioID", usuarioID));

                cmd.Connection.Open();
                int reg = Convert.ToInt32(cmd.ExecuteScalar());
                return reg;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally { cmd.Connection.Close(); }
        }

        public bool ValidarEntradaActiva(int usuarioID)
        {
            try 
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT COUNT(*) FROM Accesos WHERE usuarioID = @usuarioID AND tipo = 'Entrada' AND NOT EXISTS (SELECT 1 FROM Accesos WHERE usuarioID = @usuarioID AND tipo = 'Salida' AND created_at > (SELECT MAX(created_at) FROM Accesos WHERE usuarioID = @usuarioID AND tipo = 'Entrada'))";
                cmd.Parameters.Add(new SqlParameter("@usuarioID", usuarioID));

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

        public string QRUsuario(string cedula)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT QRcodigo FROM Usuarios WHERE cedula = @cedula;";
                cmd.Parameters.Add(new SqlParameter("@cedula", cedula));

                cmd.Connection.Open();

                var resul = cmd.ExecuteScalar();

                if (resul != null)
                {
                    return resul.ToString();
                }
                else
                {
                    return "No se encontró el código QR para la cédula proporcionada.";
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
        }

        public string QRUsuarioInv(string cedula)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT QRinvitado FROM Usuarios WHERE cedula = @cedula;";
                cmd.Parameters.Add(new SqlParameter("@cedula", cedula));

                cmd.Connection.Open();

                var resul = cmd.ExecuteScalar();

                if (resul != null)
                {
                    return resul.ToString();
                }
                else
                {
                    return "No se encontró el código QR para la cédula proporcionada.";
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
        }

        public AdminLogin ObtenerLogin(string usuario) {
            AdminLogin credenciales = new AdminLogin();
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ObtenerUsuarioYContraseña";
                cmd.Parameters.Add(new SqlParameter("@usuario", usuario));

                cmd.Connection.Open();
                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    credenciales.usuario = reader["usuario"].ToString();
                    credenciales.contras = reader["contraseña"].ToString();
                }
                return credenciales;
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

        public int ObtenerReservaPorId(int usuarioID)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT 1 FROM Reservas WHERE usuarioId = 1 AND estado = 'Activa' AND fechaReserva = CAST(GETDATE() AS DATE)";
                cmd.Parameters.Add(new SqlParameter("@usuarioId", usuarioID));

                cmd.Connection.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
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

        public int ValidarReservaPorId(int usuarioId, DateOnly fechaReserva)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT 1 FROM Reservas WHERE usuarioId = @usuarioId AND fechaReserva = @fechaReserva";
                cmd.Parameters.Add(new SqlParameter("@fechaReserva", fechaReserva));
                cmd.Parameters.Add(new SqlParameter("@usuarioId", usuarioId));

                cmd.Connection.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
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

        public List<Entrenador> ObtenerEntrenadores()
        {
            List<Entrenador> entrenadores = new List<Entrenador>();
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT nombre + ' ' + apellido AS nombre, foto FROM Entrenadores";
                cmd.Connection.Open();
                ds = new DataSet();
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);
                foreach (DataTable table in ds.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        var entrenador = new Entrenador()
                        {
                            nombre = row["nombre"].ToString(),
                            foto = row["foto"].ToString()
                        };
                        entrenadores.Add(entrenador);
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
            return entrenadores;
        }
        #endregion

        #region UPDATES
        public int ActualizarUsuario(int id, EditarRequest editar)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = 
                    "UPDATE Usuarios " +
                    "SET " +
                    "   nombre = @n, " +
                    "   apellido = @a, " +
                    "   cedula = @c, " +
                    "   edad = @edad, " +
                    "   QRcodigo = CONCAT(UPPER(LEFT(@n,1)), UPPER(LEFT(@a,1)), id), " +
                    "   QRinvitado = " +
                    "   CASE " +
                    "       WHEN estado = 'Premium' THEN CONCAT('P', UPPER(LEFT(@n, 1)), UPPER(LEFT(@a, 1)), id) " +
                    "       ELSE NULL " +
                    "   END " +
                    "WHERE id = @id";
                cmd.Parameters.Add(new SqlParameter("@n", editar.nombre));
                cmd.Parameters.Add(new SqlParameter("@a", editar.apellido));
                cmd.Parameters.Add(new SqlParameter("@c", editar.cedula));
                cmd.Parameters.Add(new SqlParameter("@edad", editar.edad));
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

        public int ActualizarUsuarioWeb(int id, EditarWeb editar)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = 
                    "UPDATE Usuarios " +
                    "SET " +
                    "   nombre = @n, " +
                    "   apellido = @a, " +
                    "   contraseña = @con, " +
                    "   cedula = @c, " +
                    "   edad = @edad, " +
                    "   QRcodigo = CONCAT(UPPER(LEFT(@n,1)), UPPER(LEFT(@a,1)), id), " +
                    "   QRinvitado = " +
                    "   CASE " +
                    "       WHEN estado = 'Premium' THEN CONCAT('P', UPPER(LEFT(@n, 1)), UPPER(LEFT(@a, 1)), id) " +
                    "       ELSE NULL " +
                    "   END " +
                    "WHERE id = @id";
                cmd.Parameters.Add(new SqlParameter("@n", editar.nombre));
                cmd.Parameters.Add(new SqlParameter("@a", editar.apellido));
                cmd.Parameters.Add(new SqlParameter("@con", editar.contraseña));
                cmd.Parameters.Add(new SqlParameter("@c", editar.cedula));
                cmd.Parameters.Add(new SqlParameter("@edad", editar.edad));
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

        public int ActualizarEstado(int id, string nuevoEstado)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE Usuarios SET estado = @estado, " +
                    "QRinvitado = " +
                    "CASE   " +
                    "WHEN @estado = 'Premium' THEN CONCAT('P', UPPER(LEFT(nombre,1)), UPPER(LEFT(apellido,1)), id)  " +
                    "ELSE NULL " +
                    "END " +
                    "WHERE id = @id;";
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

        public void ActualizarCapacidad(int val)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE Gimnasio SET ocupacionActual = ocupacionActual + @val";
                cmd.Parameters.Add(new SqlParameter("@val", val));

                cmd.Connection.Open();
                int filas = cmd.ExecuteNonQuery();
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

        public void ActualizarEstadoReserva(int usuarioId)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE Reservas SET estado = 'Cumplida' WHERE usuarioId = @usuarioId AND fechaReserva <= GETDATE()";
                cmd.Parameters.Add(new SqlParameter("@usuarioId", usuarioId));

                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
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

        #region DELETES
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
        #endregion
    }
}