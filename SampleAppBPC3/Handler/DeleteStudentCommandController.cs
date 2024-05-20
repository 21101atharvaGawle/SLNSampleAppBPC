using MediatR;
using Persistence.Repository;
using SampleAppBPC3.Commands;

namespace SampleAppBPC3.Handler
{
    public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, Unit>
    {
        private readonly IStudentService _studentService;

        public DeleteStudentCommandHandler(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public async Task<Unit> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            await _studentService.DeleteStudentAsync(request.Id);
            return Unit.Value;
        }
    }
}
