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
            _employeeList.Add(new Employee { Id = 1, Name = "Mike", Department = "HR", Email = "LOL@K.com" });
            _employeeList.Add(new Employee { Id = 2, Name = "Pike", Department = "IT", Email = "ABC@K.com" });
            _employeeList.Add(new Employee { Id = 3, Name = "Dike", Department = "PR", Email = "MAM@K.com" });
            _employeeList.Add(new Employee { Id = 4, Name = "Kike", Department = "CE", Email = "KEK@K.com" });
        }
        public Employee GetEmployee(int id)
        {
            return _employeeList.FirstOrDefault( e => e.Id == id);
        }
    }
}
