using System.Collections.Generic;
using Demo.APIModel;

namespace Demo.Business.Service {
    public interface IEmployeeService {
        void AddEmployee(EmployeeAPIModel employee);
         void DeleteEmployee(long id);
        void UpdateEmployee(EmployeeAPIModel employee);
        IEnumerable<EmployeeAPIModel> GetAllEmployees();
        IEnumerable<EmployeeAPIModel> GetEmployeesForAccount();
        EmployeeAPIModel GetEmployee(long id);


    }
}