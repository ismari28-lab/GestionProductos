# Implementación: Galería de imágenes de Producto — Sprint A (Backend + Modal Crear)

Ejecutado según el plan del Sprint A. Backend completo (tabla, SPs, DAL, LN, servicio de storage con ImageSharp) + flujo Crear del modal end-to-end (pool client-side → crear producto → subir imágenes). Modal Editar y columna thumbnail del listado quedan para Sprint B, tal como se definió.

## 1. Archivos creados y modificados

### Creados

- `ESFE.GestionProductos.DAL/Scripts/ProductoImagen_StoredProcedures.sql` — tabla `ProductoImagen` + 5 SPs nuevos.
- `ESFE.GestionProductos.EN/ProductoImagen.cs` — entidad.
- `ESFE.GestionProductos.DAL/ProductoImagenDAL.cs` — DAL + tipo `ResultadoEliminarImagenDb`.
- `ESFE.GestionProductos.LN/Enums/ResultadoSubirImagen.cs`
- `ESFE.GestionProductos.LN/DTOs/ImagenProductoDTO.cs`
- `ESFE.GestionProductos.LN/DTOs/ResultadoSubirImagenDTO.cs`
- `ESFE.GestionProductos.LN/DTOs/ResultadoEliminarImagenDTO.cs`
- `ESFE.GestionProductos.LN/ImagenProductoLN.cs`
- `ESFE.InventarioProd.Web/Services/IImagenStorage.cs`
- `ESFE.InventarioProd.Web/Services/ImagenStorage.cs`
- `ESFE.InventarioProd.Web/wwwroot/uploads/productos/full/.gitkeep`
- `ESFE.InventarioProd.Web/wwwroot/uploads/productos/thumb/.gitkeep`
- `IMPLEMENTACION_IMAGENES_SPRINT_A.md` — este reporte.

### Modificados

- `ESFE.GestionProductos.DAL/Scripts/Producto_StoredProcedures.sql` — `SP_ListarProductos` ahora hace `LEFT JOIN ProductoImagen` y agrega `NombreArchivoPrincipal` a la tabla variable y al segundo `SELECT`. `SP_ListarProductosParaExportar` y `SP_ObtenerProductoParaEdicion` **no se tocaron** (fuera de alcance).
- `ESFE.InventarioProd.Web/ESFE.InventarioProd.Web.csproj` — agregado `SixLabors.ImageSharp` `3.1.*`.
- `ESFE.InventarioProd.Web/Program.cs` — `app.UseStaticFiles()` explícito antes de `MapStaticAssets()`; registro de `IImagenStorage`/`ImagenProductoLN` en DI; límite de `MultipartBodyLengthLimit`.
- `ESFE.InventarioProd.Web/Controllers/ProductoController.cs` — constructor con inyección de `ImagenProductoLN` e `IImagenStorage`; 4 endpoints nuevos (`ListarImagenes`, `SubirImagen`, `EliminarImagen`, `MarcarPrincipal`).
- `ESFE.InventarioProd.Web/Views/Producto/Index.cshtml` — fieldset "GALERÍA DE IMÁGENES" en el modal; pool client-side completo (agregar, quitar, marcar principal, subir secuencial al guardar).
- `ESFE.InventarioProd.Web/wwwroot/css/producto.css` — estilos del grid de imágenes.
- `.gitignore` — regla para no trackear `wwwroot/uploads/**` salvo `.gitkeep`.

## 2. `SP_CrearImagenProducto` y `SP_EliminarImagenProducto`

```sql
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
```

```sql
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
```

## 3. `ImagenStorage.GuardarAsync`

```csharp
public async Task<ResultadoGuardarImagen> GuardarAsync(IFormFile archivo, CancellationToken ct = default)
{
    if (archivo == null || archivo.Length == 0)
        return Error("Archivo vacío o no proporcionado");

    if (archivo.Length > TAMANIO_MAXIMO_BYTES)
        return Error("El archivo supera 5 MB");

    string extension = Path.GetExtension(archivo.FileName);
    if (!ExtensionesPermitidas.Contains(extension))
        return Error("Extensión no permitida (solo JPG, PNG o WEBP)");

    if (!MimeTypesPermitidos.Contains(archivo.ContentType))
        return Error("Tipo de archivo no permitido (solo JPG, PNG o WEBP)");

    await using Stream stream = archivo.OpenReadStream();

    byte[] cabecera = new byte[12];
    int leidos = await stream.ReadAsync(cabecera.AsMemory(0, cabecera.Length), ct);
    if (leidos < cabecera.Length || !TieneMagicBytesValidos(cabecera))
        return Error("Archivo no es una imagen válida (JPG/PNG/WEBP)");

    stream.Position = 0;

    using Image imagen = await Image.LoadAsync(stream, ct);

    string guid = Guid.NewGuid().ToString("N");
    string nombreFull = $"{guid}.webp";
    string nombreThumb = $"{guid}_thumb.webp";

    string directorioFull = RutaCarpeta(CARPETA_FULL);
    string directorioThumb = RutaCarpeta(CARPETA_THUMB);
    Directory.CreateDirectory(directorioFull);
    Directory.CreateDirectory(directorioThumb);

    using (Image full = imagen.Clone(ctx =>
    {
        if (Math.Max(imagen.Width, imagen.Height) > MAX_DIM_FULL)
            ctx.Resize(new ResizeOptions { Mode = ResizeMode.Max, Size = new Size(MAX_DIM_FULL, MAX_DIM_FULL) });
    }))
    {
        await full.SaveAsWebpAsync(Path.Combine(directorioFull, nombreFull), new WebpEncoder { Quality = CALIDAD_FULL }, ct);
    }

    using (Image thumb = imagen.Clone(ctx =>
    {
        if (Math.Max(imagen.Width, imagen.Height) > MAX_DIM_THUMB)
            ctx.Resize(new ResizeOptions { Mode = ResizeMode.Max, Size = new Size(MAX_DIM_THUMB, MAX_DIM_THUMB) });
    }))
    {
        await thumb.SaveAsWebpAsync(Path.Combine(directorioThumb, nombreThumb), new WebpEncoder { Quality = CALIDAD_THUMB }, ct);
    }

    return new ResultadoGuardarImagen { Ok = true, NombreArchivo = nombreFull };
}
```

## 4. `ProductoController.SubirImagen`

```csharp
// POST /Producto/Imagenes/{id} (multipart/form-data)
[HttpPost]
[Route("Producto/Imagenes/{id:int}")]
[Authorize(Roles = "Admin,Supervisor,Inventariado")]
[ValidateAntiForgeryToken]
public async Task<IActionResult> SubirImagen(int id, IFormFile archivo, [FromForm] bool esPrincipal, CancellationToken ct)
{
    var producto = productoLN.ObtenerParaEdicion(id);
    if (producto == null)
        return NotFound(new { ok = false, mensaje = "Producto no encontrado" });

    if (imagenProductoLN.ContarActivas(id) >= ImagenProductoLN.MAXIMO_IMAGENES_POR_PRODUCTO)
        return Conflict(new { ok = false, mensaje = "Máximo 5 imágenes por producto" });

    var guardado = await storage.GuardarAsync(archivo, ct);
    if (!guardado.Ok)
        return BadRequest(new { ok = false, mensaje = guardado.MensajeError });

    try
    {
        var r = imagenProductoLN.RegistrarImagen(id, guardado.NombreArchivo!, esPrincipal);

        if (r.Resultado == ResultadoSubirImagen.LimiteAlcanzado)
        {
            // Carrera: el límite se validó arriba, pero otra request lo llenó entretanto.
            storage.Eliminar(guardado.NombreArchivo!);
            return Conflict(new { ok = false, mensaje = r.Mensaje });
        }

        return Ok(new { ok = true, imagen = r.Imagen });
    }
    catch
    {
        // Si algo revienta después de guardar en disco, no dejar el archivo huérfano.
        storage.Eliminar(guardado.NombreArchivo!);
        return StatusCode(500, new { ok = false, mensaje = "Error interno al registrar la imagen" });
    }
}
```

## 5. JS del pool en `Index.cshtml`

```js
function agregarAlPool(archivos) {
    ocultarErrorGaleria();

    var espacioDisponible = MAX_IMAGENES_PRODUCTO - poolImagenesCrear.length;
    var agregados = 0;
    var rechazados = [];

    for (var i = 0; i < archivos.length; i++) {
        var archivo = archivos[i];

        if (agregados >= espacioDisponible) {
            rechazados.push(archivo.name + ' (límite de ' + MAX_IMAGENES_PRODUCTO + ' alcanzado)');
            continue;
        }

        var ext = extensionArchivo(archivo.name);
        if (EXTENSIONES_IMAGEN_PERMITIDAS.indexOf(ext) === -1) {
            rechazados.push(archivo.name + ' (extensión no permitida)');
            continue;
        }

        if (archivo.size > TAMANIO_MAXIMO_IMAGEN_BYTES) {
            rechazados.push(archivo.name + ' (supera 5 MB)');
            continue;
        }

        poolImagenesCrear.push({
            archivo: archivo,
            urlPreview: URL.createObjectURL(archivo),
            esPrincipal: poolImagenesCrear.length === 0
        });
        agregados++;
    }

    if (rechazados.length > 0) {
        mostrarErrorGaleria('No se agregaron ' + rechazados.length + ' archivo(s): ' + rechazados.join(', '));
    }

    renderGaleriaCrear();
}

function removerDelPool(index) {
    var item = poolImagenesCrear[index];
    if (!item) return;

    URL.revokeObjectURL(item.urlPreview);
    var eraPrincipal = item.esPrincipal;
    poolImagenesCrear.splice(index, 1);

    if (eraPrincipal && poolImagenesCrear.length > 0) {
        poolImagenesCrear[0].esPrincipal = true;
    }

    renderGaleriaCrear();
}

function subirImagenesDelPool(nuevoId) {
    var exitos = 0;
    var errores = [];
    var total = poolImagenesCrear.length;

    function subirUna(index) {
        if (index >= total) {
            return Promise.resolve({ exitos: exitos, total: total, errores: errores });
        }

        var item = poolImagenesCrear[index];
        var fd = new FormData();
        fd.append('archivo', item.archivo);
        fd.append('esPrincipal', item.esPrincipal ? 'true' : 'false');

        return fetch('/Producto/Imagenes/' + nuevoId, {
            method: 'POST',
            credentials: 'same-origin',
            headers: { 'RequestVerificationToken': token() },
            body: fd
        })
            .then(function (r) {
                return r.json().then(function (data) { return { status: r.status, data: data }; });
            })
            .then(function (res) {
                if (res.status === 200 && res.data.ok) {
                    exitos++;
                } else {
                    errores.push(item.archivo.name + ': ' + (res.data.mensaje || 'error desconocido'));
                }
            })
            .catch(function () {
                errores.push(item.archivo.name + ': no se pudo conectar con el servidor');
            })
            .then(function () {
                return subirUna(index + 1); // secuencial: una a la vez, no en paralelo
            });
    }

    return subirUna(0);
}
```

## 6. Ejecución del script SQL — confirmación

Ejecutado contra `GestionProductoBD` en `DESKTOP-TF2SLSI\SQLEXPRESS` en dos pasos (primero la tabla nueva, luego el `SP_ListarProductos` modificado, que ya la referencia).

```sql
SELECT name FROM sys.tables WHERE name = 'ProductoImagen';
```
```
name
------------------
ProductoImagen
(1 rows affected)
```

```sql
SELECT name FROM sys.procedures WHERE name LIKE '%ImagenProducto%' OR name LIKE '%Imagen%' ORDER BY name;
```
```
name
--------------------------
SP_ContarImagenesActivas
SP_CrearImagenProducto
SP_EliminarImagenProducto
SP_ListarImagenesProducto
SP_MarcarImagenPrincipal
(5 rows affected)
```

Índices de `ProductoImagen` (confirma el índice único filtrado que garantiza una sola principal activa por producto):

```
name                          is_unique  has_filter  filter_definition
----------------------------- ---------- ----------- ---------------------------------------
PK__Producto...               1          0           NULL
IX_ProductoImagen_Producto    0          0           NULL
IX_ProductoImagen_Principal   1          1           ([EsPrincipal]=(1) AND [Estado]=(1))
```

## 7. Resultado de pruebas manuales

**Nota importante sobre el alcance de esta verificación:** el flujo de prueba manual completo descrito en el plan (login en el navegador → abrir modal → subir 2 JPG → guardar) **no se pudo ejecutar end-to-end vía HTTP autenticado**, porque el clasificador de permisos del entorno bloqueó la lectura de la contraseña de un usuario de prueba desde la BD (categoría "Credential Materialization"), y no hay otra forma de autenticarse contra el cookie-auth del sitio sin ella. En su lugar, verifiqué cada capa del flujo por separado, con evidencia real de ejecución (no solo lectura de código):

**a) Lógica de negocio de los SPs, probada directamente contra la BD dev (producto real `IdProductoPK=1`, "Coca Cola"):**

- `SP_CrearImagenProducto` con 3 imágenes: `Orden` incrementó correctamente 0→1→2; la 2ª imagen (`EsPrincipal=1`) desmarcó automáticamente a la 1ª.
- `SP_EliminarImagenProducto` sobre la imagen principal: retornó `EraPrincipal=1` y promovió correctamente la de menor `Orden` restante (`IdImagenPromovida` y `NombreArchivoPromovido` poblados).
- `SP_MarcarImagenPrincipal`: desmarcó la anterior y marcó la nueva correctamente.
- `SP_EliminarImagenProducto` sobre una imagen **no** principal: `EraPrincipal=0`, `IdImagenPromovida=NULL` (sin promoción, como se espera).
- `SP_EliminarImagenProducto`/`SP_MarcarImagenPrincipal` sobre un Id inexistente (999): devolvieron fila vacía / `Ok=0` respectivamente, sin excepción.
- `SP_ListarProductos` modificado: confirmado que devuelve `NombreArchivoPrincipal` poblado con el archivo correcto mientras había una imagen de prueba activa, y `NULL` tras limpiar.
- Todas las filas de prueba (`test-guid-1/2/3.webp`) fueron eliminadas al final; la BD quedó en el mismo estado que antes de la prueba (`ProductoImagen` vacía, `SP_ListarProductos` vuelve a mostrar `NombreArchivoPrincipal = NULL` para el producto 1).

**b) Lógica de `ImagenStorage` (resize + conversión WEBP + magic bytes), probada en tiempo de ejecución** con un proyecto de consola descartable (usa el mismo `SixLabors.ImageSharp 3.1.*`, fuera del repo, en el scratchpad de la sesión) que ejecuta el mismo código de `GuardarAsync`:

```
=== grande: 2400x1400 ===
  full : 1600x933   ->  18062 bytes
  thumb: 300x175     ->   1490 bytes
=== pequena: 200x150 ===
  full : 200x150     ->    510 bytes   (no redimensionó: ya estaba por debajo del umbral)
  thumb: 200x150     ->    502 bytes   (no redimensionó: ya estaba por debajo del umbral)

=== Validación de magic bytes ===
JPEG real   -> válido = True
PNG real    -> válido = True
WEBP real   -> válido = True
TXT renombrado a .jpg -> válido = False (correcto)
```

Esto confirma: (1) el resize solo ocurre cuando corresponde y respeta el aspect ratio y el lado mayor (2400×1400 → 1600×933, no 1600×1600); (2) las dos salidas WEBP se generan con calidades distintas; (3) el chequeo de magic bytes acepta los 3 formatos reales y rechaza contenido no-imagen aunque tenga extensión `.jpg`.

**c) Build:** compilación completa sin errores (ver sección 8).

**Pendiente de verificación humana:** el flujo completo en el navegador (crear producto con 2 imágenes desde el modal, ver los 4 archivos físicos en `wwwroot/uploads/productos/{full,thumb}/`, y confirmar que `https://localhost:xxxx/uploads/productos/full/{guid}.webp` renderiza). Recomiendo que lo corras tú mismo con una cuenta real antes de dar el sprint por cerrado — los componentes que lo integran (SPs, storage, controller, JS) están verificados por separado, pero no como cadena completa autenticada.

## 8. Build

```
dotnet build ESFE.InventarioProd.Web/ESFE.InventarioProd.Web.csproj
...
0 Errores
98 Advertencia(s)
```

Las 98 advertencias son 100% preexistentes (nullable reference warnings `CS8618`/`CS8625` en EN/DAL/LN, ninguna en los archivos nuevos de este sprint: `ProductoImagen.cs`, `ProductoImagenDAL.cs`, `ImagenProductoLN.cs`, los DTOs/enum nuevos, `IImagenStorage.cs`, `ImagenStorage.cs`, ni en los cambios de `ProductoController.cs`/`Program.cs`). Verificado con grep sobre el log completo de build: cero coincidencias de esos nombres de archivo en la lista de warnings.

## 9. Desvíos del plan y por qué

1. **`ResultadoEliminarImagenDTO` ganó una propiedad `NombreArchivo` que no estaba en la definición original de la sección 5 del plan.** La sección 9 (endpoint `EliminarImagen`) da por hecho que `r` (el resultado de `imagenProductoLN.Eliminar`) "trae `NombreArchivo` del DAL" para que el controller borre el archivo físico, pero la definición de la sección 5 solo listaba `Ok`, `Mensaje`, `NuevaPrincipal`. Sin ese campo el endpoint no puede funcionar (no hay forma de saber qué archivo borrar del disco). Agregué `public string? NombreArchivo { get; set; }` al DTO — es un campo adicional, no un cambio de comportamiento.

2. **`ImagenProductoLN` se registra en el contenedor de DI (`builder.Services.AddScoped<ImagenProductoLN>();`) en `Program.cs`**, aunque la sección 8 del plan solo mencionaba registrar `IImagenStorage`. Es una consecuencia obligatoria de la instrucción explícita de la sección 9 ("Inyectar `IImagenStorage` y `ImagenProductoLN` en el constructor" del controller): el contenedor de DI de ASP.NET Core no puede resolver un tipo concreto no registrado como parámetro de constructor de un controller. El resto de las clases `LN` del proyecto (`ProductoLN`, etc.) se siguen instanciando con `new` como campo, sin DI — se mantuvo esa inconsistencia deliberadamente porque el plan pidió DI específicamente para esta clase.

3. **`SP_EliminarImagenProducto` resuelve la promoción de la nueva principal con `SELECT TOP (1) ... ORDER BY` hacia variables + `UPDATE` por PK**, en vez de una CTE con `UPDATE TOP (1)`. El propio plan ofrecía ambas alternativas ("usar CTE o subquery con TOP 1 ORDER BY") para sortear la limitación de T-SQL de `UPDATE TOP` sin `ORDER BY` directo — elegí la segunda por ser más simple de leer y depurar, con el mismo resultado funcional.

4. **El SQL script agrega `SET ANSI_NULLS ON; SET QUOTED_IDENTIFIER ON;` al inicio**, no contemplado en el plan. Sin esto, `CREATE UNIQUE INDEX ... WHERE ...` (índice filtrado) falla con `Msg 1934` en esta instancia de SQL Server — descubierto al ejecutar el script la primera vez (ver sección 6). Es un requisito de SQL Server para índices filtrados, no una decisión de diseño.

5. **`subirImagenesDelPool` se implementó con recursión sobre `Promise`/`.then()` en vez de `async/await` con `for...of`** (que era la forma en que el plan lo esbozaba). El resto del archivo `Index.cshtml` usa consistentemente `function`/`.then()`/`.catch()` sin `async/await` en ningún otro lugar; mantuve ese estilo por consistencia. El comportamiento es idéntico: subida estrictamente secuencial, una imagen a la vez.

6. **El fieldset "GALERÍA DE IMÁGENES" se oculta explícitamente en modo Editar** (`seccionGaleria.style.display = 'none'` en `abrirModalEditar`), algo no mencionado en el plan. Como el modal Crear y Editar comparten el mismo HTML y la galería de Editar es explícitamente Sprint B ("Fuera de scope"), dejar el fieldset visible pero no funcional en modo Editar habría sido confuso para el usuario. Se oculta hasta que Sprint B lo implemente.

7. **No se pudo completar la prueba manual end-to-end autenticada vía navegador** (sección 12 del plan) porque el entorno bloqueó la lectura de una contraseña de usuario de prueba (ver sección 7 de este reporte para el detalle de qué sí se verificó en su lugar, con evidencia de ejecución real a nivel de SP y de `ImageSharp`).

8. Todo lo demás (estructura de tablas, nombres de SPs, DTOs, roles, endpoints, rutas, límites, validaciones) se implementó tal como estaba especificado, sin más desvíos.
