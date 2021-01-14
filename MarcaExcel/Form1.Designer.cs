namespace MarcaExcel
{
    partial class Form1
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
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.txtLIst = new System.Windows.Forms.TextBox();
            this.Dividir = new System.Windows.Forms.Button();
            this.fileDialog = new System.Windows.Forms.OpenFileDialog();
            this.fileArchivoMail = new System.Windows.Forms.OpenFileDialog();
            this.fileArchivoErrores = new System.Windows.Forms.OpenFileDialog();
            this.btnValidar = new System.Windows.Forms.Button();
            this.txtValidaMail = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.btnMerge = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.White;
            this.btnLimpiar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.ForeColor = System.Drawing.Color.SteelBlue;
            this.btnLimpiar.Location = new System.Drawing.Point(5, 82);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(118, 40);
            this.btnLimpiar.TabIndex = 6;
            this.btnLimpiar.Text = "Validar Archivo";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // txtLIst
            // 
            this.txtLIst.Location = new System.Drawing.Point(129, 34);
            this.txtLIst.Multiline = true;
            this.txtLIst.Name = "txtLIst";
            this.txtLIst.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLIst.Size = new System.Drawing.Size(474, 139);
            this.txtLIst.TabIndex = 7;
            // 
            // Dividir
            // 
            this.Dividir.BackColor = System.Drawing.Color.White;
            this.Dividir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Dividir.ForeColor = System.Drawing.Color.SteelBlue;
            this.Dividir.Location = new System.Drawing.Point(5, 34);
            this.Dividir.Name = "Dividir";
            this.Dividir.Size = new System.Drawing.Size(118, 42);
            this.Dividir.TabIndex = 8;
            this.Dividir.Text = "Dividir Registros";
            this.Dividir.UseVisualStyleBackColor = false;
            this.Dividir.Click += new System.EventHandler(this.Dividir_Click);
            // 
            // fileArchivoMail
            // 
            this.fileArchivoMail.FileName = "openFileDialog1";
            // 
            // fileArchivoErrores
            // 
            this.fileArchivoErrores.FileName = "openFileDialog2";
            // 
            // btnValidar
            // 
            this.btnValidar.BackColor = System.Drawing.Color.White;
            this.btnValidar.ForeColor = System.Drawing.Color.SteelBlue;
            this.btnValidar.Location = new System.Drawing.Point(5, 191);
            this.btnValidar.Name = "btnValidar";
            this.btnValidar.Size = new System.Drawing.Size(118, 42);
            this.btnValidar.TabIndex = 13;
            this.btnValidar.Text = "ValidarMail";
            this.btnValidar.UseVisualStyleBackColor = false;
            this.btnValidar.Click += new System.EventHandler(this.btnValidar_Click);
            // 
            // txtValidaMail
            // 
            this.txtValidaMail.Location = new System.Drawing.Point(129, 203);
            this.txtValidaMail.Name = "txtValidaMail";
            this.txtValidaMail.Size = new System.Drawing.Size(317, 20);
            this.txtValidaMail.TabIndex = 14;
            // 
            // btnMerge
            // 
            this.btnMerge.BackColor = System.Drawing.Color.White;
            this.btnMerge.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMerge.ForeColor = System.Drawing.Color.SteelBlue;
            this.btnMerge.Location = new System.Drawing.Point(5, 128);
            this.btnMerge.Name = "btnMerge";
            this.btnMerge.Size = new System.Drawing.Size(118, 40);
            this.btnMerge.TabIndex = 15;
            this.btnMerge.Text = "Merge archivos";
            this.btnMerge.UseVisualStyleBackColor = false;
            this.btnMerge.Click += new System.EventHandler(this.btnMerge_Click);
            // 
            // folderBrowserDialog1
            // 
           
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(632, 260);
            this.Controls.Add(this.btnMerge);
            this.Controls.Add(this.txtValidaMail);
            this.Controls.Add(this.btnValidar);
            this.Controls.Add(this.Dividir);
            this.Controls.Add(this.txtLIst);
            this.Controls.Add(this.btnLimpiar);
            this.Name = "Form1";

            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.TextBox txtLIst;
        private System.Windows.Forms.Button Dividir;
        private System.Windows.Forms.OpenFileDialog fileDialog;
        private System.Windows.Forms.OpenFileDialog fileArchivoMail;
        private System.Windows.Forms.OpenFileDialog fileArchivoErrores;
        private System.Windows.Forms.Button btnValidar;
        private System.Windows.Forms.TextBox txtValidaMail;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button btnMerge;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}

