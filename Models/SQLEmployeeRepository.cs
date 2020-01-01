using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio_Website_Core.Models
{
    public class SQLEmployeeRepository : IEmployeeRepository
    {
        private readonly AppDdContext context;

        public SQLEmployeeRepository(AppDdContext contex)
        {
            this.context = contex;
        }

        public Employee AddEmployee(Employee employee)
        {
            context.Employees.Add(employee);
            context.SaveChanges();
            return employee;
        }

        public Employee Delete(int id)
        {
            var employee =  context.Employees.Find(id);
            if(employee != null)
            {
                context.Remove(employee);
                context.SaveChanges();
            }
            return employee;

        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return context.Employees;
        }

        public List<Employee> GetAllEmployeesList()
        {
            return context.Employees.ToList<Employee>();
        }

        public Employee GetEmployee(int id)
        {
            return context.Employees.Find(id);
        }

        public Employee Update(Employee employeeChanges)
        {
            var employee  = context.Employees.Attach(employeeChanges);
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return employeeChanges;
        }
    }
}
