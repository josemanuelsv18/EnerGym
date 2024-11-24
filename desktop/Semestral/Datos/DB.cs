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
            string cadenaConexion = "Data Source=localhost;Initial Catalog=EnerGym;Integrated Security=True;TrustServerCertificate=True;";
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
                cmd.CommandText = "INSERT INTO Accesos (usuarioId, tipo, estado) VALUES (@usuarioID, @tipo, @estado);";

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
            finally { cmd.Connection.Close(); }
        }

        public bool RegistrarSalida(int id, string tipo, string estado)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO Accesos (usuarioId, tipo, estado) VALUES (@usuarioID, @tipo, @estado);";

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
                cmd.CommandText =
                    "INSERT INTO Reservas(usuarioId, fechaReserva, horaReserva, estado) " +
                    "VALUES (@usuarioId, @fechaReserva, @horaReserva, 'Pendiente')";
                cmd.Parameters.Add(new SqlParameter("@usuarioId", reserva.usuarioId));
                cmd.Parameters.Add(new SqlParameter("@fechaReserva", reserva.fechaReserva));
                cmd.Parameters.Add(new SqlParameter("@horaReserva", reserva.horaReserva));

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

        public int GimnasioCapacidadActual()
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

        public string GimnasioCapacidadPorcentual()
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
                cmd.CommandText = "SELECT * FROM Clases WHERE estado = 0";

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
                            id = Convert.ToInt32(row["id"].ToString()),
                            nombre = row["nombre"].ToString(),
                            entrenador = row["entrenador"].ToString(),
                            estado = Convert.ToInt32(row["estado"].ToString()),
                            horarioInicio = Convert.ToDateTime(row["horarioInicio"]),
                            horarioFinal = Convert.ToDateTime(row["horarioFinal"]),
                            createddAt = Convert.ToDateTime(row["created_at"]),
                            updatedAt = Convert.ToDateTime(row["updated_at"])
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
                cmd.CommandText = "SELECT TOP 1 id, created_at FROM Accesos WHERE usuarioId = @usuarioID AND tipo = 'Entrada' AND estado = 'Generado' AND created_at >= DATEADD(HOUR, -5, GETDATE()) ORDER BY created_at DESC";
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

        public List<Horarios> ObtenerHorarios()
        {
            List<Horarios> horarios = new List<Horarios>();
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = 
                    "SELECT " +
                    "    CASE dias " +
                    "        WHEN 1 THEN 'Lunes'" +
                    "        WHEN 2 THEN 'Martes'" +
                    "        WHEN 3 THEN 'Miércoles'" +
                    "        WHEN 4 THEN 'Jueves'" +
                    "        WHEN 5 THEN 'Viernes'" +
                    "        WHEN 6 THEN 'Sábado'" +
                    "        WHEN 7 THEN 'Domingo'" +
                    "        WHEN 8 THEN 'Feriados'" +
                    "    END AS Dia," +
                    "    CONVERT(NVARCHAR(5), horaInicio) AS horaInicio," +
                    "    CONVERT(NVARCHAR(5), horaFinal) AS horaFinal" +
                    " FROM Horarios" +
                    " WHERE activo = 1;";

                cmd.Connection.Open();
                ds = new DataSet();
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);

                foreach (DataTable table in ds.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        var horario = new Horarios()
                        {
                            dias = row["Dia"].ToString(),
                            horaInicio = row["horaInicio"].ToString(),
                            horaFinal = row["horaFinal"].ToString()
                        };
                        horarios.Add(horario);
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
            return horarios;
        }

        public bool EsHoraValido()
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT 1 FROM Horarios WHERE activo = 1 AND dias = @dia AND @horaActual BETWEEN horaInicio AND horaFinal;";

                int diaSemana = (int)DateTime.Now.DayOfWeek;
                int diaAjustado;
                if (diaSemana == 0)
                {
                    diaAjustado = 7;
                }
                else
                {
                    diaAjustado = diaSemana;
                }

                string horaActual = DateTime.Now.ToString("HH:mm");
                cmd.Parameters.Add(new SqlParameter("@dia", diaAjustado));
                cmd.Parameters.Add(new SqlParameter("@horaActual", horaActual));

                cmd.Connection.Open(); 
                var result = cmd.ExecuteScalar();
                return result != null;
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

        #region UPDATES
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

        public int ActualizarEstado(int id, string nuevoEstado)
        {
            try
            {
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