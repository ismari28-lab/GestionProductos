// Esta clase de lógica de negocio sirve para obtener la lista de roles disponibles en el sistema.
using System.Collections.Generic;
using ESFE.GestionProductos.DAL;
using ESFE.GestionProductos.EN;

namespace ESFE.GestionProductos.LN
{
    public class RolLN
    {
        private readonly RolDAL rolDAL = new RolDAL();

        public List<Rol> Listar()
        {
            return rolDAL.Listar();
        }
    }
}
