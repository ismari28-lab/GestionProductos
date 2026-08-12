using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ESFE.GestionProductos.UI
{
    public partial class ucCardResumen : UserControl
    {
        public ucCardResumen()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);

        }

        public void ConfigurarTarjeta(string titulo, string valor, string tendencia, bool esPositivo,
                                      string icono, Color colorIcono)
        {
            lblTitulo.Text = titulo;
            lblValor.Text = valor;
            lblTendencia.Text = tendencia;
            lblTendencia.ForeColor = esPositivo ? Color.FromArgb(40, 167, 69) : Color.FromArgb(220, 53, 69);

            lblIcono.Text = icono;
            lblIcono.ForeColor = colorIcono;

            // Fondo pastel del mismo tono del ícono
            pnlIcono.BackColor = MezclarConBlanco(colorIcono, 0.85);
        }

        // Mezcla un color con blanco. porcentajeBlanco entre 0 (color puro) y 1 (blanco).
        private Color MezclarConBlanco(Color c, double porcentajeBlanco)
        {
            int r = (int)(c.R * (1 - porcentajeBlanco) + 255 * porcentajeBlanco);
            int g = (int)(c.G * (1 - porcentajeBlanco) + 255 * porcentajeBlanco);
            int b = (int)(c.B * (1 - porcentajeBlanco) + 255 * porcentajeBlanco);
            return Color.FromArgb(r, g, b);
        }

        // Borde gris tenue alrededor de la card
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (var p = new Pen(Color.FromArgb(225, 228, 232), 1))
            {
                var r = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
                e.Graphics.DrawRectangle(p, r);
            }
        }
    }
}