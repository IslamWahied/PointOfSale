
namespace PointOfSaleSedek._101_Adds._110_Sales
{
    partial class frmPerfumCustomerHistory
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.txtHistory = new DevExpress.XtraEditors.TextEdit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtHistory.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.txtHistory);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(371, 197);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // txtHistory
            // 
            this.txtHistory.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.False;
            this.txtHistory.CausesValidation = false;
            this.txtHistory.EditValue = "لايوجد بيانات";
            this.txtHistory.Location = new System.Drawing.Point(0, 3);
            this.txtHistory.Name = "txtHistory";
            this.txtHistory.Properties.AllowFocused = false;
            this.txtHistory.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHistory.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
            this.txtHistory.Properties.Appearance.Options.UseFont = true;
            this.txtHistory.Properties.Appearance.Options.UseForeColor = true;
            this.txtHistory.Properties.Appearance.Options.UseTextOptions = true;
            this.txtHistory.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.txtHistory.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.txtHistory.Properties.AutoHeight = false;
            this.txtHistory.Properties.ReadOnly = true;
            this.txtHistory.ShowToolTips = false;
            this.txtHistory.Size = new System.Drawing.Size(368, 194);
            this.txtHistory.TabIndex = 0;
            // 
            // frmCustomerHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 197);
            this.Controls.Add(this.flowLayoutPanel1);
            this.IconOptions.ShowIcon = false;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(373, 229);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(373, 229);
            this.Name = "frmCustomerHistory";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "تعاملات العميل السابقة";
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtHistory.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        public DevExpress.XtraEditors.TextEdit txtHistory;
    }
}