namespace sürücü_kursu
{
    partial class Evrakdüzenleme
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button3 = new System.Windows.Forms.Button();
            this.btnGüncelle = new System.Windows.Forms.Button();
            this.btnsil = new System.Windows.Forms.Button();
            this.btnekle = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 16);
            this.label1.TabIndex = 58;
            this.label1.Text = "ADAY NO :";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(93, 27);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(154, 23);
            this.textBox1.TabIndex = 59;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(2, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 16);
            this.label2.TabIndex = 60;
            this.label2.Text = "EVRAK ADI :";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Fotoğraf",
            "Kan Grubu Kartı",
            "İkametgah",
            "Sabıka Kaydı",
            "Sağlık Raporu",
            "Şoförler Cemiyeti Dosyası"});
            this.comboBox1.Location = new System.Drawing.Point(93, 69);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(154, 24);
            this.comboBox1.TabIndex = 61;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.LightSkyBlue;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Century Schoolbook", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button3.ForeColor = System.Drawing.Color.Snow;
            this.button3.Location = new System.Drawing.Point(291, 17);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(121, 47);
            this.button3.TabIndex = 163;
            this.button3.Text = "Temizle";
            this.button3.UseVisualStyleBackColor = false;
            // 
            // btnGüncelle
            // 
            this.btnGüncelle.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnGüncelle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGüncelle.Font = new System.Drawing.Font("Century Schoolbook", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnGüncelle.ForeColor = System.Drawing.Color.Snow;
            this.btnGüncelle.Location = new System.Drawing.Point(291, 70);
            this.btnGüncelle.Name = "btnGüncelle";
            this.btnGüncelle.Size = new System.Drawing.Size(121, 47);
            this.btnGüncelle.TabIndex = 162;
            this.btnGüncelle.Text = "Güncelle\r\n";
            this.btnGüncelle.UseVisualStyleBackColor = false;
            // 
            // btnsil
            // 
            this.btnsil.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnsil.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnsil.Font = new System.Drawing.Font("Century Schoolbook", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsil.ForeColor = System.Drawing.Color.Snow;
            this.btnsil.Location = new System.Drawing.Point(418, 70);
            this.btnsil.Name = "btnsil";
            this.btnsil.Size = new System.Drawing.Size(95, 47);
            this.btnsil.TabIndex = 161;
            this.btnsil.Text = "Sil";
            this.btnsil.UseVisualStyleBackColor = false;
            // 
            // btnekle
            // 
            this.btnekle.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnekle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnekle.Font = new System.Drawing.Font("Century Schoolbook", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnekle.ForeColor = System.Drawing.Color.Snow;
            this.btnekle.Location = new System.Drawing.Point(418, 17);
            this.btnekle.Name = "btnekle";
            this.btnekle.Size = new System.Drawing.Size(95, 47);
            this.btnekle.TabIndex = 160;
            this.btnekle.Text = "Ekle";
            this.btnekle.UseVisualStyleBackColor = false;
            // 
            // Evrakdüzenleme
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(581, 344);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnGüncelle);
            this.Controls.Add(this.btnsil);
            this.Controls.Add(this.btnekle);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Name = "Evrakdüzenleme";
            this.Text = "Evrak Düzenleme";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnGüncelle;
        private System.Windows.Forms.Button btnsil;
        private System.Windows.Forms.Button btnekle;

    }
}