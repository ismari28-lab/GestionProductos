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
                              string icono = "$", Color? colorIcono = null)
        {
            lblTitulo.Text = titulo;
            lblValor.Text = valor;
            lblTendencia.Text = tendencia;
            lblTendencia.ForeColor = esPositivo ? Color.FromArgb(40, 167, 69) : Color.FromArgb(220, 53, 69);

            lblIcono.Text = icono;
            var c = colorIcono ?? Color.FromArgb(40, 167, 69);
            lblIcono.ForeColor = c;

            // Fondo pastel del mismo tono (mezcla el color con blanco al 85%)
            pnlIcono.BackColor = Color.FromArgb(
                (int)(c.R * 0.15 + 255 * 0.85),
                (int)(c.G * 0.15 + 255 * 0.85),
                (int)(c.B * 0.15 + 255 * 0.85)
            );
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