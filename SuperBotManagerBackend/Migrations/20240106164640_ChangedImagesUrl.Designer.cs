﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SuperBotManagerBackend.DB;

#nullable disable

namespace SuperBotManagerBackend.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20240106164640_ChangedImagesUrl")]
    partial class ChangedImagesUrl
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.Property<int>("RolesId")
                        .HasColumnType("integer");

                    b.Property<int>("UsersId")
                        .HasColumnType("integer");

                    b.HasKey("RolesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("RoleUser");
                });

            modelBuilder.Entity("SuperBotManagerBackend.DB.Repositories.Action", b =>
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

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("ActionExecutorId");

                    b.ToTable("action");
                });

            modelBuilder.Entity("SuperBotManagerBackend.DB.Repositories.ActionDefinition", b =>
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

                    b.Property<string>("ActionDefinitionIcon")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ActionDefinitionName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("actiondefinition");

                    b.HasData(
                        new
                        {
                            Id = -1328912865,
                            ActionDataSchema = "{\"InputSchema\":[{\"Name\":\"Trip date\",\"Description\":\"What day do You want ticket for?\",\"Type\":2},{\"Name\":\"TicketOwner\",\"Description\":\"Owner of the ticket (real Firstname and Lastname)\",\"Type\":0},{\"Name\":\"From\",\"Description\":\"First station where you begin trip\",\"Type\":0},{\"Name\":\"To\",\"Description\":\"Last station - end of trip\",\"Type\":0},{\"Name\":\"Login\",\"Description\":\"Login for intercity\",\"Type\":0},{\"Name\":\"Password\",\"Description\":\"Password for intercity\",\"Type\":0},{\"Name\":\"Discount\",\"Description\":\"Pick your discount\",\"Type\":6,\"SetOptions\":[{\"Display\":\"None\",\"Value\":\"None\"},{\"Display\":\"Student\",\"Value\":\"Student\"}]}],\"OutputSchema\":[{\"Name\":\"Successful\",\"Description\":\"True if ticket was ordered. You have 10 minutes to pay for it.\",\"Type\":4},{\"Name\":\"Message\",\"Description\":\"Result message\",\"Type\":0}]}",
                            ActionDefinitionDescription = "Buy ticket for intercity",
                            ActionDefinitionIcon = "/intercity.jpg",
                            ActionDefinitionName = "IntercityBuyTicket",
                            CreatedDate = new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc),
                            ModifiedDate = new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc)
                        },
                        new
                        {
                            Id = -757997869,
                            ActionDataSchema = "{\"InputSchema\":[{\"Name\":\"Email\",\"Description\":\"Address email for your new account\",\"Type\":0},{\"Name\":\"Password\",\"Description\":\"Password for your new account\",\"Type\":0},{\"Name\":\"CardNumber\",\"Description\":\"Card number eg. 1234 1234 1234 1234\",\"Type\":0},{\"Name\":\"CardCCV\",\"Description\":\"Card CCV numer eg. 321\",\"Type\":1},{\"Name\":\"CardExpiration\",\"Description\":\"Card Expiration with format MM/YY eg. 07/25\",\"Type\":0}],\"OutputSchema\":[{\"Name\":\"Successful\",\"Description\":\"True if account was created\",\"Type\":4},{\"Name\":\"Message\",\"Description\":\"Result message\",\"Type\":0}]}",
                            ActionDefinitionDescription = "Create an account in storytel",
                            ActionDefinitionIcon = "/storytel.png",
                            ActionDefinitionName = "SignUpStorytel",
                            CreatedDate = new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc),
                            ModifiedDate = new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc)
                        });
                });

            modelBuilder.Entity("SuperBotManagerBackend.DB.Repositories.ActionExecutor", b =>
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

                    b.Property<DateTime?>("LastRunDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("RunPeriod")
                        .HasColumnType("integer");

                    b.Property<int?>("TimeIntervalSeconds")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ActionDefinitionId");

                    b.HasIndex("ActionExecutorOnFinishId");

                    b.ToTable("actionexecutor");
                });

            modelBuilder.Entity("SuperBotManagerBackend.DB.Repositories.RefreshToken", b =>
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

            modelBuilder.Entity("SuperBotManagerBackend.DB.Repositories.RevokedToken", b =>
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

            modelBuilder.Entity("SuperBotManagerBackend.DB.Repositories.Role", b =>
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

            modelBuilder.Entity("SuperBotManagerBackend.DB.Repositories.User", b =>
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

            modelBuilder.Entity("SuperBotManagerBackend.DB.Repositories.UserPassword", b =>
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

            modelBuilder.Entity("SuperBotManagerBackend.DB.Repositories.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("UserRoleId");

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

                    b.ToTable("userrole");
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.HasOne("SuperBotManagerBackend.DB.Repositories.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SuperBotManagerBackend.DB.Repositories.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SuperBotManagerBackend.DB.Repositories.Action", b =>
                {
                    b.HasOne("SuperBotManagerBackend.DB.Repositories.ActionExecutor", "ActionExecutor")
                        .WithMany()
                        .HasForeignKey("ActionExecutorId");

                    b.Navigation("ActionExecutor");
                });

            modelBuilder.Entity("SuperBotManagerBackend.DB.Repositories.ActionExecutor", b =>
                {
                    b.HasOne("SuperBotManagerBackend.DB.Repositories.ActionDefinition", "ActionDefinition")
                        .WithMany()
                        .HasForeignKey("ActionDefinitionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SuperBotManagerBackend.DB.Repositories.ActionExecutor", "ActionExecutorOnFinish")
                        .WithMany()
                        .HasForeignKey("ActionExecutorOnFinishId");

                    b.Navigation("ActionDefinition");

                    b.Navigation("ActionExecutorOnFinish");
                });

            modelBuilder.Entity("SuperBotManagerBackend.DB.Repositories.RefreshToken", b =>
                {
                    b.HasOne("SuperBotManagerBackend.DB.Repositories.User", "User")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SuperBotManagerBackend.DB.Repositories.RevokedToken", b =>
                {
                    b.HasOne("SuperBotManagerBackend.DB.Repositories.User", "User")
                        .WithMany("RevokedTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SuperBotManagerBackend.DB.Repositories.UserPassword", b =>
                {
                    b.HasOne("SuperBotManagerBackend.DB.Repositories.User", "User")
                        .WithMany("UserPasswords")
                        .HasForeignKey("PasswordUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SuperBotManagerBackend.DB.Repositories.UserRole", b =>
                {
                    b.HasOne("SuperBotManagerBackend.DB.Repositories.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SuperBotManagerBackend.DB.Repositories.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SuperBotManagerBackend.DB.Repositories.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("SuperBotManagerBackend.DB.Repositories.User", b =>
                {
                    b.Navigation("RefreshTokens");

                    b.Navigation("RevokedTokens");

                    b.Navigation("UserPasswords");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
