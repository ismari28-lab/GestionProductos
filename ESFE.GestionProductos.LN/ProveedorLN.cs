using System;
using System.Collections.Generic;
using System.Data;
using ESFE.GestionProductos.DAL;
using ESFE.GestionProductos.EN;

namespace ESFE.GestionProductos.LN
{
    public class ProveedorLN
    {
        private readonly ProveedorDAL proveedorDAL = new ProveedorDAL();

        public DataTable Listar()
        {
            return proveedorDAL.Listar();
        }

        public List<Proveedor> Buscar(
            string nombre = null,
            string empresa = null,
            string telefono = null,
            string correo = null,
            string direccion = null,
            bool? estado = null)
        {
            return proveedorDAL.Buscar(nombre, empresa, telefono, correo, direccion, estado);
        }

        // Especial para combos: solo activos
        public List<Proveedor> ObtenerActivos()
        {
            return proveedorDAL.Buscar(null, null, null, null, null, true);
        }

        public int Guardar(Proveedor proveedor)
        {
            if (proveedor == null)
                throw new ArgumentNullException(nameof(proveedor));

            if (proveedor.IdProveedorPK > 0)
                return proveedorDAL.Actualizar(proveedor);

            return proveedorDAL.Insertar(proveedor);
        }

        public int Insertar(Proveedor proveedor)
        {
            if (proveedor == null)
                throw new ArgumentNullException(nameof(proveedor));
            return proveedorDAL.Insertar(proveedor);
        }

        public int Actualizar(Proveedor proveedor)
        {
            if (proveedor == null)
                throw new ArgumentNullException(nameof(proveedor));
            return proveedorDAL.Actualizar(proveedor);
        }

        public int EliminarLogico(short idProveedor)
        {
            return proveedorDAL.EliminarLogico(idProveedor);
        }
    }
}