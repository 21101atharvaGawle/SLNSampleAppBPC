using MediatR;
using Persistence.Models;

namespace SampleAppBPC3.Commands
{
    public class UpdateStudentCommand : IRequest<Unit>
    {
        public StudentDTO Student { get; set; }
    }

}
