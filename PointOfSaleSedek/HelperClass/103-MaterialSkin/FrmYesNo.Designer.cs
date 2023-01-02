namespace PointOfSaleSedek._102_MaterialSkin
{
    partial class FrmYesNo
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
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.LblMessage = new MaterialSkin.Controls.MaterialLabel();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnClose.Appearance.Options.UseBackColor = true;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(80, 165);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(97, 36);
            this.btnClose.TabIndex = 191;
            this.btnClose.Text = "اغلاق";
            // 
            // btnOk
            // 
            this.btnOk.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnOk.Appearance.Options.UseBackColor = true;
            this.btnOk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(183, 165);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(97, 36);
            this.btnOk.TabIndex = 190;
            this.btnOk.Text = "تاكيد";
            // 
            // LblMessage
            // 
            this.LblMessage.Depth = 0;
            this.LblMessage.Font = new System.Drawing.Font("Roboto", 11F);
            this.LblMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.LblMessage.Location = new System.Drawing.Point(4, 68);
            this.LblMessage.MouseState = MaterialSkin.MouseState.HOVER;
            this.LblMessage.Name = "LblMessage";
            this.LblMessage.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.LblMessage.Size = new System.Drawing.Size(354, 91);
            this.LblMessage.TabIndex = 189;
            this.LblMessage.Text = "سيبسي";
            this.LblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmYesNo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 209);
            this.ControlBox = false;
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.LblMessage);
            this.MaximumSize = new System.Drawing.Size(362, 209);
            this.MinimumSize = new System.Drawing.Size(362, 209);
            this.Name = "FrmYesNo";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "رساله تاكيد ";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmYesNo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private MaterialSkin.Controls.MaterialLabel LblMessage;
    }
}