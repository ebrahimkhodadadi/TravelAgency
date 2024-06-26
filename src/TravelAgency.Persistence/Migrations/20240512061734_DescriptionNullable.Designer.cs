﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TravelAgency.Persistence.Framework;

#nullable disable

namespace TravelAgency.Persistence.Migrations
{
    [DbContext(typeof(TravelAgencyDbContext))]
    [Migration("20240512061734_DescriptionNullable")]
    partial class DescriptionNullable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TravelAgency.Domain.Billing.Bill", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("Char(26)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("VarChar(30)");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("DateTimeOffset(2)");

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasColumnType("Char(26)");

                    b.Property<bool>("SoftDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("Bit")
                        .HasDefaultValue(false);

                    b.Property<DateTimeOffset?>("SoftDeletedOn")
                        .HasColumnType("DateTimeOffset(2)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("VarChar(10)")
                        .HasDefaultValue("InProgress")
                        .HasColumnName("BillStatus");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("VarChar(30)");

                    b.Property<DateTimeOffset?>("UpdatedOn")
                        .IsConcurrencyToken()
                        .HasColumnType("DateTimeOffset(7)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Bill", "TravelAgency");
                });

            modelBuilder.Entity("TravelAgency.Domain.Billing.DiscountLog", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("Char(26)");

                    b.Property<string>("BillId")
                        .IsRequired()
                        .HasColumnType("Char(26)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("Price")
                        .HasColumnType("int")
                        .HasColumnName("Price");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("UpdatedOn")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("BillId");

                    b.ToTable("DiscountLog", "TravelAgency");
                });

            modelBuilder.Entity("TravelAgency.Domain.Billing.Payment", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("Char(26)");

                    b.Property<string>("BillId")
                        .IsRequired()
                        .HasColumnType("Char(26)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentType")
                        .IsRequired()
                        .HasColumnType("VarChar(8)")
                        .HasColumnName("PaymentType");

                    b.Property<int>("Price")
                        .HasColumnType("int")
                        .HasColumnName("Price");

                    b.Property<string>("TransferId")
                        .HasColumnType("Char(26)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("UpdatedOn")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("BillId");

                    b.ToTable("Payment", "TravelAgency");
                });

            modelBuilder.Entity("TravelAgency.Domain.Billing.Travel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("Char(26)");

                    b.Property<string>("BillId")
                        .IsRequired()
                        .HasColumnType("Char(26)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("Price")
                        .HasColumnType("int")
                        .HasColumnName("Price");

                    b.Property<DateTimeOffset>("Start")
                        .HasColumnType("DateTimeOffset(2)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("VarChar(6)")
                        .HasColumnName("TravelType");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("UpdatedOn")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("BillId");

                    b.ToTable("Travel", "TravelAgency");
                });

            modelBuilder.Entity("TravelAgency.Domain.Users.Customer", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("Char(26)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("VarChar(30)");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("DateTimeOffset(2)");

                    b.Property<DateTimeOffset?>("DateOfBirth")
                        .HasColumnType("DateTimeOffset(2)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("FirstName");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("VarChar(6)")
                        .HasColumnName("Gender");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("LastName");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("PhoneNumber");

                    b.Property<string>("Rank")
                        .IsRequired()
                        .HasColumnType("VarChar(6)")
                        .HasColumnName("Rank");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("VarChar(30)");

                    b.Property<DateTimeOffset?>("UpdatedOn")
                        .IsConcurrencyToken()
                        .HasColumnType("DateTimeOffset(7)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("Char(26)");

                    b.HasKey("Id");

                    b.ToTable("Customer", "Master");
                });

            modelBuilder.Entity("TravelAgency.Domain.Users.Enumerations.Permission", b =>
                {
                    b.Property<byte>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TinyInt");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<byte>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VarChar(128)");

                    b.HasKey("Id");

                    b.ToTable("Permission", "Master");

                    b.HasData(
                        new
                        {
                            Id = (byte)1,
                            Name = "Review_Read"
                        },
                        new
                        {
                            Id = (byte)2,
                            Name = "Review_Add"
                        },
                        new
                        {
                            Id = (byte)3,
                            Name = "Review_Update"
                        },
                        new
                        {
                            Id = (byte)4,
                            Name = "Review_Remove"
                        },
                        new
                        {
                            Id = (byte)5,
                            Name = "INVALID_PERMISSION"
                        });
                });

            modelBuilder.Entity("TravelAgency.Domain.Users.Enumerations.Role", b =>
                {
                    b.Property<byte>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TinyInt");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<byte>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VarChar(128)");

                    b.HasKey("Id");

                    b.ToTable("Role", "Master");

                    b.HasData(
                        new
                        {
                            Id = (byte)1,
                            Name = "Customer"
                        },
                        new
                        {
                            Id = (byte)2,
                            Name = "Employee"
                        },
                        new
                        {
                            Id = (byte)3,
                            Name = "Manager"
                        },
                        new
                        {
                            Id = (byte)4,
                            Name = "Administrator"
                        });
                });

            modelBuilder.Entity("TravelAgency.Domain.Users.Enumerations.RolePermission", b =>
                {
                    b.Property<byte>("RoleId")
                        .HasColumnType("TinyInt");

                    b.Property<byte>("PermissionId")
                        .HasColumnType("TinyInt");

                    b.HasKey("RoleId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.ToTable("RolePermission", "Master");

                    b.HasData(
                        new
                        {
                            RoleId = (byte)4,
                            PermissionId = (byte)1
                        },
                        new
                        {
                            RoleId = (byte)4,
                            PermissionId = (byte)2
                        },
                        new
                        {
                            RoleId = (byte)4,
                            PermissionId = (byte)3
                        },
                        new
                        {
                            RoleId = (byte)4,
                            PermissionId = (byte)4
                        },
                        new
                        {
                            RoleId = (byte)3,
                            PermissionId = (byte)1
                        },
                        new
                        {
                            RoleId = (byte)3,
                            PermissionId = (byte)2
                        },
                        new
                        {
                            RoleId = (byte)3,
                            PermissionId = (byte)3
                        },
                        new
                        {
                            RoleId = (byte)2,
                            PermissionId = (byte)1
                        },
                        new
                        {
                            RoleId = (byte)1,
                            PermissionId = (byte)1
                        },
                        new
                        {
                            RoleId = (byte)1,
                            PermissionId = (byte)2
                        },
                        new
                        {
                            RoleId = (byte)1,
                            PermissionId = (byte)3
                        });
                });

            modelBuilder.Entity("TravelAgency.Domain.Users.RoleUser", b =>
                {
                    b.Property<byte>("RoleId")
                        .HasColumnType("TinyInt");

                    b.Property<string>("UserId")
                        .HasColumnType("Char(26)");

                    b.HasKey("RoleId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("RoleUser", "Master");
                });

            modelBuilder.Entity("TravelAgency.Domain.Users.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("Char(26)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("VarChar(30)");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("DateTimeOffset(2)");

                    b.Property<string>("CustomerId")
                        .HasColumnType("Char(26)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)")
                        .HasColumnName("Email");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("NChar(514)")
                        .HasColumnName("PasswordHash");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("VarChar(32)")
                        .HasColumnName("RefreshToken");

                    b.Property<DateTimeOffset?>("TwoFactorTokenCreatedOn")
                        .HasColumnType("DateTimeOffset(2)");

                    b.Property<string>("TwoFactorTokenHash")
                        .HasColumnType("NChar(514)")
                        .HasColumnName("TwoFactorTokenHash");

                    b.Property<string>("TwoFactorToptSecret")
                        .HasColumnType("Char(32)")
                        .HasColumnName("TwoFactorToptSecret");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("VarChar(30)");

                    b.Property<DateTimeOffset?>("UpdatedOn")
                        .IsConcurrencyToken()
                        .HasColumnType("DateTimeOffset(7)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("Username");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId")
                        .IsUnique()
                        .HasFilter("[CustomerId] IS NOT NULL");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("UX_User_Email");

                    b.HasIndex("Username")
                        .IsUnique()
                        .HasDatabaseName("UX_Username_Email");

                    SqlServerIndexBuilderExtensions.IncludeProperties(b.HasIndex("Username"), new[] { "Email" });

                    b.ToTable("User", "Master");
                });

            modelBuilder.Entity("TravelAgency.Infrastructure.Outbox.OutboxMessage", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("Char(26)");

                    b.Property<int>("AttemptCount")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("VarChar(5000)");

                    b.Property<string>("Error")
                        .HasColumnType("VarChar(8000)");

                    b.Property<string>("ExecutionStatus")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("VarChar(10)")
                        .HasDefaultValue("InProgress");

                    b.Property<DateTimeOffset?>("NextProcessAttempt")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("OccurredOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("ProcessedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("VarChar(100)");

                    b.HasKey("Id");

                    b.HasIndex("ExecutionStatus")
                        .HasDatabaseName("IX_OutboxMessage_ExecutionStatus")
                        .HasFilter("[ExecutionStatus] = 'InProgress'");

                    b.ToTable("OutboxMessage", "Outbox");
                });

            modelBuilder.Entity("TravelAgency.Infrastructure.Outbox.OutboxMessageConsumer", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("Char(26)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id", "Name");

                    b.ToTable("OutboxMessageConsumer", "Outbox");
                });

            modelBuilder.Entity("TravelAgency.Domain.Billing.Bill", b =>
                {
                    b.HasOne("TravelAgency.Domain.Users.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("TravelAgency.Domain.Billing.DiscountLog", b =>
                {
                    b.HasOne("TravelAgency.Domain.Billing.Bill", null)
                        .WithMany("DiscountLogs")
                        .HasForeignKey("BillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TravelAgency.Domain.Billing.Payment", b =>
                {
                    b.HasOne("TravelAgency.Domain.Billing.Bill", null)
                        .WithMany("Payments")
                        .HasForeignKey("BillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TravelAgency.Domain.Billing.Travel", b =>
                {
                    b.HasOne("TravelAgency.Domain.Billing.Bill", null)
                        .WithMany("Travels")
                        .HasForeignKey("BillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("TravelAgency.Domain.Billing.ValueObjects.Direction", "Direction", b1 =>
                        {
                            b1.Property<string>("TravelId")
                                .HasColumnType("Char(26)");

                            b1.Property<string>("Destination")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Origin")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("TravelId");

                            b1.ToTable("Travel", "TravelAgency");

                            b1.WithOwner()
                                .HasForeignKey("TravelId");
                        });

                    b.Navigation("Direction")
                        .IsRequired();
                });

            modelBuilder.Entity("TravelAgency.Domain.Users.Customer", b =>
                {
                    b.OwnsOne("TravelAgency.Domain.Users.ValueObjects.Address", "Address", b1 =>
                        {
                            b1.Property<string>("Id")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<int>("Building")
                                .HasMaxLength(1000)
                                .HasColumnType("int");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<string>("CustomerId")
                                .IsRequired()
                                .HasColumnType("Char(26)");

                            b1.Property<int?>("Flat")
                                .HasMaxLength(1000)
                                .HasColumnType("int");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasMaxLength(5)
                                .HasColumnType("nvarchar(5)");

                            b1.HasKey("Id");

                            b1.HasIndex("CustomerId")
                                .IsUnique();

                            b1.ToTable("Address", "Master");

                            b1.WithOwner()
                                .HasForeignKey("CustomerId");
                        });

                    b.OwnsOne("TravelAgency.Domain.Users.ValueObjects.DebtLimit", "DebtLimit", b1 =>
                        {
                            b1.Property<string>("CustomerId")
                                .HasColumnType("Char(26)");

                            b1.Property<int>("Value")
                                .HasColumnType("int")
                                .HasColumnName("DebtLimit");

                            b1.HasKey("CustomerId");

                            b1.ToTable("Customer", "Master");

                            b1.WithOwner()
                                .HasForeignKey("CustomerId");
                        });

                    b.Navigation("Address");

                    b.Navigation("DebtLimit");
                });

            modelBuilder.Entity("TravelAgency.Domain.Users.Enumerations.RolePermission", b =>
                {
                    b.HasOne("TravelAgency.Domain.Users.Enumerations.Permission", null)
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TravelAgency.Domain.Users.Enumerations.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TravelAgency.Domain.Users.RoleUser", b =>
                {
                    b.HasOne("TravelAgency.Domain.Users.Enumerations.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TravelAgency.Domain.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TravelAgency.Domain.Users.User", b =>
                {
                    b.HasOne("TravelAgency.Domain.Users.Customer", "Customer")
                        .WithOne("User")
                        .HasForeignKey("TravelAgency.Domain.Users.User", "CustomerId");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("TravelAgency.Domain.Billing.Bill", b =>
                {
                    b.Navigation("DiscountLogs");

                    b.Navigation("Payments");

                    b.Navigation("Travels");
                });

            modelBuilder.Entity("TravelAgency.Domain.Users.Customer", b =>
                {
                    b.Navigation("User")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
