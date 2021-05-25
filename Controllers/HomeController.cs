using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models.SchoolViewModels;
using Microsoft.AspNetCore.Authorization;

namespace ContosoUniversity.Controllers
{
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly SchoolContext _context;

        public HomeController(SchoolContext context, ILogger<HomeController> logger)
        {
               _context = context;
                 _logger = logger;
        }

        private readonly ILogger<HomeController> _logger;

       
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> About()
        {
            IQueryable<EnrollmentDateGroup> data =
                from student in _context.Students
                group student by student.EnrollmentDate into DateGroup
                select new EnrollmentDateGroup()
                {
                    EnrollemntDate = DateGroup.Key,
                    StudentCount = DateGroup.Count()
                };
            return View(await data.AsNoTracking().ToListAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
