namespace WindowsFormsApp1
{
    partial class FormSaving
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
               this.components = new System.ComponentModel.Container();
               this.label1 = new System.Windows.Forms.Label();
               this.savingName = new System.Windows.Forms.TextBox();
               this.btSave = new System.Windows.Forms.Button();
               this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
               this.SuspendLayout();
               // 
               // label1
               // 
               this.label1.Dock = System.Windows.Forms.DockStyle.Top;
               this.label1.Location = new System.Drawing.Point(0, 0);
               this.label1.Name = "label1";
               this.label1.Size = new System.Drawing.Size(473, 80);
               this.label1.TabIndex = 0;
               this.label1.Text = "Input Your Name";
               this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
               // 
               // savingName
               // 
               this.savingName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
               this.savingName.Location = new System.Drawing.Point(36, 74);
               this.savingName.MaxLength = 10;
               this.savingName.Name = "savingName";
               this.savingName.Size = new System.Drawing.Size(402, 33);
               this.savingName.TabIndex = 1;
               this.savingName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
               this.toolTip1.SetToolTip(this.savingName, "NAME NOT NULL !");
               // 
               // btSave
               // 
               this.btSave.Cursor = System.Windows.Forms.Cursors.Hand;
               this.btSave.Location = new System.Drawing.Point(177, 129);
               this.btSave.Name = "btSave";
               this.btSave.Size = new System.Drawing.Size(109, 35);
               this.btSave.TabIndex = 2;
               this.btSave.Text = "Save";
               this.toolTip1.SetToolTip(this.btSave, "NAME NOT NULL !");
               this.btSave.UseVisualStyleBackColor = true;
               this.btSave.Click += new System.EventHandler(this.btSave_Click);
               // 
               // toolTip1
               // 
               this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Warning;
               this.toolTip1.ToolTipTitle = "WARNING";
               // 
               // FormSaving
               // 
               this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 25F);
               this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
               this.ClientSize = new System.Drawing.Size(473, 185);
               this.ControlBox = false;
               this.Controls.Add(this.btSave);
               this.Controls.Add(this.savingName);
               this.Controls.Add(this.label1);
               this.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
               this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
               this.Margin = new System.Windows.Forms.Padding(6);
               this.MaximizeBox = false;
               this.MinimizeBox = false;
               this.Name = "FormSaving";
               this.ShowIcon = false;
               this.ShowInTaskbar = false;
               this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
               this.Text = "Saving";
               this.TopMost = true;
               this.ResumeLayout(false);
               this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox savingName;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}