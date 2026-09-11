-- =============================================
-- Stored Procedures para el módulo Productos (STOCKEO) — Sprint A
-- Listado con filtros, sort y paginación (solo lectura).
-- Ejecutar manualmente contra la base de datos (GestionProductoBD).
-- =============================================

-- SP_ListarCategoriasActivas
-- Alimenta el dropdown de filtro por categoría
CREATE OR ALTER PROCEDURE SP_ListarCategoriasActivas
AS
BEGIN
    SET NOCOUNT ON;
    SELECT IdCategoriaPK, Nombre
    FROM Categoria
    WHERE Estado = 1
    ORDER BY Nombre ASC;
END
GO

-- SP_ListarProductos
-- Listado paginado con filtros y sort dinámico.
-- Retorna DOS result sets: (1) TotalRegistros, (2) página actual.
CREATE OR ALTER PROCEDURE SP_ListarProductos
    @Termino NVARCHAR(100) = NULL,         -- LIKE código o nombre; NULL/'' = sin búsqueda
    @IdCategoria SMALLINT = NULL,          -- NULL = todas
    @IncluirInactivos BIT = 0,             -- 0 = solo Estado=1; 1 = todos
    @OrdenarPor NVARCHAR(20) = 'nombre',   -- codigo, nombre, categoria, existencias, preciocompra, precioventa, estado
    @Direccion NVARCHAR(4) = 'ASC',        -- ASC | DESC
    @Pagina INT = 1,
    @TamanioPagina INT = 25
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Offset INT = (@Pagina - 1) * @TamanioPagina;
    DECLARE @TerminoLike NVARCHAR(102) = '%' + ISNULL(@Termino, '') + '%';
    DECLARE @TieneTermino BIT = CASE WHEN ISNULL(@Termino, '') = '' THEN 0 ELSE 1 END;
    DECLARE @Dir NVARCHAR(4) = CASE WHEN UPPER(ISNULL(@Direccion, 'ASC')) = 'DESC' THEN 'DESC' ELSE 'ASC' END;

    -- Whitelist de columnas ordenables (defensa contra sort injection)
    DECLARE @Col NVARCHAR(20) = LOWER(ISNULL(@OrdenarPor, 'nombre'));
    IF @Col NOT IN ('codigo','nombre','categoria','existencias','preciocompra','precioventa','estado')
        SET @Col = 'nombre';

    -- Tabla variable: se reutiliza el conjunto filtrado en dos SELECT (total + página).
    DECLARE @ProductosFiltrados TABLE (
        IdProductoPK INT PRIMARY KEY,
        Codigo NVARCHAR(50) NULL,
        Nombre NVARCHAR(150) NULL,
        CategoriaNombre NVARCHAR(150) NULL,
        Existencias INT NULL,
        StockMinimo INT NULL,
        PrecioCompra DECIMAL(10,2) NULL,
        PrecioVenta DECIMAL(10,2) NULL,
        Estado BIT NULL
    );

    INSERT INTO @ProductosFiltrados (IdProductoPK, Codigo, Nombre, CategoriaNombre, Existencias, StockMinimo, PrecioCompra, PrecioVenta, Estado)
    SELECT
        p.IdProductoPK,
        p.Codigo,
        p.Nombre,
        ISNULL(c.Nombre, N'(sin categoría)') AS CategoriaNombre,
        ISNULL(sp.Stock, 0) AS Existencias,
        ISNULL(sp.Stock_Minimo, 0) AS StockMinimo,
        p.PrecioCompra,
        p.PrecioVenta,
        p.Estado
    FROM Producto p
    LEFT JOIN Categoria c
        ON c.IdCategoriaPK = p.IdCategoriaFK
        AND c.Estado = 1
    LEFT JOIN Stock_Producto sp
        -- TODO: eliminar cast cuando se resuelva deuda técnica de FK short vs int
        ON sp.IdProductoFK = CAST(p.IdProductoPK AS SMALLINT)
        AND sp.Estado = 1
    WHERE (@IncluirInactivos = 1 OR p.Estado = 1)
      AND (@IdCategoria IS NULL OR p.IdCategoriaFK = @IdCategoria)
      AND (
          @TieneTermino = 0
          OR p.Codigo LIKE @TerminoLike
          OR p.Nombre LIKE @TerminoLike
      );

    -- Result set 1: total
    SELECT COUNT(*) AS TotalRegistros FROM @ProductosFiltrados;

    -- Result set 2: página con ORDER BY dinámico
    SELECT
        IdProductoPK,
        Codigo,
        Nombre,
        CategoriaNombre,
        Existencias,
        StockMinimo,
        PrecioCompra,
        PrecioVenta,
        Estado
    FROM @ProductosFiltrados
    ORDER BY
        CASE WHEN @Col = 'codigo'       AND @Dir = 'ASC'  THEN Codigo       END ASC,
        CASE WHEN @Col = 'codigo'       AND @Dir = 'DESC' THEN Codigo       END DESC,
        CASE WHEN @Col = 'nombre'       AND @Dir = 'ASC'  THEN Nombre       END ASC,
        CASE WHEN @Col = 'nombre'       AND @Dir = 'DESC' THEN Nombre       END DESC,
        CASE WHEN @Col = 'categoria'    AND @Dir = 'ASC'  THEN CategoriaNombre END ASC,
        CASE WHEN @Col = 'categoria'    AND @Dir = 'DESC' THEN CategoriaNombre END DESC,
        CASE WHEN @Col = 'existencias'  AND @Dir = 'ASC'  THEN Existencias  END ASC,
        CASE WHEN @Col = 'existencias'  AND @Dir = 'DESC' THEN Existencias  END DESC,
        CASE WHEN @Col = 'preciocompra' AND @Dir = 'ASC'  THEN PrecioCompra END ASC,
        CASE WHEN @Col = 'preciocompra' AND @Dir = 'DESC' THEN PrecioCompra END DESC,
        CASE WHEN @Col = 'precioventa'  AND @Dir = 'ASC'  THEN PrecioVenta  END ASC,
        CASE WHEN @Col = 'precioventa'  AND @Dir = 'DESC' THEN PrecioVenta  END DESC,
        CASE WHEN @Col = 'estado'       AND @Dir = 'ASC'  THEN CAST(Estado AS INT) END ASC,
        CASE WHEN @Col = 'estado'       AND @Dir = 'DESC' THEN CAST(Estado AS INT) END DESC,
        Nombre ASC  -- desempate estable
    OFFSET @Offset ROWS
    FETCH NEXT @TamanioPagina ROWS ONLY;
END
GO

-- =============================================
-- Sprint B — Modal Crear/Editar/Eliminar
-- =============================================

-- SP_ListarProveedoresActivos
-- Alimenta el dropdown de proveedor (opcional) en el modal
CREATE OR ALTER PROCEDURE SP_ListarProveedoresActivos
AS
BEGIN
    SET NOCOUNT ON;
    SELECT IdProveedorPK, Nombre FROM Proveedor
    WHERE Estado = 1 ORDER BY Nombre ASC;
END
GO

-- SP_ObtenerSiguienteCodigoProducto
-- Correlativo sugerido P-0001, P-0002, ... calculado al abrir el modal Crear
CREATE OR ALTER PROCEDURE SP_ObtenerSiguienteCodigoProducto
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Siguiente INT = 1;
    SELECT @Siguiente = ISNULL(MAX(CAST(SUBSTRING(Codigo, 3, 10) AS INT)), 0) + 1
    FROM Producto
    WHERE Codigo LIKE 'P-[0-9]%'
      AND ISNUMERIC(SUBSTRING(Codigo, 3, 10)) = 1;
    SELECT 'P-' + RIGHT('0000' + CAST(@Siguiente AS VARCHAR(10)), 4) AS CodigoSugerido;
END
GO

-- SP_ValidarCodigoUnicoProducto
-- @IdExcluir: en Editar, excluye el propio producto de la comprobación
CREATE OR ALTER PROCEDURE SP_ValidarCodigoUnicoProducto
    @Codigo NVARCHAR(50),
    @IdExcluir INT = 0
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM Producto WHERE Codigo = @Codigo AND IdProductoPK <> @IdExcluir)
        SELECT 0 AS Disponible;
    ELSE
        SELECT 1 AS Disponible;
END
GO

-- SP_ObtenerProductoParaEdicion
-- Datos completos de un producto (incluye stock actual) para hidratar el modal Editar
CREATE OR ALTER PROCEDURE SP_ObtenerProductoParaEdicion
    @IdProductoPK INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        p.IdProductoPK, p.Codigo, p.Nombre,
        p.IdCategoriaFK, p.IdProveedorFK,
        p.PrecioCompra, p.PrecioVenta,
        p.AplicaIVA, p.PorcentajeIVA, p.Estado,
        ISNULL(sp.Stock, 0) AS StockActual,
        ISNULL(sp.Stock_Minimo, 0) AS StockMinimo
    FROM Producto p
    LEFT JOIN Stock_Producto sp
        -- TODO: eliminar cast cuando se resuelva deuda técnica de FK short vs int
        ON sp.IdProductoFK = CAST(p.IdProductoPK AS SMALLINT)
        AND sp.Estado = 1
    WHERE p.IdProductoPK = @IdProductoPK;
END
GO

-- SP_CrearProducto
-- Retorna el nuevo IdProductoPK
CREATE OR ALTER PROCEDURE SP_CrearProducto
    @Codigo NVARCHAR(50), @Nombre NVARCHAR(150),
    @IdCategoriaFK SMALLINT, @IdProveedorFK INT = NULL,
    @PrecioCompra DECIMAL(18,2), @PrecioVenta DECIMAL(18,2),
    @AplicaIVA BIT, @PorcentajeIVA DECIMAL(5,2)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Producto (Codigo, Nombre, IdCategoriaFK, IdProveedorFK,
                          PrecioCompra, PrecioVenta, AplicaIVA, PorcentajeIVA, Estado)
    VALUES (@Codigo, @Nombre, @IdCategoriaFK, @IdProveedorFK,
            @PrecioCompra, @PrecioVenta, @AplicaIVA, @PorcentajeIVA, 1);
    SELECT CAST(SCOPE_IDENTITY() AS INT) AS NuevoId;
END
GO

-- SP_CrearStockInicialProducto
-- Se crea automáticamente con Stock=1 al dar de alta un producto (Sprint B regla 7)
CREATE OR ALTER PROCEDURE SP_CrearStockInicialProducto
    @IdProductoFK SMALLINT, @StockMinimo SMALLINT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Stock_Producto (IdProductoFK, Stock, Stock_Minimo, Estado)
    VALUES (@IdProductoFK, 1, @StockMinimo, 1);
END
GO

-- SP_ActualizarProducto
-- No toca Codigo (readonly) ni Stock (se ajusta desde Movimientos); retorna filas afectadas
CREATE OR ALTER PROCEDURE SP_ActualizarProducto
    @IdProductoPK INT, @Nombre NVARCHAR(150),
    @IdCategoriaFK SMALLINT, @IdProveedorFK INT = NULL,
    @PrecioCompra DECIMAL(18,2), @PrecioVenta DECIMAL(18,2),
    @AplicaIVA BIT, @PorcentajeIVA DECIMAL(5,2), @Estado BIT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Producto
    SET Nombre = @Nombre, IdCategoriaFK = @IdCategoriaFK, IdProveedorFK = @IdProveedorFK,
        PrecioCompra = @PrecioCompra, PrecioVenta = @PrecioVenta,
        AplicaIVA = @AplicaIVA, PorcentajeIVA = @PorcentajeIVA, Estado = @Estado
    WHERE IdProductoPK = @IdProductoPK;
    SELECT @@ROWCOUNT AS FilasAfectadas;
END
GO

-- SP_ActualizarStockMinimoProducto
-- Actualiza (o crea, si el producto aún no tiene registro de stock) solo Stock_Minimo
CREATE OR ALTER PROCEDURE SP_ActualizarStockMinimoProducto
    @IdProductoFK SMALLINT, @StockMinimo SMALLINT
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM Stock_Producto WHERE IdProductoFK = @IdProductoFK AND Estado = 1)
        UPDATE Stock_Producto SET Stock_Minimo = @StockMinimo
        WHERE IdProductoFK = @IdProductoFK AND Estado = 1;
    ELSE
        INSERT INTO Stock_Producto (IdProductoFK, Stock, Stock_Minimo, Estado)
        VALUES (@IdProductoFK, 0, @StockMinimo, 1);
    SELECT @@ROWCOUNT AS FilasAfectadas;
END
GO

-- SP_EliminarProducto
-- Soft-delete de Producto + Stock_Producto en una transacción.
-- Retorna: -1 tiene movimientos registrados | 0 no encontrado (o ya inactivo) | 1 eliminado
CREATE OR ALTER PROCEDURE SP_EliminarProducto
    @IdProductoPK INT
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM DetalleMovimiento
               -- TODO: eliminar cast cuando se resuelva deuda técnica de FK short vs int
               WHERE IdProductoFK = CAST(@IdProductoPK AS SMALLINT) AND Estado = 1)
    BEGIN SELECT -1 AS Resultado; RETURN; END

    IF NOT EXISTS (SELECT 1 FROM Producto WHERE IdProductoPK = @IdProductoPK AND Estado = 1)
    BEGIN SELECT 0 AS Resultado; RETURN; END

    BEGIN TRAN;
        UPDATE Producto SET Estado = 0 WHERE IdProductoPK = @IdProductoPK;
        UPDATE Stock_Producto SET Estado = 0
        -- TODO: eliminar cast cuando se resuelva deuda técnica de FK short vs int
        WHERE IdProductoFK = CAST(@IdProductoPK AS SMALLINT);
    COMMIT TRAN;
    SELECT 1 AS Resultado;
END
GO

-- =============================================
-- Sprint C — Exportar a Excel
-- =============================================

-- SP_ListarProductosParaExportar
-- Mismos filtros y mismo whitelist de sort que SP_ListarProductos, pero sin paginar
-- (sin OFFSET/FETCH) y con las columnas adicionales que necesita el Excel (Proveedor, IVA).
CREATE OR ALTER PROCEDURE SP_ListarProductosParaExportar
    @Termino NVARCHAR(100) = NULL,
    @IdCategoria SMALLINT = NULL,
    @IncluirInactivos BIT = 0,
    @OrdenarPor NVARCHAR(20) = 'nombre',
    @Direccion NVARCHAR(4) = 'ASC'
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @TerminoLike NVARCHAR(102) = '%' + ISNULL(@Termino, '') + '%';
    DECLARE @TieneTermino BIT = CASE WHEN ISNULL(@Termino, '') = '' THEN 0 ELSE 1 END;
    DECLARE @Dir NVARCHAR(4) = CASE WHEN UPPER(ISNULL(@Direccion, 'ASC')) = 'DESC' THEN 'DESC' ELSE 'ASC' END;

    -- Whitelist de columnas ordenables (defensa contra sort injection)
    DECLARE @Col NVARCHAR(20) = LOWER(ISNULL(@OrdenarPor, 'nombre'));
    IF @Col NOT IN ('codigo','nombre','categoria','existencias','preciocompra','precioventa','estado')
        SET @Col = 'nombre';

    ;WITH ProductosFiltrados AS (
        SELECT
            p.Codigo,
            p.Nombre,
            ISNULL(c.Nombre, N'(sin categoría)') AS CategoriaNombre,
            ISNULL(pr.Nombre, N'(sin proveedor)') AS ProveedorNombre,
            ISNULL(sp.Stock, 0) AS Existencias,
            ISNULL(sp.Stock_Minimo, 0) AS StockMinimo,
            p.PrecioCompra,
            p.PrecioVenta,
            p.AplicaIVA,
            p.PorcentajeIVA,
            p.Estado
        FROM Producto p
        LEFT JOIN Categoria c
            ON c.IdCategoriaPK = p.IdCategoriaFK
            AND c.Estado = 1
        LEFT JOIN Proveedor pr
            ON pr.IdProveedorPK = p.IdProveedorFK
            AND pr.Estado = 1
        LEFT JOIN Stock_Producto sp
            -- TODO: eliminar cast cuando se resuelva deuda técnica de FK short vs int
            ON sp.IdProductoFK = CAST(p.IdProductoPK AS SMALLINT)
            AND sp.Estado = 1
        WHERE (@IncluirInactivos = 1 OR p.Estado = 1)
          AND (@IdCategoria IS NULL OR p.IdCategoriaFK = @IdCategoria)
          AND (
              @TieneTermino = 0
              OR p.Codigo LIKE @TerminoLike
              OR p.Nombre LIKE @TerminoLike
          )
    )
    SELECT
        Codigo, Nombre, CategoriaNombre, ProveedorNombre,
        Existencias, StockMinimo, PrecioCompra, PrecioVenta,
        AplicaIVA, PorcentajeIVA, Estado
    FROM ProductosFiltrados
    ORDER BY
        CASE WHEN @Col = 'codigo'       AND @Dir = 'ASC'  THEN Codigo       END ASC,
        CASE WHEN @Col = 'codigo'       AND @Dir = 'DESC' THEN Codigo       END DESC,
        CASE WHEN @Col = 'nombre'       AND @Dir = 'ASC'  THEN Nombre       END ASC,
        CASE WHEN @Col = 'nombre'       AND @Dir = 'DESC' THEN Nombre       END DESC,
        CASE WHEN @Col = 'categoria'    AND @Dir = 'ASC'  THEN CategoriaNombre END ASC,
        CASE WHEN @Col = 'categoria'    AND @Dir = 'DESC' THEN CategoriaNombre END DESC,
        CASE WHEN @Col = 'existencias'  AND @Dir = 'ASC'  THEN Existencias  END ASC,
        CASE WHEN @Col = 'existencias'  AND @Dir = 'DESC' THEN Existencias  END DESC,
        CASE WHEN @Col = 'preciocompra' AND @Dir = 'ASC'  THEN PrecioCompra END ASC,
        CASE WHEN @Col = 'preciocompra' AND @Dir = 'DESC' THEN PrecioCompra END DESC,
        CASE WHEN @Col = 'precioventa'  AND @Dir = 'ASC'  THEN PrecioVenta  END ASC,
        CASE WHEN @Col = 'precioventa'  AND @Dir = 'DESC' THEN PrecioVenta  END DESC,
        CASE WHEN @Col = 'estado'       AND @Dir = 'ASC'  THEN CAST(Estado AS INT) END ASC,
        CASE WHEN @Col = 'estado'       AND @Dir = 'DESC' THEN CAST(Estado AS INT) END DESC,
        Nombre ASC;
END
GO
