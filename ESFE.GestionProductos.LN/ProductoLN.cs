using System;
using System.Collections.Generic;
using System.Data;
using ESFE.GestionProductos.DAL;
using ESFE.GestionProductos.EN;

namespace ESFE.GestionProductos.LN
{
    public class ProductoLN
    {
        private readonly ProductoDAL productoDAL = new ProductoDAL();

        // Listar Productos
        public DataTable Listar()
        {
            return productoDAL.Listar();
        }

        // Buscar Productos
        public List<Producto> Buscar(string nombre = null, short? idProducto = null)
        {
            return productoDAL.Buscar(nombre, idProducto);
        }

        // Guardar Producto
        public int Guardar(Producto producto)
        {
            if (producto == null)
                throw new ArgumentNullException(nameof(producto));

            if (producto.IdProductoPK > 0)
            {
                return productoDAL.Actualizar(producto);
            }

            return productoDAL.Insertar(producto);
        }

        // Actualizar Producto
        public int Actualizar(Producto producto)
        {
            if (producto == null)
                throw new ArgumentNullException(nameof(producto));

            return productoDAL.Actualizar(producto);
        }

        // Insertar Producto
        public int Insertar(Producto producto)
        {
            if (producto == null)
                throw new ArgumentNullException(nameof(producto));

            return productoDAL.Insertar(producto);
        }

        // Eliminar lógico
        public int EliminarLogico(short idProducto)
        {
            return productoDAL.EliminarLogico(idProducto);
        }
    }
}