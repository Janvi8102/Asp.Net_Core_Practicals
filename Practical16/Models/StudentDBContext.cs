using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Practical16.Models
{
    public class StudentDBContext : IdentityDbContext<User>
    {
        public StudentDBContext(DbContextOptions<StudentDBContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}
