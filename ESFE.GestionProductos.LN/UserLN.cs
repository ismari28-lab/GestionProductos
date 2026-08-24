using ESFE.GestionProductos.DAL;
using ESFE.GestionProductos.EN;
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
    }
}