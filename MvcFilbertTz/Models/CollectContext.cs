using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MvcFilbertTz.Models
{
    public partial class CollectContext : DbContext
    {
        public CollectContext()
        {
        }

        public CollectContext(DbContextOptions<CollectContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Debt> Debts { get; set; }
        public virtual DbSet<Passport> Passports { get; set; }
        public virtual DbSet<Person> People { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite("Data Source=collect.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Debt>(entity =>
            {
                entity.ToTable("debt");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.ContractDt).HasColumnName("contract_dt");
                entity.Property(e => e.ContractNumber).HasColumnName("contract_number");
                entity.Property(e => e.DebtSum).HasColumnName("debt_sum");
                entity.Property(e => e.RPersonId)
                    .HasColumnType("INT")
                    .HasColumnName("r_person_id");

                // Связь Debt -> Person (многие к одному)
                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Debts)
                    .HasForeignKey(d => d.RPersonId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Debt_Person");
            });

            modelBuilder.Entity<Passport>(entity =>
            {
                entity.ToTable("passport");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Number).HasColumnName("number");
                entity.Property(e => e.RPersonId)
                    .HasColumnType("INT")
                    .HasColumnName("r_person_id");
                entity.Property(e => e.Series).HasColumnName("series");

                // Связь Passport -> Person (многие к одному)
                entity.HasOne(p => p.Person)
                    .WithMany(p => p.Passports)
                    .HasForeignKey(p => p.RPersonId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Passport_Person");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("person");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.BirthDate).HasColumnName("birth_date");
                entity.Property(e => e.F)
                    .HasColumnType("TEXT(256)")
                    .HasColumnName("f");
                entity.Property(e => e.I)
                    .HasColumnType("TEXT(160)")
                    .HasColumnName("i");
                entity.Property(e => e.O)
                    .HasColumnType("TEXT(160)")
                    .HasColumnName("o");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
