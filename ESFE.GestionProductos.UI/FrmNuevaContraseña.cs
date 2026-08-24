using System;
using MaterialSkin;
using MaterialSkin.Controls;

namespace ESFE.GestionProductos.UI
{
    public partial class FrmNuevaContraseña : MaterialForm
    {
        public FrmNuevaContraseña()
        {
            InitializeComponent();

            MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;

            materialSkinManager.AddFormToManage(this);

            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;

            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Blue600, Primary.Blue400,
                Primary.Blue100, Accent.LightBlue200,
                TextShade.WHITE
            );

            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Sizable = false;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void FrmNuevaContraseña_Load(object sender, EventArgs e)
        {
            const int alturaBarraTitulo = 65;
            const int espacioEntreTituloYTarjeta = 30;

            // Ajuste fino: como el margen superior se veía un poco más
            // grande que el inferior, recorremos todo el bloque hacia
            // arriba esta cantidad de píxeles.
            const int ajusteFino = 10;

            int alturaDisponible = this.ClientSize.Height - alturaBarraTitulo;
            int alturaContenidoTotal = lblTitulo.Height + espacioEntreTituloYTarjeta + cardPrincipal.Height;
            int margenSimetrico = (alturaDisponible - alturaContenidoTotal) / 2;

            int posicionYTitulo = alturaBarraTitulo + margenSimetrico - ajusteFino;
            int posicionYTarjeta = posicionYTitulo + lblTitulo.Height + espacioEntreTituloYTarjeta;

            lblTitulo.Top = posicionYTitulo;
            lblTitulo.Left = (this.ClientSize.Width - lblTitulo.Width) / 2;

            cardPrincipal.Top = posicionYTarjeta;
            cardPrincipal.Left = (this.ClientSize.Width - cardPrincipal.Width) / 2;
        }
    }
}