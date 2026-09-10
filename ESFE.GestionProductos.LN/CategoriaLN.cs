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

        // --- Módulo Categorías (STOCKEO): grid con conteo de productos ---

        public List<CategoriaConConteoDTO> ListarConConteo()
        {
            return categoriaDAL.ListarConConteo()
                .Select(c => new CategoriaConConteoDTO
                {
                    IdCategoriaPK = c.IdCategoriaPK,
                    Nombre = c.Nombre,
                    Descripcion = c.Descripcion,
                    TotalProductos = c.TotalProductos
                })
                .ToList();
        }

        public short Crear(string nombre, string? descripcion)
        {
            ValidarNombreDescripcion(nombre, descripcion);
            return categoriaDAL.Crear(nombre.Trim(), NormalizarDescripcion(descripcion));
        }

        public bool Actualizar(short idCategoriaPK, string nombre, string? descripcion)
        {
            ValidarNombreDescripcion(nombre, descripcion);
            int filasAfectadas = categoriaDAL.Actualizar(idCategoriaPK, nombre.Trim(), NormalizarDescripcion(descripcion));
            return filasAfectadas > 0;
        }

        public ResultadoEliminacionCategoria Eliminar(short idCategoriaPK)
        {
            int resultado = categoriaDAL.Eliminar(idCategoriaPK);
            return resultado switch
            {
                -1 => ResultadoEliminacionCategoria.TieneProductos,
                0 => ResultadoEliminacionCategoria.NoEncontrada,
                _ => ResultadoEliminacionCategoria.Ok
            };
        }

        private static void ValidarNombreDescripcion(string nombre, string? descripcion)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre es obligatorio.", nameof(nombre));

            if (nombre.Trim().Length > 100)
                throw new ArgumentException("El nombre no puede superar los 100 caracteres.", nameof(nombre));

            if (descripcion != null && descripcion.Length > 255)
                throw new ArgumentException("La descripción no puede superar los 255 caracteres.", nameof(descripcion));
        }

        private static string? NormalizarDescripcion(string? descripcion)
        {
            return string.IsNullOrWhiteSpace(descripcion) ? null : descripcion.Trim();
        }
    }
}