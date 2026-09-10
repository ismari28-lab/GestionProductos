-- =============================================
-- Stored Procedures para el módulo Movimientos (STOCKEO) — Sprint A + Sprint B
-- Ejecutar manualmente contra la base de datos (GestionProductoBD).
-- =============================================

-- SP_BuscarProductosParaMovimiento
-- Autocomplete: busca por Código o Nombre (LIKE), solo activos, top 10
-- Incluye stock actual (0 si no hay registro en Stock_Producto)
CREATE OR ALTER PROCEDURE SP_BuscarProductosParaMovimiento
    @Termino NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TOP 10
        p.IdProductoPK,
        p.Codigo,
        p.Nombre,
        ISNULL(sp.Stock, 0) AS StockActual
    FROM Producto p
    LEFT JOIN Stock_Producto sp
        ON sp.IdProductoFK = CAST(p.IdProductoPK AS SMALLINT)
        AND sp.Estado = 1
    WHERE p.Estado = 1
      AND (p.Codigo LIKE '%' + @Termino + '%' OR p.Nombre LIKE '%' + @Termino + '%')
    ORDER BY p.Nombre ASC;
END
GO

-- SP_ObtenerStockProducto
-- Retorna stock actual y si existe registro (para decidir crear/actualizar)
CREATE OR ALTER PROCEDURE SP_ObtenerStockProducto
    @IdProductoFK SMALLINT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        IdStock_ProductoPK,
        ISNULL(Stock, 0) AS Stock
    FROM Stock_Producto
    WHERE IdProductoFK = @IdProductoFK AND Estado = 1;
END
GO

-- SP_InsertarMovimientoCabecera
-- Retorna nuevo IdMovimientoPK
CREATE OR ALTER PROCEDURE SP_InsertarMovimientoCabecera
    @TipoMovimiento NVARCHAR(20),
    @Referencia NVARCHAR(100) = NULL,
    @Fecha DATETIME,
    @IdUsuarioFK SMALLINT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO MovimientoInventario (TipoMovimiento, Referencia, Fecha, IdUsuarioFK, Estado)
    VALUES (@TipoMovimiento, @Referencia, @Fecha, @IdUsuarioFK, 1);
    SELECT CAST(SCOPE_IDENTITY() AS SMALLINT) AS NuevoId;
END
GO

-- SP_InsertarDetalleMovimiento
CREATE OR ALTER PROCEDURE SP_InsertarDetalleMovimiento
    @IdMovimientoFK SMALLINT,
    @IdProductoFK SMALLINT,
    @Cantidad SMALLINT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO DetalleMovimiento (IdMovimientoFK, IdProductoFK, Cantidad, Estado)
    VALUES (@IdMovimientoFK, @IdProductoFK, @Cantidad, 1);
END
GO

-- SP_ActualizarStockProducto
-- Suma o resta según @Delta (positivo entrada, negativo salida)
-- Retorna filas afectadas
CREATE OR ALTER PROCEDURE SP_ActualizarStockProducto
    @IdProductoFK SMALLINT,
    @Delta SMALLINT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Stock_Producto
    SET Stock = ISNULL(Stock, 0) + @Delta
    WHERE IdProductoFK = @IdProductoFK AND Estado = 1;
    SELECT @@ROWCOUNT AS FilasAfectadas;
END
GO

-- SP_CrearStockProducto
-- Para productos que aún no tienen registro (solo se usa en Entrada)
CREATE OR ALTER PROCEDURE SP_CrearStockProducto
    @IdProductoFK SMALLINT,
    @Stock SMALLINT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Stock_Producto (IdProductoFK, Stock, Stock_Minimo, Estado)
    VALUES (@IdProductoFK, @Stock, 0, 1);
END
GO

-- =============================================
-- Sprint B — Historial de movimientos
-- =============================================

-- SP_ListarHistorialMovimientos
-- Paginado server-side con filtros opcionales.
-- Retorna DOS result sets:
--   1) Metadata: TotalRegistros
--   2) Página actual de movimientos
CREATE OR ALTER PROCEDURE SP_ListarHistorialMovimientos
    @Desde DATETIME,
    @Hasta DATETIME,
    @Tipo NVARCHAR(20) = NULL,         -- NULL o '' = todos
    @Termino NVARCHAR(100) = NULL,     -- NULL o '' = sin búsqueda
    @Pagina INT = 1,
    @TamanioPagina INT = 25
AS
BEGIN
    SET NOCOUNT ON;

    -- Normalizar Hasta al final del día para incluir movimientos del mismo día
    -- (cast intermedio a DATETIME: DATEADD(SECOND,...) no admite un operando de tipo DATE)
    DECLARE @DesdeInicioDia DATETIME = CAST(@Desde AS DATE);
    DECLARE @HastaFinDia DATETIME = DATEADD(SECOND, -1, DATEADD(DAY, 1, CAST(CAST(@Hasta AS DATE) AS DATETIME)));

    DECLARE @Offset INT = (@Pagina - 1) * @TamanioPagina;
    DECLARE @TerminoLike NVARCHAR(102) = '%' + ISNULL(@Termino, '') + '%';
    DECLARE @TieneTermino BIT = CASE WHEN ISNULL(@Termino, '') = '' THEN 0 ELSE 1 END;
    DECLARE @TieneTipo BIT = CASE WHEN ISNULL(@Tipo, '') = '' THEN 0 ELSE 1 END;

    -- Tabla variable en vez de CTE: un CTE solo es visible para el statement inmediato
    -- siguiente, y aquí necesitamos reutilizar el conjunto filtrado en dos SELECT (total + página).
    DECLARE @MovimientosFiltrados TABLE (
        IdMovimientoPK SMALLINT PRIMARY KEY,
        Referencia NVARCHAR(100) NULL,
        Fecha DATETIME NULL,
        TipoMovimiento NVARCHAR(20) NULL,
        NombreUsuario NVARCHAR(100) NULL
    );

    INSERT INTO @MovimientosFiltrados (IdMovimientoPK, Referencia, Fecha, TipoMovimiento, NombreUsuario)
    SELECT
        m.IdMovimientoPK,
        m.Referencia,
        m.Fecha,
        m.TipoMovimiento,
        u.Nombre AS NombreUsuario
    FROM MovimientoInventario m
    LEFT JOIN Usuario u ON u.IdUsuarioPK = m.IdUsuarioFK
    WHERE m.Estado = 1
      AND m.Fecha >= @DesdeInicioDia
      AND m.Fecha <= @HastaFinDia
      AND (@TieneTipo = 0 OR m.TipoMovimiento = @Tipo)
      AND (
          @TieneTermino = 0
          OR u.Nombre LIKE @TerminoLike
          OR EXISTS (
                SELECT 1
                FROM DetalleMovimiento dm
                -- TODO: eliminar cast cuando se resuelva deuda técnica de FK short vs int
                INNER JOIN Producto p ON CAST(p.IdProductoPK AS SMALLINT) = dm.IdProductoFK
                WHERE dm.IdMovimientoFK = m.IdMovimientoPK
                  AND dm.Estado = 1
                  AND (p.Codigo LIKE @TerminoLike OR p.Nombre LIKE @TerminoLike)
          )
      );

    -- Result set 1: total
    SELECT COUNT(*) AS TotalRegistros FROM @MovimientosFiltrados;

    -- Result set 2: página con TotalItems calculado
    SELECT
        mf.IdMovimientoPK,
        ISNULL(mf.Referencia, '') AS Referencia,
        mf.Fecha,
        mf.TipoMovimiento,
        ISNULL(mf.NombreUsuario, '(sin usuario)') AS NombreUsuario,
        ISNULL((
            SELECT SUM(CAST(dm.Cantidad AS INT))
            FROM DetalleMovimiento dm
            WHERE dm.IdMovimientoFK = mf.IdMovimientoPK
              AND dm.Estado = 1
        ), 0) AS TotalItems
    FROM @MovimientosFiltrados mf
    ORDER BY mf.Fecha DESC, mf.IdMovimientoPK DESC
    OFFSET @Offset ROWS
    FETCH NEXT @TamanioPagina ROWS ONLY;
END
GO

-- SP_ObtenerDetalleMovimiento
-- Para el modal: cabecera + líneas de detalle
CREATE OR ALTER PROCEDURE SP_ObtenerDetalleMovimiento
    @IdMovimientoPK SMALLINT
AS
BEGIN
    SET NOCOUNT ON;

    -- Result set 1: cabecera
    SELECT
        m.IdMovimientoPK,
        ISNULL(m.Referencia, '') AS Referencia,
        m.TipoMovimiento,
        m.Fecha,
        ISNULL(u.Nombre, '(sin usuario)') AS NombreUsuario
    FROM MovimientoInventario m
    LEFT JOIN Usuario u ON u.IdUsuarioPK = m.IdUsuarioFK
    WHERE m.IdMovimientoPK = @IdMovimientoPK
      AND m.Estado = 1;

    -- Result set 2: detalle
    SELECT
        p.Codigo,
        p.Nombre AS ProductoNombre,
        CAST(dm.Cantidad AS INT) AS Cantidad
    FROM DetalleMovimiento dm
    -- TODO: eliminar cast cuando se resuelva deuda técnica de FK short vs int
    INNER JOIN Producto p ON CAST(p.IdProductoPK AS SMALLINT) = dm.IdProductoFK
    WHERE dm.IdMovimientoFK = @IdMovimientoPK
      AND dm.Estado = 1
    ORDER BY dm.IdDetalleMovimientoPK ASC;
END
GO
