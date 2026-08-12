using System;
using System.Drawing;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace ESFE.GestionProductos.UI
{
    public partial class frmMain : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;

        public frmMain()
        {
            InitializeComponent();

            // 1. Configurar tema de MaterialSkin
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Blue700, Primary.Blue800,
                Primary.Blue500, Accent.LightBlue200,
                TextShade.WHITE
            );

            // 2. Conectar y ampliar la barra lateral (Drawer) a 280px para dar respiro
            DrawerIsOpen = true;
            DrawerShowIconsWhenHidden = true;
            DrawerTabControl = menuTabControl;
            DrawerWidth = 280;

            // 3. Inicio maximizado
            this.WindowState = FormWindowState.Maximized;

            // 4. Cargar la vista de empleados en su panel
            CargarVistaEmpleados();
        }

        private void CargarVistaEmpleados()
        {
            pnlContenedorEmpleados.Controls.Clear();
            var ucEmp = new ucEmpleado();
            ucEmp.Dock = DockStyle.Fill;
            pnlContenedorEmpleados.Controls.Add(ucEmp);
            ucEmp.BringToFront();
        }
    }
}