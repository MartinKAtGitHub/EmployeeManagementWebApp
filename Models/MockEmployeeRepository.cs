using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio_Website_Core.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;

        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>();
            _employeeList.Add(new Employee { Id = 1, Name = "Mike", Department = Dept.HR, Email = "LOL@K.com" });
            _employeeList.Add(new Employee { Id = 2, Name = "Pike", Department = Dept.IT, Email = "ABC@K.com" });
            _employeeList.Add(new Employee { Id = 3, Name = "Dike", Department = Dept.Payroll, Email = "MAM@K.com" });
            
        }

        public Employee AddEmployee(Employee employee)
        {
            employee.Id =  _employeeList.Max(e => e.Id) + 1;
            _employeeList.Add(employee);
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employeeList;
        }

        public List<Employee> GetAllEmployeesList()
        {
            return _employeeList;
        }

        public Employee GetEmployee(int id)
        {
            return _employeeList.FirstOrDefault( e => e.Id == id);
        }
    }
}
