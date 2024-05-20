using Persistence.Entity;
using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public interface IStudentService
    {
        Task<List<StudentDTO>> GetAllStudentsAsync();
        Task<StudentDTO> GetStudentByIdAsync(int id);
        Task AddStudentAsync(StudentDTO student);
        Task UpdateStudentAsync(StudentDTO student);
        Task DeleteStudentAsync(int id);
    }
}
