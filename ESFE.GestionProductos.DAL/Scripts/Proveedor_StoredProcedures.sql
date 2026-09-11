-- =============================================
-- Stored Procedures para el módulo Proveedores (STOCKEO)
-- Conexión al backend real del listado web (mismo patrón que Producto/Categoría).
-- Ejecutar manualmente contra la base de datos (GestionProductoBD).
--
-- Los SPs legados (SP_InsertarProveedor, usp_ActualizarProveedor,
-- sp_EliminarLogicoProveedor, SP_ListarProveedor, sp_BuscarProveedor) se preservan
-- intactos para la app WinForms ESFE.GestionProductos. La lista para la web reutiliza
-- sp_BuscarProveedor (sin NOCOUNT, no sufre el bug de ExecuteNonQuery); las mutaciones
-- usan SPs nuevas (no _v2, porque no colisionan de nombre con las legadas) ya que la
-- semántica de actualización parcial y el delete sin protección de las legadas no
-- calzan con un modal que envía el formulario completo.
-- =============================================

-- SP_CrearProveedor
-- Retorna el nuevo IdProveedorPK
CREATE OR ALTER PROCEDURE SP_CrearProveedor
    @Nombre NVARCHAR(100),
    @Empresa NVARCHAR(100) = NULL,
    @Telefono NVARCHAR(20) = NULL,
    @Correo NVARCHAR(150) = NULL,
    @Direccion NVARCHAR(255) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Proveedor (Nombre, Empresa, Telefono, Correo, Direccion, Estado)
    VALUES (@Nombre, @Empresa, @Telefono, @Correo, @Direccion, 1);
    SELECT CAST(SCOPE_IDENTITY() AS INT) AS NuevoId;
END
GO

-- SP_ActualizarProveedor
-- Sobrescribe todos los campos (a diferencia de usp_ActualizarProveedor, que hace
-- actualización parcial vía ISNULL). Retorna filas afectadas.
CREATE OR ALTER PROCEDURE SP_ActualizarProveedor
    @IdProveedorPK INT,
    @Nombre NVARCHAR(100),
    @Empresa NVARCHAR(100) = NULL,
    @Telefono NVARCHAR(20) = NULL,
    @Correo NVARCHAR(150) = NULL,
    @Direccion NVARCHAR(255) = NULL,
    @Estado BIT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Proveedor
    SET Nombre = @Nombre,
        Empresa = @Empresa,
        Telefono = @Telefono,
        Correo = @Correo,
        Direccion = @Direccion,
        Estado = @Estado
    WHERE IdProveedorPK = @IdProveedorPK;
    SELECT @@ROWCOUNT AS FilasAfectadas;
END
GO

-- SP_EliminarProveedor
-- Soft-delete. Retorna: -1 tiene productos activos asociados | 0 no encontrado
-- (o ya inactivo) | 1 eliminado. Mismo patrón que SP_EliminarCategoria.
CREATE OR ALTER PROCEDURE SP_EliminarProveedor
    @IdProveedorPK INT
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM Producto WHERE IdProveedorFK = @IdProveedorPK AND Estado = 1)
    BEGIN
        SELECT -1 AS Resultado;
        RETURN;
    END

    UPDATE Proveedor SET Estado = 0 WHERE IdProveedorPK = @IdProveedorPK AND Estado = 1;
    SELECT CASE WHEN @@ROWCOUNT = 0 THEN 0 ELSE 1 END AS Resultado;
END
GO
