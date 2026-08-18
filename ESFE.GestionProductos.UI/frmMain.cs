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

        // Banderas para no recrear los UserControls cada vez que se cambia de pestaña
        private bool inicioCargado = false;
        private bool empleadosCargado = false;
        private bool productosCargado = false; // Flag para el catálogo de productos

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

            // 2. Conectar y ampliar la barra lateral (Drawer) a 280px
            DrawerIsOpen = true;
            DrawerShowIconsWhenHidden = true;
            DrawerTabControl = menuTabControl;
            DrawerWidth = 280;

            // 3. Inicio maximizado
            this.WindowState = FormWindowState.Maximized;

            // 4. Escuchar cambios de pestaña
            menuTabControl.Selected += MenuTabControl_Selected;

            // 5. Cargar la vista de inicio (dashboard) al arrancar
            CargarVistaInicio();
        }

        private void MenuTabControl_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == tabInicio)
            {
                CargarVistaInicio();
            }
            else if (e.TabPage == tabEmpleados)
            {
                CargarVistaEmpleados();
            }
            else if (e.TabPage == tabProductos)
            {
                CargarVistaProductos();
            }
        }

        private void CargarVistaInicio()
        {
            if (inicioCargado) return;

            pnlContenedorInicio.Controls.Clear();
            var ucIni = new ucInicio { Dock = DockStyle.Fill };
            pnlContenedorInicio.Controls.Add(ucIni);
            ucIni.BringToFront();

            inicioCargado = true;
        }

        private void CargarVistaEmpleados()
        {
            if (empleadosCargado) return;

            pnlContenedorEmpleados.Controls.Clear();
            var ucEmp = new ucEmpleado { Dock = DockStyle.Fill };
            pnlContenedorEmpleados.Controls.Add(ucEmp);
            ucEmp.BringToFront();

            empleadosCargado = true;
        }

        private void CargarVistaProductos()
        {
            if (productosCargado) return;

            pnlContenedorProductos.Controls.Clear();
            var ucProd = new UcCatalogoProductos { Dock = DockStyle.Fill };
            pnlContenedorProductos.Controls.Add(ucProd);
            ucProd.BringToFront();

            productosCargado = true;
        }
    }
}