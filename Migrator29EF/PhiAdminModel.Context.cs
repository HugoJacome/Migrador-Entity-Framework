﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Migrator29EF
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class phiAdminEntities : DbContext
    {
        public phiAdminEntities()
            : base("name=phiAdminEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__EFMigrationsHistory> C__EFMigrationsHistory { get; set; }
        public virtual DbSet<Acquirers> Acquirers { get; set; }
        public virtual DbSet<Activities> Activities { get; set; }
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<AuditLog2> AuditLog2 { get; set; }
        public virtual DbSet<AuditLogs> AuditLogs { get; set; }
        public virtual DbSet<Authorizers> Authorizers { get; set; }
        public virtual DbSet<Branches> Branches { get; set; }
        public virtual DbSet<CardModules> CardModules { get; set; }
        public virtual DbSet<Entities> Entities { get; set; }
        public virtual DbSet<EntityActivities> EntityActivities { get; set; }
        public virtual DbSet<GroupPermission> GroupPermission { get; set; }
        public virtual DbSet<Groups> Groups { get; set; }
        public virtual DbSet<Institutions> Institutions { get; set; }
        public virtual DbSet<Resources> Resources { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Transactions> Transactions { get; set; }
        public virtual DbSet<ApiKeys> ApiKeys { get; set; }
        public virtual DbSet<Logs> Logs { get; set; }
    }
}
