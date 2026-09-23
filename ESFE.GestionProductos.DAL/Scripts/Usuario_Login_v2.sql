-- SP nuevo para el flujo de login hasheado del sistema web.
-- El SP legado SP_LoginUsuario queda INTACTO (lo usa la app WinForms ESFE).
-- Este SP devuelve el usuario por Nombre + Estado=1, incluyendo Password (hash o plano).
-- La verificación del password ocurre en C# (PasswordHasher), no en SQL.
--
-- NOTA: se usa el patrón "stub + ALTER" en vez de "CREATE OR ALTER" directo porque
-- esta instancia (SQL Server 2025 17.0.1135.8) lanza el error
-- "Msg 208, Invalid object name 'SP_ObtenerUsuarioPorNombre'" cuando CREATE OR ALTER
-- se ejecuta sobre un procedimiento que todavía no existe (reproducido de forma
-- consistente en dev). El patrón siguiente es idempotente y evita el bug.

IF OBJECT_ID('dbo.SP_ObtenerUsuarioPorNombre', 'P') IS NULL
    EXEC('CREATE PROCEDURE dbo.SP_ObtenerUsuarioPorNombre AS BEGIN SET NOCOUNT ON; END');
GO

ALTER PROCEDURE dbo.SP_ObtenerUsuarioPorNombre
    @Nombre VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        U.IdUsuarioPK,
        U.Nombre,
        U.Password,
        U.Id_RolFK,
        R.NombreRol AS Rol,
        U.Estado
    FROM Usuario AS U
    INNER JOIN Rol AS R
        ON U.Id_RolFK = R.IdRolPK
    WHERE U.Nombre = @Nombre
      AND U.Estado = 1;
END;
GO
