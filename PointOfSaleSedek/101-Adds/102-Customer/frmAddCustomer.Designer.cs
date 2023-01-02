
namespace PointOfSaleSedek._101_Adds._102_Customer
{
    partial class frmAddCustomer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddCustomer));
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.materialLabel11 = new MaterialSkin.Controls.MaterialLabel();
            this.txtEmpMob1 = new DevExpress.XtraEditors.TextEdit();
            this.materialLabel13 = new MaterialSkin.Controls.MaterialLabel();
            this.slkSex = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.TxtEmpName = new DevExpress.XtraEditors.TextEdit();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmpMob1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slkSex.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtEmpName.Properties)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn2.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.Caption = "الكود";
            this.gridColumn2.FieldName = "Customer_Code";
            this.gridColumn2.Name = "gridColumn2";
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn4});
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn4.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.Caption = "الاسم";
            this.gridColumn4.FieldName = "SexTypeName";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 0;
            // 
            // materialLabel11
            // 
            this.materialLabel11.AutoSize = true;
            this.materialLabel11.Depth = 0;
            this.materialLabel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialLabel11.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel11.Location = new System.Drawing.Point(264, 38);
            this.materialLabel11.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel11.Name = "materialLabel11";
            this.materialLabel11.Size = new System.Drawing.Size(81, 39);
            this.materialLabel11.TabIndex = 224;
            this.materialLabel11.Text = "الموبيل";
            this.materialLabel11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtEmpMob1
            // 
            this.txtEmpMob1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEmpMob1.Location = new System.Drawing.Point(3, 41);
            this.txtEmpMob1.Name = "txtEmpMob1";
            this.txtEmpMob1.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtEmpMob1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmpMob1.Properties.Appearance.Options.UseBackColor = true;
            this.txtEmpMob1.Properties.Appearance.Options.UseFont = true;
            this.txtEmpMob1.Properties.Appearance.Options.UseTextOptions = true;
            this.txtEmpMob1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtEmpMob1.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.txtEmpMob1.Properties.AutoHeight = false;
            this.txtEmpMob1.Properties.Mask.EditMask = "\\d{0,50}";
            this.txtEmpMob1.Properties.Mask.IgnoreMaskBlank = false;
            this.txtEmpMob1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtEmpMob1.Properties.Mask.ShowPlaceHolders = false;
            this.txtEmpMob1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtEmpMob1.Size = new System.Drawing.Size(255, 33);
            this.txtEmpMob1.TabIndex = 225;
            // 
            // materialLabel13
            // 
            this.materialLabel13.AutoSize = true;
            this.materialLabel13.Depth = 0;
            this.materialLabel13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialLabel13.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel13.Location = new System.Drawing.Point(264, 77);
            this.materialLabel13.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel13.Name = "materialLabel13";
            this.materialLabel13.Size = new System.Drawing.Size(81, 40);
            this.materialLabel13.TabIndex = 229;
            this.materialLabel13.Text = "الجنس";
            this.materialLabel13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // slkSex
            // 
            this.slkSex.Dock = System.Windows.Forms.DockStyle.Fill;
            this.slkSex.EditValue = " ";
            this.slkSex.Location = new System.Drawing.Point(3, 80);
            this.slkSex.Name = "slkSex";
            this.slkSex.Properties.Appearance.Options.UseTextOptions = true;
            this.slkSex.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.slkSex.Properties.AutoHeight = false;
            this.slkSex.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.slkSex.Properties.NullText = " ";
            this.slkSex.Properties.PopupView = this.gridView2;
            this.slkSex.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.slkSex.Size = new System.Drawing.Size(255, 34);
            this.slkSex.TabIndex = 230;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn5.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.Caption = "الكود";
            this.gridColumn5.FieldName = "Customer_Code";
            this.gridColumn5.Name = "gridColumn5";
            // 
            // btnAdd
            // 
            this.btnAdd.AllowFocus = false;
            this.btnAdd.Appearance.BackColor = System.Drawing.Color.Blue;
            this.btnAdd.Appearance.Options.UseBackColor = true;
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.ImageOptions.Image")));
            this.btnAdd.Location = new System.Drawing.Point(131, 43);
            this.btnAdd.LookAndFeel.SkinMaskColor = System.Drawing.Color.Blue;
            this.btnAdd.LookAndFeel.SkinMaskColor2 = System.Drawing.Color.Blue;
            this.btnAdd.LookAndFeel.SkinName = "The Bezier";
            this.btnAdd.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(121, 42);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "اضافة";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // TxtEmpName
            // 
            this.TxtEmpName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtEmpName.Location = new System.Drawing.Point(3, 3);
            this.TxtEmpName.Name = "TxtEmpName";
            this.TxtEmpName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtEmpName.Properties.Appearance.Options.UseFont = true;
            this.TxtEmpName.Properties.Appearance.Options.UseTextOptions = true;
            this.TxtEmpName.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.TxtEmpName.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.TxtEmpName.Properties.AutoHeight = false;
            this.TxtEmpName.Properties.Mask.AutoComplete = DevExpress.XtraEditors.Mask.AutoCompleteType.Optimistic;
            this.TxtEmpName.Properties.Mask.EditMask = "(.|\\s){0,50}";
            this.TxtEmpName.Properties.Mask.IgnoreMaskBlank = false;
            this.TxtEmpName.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.TxtEmpName.Properties.Mask.ShowPlaceHolders = false;
            this.TxtEmpName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.TxtEmpName.Size = new System.Drawing.Size(255, 32);
            this.TxtEmpName.TabIndex = 201;
            // 
            // btnCancel
            // 
            this.btnCancel.AllowFocus = false;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.ImageOptions.Image")));
            this.btnCancel.Location = new System.Drawing.Point(3, 43);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(122, 42);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "اغلاق";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.81818F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.18182F));
            this.tableLayoutPanel3.Controls.Add(this.btnAdd, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.btnCancel, 1, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 120);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 46.23656F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 53.76344F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(255, 88);
            this.tableLayoutPanel3.TabIndex = 12;
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel1.Location = new System.Drawing.Point(264, 0);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(81, 38);
            this.materialLabel1.TabIndex = 207;
            this.materialLabel1.Text = "الاسم ";
            this.materialLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75.20892F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.79108F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.materialLabel13, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.slkSex, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.txtEmpMob1, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.TxtEmpName, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.materialLabel11, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.materialLabel1, 1, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 29.94869F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70.05132F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(348, 211);
            this.tableLayoutPanel2.TabIndex = 226;
            // 
            // frmAddCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 211);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel2);
            this.IconOptions.ShowIcon = false;
            this.Name = "frmAddCustomer";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "اضافة عميل";
            this.Load += new System.EventHandler(this.frmAddCustomer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmpMob1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slkSex.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtEmpName.Properties)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        public DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        public DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        public DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        public MaterialSkin.Controls.MaterialLabel materialLabel11;
        public DevExpress.XtraEditors.TextEdit txtEmpMob1;
        public MaterialSkin.Controls.MaterialLabel materialLabel13;
        public DevExpress.XtraEditors.SearchLookUpEdit slkSex;
        public DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        public DevExpress.XtraEditors.SimpleButton btnAdd;
        public DevExpress.XtraEditors.TextEdit TxtEmpName;
        public DevExpress.XtraEditors.SimpleButton btnCancel;
        public System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        public MaterialSkin.Controls.MaterialLabel materialLabel1;
        public System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}