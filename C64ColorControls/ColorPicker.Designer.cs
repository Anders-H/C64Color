﻿namespace C64ColorControls
{
    partial class ColorPicker
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
            this.SuspendLayout();
            // 
            // ColorPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.DoubleBuffered = true;
            this.Name = "ColorPicker";
            this.Size = new System.Drawing.Size(341, 161);
            this.Load += new System.EventHandler(this.ColorPicker_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ColorPicker_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ColorPicker_MouseClick);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ColorPicker_MouseDoubleClick);
            this.Resize += new System.EventHandler(this.ColorPicker_Resize);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
