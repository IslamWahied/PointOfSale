namespace PointOfSaleSedek._105_Reports
{
    partial class frmSaleReports
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
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            this.dtTo = new DevExpress.XtraEditors.DateEdit();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.dtFrom = new DevExpress.XtraEditors.DateEdit();
            this.slkUsers = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.materialLabel3 = new MaterialSkin.Controls.MaterialLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dtTo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFrom.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slkUsers.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.simpleButton1.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.simpleButton1.Appearance.ForeColor = System.Drawing.Color.White;
            this.simpleButton1.Appearance.Options.UseBackColor = true;
            this.simpleButton1.Appearance.Options.UseForeColor = true;
            this.simpleButton1.Location = new System.Drawing.Point(101, 196);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(105, 38);
            this.simpleButton1.TabIndex = 148;
            this.simpleButton1.Text = "عرض";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // materialLabel2
            // 
            this.materialLabel2.AutoSize = true;
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel2.Location = new System.Drawing.Point(30, 133);
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(56, 19);
            this.materialLabel2.TabIndex = 156;
            this.materialLabel2.Text = "الي تاريخ";
            // 
            // dtTo
            // 
            this.dtTo.EditValue = null;
            this.dtTo.Location = new System.Drawing.Point(27, 155);
            this.dtTo.Name = "dtTo";
            this.dtTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtTo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtTo.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Fluent;
            this.dtTo.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dtTo.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.False;
            this.dtTo.Size = new System.Drawing.Size(251, 20);
            this.dtTo.TabIndex = 157;
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel1.Location = new System.Drawing.Point(30, 76);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(54, 19);
            this.materialLabel1.TabIndex = 154;
            this.materialLabel1.Text = "من تاريخ";
            // 
            // dtFrom
            // 
            this.dtFrom.EditValue = null;
            this.dtFrom.Location = new System.Drawing.Point(27, 98);
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtFrom.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtFrom.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Fluent;
            this.dtFrom.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dtFrom.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.False;
            this.dtFrom.Size = new System.Drawing.Size(251, 20);
            this.dtFrom.TabIndex = 155;
            // 
            // slkUsers
            // 
            this.slkUsers.EditValue = " ";
            this.slkUsers.Location = new System.Drawing.Point(27, 31);
            this.slkUsers.Name = "slkUsers";
            this.slkUsers.Properties.Appearance.Options.UseTextOptions = true;
            this.slkUsers.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.slkUsers.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.slkUsers.Properties.AutoHeight = false;
            this.slkUsers.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.slkUsers.Properties.NullText = " ";
            this.slkUsers.Properties.PopupView = this.searchLookUpEdit1View;
            this.slkUsers.Properties.EditValueChanged += new System.EventHandler(this.slkShiftsOpen_Properties_EditValueChanged);
            this.slkUsers.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.slkUsers.Size = new System.Drawing.Size(251, 25);
            this.slkUsers.TabIndex = 158;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn7});
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn7
            // 
            this.gridColumn7.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn7.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn7.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn7.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn7.Caption = "الاسم";
            this.gridColumn7.FieldName = "UserName";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 0;
            // 
            // materialLabel3
            // 
            this.materialLabel3.AutoSize = true;
            this.materialLabel3.Depth = 0;
            this.materialLabel3.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel3.Location = new System.Drawing.Point(30, 9);
            this.materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel3.Name = "materialLabel3";
            this.materialLabel3.Size = new System.Drawing.Size(75, 19);
            this.materialLabel3.TabIndex = 159;
            this.materialLabel3.Text = "اسم المستخدم";
            // 
            // frmSaleReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(290, 246);
            this.Controls.Add(this.materialLabel3);
            this.Controls.Add(this.slkUsers);
            this.Controls.Add(this.materialLabel2);
            this.Controls.Add(this.dtTo);
            this.Controls.Add(this.materialLabel1);
            this.Controls.Add(this.dtFrom);
            this.Controls.Add(this.simpleButton1);
            this.IconOptions.ShowIcon = false;
            this.LookAndFeel.SkinName = "The Bezier";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSaleReports";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "فاتورة مبيعات";
            this.Load += new System.EventHandler(this.frmSaleReports_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtTo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFrom.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slkUsers.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private DevExpress.XtraEditors.DateEdit dtTo;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private DevExpress.XtraEditors.DateEdit dtFrom;
        private DevExpress.XtraEditors.SearchLookUpEdit slkUsers;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private MaterialSkin.Controls.MaterialLabel materialLabel3;
    }
}