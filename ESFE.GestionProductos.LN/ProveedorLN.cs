using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ESFE.GestionProductos.DAL;
using ESFE.GestionProductos.EN;
using ESFE.GestionProductos.LN.DTOs;
using ESFE.GestionProductos.LN.Enums;

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

        // --- Módulo Proveedores (STOCKEO): listado, modal Crear/Editar/Eliminar ---

        public List<ProveedorListadoDTO> ListarParaWeb()
        {
            return proveedorDAL.ListarTodos()
                .Select(p => new ProveedorListadoDTO
                {
                    IdProveedorPK = p.IdProveedorPK,
                    Nombre = p.Nombre,
                    Empresa = p.Empresa,
                    Telefono = p.Telefono,
                    Correo = p.Correo,
                    Direccion = p.Direccion,
                    Estado = p.Estado
                })
                .OrderBy(p => p.Nombre)
                .ToList();
        }

        public ProveedorEdicionDTO? ObtenerParaEdicion(int id)
        {
            if (id <= 0)
                return null;

            var p = proveedorDAL.ListarTodos().FirstOrDefault(x => x.IdProveedorPK == id);
            if (p.Nombre == null)
                return null;

            return new ProveedorEdicionDTO
            {
                IdProveedorPK = p.IdProveedorPK,
                Nombre = p.Nombre,
                Empresa = p.Empresa,
                Telefono = p.Telefono,
                Correo = p.Correo,
                Direccion = p.Direccion,
                Estado = p.Estado
            };
        }

        public ResultadoGuardarProveedorDTO Crear(ProveedorFormDTO form)
        {
            var error = ValidarFormulario(form);
            if (error != null)
                return error;

            try
            {
                int nuevoId = proveedorDAL.CrearProveedor(
                    form.Nombre.Trim(), NormalizarOpcional(form.Empresa), NormalizarOpcional(form.Telefono),
                    NormalizarOpcional(form.Correo), NormalizarOpcional(form.Direccion));

                return new ResultadoGuardarProveedorDTO { Resultado = ResultadoGuardarProveedor.Ok, IdGenerado = nuevoId };
            }
            catch (Exception ex)
            {
                return new ResultadoGuardarProveedorDTO { Resultado = ResultadoGuardarProveedor.ErrorInterno, Mensaje = ex.Message };
            }
        }

        public ResultadoGuardarProveedorDTO Actualizar(ProveedorFormDTO form)
        {
            if (!form.IdProveedorPK.HasValue || form.IdProveedorPK.Value <= 0)
                return new ResultadoGuardarProveedorDTO
                {
                    Resultado = ResultadoGuardarProveedor.DatosInvalidos,
                    Mensaje = "ID de proveedor inválido."
                };

            var error = ValidarFormulario(form);
            if (error != null)
                return error;

            try
            {
                int filas = proveedorDAL.ActualizarProveedor(
                    form.IdProveedorPK.Value, form.Nombre.Trim(), NormalizarOpcional(form.Empresa),
                    NormalizarOpcional(form.Telefono), NormalizarOpcional(form.Correo), NormalizarOpcional(form.Direccion),
                    form.Estado);

                if (filas == 0)
                    return new ResultadoGuardarProveedorDTO
                    {
                        Resultado = ResultadoGuardarProveedor.NoEncontrado,
                        Mensaje = "Proveedor no encontrado."
                    };

                return new ResultadoGuardarProveedorDTO { Resultado = ResultadoGuardarProveedor.Ok, IdGenerado = form.IdProveedorPK };
            }
            catch (Exception ex)
            {
                return new ResultadoGuardarProveedorDTO { Resultado = ResultadoGuardarProveedor.ErrorInterno, Mensaje = ex.Message };
            }
        }

        public ResultadoEliminarProveedorDTO Eliminar(int id)
        {
            if (id <= 0)
                return new ResultadoEliminarProveedorDTO { Resultado = ResultadoEliminarProveedor.NoEncontrado, Mensaje = "Proveedor no encontrado." };

            try
            {
                int resultado = proveedorDAL.EliminarProveedor(id);
                return resultado switch
                {
                    -1 => new ResultadoEliminarProveedorDTO
                    {
                        Resultado = ResultadoEliminarProveedor.TieneProductos,
                        Mensaje = "No se puede eliminar: el proveedor tiene productos activos asociados."
                    },
                    0 => new ResultadoEliminarProveedorDTO
                    {
                        Resultado = ResultadoEliminarProveedor.NoEncontrado,
                        Mensaje = "Proveedor no encontrado."
                    },
                    _ => new ResultadoEliminarProveedorDTO { Resultado = ResultadoEliminarProveedor.Ok }
                };
            }
            catch (Exception ex)
            {
                return new ResultadoEliminarProveedorDTO { Resultado = ResultadoEliminarProveedor.ErrorInterno, Mensaje = ex.Message };
            }
        }

        private static ResultadoGuardarProveedorDTO? ValidarFormulario(ProveedorFormDTO form)
        {
            if (form == null)
                return Invalido("Datos del proveedor faltantes.");

            if (string.IsNullOrWhiteSpace(form.Nombre))
                return Invalido("El nombre es obligatorio.");

            if (form.Nombre.Trim().Length > 100)
                return Invalido("El nombre no puede superar los 100 caracteres.");

            if (!string.IsNullOrEmpty(form.Empresa) && form.Empresa.Length > 100)
                return Invalido("La empresa no puede superar los 100 caracteres.");

            if (!string.IsNullOrEmpty(form.Telefono) && form.Telefono.Length > 20)
                return Invalido("El teléfono no puede superar los 20 caracteres.");

            if (!string.IsNullOrEmpty(form.Correo) && form.Correo.Length > 150)
                return Invalido("El correo no puede superar los 150 caracteres.");

            if (!string.IsNullOrEmpty(form.Direccion) && form.Direccion.Length > 255)
                return Invalido("La dirección no puede superar los 255 caracteres.");

            return null;
        }

        private static string? NormalizarOpcional(string? valor)
        {
            return string.IsNullOrWhiteSpace(valor) ? null : valor.Trim();
        }

        private static ResultadoGuardarProveedorDTO Invalido(string mensaje)
        {
            return new ResultadoGuardarProveedorDTO { Resultado = ResultadoGuardarProveedor.DatosInvalidos, Mensaje = mensaje };
        }
    }
}