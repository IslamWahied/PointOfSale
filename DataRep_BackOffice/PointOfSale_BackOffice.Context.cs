﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataRep_BackOffice
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;


    public partial class PointOfSaleBackOfficeEntities : DbContext
    {
        public PointOfSaleBackOfficeEntities()
            : base("name=PointOfSaleBackOfficeEntities")
        {
        }
    
       
    
        public DbSet<Back_Office_Transaction> Back_Office_Transaction { get; set; }
        public DbSet<Branch_Back_Office> Branch_Back_Office { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Category_Back_Office> Category_Back_Office { get; set; }
        public DbSet<Customer_Info> Customer_Info { get; set; }
        public DbSet<Customer_Info_Back_Office> Customer_Info_Back_Office { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Employee_Back_Office> Employee_Back_Office { get; set; }
        public DbSet<Event_Type> Event_Type { get; set; }
        public DbSet<Event_Type_Back_Office> Event_Type_Back_Office { get; set; }
        public DbSet<Expens> Expenses { get; set; }
        public DbSet<Expenses_Back_Office> Expenses_Back_Office { get; set; }
        public DbSet<ExpensesTransaction> ExpensesTransactions { get; set; }
        public DbSet<ExpensesTransaction_Back_Office> ExpensesTransaction_Back_Office { get; set; }
        public DbSet<Item_History> Item_History { get; set; }
        public DbSet<Item_History_Back_Office> Item_History_Back_Office { get; set; }
        public DbSet<Item_History_transaction> Item_History_transaction { get; set; }
        public DbSet<Item_History_transaction_Back_Office> Item_History_transaction_Back_Office { get; set; }
        public DbSet<ItemCard> ItemCards { get; set; }
        public DbSet<ItemCard_Back_Office> ItemCard_Back_Office { get; set; }
        public DbSet<Jop> Jops { get; set; }
        public DbSet<Jops_Back_Office> Jops_Back_Office { get; set; }
        public DbSet<Operation_Type> Operation_Type { get; set; }
        public DbSet<Payment_Type> Payment_Type { get; set; }
        public DbSet<Project_Tables> Project_Tables { get; set; }
        public DbSet<Project_Tables_Back_Office> Project_Tables_Back_Office { get; set; }
        public DbSet<ProjectMangerInfo> ProjectMangerInfoes { get; set; }
        public DbSet<ProjectMangerInfo_Back_Office> ProjectMangerInfo_Back_Office { get; set; }
        public DbSet<SaleDetail> SaleDetails { get; set; }
        public DbSet<SaleDetail_Back_Office> SaleDetail_Back_Office { get; set; }
        public DbSet<SaleMaster> SaleMasters { get; set; }
        public DbSet<SaleMaster_Back_Office> SaleMaster_Back_Office { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Sales_Back_Office> Sales_Back_Office { get; set; }
        public DbSet<SexType> SexTypes { get; set; }
        public DbSet<SexType_Back_Office> SexType_Back_Office { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Shift_Back_Office> Shift_Back_Office { get; set; }
        public DbSet<Tab_Info> Tab_Info { get; set; }
        public DbSet<Tab_Info_Back_Office> Tab_Info_Back_Office { get; set; }
        public DbSet<UnitCard> UnitCards { get; set; }
        public DbSet<UnitCard_Back_Office> UnitCard_Back_Office { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<User_Auth> User_Auth { get; set; }
        public DbSet<User_Auth_Back_Office> User_Auth_Back_Office { get; set; }
        public DbSet<User_Back_Office> User_Back_Office { get; set; }
        public DbSet<UserEvent> UserEvents { get; set; }
        public DbSet<UserEvents_Back_Office> UserEvents_Back_Office { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Warehouse_Back_Office> Warehouse_Back_Office { get; set; }
        public DbSet<Warehouse_Transaction> Warehouse_Transaction { get; set; }
        public DbSet<Warehouse_Transaction_Back_Office> Warehouse_Transaction_Back_Office { get; set; }
        public DbSet<Operation_Type_Back_Office> Operation_Type_Back_Office { get; set; }
        public DbSet<Payment_Type_Back_Office> Payment_Type_Back_Office { get; set; }
        public DbSet<ProjectInfo> ProjectInfoes { get; set; }
        public DbSet<ProjectInfo_Back_Office> ProjectInfo_Back_Office { get; set; }
        public DbSet<Auth_View> Auth_View { get; set; }
        public DbSet<Customer_View> Customer_View { get; set; }
        public DbSet<Employee_View> Employee_View { get; set; }
        public DbSet<ExpensesView> ExpensesViews { get; set; }
        public DbSet<ItemCardView> ItemCardViews { get; set; }
        public DbSet<SaleDetailView> SaleDetailViews { get; set; }
        public DbSet<SaleMasterView> SaleMasterViews { get; set; }
        public DbSet<Shift_View> Shift_View { get; set; }
        public DbSet<User_View> User_View { get; set; }
        public DbSet<warhouse_view> warhouse_view { get; set; }
    
        public virtual int Delete_Trancation_By_User_Code(Nullable<long> user_Code, ObjectParameter message)
        {
            var user_CodeParameter = user_Code.HasValue ?
                new ObjectParameter("User_Code", user_Code) :
                new ObjectParameter("User_Code", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Delete_Trancation_By_User_Code", user_CodeParameter, message);
        }
    
        public virtual int TruncateTables()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("TruncateTables");
        }
    }
}
