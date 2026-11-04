using CadPasteleria;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ClnPasteleria
{
    public static class EmpleadoCln
    {
        /// <summary>
        /// Registra el empleado y, si se indica, su usuario, en UNA sola transacción
        /// (antes: dos SaveChanges separados; si el segundo fallaba quedaba un empleado sin usuario).
        /// claveTemporal: contraseña inicial generada (null si no se creó usuario).
        /// </summary>
        public static Empleado insertar(Empleado empleado, string usuario, string rol, string usuarioRegistro, out string claveTemporal)
        {
            claveTemporal = null;
            Normalizar(empleado);
            ValidarDatos(empleado);

            using (var context = new LabPasteleriaEntities())
            {
                if (ExisteCedula(context, empleado.cedulaIdentidad, 0))
                    throw new ReglaNegocioException("Ya existe un empleado con esa Cédula de Identidad.");

                empleado.usuarioRegistro = usuarioRegistro;
                empleado.fechaRegistro = DateTime.Now;
                empleado.estado = EstadoRegistro.Activo;

                if (!string.IsNullOrWhiteSpace(usuario))
                {
                    claveTemporal = CrearUsuario(context, empleado, usuario, rol, usuarioRegistro, 0);
                }

                context.Empleado.Add(empleado);
                context.SaveChanges();
                return empleado;
            }
        }

        /// <summary>
        /// Actualiza el empleado y su usuario. NO modifica la contraseña de un usuario existente
        /// (antes se reiniciaba a "hola123" en cada edición).
        /// </summary>
        public static void actualizar(Empleado empleado, string usuario, string rol, string usuarioRegistro, out string claveTemporal)
        {
            claveTemporal = null;
            Normalizar(empleado);
            ValidarDatos(empleado);

            using (var context = new LabPasteleriaEntities())
            {
                var existe = context.Empleado.Include(e => e.Usuario)
                    .FirstOrDefault(e => e.id == empleado.id && e.estado != EstadoRegistro.Eliminado);
                if (existe == null) throw new ReglaNegocioException("El empleado no existe.");

                if (ExisteCedula(context, empleado.cedulaIdentidad, empleado.id))
                    throw new ReglaNegocioException("Ya existe otro empleado con esa Cédula de Identidad.");

                existe.cedulaIdentidad = empleado.cedulaIdentidad;
                existe.nombre = empleado.nombre;
                existe.apellidoPaterno = empleado.apellidoPaterno;
                existe.apellidoMaterno = empleado.apellidoMaterno;
                existe.fechaNacimiento = empleado.fechaNacimiento;
                existe.direccion = empleado.direccion;
                existe.celular = empleado.celular;
                existe.usuarioRegistro = usuarioRegistro;

                if (!string.IsNullOrWhiteSpace(usuario))
                {
                    usuario = usuario.Trim();
                    var actual = existe.Usuario.FirstOrDefault(u => u.estado != EstadoRegistro.Eliminado);
                    if (actual == null)
                    {
                        claveTemporal = CrearUsuario(context, existe, usuario, rol, usuarioRegistro, 0);
                    }
                    else
                    {
                        var error = Validacion.ValidarNombreUsuario(usuario);
                        if (error != null) throw new ReglaNegocioException(error);
                        if (UsuarioCln.existeUsuario(usuario, actual.id))
                            throw new ReglaNegocioException("Ese nombre de usuario ya está en uso.");
                        if (!Roles.Todos.Contains(rol)) throw new ReglaNegocioException("Rol no válido.");
                        if (Roles.EsAdministrador(actual.role) && rol != Roles.Administrador
                            && !UsuarioCln.HayOtroAdministradorActivo(context, actual.id))
                            throw new ReglaNegocioException("Debe existir al menos un administrador activo.");

                        actual.usuario1 = usuario;
                        actual.role = rol;
                        actual.usuarioRegistro = usuarioRegistro;
                    }
                }
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Baja lógica del empleado y de sus usuarios (antes el usuario seguía pudiendo ingresar).
        /// idEmpleadoActual: empleado de la sesión (no puede darse de baja a sí mismo).
        /// </summary>
        public static int eliminar(int id, string usuarioRegistro, int idEmpleadoActual = 0)
        {
            using (var context = new LabPasteleriaEntities())
            {
                var existe = context.Empleado.Include(e => e.Usuario).FirstOrDefault(e => e.id == id);
                if (existe == null || existe.estado == EstadoRegistro.Eliminado)
                    throw new ReglaNegocioException("El empleado no existe.");
                if (id == idEmpleadoActual)
                    throw new ReglaNegocioException("No puede dar de baja a su propio empleado.");

                foreach (var u in existe.Usuario.Where(x => x.estado != EstadoRegistro.Eliminado))
                {
                    if (Roles.EsAdministrador(u.role) && !UsuarioCln.HayOtroAdministradorActivo(context, u.id))
                        throw new ReglaNegocioException("Debe existir al menos un administrador activo.");
                    u.estado = EstadoRegistro.Eliminado;
                    u.usuarioRegistro = usuarioRegistro;
                }

                existe.estado = EstadoRegistro.Eliminado;
                existe.usuarioRegistro = usuarioRegistro;
                return context.SaveChanges();
            }
        }

        public static Empleado obtenerUno(int id)
        {
            using (var context = new LabPasteleriaEntities())
            {
                return context.Empleado.Include(e => e.Usuario).FirstOrDefault(e => e.id == id);
            }
        }

        public static List<Empleado> listar()
        {
            using (var context = new LabPasteleriaEntities())
            {
                return context.Empleado.Where(x => x.estado != EstadoRegistro.Eliminado).ToList();
            }
        }

        public static List<paListarEmpleados_Result> listarPa(string parametro)
        {
            using (var context = new LabPasteleriaEntities())
            {
                return context.paListarEmpleados(parametro).ToList();
            }
        }

        public static bool existeCedula(string cedula, int excluirId = 0)
        {
            using (var context = new LabPasteleriaEntities())
            {
                return ExisteCedula(context, (cedula ?? "").Trim(), excluirId);
            }
        }

        // ------------------------------------------------------------------------------------------

        private static bool ExisteCedula(LabPasteleriaEntities context, string cedula, int excluirId)
        {
            return context.Empleado.Any(e => e.cedulaIdentidad == cedula
                                             && e.estado != EstadoRegistro.Eliminado
                                             && e.id != excluirId);
        }

        private static string CrearUsuario(LabPasteleriaEntities context, Empleado empleado, string usuario,
                                           string rol, string usuarioRegistro, int excluirId)
        {
            usuario = usuario.Trim();
            var error = Validacion.ValidarNombreUsuario(usuario);
            if (error != null) throw new ReglaNegocioException(error);
            if (!Roles.Todos.Contains(rol)) throw new ReglaNegocioException("Rol no válido.");
            if (context.Usuario.Any(u => u.usuario1 == usuario && u.id != excluirId))
                throw new ReglaNegocioException("Ese nombre de usuario ya está en uso.");

            var temporal = Seguridad.GenerarClaveTemporal();
            empleado.Usuario.Add(new Usuario
            {
                usuario1 = usuario,
                role = rol,
                clave = Seguridad.GenerarHash(temporal),
                usuarioRegistro = usuarioRegistro,
                fechaRegistro = DateTime.Now,
                estado = EstadoRegistro.Activo
            });
            return temporal;
        }

        private static void Normalizar(Empleado e)
        {
            e.cedulaIdentidad = (e.cedulaIdentidad ?? "").Trim();
            e.nombre = (e.nombre ?? "").Trim();
            e.apellidoPaterno = Validacion.TextoONulo(e.apellidoPaterno);
            e.apellidoMaterno = Validacion.TextoONulo(e.apellidoMaterno);
            e.direccion = (e.direccion ?? "").Trim();
        }

        private static void ValidarDatos(Empleado e)
        {
            if (e.cedulaIdentidad.Length == 0) throw new ReglaNegocioException("La Cédula de Identidad es obligatoria.");
            if (e.nombre.Length == 0) throw new ReglaNegocioException("El nombre es obligatorio.");
            if (e.direccion.Length == 0) throw new ReglaNegocioException("La dirección es obligatoria.");
            if (!Validacion.EsMayorDeEdad(e.fechaNacimiento))
                throw new ReglaNegocioException("El empleado debe ser mayor de edad (18 años o más).");
            if (e.celular < 10000000L || e.celular > 99999999L)
                throw new ReglaNegocioException("El celular debe tener exactamente 8 dígitos.");
        }
    }
}
