namespace ESFE.GestionProductos.UI
{
    partial class frmMain
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            menuTabControl = new MaterialSkin.Controls.MaterialTabControl();
            tabEmpleados = new TabPage();
            pnlContenedorEmpleados = new Panel();
            tabProductos = new TabPage();
            pnlContenedorProductos = new Panel();
            menuTabControl.SuspendLayout();
            tabEmpleados.SuspendLayout();
            tabProductos.SuspendLayout();
            SuspendLayout();
            // 
            // menuTabControl
            // 
            menuTabControl.Controls.Add(tabEmpleados);
            menuTabControl.Controls.Add(tabProductos);
            menuTabControl.Depth = 0;
            menuTabControl.Dock = DockStyle.Fill;
            menuTabControl.Location = new Point(3, 64);
            menuTabControl.MouseState = MaterialSkin.MouseState.HOVER;
            menuTabControl.Multiline = true;
            menuTabControl.Name = "menuTabControl";
            menuTabControl.SelectedIndex = 0;
            menuTabControl.Size = new Size(994, 833);
            menuTabControl.TabIndex = 0;
            // 
            // tabEmpleados
            // 
            tabEmpleados.Controls.Add(pnlContenedorEmpleados);
            tabEmpleados.Location = new Point(4, 24);
            tabEmpleados.Name = "tabEmpleados";
            tabEmpleados.Padding = new Padding(3);
            tabEmpleados.Size = new Size(986, 805);
            tabEmpleados.TabIndex = 0;
            tabEmpleados.Text = "Empleados";
            tabEmpleados.UseVisualStyleBackColor = true;
            // 
            // pnlContenedorEmpleados
            // 
            pnlContenedorEmpleados.Dock = DockStyle.Fill;
            pnlContenedorEmpleados.Location = new Point(3, 3);
            pnlContenedorEmpleados.Name = "pnlContenedorEmpleados";
            pnlContenedorEmpleados.Size = new Size(980, 799);
            pnlContenedorEmpleados.TabIndex = 0;
            // 
            // tabProductos
            // 
            tabProductos.Controls.Add(pnlContenedorProductos);
            tabProductos.Location = new Point(4, 24);
            tabProductos.Name = "tabProductos";
            tabProductos.Padding = new Padding(3);
            tabProductos.Size = new Size(986, 805);
            tabProductos.TabIndex = 1;
            tabProductos.Text = "Productos";
            tabProductos.UseVisualStyleBackColor = true;
            // 
            // pnlContenedorProductos
            // 
            pnlContenedorProductos.Dock = DockStyle.Fill;
            pnlContenedorProductos.Location = new Point(3, 3);
            pnlContenedorProductos.Name = "pnlContenedorProductos";
            pnlContenedorProductos.Size = new Size(980, 799);
            pnlContenedorProductos.TabIndex = 0;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 900);
            //WindowState = FormWindowState.Maximized; // Añade esta línea para iniciar maximizado
            Controls.Add(menuTabControl);
            Name = "frmMain";
            Text = "Sistema de Gestión";
            menuTabControl.ResumeLayout(false);
            tabEmpleados.ResumeLayout(false);
            tabProductos.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private MaterialSkin.Controls.MaterialTabControl menuTabControl;
        private TabPage tabEmpleados;
        private TabPage tabProductos;
        private Panel pnlContenedorEmpleados;
        private Panel pnlContenedorProductos;
    }
}