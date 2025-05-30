﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiceHaven_API.Migrations
{
    /// <inheritdoc />
    public partial class Migration_20250517135023 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_usuario",
                columns: table => new
                {
                    ID_USUARIO = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DS_NOME = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    DT_NASCIMENTO = table.Column<DateTime>(type: "datetime", nullable: false),
                    DS_LOGIN = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    DS_SENHA = table.Column<string>(type: "text", nullable: false),
                    DS_EMAIL = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    FL_ATIVO = table.Column<bool>(type: "INTEGER", nullable: false),
                    DT_ULTIMO_ACESSO = table.Column<DateTime>(type: "datetime", nullable: true),
                    DS_FOTO = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID_USUARIO);
                });

            migrationBuilder.CreateTable(
                name: "tb_campanha",
                columns: table => new
                {
                    ID_CAMPANHA = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DS_NOME_CAMPANHA = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    DS_LORE = table.Column<string>(type: "text", nullable: true),
                    DT_CRIACAO = table.Column<DateTime>(type: "datetime", nullable: false),
                    FL_ATIVO = table.Column<bool>(type: "INTEGER", nullable: false),
                    FL_PUBLICA = table.Column<bool>(type: "INTEGER", nullable: false),
                    DS_FOTO = table.Column<string>(type: "text", nullable: true),
                    ID_USUARIO_CRIADOR = table.Column<int>(type: "INTEGER", nullable: false),
                    ID_MESTRE_CAMPANHA = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID_CAMPANHA);
                    table.ForeignKey(
                        name: "tb_campanha_ibfk_1",
                        column: x => x.ID_USUARIO_CRIADOR,
                        principalTable: "tb_usuario",
                        principalColumn: "ID_USUARIO");
                    table.ForeignKey(
                        name: "tb_campanha_ibfk_2",
                        column: x => x.ID_MESTRE_CAMPANHA,
                        principalTable: "tb_usuario",
                        principalColumn: "ID_USUARIO");
                });

            migrationBuilder.CreateTable(
                name: "tb_config_usuario",
                columns: table => new
                {
                    ID_CONFIG_USUARIO = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FL_DARK_MODE = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID_CONFIG_USUARIO);
                    table.ForeignKey(
                        name: "tb_config_usuario_ibfk_1",
                        column: x => x.ID_CONFIG_USUARIO,
                        principalTable: "tb_usuario",
                        principalColumn: "ID_USUARIO");
                });

            migrationBuilder.CreateTable(
                name: "tb_personagem",
                columns: table => new
                {
                    ID_PERSONAGEM = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DS_NOME = table.Column<string>(type: "TEXT", maxLength: 75, nullable: false),
                    DS_FOTO = table.Column<string>(type: "text", nullable: true),
                    DS_COR = table.Column<string>(type: "TEXT", nullable: true),
                    ID_USUARIO = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID_PERSONAGEM);
                    table.ForeignKey(
                        name: "tb_personagem_ibfk_1",
                        column: x => x.ID_USUARIO,
                        principalTable: "tb_usuario",
                        principalColumn: "ID_USUARIO");
                });

            migrationBuilder.CreateTable(
                name: "tb_usuario_contato",
                columns: table => new
                {
                    ID_USUARIO_CONTATO = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ID_USUARIO = table.Column<int>(type: "INTEGER", nullable: false),
                    ID_CONTATO = table.Column<int>(type: "INTEGER", nullable: false),
                    FL_MUTADO = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID_USUARIO_CONTATO);
                    table.ForeignKey(
                        name: "tb_usuario_contato_ibfk_1",
                        column: x => x.ID_USUARIO,
                        principalTable: "tb_usuario",
                        principalColumn: "ID_USUARIO");
                    table.ForeignKey(
                        name: "tb_usuario_contato_ibfk_2",
                        column: x => x.ID_CONTATO,
                        principalTable: "tb_usuario",
                        principalColumn: "ID_USUARIO");
                });

            migrationBuilder.CreateTable(
                name: "tb_secao_ficha",
                columns: table => new
                {
                    ID_SECAO_FICHA = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DS_NOME_SECAO = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    NR_ORDEM = table.Column<int>(type: "INTEGER", nullable: false, defaultValueSql: "'0'"),
                    ID_CAMPANHA = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID_SECAO_FICHA);
                    table.ForeignKey(
                        name: "tb_secao_ficha_ibfk_1",
                        column: x => x.ID_CAMPANHA,
                        principalTable: "tb_campanha",
                        principalColumn: "ID_CAMPANHA");
                });

            migrationBuilder.CreateTable(
                name: "tb_usuario_campanha",
                columns: table => new
                {
                    ID_USUARIO_CAMPANHA = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ID_CAMPANHA = table.Column<int>(type: "INTEGER", nullable: false),
                    ID_USUARIO = table.Column<int>(type: "INTEGER", nullable: false),
                    FL_ADMIN = table.Column<bool>(type: "INTEGER", nullable: false),
                    FL_MUTADO = table.Column<bool>(type: "INTEGER", nullable: false),
                    DT_ENTRADA = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID_USUARIO_CAMPANHA);
                    table.ForeignKey(
                        name: "tb_usuario_campanha_ibfk_1",
                        column: x => x.ID_CAMPANHA,
                        principalTable: "tb_campanha",
                        principalColumn: "ID_CAMPANHA");
                    table.ForeignKey(
                        name: "tb_usuario_campanha_ibfk_2",
                        column: x => x.ID_USUARIO,
                        principalTable: "tb_usuario",
                        principalColumn: "ID_USUARIO");
                });

            migrationBuilder.CreateTable(
                name: "tb_campanha_mensagem",
                columns: table => new
                {
                    ID_CAMPANHA_MENSAGEM = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DS_MENSAGEM = table.Column<string>(type: "text", nullable: true),
                    FL_MESTRE = table.Column<bool>(type: "INTEGER", nullable: false),
                    DT_MENSAGEM = table.Column<DateTime>(type: "datetime", nullable: false),
                    ID_USUARIO = table.Column<int>(type: "INTEGER", nullable: false),
                    ID_CAMPANHA = table.Column<int>(type: "INTEGER", nullable: false),
                    ID_PERSONAGEM = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID_CAMPANHA_MENSAGEM);
                    table.ForeignKey(
                        name: "tb_campanha_mensagem_ibfk_1",
                        column: x => x.ID_CAMPANHA,
                        principalTable: "tb_campanha",
                        principalColumn: "ID_CAMPANHA");
                    table.ForeignKey(
                        name: "tb_campanha_mensagem_ibfk_2",
                        column: x => x.ID_USUARIO,
                        principalTable: "tb_usuario",
                        principalColumn: "ID_USUARIO");
                    table.ForeignKey(
                        name: "tb_campanha_mensagem_ibfk_3",
                        column: x => x.ID_PERSONAGEM,
                        principalTable: "tb_personagem",
                        principalColumn: "ID_PERSONAGEM");
                });

            migrationBuilder.CreateTable(
                name: "tb_personagem_campanha",
                columns: table => new
                {
                    ID_PERSONAGEM_CAMPANHA = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ID_PERSONAGEM = table.Column<int>(type: "INTEGER", nullable: false),
                    ID_CAMPANHA = table.Column<int>(type: "INTEGER", nullable: false),
                    DT_REGISTRO = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID_PERSONAGEM_CAMPANHA);
                    table.ForeignKey(
                        name: "tb_personagem_campanha_ibfk_1",
                        column: x => x.ID_CAMPANHA,
                        principalTable: "tb_campanha",
                        principalColumn: "ID_CAMPANHA");
                    table.ForeignKey(
                        name: "tb_personagem_campanha_ibfk_2",
                        column: x => x.ID_PERSONAGEM,
                        principalTable: "tb_personagem",
                        principalColumn: "ID_PERSONAGEM");
                });

            migrationBuilder.CreateTable(
                name: "tb_campo_ficha",
                columns: table => new
                {
                    ID_CAMPO_FICHA = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DS_NOME_CAMPO = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    NR_TIPO_CAMPO = table.Column<int>(type: "INTEGER", nullable: false),
                    FL_BLOQUEADO = table.Column<bool>(type: "INTEGER", nullable: true),
                    FL_VISIVEL = table.Column<bool>(type: "INTEGER", nullable: true),
                    FL_MODIFICADOR = table.Column<bool>(type: "INTEGER", nullable: false),
                    DS_VALOR_PADRAO = table.Column<string>(type: "text", nullable: true),
                    NR_ORDEM = table.Column<int>(type: "INTEGER", nullable: false),
                    ID_SECAO_FICHA = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID_CAMPO_FICHA);
                    table.ForeignKey(
                        name: "tb_campo_ficha_ibfk_1",
                        column: x => x.ID_SECAO_FICHA,
                        principalTable: "tb_secao_ficha",
                        principalColumn: "ID_SECAO_FICHA");
                });

            migrationBuilder.CreateTable(
                name: "tb_dados_ficha",
                columns: table => new
                {
                    ID_DADO_FICHA = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ID_CAMPO_FICHA = table.Column<int>(type: "INTEGER", nullable: false),
                    ID_PERSONAGEM = table.Column<int>(type: "INTEGER", nullable: false),
                    DS_VALOR = table.Column<string>(type: "text", nullable: true),
                    DS_VALOR_MODIFICADOR = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID_DADO_FICHA);
                    table.ForeignKey(
                        name: "tb_dados_ficha_ibfk_1",
                        column: x => x.ID_CAMPO_FICHA,
                        principalTable: "tb_campo_ficha",
                        principalColumn: "ID_CAMPO_FICHA");
                    table.ForeignKey(
                        name: "tb_dados_ficha_ibfk_2",
                        column: x => x.ID_PERSONAGEM,
                        principalTable: "tb_personagem",
                        principalColumn: "ID_PERSONAGEM");
                });

            migrationBuilder.CreateIndex(
                name: "ID_MESTRE_CAMPANHA",
                table: "tb_campanha",
                column: "ID_MESTRE_CAMPANHA");

            migrationBuilder.CreateIndex(
                name: "ID_USUARIO_CRIADOR",
                table: "tb_campanha",
                column: "ID_USUARIO_CRIADOR");

            migrationBuilder.CreateIndex(
                name: "ID_CAMPANHA2",
                table: "tb_campanha_mensagem",
                column: "ID_CAMPANHA");

            migrationBuilder.CreateIndex(
                name: "ID_PERSONAGEM1",
                table: "tb_campanha_mensagem",
                column: "ID_PERSONAGEM");

            migrationBuilder.CreateIndex(
                name: "ID_USUARIO3",
                table: "tb_campanha_mensagem",
                column: "ID_USUARIO");

            migrationBuilder.CreateIndex(
                name: "ID_CAMPANHA",
                table: "tb_campo_ficha",
                column: "ID_SECAO_FICHA");

            migrationBuilder.CreateIndex(
                name: "ID_CAMPO_FICHA",
                table: "tb_dados_ficha",
                column: "ID_CAMPO_FICHA");

            migrationBuilder.CreateIndex(
                name: "ID_PERSONAGEM",
                table: "tb_dados_ficha",
                column: "ID_PERSONAGEM");

            migrationBuilder.CreateIndex(
                name: "ID_USUARIO",
                table: "tb_personagem",
                column: "ID_USUARIO");

            migrationBuilder.CreateIndex(
                name: "ID_CAMPANHA3",
                table: "tb_personagem_campanha",
                column: "ID_CAMPANHA");

            migrationBuilder.CreateIndex(
                name: "ID_PERSONAGEM2",
                table: "tb_personagem_campanha",
                column: "ID_PERSONAGEM");

            migrationBuilder.CreateIndex(
                name: "ID_CAMPANHA4",
                table: "tb_secao_ficha",
                column: "ID_CAMPANHA");

            migrationBuilder.CreateIndex(
                name: "ID_CAMPANHA1",
                table: "tb_usuario_campanha",
                column: "ID_CAMPANHA");

            migrationBuilder.CreateIndex(
                name: "ID_USUARIO1",
                table: "tb_usuario_campanha",
                column: "ID_USUARIO");

            migrationBuilder.CreateIndex(
                name: "ID_CONTATO",
                table: "tb_usuario_contato",
                column: "ID_CONTATO");

            migrationBuilder.CreateIndex(
                name: "ID_USUARIO2",
                table: "tb_usuario_contato",
                column: "ID_USUARIO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_campanha_mensagem");

            migrationBuilder.DropTable(
                name: "tb_config_usuario");

            migrationBuilder.DropTable(
                name: "tb_dados_ficha");

            migrationBuilder.DropTable(
                name: "tb_personagem_campanha");

            migrationBuilder.DropTable(
                name: "tb_usuario_campanha");

            migrationBuilder.DropTable(
                name: "tb_usuario_contato");

            migrationBuilder.DropTable(
                name: "tb_campo_ficha");

            migrationBuilder.DropTable(
                name: "tb_personagem");

            migrationBuilder.DropTable(
                name: "tb_secao_ficha");

            migrationBuilder.DropTable(
                name: "tb_campanha");

            migrationBuilder.DropTable(
                name: "tb_usuario");
        }
    }
}
