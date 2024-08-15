﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NodaTime;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SSO.Database;

#nullable disable

namespace SSO.Database.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SSO.Entities.AuthData", b =>
                {
                    b.Property<long>("AuthDataId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("AuthDataId"));

                    b.Property<Instant>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<long>("ServiceId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("AuthDataId");

                    b.HasIndex("ServiceId");

                    b.HasIndex("UserId");

                    b.ToTable("AuthDatas");
                });

            modelBuilder.Entity("SSO.Entities.Group", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasAlternateKey("Name");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("SSO.Entities.GroupRole", b =>
                {
                    b.Property<long>("GroupId")
                        .HasColumnType("bigint");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.Property<long>("AddedByUserId")
                        .HasColumnType("bigint");

                    b.HasKey("GroupId", "RoleId");

                    b.HasIndex("AddedByUserId");

                    b.HasIndex("RoleId");

                    b.ToTable("GroupRoles");
                });

            modelBuilder.Entity("SSO.Entities.Permission", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("SSO.Entities.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasAlternateKey("Name");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("SSO.Entities.RolePermission", b =>
                {
                    b.Property<long>("PermissionId")
                        .HasColumnType("bigint");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.Property<long>("AddedByUserId")
                        .HasColumnType("bigint");

                    b.HasKey("PermissionId", "RoleId");

                    b.HasIndex("AddedByUserId");

                    b.HasIndex("RoleId");

                    b.ToTable("RolePermissions");
                });

            modelBuilder.Entity("SSO.Entities.Service", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<Instant>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasAlternateKey("Name");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("SSO.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("FullName")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<Instant>("RegisteredAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<byte[]>("Salt")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("bytea");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasAlternateKey("Email");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SSO.Entities.UserGroup", b =>
                {
                    b.Property<long>("GroupId")
                        .HasColumnType("bigint");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<long>("InvitedByUserId")
                        .HasColumnType("bigint");

                    b.HasKey("GroupId", "UserId");

                    b.HasIndex("InvitedByUserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserGroups");
                });

            modelBuilder.Entity("SSO.Entities.AuthData", b =>
                {
                    b.HasOne("SSO.Entities.Service", "Service")
                        .WithMany("AuthDatas")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SSO.Entities.User", "User")
                        .WithMany("AuthDatas")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Service");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SSO.Entities.GroupRole", b =>
                {
                    b.HasOne("SSO.Entities.User", "AddedByUser")
                        .WithMany()
                        .HasForeignKey("AddedByUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SSO.Entities.Group", "Group")
                        .WithMany("GroupRoles")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SSO.Entities.Role", "Role")
                        .WithMany("GroupRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AddedByUser");

                    b.Navigation("Group");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("SSO.Entities.RolePermission", b =>
                {
                    b.HasOne("SSO.Entities.User", "AddedByUser")
                        .WithMany()
                        .HasForeignKey("AddedByUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SSO.Entities.Permission", "Permission")
                        .WithMany("RolePermissions")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SSO.Entities.Role", "Role")
                        .WithMany("RolePermissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AddedByUser");

                    b.Navigation("Permission");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("SSO.Entities.UserGroup", b =>
                {
                    b.HasOne("SSO.Entities.Group", "Group")
                        .WithMany("UserGroups")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SSO.Entities.User", "InvitedByUser")
                        .WithMany()
                        .HasForeignKey("InvitedByUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SSO.Entities.User", "User")
                        .WithMany("UserGroups")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("InvitedByUser");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SSO.Entities.Group", b =>
                {
                    b.Navigation("GroupRoles");

                    b.Navigation("UserGroups");
                });

            modelBuilder.Entity("SSO.Entities.Permission", b =>
                {
                    b.Navigation("RolePermissions");
                });

            modelBuilder.Entity("SSO.Entities.Role", b =>
                {
                    b.Navigation("GroupRoles");

                    b.Navigation("RolePermissions");
                });

            modelBuilder.Entity("SSO.Entities.Service", b =>
                {
                    b.Navigation("AuthDatas");
                });

            modelBuilder.Entity("SSO.Entities.User", b =>
                {
                    b.Navigation("AuthDatas");

                    b.Navigation("UserGroups");
                });
#pragma warning restore 612, 618
        }
    }
}
