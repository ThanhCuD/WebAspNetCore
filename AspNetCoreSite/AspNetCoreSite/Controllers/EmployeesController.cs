using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreSite.Models;
using AspNetCoreSite.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AspNetCoreSite.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly ILogger logger;
        private readonly IGenericRepository<Employee> genericRepository;

        public EmployeesController(ILogger<EmployeesController> logger, IGenericRepository<Employee> genericRepository)
        {
            this.logger = logger;
            this.genericRepository = genericRepository;
        }
        public IActionResult Index()
        {
            try
            {
                var list =genericRepository.GetAll();
                return View(list.ToList());
            }
            catch(Exception ex)
            {
                logger.LogError(ex.ToString());
                return View("Error");
            }
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var emloyee = new Employee()
                {
                    Id = Guid.NewGuid(),
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    CreatedDate = DateTime.Now
                };
                await genericRepository.Add(emloyee);
            }
            return View("Index",genericRepository.GetAll());
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id) 
        {
            var result = await genericRepository.GetByID(new Guid(id));
            if (result != null)
            {
                var model = new EditemployeeViewModel()
                {
                    Id = result.Id.ToString(),
                    Name = result.Name,
                    Email = result.Email,
                    Department = result.Department
                };
                return View(model);
            }
            return View("Error");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditemployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = await genericRepository.GetByID(new Guid(model.Id));
                if (employee != null)
                {
                    employee.Name = model.Name;
                    employee.Email = model.Email;
                    employee.Department = model.Department;
                    employee.ModifiedDate = DateTime.Now;

                    await genericRepository.Update(employee);
                    return View("Index", genericRepository.GetAll());
                }
            }
            return View("Error");
        }

        public async Task<IActionResult> Detail(string id)
        {
            var employee = await genericRepository.GetByID(new Guid(id));
            if (employee != null)
            {
                return View(employee);
            }
            return View("Error");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            if (ModelState.IsValid)
            {
                var employee = await genericRepository.GetByID(new Guid(id));
                if (employee != null)
                {
                    await genericRepository.Delete(employee);
                    return View("Index", genericRepository.GetAll());
                }
            }
            return View("Error");
        }
    }
}