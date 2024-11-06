using Microsoft.EntityFrameworkCore;
using DDDSample1.Domain.Staffs;
using DDDSample1.Domain.Patients;
using DDDSample1.Domain.Users;
using DDDSample1.Domain.Appointments;
using DDDSample1.Domain.Passwords; 
using DDDNetCore.Domain.OperationRequestDomain;
using DDDSample1.Domain.OperationType; // Add the OperationType namespace
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using DDDNetCore.Domain;
using System;

namespace DDDSample1.Infrastructure
{
    public class DDDSample1DbContext : DbContext
    {
        public DDDSample1DbContext(DbContextOptions<DDDSample1DbContext> options) : base(options) { } // Fixed the type to DDDSample1DbContext


        public DbSet<Patient> Patients { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<OperationRequest> OperationRequests { get; set; }
        public DbSet<OperationType> OperationTypes { get; set; } // Add DbSet for OperationType

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigurePatient(modelBuilder);
            ConfigureStaff(modelBuilder);
            ConfigureOperationType(modelBuilder);
            ConfigureOperationRequest(modelBuilder);
            ConfigureAppointment(modelBuilder);
            ConfigureUser(modelBuilder);
        }
private void ConfigureUser(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<User>()
        .HasKey(u => u.Id); // Set primary key for User entity

    modelBuilder.Entity<User>()
        .Property(u => u.Id) // Assuming Id is of type UserId
        .HasConversion(
            v => v.AsGuid(), // Convert UserId to Guid for database storage
            v => new UserId(v) // Convert Guid back to UserId
        )
        .HasColumnName("UserId");

    // Configure Password as an owned type
    modelBuilder.Entity<User>()
        .OwnsOne(u => u.Password, p =>
        {
            p.Property(p => p.Pass) // Set the property of Password
                .IsRequired() // Assuming Password must always be set
                .HasMaxLength(100); // Set a max length or other constraints as needed
        });

    // Configure UserId as a keyless entity
    modelBuilder.Entity<UserId>()
        .HasNoKey(); // Mark UserId as keyless
}
       private void ConfigureStaff(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Staff>()
            .HasKey(s => s.Id);

        modelBuilder.Entity<Staff>()
            .Property(s => s.Id)
            .HasConversion(
                v => v.AsGuid(),
                v => new StaffId(v)
            )
            .HasColumnName("StaffId");

            modelBuilder.Entity<Staff>()
                .Property(s => s.Firstname)
                .IsRequired()
                .HasMaxLength(150);

            modelBuilder.Entity<Staff>()
                .Property(s => s.LastName)
                .IsRequired()
                .HasMaxLength(150);

            modelBuilder.Entity<Staff>()
                .Property(s => s.FullName)
                .IsRequired()
                .HasMaxLength(300);

            modelBuilder.Entity<Staff>()
                .Property(s => s.LicenseNumber)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Staff>()
                .Property(s => s.Specialization)
                .IsRequired()
                .HasMaxLength(150);

            modelBuilder.Entity<Staff>()
                .Property(s => s.AvailabilitySlot)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Staff>()
                .Property(s => s.PhoneNumber)
                .IsRequired()
                .HasMaxLength(15);

            modelBuilder.Entity<Staff>()
                .Property(s => s.Email)
                .IsRequired()
                .HasMaxLength(250);

            // Constraints for unique License Number, Email, and Phone
            modelBuilder.Entity<Staff>()
                .HasIndex(s => s.LicenseNumber)
                .IsUnique();

            modelBuilder.Entity<Staff>()
                .HasIndex(s => s.Email)
                .IsUnique();

            modelBuilder.Entity<Staff>()
                .HasIndex(s => s.PhoneNumber)
                .IsUnique();
             modelBuilder.Entity<Staff>()
                .HasKey(s => s.Id);
                    modelBuilder.Entity<Staff>()
            .Ignore(s => s.UserId); // Change to your actual property name if different
    }
        

        private void ConfigureOperationType(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OperationType>()
                .HasKey(ot => ot.Id);

            modelBuilder.Entity<OperationType>()
                .Property(ot => ot.Id)
                .HasConversion(
                    v => v.AsGuid(),
                    v => new OperationTypeID(v)
                )
                .HasColumnName("OperationTypeId");

            modelBuilder.Entity<OperationType>()
                .Property(ot => ot.Name)
                .IsRequired()
                .HasMaxLength(150);

            modelBuilder.Entity<OperationType>()
                .Property(ot => ot.EstimatedDuration)
                .IsRequired();

            modelBuilder.Entity<OperationType>()
                .Property(ot => ot.IsActive)
                .IsRequired();

            var valueConverter = new ValueConverter<List<string>, string>(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null)
            );

            modelBuilder.Entity<OperationType>()
                .Property(ot => ot.RequiredStaffBySpecialization)
                .HasConversion(valueConverter); // Serialize List<string> to JSON string
        }

      private void ConfigureOperationRequest(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<OperationRequest>()
        .HasKey(or => or.Id);

    modelBuilder.Entity<OperationRequest>()
        .Property(or => or.Id)
        .HasConversion(
            v => v.AsGuid(), // Assuming Id is a Guid
            v => new OperationRequestID(v)
        )
        .HasColumnName("OperationRequestId");

    modelBuilder.Entity<OperationRequest>()
        .Property(or => or.patientID) // Assuming this is now a PatientId
        .IsRequired();

    modelBuilder.Entity<OperationRequest>()
        .Property(or => or.doctorID)
        .IsRequired();

    modelBuilder.Entity<OperationRequest>()
        .Property(or => or.operationTypeID)
        .IsRequired();

    modelBuilder.Entity<OperationRequest>()
        .Property(or => or.deadline)
        .IsRequired();

    modelBuilder.Entity<OperationRequest>()
        .Property(or => or.priority)
        .IsRequired();

    // Here is the important part for setting the foreign key
    modelBuilder.Entity<OperationRequest>()
        .HasOne<Patient>() // Indicates the related entity type
        .WithMany() // Indicates that Patient can have many OperationRequests
        .HasForeignKey(or => or.patientID) // Here, patientID should match the type of PatientId
        .OnDelete(DeleteBehavior.Cascade); // Optional: set the delete behavior if needed

    modelBuilder.Entity<OperationRequest>()
        .HasOne<Staff>()
        .WithMany()
        .HasForeignKey(or => or.doctorID); // Assuming doctorID is the correct type as well
}



        private void ConfigureAppointment(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Appointment>()
        .HasKey(a => a.Id);

    modelBuilder.Entity<Appointment>()
        .Property(a => a.Id)
        .HasConversion(
            v => v.AsGuid(),
            v => new AppointmentId(v)
        )
        .HasColumnName("AppointmentId");

    modelBuilder.Entity<Appointment>()
        .Property(a => a.RequestId)
        .IsRequired()
        .HasMaxLength(150);

    // Configure StaffIds as a collection of value objects, not entities
    modelBuilder.Entity<Appointment>()
        .Property(a => a.StaffIds)
        .HasConversion(
            v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
            v => JsonSerializer.Deserialize<List<StaffId>>(v, (JsonSerializerOptions)null)
        );

    // Relationships for patient
    modelBuilder.Entity<Appointment>()
        .HasOne<Patient>()
        .WithMany(p => p.AppointmentHistory)
        .HasForeignKey(a => a.PatientId);
}

        private void ConfigurePatient(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Patient>()
                .Property(p => p.Id)
                .HasConversion(
                    v => v.AsGuid(),
                    v => new PatientId(v)
                )
                .HasColumnName("PatientId");

            modelBuilder.Entity<Patient>()
                .Property(p => p.Firstname)
                .IsRequired()
                .HasMaxLength(150);

            modelBuilder.Entity<Patient>()
                .Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(150);

            modelBuilder.Entity<Patient>()
                .Property(p => p.FullName)
                .IsRequired()
                .HasMaxLength(300);

            modelBuilder.Entity<Patient>()
                .Property(p => p.Gender)
                .IsRequired()
                .HasMaxLength(20);

            modelBuilder.Entity<Patient>()
                .Property(p => p.PhoneNumber)
                .IsRequired()
                .HasMaxLength(15);

            modelBuilder.Entity<Patient>()
                .Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(250);

            modelBuilder.Entity<Patient>()
                .Property(p => p.DateOfBirth)
                .IsRequired();

            modelBuilder.Entity<Patient>()
                .Property(p => p.MedicalRecordNumber)
                .IsRequired()
                .HasMaxLength(50);

            var valueConverter = new ValueConverter<List<string>, string>(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null)
            );

            modelBuilder.Entity<Patient>()
                .Property(p => p.Allergies)
                .HasConversion(valueConverter); // Serialize List<string> to JSON string

            // Constraints for unique Medical Record Number, Email, and Phone
            modelBuilder.Entity<Patient>()
                .HasIndex(p => p.MedicalRecordNumber)
                .IsUnique();

            modelBuilder.Entity<Patient>()
                .HasIndex(p => p.Email)
                .IsUnique();

            modelBuilder.Entity<Patient>()
                .HasIndex(p => p.PhoneNumber)
                .IsUnique();
        }
        
    }
}
