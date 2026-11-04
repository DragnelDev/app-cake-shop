using System;
using System.Data.SqlClient;

namespace ClnPasteleria
{
    /// <summary>Convierte excepciones técnicas (EF, SQL Server) en mensajes entendibles para el usuario.</summary>
    public static class Errores
    {
        public static string Mensaje(Exception ex)
        {
            if (ex == null) return "Ocurrió un error desconocido.";
            if (ex is ReglaNegocioException) return ex.Message;

            // Recorre la cadena de excepciones buscando la SqlException de origen.
            for (var e = ex; e != null; e = e.InnerException)
            {
                var sql = e as SqlException;
                if (sql == null) continue;
                switch (sql.Number)
                {
                    case 2601:
                    case 2627:
                        return "Ya existe un registro con los mismos datos únicos (CI, código, nombre o e-mail).";
                    case 547:
                        return "La operación no es válida porque el registro está relacionado con otros datos.";
                    case 18456:
                    case 4060:
                        return "No se pudo iniciar sesión en la base de datos. Revise el usuario y la contraseña de la cadena de conexión (App.config).";
                    case 53:
                    case 2:
                    case -1:
                    case 1231:
                        return "No se pudo conectar con SQL Server. Verifique que el servidor esté en ejecución y que la cadena de conexión (App.config) sea correcta.";
                    case 208:
                    case 2812:
                        return "La base de datos no está creada o está desactualizada. Ejecute el script database/pasteleria.sql.";
                    default:
                        return "Error de base de datos (" + sql.Number + "): " + sql.Message;
                }
            }

            var interno = ex;
            while (interno.InnerException != null) interno = interno.InnerException;
            return interno.Message;
        }
    }
}
