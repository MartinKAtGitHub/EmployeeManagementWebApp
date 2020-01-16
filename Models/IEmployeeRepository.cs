using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio_Website_Core.Models
{
   public interface IEmployeeRepository
    {
        //CRUD
        Employee AddEmployee(Employee employee); // Create
        Employee GetEmployee(int id); // Read
        IEnumerable<Employee> GetAllEmployees();
        List<Employee> GetAllEmployeesList();
        Employee Update(Employee employeeChanges); // Update
        Employee Delete(int id); // Delete
    }
}
