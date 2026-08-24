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
