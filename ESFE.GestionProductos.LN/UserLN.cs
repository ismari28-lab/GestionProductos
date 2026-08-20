using ESFE.GestionProductos.DAL;
using ESFE.GestionProductos.EN;
using System;
using System.Collections.Generic;
using System.Data;

namespace ESFE.GestionProductos.LN
{
    public class UserLN
    {
        private readonly UserDAL userDAL = new UserDAL();

        // 1. Listar Usuarios
        public DataTable Listar()
        {
            return userDAL.Listar();
        }

        // 2. Buscar Usuarios
        public List<Usuario> Buscar(string nombre = null, short? idUsuario = null)
        {
            return userDAL.Buscar(nombre, idUsuario);
        }

        // 3. Insertar Usuario
        public int Insertar(Usuario usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException(nameof(usuario));

            if (string.IsNullOrWhiteSpace(usuario.Nombre))
                throw new ArgumentException("El nombre del usuario es obligatorio.");

            return userDAL.Insertar(usuario);
        }

        // 4. Actualizar Usuario
        public int Actualizar(Usuario usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException(nameof(usuario));

            if (usuario.IdUsuarioPK <= 0)
                throw new ArgumentException("El ID del usuario no es válido.");

            if (string.IsNullOrWhiteSpace(usuario.Nombre))
                throw new ArgumentException("El nombre del usuario es obligatorio.");

            return userDAL.Actualizar(usuario);
        }

        // 5. Eliminación lógica
        public int EliminarLogico(short idUsuario)
        {
            if (idUsuario <= 0)
                throw new ArgumentException("El ID del usuario no es válido.");

            return userDAL.EliminarLogico(idUsuario);
        }
    }
}
