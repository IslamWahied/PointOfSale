using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using PointOfSaleSedek._102_MaterialSkin;
using DevExpress.XtraGrid;
using DataRep;
using PointOfSaleSedek.HelperClass;

namespace PointOfSaleSedek._101_Adds
{
    public partial class frmCodes : DevExpress.XtraEditors.XtraForm
    {
        readonly POSEntity context = new POSEntity();
        readonly Static st = new Static();
        public frmCodes()
        {
            InitializeComponent();
            langu();
            FillCategorygrid();
            FillUnitegrid();
        }



        void langu()
        {

            this.RightToLeft = st.isEnglish() ? RightToLeft.No : RightToLeft.Yes;
            this.Text = st.isEnglish() ? "Categories" : "الرموز";
            xtraTabPage1.Text = st.isEnglish() ? "Categories" : "المجموعات";
            xtraTabPage2.Text = st.isEnglish() ? "Units" : "الوحدات";
            labelControl1.Text = st.isEnglish() ? "Category Name" : "اسم المجموعة";
            labelControl2.Text = st.isEnglish() ? "Unit Name" : "اسم الوحدة";

            gridColumn1.Caption = st.isEnglish() ? "Category" : "المجموعة";
            gridColumn2.Caption = st.isEnglish() ? "Unit" : "الوحدة";
            colDelete.Caption = st.isEnglish() ? "Delete" : "حذف";
            colDelete1.Caption = st.isEnglish() ? "Delete" : "حذف";
 
            gvCategory.GroupPanelText = st.isEnglish() ? "Drag the field here to collect" : "اسحب الحقل هنا للتجميع";
            gvUnitCard.GroupPanelText = st.isEnglish() ? "Drag the field here to collect" : "اسحب الحقل هنا للتجميع";
 

        }

        public void FillCategorygrid()
        {
            Int64 branchCode = st.GetBranch_Code();
            var result = (from a in context.Categories where a.IsDeleted == 0 && a.Branch_Code == branchCode  select a).ToList();
            gcCategory.DataSource = result;
        }


        public void FillUnitegrid()
        {
            Int64 branchCode = st.GetBranch_Code();
            var result = (from a in context.UnitCards where a.IsDeleted == 0 &&  a.Branch_Code == branchCode select a).ToList();
            gcUnitCard.DataSource = result;
        }
        private void txtCategoryName_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            AddCategory();
        }

        void AddCategory()
        {
            if (string.IsNullOrWhiteSpace(txtCategoryName.Text))
            {

                MaterialMessageBox.Show(st.isEnglish() ? "Please write the name of the Category!" :"!برجاء كتابة اسم المجموعة", MessageBoxButtons.OK);
                return;
            }

            bool TestUpdate = context.Categories.Any(Categorie => Categorie.CategoryName == txtCategoryName.Text);
            if (TestUpdate)
            {


                MaterialMessageBox.Show(st.isEnglish() ? "Previously registered!" :"!تم نسجبلة من قبل", MessageBoxButtons.OK);

            }
            else
            {
                Int64 NewCode = context.Categories.Max(u => (Int64?)u.CategoryCode).GetValueOrDefault();
                if (NewCode == 0)
                {

                    NewCode = 0;


                }
                NewCode += 1;
                Category _Category = new Category()
                {


                    CategoryCode = NewCode,
                    CategoryName = txtCategoryName.Text,
                    Branch_Code = 0,


                };
                context.Categories.Add(_Category);
                context.SaveChanges();
                FillCategorygrid();
                txtCategoryName.ResetText();
                //MaterialMessageBox.Show("تم الحفظ", MessageBoxButtons.OK);

            }

        }

        void AddUnite()
        {
            if (string.IsNullOrWhiteSpace(txtUnitName.Text))
            {

                MaterialMessageBox.Show(st.isEnglish() ? "Please write the unit name!" :"!برجاء كتابة اسم الوحدة", MessageBoxButtons.OK);
                return;
            }

            bool TestUpdate = context.UnitCards.Any(Unit => Unit.UnitName == txtUnitName.Text);
            if (TestUpdate)
            {


                MaterialMessageBox.Show(st.isEnglish() ? "Previously registered!" : "!تم نسجبلة من قبل", MessageBoxButtons.OK);

            }
            else
            {
                Int64? NewCode = context.UnitCards.Max(u => (int?)u.UnitCode);
                if (NewCode == null)
                {

                    NewCode = 0;


                }
                NewCode += 1;
                UnitCard _UnitCard = new UnitCard()
                {


                    UnitCode = Convert.ToInt64(NewCode),
                    UnitName = txtUnitName.Text,
                    Branch_Code = 0


                };
                context.UnitCards.Add(_UnitCard);
                context.SaveChanges();
                FillUnitegrid();
                txtUnitName.ResetText();
                //MaterialMessageBox.Show("تم الحفظ", MessageBoxButtons.OK);

            }

        }



        private void txtCategoryName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                AddCategory();



            }
        }

        private void repDelete_Click(object sender, EventArgs e)
        {
            if (gvCategory.RowCount <= 0)
            {

               MaterialMessageBox.Show(st.isEnglish() ? "No Data To Delete!" :"!لا يوجد بيانات للحذف", MessageBoxButtons.OK);
                return;
            }

            Category xx = gvCategory.GetFocusedRow() as Category;
            Int64 CategoryCode = Convert.ToInt64(xx.CategoryCode);

            bool CheckRelation = context.ItemCardViews.Where(x => x.IsDeleted == 0).Any(x => x.CategoryCode == CategoryCode);
            bool CheckBackOfficeRelation = context.Category_Back_Office.Where(x => x.IsDeleted == 0  && x.Event_Code !=3).Any(x => x.CategoryCode == CategoryCode);

            if (CheckRelation && CheckBackOfficeRelation)
            {

                MaterialMessageBox.Show(st.isEnglish() ? "This group cannot be deleted because there are products on it" :"لا يمكن حذف هذه المجموعة لوجود منتجات عليها", MessageBoxButtons.OK);
                return;

            }

            using (POSEntity Context = new POSEntity())
            {
                Category deptDelete = Context.Categories.Where(x=>x.CategoryCode == CategoryCode).FirstOrDefault();
                Context.Categories.Remove(deptDelete);
                Context.SaveChanges();
            FillCategorygrid();
            }


        }

        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            AddUnite();
        }

        private void txtUnitName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                AddUnite();



            }
        }

        private void repdelete1_Click(object sender, EventArgs e)
        {
            if (gvUnitCard.RowCount <= 0)
            {

                MaterialMessageBox.Show(st.isEnglish() ? "No Data To Delete!" : "!لا يوجد بيانات للحذف", MessageBoxButtons.OK);
                return;
            }

            UnitCard xx = gvUnitCard.GetFocusedRow() as UnitCard;
            Int64 UnitCode = Convert.ToInt64(xx.UnitCode);
            bool CheckRelation = context.ItemCardViews.Where(x => x.IsDeleted == 0).Any(x => x.UnitCode == UnitCode);
            bool CheckBackOfficeRelation = context.UnitCard_Back_Office.Where(x => x.IsDeleted == 0 && x.Event_Code != 3).Any(x => x.UnitCode == UnitCode);

            if (CheckRelation && CheckBackOfficeRelation)
            {

                MaterialMessageBox.Show(st.isEnglish() ? "This group cannot be deleted because there are products on it" : "لا يمكن حذف هذه المجموعة لوجود منتجات عليها", MessageBoxButtons.OK);
                return;

            }
            using (POSEntity Context = new POSEntity())
            {
                UnitCard deptDelete = Context.UnitCards.Where(x=>x.UnitCode== UnitCode).FirstOrDefault();
                Context.UnitCards.Remove(deptDelete);
                Context.SaveChanges();
                FillUnitegrid();
            }
        }

        private void txtUnitName_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                AddUnite();



            }
        }
    }
}