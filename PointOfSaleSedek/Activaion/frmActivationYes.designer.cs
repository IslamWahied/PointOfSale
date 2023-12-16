namespace PointOfSaleSedek._Activaion
{ 
    partial class frmActivationYes
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
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.Message = new MaterialSkin.Controls.MaterialLabel();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnOk.Appearance.Options.UseBackColor = true;
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(124, 166);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(97, 36);
            this.btnOk.TabIndex = 193;
            this.btnOk.Text = "اغلاق";
            // 
            // Message
            // 
            this.Message.Depth = 0;
            this.Message.Font = new System.Drawing.Font("Roboto", 11F);
            this.Message.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Message.Location = new System.Drawing.Point(4, 69);
            this.Message.MouseState = MaterialSkin.MouseState.HOVER;
            this.Message.Name = "Message";
            this.Message.Size = new System.Drawing.Size(354, 91);
            this.Message.TabIndex = 192;
            this.Message.Text = " ";
            this.Message.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // frmYes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 209);
            this.ControlBox = false;
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.Message);
            this.MaximumSize = new System.Drawing.Size(362, 209);
            this.MinimumSize = new System.Drawing.Size(362, 209);
            this.Name = "frmYes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "تاكيد";
            this.Load += new System.EventHandler(this.frmYes_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private MaterialSkin.Controls.MaterialLabel Message;
    }
}