using CadPasteleria;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClnPasteleria
{
    public static class ClienteCln
    {
        public static int insertar(Cliente cliente)
        {
            Normalizar(cliente);
            ValidarDatos(cliente);

            using (var context = new LabPasteleriaEntities())
            {
                ValidarUnicos(context, cliente, 0);
                cliente.fechaRegistro = DateTime.Now;
                cliente.estado = EstadoRegistro.Activo;
                context.Cliente.Add(cliente);
                context.SaveChanges();
                return cliente.id;
            }
        }

        public static int actualizar(Cliente cliente)
        {
            Normalizar(cliente);
            ValidarDatos(cliente);

            using (var context = new LabPasteleriaEntities())
            {
                var existe = context.Cliente.FirstOrDefault(c => c.id == cliente.id && c.estado != EstadoRegistro.Eliminado);
                if (existe == null) throw new ReglaNegocioException("El cliente no existe.");

                ValidarUnicos(context, cliente, cliente.id);

                existe.cedulaIdentidad = cliente.cedulaIdentidad;
                existe.nombre = cliente.nombre;
                existe.apellido = cliente.apellido;
                existe.celular = cliente.celular;
                existe.email = cliente.email;
                existe.direccion = cliente.direccion;
                existe.usuarioRegistro = cliente.usuarioRegistro;
                return context.SaveChanges();
            }
        }

        public static int eliminar(int id, string usuarioRegistro)
        {
            using (var context = new LabPasteleriaEntities())
            {
                var existe = context.Cliente.Find(id);
                if (existe == null || existe.estado == EstadoRegistro.Eliminado)
                    throw new ReglaNegocioException("El cliente no existe.");

                existe.estado = EstadoRegistro.Eliminado;
                existe.usuarioRegistro = usuarioRegistro;
                return context.SaveChanges();
            }
        }

        public static Cliente obtenerUno(int id)
        {
            using (var context = new LabPasteleriaEntities())
            {
                return context.Cliente.Find(id);
            }
        }

        public static List<Cliente> listar()
        {
            using (var context = new LabPasteleriaEntities())
            {
                return context.Cliente.Where(x => x.estado != EstadoRegistro.Eliminado).ToList();
            }
        }

        public static List<paListarCliente_Result> listarPa(string parametro)
        {
            using (var context = new LabPasteleriaEntities())
            {
                return context.paListarCliente(parametro).ToList();
            }
        }

        public static List<paBuscarCliente_Result> buscarPa(string parametro)
        {
            using (var context = new LabPasteleriaEntities())
            {
                return context.paBuscarCliente(parametro).ToList();
            }
        }

        public static bool existeCedula(string cedula, int excluirId = 0)
        {
            cedula = Validacion.TextoONulo(cedula);
            if (cedula == null) return false;
            using (var context = new LabPasteleriaEntities())
            {
                return context.Cliente.Any(c => c.cedulaIdentidad == cedula && c.estado != EstadoRegistro.Eliminado && c.id != excluirId);
            }
        }

        public static bool existeEmail(string email, int excluirId = 0)
        {
            email = Validacion.TextoONulo(email);
            if (email == null) return false;
            using (var context = new LabPasteleriaEntities())
            {
                return context.Cliente.Any(c => c.email == email && c.estado != EstadoRegistro.Eliminado && c.id != excluirId);
            }
        }

        // ------------------------------------------------------------------------------------------

        /// <summary>Cédula y e-mail vacíos se guardan como NULL (con "" el segundo cliente sin dato violaba el índice único).</summary>
        private static void Normalizar(Cliente c)
        {
            c.cedulaIdentidad = Validacion.TextoONulo(c.cedulaIdentidad);
            c.email = Validacion.TextoONulo(c.email);
            c.nombre = (c.nombre ?? "").Trim();
            c.apellido = (c.apellido ?? "").Trim();
            c.celular = (c.celular ?? "").Trim();
            c.direccion = (c.direccion ?? "").Trim();
        }

        private static void ValidarDatos(Cliente c)
        {
            if (c.nombre.Length == 0) throw new ReglaNegocioException("El nombre del cliente es obligatorio.");
            if (c.apellido.Length == 0) throw new ReglaNegocioException("El apellido del cliente es obligatorio.");
            if (!Validacion.EsTelefonoValido(c.celular))
                throw new ReglaNegocioException("El celular/teléfono debe tener 7 u 8 dígitos (sólo números).");
            if (c.direccion.Length == 0) throw new ReglaNegocioException("La dirección es obligatoria.");
            if (c.email != null && !Validacion.EsEmailValido(c.email))
                throw new ReglaNegocioException("El e-mail no tiene un formato válido.");
        }

        private static void ValidarUnicos(LabPasteleriaEntities context, Cliente c, int excluirId)
        {
            if (c.cedulaIdentidad != null &&
                context.Cliente.Any(x => x.cedulaIdentidad == c.cedulaIdentidad && x.estado != EstadoRegistro.Eliminado && x.id != excluirId))
                throw new ReglaNegocioException("Ya existe un cliente con esa Cédula de Identidad.");

            if (c.email != null &&
                context.Cliente.Any(x => x.email == c.email && x.estado != EstadoRegistro.Eliminado && x.id != excluirId))
                throw new ReglaNegocioException("Ya existe un cliente con ese e-mail.");
        }
    }
}
