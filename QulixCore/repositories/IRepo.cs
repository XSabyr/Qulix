using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QulixCore
{
    public interface IRepo
    {
        Task<List<Worker>> GetWorkers();
        Task<Worker> GetWorkerById(int id);

        Task<List<Company>> GetCompanies();
        Task<Company> GetCompanyById(int id);

        Task<List<OrganizationalForm>> GetOrganizationalForms();
        Task<List<Position>> GetPositions();

        void UpdateCompany(int id, string name, int organizationalFormId);
        void AddCompany(string name, int organizationalFormId);
        void DeleteCompany(int id);

        void UpdateWorker(int id, string lastName, string firstName, string fatherName, DateTime employmentDate, int positionId, int companyId);
        void AddWorker(string lastName, string firstName, string fatherName, DateTime employmentDate, int positionId, int companyId);
        void DeleteWorker(int id);
    }
}
