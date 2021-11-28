using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CompanyRepository : RepositporyBase<Company>, ICompanyRepository
    {
        public CompanyRepository(RepositoryContext repositoryContext) 
            : base(repositoryContext)
        {
            
        }

        public void CreateCompany(Company company) => Create(company);

        public void DeleteCompany(Company company)=>Delete(company);    

        public IEnumerable<Company> GetAllCompanies(bool trackChanging)=>
            FindAll(trackChanging).OrderBy(n => n.Name).ToList();

        public IEnumerable<Company> GetByIds(IEnumerable<Guid> Ids, bool trackChanging) =>
            FindByCondition(x => Ids.Contains(x.Id), trackChanging).ToList();

        public Company GetCompany(Guid companyId, bool trackChanging) =>
            FindByCondition(c => c.Id.Equals(companyId), trackChanging).SingleOrDefault();

    }
}
