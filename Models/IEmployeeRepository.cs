using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio_Website_Core.Models
{
   public interface IEmployeeRepository
    {
        Employee GetEmployee(int id);
    }
}
