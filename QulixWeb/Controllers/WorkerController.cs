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
    public class WorkerController : Controller
    {
        private readonly IRepo repo;

        public WorkerController(IRepo repo)
        {
            this.repo = repo;
        }

        [Route("[action]")]
        public IActionResult List(WorkerListViewModel model)
        {
            model.Workers = repo.GetWorkers().Result;
            return View(model);
        }

        [Route("[action]/{id}")]
        public IActionResult Details(int id)
        {
            Worker worker = repo.GetWorkerById(id).Result;
            return worker == null ? (IActionResult)NotFound() : View(worker);
        }

        ///////////
        ///

        [HttpGet("[action]/{id:int}")]
        public IActionResult Edit(int id)
        {
            var worker = repo.GetWorkerById(id).Result;
            if (worker is null)
            {
                return NotFound();
            }

            var positions = repo.GetPositions().Result;
            var companies = repo.GetCompanies().Result;

            return View(new WorkerEditModel(worker, positions, companies));
        }

        [HttpPost("[action]/{id:int}")]
        public IActionResult Edit(WorkerEditModel editModel)
        {
            if (!ModelState.IsValid)
            {
                return View(editModel);
            }

            var worker = repo.GetWorkerById(editModel.Id).Result;
            if (worker is null)
            {
                return NotFound();
            }

            if (string.IsNullOrEmpty(editModel.FatherName))
            {
                editModel.FatherName = "";
            }

            repo.UpdateWorker(editModel.Id, editModel.LastName, editModel.FirstName, editModel.FatherName, editModel.EmploymentDate, editModel.PositionId, editModel.CompanyId);
            return RedirectToAction("List");
        }

        //


        [HttpGet("[action]")]
        public IActionResult Add()
        {
            var positions = repo.GetPositions().Result;
            var companies = repo.GetCompanies().Result;
            return View(new WorkerEditModel(positions, companies));
        }

        [HttpPost("[action]")]
        public IActionResult Add(WorkerEditModel editModel)
        {
            if (string.IsNullOrEmpty(editModel.FatherName))
            {
                editModel.FatherName = "";
            }

            repo.AddWorker(editModel.LastName, editModel.FirstName, editModel.FatherName, editModel.EmploymentDate, editModel.PositionId, editModel.CompanyId);
            return RedirectToAction("List");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var worker = repo.GetWorkerById(id).Result;
            if (worker is null)
            {
                return NotFound();
            }

            repo.DeleteWorker(id);
            return RedirectToAction("List");
        }
    }
}
