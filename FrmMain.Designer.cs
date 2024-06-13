
namespace GetMeteo
{
    partial class FrmMain
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
            this.txtCity = new System.Windows.Forms.TextBox();
            this.butExtractMeteo = new System.Windows.Forms.Button();
            this.lstResult = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(64, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ville : ";
            // 
            // txtCity
            // 
            this.txtCity.Location = new System.Drawing.Point(121, 27);
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(158, 20);
            this.txtCity.TabIndex = 1;
            this.txtCity.Text = "Mont-Joli";
            // 
            // butExtractMeteo
            // 
            this.butExtractMeteo.Location = new System.Drawing.Point(325, 25);
            this.butExtractMeteo.Name = "butExtractMeteo";
            this.butExtractMeteo.Size = new System.Drawing.Size(130, 23);
            this.butExtractMeteo.TabIndex = 2;
            this.butExtractMeteo.Text = "Extraite la météo";
            this.butExtractMeteo.UseVisualStyleBackColor = true;
            this.butExtractMeteo.Click += new System.EventHandler(this.butExtractMeteo_Click);
            // 
            // lstResult
            // 
            this.lstResult.FormattingEnabled = true;
            this.lstResult.Location = new System.Drawing.Point(67, 85);
            this.lstResult.Name = "lstResult";
            this.lstResult.Size = new System.Drawing.Size(698, 251);
            this.lstResult.TabIndex = 3;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lstResult);
            this.Controls.Add(this.butExtractMeteo);
            this.Controls.Add(this.txtCity);
            this.Controls.Add(this.label1);
            this.Name = "FrmMain";
            this.Text = "Recherche de météo d\'une ville";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.Button butExtractMeteo;
        private System.Windows.Forms.ListBox lstResult;
    }
}

