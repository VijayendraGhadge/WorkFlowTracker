﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EntityLayer
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class vvghadgeDataBaseEntities : DbContext
    {
        public vvghadgeDataBaseEntities()
            : base("name=vvghadgeDataBaseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<access> accesses { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<comment> comments { get; set; }
        public virtual DbSet<justification> justifications { get; set; }
        public virtual DbSet<project> projects { get; set; }
        public virtual DbSet<project_step> project_step { get; set; }
        public virtual DbSet<step> steps { get; set; }
        public virtual DbSet<step_comment> step_comment { get; set; }
        public virtual DbSet<step_justification> step_justification { get; set; }
        public virtual DbSet<test> tests { get; set; }
        public virtual DbSet<user> users { get; set; }
    }
}
