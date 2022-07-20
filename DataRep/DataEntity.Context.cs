﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataRep
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class SaleEntities : DbContext
    {
        public SaleEntities()
            : base("name=SaleEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer_Info> Customer_Info { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Expens> Expenses { get; set; }
        public virtual DbSet<ExpensesTransaction> ExpensesTransactions { get; set; }
        public virtual DbSet<Item_History> Item_History { get; set; }
        public virtual DbSet<Item_History_transaction> Item_History_transaction { get; set; }
        public virtual DbSet<ItemCard> ItemCards { get; set; }
        public virtual DbSet<Jop> Jops { get; set; }
        public virtual DbSet<SaleDetail> SaleDetails { get; set; }
        public virtual DbSet<SaleMaster> SaleMasters { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<SexType> SexTypes { get; set; }
        public virtual DbSet<Shift> Shifts { get; set; }
        public virtual DbSet<Tab_Info> Tab_Info { get; set; }
        public virtual DbSet<UnitCard> UnitCards { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<User_Auth> User_Auth { get; set; }
        public virtual DbSet<UserEvent> UserEvents { get; set; }
        public virtual DbSet<Operation_Type> Operation_Type { get; set; }
        public virtual DbSet<Payment_Type> Payment_Type { get; set; }
        public virtual DbSet<Auth_View> Auth_View { get; set; }
        public virtual DbSet<Customer_View> Customer_View { get; set; }
        public virtual DbSet<Employee_View> Employee_View { get; set; }
        public virtual DbSet<ExpensesView> ExpensesViews { get; set; }
        public virtual DbSet<ItemCardView> ItemCardViews { get; set; }
        public virtual DbSet<SaleDetailView> SaleDetailViews { get; set; }
        public virtual DbSet<SaleMasterView> SaleMasterViews { get; set; }
        public virtual DbSet<Shift_View> Shift_View { get; set; }
        public virtual DbSet<User_View> User_View { get; set; }
    
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
