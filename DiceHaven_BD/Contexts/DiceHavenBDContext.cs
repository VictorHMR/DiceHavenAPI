using System;
using System.Collections.Generic;
using DiceHaven_BD.Models;
using Microsoft.EntityFrameworkCore;

namespace DiceHaven_BD.Contexts;

public partial class DiceHavenBDContext : DbContext
{
    public DiceHavenBDContext()
    {
    }

    public DiceHavenBDContext(DbContextOptions<DiceHavenBDContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TB_CONFIG_USUARIO> TB_CONFIG_USUARIOs { get; set; }

    public virtual DbSet<TB_GRUPO> TB_GRUPOs { get; set; }

    public virtual DbSet<TB_GRUPO_PERMISSAO> TB_GRUPO_PERMISSAOs { get; set; }

    public virtual DbSet<TB_GRUPO_USUARIO> TB_GRUPO_USUARIOs { get; set; }

    public virtual DbSet<TB_PERMISSAO> TB_PERMISSAOs { get; set; }

    public virtual DbSet<TB_PERSONAGEM> TB_PERSONAGEMs { get; set; }

    public virtual DbSet<TB_USUARIO> TB_USUARIOs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=containers-us-west-82.railway.app;port=7795;database=railway;user=root;password=rT2hDXifJ32jBWvQJEla", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.33-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<TB_CONFIG_USUARIO>(entity =>
        {
            entity.HasKey(e => e.ID_CONFIG_USUARIO).HasName("PRIMARY");

            entity.ToTable("TB_CONFIG_USUARIO");

            entity.Property(e => e.ID_CONFIG_USUARIO).ValueGeneratedOnAdd();

            entity.HasOne(d => d.ID_CONFIG_USUARIONavigation).WithOne(p => p.TB_CONFIG_USUARIO)
                .HasForeignKey<TB_CONFIG_USUARIO>(d => d.ID_CONFIG_USUARIO)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("TB_CONFIG_USUARIO_ibfk_1");
        });

        modelBuilder.Entity<TB_GRUPO>(entity =>
        {
            entity.HasKey(e => e.ID_GRUPO).HasName("PRIMARY");

            entity.ToTable("TB_GRUPO");

            entity.Property(e => e.ID_GRUPO).ValueGeneratedNever();
            entity.Property(e => e.DS_DESCRICAO).HasMaxLength(200);
            entity.Property(e => e.DS_GRUPO)
                .IsRequired()
                .HasMaxLength(100);
        });

        modelBuilder.Entity<TB_GRUPO_PERMISSAO>(entity =>
        {
            entity.HasKey(e => e.ID_GRUPO_PERMISSAO).HasName("PRIMARY");

            entity.ToTable("TB_GRUPO_PERMISSAO");

            entity.HasIndex(e => e.ID_GRUPO, "ID_GRUPO");

            entity.HasIndex(e => e.ID_PERMISSAO, "ID_PERMISSAO");

            entity.HasOne(d => d.ID_GRUPONavigation).WithMany(p => p.TB_GRUPO_PERMISSAOs)
                .HasForeignKey(d => d.ID_GRUPO)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("TB_GRUPO_PERMISSAO_ibfk_1");

            entity.HasOne(d => d.ID_PERMISSAONavigation).WithMany(p => p.TB_GRUPO_PERMISSAOs)
                .HasForeignKey(d => d.ID_PERMISSAO)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("TB_GRUPO_PERMISSAO_ibfk_2");
        });

        modelBuilder.Entity<TB_GRUPO_USUARIO>(entity =>
        {
            entity.HasKey(e => e.ID_GRUPO_USUARIO).HasName("PRIMARY");

            entity.ToTable("TB_GRUPO_USUARIO");

            entity.HasIndex(e => e.ID_GRUPO, "ID_GRUPO");

            entity.HasIndex(e => e.ID_USUARIO, "ID_USUARIO");

            entity.HasOne(d => d.ID_GRUPONavigation).WithMany(p => p.TB_GRUPO_USUARIOs)
                .HasForeignKey(d => d.ID_GRUPO)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("TB_GRUPO_USUARIO_ibfk_1");

            entity.HasOne(d => d.ID_USUARIONavigation).WithMany(p => p.TB_GRUPO_USUARIOs)
                .HasForeignKey(d => d.ID_USUARIO)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("TB_GRUPO_USUARIO_ibfk_2");
        });

        modelBuilder.Entity<TB_PERMISSAO>(entity =>
        {
            entity.HasKey(e => e.ID_PERMISSAO).HasName("PRIMARY");

            entity.ToTable("TB_PERMISSAO");

            entity.Property(e => e.ID_PERMISSAO).ValueGeneratedNever();
            entity.Property(e => e.DS_DESCRICAO)
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(e => e.DS_PERMISSAO)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<TB_PERSONAGEM>(entity =>
        {
            entity.HasKey(e => e.ID_PERSONAGEM).HasName("PRIMARY");

            entity.ToTable("TB_PERSONAGEM");

            entity.HasIndex(e => e.ID_USUARIO, "ID_USUARIO");

            entity.Property(e => e.DS_BACKSTORY).HasColumnType("text");
            entity.Property(e => e.DS_CAMPO_LIVRE).HasColumnType("text");
            entity.Property(e => e.DS_GENERO).HasMaxLength(20);
            entity.Property(e => e.DS_NOME)
                .IsRequired()
                .HasMaxLength(75);

            entity.HasOne(d => d.ID_USUARIONavigation).WithMany(p => p.TB_PERSONAGEMs)
                .HasForeignKey(d => d.ID_USUARIO)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("TB_PERSONAGEM_ibfk_1");
        });

        modelBuilder.Entity<TB_USUARIO>(entity =>
        {
            entity.HasKey(e => e.ID_USUARIO).HasName("PRIMARY");

            entity.ToTable("TB_USUARIO");

            entity.Property(e => e.DS_EMAIL)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.DS_LOGIN)
                .IsRequired()
                .HasMaxLength(30);
            entity.Property(e => e.DS_NOME)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.DS_SENHA).HasMaxLength(100);
            entity.Property(e => e.DT_NASCIMENTO).HasColumnType("datetime");
            entity.Property(e => e.DT_ULTIMO_ACESSO).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
