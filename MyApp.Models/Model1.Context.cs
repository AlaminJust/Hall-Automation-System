﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyApp.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class JustHallAtumationEntities : DbContext
    {
        public JustHallAtumationEntities()
            : base("name=JustHallAtumationEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Communication> Communications { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<DepartmentInfo> DepartmentInfoes { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<Meal> Meals { get; set; }
        public virtual DbSet<MealCost> MealCosts { get; set; }
        public virtual DbSet<NoticeBoard> NoticeBoards { get; set; }
        public virtual DbSet<Preference> Preferences { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UsersRole> UsersRoles { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
    }
}
