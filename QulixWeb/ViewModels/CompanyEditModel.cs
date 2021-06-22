using Microsoft.AspNetCore.Mvc;
using QulixCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QulixWeb.ViewModels
{
    public class CompanyEditModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public OrganizationalForm OrganizationalForm { get; set; }
        public int OrganizationalFormId { get; set; }

        public List<OrganizationalForm> OrganizationalForms { get; set; }

        public CompanyEditModel(Company company, List<OrganizationalForm> organizationalForms)
        {
            Id = company.Id;
            Name = company.Name;
            OrganizationalForm = company.OrganizationalForm;
            OrganizationalFormId = company.OrganizationalForm.Id;
            OrganizationalForms = organizationalForms;
        }

        public CompanyEditModel(List<OrganizationalForm> organizationalForms)
        {
            OrganizationalForms = organizationalForms;
        }

        public CompanyEditModel()
        {
        }
    }
}
