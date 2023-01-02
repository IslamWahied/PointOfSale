
using BackOfficeEntity;


using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;
using PointOfSaleSedek._102_MaterialSkin;
using PointOfSaleSedek.HelperClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace PointOfSaleSedek._101_Adds._111_Warehouse
{
    public partial class Category_Back_office : DevExpress.XtraEditors.XtraForm
    {

      
          readonly db_a8f74e_posEntities _server = new db_a8f74e_posEntities();
     

        Static st = new Static();
        public Category_Back_office()
        {
            InitializeComponent();
              langu();
            FillFromGride();
           FillslkToWarhouse();

        }

         
        void langu()
        {

            this.RightToLeft = st.isEnglish() ? RightToLeft.No : RightToLeft.Yes;

 
  
            gvCategory.GroupPanelText = st.isEnglish() ? "Drag the field here to collect" : "اسحب الحقل هنا للتجميع";
            gvCategory.GroupPanelText = st.isEnglish() ? "Drag the field here to collect" : "اسحب الحقل هنا للتجميع";

            windowsUIButtonPanel.Buttons[0].Properties.Caption = st.isEnglish() ? "Save" : "حفظ";
         
 
            windowsUIButtonPanel.Buttons[6].Properties.Caption = st.isEnglish() ? "Exit" : "خروج";
            
            
            materialContextMenuStrip2.Items[0].Text = st.isEnglish() ? "Delete" : "حذف";
            label50.Text = st.isEnglish() ? "Back Office" : "من";
            label2.Text = st.isEnglish() ? "To" : "الي";
           
            gridColumn28.Caption = st.isEnglish() ? "Name" : "الاسم";

            gridColumn1.Caption = st.isEnglish() ? "Category Name" : "اسم المجموعة";
            gridColumn2.Caption = st.isEnglish() ? "Category Name" : "اسم المجموعة";
          


        }


        public void FillslkToWarhouse()
        {
              DataRep.POSEntity context = new DataRep.POSEntity();
            DataTable dt = new DataTable();
            var result = context.Branches.Where(Categories => Categories.IsDeleted == 0 && Categories.Branches_Code != 0 && Categories.Is_Back_Office == false).ToList();
            slkToWarhouse.Properties.DataSource = result;
            slkToWarhouse.Properties.ValueMember = "Branches_Code";
            slkToWarhouse.Properties.DisplayMember = "Branches_Name";

        }

      


        

       

      

        List<Category_Back_Office> _list_Category_Back_Office = new List<Category_Back_Office>();

       

        
        public void FillFromGride()
        {
            List<Category> _ItemCardView = new List<Category>();

            using (DataRep.POSEntity _context23 = new DataRep.POSEntity()) {
                var GetAllCategory = _context23.Categories.Where(x => x.IsDeleted == 0 && x.Branch_Code == 0).ToList();
              
                gcCategory.DataSource = GetAllCategory;
                gcCategory.RefreshDataSource();
            }
              

        }



   


 
        private void windowsUIButtonPanel_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(WaitForm1));
            try
            {
                WindowsUIButton btn = e.Button as WindowsUIButton;
                if (btn.Caption == "حفظ" || btn.Caption == "Save")
                {


                    // Get BranchCode
                    Int64 toBranchCode = Convert.ToInt64(slkToWarhouse.EditValue);



                    // Vaidate
                    if (gvToCategory.RowCount == 0)
                    {

                        SplashScreenManager.CloseForm();
                        MaterialMessageBox.Show(st.isEnglish() ? "There are no Data" : "لا يوجد ", MessageBoxButtons.OK);
                        return;
                    }
                    else if (String.IsNullOrWhiteSpace(slkToWarhouse.Text))
                    {

                        SplashScreenManager.CloseForm();
                        MaterialMessageBox.Show(st.isEnglish() ? "Please select the store you are transferring to" : "برجاء اختيار المخزن المحول اليه", MessageBoxButtons.OK);
                        return;
                    }

                    Int64 MaxMasterCode = 0;
                    // Get MasterCode
                    using (db_a8f74e_posEntities Contexts2 = new db_a8f74e_posEntities())
                    {
                        MaxMasterCode = Convert.ToInt64(Contexts2.Back_Office_Transaction_Master.Max(u => (Int64?)u.Master_Code + 1));
                        if (MaxMasterCode == 0)
                        {
                            MaxMasterCode = 1;
                        }

                    }






                    // Save trans Back Office Master
                    using (db_a8f74e_posEntities Contexts2 = new db_a8f74e_posEntities())
                    {
                        Back_Office_Transaction_Master _Back_Office_Transaction_Master = new Back_Office_Transaction_Master()
                        {
                            Created_By = st.GetUser_Code(),
                            Created_Date = DateTime.Now,
                            IsDeleted = 0,

                            Master_Code = MaxMasterCode,
                            To_Branch_Code = toBranchCode,

                        };
                        Contexts2.Back_Office_Transaction_Master.Add(_Back_Office_Transaction_Master);
                        Contexts2.SaveChanges();
                        Contexts2.Dispose();
                    }


                    _list_Category_Back_Office.Where(x => x.IsWindow_Transfer).ToList().ForEach(x => {

                        Int64 MaxDetailCode = 0;
                        // Get DetailCode
                        using (db_a8f74e_posEntities Contexts2 = new db_a8f74e_posEntities())
                        {
                            MaxDetailCode = Convert.ToInt64(Contexts2.Back_Office_Transaction_Detail.Max(u => (Int64?)u.Detail_Code + 1));
                            if (MaxDetailCode == 0)
                            {
                                MaxDetailCode = 1;
                            }

                        }

                        using (db_a8f74e_posEntities Contexts2 = new db_a8f74e_posEntities())
                        {
                            // 1- Delete By Update Is Deleted 
                            //2- Insert New Line  If Not Event Delete



                            // 1
                            if (x.Event_Code != 1)
                            {

                                Category_Back_Office category_Back_office = Contexts2.Category_Back_Office.FirstOrDefault(cat => cat.CategoryCode == x.CategoryCode && cat.IsDeleted == 0 && cat.Branch_Code == toBranchCode);
                                category_Back_office.IsDeleted = 1;
                                category_Back_office.IsWindow_Transfer = false;
                                Contexts2.SaveChanges();


                            }
                            //2 

                            Category_Back_Office _cat = new Category_Back_Office()
                            {
                                Branch_Code = x.Branch_Code,
                                CategoryCode = x.CategoryCode,
                                CategoryName = x.CategoryName,
                                Event_Code = x.Event_Code,

                                IsWindow_Transfer = false,
                                Back_Office_Master_Code = MaxMasterCode,
                                Back_Office_Detail_Code = MaxDetailCode == 0 ? 1 : MaxDetailCode,
                                IsDeleted = x.Event_Code == 3 ? 1 : 0
                            };
                            Contexts2.Category_Back_Office.Add(_cat);
                            Contexts2.SaveChanges();





                            Back_Office_Transaction_Detail _Back_Office_Transaction = new Back_Office_Transaction_Detail()
                            {
                                Brach_Code = toBranchCode,
                                Event_Code = x.Event_Code,
                                Fail_Take_Update_Reason = st.isEnglish() ? "Waiting" : "تحت الانتظار",
                                Is_Brach_Take_Update = false,
                                Table_Code = 29,
                                User_Id = st.GetUser_Code(),

                                Created_Date = DateTime.Now,
                                Back_Office_Master_Code = MaxMasterCode,
                                Detail_Code = MaxDetailCode,
                                IsDeleted = 0,

                            };
                            Contexts2.Back_Office_Transaction_Detail.Add(_Back_Office_Transaction);
                            Contexts2.SaveChanges();






                        }




                    });

                    fillToBranchGrid();

                    SplashScreenManager.CloseForm();
                    MaterialMessageBox.Show(st.isEnglish() ? "Converted successfully" : "تم التحويل بنجاح", MessageBoxButtons.OK);
                    return;


                }

                else if (btn.Caption == "خروج" || btn.Caption == "Exit")
                {

                    this.Close();


                }
            }
            catch {

                SplashScreenManager.CloseForm();
            }
           
         
            SplashScreenManager.CloseForm() ;
        }

        private void gvCategory_RowClick(object sender, RowClickEventArgs e)
        {
            try
            {
                var FocusRow = gvCategory.GetFocusedRow() as DataRep.Category;

                labelControl1.Text = FocusRow.CategoryName;
            }
            catch {
                labelControl1.Text = "";

            }
            
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {





            var FocusRow = gvCategory.GetFocusedRow() as DataRep.Category;
            if (String.IsNullOrWhiteSpace(slkToWarhouse.Text))
            {


                MaterialMessageBox.Show(st.isEnglish() ? "Please select the Branch you are transferring to" : "برجاء اختيار المخزن المحول اليه", MessageBoxButtons.OK);
                return;
            }
            if (String.IsNullOrWhiteSpace(labelControl1.Text)) {


                MaterialMessageBox.Show(st.isEnglish() ? "There are no Data To Add" : "لا يوجد بيانات للاضافة ", MessageBoxButtons.OK);
                return;
            }
                if (FocusRow != null)
             {



                _list_Category_Back_Office = _list_Category_Back_Office.Where(x => x.CategoryCode != FocusRow.CategoryCode).ToList();
                Int64 toBranchCode = Convert.ToInt64(slkToWarhouse.EditValue);
                var isFind = _server.Category_Back_Office.Any(xx => xx.Branch_Code == toBranchCode && xx.IsDeleted == 0 && xx.CategoryCode == FocusRow.CategoryCode);



                var model = new Category_Back_Office() { 
                
                Branch_Code = toBranchCode,
                CategoryCode = FocusRow.CategoryCode,
                CategoryName = FocusRow.CategoryName,
                IsDeleted = 0,
                
                Event_Code = isFind?2 : 1,
                Back_Office_Master_Code = 0,
                IsWindow_Transfer = true
                 
                };


                _list_Category_Back_Office.Add(model);

                gcToCategory.DataSource = _list_Category_Back_Office.ToList();
                gcToCategory.RefreshDataSource();

             
                   labelControl1.Text = "";
             
                 
            }
            else {

               MaterialMessageBox.Show(st.isEnglish() ? "There are no Data" : "لا يوجد ", MessageBoxButtons.OK);
               return;

            }




        }



       void fillToBranchGrid()
        {

            
                if (!String.IsNullOrWhiteSpace(slkToWarhouse.Text))
                {
                    Int64 toBranchCode = Convert.ToInt64(slkToWarhouse.EditValue);
                using (db_a8f74e_posEntities Contexts2 = new db_a8f74e_posEntities())
                {
                    _list_Category_Back_Office = Contexts2.Category_Back_Office.Where(x => x.IsDeleted == 0 && x.Branch_Code == toBranchCode).ToList();
                }
                  
                    gcToCategory.DataSource = _list_Category_Back_Office;
                    gcToCategory.RefreshDataSource();
                }
                else
                {
                    labelControl1.Text = "";
                    _list_Category_Back_Office = new List<Category_Back_Office>();
                    gcToCategory.DataSource = _list_Category_Back_Office;
                    gcToCategory.RefreshDataSource();
                }

        }
        private void slkToWarhouse_EditValueChanged(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(WaitForm1));
            try
            {
                _list_Category_Back_Office = new List<Category_Back_Office>();

                gcToCategory.DataSource = _list_Category_Back_Office.Where(x => x.IsDeleted == 0);
                gcToCategory.RefreshDataSource();
                fillToBranchGrid();
            }
            catch { 
            
            }
            SplashScreenManager.CloseForm();

        }

        private void حذفمنالفرعToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var FocusRow = gvToCategory.GetFocusedRow() as Category_Back_Office;



            if (FocusRow != null)
            {
                Int64 toBranchCode = Convert.ToInt64(slkToWarhouse.EditValue);
                var isFind = _server.Category_Back_Office.Any(xx => xx.Branch_Code == toBranchCode && xx.IsDeleted == 0 && xx.CategoryCode == FocusRow.CategoryCode);

                if (isFind)
                {
                    _list_Category_Back_Office.FirstOrDefault(x => x.CategoryCode == FocusRow.CategoryCode).IsDeleted = 0;
                    _list_Category_Back_Office.FirstOrDefault(x => x.CategoryCode == FocusRow.CategoryCode).IsWindow_Transfer = true;
                    _list_Category_Back_Office.FirstOrDefault(x => x.CategoryCode == FocusRow.CategoryCode).Event_Code = 3;
                    gcToCategory.DataSource = _list_Category_Back_Office;
                    gcToCategory.RefreshDataSource();
                }
                else
                {

                    MaterialMessageBox.Show(st.isEnglish() ? "This item already exists in this branch" : "هذا العنصر موجود بالفعل بهذا الفرع", MessageBoxButtons.OK);
                    return;

                }

            }



        }

        private void gvToCategory_RowStyle(object sender, RowStyleEventArgs e)
        {
            int IsDeleted = Convert.ToInt16(gvToCategory.GetRowCellValue(e.RowHandle, "IsDeleted"));
            int Event_Code = Convert.ToInt16(gvToCategory.GetRowCellValue(e.RowHandle, "Event_Code"));
            bool isWndowTransfer = Convert.ToBoolean(gvToCategory.GetRowCellValue(e.RowHandle, "IsWindow_Transfer"));


            if (isWndowTransfer && Event_Code == 3)
            {
                e.Appearance.BackColor = Color.Red;
            }
            else if (isWndowTransfer && Event_Code == 1)
            {
                e.Appearance.BackColor = Color.LightGreen;
            }
            else if (isWndowTransfer && Event_Code ==2)
            {
                e.Appearance.BackColor = Color.Yellow;

            }



                e.HighPriority = true;
        }

        private void toolStripMenuItem1_Click_1(object sender, EventArgs e)
        {


            var FocusRow = gvToCategory.GetFocusedRow() as Category_Back_Office;

            if (FocusRow != null && FocusRow.IsWindow_Transfer)
            {

                if (FocusRow.Event_Code == 2)
                {
                    _list_Category_Back_Office.FirstOrDefault(x => x.CategoryCode == FocusRow.CategoryCode).IsDeleted = 0;
                    _list_Category_Back_Office.FirstOrDefault(x => x.CategoryCode == FocusRow.CategoryCode).IsWindow_Transfer = false;
                    _list_Category_Back_Office.FirstOrDefault(x => x.CategoryCode == FocusRow.CategoryCode).Event_Code = 0;
                    gcToCategory.DataSource = _list_Category_Back_Office;
                    gcToCategory.RefreshDataSource();
                }
                else {

                    _list_Category_Back_Office = _list_Category_Back_Office.Where(x => x.CategoryCode != FocusRow.CategoryCode).ToList();
                    gvToCategory.DeleteSelectedRows();
                    gcToCategory.RefreshDataSource();
                }
                
            }
          
        }
    }
}