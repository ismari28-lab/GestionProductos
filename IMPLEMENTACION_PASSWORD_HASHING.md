# Implementación: Password Hashing con `PasswordHasher<TUser>`

Sprint ejecutado según `INVESTIGACION_PASSWORD_HASHING.md`. Estrategia: re-hash on login, sin script masivo de migración.

## 1. Archivos creados

- `ESFE.GestionProductos.DAL/Scripts/Usuario_Login_v2.sql` — nuevo SP `SP_ObtenerUsuarioPorNombre` (sin filtro de password; verificación se mueve a C#).
- `ESFE.GestionProductos.LN/Security/PasswordHasherHelper.cs` — wrapper de `PasswordHasher<Usuario>` con detección de texto plano legado y `Verificar()`/`Hash()`.
- `IMPLEMENTACION_PASSWORD_HASHING.md` — este reporte.

## 2. Archivos modificados

- `ESFE.GestionProductos.LN/ESFE.GestionProductos.LN.csproj` — agregado `Microsoft.Extensions.Identity.Core` `10.0.*`.
- `ESFE.GestionProductos.DAL/LoginDAL.cs` (clase `UsuarioDAL`) — `ValidarLogin` marcado `[Obsolete]`; agregado `ObtenerPorNombre`.
- `ESFE.GestionProductos.DAL/UserDAL.cs` — `Listar()` marcado `[Obsolete]` (SP inexistente, sin llamadores).
- `ESFE.GestionProductos.LN/UsuarioLN.cs` — `ValidarLogin` refactorizado: obtiene usuario por nombre, verifica hash en C#, rehash on login transparente.
- `ESFE.GestionProductos.LN/UserLN.cs` — `Listar()` marcado `[Obsolete]`; `Crear`, `Actualizar(UsuarioFormDTO)` y `CambiarPassword` ahora hashean el password antes de persistir; `Insertar(Usuario)`/`Actualizar(Usuario)` documentados como pass-through opaco.
- `ESFE.InventarioProd.Web/Controllers/AccountController.cs` — `CambiarPassword` (POST) recibe `[Authorize]` explícito.

## 3. Reader de `UsuarioDAL.ObtenerPorNombre` (mapeo de `Rol`)

```csharp
public static Usuario? ObtenerPorNombre(string pNombre)
{
    Usuario? _usuario = null;

    using (IDbConnection _conexion = DBComun.ObtenerConexion())
    {
        _conexion.Open();

        using (SqlCommand _command = new SqlCommand("SP_ObtenerUsuarioPorNombre", _conexion as SqlConnection))
        {
            _command.CommandType = CommandType.StoredProcedure;
            _command.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar, 100) { Value = pNombre });

            using (IDataReader _reader = _command.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (_reader.Read())
                {
                    _usuario = new Usuario
                    {
                        IdUsuarioPK = Convert.ToInt32(_reader["IdUsuarioPK"]),
                        Nombre = _reader["Nombre"].ToString(),
                        Password = _reader["Password"].ToString(),
                        Id_RolFK = Convert.ToInt16(_reader["Id_RolFK"]),
                        NombreRol = _reader["Rol"].ToString(),
                        Estado = Convert.ToBoolean(_reader["Estado"])
                    };
                }
            }
        }
    }

    return _usuario;
}
```

La columna `Rol` del `SELECT` (alias del join con `Rol.NombreRol`, igual que en `SP_LoginUsuario`) se mapea directo a `Usuario.NombreRol` (propiedad `[NotMapped]` ya existente en la entidad). Esto evita tener que resolver el rol por separado en la capa LN — `UsuarioLN.ValidarLogin` simplemente lee `usuario.NombreRol`.

## 4. `UsuarioLN.ValidarLogin` completo

```csharp
public LoginResultDTO? ValidarLogin(string nombre, string password)
{
    if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(password))
        return null;

    // 1. Traer usuario por nombre (SP nuevo, sin filtro de password).
    Usuario? usuario = UsuarioDAL.ObtenerPorNombre(nombre);
    if (usuario == null || usuario.Estado != true)
        return null;

    // 2. Verificar password en C# (nunca en SQL).
    var resultado = PasswordHasherHelper.Verificar(password, usuario.Password ?? string.Empty);
    if (resultado == VerificacionPasswordResultado.Fallido)
        return null;

    // 3. Migración transparente: si el password estaba plano o el hash quedó viejo, rehashear en BD.
    //    Fallo del UPDATE = swallow silencioso (loguear a Debug). El login procede — no penalizamos al
    //    usuario por un problema de migración.
    if (resultado == VerificacionPasswordResultado.ExitosoRequiereMigracion)
    {
        try
        {
            usuario.Password = PasswordHasherHelper.Hash(password);
            // usuario ya trae Nombre/Id_RolFK/Estado del SP_ObtenerUsuarioPorNombre, así que el UPDATE
            // (sp_ActualizarUsuario_v2, que escribe todas las columnas) preserva esos valores tal como
            // estaban en BD.
            var userDAL = new UserDAL();
            userDAL.Actualizar(usuario);
        }
        catch (System.Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[PasswordMigration] Falló re-hash on login para usuario '{nombre}': {ex.Message}");
            // Login sigue.
        }
    }

    // NombreRol viene del join del SP (columna "Rol" del SELECT), mapeado en UsuarioDAL.ObtenerPorNombre.
    return new LoginResultDTO
    {
        Usuario = usuario,
        NombreRol = usuario.NombreRol ?? string.Empty
    };
}
```

Nota: el `Estado` de `Usuario` es `bool?`, por eso `usuario.Estado != true` sigue siendo la comprobación correcta (protección extra por si el SP alguna vez deja de filtrar `Estado = 1`).

## 5. Ejecución del SP en BD

Ejecutado contra `GestionProductoBD` en `DESKTOP-TF2SLSI\SQLEXPRESS` vía `sqlcmd -i Usuario_Login_v2.sql`. Confirmación posterior:

```
name                          create_date             modify_date
----------------------------- ----------------------- -----------------------
SP_ObtenerUsuarioPorNombre    2026-09-14 10:46:57.957 2026-09-14 10:46:58.027

EXEC SP_ObtenerUsuarioPorNombre @Nombre = 'gabriela';

IdUsuarioPK Nombre    Password  Id_RolFK Rol   Estado
----------- --------- --------- -------- ----- ------
          1 gabriela  1234abc   1        Admin 1
```

Columna `Rol` presente y correcta (mismo alias que `SP_LoginUsuario`), sin filtro de password.

## 6. Verificación en BD (estado actual, pre-migración)

```sql
SELECT IdUsuarioPK, Nombre, LEFT(Password, 4) AS PasswordPrefix, LEN(Password) AS PasswordLen, Estado
FROM Usuario;
```

```
IdUsuarioPK Nombre   PasswordPrefix PasswordLen Estado
----------- -------- -------------- ----------- ------
          1 gabriela 1234           7           1
          2 ismari   secu           9           1
          3 juanc    pwd7           6           1
          4 josias   supe           8           1
          5 pedroh   inv2           7           1
          6 Prueba   2330           4           1
          7 Natalia  Nata           10          1
```

Todos siguen en texto plano (longitudes cortas, 4-10 caracteres) porque no se ejecutó ningún login desde el código nuevo durante esta sesión (por instrucción explícita, no se probó el login funcionalmente). Cada fila migrará a hash Identity (~84 caracteres) individualmente la próxima vez que ese usuario haga login exitoso.

## 7. Build

`dotnet build ESFE.InventarioProd.Web/ESFE.InventarioProd.Web.csproj` (arrastra DAL/LN/EN vía `ProjectReference`): **Compilación correcta.**

Todos los warnings son preexistentes (nullable reference warnings `CS8618`/`CS8625`/`CS8600`/`CS8603` en EN/DAL/LN, no relacionados con este sprint). No aparecieron warnings `CS0618` (uso de miembro obsoleto), confirmando que no queda ningún caller activo de `UsuarioDAL.ValidarLogin` ni de `UserLN.Listar()`/`UserDAL.Listar()`.

## 8. Desvíos del plan

1. **`SP_ObtenerUsuarioPorNombre` no usa `CREATE OR ALTER` directo.** Esta instancia de SQL Server (`Microsoft SQL Server 2025 (RTM-GDR) - 17.0.1135.8`) lanza `Msg 208, Invalid object name 'SP_ObtenerUsuarioPorNombre'` cuando `CREATE OR ALTER PROCEDURE` se ejecuta sobre un procedimiento que **todavía no existe** — reproducido de forma consistente (probado también con un SP de prueba trivial, mismo error; funciona sin problema una vez que el SP ya existe). Se reemplazó por el patrón idempotente "stub + `ALTER`":
   ```sql
   IF OBJECT_ID('dbo.SP_ObtenerUsuarioPorNombre', 'P') IS NULL
       EXEC('CREATE PROCEDURE dbo.SP_ObtenerUsuarioPorNombre AS BEGIN SET NOCOUNT ON; END');
   GO
   ALTER PROCEDURE dbo.SP_ObtenerUsuarioPorNombre ...
   ```
   Documentado con comentario en el propio script. No afecta el comportamiento del SP final (mismo cuerpo que el diseño original), solo la forma de crearlo. Vale la pena tenerlo en cuenta si en el futuro se agregan más SPs nuevos con `CREATE OR ALTER` a este proyecto.
2. Todo lo demás se implementó tal como estaba especificado en el plan, sin otros desvíos.
