export class Employee{
    Id:number;
    EmployeeId: string;
    FirstName: string;
    LastName: string;
    Email: string;
    DOJ: Date;
    Address: string;
    DOB: Date;
    Skills: SkillSet[];
}

export class SkillSet {
    Id: number;
    Technology: string;
    StartDate: Date;
    EndDate?: Date;
  }