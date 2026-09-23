# Implementación: Galería de imágenes de Producto — Sprint B (Modal Editar + Thumbnail en Listado)

Ejecutado según el plan del Sprint B. Galería en tiempo real en el modal Editar, columna de thumbnail en el listado, y verificación/arreglo de la propagación de `NombreArchivoPrincipal` desde el SP hasta el JSON del listado.

## 1. Archivos creados y modificados

### Modificados

- `ESFE.GestionProductos.DAL/ProductoDAL.cs` — el tuple de retorno de `ListarProductos` no traía `NombreArchivoPrincipal`; se agregó como último elemento y se lee del reader por ordinal.
- `ESFE.GestionProductos.LN/DTOs/ProductoListadoDTO.cs` — agregada `public string? NombreArchivoPrincipal { get; set; }`.
- `ESFE.GestionProductos.LN/ProductoLN.cs` — `ListarProductos` ahora copia `p.NombreArchivoPrincipal` al DTO.
- `ESFE.InventarioProd.Web/Views/Producto/Index.cshtml` — columna thumbnail en `<thead>`/`renderTabla`, galería en tiempo real del modal Editar (`renderGaleriaEditar`, `subirImagenesEditar`, `eliminarImagenEditar`, `marcarPrincipalEditar`), bifurcación por `modoModal` en los listeners compartidos de `#grid-imagenes` e `#prod-input-imagenes`, flag `galeriaModificada` + `cancelarModal()` para refrescar el listado al cancelar si hubo cambios de galería.
- `ESFE.InventarioProd.Web/wwwroot/css/producto.css` — estilos `.pr-th-imagen`, `.pr-td-imagen`, `.pr-thumb-listado`, `.pr-thumb-placeholder`, `.slot-imagen-placeholder`.

No se tocó `ProductoController.cs` (el endpoint `Listar` ya serializa el DTO completo, sin cambios necesarios), ni `ImagenProductoLN`/`ProductoImagenDAL`/`ImagenStorage`/SPs de `ProductoImagen` — todo fuera de alcance según el plan.

## 2. Verificación de la propagación de `NombreArchivoPrincipal`

**No llegaba al JSON.** El SP (`SP_ListarProductos`, modificado en Sprint A) ya devolvía la columna correctamente, pero la cadena DAL → DTO → LN la ignoraba por completo:

- `ProductoDAL.ListarProductos` leía el reader con una lista fija de columnas por ordinal (`ordId`, `ordCodigo`, ... `ordEstado`) y el tuple de retorno no tenía un slot para `NombreArchivoPrincipal` — la columna nueva del SP nunca se leía.
- `ProductoListadoDTO` no tenía la propiedad.
- `ProductoLN.ListarProductos` no la copiaba al DTO (no podía, no existía en el tuple ni en el DTO).

Arreglado agregando el campo en los tres puntos (tuple del DAL, propiedad del DTO, mapeo en LN — sección 1). `ProductoController.Listar` no requirió cambios: usa `Json(productoLN.ListarProductos(filtros))`, que serializa cualquier propiedad pública del DTO automáticamente.

Confirmado por inspección de código que el nombre de columna en el SP (`NombreArchivoPrincipal`) coincide exactamente con el string pasado a `GetOrdinal` en el DAL.

## 3. `renderGaleriaEditar()`

```js
function renderGaleriaEditar() {
    contadorImagenes.textContent = imagenesEditarActuales.length + ' / ' + MAX_IMAGENES_PRODUCTO + ' imágenes';
    btnAgregarImagen.disabled = imagenesEditarActuales.length >= MAX_IMAGENES_PRODUCTO;

    if (imagenesEditarActuales.length === 0) {
        gridImagenes.innerHTML = '';
        return;
    }

    var html = '';
    imagenesEditarActuales.forEach(function (img) {
        html += '<div class="slot-imagen' + (img.esPrincipal ? ' slot-principal' : '') + '">' +
            '<button type="button" class="slot-imagen-quitar" data-id="' + img.id + '" title="Quitar" aria-label="Quitar">&times;</button>' +
            (img.esPrincipal ? '<span class="slot-imagen-badge">Principal</span>' : '') +
            '<img src="' + img.urlThumb + '" alt="" ' +
            'onerror="this.outerHTML=\'<span class=&quot;slot-imagen-placeholder&quot;></span>\'" />' +
            '<label class="slot-imagen-radio">' +
            '<input type="radio" name="prod-imagen-principal" data-id="' + img.id + '" ' +
            (img.esPrincipal ? 'checked disabled' : '') + ' /> Principal' +
            '</label>' +
            '</div>';
    });
    gridImagenes.innerHTML = html;
}
```

## 4. `subirImagenesEditar()`

```js
function subirImagenesEditar(archivos) {
    ocultarErrorGaleria();

    var errores = [];
    var subidas = 0;
    var limiteAlcanzado = false;

    function subirUna(index) {
        if (index >= archivos.length) return Promise.resolve();

        var archivo = archivos[index];
        if (limiteAlcanzado) return subirUna(index + 1);

        var ext = extensionArchivo(archivo.name);
        if (EXTENSIONES_IMAGEN_PERMITIDAS.indexOf(ext) === -1) {
            errores.push(archivo.name + ': extensión no permitida');
            return subirUna(index + 1);
        }
        if (archivo.size > TAMANIO_MAXIMO_IMAGEN_BYTES) {
            errores.push(archivo.name + ': supera 5 MB');
            return subirUna(index + 1);
        }
        if (imagenesEditarActuales.length >= MAX_IMAGENES_PRODUCTO) {
            errores.push('Se alcanzó el límite de ' + MAX_IMAGENES_PRODUCTO + ' imágenes');
            limiteAlcanzado = true;
            return subirUna(index + 1);
        }

        var fd = new FormData();
        fd.append('archivo', archivo);
        fd.append('esPrincipal', 'false');

        return fetch('/Producto/Imagenes/' + prodIdActual, {
            method: 'POST',
            credentials: 'same-origin',
            headers: { 'RequestVerificationToken': token() },
            body: fd
        })
            .then(function (r) { return r.json().then(function (data) { return { status: r.status, data: data }; }); })
            .then(function (res) {
                if (res.status === 200 && res.data.ok) {
                    imagenesEditarActuales.push(res.data.imagen);
                    subidas++;
                    galeriaModificada = true;
                    renderGaleriaEditar();
                } else if (res.status === 409) {
                    errores.push(res.data.mensaje || 'Límite de imágenes alcanzado');
                    limiteAlcanzado = true;
                } else {
                    errores.push(archivo.name + ': ' + (res.data.mensaje || 'error desconocido'));
                }
            })
            .catch(function () { errores.push(archivo.name + ': no se pudo conectar con el servidor'); })
            .then(function () { return subirUna(index + 1); });
    }

    return subirUna(0).then(function () {
        if (errores.length > 0) mostrarErrorGaleria(errores.join(' · '));
        if (subidas > 0 && errores.length > 0) {
            mostrarToast(subidas + ' imagen(es) subida(s), ' + errores.length + ' error(es)', 'info');
        }
    });
}
```

## 5. Celda de thumbnail en `renderTabla()`

```js
var celdaImagen;
if (p.nombreArchivoPrincipal) {
    var urlThumb = '/uploads/productos/thumb/' +
        p.nombreArchivoPrincipal.replace(/\.webp$/, '_thumb.webp');
    celdaImagen = '<td class="pr-td-imagen">' +
        '<img src="' + urlThumb + '" alt="" class="pr-thumb-listado" ' +
        'onerror="this.outerHTML=\'<span class=&quot;pr-thumb-placeholder&quot;></span>\'" />' +
        '</td>';
} else {
    celdaImagen = '<td class="pr-td-imagen"><span class="pr-thumb-placeholder"></span></td>';
}

html += '<tr>' + celdaImagen + '<td>' + escapeHtml(p.codigo) + '</td>' + /* ... resto de columnas ... */
```

## 6. Resultado de los escenarios de verificación

No fue posible ejecutar los 3 escenarios en navegador con sesión autenticada real dentro de este entorno (misma limitación de credenciales documentada en el reporte de Sprint A: no hay forma de materializar la contraseña de un usuario de prueba para el cookie-auth del sitio). Lo que sí se verificó:

1. **Listado con thumbnail**: verificado por inspección — `renderTabla` genera `<img>` con `onerror` de fallback para productos con `nombreArchivoPrincipal`, y `<span class="pr-thumb-placeholder">` para los que no tienen. La transformación `{guid}.webp` → `{guid}_thumb.webp` coincide con el nombre físico que genera `ImagenStorage.GuardarAsync` (confirmado contra el código de Sprint A, sección 3 de ese reporte). **Pendiente de verificación visual humana en navegador.**

2. **Modal Editar — flujo galería**: verificado por inspección de código y por el shape de los DTOs/endpoints (`ImagenProductoDTO`, `ImagenProductoLN.Eliminar/MarcarPrincipal/RegistrarImagen`) contra lo que consume el JS nuevo — coinciden exactamente los nombres de campo (`id`, `url`, `urlThumb`, `esPrincipal`, `orden`, `nuevaPrincipal`). La promoción automática de principal al eliminar se refleja correctamente en el estado cliente porque `eliminarImagenEditar` consume `res.data.nuevaPrincipal` tal como lo entrega el controller. **Pendiente de verificación end-to-end en navegador** (subir imágenes, marcar principal, eliminar principal y confirmar la promoción, en BD real).

3. **Modal Crear sigue funcionando**: no se tocó ninguna función del flujo Crear (`agregarAlPool`, `removerDelPool`, `marcarPrincipalEnPool`, `limpiarPoolImagenesCrear`, `subirImagenesDelPool`); los únicos cambios que rozan ese camino son la inicialización de `modoModal = 'crear'` en `abrirModalCrear` y la bifurcación por `modoModal` en los listeners compartidos de `#grid-imagenes`/`#prod-input-imagenes`, que para `modoModal === 'crear'` ejecutan exactamente las mismas funciones que antes. **Pendiente de verificación en navegador.**

**Recomendación**: correr los 3 escenarios manualmente con una cuenta real antes de cerrar el sprint, igual que se recomendó en Sprint A.

## 7. Build

```
dotnet build ESFE.InventarioProd.Web/ESFE.InventarioProd.Web.csproj
...
0 Errores
98 Advertencia(s)
```

0 advertencias nuevas: las 98 son las mismas preexistentes de siempre (`CS8618`/`CS8625`/`CS8600` en EN/DAL/LN, en líneas ajenas a las tocadas por este sprint). Confirmado con grep dirigido a los archivos modificados (`ProductoDAL.cs`, `ProductoListadoDTO.cs`, `ProductoLN.cs`): las únicas advertencias que aparecen en esos archivos están en líneas muy anteriores a las que se tocaron (ej. `ProductoLN.cs(25,25)` y `(27,25)` vs. la edición en `ListarProductos` alrededor de la línea 113), es decir, ya existían antes de este sprint.

## 8. Desvíos del plan y por qué

1. **Se agregó `cancelarModal()` como wrapper de `cerrarModal()`**, no mencionado explícitamente como nombre de función en el plan (que solo describía el comportamiento: "trackear con un flag `galeriaModificada` ... y consumir al cerrar"). Se separó en dos funciones porque `cerrarModal()` ya se invoca también desde el camino de **guardado exitoso** (`guardarProducto`, `subirImagenesDelPool`, `eliminarDefinitivo`), que siempre refresca el listado por su cuenta inmediatamente después — si el refresco condicional por `galeriaModificada` viviera dentro de `cerrarModal()`, se dispararía un `cargarPagina` duplicado en el camino de guardado. `cancelarModal()` se conecta a los tres puntos de salida por cancelación: botón "Cancelar", click fuera del modal, y tecla Escape.

2. **`prodIdActual` se setea inmediatamente al abrir el modal Editar** (`prodIdActual = id;` al inicio de `abrirModalEditar`), no solo dentro del `.then()` de `ObtenerParaEdicion` como en Sprint A. Es necesario porque `subirImagenesEditar`/`eliminarImagenEditar`/`marcarPrincipalEditar` pueden dispararse por una acción del usuario sobre la galería antes de que la respuesta de `ObtenerParaEdicion` haya llegado (las dos requests van en paralelo, tal como pide la sección "Decisiones tomadas" punto 2). Sin este cambio, `prodIdActual` sería `null` momentáneamente y `subirImagenesEditar` fallaría el POST hacia `/Producto/Imagenes/null`.

3. **Nueva clase CSS `.slot-imagen-placeholder`**, no listada en el plan (que solo daba el CSS de la celda del listado, sección 2b). Necesaria porque el punto 3d del plan pide el mismo patrón de `onerror` para los thumbnails de la galería del modal Editar, y ese patrón reemplaza el `<img>` por un `<span>` — sin una clase que replique el `position:absolute;inset:0` del `.slot-imagen img` original, el placeholder no ocuparía el espacio del slot correctamente.

4. Todo lo demás (shapes de API, endpoints, decisiones de flujo, límite de 5 imágenes, confirmación client-side antes de eliminar, no revocar object URLs en modo Editar, resetear estado al cerrar) se implementó tal como estaba especificado, sin más desvíos.
