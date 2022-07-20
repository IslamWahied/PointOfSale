namespace PointOfSaleSedek._101_Adds
{
    partial class FrmAuthentication
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
            this.slkUser = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.materialLabel4 = new MaterialSkin.Controls.MaterialLabel();
            this.BtnClose = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.materialDivider1 = new MaterialSkin.Controls.MaterialDivider();
            this.GridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.slkUser.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            this.SuspendLayout();
            // 
            // slkUser
            // 
            this.slkUser.EditValue = " ";
            this.slkUser.Location = new System.Drawing.Point(592, 92);
            this.slkUser.Name = "slkUser";
            this.slkUser.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.slkUser.Properties.NullText = " ";
            this.slkUser.Properties.PopupView = this.searchLookUpEdit1View;
            this.slkUser.Size = new System.Drawing.Size(193, 20);
            this.slkUser.TabIndex = 0;
            this.slkUser.EditValueChanged += new System.EventHandler(this.SlkUser_EditValueChanged);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2});
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn2.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.Caption = "الاسم";
            this.gridColumn2.FieldName = "Employee_Name";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            // 
            // materialLabel4
            // 
            this.materialLabel4.AutoSize = true;
            this.materialLabel4.Depth = 0;
            this.materialLabel4.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel4.Location = new System.Drawing.Point(706, 70);
            this.materialLabel4.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel4.Name = "materialLabel4";
            this.materialLabel4.Size = new System.Drawing.Size(75, 19);
            this.materialLabel4.TabIndex = 186;
            this.materialLabel4.Text = "اسم المستخدم";
            // 
            // BtnClose
            // 
            this.BtnClose.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BtnClose.Appearance.Options.UseBackColor = true;
            this.BtnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnClose.Location = new System.Drawing.Point(601, 429);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(89, 37);
            this.BtnClose.TabIndex = 2;
            this.BtnClose.Text = "اغلاق";
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BtnSave.Appearance.Options.UseBackColor = true;
            this.BtnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnSave.Location = new System.Drawing.Point(696, 429);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(89, 37);
            this.BtnSave.TabIndex = 1;
            this.BtnSave.Text = "حفظ";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // materialDivider1
            // 
            this.materialDivider1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialDivider1.Depth = 0;
            this.materialDivider1.Location = new System.Drawing.Point(137, 422);
            this.materialDivider1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialDivider1.Name = "materialDivider1";
            this.materialDivider1.Size = new System.Drawing.Size(491, 1);
            this.materialDivider1.TabIndex = 194;
            this.materialDivider1.Text = "materialDivider1";
            // 
            // GridControl1
            // 
            this.GridControl1.EmbeddedNavigator.Appearance.BackColor = System.Drawing.Color.LightGray;
            this.GridControl1.EmbeddedNavigator.Appearance.Options.UseBackColor = true;
            this.GridControl1.Location = new System.Drawing.Point(15, 121);
            this.GridControl1.MainView = this.gvMain;
            this.GridControl1.Name = "GridControl1";
            this.GridControl1.Size = new System.Drawing.Size(773, 302);
            this.GridControl1.TabIndex = 195;
            this.GridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // gvMain
            // 
            this.gvMain.Appearance.Empty.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.gvMain.Appearance.Empty.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.gvMain.Appearance.Empty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.gvMain.Appearance.Empty.Options.UseBorderColor = true;
            this.gvMain.Appearance.Empty.Options.UseForeColor = true;
            this.gvMain.AppearancePrint.Preview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.gvMain.AppearancePrint.Preview.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.gvMain.AppearancePrint.Preview.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.gvMain.AppearancePrint.Preview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.gvMain.AppearancePrint.Preview.Options.UseBackColor = true;
            this.gvMain.AppearancePrint.Preview.Options.UseBorderColor = true;
            this.gvMain.AppearancePrint.Preview.Options.UseForeColor = true;
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn3,
            this.gridColumn1,
            this.gridColumn4});
            this.gvMain.GridControl = this.GridControl1;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsSelection.CheckBoxSelectorColumnWidth = 25;
            this.gvMain.OptionsSelection.CheckBoxSelectorField = "InActive";
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gvMain.OptionsSelection.UseIndicatorForSelection = false;
            this.gvMain.OptionsView.RowAutoHeight = true;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn3.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.Caption = "اسم الصلاحية";
            this.gridColumn3.FieldName = "Tab_Description";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "كود الصلحية";
            this.gridColumn1.FieldName = "Tab_Code";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn4.AppearanceHeader.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridColumn4.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.Caption = "حالة الصلاحية";
            this.gridColumn4.FieldName = "InActive";
            this.gridColumn4.Name = "gridColumn4";
            // 
            // FrmAuthentication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 475);
            this.ControlBox = false;
            this.Controls.Add(this.GridControl1);
            this.Controls.Add(this.materialDivider1);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.materialLabel4);
            this.Controls.Add(this.slkUser);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAuthentication";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "صلاحيات المستخدمين";
            ((System.ComponentModel.ISupportInitialize)(this.slkUser.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SearchLookUpEdit slkUser;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private MaterialSkin.Controls.MaterialLabel materialLabel4;
        private DevExpress.XtraEditors.SimpleButton BtnClose;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private MaterialSkin.Controls.MaterialDivider materialDivider1;
        private DevExpress.XtraGrid.GridControl GridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMain;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
    }
}