using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Demo.API.Extensions;
using Demo.APIModel;
using Demo.Business.Service;
using Demo.Common.ErrorHandling;

namespace Demo.API.Controllers {
    public class EmployeeController : BaseController {
        private IEmployeeService _employeeService;
        public EmployeeController (IEmployeeService employeeService) {
            _employeeService = employeeService;
        }

        [HttpGet]
        public IEnumerable<EmployeeAPIModel> Get () {
            return _employeeService.GetAllEmployees ();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id) {
            if (!ModelState.IsValid)
                return BadRequest (ModelState.GetErrorMessages());
            
            var employee = _employeeService.GetEmployee(id);
            return Ok (employee);
        }

        [HttpPost]
        public IActionResult Post ([FromBody] EmployeeAPIModel employee) {

            if (!ModelState.IsValid)
                return BadRequest (ModelState.GetErrorMessages ());

            try
            {
                _employeeService.AddEmployee (employee);
            }
            catch(DemoAPIException ex)
            {
               APIHelper.AddModelErrorState(ModelState, ex.ValidationResult);
               return BadRequest(ModelState);
            }
           
            return Ok (employee);
        }

        [HttpPut]
        public IActionResult Put ([FromBody] EmployeeAPIModel employee) {
            if (!ModelState.IsValid)
                return BadRequest (ModelState.GetErrorMessages ());

            _employeeService.UpdateEmployee (employee);

            // if (!result.Success)
            //     return BadRequest(result.Message);

            return Ok (employee);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete (int id) {
            if (!ModelState.IsValid)
                return BadRequest (ModelState.GetErrorMessages ());

            _employeeService.DeleteEmployee (id);

            return Ok ();
        }

        [HttpGet]
        IEnumerable<EmployeeAPIModel> GetEmployeesForAccount()
        {
           return _employeeService.GetEmployeesForAccount();
        }
    }
}