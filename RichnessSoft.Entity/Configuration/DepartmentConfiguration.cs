﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RichnessSoft.Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RichnessSoft.Entity.Configuration
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("department");
            builder.HasKey(m => m.id);
            builder.Property(e => e.code).HasColumnType("varchar(50)").IsRequired();
            builder.Property(e => e.name).HasColumnType("varchar(150)").IsRequired();
            builder.Property(e => e.name2).HasColumnType("varchar(150)");
            builder.Property(e => e.active).HasColumnType("varchar(1)").HasDefaultValue("Y").HasComment("active");
            builder.Property(e => e.sectionid).HasComment("ฝ่ายงาน");

            builder.HasOne(p => p.Company).WithMany(p => p.Departments).HasForeignKey(e => e.companyid).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.Section).WithMany(p => p.Departments).HasForeignKey(e => e.sectionid).OnDelete(DeleteBehavior.Restrict);
        }
    }
}