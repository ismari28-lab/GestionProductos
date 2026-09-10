-- =============================================
-- Stored Procedures para el Dashboard (STOCKEO)
-- Ejecutar manualmente contra la base de datos (GestionProductoBD).
-- =============================================

-- 1. Valor total del inventario = SUM(Stock * PrecioVenta) de productos activos
CREATE OR ALTER PROCEDURE SP_ValorTotalInventario
AS
BEGIN
    SET NOCOUNT ON;

    SELECT ISNULL(SUM(ISNULL(sp.Stock, 0) * ISNULL(p.PrecioVenta, 0)), 0) AS ValorTotal
    FROM Stock_Producto sp
    INNER JOIN Producto p ON sp.IdProductoFK = p.IdProductoPK
    WHERE sp.Estado = 1 AND p.Estado = 1;
END
GO

-- 2. Cantidad de productos con Stock < Stock_Minimo (productos y stock activos)
CREATE OR ALTER PROCEDURE SP_ContarProductosBajoStock
AS
BEGIN
    SET NOCOUNT ON;

    SELECT COUNT(*) AS Total
    FROM Stock_Producto sp
    INNER JOIN Producto p ON sp.IdProductoFK = p.IdProductoPK
    WHERE sp.Estado = 1 AND p.Estado = 1
      AND ISNULL(sp.Stock, 0) < ISNULL(sp.Stock_Minimo, 0);
END
GO

-- 3. Producto más vendido del mes calendario actual (movimientos de tipo 'Salida')
CREATE OR ALTER PROCEDURE SP_ProductoMasVendidoMes
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
GO

-- 4. Alertas de bajo stock, ordenadas por criticidad ascendente (Stock/Stock_Minimo).
--    Los productos con Stock_Minimo = 0 no pueden calcular ratio (division por cero);
--    en la practica no ocurren porque el filtro exige Stock < Stock_Minimo, pero por
--    seguridad se excluyen del calculo de ratio y se listan al final (menos criticos).
CREATE OR ALTER PROCEDURE SP_AlertasBajoStock
    @Top INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP (@Top)
        p.Nombre AS Nombre,
        ISNULL(sp.Stock, 0) AS Existencias,
        ISNULL(sp.Stock_Minimo, 0) AS Minimo
    FROM Stock_Producto sp
    INNER JOIN Producto p ON sp.IdProductoFK = p.IdProductoPK
    WHERE sp.Estado = 1 AND p.Estado = 1
      AND ISNULL(sp.Stock, 0) < ISNULL(sp.Stock_Minimo, 0)
    ORDER BY
        CASE WHEN ISNULL(sp.Stock_Minimo, 0) = 0 THEN 1 ELSE 0 END ASC,
        CASE WHEN ISNULL(sp.Stock_Minimo, 0) = 0 THEN 0
             ELSE CAST(ISNULL(sp.Stock, 0) AS DECIMAL(10, 4)) / sp.Stock_Minimo
        END ASC;
END
GO
