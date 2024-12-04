namespace EnerGymADMIN.ClasesGrupalesForm
{
    partial class RegistrarClase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTema = new System.Windows.Forms.TextBox();
            this.btnRegistrar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbEntrendor = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dtpFechaIni = new System.Windows.Forms.DateTimePicker();
            this.nudHoraFechaIni = new System.Windows.Forms.NumericUpDown();
            this.nudMinFechaIni = new System.Windows.Forms.NumericUpDown();
            this.panel2 = new System.Windows.Forms.Panel();
            this.nudMinFechaFin = new System.Windows.Forms.NumericUpDown();
            this.nudHoraFechaFin = new System.Windows.Forms.NumericUpDown();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHoraFechaIni)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinFechaIni)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinFechaFin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHoraFechaFin)).BeginInit();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(333, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(169, 46);
            this.label7.TabIndex = 25;
            this.label7.Text = "Registro";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(82, 270);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 20);
            this.label4.TabIndex = 22;
            this.label4.Text = "Horario de Inicio";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(82, 198);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 20);
            this.label2.TabIndex = 21;
            this.label2.Text = "Entrenador";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(82, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 20);
            this.label1.TabIndex = 20;
            this.label1.Text = "Tema de la clase";
            // 
            // txtTema
            // 
            this.txtTema.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTema.Location = new System.Drawing.Point(86, 149);
            this.txtTema.Name = "txtTema";
            this.txtTema.Size = new System.Drawing.Size(318, 26);
            this.txtTema.TabIndex = 16;
            // 
            // btnRegistrar
            // 
            this.btnRegistrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegistrar.Location = new System.Drawing.Point(548, 258);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(165, 61);
            this.btnRegistrar.TabIndex = 15;
            this.btnRegistrar.Text = "Registrar";
            this.btnRegistrar.UseVisualStyleBackColor = true;
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(85, 383);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(170, 20);
            this.label3.TabIndex = 27;
            this.label3.Text = "Horario de Finalización";
            // 
            // cmbEntrendor
            // 
            this.cmbEntrendor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbEntrendor.FormattingEnabled = true;
            this.cmbEntrendor.Location = new System.Drawing.Point(86, 221);
            this.cmbEntrendor.Name = "cmbEntrendor";
            this.cmbEntrendor.Size = new System.Drawing.Size(318, 28);
            this.cmbEntrendor.TabIndex = 28;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.nudMinFechaIni);
            this.panel1.Controls.Add(this.nudHoraFechaIni);
            this.panel1.Controls.Add(this.dtpFechaIni);
            this.panel1.Location = new System.Drawing.Point(86, 293);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(166, 67);
            this.panel1.TabIndex = 29;
            // 
            // dtpFechaIni
            // 
            this.dtpFechaIni.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaIni.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaIni.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaIni.Location = new System.Drawing.Point(3, 3);
            this.dtpFechaIni.Name = "dtpFechaIni";
            this.dtpFechaIni.Size = new System.Drawing.Size(157, 26);
            this.dtpFechaIni.TabIndex = 6;
            // 
            // nudHoraFechaIni
            // 
            this.nudHoraFechaIni.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudHoraFechaIni.Location = new System.Drawing.Point(31, 35);
            this.nudHoraFechaIni.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.nudHoraFechaIni.Name = "nudHoraFechaIni";
            this.nudHoraFechaIni.Size = new System.Drawing.Size(40, 26);
            this.nudHoraFechaIni.TabIndex = 30;
            // 
            // nudMinFechaIni
            // 
            this.nudMinFechaIni.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudMinFechaIni.Location = new System.Drawing.Point(80, 35);
            this.nudMinFechaIni.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.nudMinFechaIni.Name = "nudMinFechaIni";
            this.nudMinFechaIni.Size = new System.Drawing.Size(40, 26);
            this.nudMinFechaIni.TabIndex = 31;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.nudMinFechaFin);
            this.panel2.Controls.Add(this.nudHoraFechaFin);
            this.panel2.Controls.Add(this.dtpFechaFin);
            this.panel2.Location = new System.Drawing.Point(89, 406);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(166, 67);
            this.panel2.TabIndex = 32;
            // 
            // nudMinFechaFin
            // 
            this.nudMinFechaFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudMinFechaFin.Location = new System.Drawing.Point(80, 35);
            this.nudMinFechaFin.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.nudMinFechaFin.Name = "nudMinFechaFin";
            this.nudMinFechaFin.Size = new System.Drawing.Size(40, 26);
            this.nudMinFechaFin.TabIndex = 31;
            // 
            // nudHoraFechaFin
            // 
            this.nudHoraFechaFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudHoraFechaFin.Location = new System.Drawing.Point(31, 35);
            this.nudHoraFechaFin.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.nudHoraFechaFin.Name = "nudHoraFechaFin";
            this.nudHoraFechaFin.Size = new System.Drawing.Size(40, 26);
            this.nudHoraFechaFin.TabIndex = 30;
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFin.Location = new System.Drawing.Point(3, 3);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(157, 26);
            this.dtpFechaFin.TabIndex = 6;
            // 
            // RegistrarClase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 538);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cmbEntrendor);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTema);
            this.Controls.Add(this.btnRegistrar);
            this.Name = "RegistrarClase";
            this.Text = "RegistrarClase";
            this.Load += new System.EventHandler(this.RegistrarClase_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudHoraFechaIni)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinFechaIni)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudMinFechaFin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHoraFechaFin)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTema;
        private System.Windows.Forms.Button btnRegistrar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbEntrendor;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown nudMinFechaIni;
        private System.Windows.Forms.NumericUpDown nudHoraFechaIni;
        private System.Windows.Forms.DateTimePicker dtpFechaIni;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.NumericUpDown nudMinFechaFin;
        private System.Windows.Forms.NumericUpDown nudHoraFechaFin;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
    }
}