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


namespace PointOfSaleSedek._101_Adds
{
    public partial class frmCodes : DevExpress.XtraEditors.XtraForm
    {
        readonly SaleEntities context = new SaleEntities();
        public frmCodes()
        {
            InitializeComponent();
            FillCategorygrid();
            FillUnitegrid();
        }

        
        public void FillCategorygrid()
        {
            var result = (from a in context.Categories /*where a.IsDeleted == 0*/ select a).ToList();
            gcCategory.DataSource = result;
        }


        public void FillUnitegrid()
        {
            var result = (from a in context.UnitCards where a.IsDeleted == 0 select a).ToList();
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

                MaterialMessageBox.Show("!برجاء كتابة اسم المجموعة", MessageBoxButtons.OK);
                return;
            }

            bool TestUpdate = context.Categories.Any(Categorie => Categorie.CategoryName == txtCategoryName.Text);
            if (TestUpdate)
            {


                MaterialMessageBox.Show("!تم نسجبلة من قبل", MessageBoxButtons.OK);

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

                MaterialMessageBox.Show("!برجاء كتابة اسم الوحدة", MessageBoxButtons.OK);
                return;
            }

            bool TestUpdate = context.UnitCards.Any(Unit => Unit.UnitName == txtUnitName.Text);
            if (TestUpdate)
            {


                MaterialMessageBox.Show("!تم نسجبلة من قبل", MessageBoxButtons.OK);

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

               MaterialMessageBox.Show("!لا يوجد بيانات للحذف", MessageBoxButtons.OK);
                return;
            }

            Category xx = gvCategory.GetFocusedRow() as Category;
            Int64 CategoryCode = Convert.ToInt64(xx.CategoryCode);

            bool CheckRelation = context.ItemCardViews.Where(x => x.IsDeleted == 0).Any(x => x.CategoryCode == CategoryCode);

            if (CheckRelation)
            {

                MaterialMessageBox.Show("لا يمكن حذف هذه المجموعة لوجود منتجات عليها", MessageBoxButtons.OK);
                return;

            }

            using (SaleEntities Context = new SaleEntities())
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

                MaterialMessageBox.Show("!لا يوجد بيانات للحذف", MessageBoxButtons.OK);
                return;
            }

            UnitCard xx = gvUnitCard.GetFocusedRow() as UnitCard;
            Int64 UnitCode = Convert.ToInt64(xx.UnitCode);
            bool CheckRelation = context.ItemCardViews.Where(x => x.IsDeleted == 0).Any(x => x.UnitCode == UnitCode);

            if (CheckRelation)
            {

                MaterialMessageBox.Show("لا يمكن حذف هذه الوحدة لوجود منتجات عليها", MessageBoxButtons.OK);
                return;

            }
            using (SaleEntities Context = new SaleEntities())
            {
                UnitCard deptDelete = Context.UnitCards.Where(x=>x.UnitCode== UnitCode).FirstOrDefault();
                Context.UnitCards.Remove(deptDelete);
                Context.SaveChanges();
                FillUnitegrid();
            }
        }

        
    }
}