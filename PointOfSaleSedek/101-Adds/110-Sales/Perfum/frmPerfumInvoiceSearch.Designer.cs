namespace PointOfSaleSedek._101_Adds
{
    partial class frmPerfumInvoiceSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPerfumInvoiceSearch));
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions1 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.gcSaleMaster = new DevExpress.XtraGrid.GridControl();
            this.materialContextMenuStrip1 = new MaterialSkin.Controls.MaterialContextMenuStrip();
            this.الغاءالفاتورةToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gvSaleMaster = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcSaleMaster)).BeginInit();
            this.materialContextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvSaleMaster)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.groupControl1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(834, 357);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.CaptionImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("groupControl1.CaptionImageOptions.Image")));
            this.groupControl1.Controls.Add(this.tableLayoutPanel2);
            buttonImageOptions1.Image = ((System.Drawing.Image)(resources.GetObject("buttonImageOptions1.Image")));
            this.groupControl1.CustomHeaderButtons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("معاينة", true, buttonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, null, -1)});
            this.groupControl1.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(3, 3);
            this.groupControl1.LookAndFeel.SkinName = "The Bezier";
            this.groupControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(828, 351);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "بحث عن فاتورة";
            this.groupControl1.CustomButtonClick += new DevExpress.XtraBars.Docking2010.BaseButtonEventHandler(this.groupControl1_CustomButtonClick);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.gcSaleMaster, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(2, 37);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(824, 312);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // gcSaleMaster
            // 
            this.gcSaleMaster.ContextMenuStrip = this.materialContextMenuStrip1;
            this.gcSaleMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcSaleMaster.Location = new System.Drawing.Point(3, 3);
            this.gcSaleMaster.MainView = this.gvSaleMaster;
            this.gcSaleMaster.Name = "gcSaleMaster";
            this.gcSaleMaster.Size = new System.Drawing.Size(818, 306);
            this.gcSaleMaster.TabIndex = 0;
            this.gcSaleMaster.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvSaleMaster});
            this.gcSaleMaster.Click += new System.EventHandler(this.gcSaleMaster_Click);
            // 
            // materialContextMenuStrip1
            // 
            this.materialContextMenuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialContextMenuStrip1.Depth = 0;
            this.materialContextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.الغاءالفاتورةToolStripMenuItem});
            this.materialContextMenuStrip1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialContextMenuStrip1.Name = "materialContextMenuStrip1";
            this.materialContextMenuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.materialContextMenuStrip1.Size = new System.Drawing.Size(136, 26);
            // 
            // الغاءالفاتورةToolStripMenuItem
            // 
            this.الغاءالفاتورةToolStripMenuItem.Name = "الغاءالفاتورةToolStripMenuItem";
            this.الغاءالفاتورةToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.الغاءالفاتورةToolStripMenuItem.Text = "الغاء الفاتورة";
            this.الغاءالفاتورةToolStripMenuItem.Click += new System.EventHandler(this.الغاءالفاتورةToolStripMenuItem_Click);
            // 
            // gvSaleMaster
            // 
            this.gvSaleMaster.Appearance.FooterPanel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvSaleMaster.Appearance.FooterPanel.Options.UseFont = true;
            this.gvSaleMaster.Appearance.FooterPanel.Options.UseTextOptions = true;
            this.gvSaleMaster.Appearance.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvSaleMaster.Appearance.FooterPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gvSaleMaster.Appearance.GroupRow.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvSaleMaster.Appearance.GroupRow.Options.UseFont = true;
            this.gvSaleMaster.Appearance.GroupRow.Options.UseTextOptions = true;
            this.gvSaleMaster.Appearance.GroupRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvSaleMaster.Appearance.GroupRow.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gvSaleMaster.Appearance.HeaderPanel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvSaleMaster.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvSaleMaster.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvSaleMaster.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvSaleMaster.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gvSaleMaster.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6});
            this.gvSaleMaster.GridControl = this.gcSaleMaster;
            this.gvSaleMaster.Name = "gvSaleMaster";
            this.gvSaleMaster.OptionsCustomization.AllowColumnMoving = false;
            this.gvSaleMaster.OptionsCustomization.AllowColumnResizing = false;
            this.gvSaleMaster.OptionsMenu.EnableColumnMenu = false;
            this.gvSaleMaster.OptionsMenu.EnableFooterMenu = false;
            this.gvSaleMaster.OptionsView.ShowAutoFilterRow = true;
            this.gvSaleMaster.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.ShowAlways;
            this.gvSaleMaster.OptionsView.ShowFooter = true;
            this.gvSaleMaster.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn1.AppearanceHeader.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn1.AppearanceHeader.Options.UseFont = true;
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn1.Caption = "رقم الفاتورة";
            this.gridColumn1.FieldName = "SaleMasterCode";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn1.OptionsFilter.AllowFilter = false;
            this.gridColumn1.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn1.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn2.AppearanceHeader.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn2.AppearanceHeader.Options.UseFont = true;
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn2.Caption = "التاريخ";
            this.gridColumn2.FieldName = "EntryDate";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn2.OptionsFilter.AllowFilter = false;
            this.gridColumn2.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn2.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn3.AppearanceHeader.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn3.AppearanceHeader.Options.UseFont = true;
            this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn3.Caption = "الاجمالي";
            this.gridColumn3.DisplayFormat.FormatString = "N";
            this.gridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn3.FieldName = "FinalTotal";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn3.OptionsFilter.AllowFilter = false;
            this.gridColumn3.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn3.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn3.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FinalTotal", "الاجمالي = {0:N}")});
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn4.AppearanceHeader.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn4.AppearanceHeader.Options.UseFont = true;
            this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn4.Caption = "البائع";
            this.gridColumn4.FieldName = "UserName";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn4.OptionsFilter.AllowFilter = false;
            this.gridColumn4.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn4.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn4.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "Full_Name", "{0}")});
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn5.AppearanceHeader.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn5.AppearanceHeader.Options.UseFont = true;
            this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn5.Caption = "gridColumn5";
            this.gridColumn5.Name = "gridColumn5";
            // 
            // gridColumn6
            // 
            this.gridColumn6.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn6.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn6.AppearanceHeader.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn6.AppearanceHeader.Options.UseFont = true;
            this.gridColumn6.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn6.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn6.Caption = "gridColumn6";
            this.gridColumn6.Name = "gridColumn6";
            // 
            // frmInvoiceSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 357);
            this.Controls.Add(this.tableLayoutPanel1);
            this.IconOptions.ShowIcon = false;
            this.LookAndFeel.SkinName = "The Bezier";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "frmInvoiceSearch";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " ";
            this.Load += new System.EventHandler(this.frmInvoiceSearch_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcSaleMaster)).EndInit();
            this.materialContextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvSaleMaster)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private DevExpress.XtraGrid.GridControl gcSaleMaster;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSaleMaster;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private MaterialSkin.Controls.MaterialContextMenuStrip materialContextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem الغاءالفاتورةToolStripMenuItem;
    }
}