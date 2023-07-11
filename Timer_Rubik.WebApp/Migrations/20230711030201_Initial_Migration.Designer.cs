﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Timer_Rubik.WebApp.Data;

#nullable disable

namespace Timer_Rubik.WebApp.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230711030201_Initial_Migration")]
    partial class Initial_Migration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Timer_Rubik.WebApp.Models.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("RuleId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Thumbnail")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("RuleId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Timer_Rubik.WebApp.Models.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Timer_Rubik.WebApp.Models.Favorite", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("ScrambleId")
                        .HasColumnType("char(36)");

                    b.Property<long>("Time")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("ScrambleId");

                    b.ToTable("Favorites");
                });

            modelBuilder.Entity("Timer_Rubik.WebApp.Models.Rule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Rules");
                });

            modelBuilder.Entity("Timer_Rubik.WebApp.Models.Scramble", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Algorithm")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Thumbnail")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Scrambles");
                });

            modelBuilder.Entity("Timer_Rubik.WebApp.Models.Solve", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("ScrambleId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("ScrambleId")
                        .IsUnique();

                    b.ToTable("Solves");
                });

            modelBuilder.Entity("Timer_Rubik.WebApp.Models.Account", b =>
                {
                    b.HasOne("Timer_Rubik.WebApp.Models.Rule", "Rule")
                        .WithMany("Accounts")
                        .HasForeignKey("RuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rule");
                });

            modelBuilder.Entity("Timer_Rubik.WebApp.Models.Favorite", b =>
                {
                    b.HasOne("Timer_Rubik.WebApp.Models.Account", "Account")
                        .WithMany("Favorites")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Timer_Rubik.WebApp.Models.Scramble", "Scramble")
                        .WithMany("Favorites")
                        .HasForeignKey("ScrambleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Scramble");
                });

            modelBuilder.Entity("Timer_Rubik.WebApp.Models.Scramble", b =>
                {
                    b.HasOne("Timer_Rubik.WebApp.Models.Account", "Account")
                        .WithMany("Scrambles")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Timer_Rubik.WebApp.Models.Category", "Category")
                        .WithMany("Scrambles")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Timer_Rubik.WebApp.Models.Solve", b =>
                {
                    b.HasOne("Timer_Rubik.WebApp.Models.Scramble", "Scramble")
                        .WithOne("Solve")
                        .HasForeignKey("Timer_Rubik.WebApp.Models.Solve", "ScrambleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Scramble");
                });

            modelBuilder.Entity("Timer_Rubik.WebApp.Models.Account", b =>
                {
                    b.Navigation("Favorites");

                    b.Navigation("Scrambles");
                });

            modelBuilder.Entity("Timer_Rubik.WebApp.Models.Category", b =>
                {
                    b.Navigation("Scrambles");
                });

            modelBuilder.Entity("Timer_Rubik.WebApp.Models.Rule", b =>
                {
                    b.Navigation("Accounts");
                });

            modelBuilder.Entity("Timer_Rubik.WebApp.Models.Scramble", b =>
                {
                    b.Navigation("Favorites");

                    b.Navigation("Solve")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
