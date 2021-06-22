using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QulixCore
{
    public class Worker
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public DateTime EmploymentDate { get; set; }

        public int PositionId { get; set; }
        public virtual Position Position { get; set; }

        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"worker id = {Id}\n");
            sb.Append($"worker fullName = {LastName} {FirstName} {FatherName ?? ""}\n");
            sb.Append($"worker employment date = {EmploymentDate}\n");
            sb.Append($"worker position = {Position.Name}\n");
            sb.Append($"worker company = {Company.Name}\n");

            return sb.ToString();
        }

        public string GetFullName() => $"{LastName} {FirstName} {FatherName ?? ""}";

        public string GetEmploymentDate() => EmploymentDate.ToString("dd/MM/yyyy");
    }
}
