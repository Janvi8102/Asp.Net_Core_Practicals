using Microsoft.EntityFrameworkCore;
using Practical16.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Practical16.Repositories
{
    public class StudentRepo : IStudentRepo
    {
        private readonly StudentDBContext _context;

        public StudentRepo(StudentDBContext context)
        {
            _context = context;
        }
        public async Task<Student> AddAsync(Student entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetAsync(id);
            _context.Set<Student>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id)
        {
            var entity = await GetAsync(id);
            return entity != null;
        }

        public async Task<List<Student>> GetAllAsync()
        {
            return await _context.Set<Student>().ToListAsync();
        }

        public async Task<Student> GetAsync(int? id)
        {
            if (id == null)
            {
                return null;
            }
            return await _context.Set<Student>().FindAsync(id);
        }

        public async Task UpdateAsync(Student entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
