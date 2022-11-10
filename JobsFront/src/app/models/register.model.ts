export interface RegisterModel {
  userID: number;
  fullName: string;
  userName: string;
  bio: string;
  emailId: string;
  password: string;
  salt: string;
  mobileNumber: number;
  photographLink: string;
  linkedInProfile: string;
  workStatus: boolean;
  currentSalary: number;
  expectedSalary: number;
  currentLocation: string;
  preferredLocation: string;
  companyName: string;
  recruiterDescription: string;
  role: string;
}
