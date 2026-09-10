using ESFE.GestionProductos.DAL;
using ESFE.GestionProductos.EN;
using ESFE.GestionProductos.LN.DTOs;
using ESFE.GestionProductos.LN.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ESFE.GestionProductos.LN
{
    public class UserLN
    {
        private readonly UserDAL userDAL = new UserDAL();

        // 1. Listar Usuarios (todos, para grillas)
        public DataTable Listar()
        {
            return userDAL.Listar();
        }

        // 2. Buscar Usuarios (todos los estados, para filtrado en grilla)
        public List<Usuario> Buscar(string nombre = null, short? idUsuario = null)
        {
            return userDAL.Buscar(nombre, idUsuario);
        }

        // 3. Obtener solo activos (exclusivo para combos)
        public List<Usuario> ObtenerActivos()
        {
            return userDAL.Buscar(null, null)
                .Where(u => u.Estado == true)
                .ToList();
        }

        // 4. Guardar (inserta o actualiza según corresponda)
        public int Guardar(Usuario usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException(nameof(usuario));

            if (string.IsNullOrWhiteSpace(usuario.Nombre))
                throw new ArgumentException("El nombre del usuario es obligatorio.");

            if (usuario.IdUsuarioPK > 0)
            {
                // Si al editar se deja la contraseña en blanco, se conserva la actual
                if (string.IsNullOrWhiteSpace(usuario.Password))
                    usuario.Password = userDAL.ObtenerPasswordActual(usuario.IdUsuarioPK);

                return userDAL.Actualizar(usuario);
            }

            return userDAL.Insertar(usuario);
        }

        // 6. Insertar Usuario
        public int Insertar(Usuario usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException(nameof(usuario));

            if (string.IsNullOrWhiteSpace(usuario.Nombre))
                throw new ArgumentException("El nombre del usuario es obligatorio.");

            return userDAL.Insertar(usuario);
        }

        // 7. Actualizar Usuario
        public int Actualizar(Usuario usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException(nameof(usuario));

            if (usuario.IdUsuarioPK <= 0)
                throw new ArgumentException("El ID del usuario no es válido.");

            if (string.IsNullOrWhiteSpace(usuario.Nombre))
                throw new ArgumentException("El nombre del usuario es obligatorio.");

            // Si al editar se deja la contraseña en blanco, se conserva la actual
            if (string.IsNullOrWhiteSpace(usuario.Password))
                usuario.Password = userDAL.ObtenerPasswordActual(usuario.IdUsuarioPK);

            return userDAL.Actualizar(usuario);
        }

        // 8. Eliminación lógica
        public int EliminarLogico(short idUsuario)
        {
            if (idUsuario <= 0)
                throw new ArgumentException("El ID del usuario no es válido.");

            return userDAL.EliminarLogico(idUsuario);
        }

        // --- Módulo Usuarios (STOCKEO): listado, modal Crear/Editar/Eliminar ---

        // Listado para la web: nunca incluye la contraseña
        public List<UsuarioListadoDTO> ListarParaWeb()
        {
            return userDAL.Buscar(null, null)
                .Select(u => new UsuarioListadoDTO
                {
                    IdUsuarioPK = u.IdUsuarioPK,
                    Nombre = u.Nombre ?? string.Empty,
                    IdRolFK = u.Id_RolFK,
                    NombreRol = string.IsNullOrEmpty(u.NombreRol) ? "(sin rol)" : u.NombreRol,
                    Estado = u.Estado ?? false
                })
                .OrderBy(u => u.Nombre)
                .ToList();
        }

        // Datos para hidratar el modal Editar: nunca incluye la contraseña
        public UsuarioEdicionDTO? ObtenerParaEdicion(int id)
        {
            if (id <= 0)
                return null;

            // TODO: alinear a int cuando se resuelva deuda técnica de FK short vs int
            var usuario = userDAL.Buscar(null, (short)id).FirstOrDefault();
            if (usuario == null)
                return null;

            return new UsuarioEdicionDTO
            {
                IdUsuarioPK = usuario.IdUsuarioPK,
                Nombre = usuario.Nombre ?? string.Empty,
                IdRolFK = usuario.Id_RolFK,
                Estado = usuario.Estado ?? true
            };
        }

        public ResultadoGuardarUsuarioDTO Crear(UsuarioFormDTO form)
        {
            var error = ValidarFormulario(form, esCreacion: true);
            if (error != null)
                return error;

            if (NombreDuplicado(form.Nombre, 0))
                return new ResultadoGuardarUsuarioDTO
                {
                    Resultado = ResultadoGuardarUsuario.NombreDuplicado,
                    Mensaje = "Ya existe un usuario con ese nombre."
                };

            try
            {
                var usuario = new Usuario
                {
                    Nombre = form.Nombre.Trim(),
                    Password = form.Password!.Trim(),
                    Id_RolFK = form.IdRolFK,
                    Estado = true
                };

                userDAL.Insertar(usuario);

                return new ResultadoGuardarUsuarioDTO { Resultado = ResultadoGuardarUsuario.Ok };
            }
            catch (Exception ex)
            {
                return new ResultadoGuardarUsuarioDTO { Resultado = ResultadoGuardarUsuario.ErrorInterno, Mensaje = ex.Message };
            }
        }

        public ResultadoGuardarUsuarioDTO Actualizar(UsuarioFormDTO form)
        {
            if (!form.IdUsuarioPK.HasValue || form.IdUsuarioPK.Value <= 0)
                return new ResultadoGuardarUsuarioDTO
                {
                    Resultado = ResultadoGuardarUsuario.DatosInvalidos,
                    Mensaje = "ID de usuario inválido."
                };

            var error = ValidarFormulario(form, esCreacion: false);
            if (error != null)
                return error;

            if (NombreDuplicado(form.Nombre, form.IdUsuarioPK.Value))
                return new ResultadoGuardarUsuarioDTO
                {
                    Resultado = ResultadoGuardarUsuario.NombreDuplicado,
                    Mensaje = "Ya existe otro usuario con ese nombre."
                };

            try
            {
                var usuario = new Usuario
                {
                    IdUsuarioPK = form.IdUsuarioPK.Value,
                    Nombre = form.Nombre.Trim(),
                    // En blanco = conservar la contraseña actual (lógica ya existente en Actualizar())
                    Password = string.IsNullOrWhiteSpace(form.Password) ? null : form.Password.Trim(),
                    Id_RolFK = form.IdRolFK,
                    Estado = form.Estado
                };

                int filas = Actualizar(usuario);
                if (filas == 0)
                    return new ResultadoGuardarUsuarioDTO
                    {
                        Resultado = ResultadoGuardarUsuario.NoEncontrado,
                        Mensaje = "Usuario no encontrado."
                    };

                return new ResultadoGuardarUsuarioDTO { Resultado = ResultadoGuardarUsuario.Ok };
            }
            catch (Exception ex)
            {
                return new ResultadoGuardarUsuarioDTO { Resultado = ResultadoGuardarUsuario.ErrorInterno, Mensaje = ex.Message };
            }
        }

        // idUsuarioActual: usuario con sesión iniciada que realiza la acción (no puede eliminarse a sí mismo)
        public ResultadoEliminarUsuarioDTO EliminarConValidacion(int id, int idUsuarioActual)
        {
            if (id <= 0)
                return new ResultadoEliminarUsuarioDTO { Resultado = ResultadoEliminarUsuario.NoEncontrado, Mensaje = "Usuario no encontrado." };

            if (id == idUsuarioActual)
                return new ResultadoEliminarUsuarioDTO
                {
                    Resultado = ResultadoEliminarUsuario.NoPermitido,
                    Mensaje = "No puedes eliminar tu propio usuario mientras tienes la sesión iniciada."
                };

            try
            {
                // TODO: alinear a int cuando se resuelva deuda técnica de FK short vs int
                EliminarLogico((short)id);
                return new ResultadoEliminarUsuarioDTO { Resultado = ResultadoEliminarUsuario.Ok };
            }
            catch (Exception ex)
            {
                return new ResultadoEliminarUsuarioDTO { Resultado = ResultadoEliminarUsuario.ErrorInterno, Mensaje = ex.Message };
            }
        }

        private bool NombreDuplicado(string nombre, int idExcluir)
        {
            return userDAL.Buscar(null, null)
                .Any(u => u.IdUsuarioPK != idExcluir
                    && string.Equals((u.Nombre ?? string.Empty).Trim(), nombre.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        private static ResultadoGuardarUsuarioDTO? ValidarFormulario(UsuarioFormDTO form, bool esCreacion)
        {
            if (form == null)
                return Invalido("Datos del usuario faltantes.");

            if (string.IsNullOrWhiteSpace(form.Nombre))
                return Invalido("El nombre es obligatorio.");

            if (form.Nombre.Trim().Length > 100)
                return Invalido("El nombre no puede superar los 100 caracteres.");

            if (!form.IdRolFK.HasValue || form.IdRolFK.Value <= 0)
                return Invalido("El rol es obligatorio.");

            if (esCreacion && string.IsNullOrWhiteSpace(form.Password))
                return Invalido("La contraseña es obligatoria.");

            if (!string.IsNullOrEmpty(form.Password) && form.Password.Length > 256)
                return Invalido("La contraseña no puede superar los 256 caracteres.");

            return null;
        }

        private static ResultadoGuardarUsuarioDTO Invalido(string mensaje)
        {
            return new ResultadoGuardarUsuarioDTO { Resultado = ResultadoGuardarUsuario.DatosInvalidos, Mensaje = mensaje };
        }

        // --- Autoservicio (Mi perfil): el propio usuario cambia su contraseña ---

        public bool CambiarPassword(int idUsuario, string nuevaPassword)
        {
            if (idUsuario <= 0)
                throw new ArgumentException("Usuario inválido.");

            if (string.IsNullOrWhiteSpace(nuevaPassword))
                throw new ArgumentException("La nueva contraseña es obligatoria.");

            if (nuevaPassword.Length > 256)
                throw new ArgumentException("La contraseña no puede superar los 256 caracteres.");

            // TODO: alinear a int cuando se resuelva deuda técnica de FK short vs int
            var usuario = userDAL.Buscar(null, (short)idUsuario).FirstOrDefault();
            if (usuario == null)
                return false;

            usuario.Password = nuevaPassword.Trim();
            return userDAL.Actualizar(usuario) > 0;
        }
    }
}