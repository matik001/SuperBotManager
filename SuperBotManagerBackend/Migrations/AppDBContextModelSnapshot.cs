﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SuperBotManagerBase.DB;

#nullable disable

namespace SuperBotManagerBackend.Migrations
{
    [DbContext(typeof(AppDBContext))]
    partial class AppDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SuperBotManagerBase.DB.Repositories.Action", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ActionData")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("ActionExecutorId")
                        .HasColumnType("integer");

                    b.Property<int>("ActionStatus")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("ErrorId")
                        .HasColumnType("integer");

                    b.Property<int?>("ForwardedFromActionId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("RunStartType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ActionExecutorId");

                    b.HasIndex("ForwardedFromActionId");

                    b.ToTable("action");
                });

            modelBuilder.Entity("SuperBotManagerBase.DB.Repositories.ActionDefinition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ActionDataSchema")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ActionDefinitionDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ActionDefinitionGroup")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ActionDefinitionIcon")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ActionDefinitionName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ActionDefinitionQueueName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("PreserveExecutedInputs")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("actiondefinition");
                });

            modelBuilder.Entity("SuperBotManagerBase.DB.Repositories.ActionExecutor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ActionData")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ActionDefinitionId")
                        .HasColumnType("integer");

                    b.Property<string>("ActionExecutorName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("ActionExecutorOnFinishId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsValid")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("LastRunDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("PreserveExecutedInputs")
                        .HasColumnType("boolean");

                    b.Property<int>("RunMethod")
                        .HasColumnType("integer");

                    b.Property<int?>("TimeIntervalSeconds")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ActionDefinitionId");

                    b.HasIndex("ActionExecutorOnFinishId");

                    b.ToTable("actionexecutor");
                });

            modelBuilder.Entity("SuperBotManagerBase.DB.Repositories.ActionSchedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ActionSCheduleName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Enabled")
                        .HasColumnType("boolean");

                    b.Property<int>("ExecutorId")
                        .HasColumnType("integer");

                    b.Property<int>("IntervalSec")
                        .HasColumnType("integer");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("NextRun")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ExecutorId");

                    b.ToTable("actionschedule");
                });

            modelBuilder.Entity("SuperBotManagerBase.DB.Repositories.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("RefreshTokenId");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("refreshtoken");
                });

            modelBuilder.Entity("SuperBotManagerBase.DB.Repositories.RevokedToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("RevokedTokenId");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("TokenGuid")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("revokedtokens");
                });

            modelBuilder.Entity("SuperBotManagerBase.DB.Repositories.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("RoleId");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("text");

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
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<byte[]>("SecretIV")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<byte[]>("SecretValue")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.ToTable("secret");
                });

            modelBuilder.Entity("SuperBotManagerBase.DB.Repositories.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("UserId");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("user");
                });

            modelBuilder.Entity("SuperBotManagerBase.DB.Repositories.UserPassword", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("PasswordId");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("PasswordDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PasswordUserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PasswordUserId");

                    b.ToTable("userpassword");
                });

            modelBuilder.Entity("SuperBotManagerBase.DB.Repositories.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("userrole", (string)null);
                });

            modelBuilder.Entity("SuperBotManagerBase.DB.Repositories.VaultItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FieldName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("OwnerId")
                        .HasColumnType("integer");

                    b.Property<Guid?>("SecretId")
                        .HasColumnType("uuid");

                    b.Property<string>("VaultGroupName")
                        .IsRequired()
                        .HasColumnType("text");

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
                        .WithMany()
                        .HasForeignKey("ForwardedFromActionId");

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
                        .OnDelete(DeleteBehavior.Restrict);

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
