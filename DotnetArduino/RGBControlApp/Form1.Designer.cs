namespace RGBControlApp
{
    partial class Form1
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
            this.trackRed = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.trackGreen = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.trackBlue = new System.Windows.Forms.TrackBar();
            this.lbRed = new System.Windows.Forms.Label();
            this.lbGreen = new System.Windows.Forms.Label();
            this.lbBlue = new System.Windows.Forms.Label();
            this.panelColor = new System.Windows.Forms.Panel();
            this.btnExecute = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackRed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackGreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBlue)).BeginInit();
            this.SuspendLayout();
            // 
            // trackRed
            // 
            this.trackRed.Location = new System.Drawing.Point(23, 47);
            this.trackRed.Maximum = 255;
            this.trackRed.Name = "trackRed";
            this.trackRed.Size = new System.Drawing.Size(318, 56);
            this.trackRed.TabIndex = 0;
            this.trackRed.ValueChanged += new System.EventHandler(this.trackRed_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Red";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Green";
            // 
            // trackGreen
            // 
            this.trackGreen.Location = new System.Drawing.Point(23, 109);
            this.trackGreen.Maximum = 255;
            this.trackGreen.Name = "trackGreen";
            this.trackGreen.Size = new System.Drawing.Size(318, 56);
            this.trackGreen.TabIndex = 2;
            this.trackGreen.ValueChanged += new System.EventHandler(this.trackGreen_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 151);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Blue";
            // 
            // trackBlue
            // 
            this.trackBlue.Location = new System.Drawing.Point(23, 171);
            this.trackBlue.Maximum = 255;
            this.trackBlue.Name = "trackBlue";
            this.trackBlue.Size = new System.Drawing.Size(318, 56);
            this.trackBlue.TabIndex = 4;
            this.trackBlue.ValueChanged += new System.EventHandler(this.trackBlue_ValueChanged);
            // 
            // lbRed
            // 
            this.lbRed.AutoSize = true;
            this.lbRed.Location = new System.Drawing.Point(104, 27);
            this.lbRed.Name = "lbRed";
            this.lbRed.Size = new System.Drawing.Size(16, 17);
            this.lbRed.TabIndex = 6;
            this.lbRed.Text = "0";
            // 
            // lbGreen
            // 
            this.lbGreen.AutoSize = true;
            this.lbGreen.Location = new System.Drawing.Point(104, 89);
            this.lbGreen.Name = "lbGreen";
            this.lbGreen.Size = new System.Drawing.Size(16, 17);
            this.lbGreen.TabIndex = 7;
            this.lbGreen.Text = "0";
            // 
            // lbBlue
            // 
            this.lbBlue.AutoSize = true;
            this.lbBlue.Location = new System.Drawing.Point(104, 151);
            this.lbBlue.Name = "lbBlue";
            this.lbBlue.Size = new System.Drawing.Size(16, 17);
            this.lbBlue.TabIndex = 8;
            this.lbBlue.Text = "0";
            // 
            // panelColor
            // 
            this.panelColor.Location = new System.Drawing.Point(366, 56);
            this.panelColor.Name = "panelColor";
            this.panelColor.Size = new System.Drawing.Size(103, 69);
            this.panelColor.TabIndex = 9;
            // 
            // btnExecute
            // 
            this.btnExecute.Location = new System.Drawing.Point(366, 142);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(103, 37);
            this.btnExecute.TabIndex = 10;
            this.btnExecute.Text = "Execute";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 243);
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.panelColor);
            this.Controls.Add(this.lbBlue);
            this.Controls.Add(this.lbGreen);
            this.Controls.Add(this.lbRed);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.trackBlue);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.trackGreen);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackRed);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "RGB LED Control";            
            ((System.ComponentModel.ISupportInitialize)(this.trackRed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackGreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBlue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trackRed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar trackGreen;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar trackBlue;
        private System.Windows.Forms.Label lbRed;
        private System.Windows.Forms.Label lbGreen;
        private System.Windows.Forms.Label lbBlue;
        private System.Windows.Forms.Panel panelColor;
        private System.Windows.Forms.Button btnExecute;
    }
}

