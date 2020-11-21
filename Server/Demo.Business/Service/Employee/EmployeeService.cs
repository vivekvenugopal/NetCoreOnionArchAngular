using System;
using System.Collections.Generic;
using Demo.APIModel;
using Demo.Business.InfraStructure;
using Demo.Common;
using Demo.Common.ErrorHandling;
using Demo.Model;
using System.Linq;
using Demo.Common.Logger;

namespace Demo.Business.Service {
    public class EmployeeService : PersistenceService, IEmployeeService {
        private IRepository<Employee> _employeeRepository;
        private IRepository<Skill> _skillRepository;
        private IUnitOfWork _unitOfWork;
        private ILoggerManager _loggerManager;
        public EmployeeService (ILoggerManager loggerManager,IRepository<Employee> employeeRepository, 
                                IRepository<Skill> skillRepository, IUnitOfWork unitOfWork  ) {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
            _skillRepository = skillRepository;
            _loggerManager = loggerManager;
        }
        public void AddEmployee (EmployeeAPIModel employeeAPI) {

            var employee = MapEmployees (employeeAPI);
            ValidateEmployee(employeeAPI);
            _employeeRepository.Add (employee);
            SaveChanges (_unitOfWork);
        }
        private void ValidateEmployee(EmployeeAPIModel employeeAPI)
        {
             if(_employeeRepository.SelectOne(x=>x.EmployeeId == employeeAPI.EmployeeId) != null)
                throw new DemoAPIException("employeeId","The employee Id already exists");
        }
        public void DeleteEmployee (long id) {
            _skillRepository.Delete (x => x.Employee.Id == id);
            _employeeRepository.Delete (x => x.Id == id);
            SaveChanges (_unitOfWork);
            _loggerManager.LogInfo("Employee with Id "+ id +" has been deleted");
        }
        public EmployeeAPIModel GetEmployee (long id) {
            var employee = _employeeRepository.SelectOne(x=>x.Id == id);
            var employeeAPI = MapEmployeesAPI (employee);
            return employeeAPI;
        }
        public IEnumerable<EmployeeAPIModel> GetAllEmployees () {
            var employees = _employeeRepository.SelectAllWithRelatedEntities();
            List<EmployeeAPIModel> employeeAPIModel = new List<EmployeeAPIModel> ();
            foreach (var employee in employees) {
                var employeeAPI = MapEmployeesAPI (employee);
                employeeAPIModel.Add (employeeAPI);
            }
            return employeeAPIModel;
        }

        public IEnumerable<EmployeeAPIModel> GetEmployeesForAccount() {
           var employee = _employeeRepository.SelectAll(); 
           List<EmployeeAPIModel> employeeAPIModel = new List<EmployeeAPIModel> ();
           foreach(var emp  in employee )
           {
            employeeAPIModel.Add(new EmployeeAPIModel { FirstName=emp.FirstName+emp.LastName,Id=emp.Id });
           };

          return employeeAPIModel;
        }

        public void UpdateEmployee (EmployeeAPIModel employeeAPI) {
             var employee = _employeeRepository.SelectOne(x=>x.Id == employeeAPI.Id);
            employee.FirstName = employeeAPI.FirstName;
            employee.LastName = employeeAPI.LastName;
            employee.EmployeeId = employeeAPI.EmployeeId;
            employee.DateOfJoining = employeeAPI.DateOfJoining;
            employee.Address = employeeAPI.Address;
            employee.DOB = employeeAPI.DOB;
            employee.Email = employeeAPI.Email;
            var skills = employee.Skills.ToList();
            foreach (var skill in employee.Skills) {
                if(!employeeAPI.Skills.Exists(x=>x.Id == skill.Id))
                   _skillRepository.Delete(skill);
            } 
             foreach (var skill in employeeAPI.Skills) {
                if(!skills.Exists(x=>x.Id == skill.Id))
                  {
                      employee.Skills.Add(new Skill{
                            Technology = skill.Technology,
                            StartDate = skill.StartDate,
                            EndDate = skill.EndDate,
                            CreatedBy = UserStateManagement.UserId,
                            CreatedDate = DateTime.Now
                      });
                  }
            } 
            _employeeRepository.Update(employee);
            SaveChanges (_unitOfWork);
        }
        private Employee MapEmployees ( EmployeeAPIModel employeeAPI) {
            var   employee = new Employee ();
            employee.FirstName = employeeAPI.FirstName;
            employee.LastName = employeeAPI.LastName;
            employee.EmployeeId = employeeAPI.EmployeeId;
            employee.DateOfJoining = employeeAPI.DateOfJoining;
            employee.Address = employeeAPI.Address;
            employee.DOB = employeeAPI.DOB;
            employee.Email = employeeAPI.Email;
            employee.Skills = new List<Skill> ();
            foreach (var skill in employeeAPI.Skills) {
                employee.Skills.Add (new Skill {
                        Technology = skill.Technology,
                        StartDate = skill.StartDate,
                        EndDate = skill.EndDate,
                        CreatedBy = UserStateManagement.UserId,
                        CreatedDate = DateTime.Now

                });
            }
            return employee;
        }
        private EmployeeAPIModel MapEmployeesAPI (Employee employee) {
            var employeeAPI = new EmployeeAPIModel ();
            if (employee != null) {
                employeeAPI.Id = employee.Id;
                employeeAPI.FirstName = employee.FirstName;
                employeeAPI.LastName = employee.LastName;
                employeeAPI.EmployeeId = employee.EmployeeId;
                employeeAPI.DateOfJoining = employee.DateOfJoining;
                employeeAPI.Address = employee.Address;
                employeeAPI.DOB = employee.DOB;
                employeeAPI.Email = employee.Email;
                employeeAPI.Skills = new List<SkillAPIModel> ();
                if (employee.Skills != null) {
                    foreach (var skill in employee.Skills) {
                        employeeAPI.Skills.Add (new SkillAPIModel {
                                Id = skill.Id,
                                Technology = skill.Technology,
                                StartDate = skill.StartDate,
                                EndDate = skill.EndDate
                        });
                    }
                }
            }
            return employeeAPI;
        }
    }
}