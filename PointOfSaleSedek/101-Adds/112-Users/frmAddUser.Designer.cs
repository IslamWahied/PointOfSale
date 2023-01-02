
namespace PointOfSaleSedek._101_Adds._112_Users
{
    partial class frmAddUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddUser));
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.materialLabel4 = new MaterialSkin.Controls.MaterialLabel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.txtBranch = new DevExpress.XtraEditors.TextEdit();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.txtUserName = new DevExpress.XtraEditors.TextEdit();
            this.materialLabel11 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel13 = new MaterialSkin.Controls.MaterialLabel();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtPassword = new DevExpress.XtraEditors.TextEdit();
            this.chkAddUser = new DevExpress.XtraEditors.CheckEdit();
            this.slkEmp = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBranch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAddUser.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slkEmp.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75.20892F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.79108F));
            this.tableLayoutPanel2.Controls.Add(this.materialLabel4, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.txtBranch, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.materialLabel1, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.txtUserName, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.materialLabel11, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.materialLabel13, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.labelControl6, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.txtPassword, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.chkAddUser, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.slkEmp, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tableLayoutPanel2.RowCount = 6;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 0F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 144F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(359, 220);
            this.tableLayoutPanel2.TabIndex = 225;
            // 
            // materialLabel4
            // 
            this.materialLabel4.AutoSize = true;
            this.materialLabel4.Depth = 0;
            this.materialLabel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialLabel4.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel4.Location = new System.Drawing.Point(3, 33);
            this.materialLabel4.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel4.Name = "materialLabel4";
            this.materialLabel4.Size = new System.Drawing.Size(83, 1);
            this.materialLabel4.TabIndex = 208;
            this.materialLabel4.Text = "الفرع";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.97403F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.02597F));
            this.tableLayoutPanel3.Controls.Add(this.btnAdd, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.btnCancel, 1, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(92, 143);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.07752F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 89.92248F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(264, 53);
            this.tableLayoutPanel3.TabIndex = 12;
            // 
            // btnAdd
            // 
            this.btnAdd.AllowFocus = false;
            this.btnAdd.Appearance.BackColor = System.Drawing.Color.Blue;
            this.btnAdd.Appearance.Options.UseBackColor = true;
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.ImageOptions.Image")));
            this.btnAdd.Location = new System.Drawing.Point(133, 8);
            this.btnAdd.LookAndFeel.SkinMaskColor = System.Drawing.Color.Blue;
            this.btnAdd.LookAndFeel.SkinMaskColor2 = System.Drawing.Color.Blue;
            this.btnAdd.LookAndFeel.SkinName = "The Bezier";
            this.btnAdd.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(128, 42);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "اضافة";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AllowFocus = false;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.ImageOptions.Image")));
            this.btnCancel.Location = new System.Drawing.Point(3, 8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(124, 42);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "اغلاق";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtBranch
            // 
            this.txtBranch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBranch.Enabled = false;
            this.txtBranch.Location = new System.Drawing.Point(92, 36);
            this.txtBranch.Name = "txtBranch";
            this.txtBranch.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtBranch.Properties.Appearance.Options.UseBackColor = true;
            this.txtBranch.Properties.AutoHeight = false;
            this.txtBranch.Properties.Mask.EditMask = "\\d{0,12}";
            this.txtBranch.Properties.Mask.IgnoreMaskBlank = false;
            this.txtBranch.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtBranch.Properties.Mask.ShowPlaceHolders = false;
            this.txtBranch.Properties.ReadOnly = true;
            this.txtBranch.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtBranch.Size = new System.Drawing.Size(264, 1);
            this.txtBranch.TabIndex = 200;
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel1.Location = new System.Drawing.Point(3, 33);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(83, 32);
            this.materialLabel1.TabIndex = 207;
            this.materialLabel1.Text = "كلمة المرور";
            this.materialLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtUserName
            // 
            this.txtUserName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtUserName.Location = new System.Drawing.Point(92, 36);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Properties.Appearance.Options.UseTextOptions = true;
            this.txtUserName.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtUserName.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.txtUserName.Properties.AutoHeight = false;
            this.txtUserName.Properties.Mask.AutoComplete = DevExpress.XtraEditors.Mask.AutoCompleteType.Optimistic;
            this.txtUserName.Properties.Mask.EditMask = "(.|\\s){0,50}";
            this.txtUserName.Properties.Mask.IgnoreMaskBlank = false;
            this.txtUserName.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtUserName.Properties.Mask.ShowPlaceHolders = false;
            this.txtUserName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtUserName.Size = new System.Drawing.Size(264, 26);
            this.txtUserName.TabIndex = 201;
            // 
            // materialLabel11
            // 
            this.materialLabel11.AutoSize = true;
            this.materialLabel11.Depth = 0;
            this.materialLabel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialLabel11.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel11.Location = new System.Drawing.Point(3, 0);
            this.materialLabel11.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel11.Name = "materialLabel11";
            this.materialLabel11.Size = new System.Drawing.Size(83, 33);
            this.materialLabel11.TabIndex = 224;
            this.materialLabel11.Text = "اسم الموظف";
            this.materialLabel11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // materialLabel13
            // 
            this.materialLabel13.AutoSize = true;
            this.materialLabel13.Depth = 0;
            this.materialLabel13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialLabel13.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel13.Location = new System.Drawing.Point(3, 65);
            this.materialLabel13.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel13.Name = "materialLabel13";
            this.materialLabel13.Size = new System.Drawing.Size(83, 36);
            this.materialLabel13.TabIndex = 229;
            this.materialLabel13.Text = "الباسورد";
            this.materialLabel13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Appearance.Options.UseTextOptions = true;
            this.labelControl6.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl6.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl6.Location = new System.Drawing.Point(92, 104);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(264, 33);
            this.labelControl6.TabIndex = 232;
            this.labelControl6.Text = "تفعيل كمستخدم للبرنامج";
            this.labelControl6.Click += new System.EventHandler(this.labelControl6_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPassword.Location = new System.Drawing.Point(92, 68);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Properties.Appearance.Options.UseTextOptions = true;
            this.txtPassword.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtPassword.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.txtPassword.Properties.AutoHeight = false;
            this.txtPassword.Properties.Mask.AutoComplete = DevExpress.XtraEditors.Mask.AutoCompleteType.Optimistic;
            this.txtPassword.Properties.Mask.EditMask = "(.|\\s){0,50}";
            this.txtPassword.Properties.Mask.IgnoreMaskBlank = false;
            this.txtPassword.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtPassword.Properties.Mask.ShowPlaceHolders = false;
            this.txtPassword.Properties.UseSystemPasswordChar = true;
            this.txtPassword.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtPassword.Size = new System.Drawing.Size(264, 30);
            this.txtPassword.TabIndex = 233;
            // 
            // chkAddUser
            // 
            this.chkAddUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkAddUser.Location = new System.Drawing.Point(3, 104);
            this.chkAddUser.Name = "chkAddUser";
            this.chkAddUser.Properties.AllowFocused = false;
            this.chkAddUser.Properties.Appearance.Options.UseTextOptions = true;
            this.chkAddUser.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.chkAddUser.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.chkAddUser.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.chkAddUser.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.chkAddUser.Properties.AppearanceDisabled.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.chkAddUser.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.chkAddUser.Properties.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.chkAddUser.Properties.AppearanceFocused.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.chkAddUser.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.chkAddUser.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.chkAddUser.Properties.AppearanceReadOnly.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.chkAddUser.Properties.Caption = "";
            this.chkAddUser.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.chkAddUser.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.chkAddUser.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkAddUser.Size = new System.Drawing.Size(83, 33);
            this.chkAddUser.TabIndex = 231;
            // 
            // slkEmp
            // 
            this.slkEmp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.slkEmp.EditValue = " ";
            this.slkEmp.Location = new System.Drawing.Point(92, 3);
            this.slkEmp.Name = "slkEmp";
            this.slkEmp.Properties.Appearance.Options.UseTextOptions = true;
            this.slkEmp.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.slkEmp.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.slkEmp.Properties.AutoHeight = false;
            this.slkEmp.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.slkEmp.Properties.NullText = " ";
            this.slkEmp.Properties.PopupView = this.searchLookUpEdit1View;
            this.slkEmp.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.slkEmp.Size = new System.Drawing.Size(264, 27);
            this.slkEmp.TabIndex = 234;
            this.slkEmp.EditValueChanged += new System.EventHandler(this.slkEmp_EditValueChanged);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1});
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn1.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "الاسم";
            this.gridColumn1.FieldName = "Employee_Name";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // frmAddUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 220);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel2);
            this.IconOptions.ShowIcon = false;
            this.LookAndFeel.SkinName = "DevExpress Style";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddUser";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "اضافة مستخدم جديد";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmAddUser_Load);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtBranch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAddUser.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slkEmp.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        public DevExpress.XtraEditors.SimpleButton btnAdd;
        public DevExpress.XtraEditors.SimpleButton btnCancel;
        public DevExpress.XtraEditors.TextEdit txtBranch;
        public DevExpress.XtraEditors.TextEdit txtUserName;
        public DevExpress.XtraEditors.TextEdit txtPassword;
        public DevExpress.XtraEditors.CheckEdit chkAddUser;
        public System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        public MaterialSkin.Controls.MaterialLabel materialLabel4;
        public System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        public MaterialSkin.Controls.MaterialLabel materialLabel1;
        public MaterialSkin.Controls.MaterialLabel materialLabel11;
        public MaterialSkin.Controls.MaterialLabel materialLabel13;
        public DevExpress.XtraEditors.LabelControl labelControl6;
        public DevExpress.XtraEditors.SearchLookUpEdit slkEmp;
        public DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        public DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
    }
}