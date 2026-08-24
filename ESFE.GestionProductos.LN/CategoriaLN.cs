using System;
using System.Collections.Generic;
using System.Data;
using ESFE.GestionProductos.DAL;
using ESFE.GestionProductos.EN;

namespace ESFE.GestionProductos.LN
{
    public class CategoriaLN
    {
        private readonly CategoriaDAL categoriaDAL = new CategoriaDAL();

        public DataTable Listar()
        {
            return categoriaDAL.Listar();
        }

        public List<Categoria> Buscar(
            string nombre = null,
            string descripcion = null,
            bool? estado = null)
        {
            return categoriaDAL.Buscar(nombre, descripcion, estado);
        }

        // Especial para combos: solo activas
        public List<Categoria> ObtenerActivas()
        {
            return categoriaDAL.Buscar(null, null, true);
        }

        public int Guardar(Categoria categoria)
        {
            if (categoria == null)
                throw new ArgumentNullException(nameof(categoria));

            if (categoria.IdCategoriaPK > 0)
                return categoriaDAL.Actualizar(categoria);

            return categoriaDAL.Insertar(categoria);
        }

        public int Insertar(Categoria categoria)
        {
            if (categoria == null)
                throw new ArgumentNullException(nameof(categoria));
            return categoriaDAL.Insertar(categoria);
        }

        public int Actualizar(Categoria categoria)
        {
            if (categoria == null)
                throw new ArgumentNullException(nameof(categoria));
            return categoriaDAL.Actualizar(categoria);
        }

        public int EliminarLogico(short idCategoria)
        {
            return categoriaDAL.EliminarLogico(idCategoria);
        }
    }
}