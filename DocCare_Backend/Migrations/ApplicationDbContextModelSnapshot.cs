﻿// <auto-generated />
using System;
using DocCare_Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DocCare_Backend.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DocCare_Backend.Models.Consultation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Date")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<byte[]>("DossierMedical")
                        .HasColumnType("longblob");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Status")
                        .HasColumnType("longtext");

                    b.Property<string>("Time")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.ToTable("Consultations");
                });

            modelBuilder.Entity("DocCare_Backend.Models.Docteur", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Adresse")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NumeroTelephone")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Photo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("R_Token")
                        .HasColumnType("longtext");

                    b.Property<int>("SpecialiteId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SpecialiteId");

                    b.ToTable("Docteurs");
                });

            modelBuilder.Entity("DocCare_Backend.Models.DocteurDisponibilite", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("DocteurId")
                        .HasColumnType("int");

                    b.Property<string>("Time")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("DocteurId");

                    b.ToTable("DocteursDisponibilites");
                });

            modelBuilder.Entity("DocCare_Backend.Models.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Adresse")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("DateN")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<byte[]>("DossierMedical")
                        .HasColumnType("longblob");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Num")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("DocCare_Backend.Models.Specialite", b =>
                {
                    b.Property<int>("SpecialiteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ImagePath")
                        .HasColumnType("longtext");

                    b.Property<string>("Nom")
                        .HasColumnType("longtext");

                    b.HasKey("SpecialiteId");

                    b.ToTable("Specialites");
                });

            modelBuilder.Entity("DocCare_Backend.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("R_Token")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DocCare_Backend.Models.UserLogin", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Email");

                    b.ToTable("UserLogins");
                });

            modelBuilder.Entity("DocCare_Backend.Models.Consultation", b =>
                {
                    b.HasOne("DocCare_Backend.Models.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("DocCare_Backend.Models.Docteur", b =>
                {
                    b.HasOne("DocCare_Backend.Models.Specialite", "Specialite")
                        .WithMany("Docteurs")
                        .HasForeignKey("SpecialiteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Specialite");
                });

            modelBuilder.Entity("DocCare_Backend.Models.DocteurDisponibilite", b =>
                {
                    b.HasOne("DocCare_Backend.Models.Docteur", "Docteur")
                        .WithMany("DocteursDisponibilites")
                        .HasForeignKey("DocteurId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Docteur");
                });

            modelBuilder.Entity("DocCare_Backend.Models.Docteur", b =>
                {
                    b.Navigation("DocteursDisponibilites");
                });

            modelBuilder.Entity("DocCare_Backend.Models.Specialite", b =>
                {
                    b.Navigation("Docteurs");
                });
#pragma warning restore 612, 618
        }
    }
}
