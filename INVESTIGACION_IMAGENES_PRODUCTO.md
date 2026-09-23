# Investigación: superficie de Producto y storage estático del Web

Investigación de solo lectura para preparar el sprint de galería de imágenes de Producto (tabla `ProductoImagen`, upload con conversión WEBP + thumbnail, servicio de archivos estáticos). No se modificó ningún archivo ni la base de datos.

Fecha de ejecución: 2026-09-17. BD consultada: `GestionProductoBD` en `DESKTOP-TF2SLSI\SQLEXPRESS` (dev local).

---

## 1. SPs de Producto en BD

Consulta ejecutada:

```sql
SELECT name, create_date, modify_date
FROM sys.procedures
WHERE name LIKE '%Producto%'
ORDER BY name;
```

Resultado — 27 stored procedures:

| name | create_date | modify_date |
|---|---|---|
| sp_ActualizarProducto | 2026-08-20 10:33:06.520 | 2026-09-10 22:29:03.587 |
| SP_ActualizarStockMinimoProducto | 2026-09-10 08:25:51.057 | 2026-09-10 22:29:03.590 |
| SP_ActualizarStockProducto | 2026-09-10 00:36:06.317 | 2026-09-10 00:59:12.237 |
| sp_BuscarProducto | 2026-08-20 10:33:20.363 | 2026-08-20 10:51:11.453 |
| SP_BuscarProductosParaMovimiento | 2026-09-10 00:36:06.303 | 2026-09-10 00:59:12.227 |
| sp_BuscarStockProducto | 2026-05-25 16:38:33.937 | 2026-06-10 13:29:24.260 |
| SP_ContarProductosBajoStock | 2026-09-08 09:08:08.957 | 2026-09-08 09:08:08.957 |
| SP_CrearProducto | 2026-09-10 08:25:51.040 | 2026-09-10 22:29:03.573 |
| SP_CrearStockInicialProducto | 2026-09-10 08:25:51.043 | 2026-09-10 22:29:03.580 |
| SP_CrearStockProducto | 2026-09-10 00:36:06.317 | 2026-09-10 00:59:12.237 |
| sp_EliminarLogicoProducto | 2026-05-25 16:31:59.573 | 2026-06-10 13:29:17.010 |
| sp_EliminarLogicoStockProducto | 2026-05-25 16:33:08.557 | 2026-06-10 13:29:17.007 |
| SP_EliminarProducto | 2026-09-10 08:25:51.063 | 2026-09-10 22:29:03.600 |
| SP_InsertarProducto | 2026-05-22 09:49:39.830 | 2026-08-20 10:32:24.067 |
| SP_InsertarStock_Producto | 2026-05-22 09:49:39.857 | 2026-06-10 13:28:06.590 |
| SP_ListarProducto | 2026-05-25 10:39:59.553 | 2026-08-20 10:32:18.050 |
| SP_ListarProductos | 2026-09-10 07:59:13.023 | 2026-09-10 22:29:03.497 |
| SP_ListarProductosParaExportar | 2026-09-10 22:29:03.613 | 2026-09-10 22:29:03.613 |
| SP_ListarStock_Producto | 2026-05-25 10:39:59.567 | 2026-05-25 10:39:59.567 |
| SP_ObtenerProductoParaEdicion | 2026-09-10 08:25:51.030 | 2026-09-10 22:29:03.570 |
| SP_ObtenerSiguienteCodigoProducto | 2026-09-10 08:25:51.020 | 2026-09-10 22:29:03.557 |
| SP_ObtenerStockProducto | 2026-09-10 00:36:06.307 | 2026-09-10 00:59:12.230 |
| SP_ProductoMasVendidoMes | 2026-09-08 09:08:16.317 | 2026-09-08 09:08:16.317 |
| SP_ReporteProductosMasVendidos | 2026-05-25 09:31:53.517 | 2026-05-25 09:31:53.517 |
| SP_ValidarCodigoUnicoProducto | 2026-09-10 08:25:51.023 | 2026-09-10 22:29:03.563 |
| usp_ActualizarProducto | 2026-05-25 11:20:27.720 | 2026-06-10 13:29:08.987 |
| usp_ActualizarStockProducto | 2026-05-25 11:20:27.703 | 2026-06-10 13:29:08.977 |

### Presencia en `ESFE.GestionProductos.DAL/Scripts/`

**Con archivo `.sql` en el repo:**

| SP | Archivo |
|---|---|
| SP_ListarProductos | `Producto_StoredProcedures.sql` |
| SP_ListarProductosParaExportar | `Producto_StoredProcedures.sql` |
| SP_ObtenerProductoParaEdicion | `Producto_StoredProcedures.sql` |
| SP_ObtenerSiguienteCodigoProducto | `Producto_StoredProcedures.sql` |
| SP_ValidarCodigoUnicoProducto | `Producto_StoredProcedures.sql` |
| SP_CrearProducto | `Producto_StoredProcedures.sql` |
| SP_CrearStockInicialProducto | `Producto_StoredProcedures.sql` |
| sp_ActualizarProducto (script lo declara `SP_ActualizarProducto`; SQL Server no distingue mayúsculas por collation — mismo objeto) | `Producto_StoredProcedures.sql` |
| SP_ActualizarStockMinimoProducto | `Producto_StoredProcedures.sql` |
| SP_EliminarProducto | `Producto_StoredProcedures.sql` |
| SP_BuscarProductosParaMovimiento | `Movimiento_StoredProcedures.sql` |
| SP_ObtenerStockProducto | `Movimiento_StoredProcedures.sql` |
| SP_ActualizarStockProducto | `Movimiento_StoredProcedures.sql` |
| SP_CrearStockProducto | `Movimiento_StoredProcedures.sql` |
| SP_ContarProductosBajoStock | `Dashboard_StoredProcedures.sql` |
| SP_ProductoMasVendidoMes | `Dashboard_StoredProcedures.sql` |

**Solo viven en BD (sin `.sql` en el repo):**

- `sp_BuscarProducto`
- `sp_BuscarStockProducto`
- `sp_EliminarLogicoProducto`
- `sp_EliminarLogicoStockProducto`
- `SP_InsertarProducto`
- `SP_InsertarStock_Producto`
- `SP_ListarProducto`
- `SP_ListarStock_Producto`
- `SP_ReporteProductosMasVendidos`
- `usp_ActualizarProducto`
- `usp_ActualizarStockProducto`

### Cuerpo completo de cada SP (`sp_helptext`)

```sql
-- sp_ActualizarProducto
-- SP_ActualizarProducto
-- No toca Codigo (readonly) ni Stock (se ajusta desde Movimientos); retorna filas afectadas
CREATE   PROCEDURE SP_ActualizarProducto
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
```

```sql
-- SP_ActualizarStockMinimoProducto
-- Actualiza (o crea, si el producto aún no tiene registro de stock) solo Stock_Minimo
CREATE   PROCEDURE SP_ActualizarStockMinimoProducto
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
```

```sql
-- SP_ActualizarStockProducto
-- Suma o resta según @Delta (positivo entrada, negativo salida)
-- Retorna filas afectadas
CREATE   PROCEDURE SP_ActualizarStockProducto
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
```

```sql
-- sp_BuscarProducto
CREATE PROCEDURE sp_BuscarProducto
    @Nombre       VARCHAR(100) = NULL,
    @Codigo       VARCHAR(20)  = NULL,
    @IdProductoPK SMALLINT     = NULL
AS
BEGIN
    SELECT
        IdProductoPK, Codigo, Nombre, Descripcion,
        PrecioCompra, PrecioVenta, PorcentajeIVA, AplicaIVA,
        IdProveedorFK, IdCategoriaFK, Estado
    FROM Producto
    WHERE (@Nombre       IS NULL OR Nombre LIKE '%' + @Nombre + '%')
      AND (@Codigo       IS NULL OR Codigo LIKE '%' + @Codigo + '%')
      AND (@IdProductoPK IS NULL OR IdProductoPK = @IdProductoPK);
END;
```

```sql
-- SP_BuscarProductosParaMovimiento
-- Autocomplete: busca por Código o Nombre (LIKE), solo activos, top 10
-- Incluye stock actual (0 si no hay registro en Stock_Producto)
CREATE   PROCEDURE SP_BuscarProductosParaMovimiento
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
```

```sql
-- sp_BuscarStockProducto
CREATE PROCEDURE sp_BuscarStockProducto
    @IdProductoFK INT = NULL,
    @StockMinimo INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT IdStock_ProductoPK, IdProductoFK, Stock, Stock_Minimo, Estado
    FROM Stock_Producto
    WHERE (@IdProductoFK IS NULL OR IdProductoFK = @IdProductoFK)
      AND (@StockMinimo IS NULL OR Stock_Minimo = @StockMinimo);
END;
```

```sql
-- SP_ContarProductosBajoStock
CREATE   PROCEDURE SP_ContarProductosBajoStock
AS
BEGIN
    SET NOCOUNT ON;

    SELECT COUNT(*) AS Total
    FROM Stock_Producto sp
    INNER JOIN Producto p ON sp.IdProductoFK = p.IdProductoPK
    WHERE sp.Estado = 1 AND p.Estado = 1
      AND ISNULL(sp.Stock, 0) < ISNULL(sp.Stock_Minimo, 0);
END
```

```sql
-- SP_CrearProducto
-- Retorna el nuevo IdProductoPK
CREATE   PROCEDURE SP_CrearProducto
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
```

```sql
-- SP_CrearStockInicialProducto
-- Se crea automáticamente con Stock=1 al dar de alta un producto (Sprint B regla 7)
CREATE   PROCEDURE SP_CrearStockInicialProducto
    @IdProductoFK SMALLINT, @StockMinimo SMALLINT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Stock_Producto (IdProductoFK, Stock, Stock_Minimo, Estado)
    VALUES (@IdProductoFK, 1, @StockMinimo, 1);
END
```

```sql
-- SP_CrearStockProducto
-- Para productos que aún no tienen registro (solo se usa en Entrada)
CREATE   PROCEDURE SP_CrearStockProducto
    @IdProductoFK SMALLINT,
    @Stock SMALLINT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Stock_Producto (IdProductoFK, Stock, Stock_Minimo, Estado)
    VALUES (@IdProductoFK, @Stock, 0, 1);
END
```

```sql
-- sp_EliminarLogicoProducto
CREATE PROCEDURE sp_EliminarLogicoProducto @IdProducto INT
AS BEGIN SET NOCOUNT ON; UPDATE Producto SET Estado = 0 WHERE IdProductoPK = @IdProducto; END;
```

```sql
-- sp_EliminarLogicoStockProducto
CREATE PROCEDURE sp_EliminarLogicoStockProducto @IdStockProducto INT
AS BEGIN SET NOCOUNT ON; UPDATE Stock_Producto SET Estado = 0 WHERE IdStock_ProductoPK = @IdStockProducto; END;
```

```sql
-- SP_EliminarProducto
-- Soft-delete de Producto + Stock_Producto en una transacción.
-- Retorna: -1 tiene movimientos registrados | 0 no encontrado (o ya inactivo) | 1 eliminado
CREATE   PROCEDURE SP_EliminarProducto
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
```

```sql
-- SP_InsertarProducto
CREATE PROCEDURE SP_InsertarProducto
    @Codigo         VARCHAR(20),
    @Nombre         VARCHAR(100),
    @Descripcion    VARCHAR(255),
    @PrecioCompra   FLOAT,
    @PrecioVenta    FLOAT,
    @PorcentajeIVA  FLOAT,
    @AplicaIVA      BIT,
    @IdProveedorFK  SMALLINT,
    @IdCategoriaFK  SMALLINT,
    @Estado         BIT
AS
BEGIN
    INSERT INTO Producto
    (Codigo, Nombre, Descripcion, PrecioCompra, PrecioVenta,
     PorcentajeIVA, AplicaIVA, IdProveedorFK, IdCategoriaFK, Estado)
    VALUES
    (@Codigo, @Nombre, @Descripcion, @PrecioCompra, @PrecioVenta,
     @PorcentajeIVA, @AplicaIVA, @IdProveedorFK, @IdCategoriaFK, @Estado);
END;
```

```sql
-- SP_InsertarStock_Producto
CREATE PROCEDURE SP_InsertarStock_Producto
    @IdProductoFK INT,
    @Stock INT,
    @Stock_Minimo INT
AS
BEGIN
    INSERT INTO Stock_Producto (IdProductoFK, Stock, Stock_Minimo)
    VALUES (@IdProductoFK, @Stock, @Stock_Minimo);
END;
```

```sql
-- SP_ListarProducto
CREATE PROCEDURE SP_ListarProducto
AS
BEGIN
    SELECT
        P.IdProductoPK,
        P.Codigo,
        P.Nombre,
        P.Descripcion,
        P.PrecioCompra,
        P.PrecioVenta,
        P.PorcentajeIVA,
        P.AplicaIVA,
        PROV.Empresa AS Proveedor,
        CAT.Nombre   AS Categoria,
        P.Estado
    FROM Producto AS P
    LEFT JOIN Proveedor AS PROV ON P.IdProveedorFK = PROV.IdProveedorPK
    LEFT JOIN Categoria AS CAT  ON P.IdCategoriaFK = CAT.IdCategoriaPK;
END;
```

```sql
-- SP_ListarProductos
-- Listado paginado con filtros y sort dinámico.
-- Retorna DOS result sets: (1) TotalRegistros, (2) página actual.
CREATE   PROCEDURE SP_ListarProductos
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
```

```sql
-- SP_ListarProductosParaExportar
-- Mismos filtros y mismo whitelist de sort que SP_ListarProductos, pero sin paginar
-- (sin OFFSET/FETCH) y con las columnas adicionales que necesita el Excel (Proveedor, IVA).
CREATE   PROCEDURE SP_ListarProductosParaExportar
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
```

```sql
-- SP_ListarStock_Producto
CREATE PROCEDURE SP_ListarStock_Producto
AS
BEGIN
    SELECT
        S.IdStock_ProductoPK AS [ID Registro Stock],
        P.Nombre AS [Producto],
        S.Stock AS [Existencia Actual],
        S.Stock_Minimo AS [Existencia Mínima]
    FROM Stock_Producto AS S
    INNER JOIN Producto AS P ON S.IdProductoFK = P.IdProductoPK;
END;
```

```sql
-- SP_ObtenerProductoParaEdicion
-- Datos completos de un producto (incluye stock actual) para hidratar el modal Editar
CREATE   PROCEDURE SP_ObtenerProductoParaEdicion
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
```

```sql
-- SP_ObtenerSiguienteCodigoProducto
-- Correlativo sugerido P-0001, P-0002, ... calculado al abrir el modal Crear
CREATE   PROCEDURE SP_ObtenerSiguienteCodigoProducto
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
```

```sql
-- SP_ObtenerStockProducto
-- Retorna stock actual y si existe registro (para decidir crear/actualizar)
CREATE   PROCEDURE SP_ObtenerStockProducto
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
```

```sql
-- SP_ProductoMasVendidoMes
CREATE   PROCEDURE SP_ProductoMasVendidoMes
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @InicioMes DATETIME = DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1);

    SELECT TOP (1)
        p.Nombre AS Nombre,
        SUM(ISNULL(dm.Cantidad, 0)) AS TotalUnidades
    FROM DetalleMovimiento dm
    INNER JOIN MovimientoInventario mi ON dm.IdMovimientoFK = mi.IdMovimientoPK
    INNER JOIN Producto p ON dm.IdProductoFK = p.IdProductoPK
    WHERE mi.TipoMovimiento = 'Salida'
      AND mi.Fecha >= @InicioMes
      AND mi.Fecha <= GETDATE()
      AND dm.Estado = 1
    GROUP BY dm.IdProductoFK, p.Nombre
    ORDER BY SUM(ISNULL(dm.Cantidad, 0)) DESC;
END
```

```sql
-- SP_ReporteProductosMasVendidos
CREATE PROCEDURE SP_ReporteProductosMasVendidos
AS
BEGIN
    SELECT
        P.IdProductoPK AS [Código Producto],
        P.Nombre AS [Producto],
        CAT.Nombre AS [Categoría],
        SUM(DF.Cantidad) AS [Unidades Vendidas],
        SUM(DF.Cantidad * DF.PrecioUnitario) AS [Total Ingresos ($)]
    FROM DetalleFactura AS DF
    INNER JOIN Producto AS P ON DF.IdProductoFK = P.IdProductoPK
    INNER JOIN Categoria AS CAT ON P.IdCategoriaFK = CAT.IdCategoriaPK
    INNER JOIN Factura AS F ON DF.IdFacturaFK = F.IdFacturaPK
    WHERE F.Estado = 1 -- Solo tomar en cuenta facturas activas
    GROUP BY P.IdProductoPK, P.Nombre, CAT.Nombre
    ORDER BY [Unidades Vendidas] DESC;
END;
```

```sql
-- SP_ValidarCodigoUnicoProducto
-- @IdExcluir: en Editar, excluye el propio producto de la comprobación
CREATE   PROCEDURE SP_ValidarCodigoUnicoProducto
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
```

```sql
-- usp_ActualizarProducto
CREATE PROCEDURE usp_ActualizarProducto
    @IdProductoPK INT,
    @Nombre VARCHAR(100) = NULL,
    @Descripcion VARCHAR(255) = NULL,
    @PrecioCompra DECIMAL(18,2) = NULL,
    @PrecioVenta DECIMAL(18,2) = NULL,
    @PorcentajeIVA DECIMAL(5,2) = NULL,
    @AplicaIVA BIT = NULL,
    @IdProveedorFK INT = NULL,
    @IdCategoriaFK SMALLINT = NULL,
    @Estado BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        IF EXISTS (SELECT 1 FROM Producto WHERE IdProductoPK = @IdProductoPK)
        BEGIN
            UPDATE Producto
            SET Nombre = ISNULL(@Nombre, Nombre), Descripcion = ISNULL(@Descripcion, Descripcion), PrecioCompra = ISNULL(@PrecioCompra, PrecioCompra), PrecioVenta = ISNULL(@PrecioVenta, PrecioVenta), PorcentajeIVA = ISNULL(@PorcentajeIVA, PorcentajeIVA),
            AplicaIVA = ISNULL(@AplicaIVA, AplicaIVA), IdProveedorFK = ISNULL(@IdProveedorFK, IdProveedorFK), IdCategoriaFK = ISNULL(@IdCategoriaFK, IdCategoriaFK), Estado = ISNULL(@Estado, Estado)
            WHERE IdProductoPK = @IdProductoPK;
            COMMIT TRANSACTION;
        END
        ELSE BEGIN
            ROLLBACK TRANSACTION;
            RAISERROR('El IdProducto especificado no existe.', 16, 1);
        END
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrorMessage, 16, 1);
    END CATCH
END;
```

```sql
-- usp_ActualizarStockProducto
CREATE PROCEDURE usp_ActualizarStockProducto
    @IdStock_ProductoPK INT,
    @IdProductoFK INT = NULL,
    @Stock INT = NULL,
    @Stock_Minimo INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        IF EXISTS (SELECT 1 FROM Stock_Producto WHERE IdStock_ProductoPK = @IdStock_ProductoPK)
        BEGIN
            UPDATE Stock_Producto
            SET IdProductoFK = ISNULL(@IdProductoFK, IdProductoFK), Stock = ISNULL(@Stock, Stock), Stock_Minimo = ISNULL(@Stock_Minimo, Stock_Minimo)
            WHERE IdStock_ProductoPK = @IdStock_ProductoPK;
            COMMIT TRANSACTION;
        END
        ELSE BEGIN
            ROLLBACK TRANSACTION;
            RAISERROR('El IdStock_Producto especificado no existe.', 16, 1);
        END
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrorMessage, 16, 1);
    END CATCH
END;
```

---

## 2. Clase `ProductoDAL`

Ruta: `ESFE.GestionProductos.DAL/ProductoDAL.cs`

| Método | SP ejecutado | Parámetros | Retorno |
|---|---|---|---|
| `Listar()` | `SP_ListarProducto` | ninguno | `DataTable` |
| `Buscar(nombre, idProducto, codigo)` | `sp_BuscarProducto` | `@Nombre, @Codigo, @IdProductoPK` | `List<Producto>` |
| `Insertar(Producto producto)` | `SP_InsertarProducto` | todos los campos de `Producto` | `int` (filas afectadas) |
| `Actualizar(Producto producto)` | `sp_ActualizarProducto` | todos los campos de `Producto` + `@IdProductoPK` | `int` (filas afectadas) |
| `EliminarLogico(short idProducto)` | `sp_EliminarLogicoProducto` | `@IdProducto` | `int` |
| `ListarCategoriasActivas()` | `SP_ListarCategoriasActivas` | ninguno | `List<(short IdCategoriaPK, string Nombre)>` |
| `ListarProductos(termino, idCategoria, incluirInactivos, ordenarPor, direccion, pagina, tamanioPagina)` | `SP_ListarProductos` | 7 params | `(int TotalRegistros, List<(...)> Items)` |
| `ListarProveedoresActivos()` | `SP_ListarProveedoresActivos` | ninguno | `List<(int IdProveedorPK, string Nombre)>` |
| `ObtenerCodigoSugerido()` | `SP_ObtenerSiguienteCodigoProducto` | ninguno | `string` |
| `ValidarCodigoUnico(codigo, idExcluir)` | `SP_ValidarCodigoUnicoProducto` | `@Codigo, @IdExcluir` | `bool` |
| `ObtenerParaEdicion(int id)` | `SP_ObtenerProductoParaEdicion` | `@IdProductoPK` | tupla nullable con 12 campos |
| `CrearProducto(...)` | `SP_CrearProducto` | 8 params | `int` (nuevo Id) |
| `CrearStockInicial(idProductoFK, stockMinimo)` | `SP_CrearStockInicialProducto` | `@IdProductoFK, @StockMinimo` | `void` |
| `ActualizarProducto(...)` | `SP_ActualizarProducto` | 9 params | `int` (filas afectadas, vía `ExecuteScalar`) |
| `ActualizarStockMinimo(idProductoFK, stockMinimo)` | `SP_ActualizarStockMinimoProducto` | `@IdProductoFK, @StockMinimo` | `int` |
| `EliminarProducto(int id)` | `SP_EliminarProducto` | `@IdProductoPK` | `int` (-1/0/1) |
| `ListarParaExportar(termino, idCategoria, incluirInactivos, ordenarPor, direccion)` | `SP_ListarProductosParaExportar` | 5 params | `List<(11 campos)>` |

Cuerpo completo de `Insertar`:

```csharp
// Insertar Producto
public int Insertar(Producto producto)
{
    using (IDbConnection conexion = DBComun.ObtenerConexion())
    {
        conexion.Open();
        using (SqlCommand comando = new SqlCommand("SP_InsertarProducto", conexion as SqlConnection))
        {
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@Codigo", (object)producto.Codigo ?? DBNull.Value);
            comando.Parameters.AddWithValue("@Nombre", (object)producto.Nombre ?? DBNull.Value);
            comando.Parameters.AddWithValue("@Descripcion", (object)producto.Descripcion ?? DBNull.Value);
            comando.Parameters.AddWithValue("@PrecioCompra", (object)producto.PrecioCompra ?? DBNull.Value);
            comando.Parameters.AddWithValue("@PrecioVenta", (object)producto.PrecioVenta ?? DBNull.Value);
            comando.Parameters.AddWithValue("@PorcentajeIVA", (object)producto.PorcentajeIVA ?? DBNull.Value);
            comando.Parameters.AddWithValue("@AplicaIVA", (object)producto.AplicaIVA ?? DBNull.Value);
            comando.Parameters.AddWithValue("@IdProveedorFK", (object)producto.IdProveedorFK ?? DBNull.Value);
            comando.Parameters.AddWithValue("@IdCategoriaFK", (object)producto.IdCategoriaFK ?? DBNull.Value);
            comando.Parameters.AddWithValue("@Estado", (object)producto.Estado ?? true);

            return comando.ExecuteNonQuery();
        }
    }
}
```

Cuerpo completo de `Actualizar` (nótese la advertencia de código muerto en el comentario original del archivo):

```csharp
// Actualizar
// NOTA: comparte el mismo objeto de base de datos que SP_ActualizarProducto (Sprint B) —
// los nombres de SP en SQL Server no distinguen mayúsculas/minúsculas por collation, y este
// método ya no es invocado por ningún controlador (scaffolding legado de la sección "Listar Productos").
public int Actualizar(Producto producto)
{
    using (IDbConnection conexion = DBComun.ObtenerConexion())
    {
        conexion.Open();
        using (SqlCommand comando = new SqlCommand("sp_ActualizarProducto", conexion as SqlConnection))
        {
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@IdProductoPK", producto.IdProductoPK);
            comando.Parameters.AddWithValue("@Codigo", (object)producto.Codigo ?? DBNull.Value);
            comando.Parameters.AddWithValue("@Nombre", (object)producto.Nombre ?? DBNull.Value);
            comando.Parameters.AddWithValue("@Descripcion", (object)producto.Descripcion ?? DBNull.Value);
            comando.Parameters.AddWithValue("@PrecioCompra", (object)producto.PrecioCompra ?? DBNull.Value);
            comando.Parameters.AddWithValue("@PrecioVenta", (object)producto.PrecioVenta ?? DBNull.Value);
            comando.Parameters.AddWithValue("@PorcentajeIVA", (object)producto.PorcentajeIVA ?? DBNull.Value);
            comando.Parameters.AddWithValue("@AplicaIVA", (object)producto.AplicaIVA ?? DBNull.Value);
            comando.Parameters.AddWithValue("@IdProveedorFK", (object)producto.IdProveedorFK ?? DBNull.Value);
            comando.Parameters.AddWithValue("@IdCategoriaFK", (object)producto.IdCategoriaFK ?? DBNull.Value);
            comando.Parameters.AddWithValue("@Estado", (object)producto.Estado ?? true);

            return comando.ExecuteNonQuery();
        }
    }
}
```

> Nota: este `Actualizar(Producto)` invoca `sp_ActualizarProducto`, que en BD **es el mismo objeto** que `SP_ActualizarProducto` (case-insensitive collation) definido en `Producto_StoredProcedures.sql` bajo la firma nueva (9 parámetros sin `@Codigo`/`@Descripcion`). El código C# de este método pasa parámetros (`@Codigo`, `@Descripcion`, etc.) que **ya no existen** en la firma real del SP — este método quedaría roto si se invocara, pero no se invoca desde ningún controlador (confirmado en sección 10).

---

## 3. Clase `ProductoLN`

Ruta: `ESFE.GestionProductos.LN/ProductoLN.cs`

Firmas públicas:

```csharp
public DataTable Listar()
public List<Producto> Buscar(string nombre = null, short? idProducto = null, string codigo = null)
public int Guardar(Producto producto)
public int Actualizar(Producto producto)
public int Insertar(Producto producto)
public int EliminarLogico(short idProducto)
public List<CategoriaFiltroDTO> ListarCategoriasActivas()
public ResultadoPaginadoDTO<ProductoListadoDTO> ListarProductos(FiltrosProductoDTO filtros)
public List<ProveedorFiltroDTO> ListarProveedoresActivos()
public DatosNuevoProductoDTO ObtenerDatosNuevoProducto()
public ProductoEdicionDTO? ObtenerParaEdicion(int id)
public ResultadoGuardarProductoDTO Crear(ProductoFormDTO form)
public ResultadoGuardarProductoDTO Actualizar(ProductoFormDTO form)   // sobrecarga distinta de Actualizar(Producto)
public ResultadoEliminarProductoDTO Eliminar(int id)
public List<ProductoExportDTO> ListarParaExportar(FiltrosProductoDTO filtros)
```

`Crear` completo (usado por el POST `/Producto/Crear` del controlador):

```csharp
public ResultadoGuardarProductoDTO Crear(ProductoFormDTO form)
{
    var error = ValidarFormulario(form);
    if (error != null)
        return error;

    if (!productoDAL.ValidarCodigoUnico(form.Codigo, 0))
        return new ResultadoGuardarProductoDTO
        {
            Resultado = ResultadoGuardarProducto.CodigoDuplicado,
            Mensaje = "El código ya existe. Vuelve a abrir el modal para obtener uno nuevo."
        };

    try
    {
        using var scope = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted },
            TransactionScopeAsyncFlowOption.Enabled);

        decimal porcentajeIVA = form.AplicaIVA ? form.PorcentajeIVA : 0m;

        int nuevoId = productoDAL.CrearProducto(
            form.Codigo.Trim(), form.Nombre.Trim(), form.IdCategoriaFK, form.IdProveedorFK,
            form.PrecioCompra, form.PrecioVenta, form.AplicaIVA, porcentajeIVA);

        // TODO: alinear a int cuando se resuelva deuda técnica de FK short vs int
        productoDAL.CrearStockInicial((short)nuevoId, form.StockMinimo);

        scope.Complete();

        return new ResultadoGuardarProductoDTO { Resultado = ResultadoGuardarProducto.Ok, IdGenerado = nuevoId };
    }
    catch (Exception ex)
    {
        return new ResultadoGuardarProductoDTO { Resultado = ResultadoGuardarProducto.ErrorInterno, Mensaje = ex.Message };
    }
}
```

`Actualizar(ProductoFormDTO form)` completo (usado por el POST `/Producto/Editar`):

```csharp
public ResultadoGuardarProductoDTO Actualizar(ProductoFormDTO form)
{
    if (!form.IdProductoPK.HasValue || form.IdProductoPK.Value <= 0)
        return new ResultadoGuardarProductoDTO
        {
            Resultado = ResultadoGuardarProducto.DatosInvalidos,
            Mensaje = "ID de producto inválido"
        };

    var error = ValidarFormulario(form);
    if (error != null)
        return error;

    if (!productoDAL.ValidarCodigoUnico(form.Codigo, form.IdProductoPK.Value))
        return new ResultadoGuardarProductoDTO
        {
            Resultado = ResultadoGuardarProducto.CodigoDuplicado,
            Mensaje = "El código ya está en uso por otro producto"
        };

    try
    {
        using var scope = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted },
            TransactionScopeAsyncFlowOption.Enabled);

        decimal porcentajeIVA = form.AplicaIVA ? form.PorcentajeIVA : 0m;

        int filas = productoDAL.ActualizarProducto(
            form.IdProductoPK.Value, form.Nombre.Trim(), form.IdCategoriaFK, form.IdProveedorFK,
            form.PrecioCompra, form.PrecioVenta, form.AplicaIVA, porcentajeIVA, form.Estado);

        if (filas == 0)
            return new ResultadoGuardarProductoDTO
            {
                Resultado = ResultadoGuardarProducto.NoEncontrado,
                Mensaje = "Producto no encontrado"
            };

        // TODO: alinear a int cuando se resuelva deuda técnica de FK short vs int
        productoDAL.ActualizarStockMinimo((short)form.IdProductoPK.Value, form.StockMinimo);

        scope.Complete();

        return new ResultadoGuardarProductoDTO { Resultado = ResultadoGuardarProducto.Ok, IdGenerado = form.IdProductoPK };
    }
    catch (Exception ex)
    {
        return new ResultadoGuardarProductoDTO { Resultado = ResultadoGuardarProducto.ErrorInterno, Mensaje = ex.Message };
    }
}
```

> Nota: `Crear`/`Actualizar(ProductoFormDTO)` usan `TransactionScope` (2 llamadas a SPs distintos dentro de la misma transacción distribuida/local). Si el sprint de imágenes necesita persistir el registro en `ProductoImagen` dentro del mismo flujo transaccional de creación de producto, este es el patrón a replicar (aunque probablemente el alta de imágenes ocurra en un endpoint separado, después de tener el `IdProductoPK`).

---

## 4. DTOs de Producto

Todos en `ESFE.GestionProductos.LN/DTOs/`:

| Archivo | Propiedades |
|---|---|
| `ProductoListadoDTO.cs` | `IdProductoPK (int)`, `Codigo (string)`, `Nombre (string)`, `CategoriaNombre (string)`, `Existencias (int)`, `StockMinimo (int)`, `PrecioCompra (decimal)`, `PrecioVenta (decimal)`, `Estado (bool)` |
| `ProductoBusquedaDTO.cs` | `IdProductoPK (int)`, `Codigo (string)`, `Nombre (string)`, `StockActual (int)` |
| `ProductoFormDTO.cs` | `IdProductoPK (int?)`, `Codigo (string)`, `Nombre (string)`, `IdCategoriaFK (short)`, `IdProveedorFK (int?)`, `PrecioCompra (decimal)`, `PrecioVenta (decimal)`, `AplicaIVA (bool)`, `PorcentajeIVA (decimal)`, `StockMinimo (short)`, `Estado (bool)` |
| `ProductoEdicionDTO.cs` | `IdProductoPK (int)`, `Codigo (string)`, `Nombre (string)`, `IdCategoriaFK (short?)`, `IdProveedorFK (int?)`, `PrecioCompra (decimal)`, `PrecioVenta (decimal)`, `AplicaIVA (bool)`, `PorcentajeIVA (decimal)`, `Estado (bool)`, `StockActual (int)`, `StockMinimo (int)` |
| `DatosNuevoProductoDTO.cs` | `CodigoSugerido (string)`, `Categorias (List<CategoriaFiltroDTO>)`, `Proveedores (List<ProveedorFiltroDTO>)` |
| `ResultadoGuardarProductoDTO.cs` | `Resultado (ResultadoGuardarProducto)`, `Mensaje (string?)`, `IdGenerado (int?)` |
| `ResultadoEliminarProductoDTO.cs` | `Resultado (ResultadoEliminarProducto)`, `Mensaje (string?)` |
| `ProductoExportDTO.cs` | `Codigo`, `Nombre`, `CategoriaNombre`, `ProveedorNombre` (todos `string`), `Existencias (int)`, `StockMinimo (int)`, `PrecioCompra (decimal)`, `PrecioVenta (decimal)`, `AplicaIVA (bool)`, `PorcentajeIVA (decimal)`, `Estado (bool)` |
| `FiltrosProductoDTO.cs` | `Termino (string?)`, `IdCategoria (short?)`, `IncluirInactivos (bool = false)`, `OrdenarPor (string = "nombre")`, `Direccion (string = "ASC")`, `Pagina (int = 1)`, `TamanioPagina (int = 25)` |
| `CategoriaFiltroDTO.cs` | `IdCategoriaPK (short)`, `Nombre (string)` |
| `ProveedorFiltroDTO.cs` | `IdProveedorPK (int)`, `Nombre (string)` |
| `ResultadoPaginadoDTO<T>.cs` (genérico, no específico de Producto pero usado por el listado) | `Items (List<T>)`, `TotalRegistros (int)`, `Pagina (int)`, `TamanioPagina (int)`, `TotalPaginas` (calculada) |

**Respuesta a la pregunta del enunciado:**
- Sí existe `ProductoListadoDTO` — es el DTO que consume el listado (`ProductoController.Listar` → `ResultadoPaginadoDTO<ProductoListadoDTO>`, renderizado por `renderTabla()` en el JS de `Index.cshtml`).
- Sí existe `ProductoEdicionDTO` — es el DTO que hidrata el modal Editar (`ProductoController.ObtenerParaEdicion`).
- Ninguno de los dos tiene campo relacionado a imágenes.

---

## 5. `ProductoController` — endpoints actuales

Ruta: `ESFE.InventarioProd.Web/Controllers/ProductoController.cs`

Atributo de clase: `[Authorize(Roles = "Admin,Supervisor,Inventariado,Vendedor,Cajero")]`

| Acción | Verbo/Atributos | Roles adicionales |
|---|---|---|
| `Index()` | `GET` (implícito) | (hereda de la clase) |
| `Listar(termino, idCategoria, incluirInactivos, ordenarPor, direccion, pagina, tamanioPagina)` | `[HttpGet]` | (hereda de la clase) |
| `DatosNuevo()` | `[HttpGet]` | `Admin,Supervisor,Inventariado` |
| `ObtenerParaEdicion(int id)` | `[HttpGet]` | `Admin,Supervisor,Inventariado` |
| `Crear([FromBody] ProductoFormDTO form)` | `[HttpPost] [ValidateAntiForgeryToken]` | `Admin,Supervisor,Inventariado` |
| `Editar([FromBody] ProductoFormDTO form)` | `[HttpPost] [ValidateAntiForgeryToken]` | `Admin,Supervisor,Inventariado` |
| `Eliminar([FromBody] EliminarProductoRequest req)` | `[HttpPost] [ValidateAntiForgeryToken]` | `Admin,Supervisor,Inventariado` |
| `ContarParaExportar(termino, idCategoria, incluirInactivos)` | `[HttpGet]` | `Admin,Supervisor,Inventariado` |
| `Exportar(termino, idCategoria, incluirInactivos, ordenarPor, direccion)` | `[HttpGet]` | `Admin,Supervisor,Inventariado` |

Todas las acciones son AJAX/JSON excepto `Exportar` (devuelve `File`, un `.xlsx`).

`Crear` completo:

```csharp
// POST /Producto/Crear (AJAX, JSON)
[HttpPost]
[ValidateAntiForgeryToken]
[Authorize(Roles = "Admin,Supervisor,Inventariado")]
public IActionResult Crear([FromBody] ProductoFormDTO form)
{
    var r = productoLN.Crear(form);
    return r.Resultado switch
    {
        ResultadoGuardarProducto.Ok => Ok(new { ok = true, id = r.IdGenerado, mensaje = "Producto creado correctamente" }),
        ResultadoGuardarProducto.CodigoDuplicado => Conflict(new { ok = false, mensaje = r.Mensaje }),
        ResultadoGuardarProducto.DatosInvalidos => BadRequest(new { ok = false, mensaje = r.Mensaje }),
        _ => StatusCode(500, new { ok = false, mensaje = r.Mensaje ?? "Error interno" })
    };
}
```

`Editar` completo:

```csharp
// POST /Producto/Editar (AJAX, JSON)
[HttpPost]
[ValidateAntiForgeryToken]
[Authorize(Roles = "Admin,Supervisor,Inventariado")]
public IActionResult Editar([FromBody] ProductoFormDTO form)
{
    var r = productoLN.Actualizar(form);
    return r.Resultado switch
    {
        ResultadoGuardarProducto.Ok => Ok(new { ok = true, mensaje = "Producto actualizado correctamente" }),
        ResultadoGuardarProducto.CodigoDuplicado => Conflict(new { ok = false, mensaje = r.Mensaje }),
        ResultadoGuardarProducto.DatosInvalidos => BadRequest(new { ok = false, mensaje = r.Mensaje }),
        ResultadoGuardarProducto.NoEncontrado => NotFound(new { ok = false, mensaje = r.Mensaje }),
        _ => StatusCode(500, new { ok = false, mensaje = r.Mensaje ?? "Error interno" })
    };
}
```

Ambos reciben `[FromBody] ProductoFormDTO form` — es decir, hoy el body es JSON puro (`Content-Type: application/json`), **no** `multipart/form-data`. Un upload de imagen (archivo binario) no puede viajar en este mismo payload sin cambiar el binding (ver sección 6 y 10).

---

## 6. Vista `Views/Producto/Index.cshtml`

Existe en: `ESFE.InventarioProd.Web/Views/Producto/Index.cshtml` (979 líneas, incluye script inline en `@section Scripts`).

### `<thead>` completo

```cshtml
<thead>
    <tr>
        <th class="pr-th-ordenable" data-columna="codigo">Código <span class="pr-th-icono">⇅</span></th>
        <th class="pr-th-ordenable" data-columna="nombre">Nombre <span class="pr-th-icono">⇅</span></th>
        <th class="pr-th-ordenable" data-columna="categoria">Categoría <span class="pr-th-icono">⇅</span></th>
        <th class="pr-th-ordenable" data-columna="existencias">Existencias <span class="pr-th-icono">⇅</span></th>
        <th class="pr-th-ordenable" data-columna="preciocompra">P. Compra <span class="pr-th-icono">⇅</span></th>
        <th class="pr-th-ordenable" data-columna="precioventa">P. Venta <span class="pr-th-icono">⇅</span></th>
        <th class="pr-th-ordenable" data-columna="estado">Estado <span class="pr-th-icono">⇅</span></th>
        <th></th>
    </tr>
</thead>
```

8 columnas (`COLSPAN = 8` en el JS). La última `<th></th>` sin `data-columna` es para el botón de acciones (editar). No hay columna de imagen/thumbnail. El `<tbody>` se renderiza 100% desde JS (`renderTabla()`), no hay `@foreach` server-side.

### Modal Crear/Editar (`id="modal-producto"`)

```cshtml
<!-- Modal crear/editar -->
<div id="modal-producto" class="modal-overlay" style="display:none;">
    <div class="modal-card">
        <div class="modal-header">
            <h2 id="modal-titulo">Nuevo producto</h2>
        </div>

        <div class="modal-body">
            <input type="hidden" id="prod-id" />

            <fieldset class="seccion">
                <legend>INFORMACIÓN BÁSICA</legend>
                <div class="grid-2col">
                    <div class="campo">
                        <label for="prod-codigo">Código</label>
                        <input type="text" id="prod-codigo" readonly />
                        <span class="error-inline" data-campo="codigo"></span>
                    </div>
                    <div class="campo">
                        <label for="prod-nombre">Nombre *</label>
                        <input type="text" id="prod-nombre" maxlength="150" />
                        <span class="error-inline" data-campo="nombre"></span>
                    </div>
                    <div class="campo">
                        <label for="prod-categoria">Categoría *</label>
                        <select id="prod-categoria"></select>
                        <span class="error-inline" data-campo="categoria"></span>
                    </div>
                    <div class="campo">
                        <label for="prod-proveedor">Proveedor</label>
                        <select id="prod-proveedor">
                            <option value="">(sin proveedor)</option>
                        </select>
                    </div>
                </div>
            </fieldset>

            <fieldset class="seccion">
                <legend>PRECIOS E IMPUESTOS</legend>
                <div class="grid-2col">
                    <div class="campo">
                        <label for="prod-precio-compra">Precio Compra *</label>
                        <input type="number" id="prod-precio-compra" min="0" step="0.01" />
                        <span class="error-inline" data-campo="precioCompra"></span>
                    </div>
                    <div class="campo">
                        <label for="prod-precio-venta">Precio Venta *</label>
                        <input type="number" id="prod-precio-venta" min="0" step="0.01" />
                        <span class="advertencia-inline" id="advertencia-precio">Precio venta menor o igual al de compra</span>
                        <span class="error-inline" data-campo="precioVenta"></span>
                    </div>
                    <div class="campo">
                        <label>
                            <input type="checkbox" id="prod-aplica-iva" checked />
                            Aplica IVA
                        </label>
                    </div>
                    <div class="campo" id="campo-porcentaje-iva">
                        <label for="prod-porcentaje-iva">Porcentaje IVA (%)</label>
                        <input type="number" id="prod-porcentaje-iva" min="0" max="100" step="0.01" value="13.00" />
                    </div>
                </div>
            </fieldset>

            <fieldset class="seccion">
                <legend>INVENTARIO</legend>
                <div class="grid-2col">
                    <div class="campo" id="campo-stock-actual" style="display:none;">
                        <label for="prod-stock-actual">Stock actual</label>
                        <input type="text" id="prod-stock-actual" readonly />
                        <small class="nota-campo">Se ajusta desde el módulo Movimientos</small>
                    </div>
                    <div class="campo">
                        <label for="prod-stock-minimo">Stock mínimo</label>
                        <input type="number" id="prod-stock-minimo" min="0" step="1" value="0" />
                    </div>
                </div>
            </fieldset>

            <fieldset class="seccion" id="seccion-estado" style="display:none;">
                <legend>ESTADO</legend>
                <div class="campo">
                    <label>
                        <input type="checkbox" id="prod-estado" checked />
                        Producto activo
                    </label>
                </div>
            </fieldset>

            <div class="mensaje-error-general" id="mensaje-error-general"></div>
        </div>

        <div class="modal-footer">
            <button type="button" class="btn-eliminar" id="btn-eliminar" style="display:none;">Eliminar</button>
            <div class="footer-derecha">
                <button type="button" class="btn-secundario" id="btn-cancelar-producto">Cancelar</button>
                <button type="button" class="btn-primario" id="btn-guardar">Guardar</button>
            </div>
        </div>
    </div>
</div>
```

El modal está organizado en `<fieldset class="seccion">` (INFORMACIÓN BÁSICA, PRECIOS E IMPUESTOS, INVENTARIO, ESTADO). Un widget de upload de imagen encajaría naturalmente como un nuevo `<fieldset class="seccion">` (ej. "IMAGEN" o "GALERÍA") entre INFORMACIÓN BÁSICA e INVENTARIO, o como parte de INFORMACIÓN BÁSICA. No hay `<form>` HTML envolviendo el modal — el guardado se hace 100% vía `fetch()` con JSON (ver `guardarProducto()` en el script), no hay submit nativo de formulario.

### Grep de `enctype|multipart|file` (case-insensitive)

Un solo hallazgo, y es un falso positivo (comentario, no relacionado a upload de archivos):

```
Línea 572:  // El resultado es un archivo (File result): el navegador maneja la
```

**No existe** ningún `enctype="multipart/form-data"`, `<input type="file">`, ni lógica de manejo de archivos en esta vista. Todo el intercambio con el servidor es JSON vía `fetch`.

---

## 7. Configuración de archivos estáticos

### `Program.cs` completo (`ESFE.InventarioProd.Web/Program.cs`)

```csharp
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios MVC
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
        options.Cookie.Name = "STOCKEO.Auth";
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Lax;
    });

var app = builder.Build();

// Configuración para producción
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Redireccionar HTTP a HTTPS
app.UseHttpsRedirection();

// Habilitar el sistema de rutas
app.UseRouting();

// Autenticación y autorización
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

// Ruta principal: HomeController.Index() redirige a Dashboard (autenticado) o Login (anónimo)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
```

### `app.UseStaticFiles()`

**No está presente explícitamente.** El proyecto usa `app.MapStaticAssets()` (el nuevo pipeline de ASP.NET Core 9/10 para assets estáticos, que reemplaza a `UseStaticFiles()` + `UseStaticAssets()` en el modelo "Static Web Assets" con mapeo por endpoint vía `.WithStaticAssets()`). `MapStaticAssets()` sirve `wwwroot/` igual que `UseStaticFiles()`, pero está pensado para assets conocidos en tiempo de compilación (con fingerprinting/manifest). **Para servir archivos subidos dinámicamente en runtime a `wwwroot/uploads/`** (que no existen en tiempo de build), lo más seguro es agregar explícitamente `app.UseStaticFiles()` — a confirmar en el sprint de implementación si `MapStaticAssets()` por sí solo cubre archivos añadidos después del build (típicamente no los cubre, porque se basa en el manifest generado en compilación).

### Carpeta `wwwroot/uploads/`

**No existe.** El árbol actual de `wwwroot/` (confirmado por listado) contiene: `favicon.ico`, `js/`, `lib/bootstrap/`, `lib/jquery/`, `lib/jquery-validation/`, `lib/jquery-validation-unobtrusive/`, `css/` (`login.css`, `categoria.css`, `movimiento.css`, `movimiento-historial.css`, `producto.css`, `usuario.css`, `perfil.css`, `proveedores.css`, `site.css`, `proveedor.css`). Ninguna carpeta `uploads/`.

### `.gitignore`

- **Raíz del repo:** existe `.gitignore` (430 líneas, plantilla estándar de Visual Studio/GitHub — `bin/`, `obj/`, `.vs/`, etc.). Contiene la línea comentada `#wwwroot/` (línea 58-59) — es decir, `wwwroot/` **no** está ignorado por defecto; si se crea `wwwroot/uploads/` con archivos, git los trackeará a menos que se agregue una regla específica.
- **`ESFE.InventarioProd.Web/.gitignore` (a nivel de proyecto):** **No existe.**

---

## 8. FKs entrantes a Producto y columnas del PK

### FKs que apuntan a `Producto`

```sql
SELECT
    fk.name AS FKName,
    tp.name AS TablaPadre, cp.name AS ColumnaPadre,
    tr.name AS TablaHija, cr.name AS ColumnaHija
FROM sys.foreign_keys fk
JOIN sys.tables tp ON fk.referenced_object_id = tp.object_id
JOIN sys.tables tr ON fk.parent_object_id = tr.object_id
JOIN sys.foreign_key_columns fkc ON fk.object_id = fkc.constraint_object_id
JOIN sys.columns cp ON cp.object_id = tp.object_id AND cp.column_id = fkc.referenced_column_id
JOIN sys.columns cr ON cr.object_id = tr.object_id AND cr.column_id = fkc.parent_column_id
WHERE tp.name = 'Producto';
```

| FKName | TablaPadre | ColumnaPadre | TablaHija | ColumnaHija |
|---|---|---|---|---|
| FK_DetalleFactura_Producto | Producto | IdProductoPK | DetalleFactura | IdProductoFK |
| FK_StockProducto_Producto | Producto | IdProductoPK | Stock_Producto | IdProductoFK |
| FK_DetalleMovimiento_Producto | Producto | IdProductoPK | DetalleMovimiento | IdProductoFK |

(3 filas — `ProductoImagen` aún no existe, por lo que no puede aparecer aquí.)

### Columnas de `Producto`

```sql
SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, IS_NULLABLE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Producto'
ORDER BY ORDINAL_POSITION;
```

| COLUMN_NAME | DATA_TYPE | CHARACTER_MAXIMUM_LENGTH | IS_NULLABLE |
|---|---|---|---|
| IdProductoPK | int | NULL | NO |
| Nombre | varchar | 100 | YES |
| Descripcion | varchar | 255 | YES |
| PrecioCompra | decimal | NULL | YES |
| PrecioVenta | decimal | NULL | YES |
| PorcentajeIVA | decimal | NULL | YES |
| AplicaIVA | bit | NULL | YES |
| IdProveedorFK | int | NULL | YES |
| IdCategoriaFK | smallint | NULL | YES |
| Estado | bit | NULL | YES |
| Codigo | varchar | 20 | YES |

**PK confirmado:** `IdProductoPK`, tipo `int`, `NOT NULL`. Una futura `ProductoImagen.IdProductoFK` debe ser `int` para calzar exactamente (no `smallint` — a diferencia de `Stock_Producto.IdProductoFK`, que sí es `smallint` y obliga a los casts `TODO` vistos en varios SPs de la sección 1).

### ¿Existe ya `ProductoImagen`?

```sql
SELECT COUNT(*) AS Existe
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_NAME = 'ProductoImagen';
```

Resultado: **`Existe = 0`. No existe la tabla `ProductoImagen`.**

---

## 9. Paquetes NuGet actuales del Web

`ItemGroup` de `PackageReference` en `ESFE.InventarioProd.Web/ESFE.InventarioProd.Web.csproj`:

```xml
<ItemGroup>
  <PackageReference Include="ClosedXML" Version="0.104.*" />
</ItemGroup>
```

- **`SixLabors.ImageSharp`: No existe** ninguna referencia en el `.csproj` del Web ni en ningún otro `.csproj` del solution (no se encontró en la búsqueda de archivos `Producto*.cs` ni en los csproj leídos).
- **ClosedXML:** `0.104.*` (wildcard de versión menor).

Referencias de proyecto del mismo `.csproj`:
```xml
<ItemGroup>
  <ProjectReference Include="..\ESFE.GestionProductos.LN\ESFE.GestionProductos.LN.csproj" />
</ItemGroup>
```

Nota adicional: `ESFE.GestionProductos.LN.csproj` referencia `Microsoft.Data.SqlClient` (`7.0.2`) y `Microsoft.Extensions.Identity.Core` (`10.0.*`); ninguno relevante para procesamiento de imágenes.

---

## 10. Zonas grises detectadas

1. **Método `ProductoDAL.Actualizar(Producto)` roto en la práctica.** Invoca `sp_ActualizarProducto`, que por collation case-insensitive de SQL Server es el **mismo objeto** que `SP_ActualizarProducto` (definido en `Producto_StoredProcedures.sql`, Sprint B). Ese SP real tiene la firma `(@IdProductoPK, @Nombre, @IdCategoriaFK, @IdProveedorFK, @PrecioCompra, @PrecioVenta, @AplicaIVA, @PorcentajeIVA, @Estado)` — pero el método C# le sigue pasando `@Codigo` y `@Descripcion`, parámetros que ya no existen en el SP. Si se invocara, SQL Server lanzaría un error de parámetro no reconocido. Confirmé por grep que **no se invoca** desde `ESFE.InventarioProd.Web` (ningún controlador llama a `productoLN.Guardar()`, `productoLN.Actualizar(Producto)`, `productoLN.Insertar(Producto)`, `productoLN.Listar()`, `productoLN.Buscar()` ni `productoLN.EliminarLogico()`); estos métodos solo son usados por el proyecto WinForms (`ESFE.GestionProductos.UI` → `ucProductoFrm.cs`, `UcCatalogoProductos.cs`), que consume el CRUD legado directo sobre `Producto` (con `Descripcion`, sin categorías/stock por SP separado). Es decir: **`ProductoDAL.Actualizar` es código muerto para el flujo Web**, pero sigue siendo llamado por WinForms — no se puede borrar sin verificar el impacto en esa app.

2. **Colisión de nombres de SP entre dos generaciones de CRUD.** Existen simultáneamente `SP_InsertarProducto`/`sp_ActualizarProducto`/`sp_EliminarLogicoProducto` (CRUD legado usado por WinForms, con columna `Descripcion` que **no existe** como campo expuesto en `ProductoFormDTO`/`ProductoListadoDTO` del Web) y `SP_CrearProducto`/`SP_ActualizarProducto`/`SP_EliminarProducto` (CRUD nuevo del Web, Sprint B). La tabla `Producto` sí tiene columna `Descripcion` (`varchar(255)`, confirmado en sección 8), pero el Web actual **nunca la lee ni la escribe** — ningún DTO de Producto la incluye. Si el sprint de imágenes quisiera mostrar una descripción junto a la galería, ese campo ya existe en BD pero está huérfano en el flujo Web.

3. **Deuda técnica de tipos declarada explícitamente por el equipo (`TODO` repetidos).** Varios SPs (`SP_ListarProductos`, `SP_ObtenerProductoParaEdicion`, `SP_EliminarProducto`, `SP_ListarProductosParaExportar`) contienen el comentario `-- TODO: eliminar cast cuando se resuelva deuda técnica de FK short vs int` porque `Stock_Producto.IdProductoFK` es `SMALLINT` mientras que `Producto.IdProductoPK` es `INT`. Esto obliga a `CAST(p.IdProductoPK AS SMALLINT)` en los JOINs. Para `ProductoImagen`, la FK a `Producto` **debe definirse como `INT`** (no replicar el patrón `SMALLINT` de `Stock_Producto`) para no heredar el mismo problema. El código C# también refleja esto: `ProductoLN.Crear`/`Actualizar` hacen `(short)nuevoId` / `(short)form.IdProductoPK.Value` con el mismo comentario `TODO`.

4. **No hay convención de "solo lectura" para las tablas de catálogo consultadas por `SP_ListarProductos`.** `Categoria` y `Proveedor` se filtran con `Estado = 1` en los LEFT JOIN pero el producto en sí puede mostrarse aunque su categoría/proveedor esté inactivo (usa `ISNULL(c.Nombre, N'(sin categoría)')`). Si la galería de imágenes también necesita soft-delete por consistencia, este es el patrón de "estado" ya establecido en el módulo (columna `Estado BIT`, filtrado condicional por `@IncluirInactivos`).

5. **El body de `Crear`/`Editar` es JSON puro (`[FromBody] ProductoFormDTO`), no `multipart/form-data`.** Confirmado en sección 5 y 6 — no hay `<form enctype>` ni `<input type="file">` en ningún punto del flujo actual. El upload de imagen para el sprint nuevo necesitará **un endpoint HTTP separado** (ej. `POST /Producto/{id}/Imagenes`) con `[FromForm]`/`IFormFile`, ya que no se puede mezclar `IFormFile` dentro del mismo payload JSON que usa `Crear`/`Editar` sin reescribir el binding actual del modal.

6. **`MapStaticAssets()` en vez de `UseStaticFiles()` — riesgo para archivos subidos en runtime.** Confirmado en sección 7: el pipeline actual usa el modelo "Static Web Assets" de .NET 9/10 (`MapStaticAssets()` + `.WithStaticAssets()`), que resuelve archivos vía un manifest generado en tiempo de compilación. Archivos añadidos dinámicamente después del build (como las imágenes subidas por el usuario a `wwwroot/uploads/`) **probablemente no sean servidos correctamente** por este mecanismo — es una zona gris a validar en el sprint de implementación; lo más común en este escenario es añadir `app.UseStaticFiles()` explícito (antes o junto a `MapStaticAssets()`) apuntando a la carpeta de uploads, o usar un físico `PhysicalFileProvider` dedicado.

7. **`wwwroot/` no está en `.gitignore`.** La línea `#wwwroot/` en el `.gitignore` raíz está comentada (inactiva). Esto significa que si se crean `wwwroot/uploads/productos/full/` y `.../thumb/` con archivos de prueba durante desarrollo, **se trackearán en git por defecto** a menos que el sprint de implementación agregue una regla específica (ej. `wwwroot/uploads/**` o mantener solo un `.gitkeep`).

8. **Archivos no rastreados detectados en el estado de git al inicio de esta sesión** (fuera del alcance de esta investigación, pero relevantes para el contexto del sprint): `ESFE.GestionProductos.DAL/Scripts/Usuario_Login_v2.sql`, `ESFE.GestionProductos.LN/Security/` (carpeta nueva), y dos `.md` de investigación de hashing de contraseñas (`IMPLEMENTACION_PASSWORD_HASHING.md`, `INVESTIGACION_PASSWORD_HASHING.md`). No se tocaron ni se investigó su contenido — se documenta solo su existencia por transparencia, ya que corresponden a trabajo en curso ajeno a este sprint.

9. **Ambigüedad de nombre en Sprint C.** `SP_ListarProductosParaExportar` no tiene paginación (`OFFSET`/`FETCH`) — trae todos los productos filtrados en un solo `SELECT`. Si el catálogo crece mucho junto con la galería de imágenes, este SP seguiría funcionando igual (no toca imágenes), pero es una nota de escalabilidad ya presente antes del sprint de imágenes.

10. **`Producto.cs` (entidad EN) no está alineado 1:1 con la tabla real.** La clase `Producto` en `ESFE.GestionProductos.EN/Producto.cs` tiene navegación `virtual Proveedor? Proveedor` y `virtual Categoria? Categoria` con atributos `[ForeignKey]`, sugiriendo un origen Entity-Framework-like, pero el proyecto usa ADO.NET puro (`SqlCommand`/`SqlDataReader`) en todo el DAL — esas propiedades de navegación nunca se hidratan en `ProductoDAL`. Si el sprint de imágenes sigue el mismo patrón ADO.NET (recomendado, por consistencia), no debe intentar usar estas propiedades de navegación como si fueran EF Core real.
