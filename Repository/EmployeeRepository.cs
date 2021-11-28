using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class EmployeeRepository : RepositporyBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public void CreateEmployeeForCompany(Guid companyId, Employee employee)
        {
            employee.CompanyId = companyId;
            Create(employee);
        }

        public void DeleteEmployee(Employee employee)
        {
            Delete(employee);
        }

        public Employee GetEmployee(Guid companyId, Guid Id, bool trackChanging)=>
            FindByCondition(e=>e.CompanyId.Equals(companyId) && e.Id.Equals(Id),trackChanging).SingleOrDefault();

        public IEnumerable<Employee> GetEmployees(Guid companyId, bool trackChanging)=>
            FindByCondition(c=>c.CompanyId.Equals(companyId),trackChanging)
            .OrderBy(e=>e.Name);
    }
}
