using CadPasteleria;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClnPasteleria
{
    public static class ProductoCln
    {
        public static int insertar(Producto producto)
        {
            Normalizar(producto);
            ValidarDatos(producto);

            using (var context = new LabPasteleriaEntities())
            {
                ValidarUnicos(context, producto, 0);
                producto.fechaRegistro = DateTime.Now;
                producto.estado = EstadoRegistro.Activo;
                context.Producto.Add(producto);
                context.SaveChanges();
                return producto.id;
            }
        }

        public static int actualizar(Producto producto)
        {
            Normalizar(producto);
            ValidarDatos(producto);

            using (var context = new LabPasteleriaEntities())
            {
                var existe = context.Producto.FirstOrDefault(p => p.id == producto.id && p.estado != EstadoRegistro.Eliminado);
                if (existe == null) throw new ReglaNegocioException("El producto no existe.");

                ValidarUnicos(context, producto, producto.id);

                existe.codigo = producto.codigo;
                existe.nombre = producto.nombre;
                existe.unidadMedida = producto.unidadMedida;
                existe.precio = producto.precio;
                existe.tipo = producto.tipo;
                existe.descripcion = producto.descripcion;
                existe.usuarioRegistro = producto.usuarioRegistro;
                return context.SaveChanges();
            }
        }

        public static int eliminar(int id, string usuarioRegistro)
        {
            using (var context = new LabPasteleriaEntities())
            {
                var existe = context.Producto.Find(id);
                if (existe == null || existe.estado == EstadoRegistro.Eliminado)
                    throw new ReglaNegocioException("El producto no existe.");

                existe.estado = EstadoRegistro.Eliminado;
                existe.usuarioRegistro = usuarioRegistro;
                return context.SaveChanges();
            }
        }

        public static Producto obtenerUno(int id)
        {
            using (var context = new LabPasteleriaEntities())
            {
                return context.Producto.Find(id);
            }
        }

        /// <summary>Productos ACTIVOS (los inactivos/eliminados no se pueden vender).</summary>
        public static List<Producto> listar()
        {
            using (var context = new LabPasteleriaEntities())
            {
                return context.Producto.Where(x => x.estado == EstadoRegistro.Activo).OrderBy(x => x.nombre).ToList();
            }
        }

        public static List<paListarProductos_Result> listarPa(string parametro)
        {
            using (var context = new LabPasteleriaEntities())
            {
                return context.paListarProductos(parametro).ToList();
            }
        }

        public static bool existeCodigo(string codigo, int excluirId = 0)
        {
            codigo = (codigo ?? "").Trim();
            using (var context = new LabPasteleriaEntities())
            {
                return context.Producto.Any(p => p.codigo == codigo && p.estado != EstadoRegistro.Eliminado && p.id != excluirId);
            }
        }

        public static bool existeNombre(string nombre, int excluirId = 0)
        {
            nombre = (nombre ?? "").Trim();
            using (var context = new LabPasteleriaEntities())
            {
                return context.Producto.Any(p => p.nombre == nombre && p.estado != EstadoRegistro.Eliminado && p.id != excluirId);
            }
        }

        // ------------------------------------------------------------------------------------------

        private static void Normalizar(Producto p)
        {
            p.codigo = (p.codigo ?? "").Trim().ToUpperInvariant();
            p.nombre = (p.nombre ?? "").Trim();
            p.unidadMedida = (p.unidadMedida ?? "").Trim();
            p.tipo = (p.tipo ?? "").Trim();
            p.descripcion = Validacion.TextoONulo(p.descripcion);
        }

        private static void ValidarDatos(Producto p)
        {
            if (p.codigo.Length == 0) throw new ReglaNegocioException("El código es obligatorio.");
            if (p.codigo.Length > 20) throw new ReglaNegocioException("El código no puede superar los 20 caracteres.");
            if (p.nombre.Length == 0) throw new ReglaNegocioException("El nombre es obligatorio.");
            if (p.unidadMedida.Length == 0) throw new ReglaNegocioException("La unidad de medida es obligatoria.");
            if (p.tipo.Length == 0) throw new ReglaNegocioException("El tipo de producto es obligatorio.");
            if (p.precio <= 0) throw new ReglaNegocioException("El precio debe ser mayor a cero.");
        }

        private static void ValidarUnicos(LabPasteleriaEntities context, Producto p, int excluirId)
        {
            if (context.Producto.Any(x => x.codigo == p.codigo && x.estado != EstadoRegistro.Eliminado && x.id != excluirId))
                throw new ReglaNegocioException("Ya existe un producto con ese código.");
            if (context.Producto.Any(x => x.nombre == p.nombre && x.estado != EstadoRegistro.Eliminado && x.id != excluirId))
                throw new ReglaNegocioException("Ya existe un producto con ese nombre.");
        }
    }
}
