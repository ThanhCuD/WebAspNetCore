using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreSite.Models;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace AspNetCoreSite.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ILogger logger;

        public EmployeesController(ILogger logger, IGenericRepository<Employee> genericRepository)
        {
            this.logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}