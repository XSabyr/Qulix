using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QulixCore
{
    public interface IRepo
    {
        // Получение списка работников
        Task<List<Worker>> GetWorkers();
        // Получение работника по ID
        Task<Worker> GetWorkerById(int id);

        // Получение списка компаний
        Task<List<Company>> GetCompanies();
        // Получение компании по ID
        Task<Company> GetCompanyById(int id);

        // получение списка организационно-правовых форм
        Task<List<OrganizationalForm>> GetOrganizationalForms();
        // получение списка позиций
        Task<List<Position>> GetPositions();

        // изменение компании
        void UpdateCompany(int id, string name, int organizationalFormId);
        // добавление компании
        void AddCompany(string name, int organizationalFormId);
        // удаление компании
        void DeleteCompany(int id);

        // изменение информации о работнике
        void UpdateWorker(int id, string lastName, string firstName, string fatherName, DateTime employmentDate, int positionId, int companyId);
        // добавление работника
        void AddWorker(string lastName, string firstName, string fatherName, DateTime employmentDate, int positionId, int companyId);
        // удаление работника
        void DeleteWorker(int id);
    }
}
