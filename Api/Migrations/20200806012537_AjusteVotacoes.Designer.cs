﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using votacao_backend.Api.DbContexts;

namespace Api.Migrations
{
    [DbContext(typeof(VotacaoContext))]
    [Migration("20200806012537_AjusteVotacoes")]
    partial class AjusteVotacoes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("votacao_backend.Api.Entities.Votacao", b =>
                {
                    b.Property<Guid>("IdVotacao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("IdGradeamento")
                        .HasColumnType("uuid");

                    b.Property<Guid>("IdUsuario")
                        .HasColumnType("uuid");

                    b.HasKey("IdVotacao");

                    b.ToTable("Votacoes");
                });

            modelBuilder.Entity("votacao_backend.Api.Entities.Votos", b =>
                {
                    b.Property<Guid>("IdVotos")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("IdGradeamento")
                        .HasColumnType("uuid");

                    b.Property<string>("TituloTrabalho")
                        .HasColumnType("text");

                    b.Property<int>("TotalVotos")
                        .HasColumnType("integer");

                    b.HasKey("IdVotos");

                    b.ToTable("Votos");
                });
#pragma warning restore 612, 618
        }
    }
}
