using Microsoft.AspNetCore.Mvc;
using QulixCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QulixWeb.ViewModels
{
    public class WorkerEditModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        public string FatherName { get; set; }

        [Required(ErrorMessage = "Employment Date is required")]
        public DateTime EmploymentDate { get; set; } = DateTime.Today;

        public int PositionId { get; set; }
        public virtual Position Position { get; set; }
        public List<Position> Positions { get; set; }

        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public List<Company> Companies { get; set; }

        public WorkerEditModel(Worker worker, List<Position> positions, List<Company> companies)
        {
            Id = worker.Id;
            LastName = worker.LastName;
            FirstName = worker.FirstName;
            FatherName = worker.FatherName;
            EmploymentDate = worker.EmploymentDate;
            PositionId = worker.PositionId;
            Positions = positions;
            CompanyId = worker.CompanyId;
            Companies = companies;
        }

        public WorkerEditModel(List<Position> positions, List<Company> companies)
        {
            Positions = positions;
            Companies = companies;
        }

        public WorkerEditModel()
        {
        }
    }
}
