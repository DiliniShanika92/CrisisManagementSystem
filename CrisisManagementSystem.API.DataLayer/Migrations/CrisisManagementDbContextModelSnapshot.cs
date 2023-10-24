﻿// <auto-generated />
using CrisisManagementSystem.API.DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CrisisManagementSystem.API.Migrations
{
    [DbContext(typeof(CrisisManagementDbContext))]
    partial class CrisisManagementDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CrisisManagementSystem.API.DataLayer.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DeptHeadId")
                        .HasColumnType("int");

                    b.Property<string>("DeptName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UsersId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("CrisisManagementSystem.API.DataLayer.Incident", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripton")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IncidentTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ReporterId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ReporterId");

                    b.ToTable("Incidents");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Descripton = "very exapansive",
                            IncidentTypeId = 0,
                            Location = "MainBuilding",
                            Name = "fire in office",
                            ReporterId = 1
                        },
                        new
                        {
                            Id = 2,
                            Descripton = "very exapansive",
                            IncidentTypeId = 0,
                            Location = "Whole",
                            Name = "Flood in office",
                            ReporterId = 1
                        },
                        new
                        {
                            Id = 3,
                            Descripton = "very exapansive",
                            IncidentTypeId = 0,
                            Location = "Whole",
                            Name = "CyberAttack",
                            ReporterId = 1
                        });
                });

            modelBuilder.Entity("CrisisManagementSystem.API.DataLayer.IncidentMedia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("IncidentId")
                        .HasColumnType("int");

                    b.Property<int>("MediaType")
                        .HasColumnType("int");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IncidentId");

                    b.ToTable("IncidentMedia");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IncidentId = 1,
                            MediaType = 0,
                            Path = "/Crisis/Images/image1.jpg"
                        },
                        new
                        {
                            Id = 2,
                            IncidentId = 1,
                            MediaType = 0,
                            Path = "/Crisis/Images/image2.jpg"
                        },
                        new
                        {
                            Id = 3,
                            IncidentId = 1,
                            MediaType = 0,
                            Path = "/Crisis/Images/image3.jpg"
                        },
                        new
                        {
                            Id = 4,
                            IncidentId = 1,
                            MediaType = 0,
                            Path = "/Crisis/Images/image4.jpg"
                        });
                });

            modelBuilder.Entity("CrisisManagementSystem.API.DataLayer.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Password = "",
                            Role = "Ceo",
                            UserName = "ThomasRay"
                        },
                        new
                        {
                            Id = 2,
                            Password = "",
                            Role = "Excecutive",
                            UserName = "JhonDoe"
                        },
                        new
                        {
                            Id = 3,
                            Password = "",
                            Role = "Receptionist",
                            UserName = "JaneDoe"
                        });
                });

            modelBuilder.Entity("CrisisManagementSystem.API.DataLayer.Department", b =>
                {
                    b.HasOne("CrisisManagementSystem.API.DataLayer.User", "Users")
                        .WithMany("Departments")
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Users");
                });

            modelBuilder.Entity("CrisisManagementSystem.API.DataLayer.Incident", b =>
                {
                    b.HasOne("CrisisManagementSystem.API.DataLayer.User", "Reporter")
                        .WithMany()
                        .HasForeignKey("ReporterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Reporter");
                });

            modelBuilder.Entity("CrisisManagementSystem.API.DataLayer.IncidentMedia", b =>
                {
                    b.HasOne("CrisisManagementSystem.API.DataLayer.Incident", "Incident")
                        .WithMany("Media")
                        .HasForeignKey("IncidentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Incident");
                });

            modelBuilder.Entity("CrisisManagementSystem.API.DataLayer.Incident", b =>
                {
                    b.Navigation("Media");
                });

            modelBuilder.Entity("CrisisManagementSystem.API.DataLayer.User", b =>
                {
                    b.Navigation("Departments");
                });
#pragma warning restore 612, 618
        }
    }
}
