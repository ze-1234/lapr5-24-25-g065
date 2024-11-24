import { Component, OnInit } from '@angular/core';
import { UserService } from '../../Services/user.service';
import { Router } from '@angular/router';
import { User } from '../../Interfaces/user';
import { StaffService } from '../../Services/staff.service';
import { Staff } from '../../Interfaces/staff';
import { PatientService } from '../../Services/patient.service';  // Import PatientService
import { CreatePatientDTO } from '../../Interfaces/patientId';  // Assuming you have an interface for creating a patient

@Component({
  selector: 'admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent implements OnInit {

  staffs: Staff[] = []; 
  loading: boolean = true;
  error: string | null = null;
  showStaffList = false; // Initialize with `false`
  editingStaff: Staff | null = null; // Tracks the staff being edited

  // New property to control the visibility of the patient registration form
  showPatientRegistrationForm: boolean = false;

  // Patient Registration Fields
  patientData: CreatePatientDTO = {
    firstname: '',
    lastname: '',
    fullName: '',  // Added fullName property
    gender: '',
    allergies: [],  // Updated to match `allergies` (which is an array of strings, optional)
    emergencyContact: '',  // Added emergencyContact property
    dateOfBirth: '',  // Updated to `dateOfBirth` (string or Date, matching C# DateTime?)
    medicalRecordNumber: '',  // Updated to `medicalRecordNumber`
    phoneNumber: '',  // Updated to `phoneNumber`
    email: '',  // Email remains the same
  };

  constructor(
    private staffService: StaffService,
    private patientService: PatientService,  // Inject PatientService
  ) {}

  ngOnInit(): void {
    // Optional: Load initial data if necessary
  }

  loadStaffs(): void {
    this.showStaffList = true; // Show the staff list
    this.loading = true; // Show loading state
    this.error = null; // Clear any previous errors

    this.staffService.getStaffs().subscribe({
      next: (staffs) => {
        // Map the API response to match the expected structure
        this.staffs = staffs.map((staff: any) => ({
          Id: staff.id,
          FirstName: staff.firstname,
          LastName: staff.lastname,
          FullName: staff.fullName,
          Gender: staff.gender,
          Specialization: staff.specialization,
          PhoneNumber: staff.phoneNumber,
          Email: staff.email,
          LicenseNumber: staff.licenseNumber,
          Type: staff.type,
          AvailabilitySlot: staff.availabilitySlot,
          UserId: staff.userId, // Assuming this is an object or null
          Appointments: staff.appointments || [] // Default to empty array if null
        }));
        console.log(this.staffs);
        this.loading = false; // Hide loading state after data is loaded
      },
      error: (err) => {
        this.error = 'Failed to load staff list.';
        console.error(err);
        this.loading = false; // Hide loading state in case of error
      },
    });
  }

  onCreateStaff(): void {
    console.log('Create staff');
  }

  onUpdateStaff(staff: any): void {
    console.log('Update staff:', staff);
  }

  onEditStaff(staff: Staff): void {
    this.editingStaff = { ...staff }; 
  }

  cancelEdit(): void {
    this.editingStaff = null;
  }

  onSaveUpdate() {
    // Clean the form data to replace null or undefined fields with empty strings
    this.editingStaff = this.cleanFormData(this.editingStaff);
  
    // Now send the cleaned data to the backend
    this.staffService.updateStaff(this.editingStaff).subscribe({
      next: (updatedStaff) => {
        console.log('Staff updated successfully:', updatedStaff);
        const index = this.staffs.findIndex((staff) => staff.Id === updatedStaff.Id);
        if (index !== -1) {
          this.staffs[index] = { ...updatedStaff };
        }
        this.cancelEdit();
      },
      error: (err) => {
        console.error('Error updating staff:', err);
        this.error = 'Failed to update staff member.';
      },
    });
  }
  
  
  cleanFormData(formData: any) {
    for (const key in formData) {
      if (formData.hasOwnProperty(key)) {
        if ( formData[key] === undefined) {
          formData[key] = '';
        }
      }
    }
    return formData;
  }
  
  

  onDeleteStaff(index: number): void {
    console.log("Deleting staff: ", this.staffs[index].FullName);
    console.log(this.staffs[index].Id.value);
    this.staffService.deleteStaff(this.staffs[index].Id).subscribe({
      next: (response) => {
        console.log("Delete successful:", response);
        this.loadStaffs(); // Reload staff list after successful deletion
      },
      error: (err) => {
        console.error("Error deleting staff:", err);
        this.error = 'Failed to delete staff member.'; // Handle the error case
      }
    });
  }

  // Show the patient registration form
onShowPatientRegistrationForm(): void {
  this.showPatientRegistrationForm = !this.showPatientRegistrationForm; // Toggle visibility of the form
}

  // Register a new patient
  // Register a new patient
onRegisterPatient(): void {
  this.patientService.registerPatient(this.patientData).subscribe({
    next: (response) => {
      console.log('Patient registered successfully:', response);
      // Optionally reset form or show success message
      this.showPatientRegistrationForm = false;  // Hide the form after successful registration
      this.patientData = {
        firstname: '',
        lastname: '',
        fullName: '',
        gender: '',
        allergies: [],
        emergencyContact: '',
        dateOfBirth: '',
        medicalRecordNumber: '',
        phoneNumber: '',
        email: '',
      }; // Reset form data
    },
    error: (err) => {
      console.error('Error registering patient:', err);
      this.error = 'Failed to register patient.'; // Handle the error case
    }
  });
}
}
