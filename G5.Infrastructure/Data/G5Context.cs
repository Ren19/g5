using G5.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace G5.Infrastructure.Data
{
    public class G5Context : DbContext
    {
        public G5Context(DbContextOptions<G5Context> options) : base(options)
        {
            //ChangeTracker.LazyLoadingEnabled = true;
        }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<G5.Domain.Entities.Employee> Employees { get; set; }
        public DbSet<PermissionType> PermissionTypes { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<EmployeePermissionType> EmployeePermissionTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
            // Configuración de relaciones
            modelBuilder.Entity<G5.Domain.Models.Employee>()
                .HasOne(e => e.DocumentType)
                .WithMany()
                .HasForeignKey(e => e.DocumentTypeId);

            modelBuilder.Entity<Permission>()
                .HasOne(p => p.Employee)
                .WithMany()
                .HasForeignKey(p => p.EmployeeId);

            modelBuilder.Entity<Permission>()
                .HasOne(p => p.PermissionType)
                .WithMany()
                .HasForeignKey(p => p.PermissionTypeId);

            modelBuilder.Entity<EmployeePermissionType>()
                .HasOne(ept => ept.Employee)
                .WithMany()
                .HasForeignKey(ept => ept.EmployeeId);

            modelBuilder.Entity<EmployeePermissionType>()
                .HasOne(ept => ept.PermissionType)
                .WithMany()
                .HasForeignKey(ept => ept.PermissionTypeId);
            */
        }
    }
}
