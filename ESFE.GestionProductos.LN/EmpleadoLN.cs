using System;
using System.Collections.Generic;
using System.Data;
using ESFE.GestionProductos.DAL;
using ESFE.GestionProductos.EN;

namespace ESFE.GestionProductos.LN
{
    public class EmpleadoLN
    {
        private readonly EmpleadoDAL _empleadoDAL = new EmpleadoDAL();

        // 1. Listar Empleados
        public DataTable Listar()
        {
            return _empleadoDAL.Listar();
        }

        // 2. Buscar Empleados con Filtros
        public List<Empleado> Buscar(string nombre = null, short? idEmpleado = null)
        {
            return _empleadoDAL.Buscar(nombre, idEmpleado);
        }

        // 3. Guardar con Validaciones
        public int Guardar(Empleado empleado)
        {
            // Validaciones de negocio antes de persistir
            if (empleado == null)
                throw new ArgumentNullException(nameof(empleado), "El empleado no puede ser nulo.");

            if (string.IsNullOrWhiteSpace(empleado.Nombre))
                throw new Exception("El nombre del empleado es obligatorio.");

            // Decisión de negocio: Si tiene ID mayor a 0 actualiza, de lo contrario inserta
            if (empleado.IdEmpleadoPK > 0)
            {
                return _empleadoDAL.Actualizar(empleado);
            }
            else
            {
                return _empleadoDAL.Insertar(empleado);
            }
        }

        // 4. Eliminación Lógica
        public int EliminarLogico(short idEmpleado)
        {
            if (idEmpleado <= 0)
                throw new ArgumentException("El ID del empleado debe ser válido.");

            return _empleadoDAL.EliminarLogico(idEmpleado);
        }
    }
}