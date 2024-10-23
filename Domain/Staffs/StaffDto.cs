using System;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Specializations;
using DDDSample1.Domain.Users;

namespace DDDSample1.Domain.Staffs
{
    public class StaffDto
    {
        public Guid Id { get; set; }
        public string Gender { get;  private set; }
        public string Type{ get;  private set; }

        public SpecializationId SpecializationId { get;  private set; }
        public string Firstname { get;  private set; }

        public string LastName { get;  private set; }
        public string FullName { get;  private set; }

        public string LicenseNumber { get;  private set; }

        public UserId UserId{ get; private set;}
        public StaffDto(Guid id,string firstname, string lastName,string fullName, string gender, SpecializationId specializationId, string type, string licenseNumber, UserId userId ){
            this.Id = id;
            Firstname = firstname;
            LastName = lastName;
            FullName = fullName;
            Gender = gender;
            SpecializationId = specializationId;
            Type = type;
            LicenseNumber = licenseNumber;
            UserId = userId;
        }
    }
}