﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Employee_API.Models;

public partial class EmployeeDbContext : DbContext
{
    public EmployeeDbContext()
    {
    }

    public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<State> States { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=ANURAG\\SQLEXPRESS;Initial Catalog=EmployeeDb;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.CityId).HasName("PK__City__F2D21B76DC9B4CB1");

            entity.Property(e => e.CityId).ValueGeneratedNever();

            entity.HasOne(d => d.District).WithMany(p => p.Cities).HasConstraintName("FK__City__DistrictId__4E88ABD4");
        });

        modelBuilder.Entity<District>(entity =>
        {
            entity.HasKey(e => e.DistrictId).HasName("PK__District__85FDA4C6FA745CD5");

            entity.Property(e => e.DistrictId).ValueGeneratedNever();

            entity.HasOne(d => d.State).WithMany(p => p.Districts).HasConstraintName("FK__District__StateI__4BAC3F29");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04F1117B889F0");

            entity.Property(e => e.EmployeeId).ValueGeneratedOnAdd();

            entity.HasOne(d => d.City).WithMany(p => p.Employees).HasConstraintName("FK__Employee__CityId__534D60F1");

            entity.HasOne(d => d.District).WithMany(p => p.Employees).HasConstraintName("FK__Employee__Distri__52593CB8");

            entity.HasOne(d => d.State).WithMany(p => p.Employees).HasConstraintName("FK__Employee__StateI__5165187F");
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => e.StateId).HasName("PK__State__C3BA3B3A2CF3E7E6");

            entity.Property(e => e.StateId).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}