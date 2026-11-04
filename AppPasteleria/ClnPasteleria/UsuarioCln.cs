using CadPasteleria;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ClnPasteleria
{
    public static class UsuarioCln
    {
        /// <summary>
        /// Valida credenciales. Sólo entran usuarios ACTIVOS cuyo empleado también esté activo.
        /// Si la clave estaba guardada con el esquema antiguo (AES), se migra a PBKDF2 en este momento.
        /// </summary>
        public static Usuario validar(string usuario, string clave)
        {
            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrEmpty(clave)) return null;
            usuario = usuario.Trim();

            using (var context = new LabPasteleriaEntities())
            {
                var existente = context.Usuario
                    .Include(u => u.Empleado)
                    .FirstOrDefault(u => u.usuario1 == usuario
                                         && u.estado == EstadoRegistro.Activo
                                         && u.Empleado.estado == EstadoRegistro.Activo);

                if (existente == null || !Seguridad.Verificar(clave, existente.clave)) return null;

                if (!Seguridad.EsFormatoActual(existente.clave))
                {
                    existente.clave = Seguridad.GenerarHash(clave);
                    context.SaveChanges();
                }
                return existente;
            }
        }

        public static void cambiarClave(int idUsuario, string claveActual, string claveNueva, string usuarioRegistro)
        {
            var error = Seguridad.ValidarPolitica(claveNueva);
            if (error != null) throw new ReglaNegocioException(error);

            using (var context = new LabPasteleriaEntities())
            {
                var u = context.Usuario.Find(idUsuario);
                if (u == null || u.estado != EstadoRegistro.Activo)
                    throw new ReglaNegocioException("El usuario no existe o está inactivo.");
                if (!Seguridad.Verificar(claveActual, u.clave))
                    throw new ReglaNegocioException("La contraseña actual no es correcta.");
                if (Seguridad.Verificar(claveNueva, u.clave))
                    throw new ReglaNegocioException("La nueva contraseña debe ser distinta de la actual.");

                u.clave = Seguridad.GenerarHash(claveNueva);
                u.usuarioRegistro = usuarioRegistro;
                context.SaveChanges();
            }
        }

        /// <summary>Genera una contraseña temporal para el usuario y la devuelve (en texto claro, una sola vez).</summary>
        public static string restablecerClave(int idUsuario, string usuarioRegistro)
        {
            using (var context = new LabPasteleriaEntities())
            {
                var u = context.Usuario.Find(idUsuario);
                if (u == null || u.estado == EstadoRegistro.Eliminado)
                    throw new ReglaNegocioException("El usuario no existe.");

                var temporal = Seguridad.GenerarClaveTemporal();
                u.clave = Seguridad.GenerarHash(temporal);
                u.usuarioRegistro = usuarioRegistro;
                context.SaveChanges();
                return temporal;
            }
        }

        public static List<UsuarioResumen> listarResumen()
        {
            using (var context = new LabPasteleriaEntities())
            {
                return context.Usuario
                    .Where(u => u.estado != EstadoRegistro.Eliminado)
                    .OrderBy(u => u.usuario1)
                    .Select(u => new UsuarioResumen
                    {
                        Id = u.id,
                        IdEmpleado = u.idEmpleado,
                        Usuario = u.usuario1,
                        Empleado = u.Empleado.nombre + " " + (u.Empleado.apellidoPaterno ?? "") + " " + (u.Empleado.apellidoMaterno ?? ""),
                        Rol = u.role,
                        EstadoCodigo = u.estado,
                        FechaRegistro = u.fechaRegistro
                    })
                    .ToList();
            }
        }

        public static void cambiarEstado(int idUsuario, bool activo, int idUsuarioActual, string usuarioRegistro)
        {
            using (var context = new LabPasteleriaEntities())
            {
                var u = context.Usuario.Find(idUsuario);
                if (u == null || u.estado == EstadoRegistro.Eliminado)
                    throw new ReglaNegocioException("El usuario no existe.");

                if (!activo)
                {
                    if (idUsuario == idUsuarioActual)
                        throw new ReglaNegocioException("No puede desactivar su propio usuario.");
                    if (Roles.EsAdministrador(u.role) && !HayOtroAdministradorActivo(context, idUsuario))
                        throw new ReglaNegocioException("Debe existir al menos un administrador activo.");
                }

                u.estado = activo ? EstadoRegistro.Activo : EstadoRegistro.Inactivo;
                u.usuarioRegistro = usuarioRegistro;
                context.SaveChanges();
            }
        }

        public static void cambiarRol(int idUsuario, string rol, string usuarioRegistro)
        {
            if (!Roles.Todos.Contains(rol)) throw new ReglaNegocioException("Rol no válido.");

            using (var context = new LabPasteleriaEntities())
            {
                var u = context.Usuario.Find(idUsuario);
                if (u == null || u.estado == EstadoRegistro.Eliminado)
                    throw new ReglaNegocioException("El usuario no existe.");

                if (Roles.EsAdministrador(u.role) && rol != Roles.Administrador
                    && !HayOtroAdministradorActivo(context, idUsuario))
                    throw new ReglaNegocioException("Debe existir al menos un administrador activo.");

                u.role = rol;
                u.usuarioRegistro = usuarioRegistro;
                context.SaveChanges();
            }
        }

        /// <summary>¿El nombre de usuario ya está en uso? (la columna es única, incluso entre eliminados).</summary>
        public static bool existeUsuario(string usuario, int excluirId = 0)
        {
            usuario = (usuario ?? "").Trim();
            using (var context = new LabPasteleriaEntities())
            {
                return context.Usuario.Any(u => u.usuario1 == usuario && u.id != excluirId);
            }
        }

        public static Usuario obtenerUno(int id)
        {
            using (var context = new LabPasteleriaEntities())
            {
                return context.Usuario.Include(u => u.Empleado).FirstOrDefault(x => x.id == id);
            }
        }

        internal static bool HayOtroAdministradorActivo(LabPasteleriaEntities context, int idExcluido)
        {
            return context.Usuario.Any(u => u.id != idExcluido
                                            && u.estado == EstadoRegistro.Activo
                                            && u.role == Roles.Administrador
                                            && u.Empleado.estado == EstadoRegistro.Activo);
        }
    }
}
