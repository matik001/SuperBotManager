﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SuperBotManagerBase.DB;

#nullable disable

namespace SuperBotManagerBackend.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20240204233500_AddedCascadeDeleteToActionExecutorOnFinish")]
    partial class AddedCascadeDeleteToActionExecutorOnFinish
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("SuperBotManagerBase.DB.Repositories.Action", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ActionData")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("ActionExecutorId")
                        .HasColumnType("int");

                    b.Property<int>("ActionStatus")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("ErrorId")
                        .HasColumnType("int");

                    b.Property<int?>("ForwardedFromActionId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("RunStartType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ActionExecutorId");

                    b.HasIndex("ForwardedFromActionId")
                        .IsUnique();

                    b.ToTable("action");
                });

            modelBuilder.Entity("SuperBotManagerBase.DB.Repositories.ActionDefinition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ActionDataSchema")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ActionDefinitionDescription")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ActionDefinitionGroup")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ActionDefinitionIcon")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ActionDefinitionName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ActionDefinitionQueueName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("PreserveExecutedInputs")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("actiondefinition");
                });

            modelBuilder.Entity("SuperBotManagerBase.DB.Repositories.ActionExecutor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ActionData")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("ActionDefinitionId")
                        .HasColumnType("int");

                    b.Property<string>("ActionExecutorName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("ActionExecutorOnFinishId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsValid")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("LastRunDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("PreserveExecutedInputs")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("RunMethod")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ActionDefinitionId");

                    b.HasIndex("ActionExecutorOnFinishId");

                    b.ToTable("actionexecutor");
                });

            modelBuilder.Entity("SuperBotManagerBase.DB.Repositories.ActionSchedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ActionSCheduleName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("Enabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("ExecutorId")
                        .HasColumnType("int");

                    b.Property<int>("IntervalSec")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("NextRun")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ExecutorId");

                    b.ToTable("actionschedule");
                });

            modelBuilder.Entity("SuperBotManagerBase.DB.Repositories.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("RefreshTokenId");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("refreshtoken");
                });

            modelBuilder.Entity("SuperBotManagerBase.DB.Repositories.RevokedToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("RevokedTokenId");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("TokenGuid")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("revokedtokens");
                });

            modelBuilder.Entity("SuperBotManagerBase.DB.Repositories.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("RoleId");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("role");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            RoleName = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            RoleName = "Blocked"
                        });
                });

            modelBuilder.Entity("SuperBotManagerBase.DB.Repositories.Secret", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<byte[]>("SecretIV")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.Property<byte[]>("SecretValue")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.HasKey("Id");

                    b.ToTable("secret");
                });

            modelBuilder.Entity("SuperBotManagerBase.DB.Repositories.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("UserId");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("user");
                });

            modelBuilder.Entity("SuperBotManagerBase.DB.Repositories.UserPassword", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PasswordId");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("PasswordDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("PasswordUserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PasswordUserId");

                    b.ToTable("userpassword");
                });

            modelBuilder.Entity("SuperBotManagerBase.DB.Repositories.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("userrole", (string)null);
                });

            modelBuilder.Entity("SuperBotManagerBase.DB.Repositories.VaultItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("FieldName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.Property<Guid?>("SecretId")
                        .HasColumnType("char(36)");

                    b.Property<string>("VaultGroupName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("SecretId");

                    b.ToTable("vaultitem");
                });

            modelBuilder.Entity("SuperBotManagerBase.DB.Repositories.Action", b =>
                {
                    b.HasOne("SuperBotManagerBase.DB.Repositories.ActionExecutor", "ActionExecutor")
                        .WithMany("Actions")
                        .HasForeignKey("ActionExecutorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SuperBotManagerBase.DB.Repositories.Action", "ForwardedFromAction")
                        .WithOne()
                        .HasForeignKey("SuperBotManagerBase.DB.Repositories.Action", "ForwardedFromActionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("ActionExecutor");

                    b.Navigation("ForwardedFromAction");
                });

            modelBuilder.Entity("SuperBotManagerBase.DB.Repositories.ActionExecutor", b =>
                {
                    b.HasOne("SuperBotManagerBase.DB.Repositories.ActionDefinition", "ActionDefinition")
                        .WithMany("ActionExecutors")
                        .HasForeignKey("ActionDefinitionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SuperBotManagerBase.DB.Repositories.ActionExecutor", "ActionExecutorOnFinish")
                        .WithMany()
                        .HasForeignKey("ActionExecutorOnFinishId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("ActionDefinition");

                    b.Navigation("ActionExecutorOnFinish");
                });

            modelBuilder.Entity("SuperBotManagerBase.DB.Repositories.ActionSchedule", b =>
                {
                    b.HasOne("SuperBotManagerBase.DB.Repositories.ActionExecutor", "Executor")
                        .WithMany("Schedules")
                        .HasForeignKey("ExecutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Executor");
                });

            modelBuilder.Entity("SuperBotManagerBase.DB.Repositories.RefreshToken", b =>
                {
                    b.HasOne("SuperBotManagerBase.DB.Repositories.User", "User")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SuperBotManagerBase.DB.Repositories.RevokedToken", b =>
                {
                    b.HasOne("SuperBotManagerBase.DB.Repositories.User", "User")
                        .WithMany("RevokedTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SuperBotManagerBase.DB.Repositories.UserPassword", b =>
                {
                    b.HasOne("SuperBotManagerBase.DB.Repositories.User", "User")
                        .WithMany("UserPasswords")
                        .HasForeignKey("PasswordUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SuperBotManagerBase.DB.Repositories.UserRole", b =>
                {
                    b.HasOne("SuperBotManagerBase.DB.Repositories.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SuperBotManagerBase.DB.Repositories.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SuperBotManagerBase.DB.Repositories.VaultItem", b =>
                {
                    b.HasOne("SuperBotManagerBase.DB.Repositories.User", "Owner")
                        .WithMany("VaultItems")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SuperBotManagerBase.DB.Repositories.Secret", "Secret")
                        .WithMany()
                        .HasForeignKey("SecretId");

                    b.Navigation("Owner");

                    b.Navigation("Secret");
                });

            modelBuilder.Entity("SuperBotManagerBase.DB.Repositories.ActionDefinition", b =>
                {
                    b.Navigation("ActionExecutors");
                });

            modelBuilder.Entity("SuperBotManagerBase.DB.Repositories.ActionExecutor", b =>
                {
                    b.Navigation("Actions");

                    b.Navigation("Schedules");
                });

            modelBuilder.Entity("SuperBotManagerBase.DB.Repositories.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("SuperBotManagerBase.DB.Repositories.User", b =>
                {
                    b.Navigation("RefreshTokens");

                    b.Navigation("RevokedTokens");

                    b.Navigation("UserPasswords");

                    b.Navigation("UserRoles");

                    b.Navigation("VaultItems");
                });
#pragma warning restore 612, 618
        }
    }
}
