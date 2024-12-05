using Semestral.Models;
using System;
using System.Data;
using Microsoft.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;
using ZXing;
using Microsoft.AspNetCore.Http.HttpResults;

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
        
        public bool RegistrarEntrada(int id, string tipo, string codigoQR)
        {
            try {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = 
                    "INSERT INTO Accesos (usuarioId, tipoUsuario, tipo) VALUES (@usuarioID,  " +
                    "CASE  " +
                    "   WHEN LEN(@codigoQR) = 3 THEN 'Dueño' " +
                    "   WHEN LEN(@codigoQR) = 4 AND LEFT(@codigoQR,1) = 'P' THEN 'Invitado' " +
                    "   END " +
                    ",@tipo);";

                cmd.Parameters.Add(new SqlParameter("@usuarioID", id));
                cmd.Parameters.Add(new SqlParameter("@tipo", tipo));
                cmd.Parameters.Add(new SqlParameter("@codigoQR", codigoQR));

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

        public bool RegistrarSalida(int id, string tipo, string codigoQR)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText =
                    "INSERT INTO Accesos (usuarioId, tipoUsuario, tipo) VALUES (@usuarioID,  " +
                    "CASE  " +
                    "   WHEN LEN(@codigoQR) = 3 THEN 'Dueño' " +
                    "   WHEN LEN(@codigoQR) = 4 AND LEFT(@codigoQR,1) = 'P' THEN 'Invitado' " +
                    "   END " +
                    ",@tipo);";

                cmd.Parameters.Add(new SqlParameter("@usuarioID", id));
                cmd.Parameters.Add(new SqlParameter("@tipo", tipo));
                cmd.Parameters.Add(new SqlParameter("@codigoQR", codigoQR));

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

        public bool CrearClaseGrupal(ClaseRequest clase)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "InsertarClaseGrupal";
                cmd.Parameters.Add(new SqlParameter("@nombreClase", clase.nombreClase));
                cmd.Parameters.Add(new SqlParameter("@nombreEntrenador", clase.nombreEntrenador));
                cmd.Parameters.Add(new SqlParameter("@horarioInicio", clase.horarioInicio));
                cmd.Parameters.Add(new SqlParameter("@horarioFinal", clase.horarioFinal));
                cmd.Parameters.Add(new SqlParameter("@estado", clase.estado));

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

        public List<Reserva> BuscarReservas(string nombre, string estado)
        {
            List<Reserva> reservas = new List<Reserva>();
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;

                string query = "SELECT r.id, u.nombre + ' ' + u.apellido AS nombre, r.fechaReserva, r.estado, r.created_at, r.updated_at FROM Reservas AS r JOIN Usuarios AS u ON r.usuarioId = u.id WHERE 1 = 1";
                if (!string.IsNullOrEmpty(nombre))
                {
                    query += " AND nombre = @nombre";
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                }
                if (!string.IsNullOrEmpty(estado))
                {
                    query += " AND r.estado = @estado";
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new SqlParameter("@estado", estado));
                }
                if (!string.IsNullOrEmpty(nombre) && !string.IsNullOrEmpty(estado))
                {
                    query += " AND r.estado = @estado AND nombre = @nombre";
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                    cmd.Parameters.Add(new SqlParameter("@estado", estado));
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
                        var reserva = new Reserva()
                        {
                            id = Convert.ToInt32(row["id"].ToString()),
                            nombre = row["nombre"].ToString(),
                            fechaReserva = Convert.ToDateTime(row["fechaReserva"].ToString()),
                            estado = row["estado"].ToString(),
                            createdAt = Convert.ToDateTime(row["created_at"].ToString()),
                            updatedAt = Convert.ToDateTime(row["updated_at"].ToString())
                        };
                        reservas.Add(reserva);
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
            return reservas;
        }

        public List<Clase> BuscarClases(string estado, string entrenador)
        {
            List<Clase> clases = new List<Clase>();
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;

                string query =
                    "SELECT " +
                    "   c.id, " +
                    "   c.nombre AS clase, " +
                    "   e.nombre + ' ' + e.apellido AS entrenador, " +
                    "   CASE WHEN c.estado = 0 THEN 'Pendiente' " +
                    "        WHEN c.estado = 1 THEN 'En Curso' " +
                    "        WHEN c.estado = 2 THEN 'Finalizada' END AS estado, " +
                    "   CONVERT(NVARCHAR(25), c.horarioInicio) AS fechaInicio, " +
                    "   CONVERT(NVARCHAR(25), c.horarioFinal) AS fechaFinal " +
                    "FROM " +
                    "   Clases c " +
                    "JOIN " +
                    "   Entrenadores e ON c.entrenador = e.id " +
                    "WHERE " +
                    "   1 = 1 ";

                // Filtrar por estado si es necesario
                if (!string.IsNullOrEmpty(estado))
                {
                    if (estado == "Pendiente")
                    {
                        query += " AND c.estado = 0";
                    }
                    else if (estado == "En Curso")
                    {
                        query += " AND c.estado = 1";
                    }
                    else if (estado == "Finalizada")
                    {
                        query += " AND c.estado = 2";
                    }
                }

                // Filtrar por entrenador si es necesario
                if (!string.IsNullOrEmpty(entrenador))
                {
                    query += " AND e.nombre + ' ' + e.apellido = @entrenador";
                    cmd.Parameters.Add(new SqlParameter("@entrenador", entrenador));
                }

                // Agregar el ORDER BY
                query += " ORDER BY c.horarioInicio";

                cmd.CommandText = query;

                cmd.Connection.Open();
                ds = new DataSet();
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);

                // Llenar la lista de clases
                foreach (DataTable table in ds.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        var clase = new Clase()
                        {
                            id = Convert.ToInt32(row["id"].ToString()),
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
            finally
            {
                cmd.Connection.Close();
            }
            return clases;
        }

        public List<EntrenadorNombre> ObtenerEntrenadoresNombre()
        {
            List<EntrenadorNombre> entrenadores = new List<EntrenadorNombre>();
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "SELECT nombre + ' ' + apellido AS nombre FROM Entrenadores WHERE estado = 'Activo';";

                cmd.Connection.Open();
                ds = new DataSet();
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);

                foreach (DataTable table in ds.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        var entrenador = new EntrenadorNombre()
                        {
                            nombre = row["nombre"].ToString()
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

        public bool ExisteClase(int claseID)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT COUNT(*) FROM Clases WHERE id = @id";
                cmd.Parameters.Add(new SqlParameter("@id", claseID));

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

        public string GimnasioOcupacionTexto()
        {
            string capacidad;
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT CONCAT(ocupacionActual, ' de ', capacidadMaxima, ' personas en el gimnasio') FROM Gimnasio";

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

        public string GimnasioOcupacionPorcentual()
        {
            string capacidad;
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT CAST(ROUND((CAST(ocupacionActual AS FLOAT) / capacidadMaxima) * 100, 0) AS VARCHAR(10)) + '%' FROM Gimnasio";

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
                            id = Convert.ToInt32(row["id"].ToString()),
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
                cmd.CommandText = "SELECT * FROM Usuarios WHERE QRcodigo = @QRcodigo OR QRinvitado = @QRcodigo;";
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
                cmd.CommandText =
                    "SELECT TOP 1 id, created_at " +
                    "FROM Accesos " +
                    "WHERE usuarioId = @usuarioID " +
                    "    AND tipo = 'Salida' " +
                    "    AND tipoUsuario = 'Dueño' " +
                    "    AND created_at > ( " +
                    "        SELECT TOP 1 created_at " +
                    "        FROM Accesos " +
                    "        WHERE usuarioId = @usuarioID " +
                    "            AND tipo = 'Entrada' " +
                    "        ORDER BY created_at DESC " +
                    "    ) " +
                    "ORDER BY created_at DESC;";
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

        public int ValidarInvitadoSalida(int usuarioID)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText =
                    "SELECT TOP 1 id, created_at " +
                    "FROM Accesos " +
                    "WHERE usuarioId = @usuarioID " +
                    "    AND tipo = 'Salida' " +
                    "    AND tipoUsuario = 'Invitado' " +
                    "    AND created_at > ( " +
                    "        SELECT TOP 1 created_at " +
                    "        FROM Accesos " +
                    "        WHERE usuarioId = @usuarioID " +
                    "            AND tipo = 'Entrada' " +
                    "        ORDER BY created_at DESC " +
                    "    ) " +
                    "ORDER BY created_at DESC;";
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
                cmd.CommandText = "SELECT COUNT(*) FROM Accesos WHERE usuarioID = @usuarioID AND tipo = 'Entrada' AND NOT EXISTS (SELECT 1 FROM Accesos WHERE usuarioID = @usuarioID AND tipo = 'Salida' AND created_at > (SELECT MAX(created_at) FROM Accesos WHERE usuarioID = @usuarioID AND tipo = 'Entrada'));";
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

        public bool ValidarInvitadoEntradaAvtiva(int usuarioID, string tipoUsuario)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT COUNT(*) FROM Accesos WHERE usuarioID = @usuarioID AND tipo = 'Entrada' AND tipoUsuario = @tipoUsuario AND NOT EXISTS (SELECT 1 FROM Accesos WHERE usuarioID = @usuarioID AND tipo = 'Salida' AND tipoUsuario = @tipoUsuario AND created_at > (SELECT MAX(created_at) FROM Accesos WHERE usuarioID = @usuarioID AND tipo = 'Entrada' AND tipoUsuario = @tipoUsuario));";
                cmd.Parameters.Add(new SqlParameter("@usuarioID", usuarioID));
                cmd.Parameters.Add(new SqlParameter("@tipoUsuario", tipoUsuario));

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
                cmd.CommandText = "SELECT 1 FROM Reservas WHERE usuarioId = @usuarioId AND estado = 'Activa' AND fechaReserva = CAST(GETDATE() AS DATE)";
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

        public int ValidarReservaPorId(int usuarioId, DateTime fechaReserva)
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

        public int BuscarClasePorNombreHorarioYEstado(string nombreClase, string horarioInicio, string horarioFinal)
        {
            int filas;
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT 1 FROM Clases WHERE nombre = @nombreClase AND horarioInicio = @horarioInicio AND horarioFinal = @horarioFinal AND estado = 0";
                cmd.Parameters.Add(new SqlParameter("@nombreClase", nombreClase));
                cmd.Parameters.Add(new SqlParameter("@horarioInicio", horarioInicio));
                cmd.Parameters.Add(new SqlParameter("@horarioFinal", horarioFinal));
                
                cmd.Connection.Open();

                filas = Convert.ToInt32(cmd.ExecuteScalar());
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

        public int ActualizarEstadoClase(int id, string nuevoEstado)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = 
                    "UPDATE Clases SET " +
                    "estado = CASE " +
                    "            WHEN @estado = 'Pendiente' THEN 0 " +
                    "            WHEN @estado = 'En Curso' THEN 1 " +
                    "            WHEN @estado = 'Finalizada' THEN 2 " +
                    "         END " +
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
                cmd.CommandText = "UPDATE Reservas SET estado = 'Cumplida' WHERE usuarioId = @usuarioId AND fechaReserva = CAST(GETDATE() AS DATE)";
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

        public int BorrarClase(int id)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM Clases WHERE id = " + id;

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