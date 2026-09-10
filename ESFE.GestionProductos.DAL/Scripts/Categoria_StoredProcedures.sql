    -- =============================================
    -- Stored Procedures para el módulo Categorías (STOCKEO)
    -- Ejecutar manualmente contra la base de datos (GestionProductoBD).
    -- =============================================

    -- SP_ListarCategoriasConConteo
    -- Devuelve categorías activas (Estado=1) ordenadas alfabéticamente por Nombre
    -- con conteo de productos activos (Producto.Estado=1) asociados
    CREATE OR ALTER PROCEDURE SP_ListarCategoriasConConteo
    AS
    BEGIN
        SET NOCOUNT ON;
        SELECT
            c.IdCategoriaPK,
            c.Nombre,
            ISNULL(c.Descripcion, '') AS Descripcion,
            ISNULL(COUNT(p.IdProductoPK), 0) AS TotalProductos
        FROM Categoria c
        LEFT JOIN Producto p
            ON p.IdCategoriaFK = c.IdCategoriaPK
            AND p.Estado = 1
        WHERE c.Estado = 1
        GROUP BY c.IdCategoriaPK, c.Nombre, c.Descripcion
        ORDER BY c.Nombre ASC;
    END
    GO

    -- SP_CrearCategoria
    CREATE OR ALTER PROCEDURE SP_CrearCategoria
        @Nombre NVARCHAR(100),
        @Descripcion NVARCHAR(255) = NULL
    AS
    BEGIN
        SET NOCOUNT ON;
        INSERT INTO Categoria (Nombre, Descripcion, Estado)
        VALUES (@Nombre, @Descripcion, 1);
        SELECT CAST(SCOPE_IDENTITY() AS SMALLINT) AS NuevoId;
    END
    GO

    -- SP_ActualizarCategoria
    CREATE OR ALTER PROCEDURE SP_ActualizarCategoria
        @IdCategoriaPK SMALLINT,
        @Nombre NVARCHAR(100),
        @Descripcion NVARCHAR(255) = NULL
    AS
    BEGIN
        SET NOCOUNT ON;
        UPDATE Categoria
        SET Nombre = @Nombre,
            Descripcion = @Descripcion
        WHERE IdCategoriaPK = @IdCategoriaPK AND Estado = 1;
        SELECT @@ROWCOUNT AS FilasAfectadas;
    END
    GO

    -- SP_EliminarCategoria (soft-delete)
    -- Retorna:
    --   -1 si tiene productos activos asociados (no elimina)
    --    0 si no encontró la categoría
    --    1 si eliminó correctamente
    CREATE OR ALTER PROCEDURE SP_EliminarCategoria
        @IdCategoriaPK SMALLINT
    AS
    BEGIN
        SET NOCOUNT ON;
        IF EXISTS (
            SELECT 1 FROM Producto
            WHERE IdCategoriaFK = @IdCategoriaPK AND Estado = 1
        )
        BEGIN
            SELECT -1 AS Resultado;
            RETURN;
        END

        UPDATE Categoria
        SET Estado = 0
        WHERE IdCategoriaPK = @IdCategoriaPK AND Estado = 1;

        SELECT CASE WHEN @@ROWCOUNT = 0 THEN 0 ELSE 1 END AS Resultado;
    END
    GO
