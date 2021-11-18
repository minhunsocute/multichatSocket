
namespace Client
{
    partial class ListClietnActi
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.nameText = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.acti = new Client.CircularPicture();
            this.avt = new Client.CircularPicture();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            ((System.ComponentModel.ISupportInitialize)(this.acti)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.avt)).BeginInit();
            this.SuspendLayout();
            // 
            // nameText
            // 
            this.nameText.AutoSize = true;
            this.nameText.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.nameText.ForeColor = System.Drawing.Color.Black;
            this.nameText.Location = new System.Drawing.Point(62, 12);
            this.nameText.Name = "nameText";
            this.nameText.Size = new System.Drawing.Size(92, 20);
            this.nameText.TabIndex = 22;
            this.nameText.Text = "Being Active";
            this.nameText.MouseEnter += new System.EventHandler(this.nameText_MouseEnter);
            this.nameText.MouseLeave += new System.EventHandler(this.nameText_MouseLeave);
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 47);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(180, 1);
            this.panel1.TabIndex = 23;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 46);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(180, 1);
            this.panel2.TabIndex = 24;
            // 
            // acti
            // 
            this.acti.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(162)))), ((int)(((byte)(76)))));
            this.acti.Location = new System.Drawing.Point(45, 33);
            this.acti.Name = "acti";
            this.acti.Size = new System.Drawing.Size(10, 10);
            this.acti.TabIndex = 21;
            this.acti.TabStop = false;
            // 
            // avt
            // 
            this.avt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.avt.Image = global::Client.Properties.Resources._245563627_551284619266395_1057269644741995866_n;
            this.avt.Location = new System.Drawing.Point(11, 3);
            this.avt.Name = "avt";
            this.avt.Size = new System.Drawing.Size(39, 42);
            this.avt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.avt.TabIndex = 20;
            this.avt.TabStop = false;
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.AutoSize = false;
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Red;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel1.ForeColor = System.Drawing.Color.White;
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(160, 3);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(17, 20);
            this.guna2HtmlLabel1.TabIndex = 25;
            this.guna2HtmlLabel1.Text = "1";
            this.guna2HtmlLabel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ListClietnActi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.guna2HtmlLabel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.nameText);
            this.Controls.Add(this.acti);
            this.Controls.Add(this.avt);
            this.Name = "ListClietnActi";
            this.Size = new System.Drawing.Size(180, 48);
            this.Click += new System.EventHandler(this.ListClietnActi_Click);
            this.MouseEnter += new System.EventHandler(this.ListClietnActi_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.ListClietnActi_MouseLeave);
            ((System.ComponentModel.ISupportInitialize)(this.acti)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.avt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CircularPicture avt;
        private CircularPicture acti;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Label nameText;
        public Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
    }
}
