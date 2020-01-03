using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio_Website_Core.Models
{
    public class SQLEmployeeRepository : IEmployeeRepository
    {
        private readonly AppDdContext dbContext;

        public SQLEmployeeRepository(AppDdContext dbContex)
        {
            this.dbContext = dbContex;
        }

        public Employee AddEmployee(Employee employee)
        {
            dbContext.Employees.Add(employee);
            dbContext.SaveChanges();
            return employee;
        }

        public Employee Delete(int id)
        {
            var employee =  dbContext.Employees.Find(id);
            if(employee != null)
            {
                dbContext.Remove(employee);
                dbContext.SaveChanges();
            }
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return dbContext.Employees;
        }

        public List<Employee> GetAllEmployeesList()
        {
            return dbContext.Employees.ToList<Employee>();
        }

        public Employee GetEmployee(int id)
        {
            return dbContext.Employees.Find(id);
        }

        public Employee Update(Employee employeeChanges)
        {
            var employee  = dbContext.Employees.Attach(employeeChanges);
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            dbContext.SaveChanges();
            return employeeChanges;
        }
    }
}
