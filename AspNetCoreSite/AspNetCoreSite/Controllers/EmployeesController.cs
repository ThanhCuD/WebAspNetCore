using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AspNetCoreSite.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ILogger logger;
        private readonly IGenericRepository<Employee> genericRepository;

        public EmployeesController(ILogger<EmployeesController> logger, IGenericRepository<Employee> genericRepository)
        {
            this.logger = logger;
            this.genericRepository = genericRepository;
        }
        [Route("Employees/Index")]
        public IActionResult Index()
        {
            try
            {
                var list =genericRepository.GetAll();
                return View(list);
            }
            catch(Exception ex)
            {
                logger.LogError(ex.ToString());
                return View("Error");
            }
            
        }
    }
}