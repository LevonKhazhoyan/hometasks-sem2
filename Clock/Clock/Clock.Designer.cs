namespace Clock
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
            this.Clock = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Clock)).BeginInit();
            this.SuspendLayout();
            // 
            // Clock
            // 
            this.Clock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Clock.Location = new System.Drawing.Point(0, 0);
            this.Clock.Name = "Clock";
            this.Clock.Size = new System.Drawing.Size(800, 450);
            this.Clock.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Clock.TabIndex = 0;
            this.Clock.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Clock);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Clock";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Clock)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.PictureBox Clock;

        #endregion
    }
}