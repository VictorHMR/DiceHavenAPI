using System;
using System.Collections.Generic;
using DiceHavenAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DiceHavenAPI.Contexts;

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

    public virtual DbSet<tb_campo_ficha> tb_campo_fichas { get; set; }

    public virtual DbSet<tb_chat> tb_chats { get; set; }

    public virtual DbSet<tb_chat_mensagem> tb_chat_mensagems { get; set; }

    public virtual DbSet<tb_config_usuario> tb_config_usuarios { get; set; }

    public virtual DbSet<tb_dados_ficha> tb_dados_fichas { get; set; }

    public virtual DbSet<tb_personagem> tb_personagems { get; set; }

    public virtual DbSet<tb_usuario> tb_usuarios { get; set; }

    public virtual DbSet<tb_usuario_campanha> tb_usuario_campanhas { get; set; }

    public virtual DbSet<tb_usuario_contato> tb_usuario_contatos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("Data Source=../DiceHaven.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci");

        modelBuilder.Entity<tb_campanha>(entity =>
        {
            entity.HasKey(e => e.ID_CAMPANHA).HasName("PRIMARY");

            entity.ToTable("tb_campanha");

            entity.HasIndex(e => e.ID_MESTRE_CAMPANHA, "ID_MESTRE_CAMPANHA");

            entity.HasIndex(e => e.ID_USUARIO_CRIADOR, "ID_USUARIO_CRIADOR");

            entity.Property(e => e.DS_FOTO).HasColumnType("text");
            entity.Property(e => e.DS_LORE).HasColumnType("text");
            entity.Property(e => e.DS_NOME_CAMPANHA)
                .IsRequired()
                .HasMaxLength(100);
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

        modelBuilder.Entity<tb_campo_ficha>(entity =>
        {
            entity.HasKey(e => e.ID_CAMPO_FICHA).HasName("PRIMARY");

            entity.ToTable("tb_campo_ficha");

            entity.HasIndex(e => e.ID_CAMPANHA, "ID_CAMPANHA");

            entity.Property(e => e.DS_DESCRICAO).HasColumnType("text");
            entity.Property(e => e.DS_FORMULA_MODIFICADOR).HasMaxLength(50);
            entity.Property(e => e.DS_NOME_CAMPO)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.DS_REFERENCIA).HasMaxLength(30);
            entity.Property(e => e.DS_VALOR_PADRAO).HasColumnType("text");

            entity.HasOne(d => d.ID_CAMPANHANavigation).WithMany(p => p.tb_campo_fichas)
                .HasForeignKey(d => d.ID_CAMPANHA)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tb_campo_ficha_ibfk_1");
        });

        modelBuilder.Entity<tb_chat>(entity =>
        {
            entity.HasKey(e => e.ID_CHAT).HasName("PRIMARY");

            entity.ToTable("tb_chat");

            entity.HasIndex(e => e.ID_USUARIO_1, "ID_USUARIO_1");

            entity.HasIndex(e => e.ID_USUARIO_2, "ID_USUARIO_2");

            entity.HasOne(d => d.ID_USUARIO_1Navigation).WithMany(p => p.tb_chatID_USUARIO_1Navigations)
                .HasForeignKey(d => d.ID_USUARIO_1)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tb_chat_ibfk_1");

            entity.HasOne(d => d.ID_USUARIO_2Navigation).WithMany(p => p.tb_chatID_USUARIO_2Navigations)
                .HasForeignKey(d => d.ID_USUARIO_2)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tb_chat_ibfk_2");
        });

        modelBuilder.Entity<tb_chat_mensagem>(entity =>
        {
            entity.HasKey(e => e.ID_CHAT_MENSAGEM).HasName("PRIMARY");

            entity.ToTable("tb_chat_mensagem");

            entity.HasIndex(e => e.ID_CHAT, "ID_CHAT");

            entity.HasIndex(e => e.ID_USUARIO, "ID_USUARIO");

            entity.Property(e => e.DS_LINK_IMAGEM).HasColumnType("text");
            entity.Property(e => e.DS_MENSAGEM)
                .IsRequired()
                .HasColumnType("text");
            entity.Property(e => e.DT_DATA_ENVIO).HasColumnType("datetime");

            entity.HasOne(d => d.ID_CHATNavigation).WithMany(p => p.tb_chat_mensagems)
                .HasForeignKey(d => d.ID_CHAT)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tb_chat_mensagem_ibfk_2");

            entity.HasOne(d => d.ID_USUARIONavigation).WithMany(p => p.tb_chat_mensagems)
                .HasForeignKey(d => d.ID_USUARIO)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tb_chat_mensagem_ibfk_1");
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

        modelBuilder.Entity<tb_dados_ficha>(entity =>
        {
            entity.HasKey(e => e.ID_DADO_FICHA).HasName("PRIMARY");

            entity.ToTable("tb_dados_ficha");

            entity.HasIndex(e => e.ID_CAMPO_FICHA, "ID_CAMPO_FICHA");

            entity.HasIndex(e => e.ID_PERSONAGEM, "ID_PERSONAGEM");

            entity.Property(e => e.DS_VALOR).HasColumnType("text");

            entity.HasOne(d => d.ID_CAMPO_FICHANavigation).WithMany(p => p.tb_dados_fichas)
                .HasForeignKey(d => d.ID_CAMPO_FICHA)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tb_dados_ficha_ibfk_1");

            entity.HasOne(d => d.ID_PERSONAGEMNavigation).WithMany(p => p.tb_dados_fichas)
                .HasForeignKey(d => d.ID_PERSONAGEM)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tb_dados_ficha_ibfk_2");
        });

        modelBuilder.Entity<tb_personagem>(entity =>
        {
            entity.HasKey(e => e.ID_PERSONAGEM).HasName("PRIMARY");

            entity.ToTable("tb_personagem");

            entity.HasIndex(e => e.ID_USUARIO, "ID_USUARIO");

            entity.Property(e => e.DS_BACKSTORY).HasColumnType("text");
            entity.Property(e => e.DS_CAMPO_LIVRE).HasColumnType("text");
            entity.Property(e => e.DS_FOTO).HasColumnType("text");
            entity.Property(e => e.DS_GENERO).HasMaxLength(20);
            entity.Property(e => e.DS_NOME)
                .IsRequired()
                .HasMaxLength(75);

            entity.HasOne(d => d.ID_USUARIONavigation).WithMany(p => p.tb_personagems)
                .HasForeignKey(d => d.ID_USUARIO)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tb_personagem_ibfk_1");
        });

        modelBuilder.Entity<tb_usuario>(entity =>
        {
            entity.HasKey(e => e.ID_USUARIO).HasName("PRIMARY");

            entity.ToTable("tb_usuario");

            entity.Property(e => e.DS_EMAIL)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.DS_FOTO).HasColumnType("text");
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

        modelBuilder.Entity<tb_usuario_contato>(entity =>
        {
            entity.HasKey(e => e.ID_USUARIO_CONTATO).HasName("PRIMARY");

            entity.ToTable("tb_usuario_contato");

            entity.HasIndex(e => e.ID_CONTATO, "ID_CONTATO");

            entity.HasIndex(e => e.ID_USUARIO, "ID_USUARIO");

            entity.HasOne(d => d.ID_CONTATONavigation).WithMany(p => p.tb_usuario_contatoID_CONTATONavigations)
                .HasForeignKey(d => d.ID_CONTATO)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tb_usuario_contato_ibfk_2");

            entity.HasOne(d => d.ID_USUARIONavigation).WithMany(p => p.tb_usuario_contatoID_USUARIONavigations)
                .HasForeignKey(d => d.ID_USUARIO)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tb_usuario_contato_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
