﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace techtalk_revised.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class techtalkEntities : DbContext
    {
        public techtalkEntities()
            : base("name=techtalkEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tevent_presenters> tevent_presenters { get; set; }
        public virtual DbSet<tevent_users> tevent_users { get; set; }
        public virtual DbSet<tevent> tevents { get; set; }
        public virtual DbSet<user> users { get; set; }
    }
}