using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        private ICompanyRepository _companyRepository;
        private IEmployeeRepository _employeeRepository;
        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        public ICompanyRepository Company
        {
            get
            {
                if (_companyRepository is null)
                {
                    _companyRepository = new CompanyRepository(_repositoryContext);
                }
                return _companyRepository;
            }
        }


        public IEmployeeRepository Employee 
        {
            get
            {
                if (_employeeRepository is null)
                {
                    _employeeRepository = new EmployeeRepository(_repositoryContext);
                }
                return _employeeRepository;
            }
        }


        public void Save() => _repositoryContext.SaveChanges();
    }
}
