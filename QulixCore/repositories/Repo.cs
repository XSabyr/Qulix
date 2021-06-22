using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QulixCore
{
    public class Repo : IRepo
    {
        private readonly string connectionString;
        private readonly SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder();

        public Repo()
        {
            connectionStringBuilder.DataSource = @"(local)";
            connectionStringBuilder.InitialCatalog = "QulixDB";
            connectionStringBuilder.IntegratedSecurity = true;
            connectionStringBuilder.Pooling = true;

            connectionString = connectionStringBuilder.ToString();
        }

        public async Task<Worker> GetWorkerById(int id)
        {
            string sql = @"SELECT w.[Id]
                              ,w.[LastName]
                              ,w.[FirstName]
                              ,w.[FatherName]
                              ,w.[EmploymentDate]

                              ,w.[PositionId]
                              ,w.[CompanyId]
	                          ,p.Id as posid
	                          ,p.Name
	                          ,c.Id as compid

	                          ,c.Name
	                          ,c.OrganizationalFormId
	                          ,o.Id as ofid
	                          ,o.Name
                          FROM [QulixDB].[dbo].[Worker] as w,
                               [QulixDB].[dbo].[Position] as p, 
                               [QulixDB].[dbo].[Company] as c, 
                               [QulixDB].[dbo].OrganizationalForm as o
                          WHERE 
		                        w.PositionId = p.Id
	                        AND w.CompanyId = c.Id
	                        AND c.OrganizationalFormId = o.Id
                            AND w.Id = @id";
            Worker worker = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add(new SqlParameter("@id", id));
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrganizationalForm organizationalForm = new OrganizationalForm();
                        organizationalForm.Id = reader.GetInt32(12);
                        organizationalForm.Name = reader.GetString(13);

                        Company company = new Company();
                        company.Id = reader.GetInt32(9);
                        company.Name = reader.GetString(10);
                        company.OrganizationalForm = organizationalForm;

                        Position position = new Position();
                        position.Id = reader.GetInt32(7);
                        position.Name = reader.GetString(8);

                        worker = new Worker();
                        worker.Id = reader.GetInt32(0);
                        worker.LastName = reader.GetString(1);
                        worker.FirstName = reader.GetString(2);
                        worker.FatherName = reader.GetString(3);
                        worker.EmploymentDate = reader.GetDateTime(4);
                        worker.Position = position;
                        worker.Company = company;
                    }
                }
            }

            return worker;
        }

        public async Task<List<Worker>> GetWorkers()
        {
            string sql = @"SELECT w.[Id]
                              ,w.[LastName]
                              ,w.[FirstName]
                              ,w.[FatherName]
                              ,w.[EmploymentDate]

                              ,w.[PositionId]
                              ,w.[CompanyId]
	                          ,p.Id as posid
	                          ,p.Name
	                          ,c.Id as compid

	                          ,c.Name
	                          ,c.OrganizationalFormId
	                          ,o.Id as ofid
	                          ,o.Name
                          FROM [QulixDB].[dbo].[Worker] as w,
                               [QulixDB].[dbo].[Position] as p, 
                               [QulixDB].[dbo].[Company] as c, 
                               [QulixDB].[dbo].OrganizationalForm as o
                          WHERE 
		                        w.PositionId = p.Id
	                        AND w.CompanyId = c.Id
	                        AND c.OrganizationalFormId = o.Id";
            List<Worker> workers = new List<Worker>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand(sql, connection);
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrganizationalForm organizationalForm = new OrganizationalForm();
                        organizationalForm.Id = reader.GetInt32(12);
                        organizationalForm.Name = reader.GetString(13);

                        Company company = new Company();
                        company.Id = reader.GetInt32(9);
                        company.Name = reader.GetString(10);
                        company.OrganizationalForm = organizationalForm;

                        Position position = new Position();
                        position.Id = reader.GetInt32(7);
                        position.Name = reader.GetString(8);

                        Worker worker = new Worker();
                        worker.Id = reader.GetInt32(0);
                        worker.LastName = reader.GetString(1);
                        worker.FirstName = reader.GetString(2);
                        worker.FatherName = reader.GetString(3);
                        worker.EmploymentDate = reader.GetDateTime(4);
                        worker.Position = position;
                        worker.Company = company;

                        workers.Add(worker);
                    }
                }
            }

            return workers;
        }

        public async Task<List<Company>> GetCompanies()
        {
            List<Company> companies = new List<Company>();

            string sql = @"SELECT  c.[Id]
                                  ,c.[Name]
                                  ,c.[OrganizationalFormId]
	                              , o.[Id]
	                              , o.[Name]
                              FROM [QulixDB].[dbo].[Company] as c, [QulixDB].[dbo].[OrganizationalForm] as o
                              WHERE c.OrganizationalFormId = o.Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand(sql, connection);
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrganizationalForm organizationalForm = new OrganizationalForm();
                        organizationalForm.Id = reader.GetInt32(3);
                        organizationalForm.Name = reader.GetString(4);

                        Company company = new Company();
                        company.Id = reader.GetInt32(0);
                        company.Name = reader.GetString(1);
                        company.OrganizationalForm = organizationalForm;

                        companies.Add(company);
                    }
                }
            }

            return companies;
        }

        public async Task<Company> GetCompanyById(int id)
        {
            string sql = @"SELECT  c.[Id]
                                  ,c.[Name]
                                  ,c.[OrganizationalFormId]
	                              , o.[Id]
	                              , o.[Name]
                              FROM [QulixDB].[dbo].[Company] as c, [QulixDB].[dbo].[OrganizationalForm] as o
                              WHERE c.OrganizationalFormId = o.Id 
                              AND c.Id = @id";
            Company company = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add(new SqlParameter("@id", id));
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrganizationalForm organizationalForm = new OrganizationalForm();
                        organizationalForm.Id = reader.GetInt32(3);
                        organizationalForm.Name = reader.GetString(4);

                        company = new Company();
                        company.Id = reader.GetInt32(0);
                        company.Name = reader.GetString(1);
                        company.OrganizationalForm = organizationalForm;
                    }
                }
            }

            return company;
        }

        public async Task<List<OrganizationalForm>> GetOrganizationalForms()
        {
            List<OrganizationalForm> organizationalForms = new List<OrganizationalForm>();

            string sql = @"SELECT [Id]
                              ,[Name]
                          FROM [QulixDB].[dbo].[OrganizationalForm]";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand(sql, connection);
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrganizationalForm organizationalForm = new OrganizationalForm();
                        organizationalForm.Id = reader.GetInt32(0);
                        organizationalForm.Name = reader.GetString(1);

                        organizationalForms.Add(organizationalForm);
                    }
                }
            }

            return organizationalForms;
        }

        public async Task<List<Position>> GetPositions()
        {
            List<Position> positions = new List<Position>();

            string sql = @"SELECT [Id]
                              ,[Name]
                          FROM [QulixDB].[dbo].[Position]";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand(sql, connection);
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Position position = new Position();
                        position.Id = reader.GetInt32(0);
                        position.Name = reader.GetString(1);

                        positions.Add(position);
                    }
                }
            }

            return positions;
        }

        public void UpdateCompany(int id, string name, int organizationalFormId)
        {
            string sql = @"UPDATE [dbo].[Company]
                               SET [Name] = @name
                                  ,[OrganizationalFormId] = @organizationalFormId
                             WHERE Id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add(new SqlParameter("@id", id));
                cmd.Parameters.Add(new SqlParameter("@name", name));
                cmd.Parameters.Add(new SqlParameter("@organizationalFormId", organizationalFormId));

                cmd.ExecuteNonQuery();
            }

        }

        public void AddCompany(string name, int organizationalFormId)
        {
            string sql = @"INSERT INTO [dbo].[Company]
                                   ([Name]
                                   ,[OrganizationalFormId])
                             VALUES
                                   (@name
                                   ,@organizationalFormId)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add(new SqlParameter("@name", name));
                cmd.Parameters.Add(new SqlParameter("@organizationalFormId", organizationalFormId));

                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteCompany(int id)
        {
            string sql = @"DELETE FROM [dbo].[Company]
                              WHERE Id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add(new SqlParameter("@id", id));

                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateWorker(int id, string lastName, string firstName, string fatherName, DateTime employmentDate, int positionId, int companyId)
        {
            var sql = @"UPDATE [dbo].[Worker]
                           SET [LastName] = @lastName
                              ,[FirstName] = @firstName
                              ,[FatherName] = @fatherName
                              ,[EmploymentDate] = @employmentDate
                              ,[PositionId] = @positionId
                              ,[CompanyId] = @companyId
                        WHERE [Id] = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add(new SqlParameter("@id", id));
                cmd.Parameters.Add(new SqlParameter("@firstName", firstName));
                cmd.Parameters.Add(new SqlParameter("@lastName", lastName));
                cmd.Parameters.Add(new SqlParameter("@fatherName", fatherName));
                cmd.Parameters.Add(new SqlParameter("@employmentDate", employmentDate));
                cmd.Parameters.Add(new SqlParameter("@positionId", positionId));
                cmd.Parameters.Add(new SqlParameter("@companyId", companyId));

                cmd.ExecuteNonQuery();
            }
        }

        public void AddWorker(string lastName, string firstName, string fatherName, DateTime employmentDate, int positionId, int companyId)
        {
            var sql = @"INSERT INTO [dbo].[Worker]
                               ([LastName]
                               ,[FirstName]
                               ,[FatherName]
                               ,[EmploymentDate]
                               ,[PositionId]
                               ,[CompanyId])
                         VALUES
                               (@lastName
                               ,@firstName
                               ,@fatherName
                               ,@employmentDate
                               ,@positionId
                               ,@companyId)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add(new SqlParameter("@firstName", firstName));
                cmd.Parameters.Add(new SqlParameter("@lastName", lastName));
                cmd.Parameters.Add(new SqlParameter("@fatherName", fatherName));
                cmd.Parameters.Add(new SqlParameter("@employmentDate", employmentDate));
                cmd.Parameters.Add(new SqlParameter("@positionId", positionId));
                cmd.Parameters.Add(new SqlParameter("@companyId", companyId));

                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteWorker(int id)
        {
            var sql = @"DELETE FROM [dbo].[Worker]
                          WHERE [Id] = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add(new SqlParameter("@id", id));

                cmd.ExecuteNonQuery();
            }
        }
    }
}
