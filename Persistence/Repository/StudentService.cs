using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Persistence.Entity;
using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Persistence.Repository
{
    public class StudentService : IStudentService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;
        public StudentService(IMapper mapper, AppDbContext appDbContext)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }
        public async Task AddStudentAsync(StudentDTO student)
        {
            var studentEntity = _mapper.Map<Student>(student);
            _appDbContext.Students.Add(studentEntity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteStudentAsync(int id)
        {
            var student = await _appDbContext.Students.FindAsync(id);
            if (student != null)
            {
                _appDbContext.Students.Remove(student);
                await _appDbContext.SaveChangesAsync();
            }

        }

        public async Task<List<StudentDTO>> GetAllStudentsAsync()
        {
            var students = await _appDbContext.Students.ToListAsync();
            return _mapper.Map<List<StudentDTO>>(students);
        }

        public async Task<StudentDTO> GetStudentByIdAsync(int id)
        {
            var student = await _appDbContext.Students.FindAsync(id);
            return _mapper.Map<StudentDTO>(student);
        }

        public async Task UpdateStudentAsync(StudentDTO student)
        {
            var studentEntity = await _appDbContext.Students.FindAsync(student.Id);
            if (studentEntity != null)
            {
                // Update properties
                studentEntity.Name = student.Name;
                studentEntity.Age = student.Age;

                // Save changes
                await _appDbContext.SaveChangesAsync();
            }
        }
    }
}