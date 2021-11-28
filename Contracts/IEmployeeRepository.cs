using Entities.Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Contracts
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetEmployees(Guid companyId, bool trackChanging);
        Employee GetEmployee(Guid companyId, Guid Id, bool trackChanging);
        void CreateEmployeeForCompany(Guid companyId, Employee employee);
        void DeleteEmployee(Employee employee);
    }
}
