namespace ESFE.GestionProductos.UI
{
    partial class ucCardResumen
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblValor = new System.Windows.Forms.Label();
            this.lblTendencia = new System.Windows.Forms.Label();
            this.pnlIcono = new System.Windows.Forms.Panel();
            this.lblIcono = new System.Windows.Forms.Label();
            this.pnlIcono.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(120, 130, 145);
            this.lblTitulo.Location = new System.Drawing.Point(20, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Text = "Total Vendido";
            // 
            // lblValor
            // 
            this.lblValor.AutoSize = true;
            this.lblValor.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblValor.ForeColor = System.Drawing.Color.FromArgb(30, 40, 50);
            this.lblValor.Location = new System.Drawing.Point(18, 48);
            this.lblValor.Name = "lblValor";
            this.lblValor.Text = "21,324";
            // 
            // lblTendencia
            // 
            this.lblTendencia.AutoSize = true;
            this.lblTendencia.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblTendencia.ForeColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.lblTendencia.Location = new System.Drawing.Point(20, 110);
            this.lblTendencia.Name = "lblTendencia";
            this.lblTendencia.Text = "+2,031";
            // 
            // pnlIcono
            // 
            this.pnlIcono.BackColor = System.Drawing.Color.FromArgb(232, 245, 233);
            this.pnlIcono.Controls.Add(this.lblIcono);
            this.pnlIcono.Location = new System.Drawing.Point(210, 20);
            this.pnlIcono.Name = "pnlIcono";
            this.pnlIcono.Size = new System.Drawing.Size(36, 36);
            // 
            // lblIcono
            // 
            this.lblIcono.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIcono.Font = new System.Drawing.Font("Segoe UI Symbol", 12F, System.Drawing.FontStyle.Bold);
            this.lblIcono.ForeColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.lblIcono.Name = "lblIcono";
            this.lblIcono.Text = "$";
            this.lblIcono.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ucCardResumen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnlIcono);
            this.Controls.Add(this.lblTendencia);
            this.Controls.Add(this.lblValor);
            this.Controls.Add(this.lblTitulo);
            this.Name = "ucCardResumen";
            this.Size = new System.Drawing.Size(260, 150);
            this.pnlIcono.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblValor;
        private System.Windows.Forms.Label lblTendencia;
        private System.Windows.Forms.Panel pnlIcono;
        private System.Windows.Forms.Label lblIcono;

    }
}