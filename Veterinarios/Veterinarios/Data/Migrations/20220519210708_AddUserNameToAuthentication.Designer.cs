﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Veterinarios.Data;

#nullable disable

namespace Veterinarios.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220519210708_AddUserNameToAuthentication")]
    partial class AddUserNameToAuthentication
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "v",
                            ConcurrencyStamp = "69210bda-aa8a-4b79-9642-ed47436eb5a1",
                            Name = "Veterinario",
                            NormalizedName = "VETERINARIO"
                        },
                        new
                        {
                            Id = "a",
                            ConcurrencyStamp = "fae506e9-73ac-421e-ae81-2c7d683de179",
                            Name = "Administrativo",
                            NormalizedName = "ADMINISTRATIVO"
                        },
                        new
                        {
                            Id = "c",
                            ConcurrencyStamp = "1c53e044-2c39-48be-9f7e-9d96f1cda707",
                            Name = "Cliente",
                            NormalizedName = "CLIENTE"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Veterinarios.Data.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DataRegisto")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NomeBatismo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Veterinarios.Models.Animais", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("DonoFK")
                        .HasColumnType("int");

                    b.Property<string>("Especie")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Foto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Peso")
                        .HasColumnType("float");

                    b.Property<string>("Raca")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DonoFK");

                    b.ToTable("Animais");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DonoFK = 1,
                            Especie = "Cão",
                            Foto = "animal1.jpg",
                            Nome = "Bubi",
                            Peso = 24.0,
                            Raca = "Pastor Alemão"
                        },
                        new
                        {
                            Id = 2,
                            DonoFK = 3,
                            Especie = "Cão",
                            Foto = "animal2.jpg",
                            Nome = "Pastor",
                            Peso = 50.0,
                            Raca = "Serra Estrela"
                        },
                        new
                        {
                            Id = 3,
                            DonoFK = 2,
                            Especie = "Cão",
                            Foto = "animal3.jpg",
                            Nome = "Tripé",
                            Peso = 4.0,
                            Raca = "Serra Estrela"
                        },
                        new
                        {
                            Id = 4,
                            DonoFK = 3,
                            Especie = "Cavalo",
                            Foto = "animal4.jpg",
                            Nome = "Saltador",
                            Peso = 580.0,
                            Raca = "Lusitana"
                        },
                        new
                        {
                            Id = 5,
                            DonoFK = 3,
                            Especie = "Gato",
                            Foto = "animal5.jpg",
                            Nome = "Tareco",
                            Peso = 1.0,
                            Raca = "siamês"
                        },
                        new
                        {
                            Id = 6,
                            DonoFK = 2,
                            Especie = "Cão",
                            Foto = "animal6.jpg",
                            Nome = "Cusca",
                            Peso = 45.0,
                            Raca = "Labrador"
                        },
                        new
                        {
                            Id = 7,
                            DonoFK = 4,
                            Especie = "Cão",
                            Foto = "animal7.jpg",
                            Nome = "Morde Tudo",
                            Peso = 39.0,
                            Raca = "Dobermann"
                        },
                        new
                        {
                            Id = 8,
                            DonoFK = 2,
                            Especie = "Cão",
                            Foto = "animal8.jpg",
                            Nome = "Forte",
                            Peso = 20.0,
                            Raca = "Rottweiler"
                        },
                        new
                        {
                            Id = 9,
                            DonoFK = 3,
                            Especie = "Vaca",
                            Foto = "animal9.jpg",
                            Nome = "Castanho",
                            Peso = 652.0,
                            Raca = "Mirandesa"
                        },
                        new
                        {
                            Id = 10,
                            DonoFK = 1,
                            Especie = "Gato",
                            Foto = "animal10.jpg",
                            Nome = "Saltitão",
                            Peso = 2.0,
                            Raca = "Persa"
                        });
                });

            modelBuilder.Entity("Veterinarios.Models.Consultas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AnimalFK")
                        .HasColumnType("int");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<string>("Observacoes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("ValorConsulta")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("VeterinarioFK")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AnimalFK");

                    b.HasIndex("VeterinarioFK");

                    b.ToTable("Consultas");
                });

            modelBuilder.Entity("Veterinarios.Models.Donos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NIF")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Sexo")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Donos");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            NIF = "813635582",
                            Nome = "Luís Freitas",
                            Sexo = "M"
                        },
                        new
                        {
                            Id = 2,
                            NIF = "854613462",
                            Nome = "Andreia Gomes",
                            Sexo = "F"
                        },
                        new
                        {
                            Id = 3,
                            NIF = "265368715",
                            Nome = "Cristina Sousa",
                            Sexo = "F"
                        },
                        new
                        {
                            Id = 4,
                            NIF = "835623190",
                            Nome = "Sónia Rosa",
                            Sexo = "F"
                        });
                });

            modelBuilder.Entity("Veterinarios.Models.MedicosVeterinarios", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Fotografia")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumCedulaProf")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Veterinarios");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Fotografia = "Jose.jpg",
                            Nome = "José Lopes",
                            NumCedulaProf = "Vet-8768"
                        },
                        new
                        {
                            Id = 2,
                            Fotografia = "Maria.jpg",
                            Nome = "Maria dos Santos",
                            NumCedulaProf = "Vet-2568"
                        },
                        new
                        {
                            Id = 3,
                            Fotografia = "Ricardo.jpg",
                            Nome = "Ricardo Gonçalo Silva",
                            NumCedulaProf = "Vet-2344"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Veterinarios.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Veterinarios.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Veterinarios.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Veterinarios.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Veterinarios.Models.Animais", b =>
                {
                    b.HasOne("Veterinarios.Models.Donos", "Dono")
                        .WithMany("ListaAnimais")
                        .HasForeignKey("DonoFK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dono");
                });

            modelBuilder.Entity("Veterinarios.Models.Consultas", b =>
                {
                    b.HasOne("Veterinarios.Models.Animais", "Animal")
                        .WithMany("ListaConsultas")
                        .HasForeignKey("AnimalFK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Veterinarios.Models.MedicosVeterinarios", "Veterinario")
                        .WithMany("ListaConsultas")
                        .HasForeignKey("VeterinarioFK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Animal");

                    b.Navigation("Veterinario");
                });

            modelBuilder.Entity("Veterinarios.Models.Animais", b =>
                {
                    b.Navigation("ListaConsultas");
                });

            modelBuilder.Entity("Veterinarios.Models.Donos", b =>
                {
                    b.Navigation("ListaAnimais");
                });

            modelBuilder.Entity("Veterinarios.Models.MedicosVeterinarios", b =>
                {
                    b.Navigation("ListaConsultas");
                });
#pragma warning restore 612, 618
        }
    }
}
