using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using PointOfSaleSedek._102_MaterialSkin;
using DataRep;
using PointOfSaleSedek.HelperClass;
using System.Drawing;

namespace PointOfSaleSedek._101_Adds
{
    public partial class frmItemDeletedHistory : DevExpress.XtraEditors.XtraForm
    {
        readonly POSEntity context = new POSEntity();
        readonly Static st = new Static();
        public frmItemDeletedHistory()
        {
            InitializeComponent();
            FillGrid();
          
            langu();
        }


        void langu()
        {


            gridColumn1.Caption = st.isEnglish() ? "Code" : "التسلسل";
            gridColumn7.Caption = st.isEnglish() ? "Category" : "المجموعه";
            gridColumn10.Caption = st.isEnglish() ? "BarCode" : "الباركود";
            gridColumn2.Caption = st.isEnglish() ? "Arabic Name" : "الاسم بالعربية";
            gridColumn13.Caption = st.isEnglish() ? "English Name" : "الاسم بالانجليزية";
            gridColumn3.Caption = st.isEnglish() ? "Unit" : "وحدة القياس";
            gridColumn4.Caption = st.isEnglish() ? "Selling Price" : "سعر البيع";
            gridColumn12.Caption = st.isEnglish() ? "Danger Limit" : "حد الخطر";
            gvCafeItemCard.GroupPanelText = st.isEnglish() ? "Drag the field here to collect" : "اسحب الحقل هنا للتجميع";
            this.RightToLeft = st.isEnglish() ? RightToLeft.No : RightToLeft.Yes;
            tableLayoutPanel2.RightToLeft = st.isEnglish() ? RightToLeft.Yes : RightToLeft.No;
       

           
            this.gridColumn4.Caption = st.isEnglish() ? "Created by" : "البائع";

            this.groupControl1.CustomHeaderButtons[0].Properties.Caption = st.isEnglish() ? "Recovery" : "استرجاع";
            
            materialContextMenuStrip1.Items[0].Text = st.isEnglish() ? "Recovery" : "استرجاع";



        }
        void FillGrid()
        {
            var brachCode = st.GetBranch_Code();
            var result = (from a in context.ItemCardViews where a.IsDeleted == 1 && a.Branch_Code == brachCode select a).ToList();
            gcItemCard.DataSource = result;
            CheckGridDataCount();
        }

        public void CheckGridDataCount()
        {

            if (gvCafeItemCard.RowCount <= 0)
            {

                gcItemCard.Enabled = false;
            

            }
            else
            {

                gcItemCard.Enabled = true;
              


            }

        }


        void RecaveryAction() {
            var brachCode = st.GetBranch_Code();
            try
            {
                if (Application.OpenForms.OfType<frmCafeItemCard>().Any())
                {
                    if (gvCafeItemCard.RowCount <= 0)
                    {

                        MaterialMessageBox.Show(st.isEnglish() ? "There are no Data for Recovery" : "!لا يوجد بيانات ليتم استرجاعها", MessageBoxButtons.OK);
                        return;


                    }

                    frmCafeItemCard frm = (frmCafeItemCard)Application.OpenForms["frmCafeItemCard"];


                    var FocusRow = gvCafeItemCard.GetFocusedRow() as ItemCardView;

                    List<ItemCard> listItemCards = context.ItemCards.Where(x=>x.Branch_Code == brachCode).ToList();


                    //parCode
                    // name 
                    // itemCode
                    bool isdubleCated = listItemCards.Any(x =>
                    x.IsDeleted == 0
                    &&
                    (

                    x.ItemCode == FocusRow.ItemCode ||

                    x.ParCode.ToLower() == FocusRow.ParCode.ToLower() ||

                    x.Name.ToLower() == FocusRow.Name.ToLower() ||
                    x.Name_En.ToLower() == FocusRow.Name_En.ToLower()


                    ));


                    if (isdubleCated)
                    {
                        MessageBox.Show(st.isEnglish() ? "Item has already been registered" : "تم  تسجيل الصنف  من قبل");
                    }
                    else
                    {

                        ItemCard _ItemCard = new ItemCard();
                        _ItemCard = listItemCards.SingleOrDefault(item => item.ItemCode == FocusRow.ItemCode);
                        _ItemCard.IsDeleted = 0;
                        context.SaveChanges();
                        FillGrid();
                        frm.fillgrid();

                    }







                }
            }
            catch { 
            }
          
        }

        private void groupControl1_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            var Warehouse_Code = st.Get_Warehouse_Code();
            var Branch_Code = st.GetBranch_Code();
            if (e.Button.Properties.Caption == "استرجاع" || e.Button.Properties.Caption == "Recovery")
            {
                RecaveryAction();

            }
          

            
        }

        private void frmInvoiceSearch_Load(object sender, EventArgs e)
        {
            if (gvCafeItemCard.RowCount <= 0)
            {

                groupControl1.Enabled = false;


            }
        }

        private void الغاءالفاتورةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RecaveryAction();
        }
    }
}