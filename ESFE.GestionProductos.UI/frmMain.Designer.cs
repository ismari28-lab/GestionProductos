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

        private void InitializeComponent()
        {
            menuTabControl = new MaterialSkin.Controls.MaterialTabControl();
            tabInicio = new TabPage();
            pnlContenedorInicio = new Panel();
            tabEmpleados = new TabPage();
            pnlContenedorEmpleados = new Panel();
            tabProductos = new TabPage();
            pnlContenedorProductos = new Panel();
            tabUsuarios = new TabPage();
            pnlContenedorUsuarios = new Panel();
            menuTabControl.SuspendLayout();
            tabInicio.SuspendLayout();
            tabEmpleados.SuspendLayout();
            tabProductos.SuspendLayout();
            tabUsuarios.SuspendLayout();
            SuspendLayout();
            //
            // menuTabControl
            //
            menuTabControl.Controls.Add(tabInicio);
            menuTabControl.Controls.Add(tabEmpleados);
            menuTabControl.Controls.Add(tabProductos);
            menuTabControl.Controls.Add(tabUsuarios);
            menuTabControl.Depth = 0;
            menuTabControl.Dock = DockStyle.Fill;
            menuTabControl.Location = new Point(3, 64);
            menuTabControl.MouseState = MaterialSkin.MouseState.HOVER;
            menuTabControl.Multiline = true;
            menuTabControl.Name = "menuTabControl";
            menuTabControl.SelectedIndex = 0;
            menuTabControl.Size = new Size(994, 721);
            menuTabControl.TabIndex = 0;
            // 
            // tabInicio
            // 
            tabInicio.BackColor = Color.White;
            tabInicio.Controls.Add(pnlContenedorInicio);
            tabInicio.Location = new Point(4, 24);
            tabInicio.Name = "tabInicio";
            tabInicio.Padding = new Padding(3);
            tabInicio.Size = new Size(986, 693);
            tabInicio.TabIndex = 0;
            tabInicio.Text = "Inicio";
            // 
            // pnlContenedorInicio
            // 
            pnlContenedorInicio.Dock = DockStyle.Fill;
            pnlContenedorInicio.Location = new Point(3, 3);
            pnlContenedorInicio.Name = "pnlContenedorInicio";
            pnlContenedorInicio.Size = new Size(980, 687);
            pnlContenedorInicio.TabIndex = 0;
            // 
            // tabEmpleados
            // 
            tabEmpleados.Controls.Add(pnlContenedorEmpleados);
            tabEmpleados.Location = new Point(4, 24);
            tabEmpleados.Name = "tabEmpleados";
            tabEmpleados.Padding = new Padding(3);
            tabEmpleados.Size = new Size(986, 805);
            tabEmpleados.TabIndex = 1;
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
            tabProductos.Size = new Size(986, 693);
            tabProductos.TabIndex = 2;
            tabProductos.Text = "Productos";
            tabProductos.UseVisualStyleBackColor = true;
            // 
            // pnlContenedorProductos
            // 
            pnlContenedorProductos.Dock = DockStyle.Fill;
            pnlContenedorProductos.Location = new Point(3, 3);
            pnlContenedorProductos.Name = "pnlContenedorProductos";
            pnlContenedorProductos.Size = new Size(980, 687);
            pnlContenedorProductos.TabIndex = 0;
            //
            // tabUsuarios
            //
            tabUsuarios.Controls.Add(pnlContenedorUsuarios);
            tabUsuarios.Location = new Point(4, 24);
            tabUsuarios.Name = "tabUsuarios";
            tabUsuarios.Padding = new Padding(3);
            tabUsuarios.Size = new Size(986, 693);
            tabUsuarios.TabIndex = 3;
            tabUsuarios.Text = "Usuarios";
            tabUsuarios.UseVisualStyleBackColor = true;
            //
            // pnlContenedorUsuarios
            //
            pnlContenedorUsuarios.Dock = DockStyle.Fill;
            pnlContenedorUsuarios.Location = new Point(3, 3);
            pnlContenedorUsuarios.Name = "pnlContenedorUsuarios";
            pnlContenedorUsuarios.Size = new Size(980, 687);
            pnlContenedorUsuarios.TabIndex = 0;
            //
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 788);
            Controls.Add(menuTabControl);
            Name = "frmMain";
            Text = "Sistema de Gestión";
            menuTabControl.ResumeLayout(false);
            tabInicio.ResumeLayout(false);
            tabEmpleados.ResumeLayout(false);
            tabProductos.ResumeLayout(false);
            tabUsuarios.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private MaterialSkin.Controls.MaterialTabControl menuTabControl;
        private TabPage tabInicio;
        private Panel pnlContenedorInicio;   // NUEVO
        private TabPage tabEmpleados;
        private TabPage tabProductos;
        private Panel pnlContenedorEmpleados;
        private Panel pnlContenedorProductos;
        private TabPage tabUsuarios;
        private Panel pnlContenedorUsuarios;
    }
}