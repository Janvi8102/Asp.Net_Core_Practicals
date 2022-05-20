using Microsoft.AspNetCore.Mvc;
using Practical17.Data;
using Practical17.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Practical17.Controllers
{
    [Route("api/[Controller]")]
    public class StudentController : Controller
    {
        private StudentContext _context;
        public StudentController(StudentContext context)
        {
            _context = context;
        }
        
        //Get all student
        [HttpGet]
        public List<Student> Get()
        {
            return _context.Students.ToList();
        }
        
    }
}
