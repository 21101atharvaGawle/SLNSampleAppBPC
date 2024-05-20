using MediatR;
using Persistence.Models;
using Persistence.Repository;

namespace SampleAppBPC3.Handler
{
    public class GetAllStudentsQueryHandler : IRequestHandler<GetAllStudentsQuery, List<StudentDTO>>
    {
        private readonly IStudentService _studentService;

        public GetAllStudentsQueryHandler(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public async Task<List<StudentDTO>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            return await _studentService.GetAllStudentsAsync();
        }
    }
}
