using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using Practical_18.Models;
using RoleBasedBasicAuthenticationWEBAPI.Models;

namespace Practical_18.Controllers
{
    public class EmployeeController : ApiController
    {
        [BasicAuthentication]
        [MyAuthorize(Roles = "Admin")]
        [Route("api/AllMaleEmployees")]
        public HttpResponseMessage GetAllMaleEmployees()
        {
            var identity = (ClaimsIdentity)User.Identity;
           
            var ID = identity.Claims
                       .FirstOrDefault(c => c.Type == "ID").Value;
         
            var Email = identity.Claims
                      .FirstOrDefault(c => c.Type == "Email").Value;
           
            var username = identity.Name;
       
            var EmpList = new EmployeeBL().GetEmployees().Where(e => e.Gender.ToLower() == "male").ToList();
            return Request.CreateResponse(HttpStatusCode.OK, EmpList);
        }
        [BasicAuthentication]
        [MyAuthorize(Roles = "Superadmin")]
        [Route("api/AllFemaleEmployees")]
        public HttpResponseMessage GetAllFemaleEmployees()
        {
            var EmpList = new EmployeeBL().GetEmployees().Where(e => e.Gender.ToLower() == "female").ToList();
            return Request.CreateResponse(HttpStatusCode.OK, EmpList);
        }
        [BasicAuthentication]
        [MyAuthorize(Roles = "Admin,Superadmin")]
        [Route("api/AllEmployees")]
        public HttpResponseMessage GetAllEmployees()
        {
            var EmpList = new EmployeeBL().GetEmployees();
            return Request.CreateResponse(HttpStatusCode.OK, EmpList);
        }
    }
}
