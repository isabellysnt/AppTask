using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AppTask.Models;

public partial class DbTasksColegaContext : DbContext
{
    public DbTasksColegaContext()
    {
    }

    public DbTasksColegaContext(DbContextOptions<DbTasksColegaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CentralDeCusto> CentralDeCustos { get; set; }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<Funcionario> Funcionarios { get; set; }

    public virtual DbSet<Incidente> Incidentes { get; set; }

    public virtual DbSet<Tarefa> Tarefas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SENAI;Database=dbTasks_Colega;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CentralDeCusto>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PK__CentralD__06370DADF553F44F");

            entity.ToTable("CentralDeCusto");

            entity.Property(e => e.NomeCentral)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.ValorMetaAnual)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 0)");
        });

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PK__Departam__06370DAD247EE8E7");

            entity.ToTable("Departamento");

            entity.Property(e => e.Descricao)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Funcionario>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PK__Funciona__06370DAD2A81FE73");

            entity.ToTable("Funcionario");

            entity.Property(e => e.Cargo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Incidente>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PK__Incident__06370DAD1619FB34");

            entity.ToTable("Incidente");

            entity.Property(e => e.DataIncidente).HasColumnType("datetime");
            entity.Property(e => e.DescricaoProblema)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Resolvido)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.Solucao)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Tarefa>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PK__Tarefa__06370DAD84056A90");

            entity.ToTable("Tarefa");

            entity.Property(e => e.DataCancelada).HasColumnType("datetime");
            entity.Property(e => e.DataFinalizada).HasColumnType("datetime");
            entity.Property(e => e.DataIniciada).HasColumnType("datetime");
            entity.Property(e => e.DataPlanejada).HasColumnType("datetime");
            entity.Property(e => e.Descricao)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Prazo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.StatusTarefa)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.Funcionario).WithMany(p => p.Tarefas)
                .HasForeignKey(d => d.FuncionarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tarefa_Funcionario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
