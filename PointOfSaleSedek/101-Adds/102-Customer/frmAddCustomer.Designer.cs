
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
            this.txtJop = new System.Windows.Forms.RichTextBox();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.materialLabel6 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel10 = new MaterialSkin.Controls.MaterialLabel();
            this.txtEmpCode = new DevExpress.XtraEditors.TextEdit();
            this.materialLabel11 = new MaterialSkin.Controls.MaterialLabel();
            this.txtEmpMob1 = new DevExpress.XtraEditors.TextEdit();
            this.materialLabel13 = new MaterialSkin.Controls.MaterialLabel();
            this.slkSex = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.TxtEmpName = new DevExpress.XtraEditors.TextEdit();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.TxtEmpNote = new System.Windows.Forms.RichTextBox();
            this.materialLabel5 = new MaterialSkin.Controls.MaterialLabel();
            this.TxtEmpAddress = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.TxtEmpEmail = new DevExpress.XtraEditors.TextEdit();
            this.materialLabel3 = new MaterialSkin.Controls.MaterialLabel();
            this.TxtEmpNataionalId = new DevExpress.XtraEditors.TextEdit();
            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel7 = new MaterialSkin.Controls.MaterialLabel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmpCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmpMob1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slkSex.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtEmpName.Properties)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TxtEmpEmail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtEmpNataionalId.Properties)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtJop
            // 
            this.txtJop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtJop.Location = new System.Drawing.Point(3, 209);
            this.txtJop.MaxLength = 100;
            this.txtJop.Name = "txtJop";
            this.txtJop.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtJop.Size = new System.Drawing.Size(307, 26);
            this.txtJop.TabIndex = 231;
            this.txtJop.Text = "";
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
            // materialLabel6
            // 
            this.materialLabel6.AutoSize = true;
            this.materialLabel6.Depth = 0;
            this.materialLabel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.materialLabel6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel6.Location = new System.Drawing.Point(316, 273);
            this.materialLabel6.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel6.Name = "materialLabel6";
            this.materialLabel6.Size = new System.Drawing.Size(98, 41);
            this.materialLabel6.TabIndex = 213;
            this.materialLabel6.Text = "ملاحظات";
            // 
            // materialLabel10
            // 
            this.materialLabel10.AutoSize = true;
            this.materialLabel10.Depth = 0;
            this.materialLabel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialLabel10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.materialLabel10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel10.Location = new System.Drawing.Point(316, 0);
            this.materialLabel10.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel10.Name = "materialLabel10";
            this.materialLabel10.Size = new System.Drawing.Size(98, 28);
            this.materialLabel10.TabIndex = 220;
            this.materialLabel10.Text = "كود الموظف";
            // 
            // txtEmpCode
            // 
            this.txtEmpCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEmpCode.EditValue = "";
            this.txtEmpCode.Enabled = false;
            this.txtEmpCode.Location = new System.Drawing.Point(3, 3);
            this.txtEmpCode.Name = "txtEmpCode";
            this.txtEmpCode.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtEmpCode.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmpCode.Properties.Appearance.ForeColor = System.Drawing.Color.DodgerBlue;
            this.txtEmpCode.Properties.Appearance.Options.UseBackColor = true;
            this.txtEmpCode.Properties.Appearance.Options.UseFont = true;
            this.txtEmpCode.Properties.Appearance.Options.UseForeColor = true;
            this.txtEmpCode.Properties.Appearance.Options.UseTextOptions = true;
            this.txtEmpCode.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtEmpCode.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.txtEmpCode.Properties.AutoHeight = false;
            this.txtEmpCode.Properties.Mask.EditMask = "\\d{0,100}";
            this.txtEmpCode.Properties.Mask.IgnoreMaskBlank = false;
            this.txtEmpCode.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtEmpCode.Properties.Mask.ShowPlaceHolders = false;
            this.txtEmpCode.Properties.ReadOnly = true;
            this.txtEmpCode.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtEmpCode.Size = new System.Drawing.Size(307, 22);
            this.txtEmpCode.TabIndex = 221;
            // 
            // materialLabel11
            // 
            this.materialLabel11.AutoSize = true;
            this.materialLabel11.Depth = 0;
            this.materialLabel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialLabel11.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.materialLabel11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel11.Location = new System.Drawing.Point(316, 28);
            this.materialLabel11.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel11.Name = "materialLabel11";
            this.materialLabel11.Size = new System.Drawing.Size(98, 36);
            this.materialLabel11.TabIndex = 224;
            this.materialLabel11.Text = "موبيل 1";
            // 
            // txtEmpMob1
            // 
            this.txtEmpMob1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEmpMob1.Location = new System.Drawing.Point(3, 31);
            this.txtEmpMob1.Name = "txtEmpMob1";
            this.txtEmpMob1.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtEmpMob1.Properties.Appearance.Options.UseBackColor = true;
            this.txtEmpMob1.Properties.AutoHeight = false;
            this.txtEmpMob1.Properties.Mask.EditMask = "\\d{0,12}";
            this.txtEmpMob1.Properties.Mask.IgnoreMaskBlank = false;
            this.txtEmpMob1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtEmpMob1.Properties.Mask.ShowPlaceHolders = false;
            this.txtEmpMob1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtEmpMob1.Size = new System.Drawing.Size(307, 30);
            this.txtEmpMob1.TabIndex = 225;
            // 
            // materialLabel13
            // 
            this.materialLabel13.AutoSize = true;
            this.materialLabel13.Depth = 0;
            this.materialLabel13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialLabel13.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.materialLabel13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel13.Location = new System.Drawing.Point(316, 96);
            this.materialLabel13.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel13.Name = "materialLabel13";
            this.materialLabel13.Size = new System.Drawing.Size(98, 40);
            this.materialLabel13.TabIndex = 229;
            this.materialLabel13.Text = "الجنس";
            // 
            // slkSex
            // 
            this.slkSex.Dock = System.Windows.Forms.DockStyle.Fill;
            this.slkSex.EditValue = " ";
            this.slkSex.Location = new System.Drawing.Point(3, 99);
            this.slkSex.Name = "slkSex";
            this.slkSex.Properties.AutoHeight = false;
            this.slkSex.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.slkSex.Properties.NullText = " ";
            this.slkSex.Properties.PopupView = this.gridView2;
            this.slkSex.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.slkSex.Size = new System.Drawing.Size(307, 34);
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
            this.btnAdd.Location = new System.Drawing.Point(158, 11);
            this.btnAdd.LookAndFeel.SkinMaskColor = System.Drawing.Color.Blue;
            this.btnAdd.LookAndFeel.SkinMaskColor2 = System.Drawing.Color.Blue;
            this.btnAdd.LookAndFeel.SkinName = "The Bezier";
            this.btnAdd.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(146, 61);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "اضافة";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // TxtEmpName
            // 
            this.TxtEmpName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtEmpName.Location = new System.Drawing.Point(3, 67);
            this.TxtEmpName.Name = "TxtEmpName";
            this.TxtEmpName.Properties.AutoHeight = false;
            this.TxtEmpName.Properties.Mask.AutoComplete = DevExpress.XtraEditors.Mask.AutoCompleteType.Optimistic;
            this.TxtEmpName.Properties.Mask.EditMask = "(.|\\s){0,50}";
            this.TxtEmpName.Properties.Mask.IgnoreMaskBlank = false;
            this.TxtEmpName.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.TxtEmpName.Properties.Mask.ShowPlaceHolders = false;
            this.TxtEmpName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.TxtEmpName.Size = new System.Drawing.Size(307, 26);
            this.TxtEmpName.TabIndex = 201;
            // 
            // btnCancel
            // 
            this.btnCancel.AllowFocus = false;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.ImageOptions.Image")));
            this.btnCancel.Location = new System.Drawing.Point(3, 11);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(149, 61);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "اغلاق";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // TxtEmpNote
            // 
            this.TxtEmpNote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtEmpNote.Location = new System.Drawing.Point(3, 276);
            this.TxtEmpNote.MaxLength = 100;
            this.TxtEmpNote.Name = "TxtEmpNote";
            this.TxtEmpNote.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.TxtEmpNote.Size = new System.Drawing.Size(307, 35);
            this.TxtEmpNote.TabIndex = 212;
            this.TxtEmpNote.Text = "";
            // 
            // materialLabel5
            // 
            this.materialLabel5.AutoSize = true;
            this.materialLabel5.Depth = 0;
            this.materialLabel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.materialLabel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel5.Location = new System.Drawing.Point(316, 238);
            this.materialLabel5.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel5.Name = "materialLabel5";
            this.materialLabel5.Size = new System.Drawing.Size(98, 35);
            this.materialLabel5.TabIndex = 209;
            this.materialLabel5.Text = "العنوان";
            // 
            // TxtEmpAddress
            // 
            this.TxtEmpAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtEmpAddress.Location = new System.Drawing.Point(3, 241);
            this.TxtEmpAddress.MaxLength = 100;
            this.TxtEmpAddress.Name = "TxtEmpAddress";
            this.TxtEmpAddress.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.TxtEmpAddress.Size = new System.Drawing.Size(307, 29);
            this.TxtEmpAddress.TabIndex = 204;
            this.TxtEmpAddress.Text = "";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.81818F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.18182F));
            this.tableLayoutPanel3.Controls.Add(this.btnAdd, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.btnCancel, 1, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 317);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.76471F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88.23529F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(307, 75);
            this.tableLayoutPanel3.TabIndex = 12;
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.materialLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel1.Location = new System.Drawing.Point(316, 64);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(98, 32);
            this.materialLabel1.TabIndex = 207;
            this.materialLabel1.Text = "الاسم ";
            // 
            // TxtEmpEmail
            // 
            this.TxtEmpEmail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtEmpEmail.Location = new System.Drawing.Point(3, 173);
            this.TxtEmpEmail.Name = "TxtEmpEmail";
            this.TxtEmpEmail.Properties.AutoHeight = false;
            this.TxtEmpEmail.Properties.Mask.EditMask = "(.|\\s){0,50}";
            this.TxtEmpEmail.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.TxtEmpEmail.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.TxtEmpEmail.Size = new System.Drawing.Size(307, 30);
            this.TxtEmpEmail.TabIndex = 202;
            // 
            // materialLabel3
            // 
            this.materialLabel3.AutoSize = true;
            this.materialLabel3.Depth = 0;
            this.materialLabel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.materialLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel3.Location = new System.Drawing.Point(316, 170);
            this.materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel3.Name = "materialLabel3";
            this.materialLabel3.Size = new System.Drawing.Size(98, 36);
            this.materialLabel3.TabIndex = 206;
            this.materialLabel3.Text = "الايميل";
            // 
            // TxtEmpNataionalId
            // 
            this.TxtEmpNataionalId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtEmpNataionalId.Location = new System.Drawing.Point(3, 139);
            this.TxtEmpNataionalId.Name = "TxtEmpNataionalId";
            this.TxtEmpNataionalId.Properties.AutoHeight = false;
            this.TxtEmpNataionalId.Properties.Mask.EditMask = "\\d{0,50}";
            this.TxtEmpNataionalId.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.TxtEmpNataionalId.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.TxtEmpNataionalId.Size = new System.Drawing.Size(307, 28);
            this.TxtEmpNataionalId.TabIndex = 203;
            // 
            // materialLabel2
            // 
            this.materialLabel2.AutoSize = true;
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.materialLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel2.Location = new System.Drawing.Point(316, 136);
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(98, 34);
            this.materialLabel2.TabIndex = 205;
            this.materialLabel2.Text = "الرقم القومي";
            // 
            // materialLabel7
            // 
            this.materialLabel7.AutoSize = true;
            this.materialLabel7.Depth = 0;
            this.materialLabel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.materialLabel7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel7.Location = new System.Drawing.Point(316, 206);
            this.materialLabel7.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel7.Name = "materialLabel7";
            this.materialLabel7.Size = new System.Drawing.Size(98, 32);
            this.materialLabel7.TabIndex = 211;
            this.materialLabel7.Text = "الوظيفة";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75.20892F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.79108F));
            this.tableLayoutPanel2.Controls.Add(this.TxtEmpNote, 0, 8);
            this.tableLayoutPanel2.Controls.Add(this.materialLabel5, 1, 7);
            this.tableLayoutPanel2.Controls.Add(this.TxtEmpAddress, 0, 7);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 9);
            this.tableLayoutPanel2.Controls.Add(this.materialLabel1, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.TxtEmpEmail, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.materialLabel3, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this.TxtEmpNataionalId, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.materialLabel2, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.TxtEmpName, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.materialLabel6, 1, 8);
            this.tableLayoutPanel2.Controls.Add(this.materialLabel10, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtEmpCode, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.materialLabel11, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.txtEmpMob1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.materialLabel7, 1, 6);
            this.tableLayoutPanel2.Controls.Add(this.materialLabel13, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.slkSex, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.txtJop, 0, 6);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tableLayoutPanel2.RowCount = 10;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.16354F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.82796F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5448F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.18638F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.97849F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 26.11202F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(417, 395);
            this.tableLayoutPanel2.TabIndex = 226;
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
            // frmAddCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 395);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel2);
            this.IconOptions.ShowIcon = false;
            this.Name = "frmAddCustomer";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "اضافة عميل";
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmpCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmpMob1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slkSex.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtEmpName.Properties)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TxtEmpEmail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtEmpNataionalId.Properties)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.RichTextBox txtJop;
        public DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        public DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        public DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        public MaterialSkin.Controls.MaterialLabel materialLabel6;
        public MaterialSkin.Controls.MaterialLabel materialLabel10;
        public DevExpress.XtraEditors.TextEdit txtEmpCode;
        public MaterialSkin.Controls.MaterialLabel materialLabel11;
        public DevExpress.XtraEditors.TextEdit txtEmpMob1;
        public MaterialSkin.Controls.MaterialLabel materialLabel13;
        public DevExpress.XtraEditors.SearchLookUpEdit slkSex;
        public DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        public DevExpress.XtraEditors.SimpleButton btnAdd;
        public DevExpress.XtraEditors.TextEdit TxtEmpName;
        public DevExpress.XtraEditors.SimpleButton btnCancel;
        public System.Windows.Forms.RichTextBox TxtEmpNote;
        public MaterialSkin.Controls.MaterialLabel materialLabel5;
        public System.Windows.Forms.RichTextBox TxtEmpAddress;
        public System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        public MaterialSkin.Controls.MaterialLabel materialLabel1;
        public DevExpress.XtraEditors.TextEdit TxtEmpEmail;
        public MaterialSkin.Controls.MaterialLabel materialLabel3;
        public DevExpress.XtraEditors.TextEdit TxtEmpNataionalId;
        public MaterialSkin.Controls.MaterialLabel materialLabel2;
        public MaterialSkin.Controls.MaterialLabel materialLabel7;
        public System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}