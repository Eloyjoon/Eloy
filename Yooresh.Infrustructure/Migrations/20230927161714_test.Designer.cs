﻿// <auto-generated />
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Yooresh.Infrastructure.Persistence;

#nullable disable

namespace Yooresh.Infrastructure.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20230927161714_test")]
    partial class test
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Yooresh.Domain.Entities.Faction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Faction");
                });

            modelBuilder.Entity("Yooresh.Domain.Entities.Players.Player", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Confirmed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(320)
                        .HasColumnType("nvarchar(320)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasAnnotation("Microsoft.EntityFrameworkCore.DataEncryption.IsEncrypted", true)
                        .HasAnnotation("Microsoft.EntityFrameworkCore.DataEncryption.StorageFormat", StorageFormat.Default);

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(24)");

                    b.HasKey("Id");

                    b.ToTable("Player", (string)null);
                });

            modelBuilder.Entity("Yooresh.Domain.Entities.Villages.Village", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FactionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("PlayerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("FactionId");

                    b.HasIndex("PlayerId")
                        .IsUnique();

                    b.ToTable("Village", (string)null);
                });

            modelBuilder.Entity("Yooresh.Domain.Entities.Villages.Village", b =>
                {
                    b.HasOne("Yooresh.Domain.Entities.Faction", "Faction")
                        .WithMany()
                        .HasForeignKey("FactionId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Yooresh.Domain.Entities.Players.Player", "Player")
                        .WithOne()
                        .HasForeignKey("Yooresh.Domain.Entities.Villages.Village", "PlayerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.OwnsOne("Yooresh.Domain.Entities.Villages.Resource", "Resource", b1 =>
                        {
                            b1.Property<Guid>("VillageId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Food")
                                .HasColumnType("int")
                                .HasColumnName("ResourceFood");

                            b1.Property<int>("Gold")
                                .HasColumnType("int")
                                .HasColumnName("ResourceGold");

                            b1.Property<int>("Lumber")
                                .HasColumnType("int")
                                .HasColumnName("ResourceLumber");

                            b1.Property<int>("Metal")
                                .HasColumnType("int")
                                .HasColumnName("ResourceMetal");

                            b1.Property<int>("Stone")
                                .HasColumnType("int")
                                .HasColumnName("ResourceStone");

                            b1.HasKey("VillageId");

                            b1.ToTable("Village");

                            b1.WithOwner()
                                .HasForeignKey("VillageId");
                        });

                    b.Navigation("Faction");

                    b.Navigation("Player");

                    b.Navigation("Resource")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
