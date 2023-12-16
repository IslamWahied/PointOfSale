﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BackOfficeEntity
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity.Infrastructure;
  
    using System.Linq;
    
    public partial class db_a8f74e_posEntities : DbContext
    {
        public db_a8f74e_posEntities()
            : base("name=db_a8f74e_posEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Back_Office_Transaction_Detail> Back_Office_Transaction_Detail { get; set; }
        public DbSet<Back_Office_Transaction_Master> Back_Office_Transaction_Master { get; set; }
        public DbSet<Branch_Back_Office> Branch_Back_Office { get; set; }
        public DbSet<Branches> Branches { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Category_Back_Office> Category_Back_Office { get; set; }
        public DbSet<Customer_Info> Customer_Info { get; set; }
        public DbSet<Customer_Info_Back_Office> Customer_Info_Back_Office { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Employee_Back_Office> Employee_Back_Office { get; set; }
        public DbSet<Event_Type> Event_Type { get; set; }
        public DbSet<Event_Type_Back_Office> Event_Type_Back_Office { get; set; }
        public DbSet<Expenses> Expenses { get; set; }
        public DbSet<Expenses_Back_Office> Expenses_Back_Office { get; set; }
        public DbSet<ExpensesTransaction> ExpensesTransaction { get; set; }
        public DbSet<ExpensesTransaction_Back_Office> ExpensesTransaction_Back_Office { get; set; }
        public DbSet<Item_Balance> Item_Balance { get; set; }
        public DbSet<Item_History> Item_History { get; set; }
        public DbSet<Item_History_Back_Office> Item_History_Back_Office { get; set; }
        public DbSet<Item_History_transaction> Item_History_transaction { get; set; }
        public DbSet<Item_History_transaction_Back_Office> Item_History_transaction_Back_Office { get; set; }
        public DbSet<ItemCard> ItemCard { get; set; }
        public DbSet<ItemCard_Back_Office> ItemCard_Back_Office { get; set; }
        public DbSet<Jops> Jops { get; set; }
        public DbSet<Jops_Back_Office> Jops_Back_Office { get; set; }
        public DbSet<Operation_Type> Operation_Type { get; set; }
        public DbSet<Payment_Type> Payment_Type { get; set; }
        public DbSet<PO> PO { get; set; }
        public DbSet<Project_Tables> Project_Tables { get; set; }
        public DbSet<Project_Tables_Back_Office> Project_Tables_Back_Office { get; set; }
        public DbSet<ProjectMangerInfo> ProjectMangerInfo { get; set; }
        public DbSet<ProjectMangerInfo_Back_Office> ProjectMangerInfo_Back_Office { get; set; }
        public DbSet<SaleDetail> SaleDetail { get; set; }
        public DbSet<SaleDetail_Back_Office> SaleDetail_Back_Office { get; set; }
        public DbSet<SaleMaster> SaleMaster { get; set; }
        public DbSet<SaleMaster_Back_Office> SaleMaster_Back_Office { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public DbSet<Sales_Back_Office> Sales_Back_Office { get; set; }
        public DbSet<SexType> SexType { get; set; }
        public DbSet<SexType_Back_Office> SexType_Back_Office { get; set; }
        public DbSet<Shift> Shift { get; set; }
        public DbSet<Shift_Back_Office> Shift_Back_Office { get; set; }
        public DbSet<States> States { get; set; }
        public DbSet<Tab_Info> Tab_Info { get; set; }
        public DbSet<Tab_Info_Back_Office> Tab_Info_Back_Office { get; set; }
        public DbSet<UnitCard> UnitCard { get; set; }
        public DbSet<UnitCard_Back_Office> UnitCard_Back_Office { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<User_Auth> User_Auth { get; set; }
        public DbSet<User_Auth_Back_Office> User_Auth_Back_Office { get; set; }
        public DbSet<User_Back_Office> User_Back_Office { get; set; }
        public DbSet<UserEvents> UserEvents { get; set; }
        public DbSet<UserEvents_Back_Office> UserEvents_Back_Office { get; set; }
        public DbSet<Warehouse> Warehouse { get; set; }
        public DbSet<Warehouse_Back_Office> Warehouse_Back_Office { get; set; }
        public DbSet<Warehouse_Transaction> Warehouse_Transaction { get; set; }
        public DbSet<Warehouse_Transaction_Back_Office> Warehouse_Transaction_Back_Office { get; set; }
        public DbSet<Operation_Type_Back_Office> Operation_Type_Back_Office { get; set; }
        public DbSet<Payment_Type_Back_Office> Payment_Type_Back_Office { get; set; }
        public DbSet<ProjectInfo> ProjectInfo { get; set; }
        public DbSet<ProjectInfo_Back_Office> ProjectInfo_Back_Office { get; set; }
        public DbSet<Auth_View> Auth_View { get; set; }
        public DbSet<BackOfficeTranDetail_View> BackOfficeTranDetail_View { get; set; }
        public DbSet<Customer_View> Customer_View { get; set; }
        public DbSet<Employee_View> Employee_View { get; set; }
        public DbSet<ExpensesView> ExpensesView { get; set; }
        public DbSet<ItemCardView> ItemCardView { get; set; }
        public DbSet<PO_View> PO_View { get; set; }
        public DbSet<SaleDetailView> SaleDetailView { get; set; }
        public DbSet<SaleMasterView> SaleMasterView { get; set; }
        public DbSet<Shift_View> Shift_View { get; set; }
        public DbSet<User_View> User_View { get; set; }
        public DbSet<warhouse_view> warhouse_view { get; set; }
    
        public virtual int Delete_Trancation_By_User_Code(Nullable<long> user_Code, Nullable<long> branch_Code, ObjectParameter message)
        {
            var user_CodeParameter = user_Code.HasValue ?
                new ObjectParameter("User_Code", user_Code) :
                new ObjectParameter("User_Code", typeof(long));
    
            var branch_CodeParameter = branch_Code.HasValue ?
                new ObjectParameter("Branch_Code", branch_Code) :
                new ObjectParameter("Branch_Code", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Delete_Trancation_By_User_Code", user_CodeParameter, branch_CodeParameter, message);
        }
    
        public virtual int TruncateTables()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("TruncateTables");
        }
    }
}
