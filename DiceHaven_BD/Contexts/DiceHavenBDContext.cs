﻿using System;
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

    public virtual DbSet<tb_campanha> tb_campanhas { get; set; }

    public virtual DbSet<tb_classe> tb_classes { get; set; }

    public virtual DbSet<tb_config_usuario> tb_config_usuarios { get; set; }

    public virtual DbSet<tb_ficha> tb_fichas { get; set; }

    public virtual DbSet<tb_grupo> tb_grupos { get; set; }

    public virtual DbSet<tb_grupo_permissao> tb_grupo_permissaos { get; set; }

    public virtual DbSet<tb_grupo_usuario> tb_grupo_usuarios { get; set; }

    public virtual DbSet<tb_permissao> tb_permissaos { get; set; }

    public virtual DbSet<tb_personagem> tb_personagems { get; set; }

    public virtual DbSet<tb_raca> tb_racas { get; set; }

    public virtual DbSet<tb_usuario> tb_usuarios { get; set; }

    public virtual DbSet<tb_usuario_campanha> tb_usuario_campanhas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=3306;database=DiceHaven;user=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.31-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<tb_campanha>(entity =>
        {
            entity.HasKey(e => e.ID_CAMPANHA).HasName("PRIMARY");

            entity.ToTable("tb_campanha");

            entity.HasIndex(e => e.ID_MESTRE_CAMPANHA, "ID_MESTRE_CAMPANHA");

            entity.HasIndex(e => e.ID_USUARIO_CRIADOR, "ID_USUARIO_CRIADOR");

            entity.Property(e => e.DS_LORE)
                .IsRequired()
                .HasColumnType("text");
            entity.Property(e => e.DS_NOME_CAMPANHA)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.DS_PERIODO).HasMaxLength(30);
            entity.Property(e => e.DS_XP_SUBIR_LVL)
                .IsRequired()
                .HasColumnType("text");
            entity.Property(e => e.DT_CRIACAO).HasColumnType("datetime");

            entity.HasOne(d => d.ID_MESTRE_CAMPANHANavigation).WithMany(p => p.tb_campanhaID_MESTRE_CAMPANHANavigations)
                .HasForeignKey(d => d.ID_MESTRE_CAMPANHA)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tb_campanha_ibfk_2");

            entity.HasOne(d => d.ID_USUARIO_CRIADORNavigation).WithMany(p => p.tb_campanhaID_USUARIO_CRIADORNavigations)
                .HasForeignKey(d => d.ID_USUARIO_CRIADOR)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tb_campanha_ibfk_1");
        });

        modelBuilder.Entity<tb_classe>(entity =>
        {
            entity.HasKey(e => e.ID_CLASSE).HasName("PRIMARY");

            entity.ToTable("tb_classe");

            entity.HasIndex(e => e.ID_CAMPANHA, "ID_CAMPANHA");

            entity.Property(e => e.DS_CLASSE)
                .IsRequired()
                .HasMaxLength(30);
            entity.Property(e => e.DS_DESCRICAO).HasColumnType("text");

            entity.HasOne(d => d.ID_CAMPANHANavigation).WithMany(p => p.tb_classes)
                .HasForeignKey(d => d.ID_CAMPANHA)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tb_classe_ibfk_1");
        });

        modelBuilder.Entity<tb_config_usuario>(entity =>
        {
            entity.HasKey(e => e.ID_CONFIG_USUARIO).HasName("PRIMARY");

            entity.ToTable("tb_config_usuario");

            entity.Property(e => e.ID_CONFIG_USUARIO).ValueGeneratedOnAdd();

            entity.HasOne(d => d.ID_CONFIG_USUARIONavigation).WithOne(p => p.tb_config_usuario)
                .HasForeignKey<tb_config_usuario>(d => d.ID_CONFIG_USUARIO)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tb_config_usuario_ibfk_1");
        });

        modelBuilder.Entity<tb_ficha>(entity =>
        {
            entity.HasKey(e => e.ID_FICHA).HasName("PRIMARY");

            entity.ToTable("tb_ficha");

            entity.HasIndex(e => e.ID_CAMPANHA, "ID_CAMPANHA");

            entity.HasIndex(e => e.ID_CLASSE, "ID_CLASSE");

            entity.HasIndex(e => e.ID_PERSONAGEM, "ID_PERSONAGEM");

            entity.HasIndex(e => e.ID_RACA, "ID_RACA");

            entity.Property(e => e.DS_PASSADO).HasColumnType("text");
            entity.Property(e => e.DS_TENDENCIA).HasMaxLength(30);

            entity.HasOne(d => d.ID_CAMPANHANavigation).WithMany(p => p.tb_fichas)
                .HasForeignKey(d => d.ID_CAMPANHA)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tb_ficha_ibfk_4");

            entity.HasOne(d => d.ID_CLASSENavigation).WithMany(p => p.tb_fichas)
                .HasForeignKey(d => d.ID_CLASSE)
                .HasConstraintName("tb_ficha_ibfk_2");

            entity.HasOne(d => d.ID_PERSONAGEMNavigation).WithMany(p => p.tb_fichas)
                .HasForeignKey(d => d.ID_PERSONAGEM)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tb_ficha_ibfk_3");

            entity.HasOne(d => d.ID_RACANavigation).WithMany(p => p.tb_fichas)
                .HasForeignKey(d => d.ID_RACA)
                .HasConstraintName("tb_ficha_ibfk_1");
        });

        modelBuilder.Entity<tb_grupo>(entity =>
        {
            entity.HasKey(e => e.ID_GRUPO).HasName("PRIMARY");

            entity.ToTable("tb_grupo");

            entity.Property(e => e.ID_GRUPO).ValueGeneratedNever();
            entity.Property(e => e.DS_DESCRICAO).HasMaxLength(200);
            entity.Property(e => e.DS_GRUPO)
                .IsRequired()
                .HasMaxLength(100);
        });

        modelBuilder.Entity<tb_grupo_permissao>(entity =>
        {
            entity.HasKey(e => e.ID_GRUPO_PERMISSAO).HasName("PRIMARY");

            entity.ToTable("tb_grupo_permissao");

            entity.HasIndex(e => e.ID_GRUPO, "ID_GRUPO");

            entity.HasIndex(e => e.ID_PERMISSAO, "ID_PERMISSAO");
        });

        modelBuilder.Entity<tb_grupo_usuario>(entity =>
        {
            entity.HasKey(e => e.ID_GRUPO_USUARIO).HasName("PRIMARY");

            entity.ToTable("tb_grupo_usuario");

            entity.HasIndex(e => e.ID_GRUPO, "ID_GRUPO");

            entity.HasIndex(e => e.ID_USUARIO, "ID_USUARIO");
        });

        modelBuilder.Entity<tb_permissao>(entity =>
        {
            entity.HasKey(e => e.ID_PERMISSAO).HasName("PRIMARY");

            entity.ToTable("tb_permissao");

            entity.Property(e => e.ID_PERMISSAO).ValueGeneratedNever();
            entity.Property(e => e.DS_DESCRICAO)
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(e => e.DS_PERMISSAO)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<tb_personagem>(entity =>
        {
            entity.HasKey(e => e.ID_PERSONAGEM).HasName("PRIMARY");

            entity.ToTable("tb_personagem");

            entity.HasIndex(e => e.ID_USUARIO, "ID_USUARIO");

            entity.Property(e => e.DS_BACKSTORY).HasColumnType("text");
            entity.Property(e => e.DS_CAMPO_LIVRE).HasColumnType("text");
            entity.Property(e => e.DS_GENERO).HasMaxLength(20);
            entity.Property(e => e.DS_NOME)
                .IsRequired()
                .HasMaxLength(75);

            entity.HasOne(d => d.ID_USUARIONavigation).WithMany(p => p.tb_personagems)
                .HasForeignKey(d => d.ID_USUARIO)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tb_personagem_ibfk_1");
        });

        modelBuilder.Entity<tb_raca>(entity =>
        {
            entity.HasKey(e => e.ID_RACA).HasName("PRIMARY");

            entity.ToTable("tb_raca");

            entity.HasIndex(e => e.ID_CAMPANHA, "ID_CAMPANHA");

            entity.Property(e => e.DS_DESCRICAO).HasColumnType("text");
            entity.Property(e => e.DS_RACA)
                .IsRequired()
                .HasMaxLength(30);

            entity.HasOne(d => d.ID_CAMPANHANavigation).WithMany(p => p.tb_racas)
                .HasForeignKey(d => d.ID_CAMPANHA)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tb_raca_ibfk_1");
        });

        modelBuilder.Entity<tb_usuario>(entity =>
        {
            entity.HasKey(e => e.ID_USUARIO).HasName("PRIMARY");

            entity.ToTable("tb_usuario");

            entity.Property(e => e.DS_EMAIL)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.DS_LOGIN)
                .IsRequired()
                .HasMaxLength(30);
            entity.Property(e => e.DS_NOME)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.DS_SENHA)
                .IsRequired()
                .HasColumnType("text");
            entity.Property(e => e.DT_NASCIMENTO).HasColumnType("datetime");
            entity.Property(e => e.DT_ULTIMO_ACESSO).HasColumnType("datetime");
        });

        modelBuilder.Entity<tb_usuario_campanha>(entity =>
        {
            entity.HasKey(e => e.ID_USUARIO_CAMPANHA).HasName("PRIMARY");

            entity.ToTable("tb_usuario_campanha");

            entity.HasIndex(e => e.ID_CAMPANHA, "ID_CAMPANHA");

            entity.HasIndex(e => e.ID_USUARIO, "ID_USUARIO");

            entity.Property(e => e.DT_ENTRADA).HasColumnType("datetime");

            entity.HasOne(d => d.ID_CAMPANHANavigation).WithMany(p => p.tb_usuario_campanhas)
                .HasForeignKey(d => d.ID_CAMPANHA)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tb_usuario_campanha_ibfk_1");

            entity.HasOne(d => d.ID_USUARIONavigation).WithMany(p => p.tb_usuario_campanhas)
                .HasForeignKey(d => d.ID_USUARIO)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tb_usuario_campanha_ibfk_2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
