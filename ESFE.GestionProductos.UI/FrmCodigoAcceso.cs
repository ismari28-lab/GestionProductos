using MaterialSkin;
using MaterialSkin.Controls;

namespace ESFE.GestionProductos.UI
{
    public partial class FrmCodigoAcceso : MaterialForm
    {
        public FrmCodigoAcceso()
        {
            InitializeComponent();

            // Configuración de MaterialSkin (mismo esquema azul usado en el resto de la app)
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Blue600, Primary.Blue400,
                Primary.Blue100, Accent.LightBlue200,
                TextShade.WHITE
            );

            // Configuración de la ventana
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Sizable = false;
            this.StartPosition = FormStartPosition.CenterScreen;
        }
    }
}