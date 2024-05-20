using MediatR;
using Persistence.Repository;
using SampleAppBPC3.Commands;

namespace SampleAppBPC3.Handler
{
    public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, Unit>
    {
        private readonly IStudentService _studentService;

        public UpdateStudentCommandHandler(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public async Task<Unit> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            await _studentService.UpdateStudentAsync(request.Student);
            return Unit.Value;
        }
    }

}
