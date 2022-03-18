using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Demo.Course.Models;
using WebApp.Demo.Course.Models.SchoolViewModels;
using WebApp.Demo.Infra.Context;

namespace WebApp.Demo.Course.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SchoolContext _context;

        public HomeController(ILogger<HomeController> logger, SchoolContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<ActionResult> About()
        {
                IQueryable<EnrollmentDateGroup> data =
                from student in _context.Students
                group student by student.EnrollmentDate into dateGroup
                select new EnrollmentDateGroup()
                {
                    EnrollmentDate = dateGroup.Key,
                    StudentCount = dateGroup.Count()
                };

            return View(await data.AsNoTracking().ToListAsync());
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        public IActionResult ErrorProblem() => Problem();
   

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("/error-app")]
        [Route("/error-app/{id}")]
        public IActionResult Error(string id)
        {

            switch (id)
            {
                case "404":
                    return View("NotFound", new ErrorViewModel { RequestId = id });
                case "403":
                case "401":
                default:
                    return View("Error");
            }

        }
    }
}
