# Cambios realizados (refactorización, correcciones, UI y funcionalidades nuevas)

## 1. Errores corregidos

| # | Problema encontrado | Corrección |
|---|---|---|
| 1 | **Los pedidos nunca guardaban `idUsuario`**, y `paReportePedidos` hacía `INNER JOIN Usuario`: ninguna venta aparecía en el reporte. | `PedidoCln.registrar` guarda el usuario de la sesión; el SP ahora usa `LEFT JOIN`. |
| 2 | Pedido y detalle se guardaban con **dos `SaveChanges`** (quedaban pedidos vacíos si fallaba el segundo). | Un solo guardado transaccional; precios tomados de la BD. |
| 3 | El script SQL hacía `CREATE DATABASE` y enseguida `DROP DATABASE`. | Script reescrito (`database/pasteleria.sql`). |
| 4 | Faltaba el procedimiento **`paBuscarCliente`** (lo usa el registro de pedidos). | Creado. |
| 5 | `UNIQUE` con NULL: sólo se podía registrar **un** cliente sin e-mail/CI; además se guardaba `""` en vez de NULL. | Índices únicos filtrados + normalización a NULL. |
| 6 | Unicidad de producto/CI incluía registros dados de baja (no se podía volver a crear). | Unicidad sólo entre registros no eliminados. |
| 7 | **Un usuario dado de baja o inactivo podía seguir ingresando** (`validar` no miraba `estado`). | `validar` exige usuario y empleado activos; `eliminar` empleado baja también sus usuarios. |
| 8 | Al **editar un empleado se reiniciaba su contraseña** a `hola123`; además la validación impedía editar (el usuario "ya existía"). | La edición no toca la clave; la unicidad excluye al propio usuario. |
| 9 | El botón **Usuarios** abría la pantalla de Empleados; había un handler duplicado (`ribbonButton2`). | Menú nuevo; pantalla de usuarios real. |
| 10 | `FrmCliente` nunca devolvía `DialogResult.OK`/`Tag`, así que **"Agregar nuevo cliente"** desde Pedidos no seleccionaba al cliente creado. | Propiedad `UltimoClienteId`. |
| 11 | Al salir del combo de producto aparecía un mensaje de error aunque estuviera vacío (incluso al pulsar *Cancelar*). | Validación sólo si hay texto. |
| 12 | Tras guardar un pedido la grilla perdía encabezados (usaba nombres de columna con otra capitalización). | Nombres corregidos. |
| 13 | Datos de ejemplo incoherentes con la app (tipos de producto, tipo de entrega, celulares de 8 dígitos, fechas pasadas). | Datos nuevos y fechas relativas a hoy. |
| 14 | Sin manejo de errores: una cadena de conexión mala o un dato duplicado cerraba la app con una excepción cruda. | `Errores.Mensaje` + manejador global de excepciones. |
| 15 | `ProductoCln.listar()` devolvía inactivos; `listarReporte` ignoraba su filtro; `buscarUnoPorId` y `DetallePedidoCln.eliminarPorPedido` (borrado físico) eran código muerto/riesgoso. | Corregido o eliminado. |
| 16 | Mensajes erróneos ("eliminar empleados" en Productos, "eliminar el producto" en Empleados). | Corregidos. |
| 17 | Longitud de usuario sin límite (columna de 15) → error SQL. | Validación 5–15 caracteres. |

## 2. Seguridad

* Contraseñas: **PBKDF2-SHA256, sal aleatoria por usuario, 100 000 iteraciones** (`ClnPasteleria/Seguridad.cs`). Antes: AES determinista con llave fija en el código (y el valor del script en mayúsculas funcionaba sólo por la collation de SQL Server).
* Migración automática: las claves antiguas siguen sirviendo y se convierten al nuevo formato en el primer login correcto.
* Altas de empleado generan una **contraseña temporal aleatoria** (antes: `hola123` para todos).
* No se puede desactivar/quitar al último administrador ni a uno mismo.
* La cadena de conexión de `App.config` sigue teniendo el usuario de laboratorio (`usrpasteleria`/`123456`): cámbiela para cualquier uso real.

## 3. Refactorización

* **Capa de negocio** (`ClnPasteleria`): validaciones y reglas movidas desde los formularios; `ReglaNegocioException`, `Validacion`, `Errores`, DTOs, constantes (`Roles`, `EstadoRegistro`, `EstadoPedido`).
* **Presentación** (`CpPasteleria`): namespace `CpMinerva` (copiado de otro proyecto) → `CpPasteleria`; `Util` sin criptografía; `Sesion` en lugar de un campo estático suelto; `FormBase` + `Estilo` para el tema; `LineaPedido` tipado en lugar de `Dictionary<string, object>` y `dynamic`.
* **Dependencias:** se eliminó **ComponentOne** (referencias con ruta absoluta `C:\Program Files (x86)\ComponentOne…`, `licenses.licx`, `C1ThemeController` en cada formulario). El proyecto compila en cualquier PC con Visual Studio.
* `.csproj`: eliminada la firma ClickOnce que apuntaba a un `.pfx` inexistente y rutas personales (`C:\Users\Nam\Desktop`).
* **Peso del repositorio:** el emblema de 1,2 MB estaba incrustado en dos `.resx` y una imagen de 1,2 MB sin usar en Recursos; ahora hay un único recurso de 200 KB.

## 4. Interfaz de usuario

* Paleta tomada del logo (rosa/cacao/crema), tipografía Segoe UI, botones planos con estados (primario, secundario, peligro, éxito) y grillas legibles (encabezado oscuro, filas alternas, formato de fechas e importes).
* **Nuevo menú principal** con barra lateral y panel de indicadores (reemplaza el ribbon).
* Formularios con `ErrorProvider` en todos los campos, mensajes consistentes, Enter/Esc en login y cambio de clave, contraseña oculta, ventana de empleados con tope de edad.

## 5. Funcionalidades nuevas

Panel de inicio · Gestión de pedidos (entregar / anular, detalle) · Gestión de usuarios (rol, activar, restablecer clave) · Cambiar contraseña · Quitar ítems del pedido · Exportar reporte a CSV · Búsqueda de productos por código y de clientes por CI/celular · Roles `ADMINISTRADOR` / `CAJERO` aplicados en toda la app.

## 6. `.gitignore`

Nuevo, por secciones: salida de compilación (`bin/`, `obj/`, `*.dll`, `*.exe`, `*.pdb`…), **paquetes NuGet** (`packages/`), archivos de IDE/usuario (`.vs/`, `*.user`, `*.suo`), temporales de EF (`edmxResourcesToEmbed`, `*.csdl/ssdl/msl`), bases locales, **secretos** (`*.pfx`, `*.local.config`, `.env`), ClickOnce/MSIX, licencias de componentes, SO y exportaciones. Se eliminó el `.gitignore` duplicado de `AppPasteleria/`.

**Importante:** el repositorio original ya tenía `bin/`, `obj/` y `packages/` versionados. El `.gitignore` no los quita del historial; hay que ejecutar una vez:

```
git rm -r --cached AppPasteleria/packages AppPasteleria/*/bin AppPasteleria/*/obj
git commit -m "Dejar de versionar binarios y paquetes"
```

## 7. Qué se verificó y qué no

* ✔ Las tres capas (CAD, CLN, CP) **compilan sin errores** (compilador de Mono con C# 7+, contra EntityFramework 6.5.1).
* ✔ Probado el módulo de contraseñas: la clave `hola123` del script SQL valida contra el hash PBKDF2; el formato antiguo sigue aceptándose; la política de contraseñas y las validaciones de formato responden como se espera.
* ✘ **No se pudo ejecutar contra SQL Server ni probar visualmente la interfaz** (entorno Linux sin SQL Server/Windows). Recomendado antes de entregar: ejecutar el script SQL, recorrer cada pantalla y ajustar posiciones si algún control queda desalineado (los formularios existentes conservan su diseño original; el tema se aplica en tiempo de ejecución).
* ✘ `Modelo.edmx` no se modificó. El procedimiento `paResumenPedidos` aparece en el modelo pero nunca existió en el script ni se usa; si quiere conservarlo hay que crearlo o quitarlo del modelo.
* ℹ Se asume que el esquema es el del nuevo script; si ya tiene una base creada con el script antiguo, vuelva a ejecutar el nuevo (recrea tablas y datos de ejemplo).

## 8. Siguientes pasos sugeridos

Comprobante imprimible del pedido · reporte por producto · bitácora de cambios de estado (hoy sólo se guarda el estado final) · pruebas automatizadas de `ClnPasteleria` · mover la cadena de conexión a un archivo local no versionado.
