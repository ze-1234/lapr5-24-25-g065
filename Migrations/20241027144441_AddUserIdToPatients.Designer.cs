﻿// <auto-generated />
using System;
using DDDSample1.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DDDNetCore.Migrations
{
    [DbContext(typeof(DDDSample1DbContext))]
    [Migration("20241027144441_AddUserIdToPatients")]
    partial class AddUserIdToPatients
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("DDDSample1.Domain.Appointments.Appointment", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)")
                        .HasColumnName("AppointmentId");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("PatientId")
                        .HasColumnType("char(36)");

                    b.Property<string>("RequestId")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("RoomId")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<Guid?>("StaffId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.HasIndex("StaffId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("DDDSample1.Domain.OperationTypes.OperationType", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)")
                        .HasColumnName("OperationTypeId");

                    b.Property<TimeSpan>("EstimatedDuration")
                        .HasMaxLength(500)
                        .HasColumnType("time(6)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("RequiredStaffBySpecialization")
                        .HasColumnType("longtext");

                    b.Property<Guid?>("SpecializationId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("SpecializationId");

                    b.ToTable("OperationTypes");
                });

            modelBuilder.Entity("DDDSample1.Domain.Patients.Patient", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)")
                        .HasColumnName("PatientId");

                    b.Property<string>("Allergies")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("DateOfBirth")
                        .IsRequired()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("EmergencyContact")
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("MedicalRecordNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("DDDSample1.Domain.Specializations.Specialization", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)")
                        .HasColumnName("SpecializationId");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Specializations");
                });

            modelBuilder.Entity("DDDSample1.Domain.Staffs.Staff", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)")
                        .HasColumnName("StaffId");

                    b.Property<string>("AvailabilitySlot")
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("LicenseNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<Guid?>("SpecializationId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("SpecializationId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Staffs");
                });

            modelBuilder.Entity("DDDSample1.Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)")
                        .HasColumnName("UserId");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DDDSample1.Domain.Appointments.Appointment", b =>
                {
                    b.HasOne("DDDSample1.Domain.Patients.Patient", null)
                        .WithMany("AppointmentHistory")
                        .HasForeignKey("PatientId");

                    b.HasOne("DDDSample1.Domain.Staffs.Staff", null)
                        .WithMany("Appointments")
                        .HasForeignKey("StaffId");
                });

            modelBuilder.Entity("DDDSample1.Domain.OperationTypes.OperationType", b =>
                {
                    b.HasOne("DDDSample1.Domain.Specializations.Specialization", "Specialization")
                        .WithMany()
                        .HasForeignKey("SpecializationId");

                    b.Navigation("Specialization");
                });

            modelBuilder.Entity("DDDSample1.Domain.Staffs.Staff", b =>
                {
                    b.HasOne("DDDSample1.Domain.Specializations.Specialization", "Specialization")
                        .WithMany("StaffMembers")
                        .HasForeignKey("SpecializationId");

                    b.HasOne("DDDSample1.Domain.Users.User", "User")
                        .WithOne()
                        .HasForeignKey("DDDSample1.Domain.Staffs.Staff", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Specialization");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DDDSample1.Domain.Users.User", b =>
                {
                    b.OwnsOne("DDDSample1.Domain.Passwords.Password", "Password", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("char(36)");

                            b1.Property<string>("Pass")
                                .HasColumnType("longtext");

                            b1.HasKey("UserId");

                            b1.ToTable("Passwords");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("Password");
                });

            modelBuilder.Entity("DDDSample1.Domain.Patients.Patient", b =>
                {
                    b.Navigation("AppointmentHistory");
                });

            modelBuilder.Entity("DDDSample1.Domain.Specializations.Specialization", b =>
                {
                    b.Navigation("StaffMembers");
                });

            modelBuilder.Entity("DDDSample1.Domain.Staffs.Staff", b =>
                {
                    b.Navigation("Appointments");
                });
#pragma warning restore 612, 618
        }
    }
}
