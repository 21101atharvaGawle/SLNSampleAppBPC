using MediatR;
using Persistence.Repository;
using SampleAppBPC3.Commands;

namespace SampleAppBPC3.Handler
{
    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, Unit>
    {
        private readonly IStudentService _studentService;

        public CreateStudentCommandHandler(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public async Task<Unit> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            await _studentService.AddStudentAsync(request.Student);
            return Unit.Value;
        }
    }

}