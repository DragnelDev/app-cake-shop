/* =============================================================================
   Pastelería "Carmen" - Script de base de datos (SQL Server 2016 SP1 o superior)
   Base: LabPasteleria        Usuario de aplicación: usrpasteleria

   ORDEN DE EJECUCIÓN: ejecutar el script COMPLETO una sola vez, con un usuario
   con permisos de administrador (por ejemplo "sa" o su cuenta de Windows).

   ⚠  El script BORRA y vuelve a crear las tablas y los datos de ejemplo
      (sección 3 y 4). No lo ejecute sobre una base con datos reales.

   Cambios respecto al script original (ddl_dml_pasteleria.sql de la raíz, ya reemplazado):
   - Se eliminó el "DROP DATABASE" que se ejecutaba justo después de CREATE.
   - Se agregó el procedimiento paBuscarCliente (lo usa FrmRegistrarPedido y NO existía).
   - Las claves de los usuarios de ejemplo usan el nuevo formato PBKDF2 (antes: AES con
     llave fija en el código y valores en mayúsculas que sólo funcionaban por la
     collation insensible a mayúsculas de SQL Server).
   - UNIQUE con NULL: SQL Server sólo admite UN NULL en un UNIQUE normal, por lo que sólo
     se podía registrar un cliente sin e-mail / sin CI. Ahora son índices únicos
     filtrados (ignoran NULL y los registros dados de baja).
   - nombre/código de Producto, CI de Empleado/Cliente: unicidad sólo entre registros
     NO eliminados (eliminación lógica estado = -1), de modo que se pueda volver a
     registrar un producto dado de baja.
   - Pedido.estadoPedido: 1 = PENDIENTE, 0 = ENTREGADO, 2 = ANULADO (nuevo).
   - paReportePedidos: LEFT JOIN al usuario (los pedidos sin usuario ya no
     desaparecen del reporte) y excluye pedidos anulados.
   - Datos de ejemplo coherentes con las listas de la aplicación (tipo de producto,
     unidad, tipo de entrega, celulares de 8 dígitos, fechas relativas a hoy).
   ============================================================================= */

USE master;
GO

IF DB_ID(N'LabPasteleria') IS NULL
    CREATE DATABASE LabPasteleria;
GO

/* ---------------------------------------------------------------------------
   1. Login / usuario de aplicación  (SOLO DESARROLLO: cambie la contraseña
      en producción y actualice la cadena de conexión de App.config)
   --------------------------------------------------------------------------- */
IF NOT EXISTS (SELECT 1 FROM sys.server_principals WHERE name = N'usrpasteleria')
    CREATE LOGIN usrpasteleria WITH PASSWORD = N'123456',
        CHECK_POLICY = OFF,
        CHECK_EXPIRATION = OFF,
        DEFAULT_DATABASE = LabPasteleria;
GO

USE LabPasteleria;
GO

IF NOT EXISTS (SELECT 1 FROM sys.database_principals WHERE name = N'usrpasteleria')
    CREATE USER usrpasteleria FOR LOGIN usrpasteleria;
GO

ALTER ROLE db_owner ADD MEMBER usrpasteleria;
GO

/* ---------------------------------------------------------------------------
   2. Eliminación de tablas (orden inverso a las dependencias)
   --------------------------------------------------------------------------- */
DROP TABLE IF EXISTS DetallePedido;
DROP TABLE IF EXISTS Pedido;
DROP TABLE IF EXISTS Producto;
DROP TABLE IF EXISTS Cliente;
DROP TABLE IF EXISTS Usuario;
DROP TABLE IF EXISTS Empleado;
GO

/* ---------------------------------------------------------------------------
   3. Tablas
      estado:       -1 = Eliminado (lógico) | 0 = Inactivo | 1 = Activo
      estadoPedido:  0 = ENTREGADO | 1 = PENDIENTE | 2 = ANULADO
   --------------------------------------------------------------------------- */
CREATE TABLE Empleado (
    id               INT           NOT NULL PRIMARY KEY IDENTITY(1, 1),
    cedulaIdentidad  VARCHAR(12)   NOT NULL,
    nombre           VARCHAR(50)   NOT NULL,
    apellidoPaterno  VARCHAR(50)   NULL,
    apellidoMaterno  VARCHAR(50)   NULL,
    fechaNacimiento  DATE          NOT NULL,
    direccion        VARCHAR(250)  NOT NULL,
    celular          BIGINT        NOT NULL,
    usuarioRegistro  VARCHAR(50)   NOT NULL DEFAULT SUSER_NAME(),
    fechaRegistro    DATETIME2(0)  NOT NULL DEFAULT GETDATE(),
    estado           SMALLINT      NOT NULL DEFAULT 1,
    CONSTRAINT ck_Empleado_estado CHECK (estado IN (-1, 0, 1))
);
CREATE UNIQUE INDEX ux_Empleado_cedula ON Empleado (cedulaIdentidad) WHERE estado <> -1;

CREATE TABLE Usuario (
    id               INT           NOT NULL PRIMARY KEY IDENTITY(1, 1),
    idEmpleado       INT           NOT NULL,
    usuario          VARCHAR(15)   NOT NULL UNIQUE,
    clave            VARCHAR(250)  NOT NULL,   -- "PBKDF2$iteraciones$sal$hash" (ver ClnPasteleria.Seguridad)
    role             VARCHAR(20)   NOT NULL,   -- ADMINISTRADOR | CAJERO
    usuarioRegistro  VARCHAR(50)   NOT NULL DEFAULT SUSER_NAME(),
    fechaRegistro    DATETIME2(0)  NOT NULL DEFAULT GETDATE(),
    estado           SMALLINT      NOT NULL DEFAULT 1,
    CONSTRAINT fk_Usuario_Empleado FOREIGN KEY (idEmpleado) REFERENCES Empleado(id),
    CONSTRAINT ck_Usuario_estado   CHECK (estado IN (-1, 0, 1)),
    CONSTRAINT ck_Usuario_role     CHECK (role IN ('ADMINISTRADOR', 'CAJERO'))
);

CREATE TABLE Cliente (
    id               INT           NOT NULL PRIMARY KEY IDENTITY(1, 1),
    cedulaIdentidad  VARCHAR(12)   NULL,
    nombre           VARCHAR(100)  NOT NULL,
    apellido         VARCHAR(100)  NOT NULL,
    celular          VARCHAR(20)   NOT NULL,
    email            VARCHAR(100)  NULL,
    direccion        VARCHAR(255)  NOT NULL,
    usuarioRegistro  VARCHAR(50)   NOT NULL DEFAULT SUSER_NAME(),
    fechaRegistro    DATETIME2(0)  NOT NULL DEFAULT GETDATE(),
    estado           SMALLINT      NOT NULL DEFAULT 1,
    CONSTRAINT ck_Cliente_estado CHECK (estado IN (-1, 0, 1))
);
CREATE UNIQUE INDEX ux_Cliente_cedula ON Cliente (cedulaIdentidad) WHERE cedulaIdentidad IS NOT NULL AND estado <> -1;
CREATE UNIQUE INDEX ux_Cliente_email  ON Cliente (email)           WHERE email           IS NOT NULL AND estado <> -1;

CREATE TABLE Producto (
    id               INT            NOT NULL PRIMARY KEY IDENTITY(1, 1),
    codigo           VARCHAR(20)    NOT NULL,
    nombre           VARCHAR(150)   NOT NULL,
    unidadMedida     VARCHAR(50)    NOT NULL,
    precio           DECIMAL(10, 2) NOT NULL,
    tipo             VARCHAR(50)    NOT NULL,
    descripcion      VARCHAR(MAX)   NULL,
    usuarioRegistro  VARCHAR(50)    NOT NULL DEFAULT SUSER_NAME(),
    fechaRegistro    DATETIME2(0)   NOT NULL DEFAULT GETDATE(),
    estado           SMALLINT       NOT NULL DEFAULT 1,
    CONSTRAINT ck_Producto_precio CHECK (precio > 0),
    CONSTRAINT ck_Producto_estado CHECK (estado IN (-1, 0, 1))
);
CREATE UNIQUE INDEX ux_Producto_codigo ON Producto (codigo) WHERE estado <> -1;
CREATE UNIQUE INDEX ux_Producto_nombre ON Producto (nombre) WHERE estado <> -1;

CREATE TABLE Pedido (
    id               INT           NOT NULL PRIMARY KEY IDENTITY(1, 1),
    fechaPedido      DATETIME2(0)  NOT NULL DEFAULT GETDATE(),
    fechaEntrega     DATETIME2(0)  NOT NULL,
    idCliente        INT           NULL,
    idUsuario        INT           NULL,
    tipoEntrega      VARCHAR(50)   NULL,
    usuarioRegistro  VARCHAR(50)   NOT NULL DEFAULT SUSER_NAME(),
    fechaRegistro    DATETIME2(0)  NOT NULL DEFAULT GETDATE(),
    estadoPedido     SMALLINT      NOT NULL DEFAULT 1,
    estado           SMALLINT      NOT NULL DEFAULT 1,
    CONSTRAINT fk_Pedido_Cliente      FOREIGN KEY (idCliente) REFERENCES Cliente(id),
    CONSTRAINT fk_Pedido_Usuario      FOREIGN KEY (idUsuario) REFERENCES Usuario(id),
    CONSTRAINT ck_Pedido_estadoPedido CHECK (estadoPedido IN (0, 1, 2)),
    CONSTRAINT ck_Pedido_estado       CHECK (estado IN (-1, 0, 1))
);
CREATE INDEX ix_Pedido_fechaPedido ON Pedido (fechaPedido);
CREATE INDEX ix_Pedido_cliente     ON Pedido (idCliente);

CREATE TABLE DetallePedido (
    id               INT            NOT NULL PRIMARY KEY IDENTITY(1, 1),
    idPedido         INT            NOT NULL,
    idProducto       INT            NOT NULL,
    metodoPago       VARCHAR(50)    NULL,
    cantidad         INT            NOT NULL CHECK (cantidad > 0),
    precioUnitario   DECIMAL(10, 2) NOT NULL CHECK (precioUnitario >= 0),
    subTotal         AS (cantidad * precioUnitario) PERSISTED,
    usuarioRegistro  VARCHAR(50)    NOT NULL DEFAULT SUSER_NAME(),
    fechaRegistro    DATETIME2(0)   NOT NULL DEFAULT GETDATE(),
    estado           SMALLINT       NOT NULL DEFAULT 1,
    CONSTRAINT fk_DetallePedido_Pedido   FOREIGN KEY (idPedido)   REFERENCES Pedido(id),
    CONSTRAINT fk_DetallePedido_Producto FOREIGN KEY (idProducto) REFERENCES Producto(id),
    CONSTRAINT ck_DetallePedido_estado   CHECK (estado IN (-1, 0, 1))
);
CREATE INDEX ix_DetallePedido_pedido ON DetallePedido (idPedido);
GO

/* ---------------------------------------------------------------------------
   4. Datos de ejemplo
      Usuarios: jperez / jluis (ADMINISTRADOR) y mlopez (CAJERO)
      Contraseña de los tres: hola123   (cámbiela desde la aplicación)
   --------------------------------------------------------------------------- */
INSERT INTO Empleado (cedulaIdentidad, nombre, apellidoPaterno, apellidoMaterno, fechaNacimiento, direccion, celular) VALUES
('12345678', 'Juan',  'Perez', 'Lopez', '1990-05-15', 'Calle Falsa 123',      71234567),
('12345788', 'Luis',  'Perez', 'Lopez', '1992-08-21', 'Calle Falsa 123',      72345678),
('4567890',  'Maria', 'Lopez', NULL,    '1995-03-02', 'Av. Siempre Viva 742', 73456789);

DECLARE @claveHola123 VARCHAR(250) =
    'PBKDF2$100000$WjyR0n4ItG8dKsDpg3tUpg==$txAirLZOllr6SMe61BSNCd43/30GSWsugo2nZcDAo5U=';

INSERT INTO Usuario (idEmpleado, usuario, clave, role) VALUES
(1, 'jperez', @claveHola123, 'ADMINISTRADOR'),
(2, 'jluis',  @claveHola123, 'ADMINISTRADOR'),
(3, 'mlopez', @claveHola123, 'CAJERO');

INSERT INTO Cliente (cedulaIdentidad, nombre, apellido, celular, email, direccion) VALUES
('749426',  'Juan',   'Perez', '71110001', 'juan@gmail.com',   'Calle Falsa 123'),
('756446',  'Maria',  'Lopez', '72220002', 'maria@gmail.com',  'Avenida Siempre Viva 456'),
('7493432', 'Carlos', 'Gomez', '73330003', 'carlos@gmail.com', 'Boulevard Central 789'),
(NULL,      'Ana',    'Rojas', '74440004', NULL,               'Zona Norte, Calle 5 #120');

INSERT INTO Producto (codigo, nombre, unidadMedida, precio, tipo, descripcion) VALUES
('TOR001', 'Torta de Chocolate',    'Grande',  120.00, 'Torta',    'Torta de chocolate con cobertura de ganache.'),
('TOR002', 'Torta Tres Leches',     'Mediana',  90.00, 'Torta',    'Bizcocho húmedo bañado en tres leches con crema.'),
('TAR001', 'Tarta de Frutilla',     'Mediana',  70.00, 'Tarta',    'Base crocante con crema pastelera y frutillas frescas.'),
('PAY001', 'Pay de Limón',          'Pequeña',  35.00, 'Pay',      'Pay de limón con merengue italiano.'),
('CUP001', 'Cupcake de Vainilla',   'Pequeña',   8.00, 'Cupcakes', 'Cupcake de vainilla con glaseado de crema.'),
('CUP002', 'Cupcake de Chocolate',  'Pequeña',   8.00, 'Cupcakes', 'Cupcake de chocolate con relleno de dulce de leche.');

-- Pedidos con fechas relativas a "hoy" para que el panel y los reportes muestren datos.
INSERT INTO Pedido (fechaPedido, fechaEntrega, idCliente, idUsuario, tipoEntrega, usuarioRegistro, estadoPedido) VALUES
(DATEADD(DAY, -2, GETDATE()), DATEADD(DAY,  3, CAST(GETDATE() AS DATETIME2(0))), 1, 1, 'A DOMICILIO',   'jperez', 1),
(DATEADD(DAY, -5, GETDATE()), DATEADD(DAY, -1, CAST(GETDATE() AS DATETIME2(0))), 2, 1, 'LOCAL (TIENDA)', 'jperez', 0),
(DATEADD(DAY, -1, GETDATE()), CAST(GETDATE() AS DATETIME2(0)),                   3, 3, 'LOCAL (TIENDA)', 'mlopez', 1),
(GETDATE(),                   DATEADD(DAY,  7, CAST(GETDATE() AS DATETIME2(0))), 4, 3, 'A DOMICILIO',   'mlopez', 1);

INSERT INTO DetallePedido (idPedido, idProducto, metodoPago, cantidad, precioUnitario, usuarioRegistro) VALUES
(1, 1, 'EFECTIVO', 1, 120.00, 'jperez'),
(1, 5, 'EFECTIVO', 12,  8.00, 'jperez'),
(2, 3, 'TARJETA',  2,  70.00, 'jperez'),
(3, 4, 'EFECTIVO', 3,  35.00, 'mlopez'),
(4, 2, 'TARJETA',  1,  90.00, 'mlopez'),
(4, 6, 'TARJETA',  6,   8.00, 'mlopez');
GO

/* ---------------------------------------------------------------------------
   5. Procedimientos almacenados (los usa el modelo EF: CadPasteleria/Modelo.edmx)
      Si modifica las columnas que devuelve un procedimiento, actualice el modelo
      EF ("Actualizar modelo desde la base de datos") y su "Result".
   --------------------------------------------------------------------------- */
CREATE OR ALTER PROCEDURE paListarProductos @parametro VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT P.id, P.codigo, P.nombre, P.unidadMedida, P.precio, P.tipo, P.descripcion,
           P.estado, P.usuarioRegistro, P.fechaRegistro
    FROM Producto P
    WHERE P.estado = 1
      AND (P.nombre LIKE '%' + @parametro + '%' OR P.codigo LIKE '%' + @parametro + '%')
    ORDER BY P.nombre ASC;
END
GO

CREATE OR ALTER PROCEDURE paListarEmpleados @parametro VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT E.id, E.cedulaIdentidad, E.nombre, E.apellidoPaterno, E.apellidoMaterno,
           E.fechaNacimiento, E.direccion, E.celular, U.role,
           ISNULL(U.usuario, 'Sin Usuario') AS UsuarioAsignado,
           E.estado, E.usuarioRegistro, E.fechaRegistro
    FROM Empleado E
    LEFT JOIN Usuario U ON E.id = U.idEmpleado AND U.estado <> -1
    WHERE E.estado = 1
      AND (E.nombre LIKE '%' + @parametro + '%'
        OR E.apellidoPaterno LIKE '%' + @parametro + '%'
        OR E.apellidoMaterno LIKE '%' + @parametro + '%'
        OR E.cedulaIdentidad LIKE '%' + @parametro + '%')
    ORDER BY E.nombre ASC;
END
GO

CREATE OR ALTER PROCEDURE paListarCliente @parametro VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT id, cedulaIdentidad, nombre, apellido, celular, email, direccion,
           usuarioRegistro, fechaRegistro, estado
    FROM Cliente
    WHERE estado = 1
      AND (nombre + ' ' + apellido LIKE '%' + @parametro + '%'
        OR email LIKE '%' + @parametro + '%'
        OR cedulaIdentidad LIKE '%' + @parametro + '%'
        OR celular LIKE '%' + @parametro + '%')
    ORDER BY apellido ASC, nombre ASC;
END
GO

-- NUEVO: búsqueda de clientes para el registro de pedidos (por nombre, CI, celular o dirección)
CREATE OR ALTER PROCEDURE paBuscarCliente @Busqueda VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT id, cedulaIdentidad, nombre, apellido, celular, email, direccion
    FROM Cliente
    WHERE estado = 1
      AND (nombre + ' ' + apellido LIKE '%' + @Busqueda + '%'
        OR cedulaIdentidad LIKE '%' + @Busqueda + '%'
        OR celular LIKE '%' + @Busqueda + '%'
        OR direccion LIKE '%' + @Busqueda + '%')
    ORDER BY apellido ASC, nombre ASC;
END
GO

-- Pedidos pendientes + entregados HOY (anulados y eliminados no se muestran)
CREATE OR ALTER PROCEDURE paListarPedidos
AS
BEGIN
    SET NOCOUNT ON;
    SELECT p.id, p.fechaPedido, p.fechaEntrega,
           c.nombre + ' ' + ISNULL(c.apellido, '') AS ClienteNombre,
           c.celular   AS ClienteTelefono,
           c.direccion AS ClienteDireccion,
           p.tipoEntrega, p.usuarioRegistro,
           CASE p.estadoPedido WHEN 1 THEN 'PENDIENTE' WHEN 0 THEN 'ENTREGADO' ELSE 'ANULADO' END AS EstadoNegocio
    FROM dbo.Pedido p
    INNER JOIN dbo.Cliente c ON p.idCliente = c.id
    WHERE p.estado = 1
      AND (p.estadoPedido = 1
           OR (p.estadoPedido = 0 AND CAST(p.fechaEntrega AS DATE) = CAST(GETDATE() AS DATE)))
    ORDER BY p.fechaPedido DESC;
END
GO

CREATE OR ALTER PROCEDURE paListarDetallePedido @idPedido INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT d.id, p.nombre AS Producto, d.cantidad, d.precioUnitario, d.metodoPago,
           (d.cantidad * d.precioUnitario) AS subTotal,
           d.usuarioRegistro, d.fechaRegistro AS FechaRegistro, d.estado AS estado,
           CASE ped.estadoPedido WHEN 1 THEN 'PENDIENTE' WHEN 0 THEN 'ENTREGADO' ELSE 'ANULADO' END AS EstadoNegocio,
           ped.fechaEntrega AS FechaEntrega
    FROM DetallePedido d
    INNER JOIN Producto p   ON p.id   = d.idProducto
    INNER JOIN Pedido   ped ON ped.id = d.idPedido
    WHERE d.idPedido = @idPedido;
END
GO

CREATE OR ALTER PROCEDURE paReportePedidos
    @FechaInicio   DATE,
    @FechaFin      DATE,
    @NombreCliente VARCHAR(200) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        P.id AS IdPedido, P.fechaPedido AS FechaPedido, P.fechaEntrega AS FechaEntrega,
        P.tipoEntrega AS TipoEntrega, P.estadoPedido AS EstadoPedido,
        C.nombre + ' ' + C.apellido AS NombreCliente,
        C.celular AS CelularCliente, C.direccion AS DireccionCliente,
        ISNULL(LTRIM(RTRIM(E.nombre + ' ' + ISNULL(E.apellidoPaterno, '') + ' ' + ISNULL(E.apellidoMaterno, ''))), 'N/D') AS NombreEmpleado,
        (SELECT DP.cantidad, PR.nombre AS Producto, DP.precioUnitario, DP.subTotal, DP.metodoPago
           FROM DetallePedido DP
           INNER JOIN Producto PR ON DP.idProducto = PR.id
          WHERE DP.idPedido = P.id
          FOR JSON PATH) AS DetalleProductosJSON,
        SUM(DP_Total.subTotal) AS MontoTotal
    FROM Pedido P
    INNER JOIN Cliente C           ON P.idCliente = C.id
    LEFT  JOIN Usuario U           ON P.idUsuario = U.id
    LEFT  JOIN Empleado E          ON U.idEmpleado = E.id
    INNER JOIN DetallePedido DP_Total ON P.id = DP_Total.idPedido
    WHERE P.fechaPedido >= @FechaInicio
      AND P.fechaPedido <  DATEADD(DAY, 1, @FechaFin)
      AND P.estado = 1
      AND P.estadoPedido <> 2                         -- sin pedidos anulados
      AND (@NombreCliente IS NULL OR @NombreCliente = ''
           OR C.nombre + ' ' + C.apellido LIKE '%' + @NombreCliente + '%'
           OR C.cedulaIdentidad LIKE '%' + @NombreCliente + '%')
    GROUP BY P.id, P.fechaPedido, P.fechaEntrega, P.tipoEntrega, P.estadoPedido,
             C.nombre, C.apellido, C.celular, C.direccion,
             E.nombre, E.apellidoPaterno, E.apellidoMaterno
    ORDER BY P.fechaPedido DESC;
END
GO

/* ---------------------------------------------------------------------------
   6. Verificación rápida (opcional)
   --------------------------------------------------------------------------- */
-- EXEC paListarProductos '';
-- EXEC paListarEmpleados '';
-- EXEC paListarCliente '';
-- EXEC paBuscarCliente 'perez';
-- EXEC paListarPedidos;
-- EXEC paReportePedidos @FechaInicio = '2020-01-01', @FechaFin = '2099-12-31';
SELECT * FROM Usuario;
