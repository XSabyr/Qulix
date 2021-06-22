using Microsoft.AspNetCore.Mvc;
using QulixCore;
using QulixWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QulixWeb.Controllers
{
    [Route("[controller]")]
    public class CompanyController : Controller
    {
        private readonly IRepo repo;

        public CompanyController(IRepo repo)
        {
            this.repo = repo;
        }

        [Route("[action]")]
        public IActionResult List(CompanyListViewModel model)
        {
            model.Companies = repo.GetCompanies().Result;
            return View(model);
        }

        [Route("[action]/{id}")]
        public IActionResult Details(int id)
        {
            Company company = repo.GetCompanyById(id).Result;
            return company == null ? (IActionResult)NotFound() : View(company);
        }

        [HttpGet("[action]/{id:int}")]
        public IActionResult Edit(int id)
        {
            var company = repo.GetCompanyById(id).Result;
            if (company is null)
            {
                return NotFound();
            }

            var organizationalForms = repo.GetOrganizationalForms().Result;

            return View(new CompanyEditModel(company, organizationalForms));
        }

        [HttpPost("[action]/{id:int}")]
        public IActionResult Edit(CompanyEditModel editModel)
        {
            if (!ModelState.IsValid)
            {
                return View(editModel);
            }

            var company = repo.GetCompanyById(editModel.Id).Result;
            if (company is null)
            {
                return NotFound();
            }

            repo.UpdateCompany(editModel.Id, editModel.Name, editModel.OrganizationalFormId);
            return RedirectToAction("List");
        }

        [HttpGet("[action]")]
        public IActionResult Add()
        {
            var organizationalForms = repo.GetOrganizationalForms().Result;
            return View(new CompanyEditModel(organizationalForms));
        }

        [HttpPost("[action]")]
        public IActionResult Add(CompanyEditModel editModel)
        {
            repo.AddCompany(editModel.Name, editModel.OrganizationalFormId);
            return RedirectToAction("List");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var company = repo.GetCompanyById(id).Result;
            if (company is null)
            {
                return NotFound();
            }

            repo.DeleteCompany(id);
            return RedirectToAction("List");
        }
    }
}
