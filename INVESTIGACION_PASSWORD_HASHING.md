# Investigación: Módulo de Usuarios y superficie que toca `Password`

Generado en modo solo-lectura para diseñar el sprint de migración a `PasswordHasher<TUser>`. Ningún archivo del repo ni de la BD fue modificado durante esta investigación.

Alcance: solución `ESFE.GestionProductos` (proyectos `EN`, `DAL`, `LN`, `UI` WinForms, `ESFE.InventarioProd.Web` MVC). Base de datos de dev consultada: `GestionProductoBD` en `DESKTOP-TF2SLSI\SQLEXPRESS` (cadena de conexión hardcodeada en `ESFE.GestionProductos.DAL/DBCommun.cs`).

---

## 1. Módulo CRUD de Usuarios

**Sí existe** `UsuariosController`, en `ESFE.InventarioProd.Web/Controllers/UsuariosController.cs`. Es un controller **100% AJAX/JSON** (no usa vistas `Crear.cshtml`/`Editar.cshtml` tradicionales, todo pasa por un modal en `Index.cshtml`).

```csharp
[Authorize(Roles = "Admin")]
public class UsuariosController : Controller
{
    private readonly UserLN userLN = new UserLN();
    private readonly RolLN rolLN = new RolLN();

    // GET /Usuarios
    public IActionResult Index()

    // GET /Usuarios/Listar (AJAX)
    [HttpGet]
    public IActionResult Listar()

    // GET /Usuarios/ObtenerParaEdicion/{id} (AJAX)
    [HttpGet]
    public IActionResult ObtenerParaEdicion(int id)

    // POST /Usuarios/Crear (AJAX, JSON)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Crear([FromBody] UsuarioFormDTO form)

    // POST /Usuarios/Editar (AJAX, JSON)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Editar([FromBody] UsuarioFormDTO form)

    // POST /Usuarios/Eliminar (AJAX, JSON) — eliminación lógica
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Eliminar([FromBody] EliminarUsuarioRequest req)
}
```

No hay atributos `[HttpGet]/[HttpPost]` distintos por acción más allá de los mostrados; no hay acciones `EliminarLogico` separadas — la eliminación lógica ocurre dentro de `Eliminar`.

### Vistas asociadas

Solo existe **una** vista real para este módulo:

- `ESFE.InventarioProd.Web/Views/Usuarios/Index.cshtml`

No existen `Crear.cshtml`, `Editar.cshtml` ni `Eliminar.cshtml` — el CRUD completo (crear, editar, eliminar) vive dentro de `Index.cshtml` como dos modales (`#modal-usuario` y `#modal-confirmar-eliminar`) manejados con JavaScript vanilla que hace `fetch()` a las acciones del controller.

### ViewModels / DTOs usados

No hay un "ViewModel" MVC clásico con `[Required]` para Crear/Editar — el controller recibe directamente `UsuarioFormDTO` (definido en la capa LN) vía `[FromBody]`, y la validación de campos ocurre a mano dentro de `UserLN` (ver sección 2), no con Data Annotations.

```csharp
// ESFE.GestionProductos.LN/DTOs/UsuarioFormDTO.cs
public class UsuarioFormDTO
{
    public int? IdUsuarioPK { get; set; }      // null en Crear
    public string Nombre { get; set; } = string.Empty;
    public string? Password { get; set; }      // obligatoria en Crear; en blanco en Editar = conservar la actual
    public short? IdRolFK { get; set; }
    public bool Estado { get; set; } = true;   // ignorado en Crear (siempre true)
}
```

```csharp
// ESFE.GestionProductos.LN/DTOs/UsuarioEdicionDTO.cs
// No incluye Password: nunca se devuelve la contraseña al cliente.
public class UsuarioEdicionDTO
{
    public int IdUsuarioPK { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public short? IdRolFK { get; set; }
    public bool Estado { get; set; }
}
```

```csharp
// ESFE.GestionProductos.LN/DTOs/UsuarioListadoDTO.cs
public class UsuarioListadoDTO
{
    public int IdUsuarioPK { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public short? IdRolFK { get; set; }
    public string NombreRol { get; set; } = string.Empty;
    public bool Estado { get; set; }
}
```

El único ViewModel real de `Models/` que participa es:

```csharp
// ESFE.InventarioProd.Web/Models/UsuariosIndexViewModel.cs
public class UsuariosIndexViewModel
{
    public List<RolFiltroDTO> Roles { get; set; } = new();
}
```

Este solo alimenta el `<select>` de roles del modal; no tiene relación con `Password`.

### Campo Password en Editar: ¿obligatorio, opcional u otro?

**Opcional — vacío = "no cambiar".** Está implementado en tres capas coincidentes:

- Cliente (`Views/Usuarios/Index.cshtml:330`): solo valida "obligatoria" si `!modoEdicion` (es decir, en Crear). En Editar el campo puede quedar vacío.
- Servidor (`UserLN.ValidarFormulario`, `UserLN.cs:262`): `if (esCreacion && string.IsNullOrWhiteSpace(form.Password)) return Invalido(...)` — la obligatoriedad solo aplica en creación.
- Persistencia (`UserLN.Actualizar`, `UserLN.cs:190-198`): si `form.Password` viene vacío se setea `Password = null` en la entidad, y luego `Actualizar(Usuario usuario)` (`UserLN.cs:70-86`) detecta `string.IsNullOrWhiteSpace(usuario.Password)` y llama a `userDAL.ObtenerPasswordActual(usuario.IdUsuarioPK)` para recuperar y reinyectar la contraseña actual antes del `UPDATE`.

No es un campo separado (no hay "ConfirmarPassword" en el CRUD de administración; el "confirmar contraseña" solo existe en el flujo de autoservicio, sección 6).

---

## 2. Capa LN — métodos de Usuario

Hay **dos clases LN** relacionadas con `Usuario`: `UsuarioLN` (solo login) y `UserLN` (CRUD completo). Nombres inconsistentes entre sí (ver "Zonas grises").

### `ESFE.GestionProductos.LN.UsuarioLN`

| Método | Firma | Llama a (DAL) | Toca Password |
|---|---|---|---|
| `ValidarLogin` | `public LoginResultDTO? ValidarLogin(string nombre, string password)` | `UsuarioDAL.ValidarLogin(nombre, password)` (estático) | **Sí** — pasa el password en texto plano al DAL para que el SP lo compare con `=` |

### `ESFE.GestionProductos.LN.UserLN`

Instancia interna: `private readonly UserDAL userDAL = new UserDAL();`

| Método | Firma | Llama a (DAL) | Toca Password |
|---|---|---|---|
| Listar | `public DataTable Listar()` | `userDAL.Listar()` | Indirecto — el SP que invoca (`SP_ListarUsuarios`) seleccionaría la columna Password si existiera (ver sección 4/8, SP no existe con ese nombre) |
| Buscar | `public List<Usuario> Buscar(string nombre = null, short? idUsuario = null)` | `userDAL.Buscar(nombre, idUsuario)` | No — el SP `sp_BuscarUsuario` no selecciona `Password` |
| ObtenerActivos | `public List<Usuario> ObtenerActivos()` | `userDAL.Buscar(null, null)` + filtro en memoria `Estado == true` | No |
| Guardar | `public int Guardar(Usuario usuario)` | `userDAL.ObtenerPasswordActual(...)` (si blanco) + `userDAL.Actualizar`/`userDAL.Insertar` | **Sí** — lee password actual para conservarlo si viene vacío, y escribe el nuevo/actual |
| Insertar | `public int Insertar(Usuario usuario)` | `userDAL.Insertar(usuario)` | **Sí** — escribe `usuario.Password` tal cual (texto plano) |
| Actualizar | `public int Actualizar(Usuario usuario)` | `userDAL.ObtenerPasswordActual(...)` (si blanco) + `userDAL.Actualizar(usuario)` | **Sí** — lee (para conservar) y escribe |
| EliminarLogico | `public int EliminarLogico(short idUsuario)` | `userDAL.EliminarLogico(idUsuario)` | No |
| ListarParaWeb | `public List<UsuarioListadoDTO> ListarParaWeb()` | `userDAL.Buscar(null, null)` | No — el DTO de salida **nunca** incluye Password (comentario explícito en el código) |
| ObtenerParaEdicion | `public UsuarioEdicionDTO? ObtenerParaEdicion(int id)` | `userDAL.Buscar(null, (short)id).FirstOrDefault()` | No — el DTO de salida excluye Password a propósito |
| Crear | `public ResultadoGuardarUsuarioDTO Crear(UsuarioFormDTO form)` | `userDAL.Insertar(usuario)` (vía construcción de `Usuario` con `Password = form.Password!.Trim()`) | **Sí** — texto plano, trim únicamente |
| Actualizar (DTO) | `public ResultadoGuardarUsuarioDTO Actualizar(UsuarioFormDTO form)` | delega a `Actualizar(Usuario usuario)` de arriba | **Sí** |
| EliminarConValidacion | `public ResultadoEliminarUsuarioDTO EliminarConValidacion(int id, int idUsuarioActual)` | `EliminarLogico((short)id)` | No |
| NombreDuplicado (privado) | `private bool NombreDuplicado(string nombre, int idExcluir)` | `userDAL.Buscar(null, null)` | No |
| ValidarFormulario (privado, estático) | `private static ResultadoGuardarUsuarioDTO? ValidarFormulario(UsuarioFormDTO form, bool esCreacion)` | — | **Sí** — valida longitud (`> 256`) y obligatoriedad en creación, pero **no valida complejidad ni formato** |
| CambiarPassword | `public bool CambiarPassword(int idUsuario, string nuevaPassword)` | `userDAL.Buscar(null, (short)idUsuario).FirstOrDefault()` + `userDAL.Actualizar(usuario)` | **Sí** — único punto de entrada del autoservicio "Mi perfil"; asigna `usuario.Password = nuevaPassword.Trim()` en texto plano |

---

## 3. Capa DAL — métodos de Usuario

Hay **dos clases DAL**: `UsuarioDAL` (curiosamente definida dentro del archivo `LoginDAL.cs`, no `UsuarioDAL.cs` — ver "Zonas grises") y `UserDAL` (`UserDAL.cs`).

### `ESFE.GestionProductos.DAL.UsuarioDAL` (archivo físico: `LoginDAL.cs`)

| Método | Firma | SP | Parámetros | Retorna |
|---|---|---|---|---|
| ValidarLogin | `public static Usuario? ValidarLogin(string pNombre, string pPassword)` | `SP_LoginUsuario` | `@Nombre VARCHAR(100)`, `@Password VARCHAR(256)` | `Usuario?` (incluye `Password` leído de BD, aunque no se usa después) o `null` si no hay filas |

### `ESFE.GestionProductos.DAL.UserDAL`

| Método | Firma | SP | Parámetros | Retorna |
|---|---|---|---|---|
| Listar | `public DataTable Listar()` | `"SP_ListarUsuarios"` (string literal — **este SP no existe en la BD**, ver sección 8) | ninguno | `DataTable` |
| Buscar | `public List<Usuario> Buscar(string nombre = null, short? idUsuario = null)` | `sp_BuscarUsuario` | `@Nombre`, `@IdUsuarioPK` | `List<Usuario>` (no lee columna Password del reader — el `SqlDataReader` solo hace `GetOrdinal` de `IdUsuarioPK`, `Nombre`, `Id_RolFK`, `NombreRol`, `Estado`) |
| Insertar | `public int Insertar(Usuario usuario)` | `SP_InsertarUsuario` | `@Nombre`, `@Password`, `@Id_RolFK`, `@Estado` | filas afectadas de `ExecuteNonQuery()` |
| ObtenerPasswordActual | `public string ObtenerPasswordActual(int idUsuario)` | **SQL inline, no SP**: `"SELECT Password FROM Usuario WHERE IdUsuarioPK = @IdUsuarioPK"` | `@IdUsuarioPK` | password actual en texto plano, o `null` |
| Actualizar | `public int Actualizar(Usuario usuario)` | `sp_ActualizarUsuario_v2` (nota en código explica por qué no usa el original) | `@IdUsuarioPK`, `@Nombre`, `@Password`, `@Id_RolFK`, `@Estado` | filas afectadas (`ExecuteScalar()` → `SELECT @Filas AS FilasAfectadas`) |
| EliminarLogico | `public int EliminarLogico(short idUsuario)` | `sp_EliminarLogicoUsuario_v2` (misma razón) | `@IdUsuario` | filas afectadas (`ExecuteScalar()`) |

---

## 4. Stored Procedures que tocan Usuario

Búsqueda en `**/*.sql` del repo: solo hay **un** archivo con SPs de Usuario:

- `ESFE.GestionProductos.DAL/Scripts/Usuario_StoredProcedures_v2.sql`

**Importante:** los SPs "legados" (`SP_LoginUsuario`, `sp_BuscarUsuario`, `SP_InsertarUsuario`, `SP_ListarUsuario`, `sp_ActualizarUsuario`, `sp_EliminarLogicoUsuario`) **no existen como archivo `.sql` en el repositorio** — solo viven en la base de datos (compartidos con la app WinForms legada `ESFE.GestionProductos`). Fueron extraídos directamente de la BD de dev vía `sp_helptext` para este reporte.

### 4.1 `sp_ActualizarUsuario_v2` (repo: `Usuario_StoredProcedures_v2.sql`, líneas 21-58)

```sql
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
```

`Password` se **sobreescribe directo** con el valor que llega (`@Password = @Password`), sin comparar. Versión **`_v2`** (activa, usada por el sistema web).

### 4.2 `sp_EliminarLogicoUsuario_v2` (repo: `Usuario_StoredProcedures_v2.sql`, líneas 64-74)

```sql
CREATE OR ALTER PROCEDURE sp_EliminarLogicoUsuario_v2
    @IdUsuario INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Usuario SET Estado = 0 WHERE IdUsuarioPK = @IdUsuario;
    SELECT @@ROWCOUNT AS FilasAfectadas;
END
```

No toca `Password`. Versión **`_v2`** (activa).

### 4.3 `sp_ActualizarUsuario` (legado — solo en BD, sin archivo en repo)

```sql
CREATE PROCEDURE sp_ActualizarUsuario
    @IdUsuarioPK INT,
    @Nombre Varchar (100),
    @Password Varchar (256),
    @Id_RolFK SMALLINT,
    @Estado BIT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        IF EXISTS (SELECT 1 FROM Usuario WHERE IdUsuarioPK = @IdUsuarioPK)
        BEGIN
            UPDATE Usuario
            SET Nombre = @Nombre, Password = @Password, Id_RolFK = @Id_RolFK, Estado = @Estado
            WHERE IdUsuarioPK = @IdUsuarioPK;
            COMMIT TRANSACTION;
        END
        ELSE BEGIN
            ROLLBACK TRANSACTION;
            RAISERROR ('El usuario especificado no existe', 16, 1);
        END
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrorMessage, 16, 1);
    END CATCH
END;
```

Mismo `UPDATE ... Password = @Password` directo. **Legado**, usado hoy por la app WinForms (`ESFE.GestionProductos.UI`) — no invocado por el DAL del sistema web.

### 4.4 `sp_BuscarUsuario` (legado — solo en BD)

```sql
CREATE PROCEDURE sp_BuscarUsuario
    @Nombre      VARCHAR(100) = NULL,
    @IdUsuarioPK SMALLINT     = NULL,
    @IdRolFK     SMALLINT     = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        U.IdUsuarioPK,
        U.Nombre,
        U.Id_RolFK,
        R.NombreRol AS NombreRol,
        U.Estado
    FROM Usuario AS U
    LEFT JOIN Rol AS R ON U.Id_RolFK = R.IdRolPK
    WHERE (@Nombre      IS NULL OR U.Nombre LIKE '%' + @Nombre + '%')
      AND (@IdUsuarioPK IS NULL OR U.IdUsuarioPK = @IdUsuarioPK)
      AND (@IdRolFK     IS NULL OR U.Id_RolFK    = @IdRolFK);
END;
```

No selecciona `Password` — seguro para el listado. Usado por `UserDAL.Buscar`. Solo existe en BD, sin `_v2`.

### 4.5 `SP_InsertarUsuario` (legado — solo en BD)

```sql
CREATE PROCEDURE SP_InsertarUsuario
    @Nombre VARCHAR(100),
    @Password VARCHAR(256),
    @Id_RolFK SMALLINT,
    @Estado BIT
AS
BEGIN
    INSERT INTO Usuario (Nombre, Password, Id_RolFK, Estado)
    VALUES (@Nombre, @Password, @Id_RolFK, @Estado);
END;
```

Inserta `Password` directo, sin `_v2`. Usado hoy por `UserDAL.Insertar`.

### 4.6 `SP_ListarUsuario` (legado — solo en BD; **nombre singular**, ver sección 8)

```sql
CREATE PROCEDURE SP_ListarUsuario
AS
BEGIN
    SELECT
        U.IdUsuarioPK AS [ID Usuario],
        U.Nombre AS [Nombre de Cuenta],
        U.Password AS [Contraseña Hash],
        R.NombreRol AS [Rol Asignado],
        CASE U.Estado WHEN 1 THEN 'Activo' ELSE 'Inactivo' END AS [Estado]
    FROM Usuario AS U
    INNER JOIN Rol AS R ON U.Id_RolFK = R.IdRolPK;
END;
```

**Selecciona `Password` completo** con alias `[Contraseña Hash]` (el alias ya anticipaba hashing, pero hoy es texto plano). Sin `_v2`. Este SP es invocado desde el proyecto WinForms si existe una pantalla que use un listado tabular; no se identificó llamada directa en `.cs` fuera de la incongruencia de nombre descrita en sección 8.

### 4.7 `usp_Usuario_Update` (solo en BD — **no referenciado por ningún `.cs` del repo**)

```sql
CREATE PROCEDURE dbo.usp_Usuario_Update
    @IdUsuarioPK INT,
    @Nombre VARCHAR(100),
    @Password VARCHAR(256),
    @Id_RolFK SMALLINT,
    @Estado BIT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM dbo.Usuario WHERE IdUsuarioPK = @IdUsuarioPK)
        BEGIN
            THROW 51000, 'Error: El IdUsuario proporcionado no existe en el sistema.', 1;
        END
        BEGIN TRANSACTION;
        UPDATE dbo.Usuario
        SET Nombre = @Nombre, Password = @Password, Id_RolFK = @Id_RolFK, Estado = @Estado
        WHERE IdUsuarioPK = @IdUsuarioPK;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
```

SP huérfano en la BD: existe pero **ningún `.cs` del repo lo invoca** (búsqueda `grep -r "usp_Usuario_Update"` sin resultados en código). Sobreescribe `Password` directo igual que los demás.

---

## 5. `SP_LoginUsuario` — detalle

Obtenido de la BD de dev vía `EXEC sp_helptext 'SP_LoginUsuario'`. **No existe como archivo `.sql` en el repositorio.**

```sql
CREATE PROCEDURE SP_LoginUsuario
    @Nombre VARCHAR(100),
    @Password VARCHAR(256)
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
      AND U.Password = @Password
      AND U.Estado = 1;
END;
```

**Validación exacta hoy:** el SP compara `U.Password = @Password` **dentro del propio SQL**, con comparación de igualdad de string sobre `VARCHAR(256)`. Si no hay fila que cumpla `Nombre + Password + Estado=1` simultáneamente, el `SqlDataReader` no devuelve filas y `UsuarioDAL.ValidarLogin` retorna `null` (mapeado en la capa LN a "Credenciales inválidas o usuario inactivo").

**Implicación directa para el sprint de hashing:** este SP filtra por `Password` en el `WHERE`, así que **no puede seguir comparando con `=` una vez que la columna `Password` contenga un hash** — el hashing con `PasswordHasher<TUser>` no es determinístico byte-a-byte reproducible desde SQL (usa salt aleatorio), por lo que la comparación **debe** moverse a C# (`PasswordHasher.VerifyHashedPassword`) y el SP debe dejar de recibir/filtrar por `@Password`, retornando el usuario solo por `@Nombre` (+ `Estado = 1`) para que la app verifique el hash en memoria. Como el mismo SP es compartido con la app WinForms ESFE (ver comentario en `UserDAL.cs` sobre por qué se crearon las versiones `_v2` para no tocar los SPs legados), cualquier cambio a `SP_LoginUsuario` **rompe el login del WinForms** salvo que también se migre ese cliente o se cree un `SP_LoginUsuario_v2` siguiendo el mismo patrón ya establecido en el repo.

---

## 6. Flujo `/Account/CambiarPassword`

- **Archivo:** `ESFE.InventarioProd.Web/Controllers/AccountController.cs`
- **Acción:** `CambiarPassword` (una sola, solo `[HttpPost]` — no hay `[HttpGet] CambiarPassword`, la pantalla se sirve desde `GET /Account/Perfil`)

```csharp
[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult CambiarPassword(PerfilViewModel model)
{
    // Recargar datos de solo lectura: el POST no los trae de vuelta
    model.Nombre = User.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty;
    model.NombreRol = User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty;

    if (!ModelState.IsValid)
        return View("Perfil", model);

    var validacion = usuarioLN.ValidarLogin(model.Nombre, model.PasswordActual);
    if (validacion == null)
    {
        ModelState.AddModelError(nameof(model.PasswordActual), "La contraseña actual no es correcta.");
        return View("Perfil", model);
    }

    try
    {
        bool actualizado = userLN.CambiarPassword(validacion.Usuario.IdUsuarioPK, model.PasswordNueva);
        if (!actualizado)
        {
            ModelState.AddModelError(string.Empty, "No se pudo actualizar la contraseña.");
            return View("Perfil", model);
        }
    }
    catch (ArgumentException ex)
    {
        ModelState.AddModelError(string.Empty, ex.Message);
        return View("Perfil", model);
    }

    TempData["PerfilMensaje"] = "Contraseña actualizada correctamente.";
    return RedirectToAction("Perfil");
}
```

**Nota:** esta acción **no tiene `[Authorize]`** a nivel de método (el controller tampoco tiene `[Authorize]` de clase — solo `Perfil()` GET lo declara explícitamente). Ver "Zonas grises".

### ViewModel

```csharp
// ESFE.InventarioProd.Web/Models/PerfilViewModel.cs
public class PerfilViewModel
{
    public string Nombre { get; set; } = string.Empty;
    public string NombreRol { get; set; } = string.Empty;

    [Required(ErrorMessage = "La contraseña actual es obligatoria")]
    [DataType(DataType.Password)]
    [Display(Name = "Contraseña actual")]
    public string PasswordActual { get; set; } = string.Empty;

    [Required(ErrorMessage = "La nueva contraseña es obligatoria")]
    [StringLength(256, ErrorMessage = "La contraseña no puede superar los 256 caracteres")]
    [DataType(DataType.Password)]
    [Display(Name = "Nueva contraseña")]
    public string PasswordNueva { get; set; } = string.Empty;

    [Required(ErrorMessage = "Debes confirmar la nueva contraseña")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirmar nueva contraseña")]
    [Compare(nameof(PasswordNueva), ErrorMessage = "Las contraseñas no coinciden")]
    public string PasswordConfirmar { get; set; } = string.Empty;
}
```

Sin `[MinLength]` ni validación de complejidad — solo obligatoriedad, longitud máxima (256) y coincidencia con la confirmación.

### Validación de la contraseña anterior (hoy)

Reutiliza **el mismo camino que el login**: `usuarioLN.ValidarLogin(model.Nombre, model.PasswordActual)` → `UsuarioDAL.ValidarLogin` → `SP_LoginUsuario`, es decir, vuelve a ejecutar una comparación `=` en SQL contra la contraseña en texto plano. Esto significa que **el sprint de hashing debe tocar este flujo dos veces**: una para el login normal y otra porque `CambiarPassword` depende del mismo método `ValidarLogin`.

### Persistencia de la nueva contraseña

`userLN.CambiarPassword(validacion.Usuario.IdUsuarioPK, model.PasswordNueva)` → `UserLN.CambiarPassword` (`UserLN.cs:278-296`):

```csharp
public bool CambiarPassword(int idUsuario, string nuevaPassword)
{
    if (idUsuario <= 0)
        throw new ArgumentException("Usuario inválido.");
    if (string.IsNullOrWhiteSpace(nuevaPassword))
        throw new ArgumentException("La nueva contraseña es obligatoria.");
    if (nuevaPassword.Length > 256)
        throw new ArgumentException("La contraseña no puede superar los 256 caracteres.");

    var usuario = userDAL.Buscar(null, (short)idUsuario).FirstOrDefault();
    if (usuario == null)
        return false;

    usuario.Password = nuevaPassword.Trim();
    return userDAL.Actualizar(usuario) > 0;
}
```

Termina en `UserDAL.Actualizar` → `sp_ActualizarUsuario_v2` (mismo SP que usa el CRUD de administración de la sección 1-3). **Punto interesante:** `userDAL.Buscar(...)` no trae `Password` en el objeto `Usuario` (el reader de `Buscar` no lee esa columna — ver sección 3), así que `usuario.Password` llega `null` desde el `Buscar`, se sobreescribe con la nueva contraseña, y el resto de columnas (`Nombre`, `Id_RolFK`, `Estado`) se reenvían intactas al `UPDATE`.

---

## 7. Verificación en BD

Query ejecutado contra `GestionProductoBD` (dev, `DESKTOP-TF2SLSI\SQLEXPRESS`):

```sql
SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, IS_NULLABLE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Usuario';
```

Resultado real:

| COLUMN_NAME | DATA_TYPE | CHARACTER_MAXIMUM_LENGTH | IS_NULLABLE |
|---|---|---|---|
| IdUsuarioPK | int | NULL | NO |
| Nombre | varchar | 100 | YES |
| Password | **varchar** | **256** | YES |
| Id_RolFK | smallint | NULL | YES |
| Estado | bit | NULL | YES |

**`Password` es `VARCHAR(256)`, nullable.** `PasswordHasher<TUser>.HashPassword` produce una cadena Base64 de longitud fija (~84 caracteres en la variante actual de ASP.NET Core Identity), así que 256 caracteres **alcanza de sobra** — no hace falta `ALTER TABLE` para el tamaño. Ojo: es `VARCHAR` (no `NVARCHAR`); el Base64 del hash usa solo caracteres ASCII, así que no debería haber pérdida de datos, pero conviene confirmarlo explícitamente en el diseño del sprint.

---

## 8. Búsqueda global de "Password"

Grep case-insensitive de `password` en `*.cs`, `*.cshtml`, `*.sql` (excluyendo `bin/`, `obj/`, `.git/`). Se listan los hallazgos relevantes agrupados por archivo (se omiten líneas puramente de UI de WinForms sin relación semántica, ej. `PasswordChar`/`UseSystemPasswordChar` de controles `MaterialMaskedTextBox`, que solo controlan el enmascarado visual del textbox y no tocan el dato):

### Backend / lógica

- `ESFE.GestionProductos.EN/Usuario.cs:17` — `[StringLength(256)] public string? Password { get; set; }` — la entidad central, mapea 1:1 la columna de BD.
- `ESFE.GestionProductos.DAL/LoginDAL.cs` (clase `UsuarioDAL`) — líneas 15, 27-29, 39: construye y ejecuta `SP_LoginUsuario` pasando `@Password` en texto plano y leyendo `Password` del reader hacia `Usuario.Password`.
- `ESFE.GestionProductos.DAL/UserDAL.cs` — líneas 116-117 (`Insertar`), 133-151 (`ObtenerPasswordActual`, con SQL inline fuera de SP), 179-180 (`Actualizar`) — todos los puntos DAL que leen/escriben la columna.
- `ESFE.GestionProductos.LN/UsuarioLN.cs:10-12` — `ValidarLogin(string nombre, string password)` reenvía el password sin tocarlo.
- `ESFE.GestionProductos.LN/UserLN.cs` — múltiples líneas (48-49, 82-83, 153, 195, 262-266, 278-296) — toda la lógica de "conservar si viene vacío", validación de longitud máxima, y `CambiarPassword`.
- `ESFE.GestionProductos.LN/DTOs/UsuarioFormDTO.cs:7` — campo `Password` de entrada del formulario Crear/Editar.
- `ESFE.GestionProductos.LN/DTOs/UsuarioEdicionDTO.cs:3` — comentario explícito: *"No incluye Password: nunca se devuelve la contraseña al cliente."*

### Web (controllers/views)

- `ESFE.InventarioProd.Web/Controllers/AccountController.cs` — líneas 37 (`Login`), 93/102/105/111 (`CambiarPassword`) — únicos puntos del controller que tocan password.
- `ESFE.InventarioProd.Web/Models/LoginViewModel.cs:12-14` — `[Required] [DataType(DataType.Password)] Password`.
- `ESFE.InventarioProd.Web/Models/PerfilViewModel.cs:11-25` — `PasswordActual`, `PasswordNueva`, `PasswordConfirmar` (ver sección 6).
- `ESFE.InventarioProd.Web/Views/Account/Login.cshtml:40-42` — input del formulario de login (`asp-for="Password"`).
- `ESFE.InventarioProd.Web/Views/Account/Perfil.cshtml:33-50` — formulario de cambio de contraseña (`PasswordActual`, `PasswordNueva`, `PasswordConfirmar`).
- `ESFE.InventarioProd.Web/Views/Usuarios/Index.cshtml` — múltiples líneas (53-56, 121-123, 268-349) — input/lógica JS del modal Crear/Editar.
- `ESFE.InventarioProd.Web/Views/html/usuario.cshtml:270-271, 354, 370` — **mockup HTML estático huérfano**, ver "Zonas grises".
- `ESFE.InventarioProd.Web/Views/html/login.cshtml:58-65` — **mockup HTML estático huérfano**, excluido explícitamente del build (`<Content Remove="Views\html\login.cshtml" />` + `<None Include=...>` en el `.csproj`), ver "Zonas grises".

### WinForms (`ESFE.GestionProductos.UI`) — comparte la misma BD/SPs legados

- `ESFE.GestionProductos.UI/frmUsuarioModal.cs:87` — `txtPassword.Text = UsuarioActual.Password ?? string.Empty;` — **la contraseña actual se muestra en texto plano** dentro del textbox al abrir "Editar Usuario" (aunque el control tiene `UseSystemPasswordChar = true`, así que visualmente queda enmascarada, pero el valor real está cargado en el control).
- `ESFE.GestionProductos.UI/frmUsuarioModal.cs:116` — `UsuarioActual.Password = txtPassword.Text.Trim();` — siempre reenvía lo que esté en el textbox (a diferencia del flujo web, **no tiene lógica de "vacío = conservar"**; si el admin borra el campo antes de guardar, la contraseña se pone en blanco).
- `ESFE.GestionProductos.UI/login.cs:127, 136, 200-219` — pantalla de login WinForms, mismo flujo de credenciales en texto plano.

### SQL

- `ESFE.GestionProductos.DAL/Scripts/Usuario_StoredProcedures_v2.sql` — líneas 24, 39 (`sp_ActualizarUsuario_v2` sobreescribe `Password` directo).
- SPs legados (`SP_LoginUsuario`, `sp_ActualizarUsuario`, `SP_InsertarUsuario`, `SP_ListarUsuario`, `usp_Usuario_Update`) — no están en archivos `.sql` del repo; su texto completo se extrajo de la BD y se documenta en las secciones 4 y 5.

**No se encontraron** seeds, fixtures, datos de prueba hardcodeados, ni archivos de test (`*Test*.cs`, `*Tests*.cs`) en toda la solución — no hay proyecto de pruebas automatizadas.

---

## 9. Paquetes NuGet actuales

### `ESFE.InventarioProd.Web/ESFE.InventarioProd.Web.csproj`
```xml
<PackageReference Include="ClosedXML" Version="0.104.*" />
```
(+ `ProjectReference` a `ESFE.GestionProductos.LN`)

### `ESFE.GestionProductos.LN/ESFE.GestionProductos.LN.csproj`
```xml
<PackageReference Include="Microsoft.Data.SqlClient" Version="7.0.2" />
```
(+ `ProjectReference` a `DAL` y `EN`)

### `ESFE.GestionProductos.DAL/ESFE.GestionProductos.DAL.csproj`
```xml
<PackageReference Include="Microsoft.Data.SqlClient" Version="7.0.2" />
```
(+ `ProjectReference` a `EN`)

### `ESFE.GestionProductos.EN/ESFE.GestionProductos.EN.csproj`
```xml
<PackageReference Include="Microsoft.Data.SqlClient" Version="7.0.2" />
```
Sin `ProjectReference` (proyecto base).

**`Microsoft.Extensions.Identity.Core` NO está referenciado en ningún `.csproj` de la solución** (se revisaron los 4 pedidos más `ESFE.GestionProductos.UI.csproj`, que tampoco lo tiene). Habrá que agregarlo explícitamente — probablemente en `ESFE.GestionProductos.LN` (donde vive hoy toda la lógica de password) o en `ESFE.InventarioProd.Web` si se prefiere mantener el hashing fuera de la capa LN compartida con WinForms.

---

## 10. Roles y `[Authorize]` en el módulo Usuarios

- `UsuariosController` (CRUD de administración): **`[Authorize(Roles = "Admin")]` a nivel de clase** (`UsuariosController.cs:11`) — aplica a las 5 acciones (`Index`, `Listar`, `ObtenerParaEdicion`, `Crear`, `Editar`, `Eliminar`). Solo el rol `"Admin"` (string literal, comparado contra el claim `ClaimTypes.Role` seteado en el login) puede acceder.
- `AccountController`: **sin `[Authorize]` de clase.** Por acción:
  - `Login` (GET/POST) → `[AllowAnonymous]`
  - `Logout` (POST) → sin atributo (autenticado o no, ejecuta `SignOutAsync`; no hay corte funcional distinto)
  - `AccessDenied` (GET) → `[AllowAnonymous]`
  - `Perfil` (GET) → `[Authorize]` (sin rol específico — cualquier usuario autenticado)
  - `CambiarPassword` (POST) → **sin `[Authorize]` explícito** (ver "Zonas grises")
- No hay `FallbackPolicy` ni `RequireAuthenticatedUser()` global en `Program.cs` — la autorización es 100% opt-in por atributo, no hay protección por defecto para acciones sin `[Authorize]`.
- El rol se determina en el login (`AccountController.Login`, POST) leyendo `resultado.NombreRol` (viene de `UsuarioLN.ValidarLogin` → join con tabla `Rol`) y se guarda como claim `ClaimTypes.Role`. No hay enum ni constante centralizada de roles — `"Admin"` es un string literal en el atributo.

---

## Zonas grises detectadas

1. **`SP_ListarUsuarios` no existe.** `UserDAL.Listar()` (`UserDAL.cs:21-23`) ejecuta el string literal `"SP_ListarUsuarios"` (plural), pero en la BD el procedimiento se llama `SP_ListarUsuario` (singular). Si algo llegara a invocar `UserLN.Listar()` / `UserDAL.Listar()`, lanzaría `SqlException: Could not find stored procedure 'SP_ListarUsuarios'`. Hoy es inofensivo porque **no se encontró ningún llamador** de ese método en toda la solución (ni Web ni WinForms) — es código muerto con un bug latente. Vale la pena decidir en el sprint si se elimina o se corrige, ya que al tocar el módulo de todas formas conviene no dejar una trampa.

2. **Naming inconsistente entre capas.** Existen simultáneamente `UsuarioLN`/`UsuarioDAL` (solo login) y `UserLN`/`UserDAL` (CRUD completo) para la misma entidad `Usuario`. Además, la clase `UsuarioDAL` está físicamente definida dentro del archivo `LoginDAL.cs`, no en `UsuarioDAL.cs` — el nombre de archivo no coincide con el nombre de la clase. Esto puede generar confusión al ubicar dónde vive cada pieza durante el sprint.

3. **`usp_Usuario_Update` es un SP huérfano.** Existe en la BD, tiene lógica de actualización completa (incluye `Password`), pero ningún archivo `.cs` de la solución lo referencia. Origen desconocido (¿prueba manual, migración abandonada, intento previo de "v2" que se descartó?). Debería auditarse/eliminarse o documentarse antes de decidir si el sprint de hashing lo toca.

4. **`CambiarPassword` (POST) sin `[Authorize]` explícito.** A diferencia de `Perfil` (GET), que sí tiene `[Authorize]`, la acción `CambiarPassword` no lo declara. En la práctica el flujo depende de que `User.FindFirst(ClaimTypes.Name)` devuelva un nombre válido para poder llamar a `ValidarLogin`, así que un usuario anónimo terminaría con `Nombre = ""` y probablemente fallando la validación de contraseña actual — pero es un comportamiento "accidental", no una protección declarada. Vale la pena agregar `[Authorize]` explícito al mismo tiempo que se toque este método para el hashing, por defensa en profundidad.

5. **`ObtenerPasswordActual` usa SQL inline, no un SP.** Es el único punto de toda la capa DAL de Usuario que no pasa por un stored procedure (`UserDAL.cs:139-141`): `"SELECT Password FROM Usuario WHERE IdUsuarioPK = @IdUsuarioPK"`. Aunque está parametrizado (no hay riesgo de inyección), rompe el patrón "todo por SP" del resto del módulo. Con hashing, este método simplemente devolverá el hash actual en vez del texto plano — su contrato (`string`) no cambia, pero conviene revisar si su nombre (`ObtenerPasswordActual`) sigue siendo preciso o debería renombrarse a algo como `ObtenerHashActual`.

6. **El flujo `CambiarPassword` reutiliza `ValidarLogin`/`SP_LoginUsuario` para validar la contraseña actual.** Esto significa que el sprint de hashing no solo debe migrar el login "de verdad", sino también este segundo caller de `ValidarLogin`, que vive en un controller distinto (`AccountController.CambiarPassword`) y con una historia de errores previa (el comentario en `Usuario_StoredProcedures_v2.sql` documenta un bug histórico de HTTP 200 falso-positivo en este mismo endpoint, ya resuelto con los SPs `_v2` pero sin relación con hashing).

7. **Vistas mockup huérfanas dentro de `Views/`.** `ESFE.InventarioProd.Web/Views/html/usuario.cshtml`, `login.cshtml`, `create.cshtml`, `delete.cshtml`, `details.cshtml`, `edit.cshtml`, `proveedores.cshtml` son maquetas estáticas (con datos hardcodeados como "Marta Ríos · Administrador", "Ana Pérez") que **no están conectadas a ningún controller real** — no hay acción que haga `return View("html/usuario")`. `login.cshtml` está explícitamente excluido del build en el `.csproj` (`<Content Remove="Views\html\login.cshtml" /> <None Include="Views\html\login.cshtml" />`), pero los demás archivos de esa carpeta no tienen esa exclusión y probablemente sí se compilan como razor views aunque nadie los renderice. Es ruido a tener en cuenta al grepear "Password" (ya filtrado en sección 8) pero no requiere cambios para el sprint de hashing salvo que alguien decida limpiar la carpeta.

8. **Discrepancia de comportamiento WinForms vs Web al "dejar en blanco" la contraseña en Editar.** En la Web, dejar el campo Password vacío en Editar **conserva** la contraseña actual (lógica explícita en `UserLN.Actualizar`/`Guardar`). En WinForms (`frmUsuarioModal.cs:116`), **no existe esa protección** — si el usuario borra el textbox antes de guardar, `UsuarioActual.Password = txtPassword.Text.Trim()` queda como cadena vacía y se persiste tal cual vía `sp_ActualizarUsuario` (legado). Esto es una inconsistencia funcional preexistente (no introducida por este análisis) que probablemente convenga señalar al equipo, ya que después del hashing una contraseña vacía hasheada seguiría siendo "válida" para `PasswordHasher.VerifyHashedPassword`, lo cual sería aún más confuso que hoy.

9. **`Usuario.Password` en la entidad EN se comparte entre todos los flujos**, incluido el que llega desde `SP_LoginUsuario` (que sí trae el valor de BD) y los que nunca lo traen (`Buscar`, usado por `ListarParaWeb`/`ObtenerParaEdicion`/`CambiarPassword`). Hoy esto se maneja con disciplina manual (comentarios explícitos "nunca se devuelve la contraseña al cliente"), pero no hay ningún mecanismo de tipos que lo garantice — cualquier nuevo DTO que mapee `Usuario` completo podría filtrar el hash si no se es cuidadoso.

10. **Cadena de conexión hardcodeada en código fuente.** `DBCommun.cs:14` tiene `Data Source=DESKTOP-TF2SLSI\SQLEXPRESS;...` embebido como `const string`, no en `appsettings.json` ni en variables de entorno. No es parte del alcance de este sprint, pero es la razón por la que este reporte pudo conectarse directo a la BD de dev sin configuración adicional — vale mencionarlo porque cualquier cambio de SP en dev es inmediato para cualquiera con este repo, sin gestión de secretos.
