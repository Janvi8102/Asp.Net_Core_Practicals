using Practical16.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Practical16.Repositories
{
    interface IStudentRepo
    {
        Task<Student> GetAsync(int? id);
        Task<List<Student>> GetAllAsync();
        Task<Student> AddAsync(Student entity);

        Task<bool> Exists(int id);
        Task DeleteAsync(int id);
        Task UpdateAsync(Student entity);
    }
}
