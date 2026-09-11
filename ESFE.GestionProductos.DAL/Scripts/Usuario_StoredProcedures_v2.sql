-- =============================================
-- Stored Procedures _v2 para el módulo Usuario (fix HTTP 200 en /Account/CambiarPassword)
-- Ejecutar manualmente contra la base de datos (GestionProductoBD).
--
-- Causa raíz: los SPs originales (sp_ActualizarUsuario, sp_EliminarLogicoUsuario)
-- tienen SET NOCOUNT ON sin un SELECT @@ROWCOUNT final. Esto hace que
-- SqlCommand.ExecuteNonQuery() en el DAL devuelva -1 en vez del conteo real de
-- filas afectadas, por lo que comprobaciones como "Actualizar(usuario) > 0"
-- evalúan false aunque el UPDATE haya sido exitoso.
--
-- Estas versiones _v2 son NUEVAS: los SPs originales NO se modifican ni se
-- eliminan, para preservar compatibilidad con la app WinForms ESFE.GestionProductos,
-- que sigue usando los originales. Solo el DAL del sistema web (UserDAL) se
-- actualiza para invocar estas versiones _v2.
-- =============================================

-- sp_ActualizarUsuario_v2
-- Misma lógica exacta que sp_ActualizarUsuario (mismo BEGIN TRY, mismo IF EXISTS,
-- mismos parámetros, mismo orden). Único agregado: captura @@ROWCOUNT y lo expone
-- mediante SELECT para que ExecuteScalar() en el DAL reciba el conteo real.
CREATE OR ALTER PROCEDURE sp_ActualizarUsuario_v2
    @IdUsuarioPK INT,
    @Nombre VARCHAR(100),
    @Password VARCHAR(256),
    @Id_RolFK SMALLINT,
    @Estado BIT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Filas INT = 0;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF EXISTS (SELECT 1 FROM Usuario WHERE IdUsuarioPK = @IdUsuarioPK)
        BEGIN
            UPDATE Usuario
            SET Nombre = @Nombre,
                Password = @Password,
                Id_RolFK = @Id_RolFK,
                Estado = @Estado
            WHERE IdUsuarioPK = @IdUsuarioPK;

            SET @Filas = @@ROWCOUNT;
            COMMIT TRANSACTION;
        END
        ELSE
        BEGIN
            ROLLBACK TRANSACTION;
        END
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH

    SELECT @Filas AS FilasAfectadas;
END
GO

-- sp_EliminarLogicoUsuario_v2
-- Misma lógica exacta que sp_EliminarLogicoUsuario (UPDATE Estado = 0 por Id).
-- Único agregado: SELECT @@ROWCOUNT final para exponer el conteo real.
CREATE OR ALTER PROCEDURE sp_EliminarLogicoUsuario_v2
    @IdUsuario INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Usuario SET Estado = 0 WHERE IdUsuarioPK = @IdUsuario;

    SELECT @@ROWCOUNT AS FilasAfectadas;
END
GO
