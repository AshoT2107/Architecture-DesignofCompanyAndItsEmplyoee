using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICompanyRepository
    {

        IEnumerable<Company> GetAllCompanies(bool trackChanging);
        Company GetCompany(Guid companyId,bool trackChanging); 
        void CreateCompany(Company company);    
        IEnumerable<Company>GetByIds(IEnumerable<Guid> Ids,bool trackChanging);
        void DeleteCompany(Company company);

    }
}
