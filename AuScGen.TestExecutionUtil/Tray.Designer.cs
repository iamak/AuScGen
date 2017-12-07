namespace AuScGen.TestExecutionUtil
{
    partial class Tray
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
            this.txtStepMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtStepMessage
            // 
            this.txtStepMessage.AutoSize = true;
            this.txtStepMessage.BackColor = System.Drawing.Color.Black;
            this.txtStepMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStepMessage.ForeColor = System.Drawing.Color.Lime;
            this.txtStepMessage.Location = new System.Drawing.Point(12, 3);
            this.txtStepMessage.Name = "txtStepMessage";
            this.txtStepMessage.Size = new System.Drawing.Size(87, 13);
            this.txtStepMessage.TabIndex = 1;
            this.txtStepMessage.Text = "Step Message";
            // 
            // Tray
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(125, 19);
            this.Controls.Add(this.txtStepMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Tray";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tray";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label txtStepMessage;
    }
}