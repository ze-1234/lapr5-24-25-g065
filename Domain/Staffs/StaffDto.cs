using System;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Specializations;

namespace DDDSample1.Domain.Staffs
{
    public class StaffDto
    {
        public Guid Id { get; set; }
        public string Gender { get;  private set; }
        public string Type{ get;  private set; }

        public Specialization Specialization { get;  private set; }
        public string Firstname { get;  private set; }

        public string LastName { get;  private set; }
        public string FullName { get;  private set; }

        public string LicenseNumber { get;  private set; }

        public StaffDto(Guid id,string firstname, string lastName,string fullName, string gender, Specialization specialization, string type, string licenseNumber ){
            this.Id = id;
            Firstname = firstname;
            LastName = lastName;
            FullName = fullName;
            Gender = gender;
            Specialization = specialization;
            Type = type;
            LicenseNumber = licenseNumber;
        }
    }
}