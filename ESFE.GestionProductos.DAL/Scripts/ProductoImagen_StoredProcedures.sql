-- =============================================
-- Stored Procedures para la galería de imágenes de Producto (STOCKEO) — Sprint A
-- Tabla nueva ProductoImagen (1:N con Producto) + CRUD de imágenes.
-- Ejecutar manualmente contra la base de datos (GestionProductoBD).
--
-- NOTA: los SPs nuevos usan el patrón "stub + ALTER" en vez de "CREATE OR ALTER"
-- directo porque esta instancia (SQL Server 2025 17.0.1135.8) lanza
-- "Msg 208, Invalid object name '<SP>'" cuando CREATE OR ALTER se ejecuta sobre
-- un procedimiento que todavía no existe (mismo issue documentado en
-- Usuario_Login_v2.sql / IMPLEMENTACION_PASSWORD_HASHING.md). El patrón
-- stub + ALTER es idempotente y evita el bug.
-- =============================================

-- El índice filtrado único de más abajo (IX_ProductoImagen_Principal) exige estas
-- dos opciones de sesión encendidas; sqlcmd no las trae por defecto.
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

-- =============================================
-- Tabla ProductoImagen
-- =============================================
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'ProductoImagen')
BEGIN
    CREATE TABLE ProductoImagen (
        IdProductoImagenPK INT IDENTITY(1,1) PRIMARY KEY,
        IdProductoFK INT NOT NULL,
        NombreArchivo VARCHAR(50) NOT NULL,
        Orden SMALLINT NOT NULL DEFAULT 0,
        EsPrincipal BIT NOT NULL DEFAULT 0,
        FechaSubida DATETIME NOT NULL DEFAULT GETDATE(),
        Estado BIT NOT NULL DEFAULT 1,
        CONSTRAINT FK_ProductoImagen_Producto FOREIGN KEY (IdProductoFK) REFERENCES Producto(IdProductoPK)
    );

    CREATE INDEX IX_ProductoImagen_Producto ON ProductoImagen(IdProductoFK, Estado);

    -- Solo una principal activa por producto (constraint a nivel BD, además de la
    -- lógica de los SPs de abajo que ya desmarca las demás antes de marcar una nueva).
    CREATE UNIQUE INDEX IX_ProductoImagen_Principal
        ON ProductoImagen(IdProductoFK)
        WHERE EsPrincipal = 1 AND Estado = 1;
END
GO

-- =============================================
-- SP_ListarImagenesProducto
-- Imágenes activas de un producto, ordenadas para la galería.
-- =============================================
IF OBJECT_ID('dbo.SP_ListarImagenesProducto', 'P') IS NULL
    EXEC('CREATE PROCEDURE dbo.SP_ListarImagenesProducto AS BEGIN SET NOCOUNT ON; END');
GO

ALTER PROCEDURE dbo.SP_ListarImagenesProducto
    @IdProductoFK INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT IdProductoImagenPK, NombreArchivo, EsPrincipal, Orden
    FROM ProductoImagen
    WHERE IdProductoFK = @IdProductoFK AND Estado = 1
    ORDER BY Orden, IdProductoImagenPK;
END
GO

-- =============================================
-- SP_ContarImagenesActivas
-- Usado para validar el límite de 5 imágenes por producto antes de subir.
-- =============================================
IF OBJECT_ID('dbo.SP_ContarImagenesActivas', 'P') IS NULL
    EXEC('CREATE PROCEDURE dbo.SP_ContarImagenesActivas AS BEGIN SET NOCOUNT ON; END');
GO

ALTER PROCEDURE dbo.SP_ContarImagenesActivas
    @IdProductoFK INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT COUNT(*) AS Total
    FROM ProductoImagen
    WHERE IdProductoFK = @IdProductoFK AND Estado = 1;
END
GO

-- =============================================
-- SP_CrearImagenProducto
-- Calcula el siguiente Orden, desmarca la principal anterior si corresponde,
-- inserta y retorna el nuevo Id.
-- =============================================
IF OBJECT_ID('dbo.SP_CrearImagenProducto', 'P') IS NULL
    EXEC('CREATE PROCEDURE dbo.SP_CrearImagenProducto AS BEGIN SET NOCOUNT ON; END');
GO

ALTER PROCEDURE dbo.SP_CrearImagenProducto
    @IdProductoFK INT,
    @NombreArchivo VARCHAR(50),
    @EsPrincipal BIT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @Orden SMALLINT;
        SELECT @Orden = ISNULL(MAX(Orden), -1) + 1
        FROM ProductoImagen
        WHERE IdProductoFK = @IdProductoFK AND Estado = 1;

        IF @EsPrincipal = 1
            UPDATE ProductoImagen SET EsPrincipal = 0
            WHERE IdProductoFK = @IdProductoFK AND Estado = 1;

        INSERT INTO ProductoImagen (IdProductoFK, NombreArchivo, Orden, EsPrincipal, Estado)
        VALUES (@IdProductoFK, @NombreArchivo, @Orden, @EsPrincipal, 1);

        DECLARE @NuevoId INT = CAST(SCOPE_IDENTITY() AS INT);

        COMMIT TRANSACTION;

        SELECT @NuevoId AS NuevoId;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

-- =============================================
-- SP_EliminarImagenProducto
-- Soft-delete. Si la imagen borrada era la principal, promueve automáticamente
-- la de menor Orden que quede activa. Retorna los datos necesarios para que el
-- controller borre los archivos físicos y refresque el estado "principal" en UI.
-- =============================================
IF OBJECT_ID('dbo.SP_EliminarImagenProducto', 'P') IS NULL
    EXEC('CREATE PROCEDURE dbo.SP_EliminarImagenProducto AS BEGIN SET NOCOUNT ON; END');
GO

ALTER PROCEDURE dbo.SP_EliminarImagenProducto
    @IdProductoImagenPK INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NombreArchivo VARCHAR(50) = '';
    DECLARE @IdProductoFK INT = 0;
    DECLARE @EraPrincipal BIT = 0;
    DECLARE @IdImagenPromovida INT = NULL;
    DECLARE @NombreArchivoPromovido VARCHAR(50) = NULL;

    IF NOT EXISTS (SELECT 1 FROM ProductoImagen WHERE IdProductoImagenPK = @IdProductoImagenPK AND Estado = 1)
    BEGIN
        SELECT @NombreArchivo AS NombreArchivo, @IdProductoFK AS IdProductoFK,
               @EraPrincipal AS EraPrincipal,
               @IdImagenPromovida AS IdImagenPromovida,
               @NombreArchivoPromovido AS NombreArchivoPromovido;
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;

        SELECT @NombreArchivo = NombreArchivo, @IdProductoFK = IdProductoFK, @EraPrincipal = EsPrincipal
        FROM ProductoImagen
        WHERE IdProductoImagenPK = @IdProductoImagenPK;

        UPDATE ProductoImagen SET Estado = 0 WHERE IdProductoImagenPK = @IdProductoImagenPK;

        IF @EraPrincipal = 1
        BEGIN
            -- UPDATE TOP (1) no soporta ORDER BY directo: se resuelve el candidato
            -- con SELECT TOP 1 ... ORDER BY hacia variables y se actualiza por PK.
            SELECT TOP (1) @IdImagenPromovida = IdProductoImagenPK, @NombreArchivoPromovido = NombreArchivo
            FROM ProductoImagen
            WHERE IdProductoFK = @IdProductoFK AND Estado = 1
            ORDER BY Orden, IdProductoImagenPK;

            IF @IdImagenPromovida IS NOT NULL
                UPDATE ProductoImagen SET EsPrincipal = 1 WHERE IdProductoImagenPK = @IdImagenPromovida;
        END

        COMMIT TRANSACTION;

        SELECT @NombreArchivo AS NombreArchivo, @IdProductoFK AS IdProductoFK,
               @EraPrincipal AS EraPrincipal,
               @IdImagenPromovida AS IdImagenPromovida,
               @NombreArchivoPromovido AS NombreArchivoPromovido;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

-- =============================================
-- SP_MarcarImagenPrincipal
-- Desmarca la principal actual del producto y marca la indicada.
-- =============================================
IF OBJECT_ID('dbo.SP_MarcarImagenPrincipal', 'P') IS NULL
    EXEC('CREATE PROCEDURE dbo.SP_MarcarImagenPrincipal AS BEGIN SET NOCOUNT ON; END');
GO

ALTER PROCEDURE dbo.SP_MarcarImagenPrincipal
    @IdProductoImagenPK INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @IdProductoFK INT;

    SELECT @IdProductoFK = IdProductoFK
    FROM ProductoImagen
    WHERE IdProductoImagenPK = @IdProductoImagenPK AND Estado = 1;

    IF @IdProductoFK IS NULL
    BEGIN
        SELECT 0 AS Ok;
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;

        UPDATE ProductoImagen SET EsPrincipal = 0
        WHERE IdProductoFK = @IdProductoFK AND Estado = 1;

        UPDATE ProductoImagen SET EsPrincipal = 1
        WHERE IdProductoImagenPK = @IdProductoImagenPK;

        COMMIT TRANSACTION;

        SELECT 1 AS Ok;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO
